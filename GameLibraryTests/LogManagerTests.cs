using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using activity_00_tap_26_27;
using activity_00_tap_26_27.Core.Events;

namespace GameLibraryTests
{
    public class LogManagerTests
    {
        public class LogFakeWriter : ILogWriter // memorise les lignes au lieu de les ecrires
        {
            public List<string> _log = new List<string>(); //list pour stocker les message recu
            public void WriteLine(string line)
            {
               _log.Add(line);
            }
        }

        [Test]
        public void LogMessage_WriteTheCorrectSentence() // ecrit les message correctement
        {
            EventManager fakeEventManager = new EventManager();
            LogFakeWriter fakeWriter = new LogFakeWriter();

            LogManager logManager = new LogManager(fakeEventManager,fakeWriter);

            string message = "message test";

            logManager.Message(message); //enregistre le message

            Assert.That(fakeWriter._log.Count, Is.EqualTo(1)); // verifie si 1 ligne

            string lignefinal = fakeWriter._log[0]; // on recup la ligne

            Assert.IsTrue(lignefinal.Contains(message)); //verifie si le message est present

            Assert.IsTrue(lignefinal.EndsWith("\n")); //verifie retour a la ligne
        }

        [Test]
        public void LogMessage_DoesNotReorderSentences() //ne reorganise pas les phrases
        {
            EventManager fakeEventManager = new EventManager();
            LogFakeWriter fakeWriter = new LogFakeWriter();

            LogManager logManager = new LogManager(fakeEventManager, fakeWriter);

            string message1 = "message 1";
            string message2 = "message 2";
            string message3 = "message 3";

            logManager.Message(message1);
            logManager.Message(message2);
            logManager.Message(message3);

            Assert.That(fakeWriter._log.Count,Is.EqualTo(3)); // s'assure qu'il y a 3 element
            Assert.IsTrue(fakeWriter._log[0].Contains(message1));
            Assert.IsTrue(fakeWriter._log[1].Contains(message2));
            Assert.IsTrue(fakeWriter._log[2].Contains(message3)); //verifie que l'ordre na pas ete change 


        }
    }
}
