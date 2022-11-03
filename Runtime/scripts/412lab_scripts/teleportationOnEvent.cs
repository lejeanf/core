using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportationOnEvent : MonoBehaviour
{
    public Transform ObjectToTeleport;
    public Transform targetPosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ObjectToTeleport.position = targetPosition.position;
        }
        
    }


}
