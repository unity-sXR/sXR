using UnityEngine;

/// <summary>
/// Contains static calls to allow for simple, easy to understand access to sxr's functions...
/// Designed to allow for typing "sxr." + whatever it is you're trying to do into your IDE.
/// For example, typing "sxr.move" should show options to move objects. Optional parameters
/// are intentionally specified as separate method invocations meaning you can leave them out
/// to use the default value, or specify the variable to use another value. (Do not use the ":"
/// operator for optional parameters.)
/// </summary>
public static class sxr {
// ****   INPUT DEVICE COMMANDS   ****
    /// <summary>
    /// Offers a combined "Trigger" across joystick trigger, vr controller trigger, left mouse click, and keyboard spacebar
    /// </summary>
    /// <param name="frequency">Pause between returning true when trigger/spacebar is held. Optional, default
    /// value is 0.5 seconds.</param>
    /// <returns>true if [frequency] seconds has passed since last trigger and trigger/space is pressed</returns>
    public static bool GetTrigger(float frequency) { return ExperimentHandler.Instance.GetTrigger(frequency);}
    public static bool GetTrigger() { return GetTrigger(0.5f);}

    /// <summary>
    /// Will return true at specified [frequency] if the specified key
    /// is held
    /// </summary>
    /// <param name="whichKey">Choose with KeyCode.[Key]. e.g. KeyCode.A, KeyCode.Space</param>
    /// <param name="frequency">Pause between returning true values when a key is held (in seconds).
    /// Optional, default is 0.5 seconds</param>
    /// <returns></returns>
    public static bool KeyHeld(KeyCode whichKey, float frequency) { return KeyboardHandler.Instance.KeyHeld(whichKey, frequency); }
    public static bool KeyHeld(KeyCode whichKey) { return KeyHeld(whichKey, .5f);}

    /// <summary>
    /// Returns true on the initial frame that KeyCode [whichKey] is pressed
    /// </summary>
    public static bool InitialKeyPress(KeyCode whichKey) { return Input.GetKeyDown(whichKey);}
    /// <summary>
    /// Returns true on the initial frame that KeyCode [whichKey] is released
    /// </summary>
    public static bool KeyReleased(KeyCode whichKey) { return Input.GetKeyUp(whichKey);}


    /// <summary>
    /// Returns the direction the joystick is being pushed in. Can be any of the 9 possible
    /// directions  
    /// </summary>
    /// <returns>JoystickDirection (Left, Right, Up, Down, UpLeft, UpRight, DownLeft, DownRight, None</returns>
    public static JoystickHandler.JoyStickDirection GetJoystickDirection() { return GetJoystickDirection();}

    
    /// <summary>
    /// Marks the time  of a "true" value for each check of the  button.
    /// Returns true if ControllerButton [whichButton] is pressed and the
    /// specified amount of time (frequency) has passed. 
    /// </summary>
    /// <param name="whichButton">Use sxr.ControllerButton to choose which button to listen for.
    /// e.g. sxr.ControllerVR.LH_TriggerPressed for left handed trigger. Controller options
    /// without RH/LH return true if either hand is pressed</param>
    /// <param name="frequency">Delay before returning true when button is held. Optional, default is 0.5 seconds</param>
    /// <returns>true if button VR controller button is pressed and delay has passed</returns>
    public static bool CheckController(ControllerButton whichButton, float frequency) {
        #if SXR_USE_STEAMVR
        ControllerVR controller = SteamControllerVR.Instance; 
        #else
        ControllerVR controller = OpenXR_Controller.Instance; 
        #endif 
        controller.EnableControllers();
        
        if (controller.buttonPressed[(int) whichButton]) {
            if (Time.time - controller.buttonTimers[(int) whichButton] > frequency) {
                sxr.DebugLog("Controller Button: " + whichButton + " pressed after delay of: " + frequency);
                controller.buttonTimers[(int) whichButton] = Time.time; 
                return true; }

            sxr.DebugLog("Controller Button: " + whichButton + "pressed before delay of: " + frequency); }
        else 
            sxr.DebugLog("Controller Button: " + whichButton + "not pressed");
        return false; }
    public static bool CheckController(ControllerButton whichButton) {
        return CheckController(whichButton, 0.5f); }

// ****  EXPERIMENT FLOW COMMANDS   ****
    /// <summary>
    /// Call at beginning of experiment to set data path
    /// </summary>
    /// <param name="experimentName"></param>
    /// <param name="subjectNumber"></param>
    public static void StartExperiment(string experimentName, int subjectNumber) {ExperimentHandler.Instance.StartExperiment(experimentName, subjectNumber); }

    
// ****   DATA RECORDING COMMANDS   ****
    /// <summary>
    /// Start recording the camera position every time sxrSettings.recordFrame==currentFrame. Updates automatically
    /// based on sxrSettings.recordFrequency or can be manually called by setting sxrSettings.recordFrame=[frame to record]
    /// Default output location is "Assets/Experiments/[experimentName]/[subject_number]_camera_tracker.csv
    /// </summary>
    /// <param name="recordGaze"></param> Optional (default=false), if true will record gaze screenFixation point
    public static void StartRecordingCameraPos(bool recordGaze) { CameraTracker.Instance.StartRecording(recordGaze); }
    public static void StartRecordingCameraPos() { CameraTracker.Instance.StartRecording(false);}
    
    /// <summary>
    /// Used with StartRecordingCameraPos. Used to pause recording between trials or during rest periods
    /// </summary>
    public static void PauseRecordingCameraPos() {CameraTracker.Instance.PauseRecording();}

    /// <summary>
    /// Start recording the eyetracker information every time sxrSettings.recordFrame==currentFrame. Updates automatically
    /// based on sxrSettings.recordFrequency or can be manually called by setting sxrSettings.recordFrame=[frame to record]
    /// Default output location is "Assets/Experiments/[experimentName]/[subject_number]_camera_tracker.csv
    /// </summary>
    public static void StartRecordingEyeTrackerInfo() { GazeHandler.Instance.StartRecording(); }
    /// <summary>
    /// Used with StartRecordingEyeTrackerInfo. Used to pause recording between trials or during rest periods
    /// </summary>
    public static void PauseRecordingEyeTrackerInfo() {GazeHandler.Instance.PauseRecording();}
    
    /// <summary>
    /// *In progress* Adds a tracker to an object to record it's location every time sxrSettings.recordFrame==currentFrame.
    /// Updates automatically based on sxrSettings.recordFrequency or can be manually called by setting
    /// sxrSettings.recordFrame=[frame to record]
    /// Default output location is "Assets/Experiments/[experimentName]/[subject_number]_[object_name]_tracker.csv
    /// </summary>
    /// <param name="name"></param>
    /// <param name="active"></param>
    public static void StartTrackingObject(string name)
    { } // TODO Add tracker to tracked objects } public static void TrackObject(string name){TrackObject(name, true);}
    /// <summary>
    /// Used with StartTrackingObject. Used to pause recording between trials or during rest periods
    /// </summary>
    public static void PauseTrackingObject() {}
   
// ****   OBJECT MANIPULATION   ****
    /// <summary>
    /// Gets a Unity GameObject using the name, returns the highest object with that name in the hierarchy
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject GetObject(string name) { return SceneObjectsHandler.Instance.GetObjectByName(name); }
    
    /// <summary>
    /// Moves object by the specified x/y/z distance over [time] milliseconds
    /// </summary>
    /// <param name="name"></param>
    /// <param name="delta_x"></param>
    /// <param name="delta_y"></param>
    /// <param name="delta_z"></param>
    /// <param name="time"></param>
    public static void MoveObject(string name, float delta_x, float delta_y, float delta_z, float time) {
        var currentPos = GetObject(name).transform.position; 
         MoveObjectTo(name, currentPos.x+delta_x, currentPos.y+delta_y, currentPos.z+delta_z, time); }
    
    /// <summary>
    /// Moves object to the specified x/y/z distance over [time] milliseconds
    /// </summary>
    /// <param name="name"></param>
    /// <param name="delta_x"></param>
    /// <param name="delta_y"></param>
    /// <param name="delta_z"></param>
    /// <param name="time"></param>
    public static void MoveObjectTo(string name, float dest_x, float dest_y, float dest_z, float time) {
        SceneObjectsHandler.Instance.AddMotionObject(new ObjectMotion(GetObject(name), 
            new Vector3(dest_x, dest_y, dest_z), time )); }
    
// *****   DEBUG COMMANDS   ****  
    /// <summary>
    /// Displays a debug message every [frameFrequency] frames if sxrSettings.debugMode==Frequent or every
    /// frame if sxrSettings.DebugMode==Framewise
    /// </summary>
    /// <param name="toWrite"></param>
    /// <param name="frameFrequency"></param> Optional, how often to display message, default=50 frames
    public static void DebugLog(string toWrite, int frameFrequency) {
       if (sxrSettings.Instance.debugMode == sxrSettings.DebugMode.Framewise ||
           (sxrSettings.Instance.debugMode == sxrSettings.DebugMode.Frequent &&
            sxrSettings.Instance.GetCurrentFrame() % frameFrequency == 0))
           Debug.Log("Frame " + sxrSettings.Instance.GetCurrentFrame() + ": " + toWrite); }
    public static void DebugLog(string toWrite) { DebugLog(toWrite, 50); }
    
   
}   

