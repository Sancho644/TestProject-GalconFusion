using Scripts.Components;
using UnityEngine;

namespace Scripts.Model
{
    public class SetRandomPlayerPlanet
    {
        public void Set()
        {
            var planets = RandomSpawnComponent.Planets;
            if (planets.Length == 0) return;

            var rand = Random.Range(0, planets.Length);
            if (planets[rand].TryGetComponent(out PlanetController controller))
            {
                controller.SetPlayerPlanet();
            }
        }
    }
}