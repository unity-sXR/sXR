using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ExperimenterDisplayHandler : MonoBehaviour
{
    
    public bool defaultDisplayTexts = true;

    private ExperimentHandler eh;
    private TextMeshProUGUI subjNum, phaseNum, blockNum, trialNum;
    public TextMeshProUGUI displayText1, displayText2, displayText3, displayText4, displayText5;

    /// <summary>
    /// Sets the experiment to specified subject/phase/block/trial
    /// Automatically parses experiment name from the project name
    /// </summary>
    public void StartButton() {
        if (!Int32.TryParse(subjNum.text, out eh.subjectNumber))
            Debug.Log("Failed to parse subject number, using subject = 0");
        if (!Int32.TryParse(phaseNum.text, out eh.phase))
            Debug.Log("Failed to parse phase number, using phase = 0");
        if (!Int32.TryParse(blockNum.text, out eh.block))
            Debug.Log("Failed to block phase number, using block = 0");
        if (!Int32.TryParse(trialNum.text, out eh.trial))
            Debug.Log("Failed to parse trial number, using trial = 0");
        ExperimentHandler.Instance.StartExperiment(
            Application.dataPath.Split("/")[Application.dataPath.Split("/").Length-2], 
            eh.subjectNumber);

        sxr.GetObject("StartScreen").SetActive(false); }

    private void Update() {
        if (defaultDisplayTexts && ExperimentHandler.Instance.phase > 0) {
            displayText1.text = "Phase - Block (trial)  =  " + eh.phase + "-" + eh.block + "(" + eh.trial + ")";
            displayText1.enabled = true; 
            
            displayText2.text = "Player Position: " + sxrSettings.Instance.vrCamera.transform.position;
            displayText2.enabled = true;
        } }
    
    private void Start() {
        if (subjNum == null) subjNum = sxr.GetObject("SubjectNumber").GetComponentInChildren<TextMeshProUGUI>();
        if (phaseNum == null) phaseNum = sxr.GetObject("SubjectNumber").GetComponentInChildren<TextMeshProUGUI>();
        if (blockNum == null) blockNum = sxr.GetObject("SubjectNumber").GetComponentInChildren<TextMeshProUGUI>();
        if (trialNum == null) trialNum = sxr.GetObject("SubjectNumber").GetComponentInChildren<TextMeshProUGUI>();
        if (!displayText1) displayText1 = sxr.GetObject("DisplayText1").GetComponent<TextMeshProUGUI>();
        if (!displayText2) displayText2 = sxr.GetObject("DisplayText2").GetComponent<TextMeshProUGUI>();
        if (!displayText3) displayText3 = sxr.GetObject("DisplayText3").GetComponent<TextMeshProUGUI>();
        if (!displayText4) displayText4 = sxr.GetObject("DisplayText4").GetComponent<TextMeshProUGUI>();
        if (!displayText5) displayText5 = sxr.GetObject("DisplayText5").GetComponent<TextMeshProUGUI>();
        eh = ExperimentHandler.Instance;
    }

    public static ExperimenterDisplayHandler Instance;
    void Awake() {
        if ( Instance == null) {Instance = this;  DontDestroyOnLoad(gameObject.transform.root);}
        else Destroy(gameObject); }
}
