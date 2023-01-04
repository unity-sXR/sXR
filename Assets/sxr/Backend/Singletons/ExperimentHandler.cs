using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class ExperimentHandler : MonoBehaviour {
    public int subjectNumber;
    public int phase;
    public int block;
    public int trial;
    public int stepInTrial; 
    
    private string experimentName = "";
    private string subjectFile = "";
    private string backupFile = "";

    private float trialStartTime;
    private FileHandler fh = new FileHandler();

    private float lastTriggerPress;
    
    /// <summary>
    /// Offers a combined "Trigger" across joystick trigger, vr controller trigger, left mouse click, and keyboard spacebar
    /// </summary>
    /// <param name="frequency">Pause between returning true when trigger/spacebar/mouse is held</param>
    /// <returns>true if [frequency] seconds has passed since last trigger and trigger/space/mouse is pressed</returns>
    public bool GetTrigger(float frequency){
        if((sxr.CheckController( ControllerButton.Trigger) || 
            Input.GetKey(KeyCode.Space) || Input.GetAxis("Fire1")>0 || Input.GetMouseButton((int) MouseButton.Left))
           && Time.time-lastTriggerPress > frequency) {
            sxr.DebugLog("Valid trigger press detected: " + Time.time); 
            lastTriggerPress = Time.time;
            return true; }
        return false; }

    /// <summary>
    /// Sets experiment name (overrides the automatic naming when the "Start" button is pressed)
    /// </summary>
    /// <param name="name"></param>
    public void SetExperimentName(string name) { experimentName = name; ParseFileNames(); }
    
    /// <summary>
    /// Starts the experiment and parses 
    /// </summary>
    /// <param name="experimentName"></param>
    /// <param name="subjectNumber"></param>
    public void StartExperiment(string experimentName, int subjectNumber) {
        this.experimentName = experimentName;
        this.subjectNumber = subjectNumber; 
        ParseFileNames();
        phase = 1; 
    }

    /// <summary>
     /// Parses file names from specified directory/subject number
     /// </summary>
    private void ParseFileNames() {
        sxrSettings.Instance.subjectDataDirectory = sxrSettings.Instance.subjectDataDirectory == "" ? 
            Application.dataPath + Path.DirectorySeparatorChar + "Experiments" + Path.DirectorySeparatorChar + 
            experimentName + Path.DirectorySeparatorChar : sxrSettings.Instance.subjectDataDirectory;

        subjectFile = sxrSettings.Instance.subjectDataDirectory + subjectNumber;
        backupFile = sxrSettings.Instance.backupDataDirectory != ""
            ? sxrSettings.Instance.backupDataDirectory + subjectNumber
            : ""; }

    public void WriteHeaderToTaggedFile(string tag, string headerInfo) {
        headerInfo = "SubjectNumber,Time,BlockNumber,TrialNumber,TrialTime," + headerInfo;
        fh.AppendLine(subjectFile + "_" + tag + ".csv", headerInfo);
        if (backupFile != "") fh.AppendLine(backupFile + "_" + tag + ".csv", headerInfo); }
    
    public void WriteToTaggedFile(string tag, string toWrite) {
        toWrite = subjectNumber + "," + Time.time + "," + block + "," + trial + "," +
                  (Time.time - trialStartTime) + toWrite;
        fh.AppendLine(subjectFile + "_" + tag + ".csv", toWrite);
        if (backupFile != "") fh.AppendLine(backupFile + "_" + tag + ".csv", toWrite); }

    
    // Singleton initiated on Awake()
    public static ExperimentHandler Instance;
    void Awake() {
        if ( Instance == null) {Instance = this;  DontDestroyOnLoad(gameObject.transform.root);}
        else Destroy(gameObject); }
}
