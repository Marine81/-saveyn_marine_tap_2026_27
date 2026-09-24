using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using activity_00_tap_26_27.Core;
using GameLibrary;

namespace activity_00_tap_26_27.Core.Events
{
    public class RegisterGameObjectGameEvent : IGameEvent
    {
        private readonly GameObject _gameObject;
        public RegisterGameObjectGameEvent(GameObject game_object_to_register)
        {
            _gameObject = game_object_to_register;
        }

        public GameObject GetGameObject()
        {
            return _gameObject;
        }
    }
}
