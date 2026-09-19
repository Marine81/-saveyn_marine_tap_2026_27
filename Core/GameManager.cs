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

        public GameManager(EventManager event_manager)
        {
            _eventManager = event_manager;
            GameObject world = new GameObject("world");
            GameObject daisyTown = new GameObject("daisy Town");
            GameObject silverMine = new GameObject("silver Mine Dungeon");

            LocationComponent worldLocation = new LocationComponent("world");
            world.AddComponent(worldLocation);

            LocationComponent daisyLocation = new LocationComponent("Daisy town");
            daisyTown.AddComponent(worldLocation);
            LocationComponent silverMineLocation = new LocationComponent("Silver Mine Dungeon");
            silverMine.AddComponent(worldLocation);

            worldLocation.ConnectTo(daisyLocation,10);
            worldLocation.ConnectTo(silverMineLocation,15);

            world.SetIsActive(true);
            daisyTown.SetIsActive(true);
            silverMine.SetIsActive(true);

            _currentLocation = worldLocation;
    
            GameObject gameObjectTest = new GameObject("test");
            RegisterGameObjectGameEvent registerEvent = new RegisterGameObjectGameEvent(gameObjectTest);
            _eventManager.TriggerDelayedEvent(registerEvent);
            //creation gameobject test
        }

        public bool GetShouldQuit()
        {
           return _shouldQuit;
        }

        public LocationComponent GetCurrentLocation()
        {
            return _currentLocation;
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
