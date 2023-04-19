using Scripts.Components;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Model
{
    public class PlanetController : MonoBehaviour
    {
        [SerializeField] private int _layer;
        [SerializeField] private Text _countValue;
        [SerializeField] private SpawnComponent _spawn;
        [SerializeField] private Transform _agentTarget;
        [SerializeField] private Renderer _renderer;

        private bool _isNeutral = true;
        private int _index = 0;
        private int _shipCount = 0;
        private readonly float _minScale = 0.5f;
        private readonly float _maxScale = 1.5f;

        public int Index => _index;
        public Transform AgentTarget => _agentTarget;

        private void Start()
        {
            SetPlanetSize();

            _shipCount = DefsFacade.I.Settings.PlayerShips;

            if (_isNeutral)
            {
                _shipCount = Random.Range(1, _shipCount);
            }

            SetShipsValue();
        }

        private void SetPlanetSize()
        {
            var scale = Random.Range(_minScale, _maxScale);

            gameObject.transform.localScale = new Vector3(scale, scale, scale);
        }

        public void OnCollisionAction()
        {
            if (_isNeutral)
            {
                _shipCount -= 1;
                SetShipsValue();

                if (_shipCount == 0)
                {
                    SetPlayerSettings();
                    StartCoroutine(StartProduction());
                }
            }
            else
            {
                _shipCount += 1;
                SetShipsValue();
            }
        }

        public void SpawnShips(Transform target, int index)
        {
            _shipCount = _shipCount / 2;
            _spawn.Spawn(_shipCount);
            _spawn.SetDestination(target, index);
            SetShipsValue();
        }

        public void SetIndex(int value)
        {
            _index = value;
        }

        public void SetPlayerPlanet()
        {
            SetPlayerSettings();
            _shipCount = 50;
            SetShipsValue();

            StartCoroutine(StartProduction());
        }

        private void SetPlayerSettings()
        {
            gameObject.layer = _layer;
            _renderer.material = DefsFacade.I.Settings.PlayerPlanetMaterial;
            _isNeutral = false;
        }

        private void SetShipsValue()
        {
            _countValue.text = _shipCount.ToString();
        }

        private IEnumerator StartProduction()
        {
            while (enabled)
            {
                _shipCount += DefsFacade.I.Settings.ShipProduction;
                _countValue.text = _shipCount.ToString();

                yield return new WaitForSeconds(DefsFacade.I.Settings.ProductionColldown);
            }
        }
    }
}