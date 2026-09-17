using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace activity_00_tap_26_27.Events
{
    public class LogManager
    {
        private string text = "../../../../debug.log.";

        public LogManager(EventManager event_manager)
        {
            event_manager.RegisterToEvent<LogMessageGameEvent>(OnLogGameEvent);
        }
        public void Message(string message)
        {
            File.WriteAllText(text,$"{DateTime.Now}{message}");
        }

        public void OnLogGameEvent(IGameEvent game_event)
        {
          if(game_event is LogMessageGameEvent log_message)
            {
                Message(log_message.GetMessage());
            }
        }
    }

    
}
