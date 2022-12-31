using UnityEngine;
using UnityEngine.XR;

public class CameraTracker : MonoBehaviour {
    [SerializeField] private bool recordCamera = false, recordGaze=false;
    
    private float recordHeadTimer = 0.0f;
    private Camera vrCamera;
    private bool headerPrinted;

    public void WriteCameraTrackerHeader(bool recordingGaze) {
        ExperimentHandler.Instance.WriteToTaggedFile("camera_tracker",
            "xPos,yPos,zPos,xRot,yRot,zRot,gazeScreenPosX,gazeScreenPosY"); }
    
    public void StartRecording(bool recordGaze) {
        if (!headerPrinted) WriteCameraTrackerHeader(recordGaze);
        this.recordGaze = recordGaze && XRSettings.enabled;
        recordCamera = true; }

    public void PauseRecording() {
        recordGaze = false;
        recordCamera = false; }

    public bool RecordingGaze() { return recordGaze; }
    
    private void Start()
    {  vrCamera = sxrSettings.Instance.vrCamera; }

    private void Update() {
        if (sxrSettings.Instance.RecordThisFrame() & recordCamera) {
            if (recordGaze)
                ExperimentHandler.Instance.WriteToTaggedFile("camera_tracker",
                    (gameObject.transform.position.x + "," +
                    gameObject.transform.position.y + "," + 
                    gameObject.transform.position.z + "," +
                    gameObject.transform.rotation.eulerAngles.x + "," +
                    gameObject.transform.rotation.eulerAngles.y + "," + 
                    gameObject.transform.rotation.eulerAngles.z + "," +
                    GazeHandler.Instance.GetScreenFixationPoint()).Replace("(","").Replace(")",""));
            
            else
                ExperimentHandler.Instance.WriteToTaggedFile("camera_tracker",
                    gameObject.transform.position.x + "," +
                    gameObject.transform.position.y + "," + 
                    gameObject.transform.position.z +
                    gameObject.transform.rotation.eulerAngles.x + "," +
                    gameObject.transform.rotation.eulerAngles.y + "," + 
                    gameObject.transform.rotation.eulerAngles.z);
            
            recordHeadTimer = Time.time; } }

    // Singleton initiated on Awake()
    public static CameraTracker Instance; 
    private void Awake() {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject.transform.root); }
        else  Destroy(gameObject); }
}
