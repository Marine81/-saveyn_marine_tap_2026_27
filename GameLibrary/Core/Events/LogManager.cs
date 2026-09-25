using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary;


namespace activity_00_tap_26_27.Core.Events
{
    public class LogManager
    {
        private string text = "../../../../debug.log.";

        private readonly ILogWriter _logWriter;

        public LogManager(EventManager event_manager, ILogWriter logWriter)
        {
            event_manager.RegisterToEvent<LogMessageGameEvent>(OnLogGameEvent);
            _logWriter = logWriter;
        }
        public void Message(string message)
        {
            string final_message = $"{DateTime.Now}{message}\n";
            _logWriter.WriteLine(final_message);
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
