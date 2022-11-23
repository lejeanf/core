using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class BroadcastHmdStatus : MonoBehaviour
{
    private bool isHandsVisible = false;
    public List<GameObject> hands;

    [Space(10)]
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.XRInteractorLineVisual leftRay;
    [SerializeField] private LineRenderer leftRayLineRenderer;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.XRInteractorLineVisual rightRay;
    [SerializeField] private LineRenderer rightRayLineRenderer;

    public delegate void HmdStatus(bool status);
    public static HmdStatus hmdStatus;
    
    private static bool _isHmdPresent = false;
    private static bool _lastHmdState = false;
    [Space(10)]
    public bool _isHmdActive = false;

    [SerializeField] private GameObject _playerPosition;
    [SerializeField] private GameObject _trackerOffset;

    private void Awake()
    {
        _isHmdActive = false;
        foreach (GameObject g in hands)
        {
            g.SetActive(false);
        }
        setHMD();
    }
    private void FixedUpdate()
    {
        if (_isHmdActive == isHmdPresent) return;
        setHMD();
    }

    void setHMD() 
    {
        Vector3 _playerPos = _playerPosition.transform.position;

        if (!isHmdPresent) // si pas de casque
        {
            _lastHmdState = true;
            if (isHandsVisible)
            {
                foreach (GameObject g in hands)
                {
                    g.SetActive(false);
                }
                isHandsVisible = false;
                leftRay.enabled = rightRay.enabled = leftRayLineRenderer.enabled = rightRayLineRenderer.enabled = false;
            }
        }
        else
        {
            _lastHmdState = false;
            if (!isHandsVisible)
            {
                foreach (GameObject g in hands)
                {
                    g.SetActive(true);
                }
                isHandsVisible = true;
                leftRay.enabled = rightRay.enabled = leftRayLineRenderer.enabled = rightRayLineRenderer.enabled = true;
            }
        }

        _isHmdActive = isHmdPresent;

        hmdStatus?.Invoke(isHmdPresent);
    }

    public static bool isHmdPresent
    {
        get
        {
            var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
            SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
            foreach (var xrDisplay in xrDisplaySubsystems)
            {
                if (xrDisplay.running)
                {
                    _isHmdPresent = true;
                    return _isHmdPresent;
                }
            }
            return false;
        }
        set
        {
            _isHmdPresent = value;
        }
    }
}
