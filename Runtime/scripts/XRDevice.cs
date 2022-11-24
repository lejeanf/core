using UnityEngine;
using UnityEngine.XR;

public class XRDevice
{
    private static InputDevice headDevice;
    public XRDevice()
    {
        if (headDevice == null)
        {
            headDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);
        }
    }

    /// <summary>
    /// returns true if the HMD is mounted on the users head. Returns false if the current headset does not support this feature or if the HMD is not mounted.
    /// </summary>
    /// <returns></returns>
    public static bool IsHMDMounted()
    {
        
        if (headDevice == null || headDevice.isValid == false)
        {
            headDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);
        }
        if (headDevice != null)
        {
            bool presenceFeatureSupported = headDevice.TryGetFeatureValue(CommonUsages.userPresence, out bool userPresent);
            if (headDevice.isValid && presenceFeatureSupported)
            {
                return userPresent;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        /*
        InputDevice headDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);
        if (headDevice.isValid == false) return false;
        bool userPresent = false;
        bool presenceFeatureSupported = headDevice.TryGetFeatureValue(CommonUsages.userPresence, out userPresent);

        Debug.Log(headDevice.isValid + " ** " + presenceFeatureSupported + " ** " + userPresent);

        return userPresent;
        */
    }
}
