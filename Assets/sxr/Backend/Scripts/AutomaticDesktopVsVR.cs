using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

/// <summary>
/// Attach this to your vrCamera to automatically switch to
/// headset tracking if a headset is connected. Without a headset,
/// arrow keys/WASD will move the camera and mouse will rotate
/// (See SimpleFirstPersonMovement script)
/// </summary>
public class AutomaticDesktopVsVR : MonoBehaviour
{
#if SXR_USE_AUTOVR
[SerializeField] float checkFrequency=5000; 
	float lastCheck;
    private SimpleFirstPersonMovement firstPerson;
    private bool activeXR; 
    private bool initialized; 

    void Start() {
Debug.Log("Using automatic Desktop vs VR");
        firstPerson = gameObject.GetComponent<SimpleFirstPersonMovement>();
        InputDevices.deviceConnected += CheckHeadset;
        InputDevices.deviceDisconnected += CheckHeadset;
    }

    void Update(){
        if ((!initialized && XRGeneralSettings.Instance.Manager.isInitializationComplete) || Time.time - lastCheck > checkFrequency) {
            Debug.Log("Initialization complete (XR)");
            initialized = true; 
            CheckHeadset(new InputDevice()); }}

    /// <summary>
    /// Checks all devices for head-tracking whenever a device is
    /// disconnected/connected
    /// </summary>
    /// <param name="device"></param>
    void CheckHeadset(InputDevice device)
    {
        Debug.Log("Device configuration change: " + device.name + ", " + device.manufacturer);  
        List<InputDevice> inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeadMounted, inputDevices); 
        
        if (inputDevices.Count > 0 & !activeXR) 
            StartXR();
        else 
            if (activeXR) 
                StopXR();
    }

    void StartXR() {
        if (gameObject.GetComponent<UnityEngine.SpatialTracking.TrackedPoseDriver>() == null) 
            gameObject.AddComponent<UnityEngine.SpatialTracking.TrackedPoseDriver>();
        firstPerson.enabled = false;
        gameObject.GetComponent<UnityEngine.SpatialTracking.TrackedPoseDriver>().enabled = true;
                                                                                                  
        if(!XRGeneralSettings.Instance.Manager.activeLoader){
            XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
            XRGeneralSettings.Instance.Manager.StartSubsystems(); }

        activeXR = XRGeneralSettings.Instance.Manager.activeLoader; }
 
    void StopXR() {
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete) {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            firstPerson.enabled = true;
            activeXR = false;
            if (gameObject.GetComponent<UnityEngine.SpatialTracking.TrackedPoseDriver>() != null)
                gameObject.GetComponent<UnityEngine.SpatialTracking.TrackedPoseDriver>().enabled = false;
        } }
#endif
}
