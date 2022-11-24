using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventOnRaycast : MonoBehaviour
{

    public GameObject cameraForward;
    List<GameObject> targetList = new();
    private bool isCurrentTargetInSight = false;
    private bool lastTargetStatus = false;

    public UnityEvent TargetInSight;
    public UnityEvent TargetInOutOfSight;


    private void OnEnable()
    {
        SendRaycastTarget.sendRaycastTarget += UpdateCurrentTarget;
        SendRaycastTarget.removeRaycastTarget += RemoveCurrentTarget;
    }
    private void OnDisable() => Unsubscribe();
    private void OnDestroy() => Unsubscribe();
    void Unsubscribe()
    {
        SendRaycastTarget.sendRaycastTarget -= UpdateCurrentTarget;
        SendRaycastTarget.removeRaycastTarget -= RemoveCurrentTarget;
    }

    void UpdateCurrentTarget(Collider collider)
    {
        if (!targetList.Contains(collider.gameObject))
        {
            targetList.Add(collider.gameObject);
        }
    }
    void RemoveCurrentTarget(Collider collider)
    {
        if (targetList.Contains(collider.gameObject))
        {
            targetList.Remove(collider.gameObject);
        }
    }

    private void LateUpdate()
    {
        FireRay();
    }

    void FireRay()
    {
        if (targetList.Count <= 0 || targetList == null) return;

        Ray ray = new Ray(cameraForward.transform.position, cameraForward.transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        if ( Physics.Raycast(cameraForward.transform.position, cameraForward.transform.forward, 100)) {

            Debug.DrawRay(cameraForward.transform.position, cameraForward.transform.forward * hit.distance);
            if (hit.transform.gameObject && targetList.Contains(hit.transform.gameObject))
            {
                isCurrentTargetInSight = true;
            }
            else
            {
                isCurrentTargetInSight = false;
            }
            if (isCurrentTargetInSight)
            {
                TargetInSight?.Invoke();
                Debug.Log($"{hit.transform.gameObject.name} is in sight.");
                lastTargetStatus = true;
            }
            else
            {
                TargetInOutOfSight?.Invoke();
                Debug.Log($"{hit.transform.gameObject.name} is not in sight.");
                lastTargetStatus = false;
            }
        }
    }
}
