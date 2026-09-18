using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace activity_00_tap_26_27.Core.Events
{
    public class GameActionGameEvent : IGameEvent
    {
        private GameActionType Action;
        public GameActionGameEvent(GameActionType action)
        {
            Action = action;
        }

        public GameActionType GetActionType()
        {
            return Action;
        }
    }
}
