using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Windows.Speech;

/// <summary>
/// Used to create voice activation commands 
/// </summary>
public class VoiceCommands : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;

    private Dictionary<string, Action> actions = new Dictionary<string, Action>(); 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Microphone.devices[0]);
        actions.Add("Keyword", Command1);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start(); 
    }

    // Update is called once per frame
    void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    void Command1(){ /*Do Stuff*/}
    

}
