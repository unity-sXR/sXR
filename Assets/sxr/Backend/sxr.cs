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
   
// ****   USER INTERFACE   ****

    public enum UI_Position {
        FullScreen1, FullScreen2, FullScreen3, FullScreen4, FullScreen5, 
        PartialScreenMiddle1, PartialScreenMiddle2, PartialScreenMiddle3, PartialScreenMiddle4,
        PartialScreenBottomLeft, PartialScreenBottom, PartialScreenBottomRight,
        PartialScreenTopLeft, PartialScreenTop, PartialScreenTopRight,
        PartialScreenLeft, PartialScreenRight, VRcamera }
    
    public enum Prebuilt_Images{Stop, Loading, Finished, EyeError}
    public static void DisplayPrebuilt(Prebuilt_Images image)
    {UI_Handler.Instance.DisplayPrebuilt(image); }
    
    /// <summary>
    /// Displays an input slider on the user interface that can be
    /// moved with controller lasers
    /// </summary>
    /// <param name="min">Minimum value of slider</param>
    /// <param name="max">Maximum value of slider</param>
    /// <param name="questionText">Question to display above slider</param>
    public static void InputSlider(int min, int max, string questionText, bool wholeNumbers) 
    { UI_Handler.Instance.InputSlider(min, max, questionText); }
    public static void InputSlider(int min, int max, string questionText) { InputSlider(min, max, questionText, true); }
    
    /// <summary>
    /// Displays a dropdown UI element with the provided options
    /// </summary>
    /// <param name="options">List of options for the dropdown menu</param>
    /// <param name="questionText">Text to display above the dropdown menu</param>
    public static void InputDropdown(string[] options, string questionText)
    {UI_Handler.Instance.InputDropdown(options, questionText);}

    /// <summary>
    /// Parses values from the active InputSlider or InputDropdown.
    /// Can only parse float or int values for the slider and
    /// int or string values for the dropdown
    /// </summary>
    /// <param name="output"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool ParseInputUI<T>(out T output)
    { return UI_Handler.Instance.ParseInputUI(out output); }

    public enum TextPosition{Top, MiddleTop, MiddleBottom, Bottom, TopLeft}
    public static void DisplayText(string text, TextPosition position) {
        switch (position) {
            case TextPosition.Top: UI_Handler.Instance.textboxTop.text = text;
                UI_Handler.Instance.textboxTop.enabled = true; 
                break;
            case TextPosition.MiddleTop: UI_Handler.Instance.textboxTopMiddle.text = text; 
                UI_Handler.Instance.textboxTopMiddle.enabled = true;
                break; 
            case TextPosition.MiddleBottom: UI_Handler.Instance.textboxBottomMiddle.text = text; 
                UI_Handler.Instance.textboxBottomMiddle.enabled = true;
                break; 
            case TextPosition.Bottom: UI_Handler.Instance.textboxBottom.text = text; 
                UI_Handler.Instance.textboxBottom.enabled = true;
                break;     
            case TextPosition.TopLeft: UI_Handler.Instance.textboxTopLeft.text = text; 
                UI_Handler.Instance.textboxTopMiddle.enabled = true;
                break; 
            default: sxr.DebugLog("Text position not found");
                break; } }
    public static void DisplayText(string text){DisplayText(text, TextPosition.MiddleTop);}

    public static void HideText(TextPosition position) {
        switch (position) {
            case TextPosition.Top: UI_Handler.Instance.textboxTop.enabled = false; 
                break;
            case TextPosition.MiddleTop: UI_Handler.Instance.textboxTopMiddle.enabled = false; 
                break; 
            case TextPosition.MiddleBottom: UI_Handler.Instance.textboxBottomMiddle.enabled = false; 
                break; 
            case TextPosition.Bottom: UI_Handler.Instance.textboxBottom.enabled = false; 
                break;     
            case TextPosition.TopLeft: UI_Handler.Instance.textboxTopMiddle.enabled = false; 
                break; 
            default: sxr.DebugLog("Text position not found");
                break; } }
    public static void HideAllText() {
        UI_Handler.Instance.textboxTop.enabled = false;
        UI_Handler.Instance.textboxTopMiddle.enabled = false;
        UI_Handler.Instance.textboxBottomMiddle.enabled = false;
        UI_Handler.Instance.textboxBottom.enabled = false;
        UI_Handler.Instance.textboxTopLeft.enabled = false; }
    
    /// <summary>
    /// Displays the specified image (searches by image name without the extension, e.g. "myImage" not "myImage.jpeg".
    /// </summary>
    /// <param name="imageName">Name of the image to display</param>
    /// <param name="position">Position on the UI to display image</param>
    /// <param name="overridePrevious">If there is a previous image, overwrite it with the new image</param>
    public static void DisplayImage(string imageName, UI_Position position, bool overridePrevious)
    { UI_Handler.Instance.DisplayImage(imageName, position, overridePrevious);}
    public static void DisplayImage(string imageName, UI_Position position){DisplayImage(imageName, position, true);}
    public static void DisplayImage(string imageName){DisplayImage(imageName, UI_Position.FullScreen1);}
    
    public static void HideImageUI(UI_Position position){UI_Handler.Instance.DisableComponentUI(position);}
    public static void HideImagesUI(){UI_Handler.Instance.DisableAllComponentsUI();}
    
// ****   INPUT DEVICES   ****
    /// <summary>
    /// Offers a combined "Trigger" across joystick trigger, vr controller trigger, left mouse click, and keyboard spacebar
    /// </summary>
    /// <param name="frequency">Pause between returning true when trigger/spacebar is held. Optional, default
    /// value is 0 seconds (continuous input)</param>
    /// <returns>true if [frequency] seconds has passed since last trigger and trigger/space is pressed</returns>
    public static bool GetTrigger(float frequency) { return ExperimentHandler.Instance.GetTrigger(frequency);}
    public static bool GetTrigger() { return GetTrigger(0f);}

    /// <summary>
    /// Will return true at specified [frequency] if the specified key
    /// is held
    /// </summary>
    /// <param name="whichKey">Choose with KeyCode.[Key]. e.g. KeyCode.A, KeyCode.Space</param>
    /// <param name="frequency">Pause between returning true values when a key is held (in seconds).
    /// Optional, default is 0 seconds (continuous input)</param>
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
    /// <param name="frequency">Delay before returning true when button is held. Optional, default is 0 seconds (continuous input)</param>
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
            sxr.DebugLog("Controller Button: " + whichButton + " not pressed");
        return false; }
    public static bool CheckController(ControllerButton whichButton) {
        return CheckController(whichButton, 0f); }

// ****  EXPERIMENT FLOW COMMANDS   ****
    /// <summary>
    /// Call at beginning of experiment to set data path
    /// </summary>
    /// <param name="experimentName"></param>
    /// <param name="subjectNumber"></param>
    public static void StartExperiment(string experimentName, int subjectNumber) {ExperimentHandler.Instance.StartExperiment(experimentName, subjectNumber); }

    public static int GetPhase() { return ExperimentHandler.Instance.phase; }
    public static int GetBlock(){return ExperimentHandler.Instance.block; }
    public static int GetTrial(){return ExperimentHandler.Instance.trial; }
    public static int GetStepInTrial() { return ExperimentHandler.Instance.stepInTrial;}

    public static void NextPhase() {
        ExperimentHandler.Instance.phase++;
        ExperimentHandler.Instance.block = 0;
        ExperimentHandler.Instance.trial = 0;
        ExperimentHandler.Instance.stepInTrial = 0; }

    public static void NextBlock() {
        ExperimentHandler.Instance.block++;
        ExperimentHandler.Instance.trial = 0;
        ExperimentHandler.Instance.stepInTrial = 0; }

    public static void NextTrial() {
        ExperimentHandler.Instance.trial++;
        ExperimentHandler.Instance.stepInTrial = 0; }
    public static void NextStep() { ExperimentHandler.Instance.stepInTrial++;}

    public static void SetStep(int stepNumber) { ExperimentHandler.Instance.stepInTrial = stepNumber;}
    
    public static void StartTimer(string timerName, float duration)
    {TimerHandler.Instance.AddTimer(timerName, duration);}

    public static bool CheckTimer(string name) {
        return TimerHandler.Instance.CheckTimer(name); }
    
    public static void WriteToTaggedFile(string tag, string text)
    {ExperimentHandler.Instance.WriteToTaggedFile(tag, text);}
    public static void WriteHeaderToTaggedFile(string tag, string text)
    {ExperimentHandler.Instance.WriteHeaderToTaggedFile(tag, text);}

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
    public static void MoveObjectTo(GameObject obj, float dest_x, float dest_y, float dest_z, float time) {
        SceneObjectsHandler.Instance.AddMotionObject(new ObjectMotion(obj, 
            new Vector3(dest_x, dest_y, dest_z), time )); }
    public static void MoveObjectTo(string name, float dest_x, float dest_y, float dest_z)
    { MoveObjectTo(name, dest_x, dest_y, dest_z, 0); }

    public static void SpawnObject(PrimitiveType type, string name, float x, float y, float z) {
        var obj = GameObject.CreatePrimitive(type);
        obj.name = name; 
        MoveObjectTo(obj, x, y, z, 0); }
    public static void SpawnObject(PrimitiveType type, string name)
    { SpawnObject(type, name, 0, 0, 0); }

    public static void SpawnObject(GameObject objectToCopy, string name) {
        var obj = GameObject.Instantiate(objectToCopy);
        obj.name = name; }

    public static bool ObjectExists(string name)
    { return GetObject(name) != null; }
    
    public static void MakeObjectGrabbable(string objectName)
    {
        if (GetObject(objectName).GetComponents<GrabbableObject>().Length == 0)
            GetObject(objectName).AddComponent<GrabbableObject>(); 
    }

    public static void EnableObjectPhysics(string objectName, bool useGravity)
    {
        if (GetObject(objectName).GetComponents<Rigidbody>().Length == 0)
            GetObject(objectName).AddComponent<Rigidbody>();
        GetObject(objectName).GetComponent<Rigidbody>().isKinematic = false;
        GetObject(objectName).GetComponent<Rigidbody>().useGravity = useGravity; 
    }public static void EnableObjectPhysics(string objectName){EnableObjectPhysics(objectName, true);}

    public static bool CheckCollision(GameObject obj1, GameObject obj2)
    { return CollisionHandler.Instance.ObjectsCollidersTouching(obj1, obj2);}
    public static bool CheckCollision(string obj1, string obj2)
    { return CheckCollision(GetObject(obj1), GetObject(obj2));}
    
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

