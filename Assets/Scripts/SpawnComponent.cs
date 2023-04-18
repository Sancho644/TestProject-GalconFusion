using Scripts;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private PlanetController _planet;
        [SerializeField] private int _count = 0;

        public static List<GameObject> Ships = new List<GameObject>();

        public void Spawn()
        {
            _count = _planet.ShipCount / 2;

            if (_prefab != null)
            {
                for (int i = 0; i < _count; i++)
                {
                    Ships.Add(Instantiate(_prefab, _spawnPosition));
                    Ships[i].gameObject.SetActive(true);
                }
            }
        }

        public void SetDestination(Transform target)
        {
            for (int i = 0; i < Ships.Count; i++)
            {
                if (Ships[i].TryGetComponent(out ShipController controller))
                {
                    controller.SetTarget(target);
                }
            }
        }
    }
}