using UnityEngine;

namespace Assets.Scripts
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Material _selectionMaterial;
        [SerializeField] private float _maxDistance = 20f;
        [SerializeField] private LayerMask _playerPlanetLayer;
        [SerializeField] private LayerMask _neutralPlanetLayer;

        private Material _defaultMaterial;
        private Renderer _selectionRenderer;
        private Transform _selection;
        private Transform _firstSelection = default;
        private bool _isSelect = false;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Select();
            }
        }

        private void Select()
        {
            RaycastHit hit;

            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (_isSelect)
            {
                if (Physics.Raycast(ray, out hit, _maxDistance, _playerPlanetLayer))
                {
                    SelectNeutral(hit, _selection);

                    return;
                }
            }

            if (Physics.Raycast(ray, out hit, _maxDistance, _playerPlanetLayer))
            {
                SelectPlayerPlanet(hit);

                return;
            }

            if (Physics.Raycast(ray, out hit, _maxDistance, _neutralPlanetLayer) && _isSelect)
            {
                Debug.Log("neutral");
                SelectNeutral(hit, _selection);
            }
            else if (_selectionRenderer != null)
            {
                _firstSelection = default;
                _isSelect = false;
                _selectionRenderer.material = _defaultMaterial;
                Debug.Log("deselect");
            }
        }

        private void SelectPlayerPlanet(RaycastHit hit)
        {
            _selection = hit.transform;
            _selectionRenderer = _selection.GetComponent<Renderer>();

            if (_firstSelection == _selection)
            {
                return;
            }

            _defaultMaterial = _selectionRenderer.material;

            if (_selectionRenderer != null)
            {
                _firstSelection = _selection;
                _selectionRenderer.material = _selectionMaterial;
                _isSelect = true;
                Debug.Log("select");
                return;
            }
        }

        private void SelectNeutral(RaycastHit hit, Transform select)
        {
            var selection = hit.transform;
            var selectionController = selection.GetComponent<PlanetController>();

            if (selectionController != null)
            {
                var playerPlanet = select.GetComponent<PlanetController>();
                playerPlanet.SpawnShips(selectionController.AgentTarget);
            }
        }
    }
}