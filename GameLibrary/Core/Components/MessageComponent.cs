using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary;

namespace activity_00_tap_26_27.Core.Components
{
    public class MessageComponent : Component
    {
        private string _message;
        private GameObject _owner;

        public MessageComponent(string message)
        {
            _message = message;
        }

        public string GetMessage()
        {
            return _message;
        }

        public void SetOwner(GameObject Owner)
        {
            _owner = Owner;
        }

        public GameObject GetOwner()
        {
            return _owner;
        }
    }
}
