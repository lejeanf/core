using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jeanf.core{
    public class TeleportOnEvent : MonoBehaviour
    {
        public GameObject objectToTeleport;
        public GameObject teleportTargetPosition;
        public bool ignoreManualTeleportTargetPosition = true;

        public delegate void GetTeleportTarget(string sceneName, List<Transform> targetTransform);
        public static event GetTeleportTarget getTeleportTarget;

        private void OnEnable()
        {
            PhaseManager.teleport += ctx => Teleport(ctx);
        }
        private void OnDisable() => Unsubscribe();
        private void OnDestroy() => Unsubscribe();
        void Unsubscribe()
        {
            PhaseManager.teleport -= ctx => Teleport(ctx);
        }

        public void Teleport(string sceneName)
        {
            try {
                List<Transform> teleportTargets = new();
                Transform chosenTarget = null;
                if (ignoreManualTeleportTargetPosition)
                {
                    getTeleportTarget?.Invoke(sceneName, teleportTargets);
                    chosenTarget = teleportTargets[0];
                }
                else
                {
                    chosenTarget = teleportTargetPosition.transform;
                }

                objectToTeleport.transform.position = chosenTarget.position;
                objectToTeleport.transform.rotation = chosenTarget.rotation;
            }
            catch
            {
                Debug.Log("error with teleportation.");
            }
            
        }
    }

}
