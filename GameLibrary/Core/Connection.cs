using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using activity_00_tap_26_27.Core.Components;
using GameLibrary;

namespace activity_00_tap_26_27.Core
{
    public class Connection
    {
        private LocationComponent _destination;
        private int _distance;

        public Connection(LocationComponent destination, int distance)
        {
            _destination = destination;
            _distance = distance;
        }

        public LocationComponent GetDestination()
        {
            return _destination;
        }
        public int GetDistance()
        {
            return _distance;
        }
    }
}
