using Scripts.Components;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Model
{
    public class ShipController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnterCollisionComponent _collision;

        private int _planetIndex = 0;

        private void Start()
        {
            _collision.OnAction += OnCollisionAction;
        }

        private void OnCollisionAction(GameObject go)
        {
            if (go.TryGetComponent(out PlanetController controller))
            {
                if (controller.Index != _planetIndex) return;

                controller.OnCollisionAction();

                Destroy(gameObject);
            }
        }

        public void SetTarget(Transform target, int index)
        {
            _agent.destination = target.position;
            _planetIndex = index;
        }

        private void OnDestroy()
        {
            _collision.OnAction -= OnCollisionAction;
            SpawnComponent.Ships.Remove(gameObject);
        }
    }
}