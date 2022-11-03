using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

}
