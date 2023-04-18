using Assets.Scripts;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts
{
    public class ShipController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnterCollisionComponent _collision;

        private void Awake()
        {
            _collision.OnAction += OnCollisionAction;
        }

        private void OnCollisionAction()
        {
            SpawnComponent.Ships.Remove(gameObject);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _collision.OnAction -= OnCollisionAction;
        }

        public void SetTarget(Transform target)
        {
            _agent.destination = target.position;
        }
    }
}