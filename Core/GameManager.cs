using System.Collections.Generic;
using activity_00_tap_26_27.Core.Components;
using activity_00_tap_26_27.Core.Events;

namespace activity_00_tap_26_27.Core
{
    public class GameManager
    {
        private LocationComponent _currentLocation;

        private EventManager _eventManager;
        private readonly List<GameObject> _gameObjectTable = new List<GameObject>();

        private bool _shouldQuit = false;

        private int _indexSelection = -1; // pour changer la selection

        public GameManager(EventManager event_manager)
        {
            _eventManager = event_manager;
            _eventManager.RegisterToEvent<GameActionGameEvent>(Navigation);
            
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


            //creation gameobject test
            GameObject gameObjectTest = new GameObject("test");
            RegisterGameObjectGameEvent registerEvent = new RegisterGameObjectGameEvent(gameObjectTest);
            _eventManager.TriggerDelayedEvent(registerEvent);
           
        }

        public bool GetShouldQuit()
        {
           return _shouldQuit;
        }

        public LocationComponent GetCurrentLocation()
        {
            return _currentLocation;
        }

        public int GetSelectionIndex()
        {
            return _indexSelection;
        }

        private void Navigation(IGameEvent base_event)// le param contient l'action du joueur
        {
            GameActionGameEvent game_event = (GameActionGameEvent)base_event;
            GameActionType action = game_event.GetActionType();// demander explication
            int maxIndex = _currentLocation.GetConnectionCount() - 1;

            if(action == GameActionType.NAVIGATE_DOWN)
            {
                if(_indexSelection == -1) // verifie si la selection est a -1 (encore aucune destination selectione)
                {
                    _indexSelection = 0; // met sur la premiere destination de la liste
                }
                else if(_indexSelection < maxIndex) 
                {
                    _indexSelection++; //descend sans depasser la liste puisqu'on est pas tout en dessous
                }
            }

            else if (action == GameActionType.NAVIGATE_UP)
            {
                if (_indexSelection == -1)
                {
                    _indexSelection = 0; 
                }
                else if (_indexSelection > 0)// quand un index est deja selectione
                {
                    _indexSelection--; // monte sans depasser la liste
                }
            }
            else if( action == GameActionType.CANCEL)
            {
                _indexSelection = -1; // annule selection et revient etat aucune destination selectione
            }

            else if (action == GameActionType.CONFIRM) //entre dans le monde selectione
            {
               if(_indexSelection != -1)//verifie si on selectione quelque chose
                {
                    Connection chosenConection = _currentLocation.GetConnection(_indexSelection);

                    _currentLocation= chosenConection.GetDestination();// entre dans la destination
                    _indexSelection = -1; // reinitialise l'index
                }
            }

            else if ( action == GameActionType.ESCAPE)
            {
                _shouldQuit = true;
            }
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
    }
}
