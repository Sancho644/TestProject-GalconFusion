using Scripts.Model;
using System;
using UnityEngine;

namespace Scripts.Components
{
    public class EnterCollisionComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;

        public event Action<GameObject> OnAction = default;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.IsInLayer(_layer))
            {
                OnAction?.Invoke(other.gameObject);
            }
        }
    }
}