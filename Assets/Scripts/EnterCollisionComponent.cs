using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnterCollisionComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask[] _layer;

        public event Action OnAction = default;

        private void OnCollisionEnter(Collision other)
        {
            foreach (var mask in _layer)
            {
                if (other.gameObject.IsInLayer(mask))
                {
                    OnAction?.Invoke();
                }
            }
        }
    }
}