using UnityEngine;
using UnityEngine.XR;
using CommonUsages = UnityEngine.XR.CommonUsages;
using InputDevice = UnityEngine.XR.InputDevice;


public class OpenXR_Controller : ControllerVR {
    private InputDevice leftController, rightController;

    private void Update() {
        if(useController){
            leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            if (!rightController.isValid && !leftController.isValid) {
                sxr.DebugLog("Failed to find Left/Right hand, searching for other GameController");
                rightController = InputDevices.GetDeviceAtXRNode(XRNode.GameController); }

            if (!rightController.isValid && !leftController.isValid )
                sxr.DebugLog("Failed to find VR controller"); 

            InputDevice[] controllers = {rightController, leftController}; 
            foreach (var controller in controllers) 
                if(controller.isValid) {
                    bool rightSide = controller == rightController;
                    if (!controller.TryGetFeatureValue(CommonUsages.triggerButton, out buttonPressed[
                        (int) (rightSide ? ControllerButton.RH_Trigger : ControllerButton.LH_Trigger)]))
                        sxr.DebugLog("No trigger found for device: " + controller.name);
                    
                    if (!controller.TryGetFeatureValue(CommonUsages.gripButton, out buttonPressed[
                        (int) (rightSide ? ControllerButton.RH_SideButton: ControllerButton.LH_SideButton)]))
                        sxr.DebugLog("No side button found for device: " + controller.name);
                    
                    if (!controller.TryGetFeatureValue(CommonUsages.primaryButton, out buttonPressed[
                        (int) (rightSide ? ControllerButton.RH_ButtonA : ControllerButton.LH_ButtonA)]))
                        sxr.DebugLog("No primary button found for device: " + controller.name);
                    
                    if (!controller.TryGetFeatureValue(CommonUsages.secondaryButton, out buttonPressed[
                        (int) (rightSide ? ControllerButton.RH_ButtonB : ControllerButton.LH_ButtonB)]))
                        sxr.DebugLog("No secondary button found for device: " + controller.name);

                    if (controller.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool trackPadClicked)) {
                        if (trackPadClicked) {
                            Vector2 trackPad; 
                            if (!controller.TryGetFeatureValue(CommonUsages.primary2DAxis, out trackPad))
                                sxr.DebugLog("Primary 2D axis clicked but unable to set Vector2 value");
                            else {
                                if (trackPad.x > .2f)
                                    buttonPressed[(int) (rightSide
                                        ? ControllerButton.RH_TrackPadRight : ControllerButton.LH_TrackPadRight) ]= true;
                                if (trackPad.x < -.2f)
                                    buttonPressed[(int) (rightSide
                                        ? ControllerButton.RH_TrackPadLeft : ControllerButton.LH_TrackPadLeft) ]= true;
                                if (trackPad.y > .2f)
                                    buttonPressed[(int) (rightSide
                                        ? ControllerButton.RH_TrackPadUp : ControllerButton.LH_TrackPadUp) ]= true;
                                if (trackPad.y < -.2f)
                                    buttonPressed[(int) (rightSide
                                        ? ControllerButton.RH_TrackPadDown : ControllerButton.LH_TrackPadDown) ]= true; } }
                    }

                    buttonPressed[(int) ControllerButton.Trigger] =
                        buttonPressed[(int) ControllerButton.LH_Trigger] ||
                        buttonPressed[(int) ControllerButton.RH_Trigger];
                    
                    buttonPressed[(int) ControllerButton.SideButton] =
                        buttonPressed[(int) ControllerButton.LH_SideButton] ||
                        buttonPressed[(int) ControllerButton.RH_SideButton];

                    buttonPressed[(int) ControllerButton.ButtonA] =
                        buttonPressed[(int) ControllerButton.LH_ButtonA] ||
                        buttonPressed[(int) ControllerButton.RH_ButtonA];
                    
                    buttonPressed[(int) ControllerButton.ButtonB] =
                        buttonPressed[(int) ControllerButton.ButtonB] ||
                        buttonPressed[(int) ControllerButton.RH_ButtonB];
                    
                    buttonPressed[(int) ControllerButton.TrackPadDown] =
                        buttonPressed[(int) ControllerButton.LH_TrackPadDown] ||
                        buttonPressed[(int) ControllerButton.RH_TrackPadDown];
                    
                    buttonPressed[(int) ControllerButton.TrackPadLeft] =
                        buttonPressed[(int) ControllerButton.LH_TrackPadLeft] ||
                        buttonPressed[(int) ControllerButton.RH_TrackPadLeft];
                    
                    buttonPressed[(int) ControllerButton.Trigger] =
                        buttonPressed[(int) ControllerButton.LH_TrackPadRight] ||
                        buttonPressed[(int) ControllerButton.RH_TrackPadRight];
                    
                    buttonPressed[(int) ControllerButton.TrackPadUp] =
                        buttonPressed[(int) ControllerButton.LH_TrackPadUp] ||
                        buttonPressed[(int) ControllerButton.RH_TrackPadUp];
                }
        }
    }

    // Singleton initiated on Awake()
    public static OpenXR_Controller Instance;
    private void Awake() {  if ( Instance == null) {Instance = this; DontDestroyOnLoad(gameObject.transform.root);} 
        else Destroy(gameObject);  }
}
