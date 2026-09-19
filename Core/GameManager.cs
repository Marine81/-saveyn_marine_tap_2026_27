using System.Collections.Generic;
using activity_00_tap_26_27.Core.Components;
using activity_00_tap_26_27.Core.Events;
using activity_00_tap_26_27.Core.States;

namespace activity_00_tap_26_27.Core
{
    public class GameManager
    {
        private LocationComponent _currentLocation;

        private EventManager _eventManager;
        private readonly List<GameObject> _gameObjectTable = new List<GameObject>();

        private bool _shouldQuit = false;

        private int _indexSelection = -1; // pour changer la selection

        private IState _currentState;

        public GameManager(EventManager event_manager)
        {
            _eventManager = event_manager;
            
            GameObject world = new GameObject("world");
            LocationComponent worldLocation = new LocationComponent("world");
            worldLocation.SetOwner(world);
            world.AddComponent(worldLocation);

            GameObject daisyTown = new GameObject("daisy Town");
            LocationComponent daisyLocation = new LocationComponent("Daisy town");
            daisyLocation.SetOwner(daisyTown);
            daisyTown.AddComponent(worldLocation);

            GameObject silverMine = new GameObject("silver Mine Dungeon");
            LocationComponent silverMineLocation = new LocationComponent("Silver Mine Dungeon");
            silverMineLocation.SetOwner(silverMine);
            silverMine.AddComponent(worldLocation);

            MessageComponent message_component = new MessageComponent("Welcome to daisy town !");
            message_component.SetOwner(daisyTown);
            daisyTown.AddComponent(message_component);

            MessageComponent messageComponent2 = new MessageComponent("Welcome to the mine");
            messageComponent2.SetOwner(silverMine);
            silverMine.AddComponent(messageComponent2);

            worldLocation.ConnectTo(daisyLocation,10);
            worldLocation.ConnectTo(silverMineLocation,15);

            world.SetIsActive(true);
            daisyTown.SetIsActive(true);
            silverMine.SetIsActive(true);

            _currentLocation = worldLocation;

            //definit etat depart puis entre dedans
            _currentState = new TitleState(this);
            _currentState.Enter();

            //creation gameobject test
            GameObject gameObjectTest = new GameObject("test");
            RegisterGameObjectGameEvent registerEvent = new RegisterGameObjectGameEvent(gameObjectTest);
            _eventManager.TriggerDelayedEvent(registerEvent);
           
        }

        public bool GetShouldQuit()
        {
           return _shouldQuit;
        }

        public void SetShouldQuit(bool shouldQuit)
        {
            _shouldQuit = shouldQuit;
        }

        public LocationComponent GetCurrentLocation()
        {
            return _currentLocation;
        }

        public int GetSelectionIndex()
        {
            return _indexSelection;
        }


        public void SetCurrentLocation(LocationComponent location_component)
        {
            _currentLocation = location_component;
        }

        public void FixedUpdate(float fixed_elapsed_time)
        {
            for (int object_index = 0; object_index < _gameObjectTable.Count; object_index++)
            {
                GameObject game_object = _gameObjectTable[object_index];

                if (game_object.GetIsActive())
                {
                    game_object.FixedUpdate(fixed_elapsed_time);
                }
            }
        }

        public void Update(float elapsed_time)
        {
            for (int object_index = 0; object_index < _gameObjectTable.Count; object_index++)
            {
                GameObject game_object = _gameObjectTable[object_index];

                if (game_object.GetIsActive())
                {
                    game_object.Update(elapsed_time);
                }
            }
        }

        public void ChangeState(IState newState)
        {
            if(_currentState != null)//Si deja un etat
            {
                _currentState.Exit();
            }

            _currentState = newState;//change etat

            _currentState.Enter();
        }

        public EventManager GetEventManager()
        {
            return _eventManager;
        }

        public IState GetCurrentState()
        {
            return _currentState;
        }

        public void SetSelectionIndex(int new_index)
        {
            _indexSelection = new_index;
        }
    }
}
