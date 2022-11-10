using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoPeeking : MonoBehaviour
{
    [SerializeField] LayerMask collisionLayer;
    [SerializeField] float sphereCheckSize = .15f;
    [Range(1f, 10f)]
    [SerializeField] float fadeSpeed = 4.0f;

    private Material cameraFadeMat;
    private bool isCameraFadeOut = true;

    private void Awake() 
    {
        cameraFadeMat = this.GetComponent<Renderer>().material;
        CameraFade(0f);
        isCameraFadeOut = true;
    }

    void Update()
    {
        if (Physics.CheckSphere(transform.position, sphereCheckSize, collisionLayer, QueryTriggerInteraction.Ignore))
        {
            CameraFade(1f);
            isCameraFadeOut = true;
        }
        else 
        {
            if (!isCameraFadeOut)
                return;

            CameraFade(0f);
        }
    }

    public void CameraFade(float targetAlpha) 
    {
        var fadeValue = Mathf.MoveTowards(cameraFadeMat.GetFloat("_AlphaValue"), targetAlpha, Time.deltaTime * fadeSpeed);
        cameraFadeMat.SetFloat("_AlphaValue", fadeValue);

        if (fadeValue <= 0.01f)
            isCameraFadeOut = false; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 0f, .75f);
        Gizmos.DrawSphere(transform.position, sphereCheckSize);
    }
}
