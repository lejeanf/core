using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jeanf.core 
{ 
    [RequireComponent(typeof(Collider))]
    public class SendRaycastTargetEvent : MonoBehaviour
    {
        private Collider collider;
        public delegate void SendRaycastTarget(Collider colliders);
        public static SendRaycastTarget sendRaycastTarget;
        public static SendRaycastTarget removeRaycastTarget;
        private void Awake()
        {
            collider = this.gameObject.GetComponent<Collider>();
        }

        private void OnEnable()
        {
            sendRaycastTarget?.Invoke(collider);
        }
        private void OnDestroy()
        {
            removeRaycastTarget?.Invoke(collider);
        }
        private void OnDisable()
        {
            removeRaycastTarget?.Invoke(collider);
        }
    }
}
