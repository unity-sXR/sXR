using UnityEngine;
using UnityEngine.UI;
//TODO - Rewrite to use x/y boundaries instead of physical walls
/// <summary>
/// /// <summary>
/// [Singleton]  *In progress* - displays a "STOP" message and plays a sound
/// Implemented with sxrSettings.safetywalls
/// </summary>
public class SafetyHandler : MonoBehaviour {
    [SerializeField] bool displayEmergency; 
    
    public void SafetyMessage(bool enable)  {  displayEmergency = enable;  }

    void Update() {
        if (displayEmergency) {
            SoundHandler.Instance.Stop();
            UI_Handler.Instance.emergencyStop.enabled = true; }
        else { UI_Handler.Instance.emergencyStop.enabled =  false; }

        displayEmergency = false; }

    // Singleton initiated on Awake()
    public static SafetyHandler Instance;
    private void Awake() {  if ( Instance == null) {Instance = this; DontDestroyOnLoad(gameObject.transform.root);} else Destroy(gameObject);  }
    
}
