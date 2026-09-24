using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using activity_00_tap_26_27.Core;
using GameLibrary;

namespace activity_00_tap_26_27.Core.Events
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
