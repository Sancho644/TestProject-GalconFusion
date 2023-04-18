using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlanetController : MonoBehaviour
    {
        [SerializeField] private int _shipCount = 50;
        [SerializeField] private int _shipProduction = 5;
        [SerializeField] private int _layer;
        [SerializeField] private float _productionColldown = 1;
        [SerializeField] private Text _countValue;
        [SerializeField] private SpawnComponent _spawn;
        [SerializeField] private EnterCollisionComponent _collisison;
        [SerializeField] private Transform _agentTarget;
        [SerializeField] private Material _playerPlanetMaterial;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private bool _isNeutral = false;

        private float _maxScale = 1.5f;
        private float _minScale = 0.5f;
        private float _scaleY = 0.1f;
        private bool isStart = false;

        public int ShipCount => _shipCount;
        public Transform AgentTarget => _agentTarget;

        private void Start()
        {
            _collisison.OnAction += OnCollisionAction;

            SetPlanetScale();

            if (_isNeutral)
            {
                _shipCount = Random.Range(1, _shipCount);
            }
            else
            {
                StartCoroutine(StartProduction());
            }

            _countValue.text = _shipCount.ToString();
        }

        private void OnCollisionAction()
        {
            if (_isNeutral)
            {
                _shipCount -= 1;
                _countValue.text = _shipCount.ToString();

                if (_shipCount == 0)
                {
                    gameObject.layer = _layer;
                    _renderer.material = _playerPlanetMaterial;
                    _isNeutral = false;
                    isStart = true;
                }
            }
            else
            {
                _shipCount += 1;
                _countValue.text = _shipCount.ToString();
                if(isStart)
                {
                    StartCoroutine(StartProduction());
                    isStart = false;
                }
            }
        }

        private void SetPlanetScale()
        {
            var scale = Random.Range(_minScale, _maxScale);

            gameObject.transform.localScale = new Vector3(scale, _scaleY, scale);
        }

        public void SpawnShips(Transform target)
        {
            _spawn.Spawn();
            _spawn.SetDestination(target);
        }

        private IEnumerator StartProduction()
        {
            while (enabled)
            {
                _shipCount += _shipProduction;

                _countValue.text = _shipCount.ToString();

                yield return new WaitForSeconds(_productionColldown);
            }
        }

        private void OnDestroy()
        {
            _collisison.OnAction -= OnCollisionAction;
        }
    }
}