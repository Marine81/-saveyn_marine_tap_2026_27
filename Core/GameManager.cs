using System.Collections.Generic;
using activity_00_tap_26_27.Core.Components;
using activity_00_tap_26_27.Core.Events;

namespace activity_00_tap_26_27.Core
{
    public class GameManager
    {
        private EventManager _eventManager;
        private readonly List<GameObject> _gameObjectTable = new List<GameObject>();

        private bool _shouldQuit = false;

        public GameManager(EventManager event_manager)
        {
            GameObject world = new GameObject("world");
            GameObject daisyTown = new GameObject("daisy Town");

            LocationComponent worldLocation = new LocationComponent("world");
            world.AddComponent(worldLocation);

            LocationComponent daisyLocation = new LocationComponent("Daisy town");
            daisyTown.AddComponent(worldLocation);

            worldLocation.ConnectTo(daisyLocation,10);

            world.SetIsActive(true);
            daisyTown.SetIsActive(true);
    
            GameObject gameObjectTest = new GameObject("test");
            RegisterGameObjectGameEvent registerEvent = new RegisterGameObjectGameEvent(gameObjectTest);
            _eventManager.TriggerDelayedEvent(registerEvent);
            //creation gameobject test
        }

        public bool GetShouldQuit()
        {
           return _shouldQuit;
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
