using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace activity_00_tap_26_27.Core.Components
{
    public class LocationComponent : Component
    {
        private string _locationName;
        private List<Connection> _destinations;
        public LocationComponent(string location_name) 
        {
            _locationName = location_name;
            _destinations = new List<Connection>();
        }

        public int GetConnectionCount() //compteur
        {
            return _destinations.Count;
        }

        public Connection GetConnection(int index) //acces avec index
        {
            if (index <0 || index >= _destinations.Count)
            {
                throw new ArgumentOutOfRangeException("index invalid");
            }

            return _destinations[index];
        }

        public void ConnectTo(LocationComponent other_location, int distance)
        {
            _destinations.Add(new Connection(other_location,distance)); 
            other_location._destinations.Add(new Connection(this,distance)); //ajout du lieu liste voisin
        }

        public String GetLocationName()
        {
            return _locationName;
        }
    }
}
