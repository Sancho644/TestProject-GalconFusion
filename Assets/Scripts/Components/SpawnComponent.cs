using Scripts.Model;
using Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private PlanetController _planet;
        
        private int _defaultShipCount;

        public static List<GameObject> Ships = new List<GameObject>();

        public void Spawn(int count)
        {
            if (_prefab != null)
            {
                _defaultShipCount = Ships.Count;

                for (int i = 0; i < count; i++)
                {
                    Ships.Add(SpawnUtils.Spawn(_prefab, _spawnPosition.position));
                    Ships[i].SetActive(true);
                }
            }
        }

        public void SetDestination(Transform target, int index)
        {
            for (int i = _defaultShipCount; i < Ships.Count; i++)
            {
                if (Ships[i].TryGetComponent(out ShipController controller))
                {
                    Ships[i].SetActive(true);
                    controller.SetTarget(target, index);
                }
            }
        }
    }
}