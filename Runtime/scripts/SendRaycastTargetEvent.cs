using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SendRaycastTarget : MonoBehaviour
{
    private Collider collider;
    public delegate void SendRaycastTargetEvent(Collider colliders);
    public static SendRaycastTargetEvent sendRaycastTarget;
    public static SendRaycastTargetEvent removeRaycastTarget;
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
