using Scripts.Model;
using Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace Scripts.Components
{
    public class RandomSpawnComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Vector3 _volume;

        private readonly SetRandomPlayerPlanet _setPlanet = new SetRandomPlayerPlanet();
        private Collider[] _colliders;
        private bool _checkCollision;
        private int _countOfObjects = 0;

        public static GameObject[] Planets;

        public void Awake()
        {
            _countOfObjects = DefsFacade.I.Settings.PlanetCount;

            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            Planets = new GameObject[_countOfObjects];

            int i = 0;

            while (i < _countOfObjects)
            {
                var position = new Vector3(Random.Range(_spawnPoint.position.x - _volume.x, _spawnPoint.position.x + _volume.x),
                _spawnPoint.position.y,
                Random.Range(_spawnPoint.position.z - _volume.z, _spawnPoint.position.z + _volume.z));

                _checkCollision = CheckSpawnPoint(position, _prefab.transform.localScale);

                if (_checkCollision)
                {
                    Planets[i] = SpawnUtils.Spawn(_prefab, position);
                    Planets[i].SetActive(true);

                    if (Planets[i].TryGetComponent(out PlanetController controller))
                    {
                        controller.SetIndex(i);
                    }

                    i++;

                    yield return null;
                }
                else
                {
                    yield return null;
                }
            }

            _setPlanet.Set();
        }

        private bool CheckSpawnPoint(Vector3 position, Vector3 sizeCollider)
        {
            _colliders = Physics.OverlapBox(position, sizeCollider);

            return _colliders.Length <= 1;
        }
    }
}