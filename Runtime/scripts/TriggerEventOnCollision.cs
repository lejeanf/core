using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace jeanf.core
{
    public class RriggerEventOnCollision : MonoBehaviour
    {
        //[Tooltip("This is the object that detects collisions with the objects in the scene that have the layer [collisionTrigger]")]
        //public GameObject colliderObject;
        [Tooltip("This the layer of detection [collisionTrigger]")]
        public LayerMask triggerLayer;
        public UnityEvent triggerEvent;
        public delegate void CollisionEvent();
        public static CollisionEvent collisionEvent;

        private void OnTriggerEnter(Collider collider)
        {
            Debug.Log($"Collision with {collider.gameObject.name} detected");
            if ((triggerLayer.value & (1 << collider.gameObject.layer)) > 0)
            {
                Debug.Log("Event triggered");
                triggerEvent?.Invoke();
                collisionEvent?.Invoke();
            }
        }
    }
}