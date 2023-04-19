using UnityEngine;

namespace Scripts.Model
{
    [CreateAssetMenu(menuName = "Data/GameSettings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private int _planetCount = 15;
        [SerializeField] private int _playerShips = 50;
        [SerializeField] private int _shipProduction = 5;
        [SerializeField] private float _productionColldown = 1;
        [SerializeField] private Material _playerPlanetMaterial;
        [SerializeField] private Material _selectionMaterial;

        public int PlanetCount => _planetCount;
        public int PlayerShips => _playerShips;
        public int ShipProduction => _shipProduction;
        public float ProductionColldown => _productionColldown;
        public Material PlayerPlanetMaterial => _playerPlanetMaterial;
        public Material SelectionMaterial => _selectionMaterial;

    }
}