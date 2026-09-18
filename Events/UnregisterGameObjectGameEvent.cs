using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace activity_00_tap_26_27.Events
{
    public class UnregisterGameObjectGameEvent : IGameEvent
    {
        private readonly GameObject _gameObject;
        public UnregisterGameObjectGameEvent(GameObject game_object_to_unregister)
        {
            _gameObject = game_object_to_unregister;
        }

        public GameObject GetGameGameObject()
        {
            return _gameObject;
        }
    }
}
