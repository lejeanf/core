using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jeanf.core{
    public class TeleportOnEvent : MonoBehaviour
    {
        public GameObject objectToTeleport;
        public GameObject manualTeleportTargetPosition;
        public bool ignoreManualTeleportTargetPosition = true;
        private void OnEnable()
        {
            SendTeleportTarget.teleportPlayer += ctx => Teleport(ctx);
        }
        private void OnDisable() => Unsubscribe();
        private void OnDestroy() => Unsubscribe();
        void Unsubscribe()
        {
            SendTeleportTarget.teleportPlayer -= ctx => Teleport(ctx);
        }

        public void Teleport(Transform teleportTarget)
        {
            Transform chosenTarget = null;
            if (ignoreManualTeleportTargetPosition)
            {
                chosenTarget = teleportTarget;
            }
            else
            {
                chosenTarget = manualTeleportTargetPosition.transform;
            }
            objectToTeleport.transform.position = chosenTarget.position;
            objectToTeleport.transform.rotation = chosenTarget.rotation;
        }

    }

}
