using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace jeanf.core{
    public class SendTeleportTarget : MonoBehaviour
    {
        [SerializeField] string currentScene;

        public delegate void TeleportPlayer(Transform teleportTarget);
        public static event TeleportPlayer teleportPlayer;

        private void Awake()
        {
            teleportPlayer?.Invoke(this.transform);
        }
        private void OnDrawGizmos()
        {
           Gizmos.color = new Color(.2f, .6f, .2f, .5f);
           Gizmos.DrawSphere(transform.position, .25f);
        }
    }

}
