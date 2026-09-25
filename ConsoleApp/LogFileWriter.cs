using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace activity_00_tap_26_27
{
    public class LogFileWriter : ILogWriter
    {
        //s'occupe d'ecrire dans le fichier a la place du logmanager
        private string _logFilePath;

        public LogFileWriter(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void WriteLine(string line)
        {
            File.AppendAllText(_logFilePath, line);
        }
    }
}
