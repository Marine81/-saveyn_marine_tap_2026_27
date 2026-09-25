using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using activity_00_tap_26_27.Core.Components;
using activity_00_tap_26_27.Core;

namespace GameLibraryTests
{
    public class ConnectionTests
    {
        private LocationComponent CreateLocation(string name)
        {
            GameObject gameObject = new GameObject(name);
            LocationComponent locationComponent = new LocationComponent(name);

            locationComponent.SetOwner(gameObject);//liaison
            gameObject.AddComponent(locationComponent);//ajout du component

            return locationComponent;
        }

        [Test]
        public void GetDestination_ReturnsConstructionDestination()
        {
            LocationComponent destination = CreateLocation("Daisy Town");
            Connection connection = new Connection(destination, 3);

            Assert.That(connection.GetDestination(), Is.EqualTo(destination));
            Assert.That(connection.GetDistance(), Is.EqualTo(3.0f).Within(0.001f));
        }
    }
}
