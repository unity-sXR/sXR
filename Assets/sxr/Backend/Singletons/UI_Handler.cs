using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// [Singleton] Keeps track of images on the UI
///  Attach to playerCamera -> Canvas
/// Contains: 
///     void EnableOnly(Texture2D whichImage)
///     void SetPosition(Position whichPosition, Texture2D image, bool overridePosition=false)
///     void SetPosition(int whichPosition, Texture2D image, bool overridePosition
///     void FlipComponentUI(Texture2D whichImage)
///     void EnableComponentUI(Texture2D whichImage, bool enabled)
///     void EnableComponentUI(Position whichPosition, bool enabled)
///     void EnableComponentUI(Position whichPosition)
///     void DisableAllComponentsUI()
///     bool GetEnabled(Texture2D whichImage)
/// On Awake:
///     Finds all RawImage components in children with it's name being a Position, and sets the corresponding Positions to be the component
///     Initializes singleton
/// On Start:
///     N/A
/// On Update:
///     N/A
/// </summary>
public class UI_Handler : MonoBehaviour
{
    public RawImage[] UI_overlays = new RawImage[18];
    public RawImage pleaseWait, finished, eyeError, emergencyStop;
    public TextMeshProUGUI textbox1, textbox2, textbox3, textbox4, label;
    // TODO Add get/set methods for textbox, all UI_overlays, pleaseWait, finished, etc and turn private
    
    public enum Position {
        FullScreen1, FullScreen2, FullScreen3, FullScreen4, FullScreen5, 
        PartialScreenMiddle1, PartialScreenMiddle2, PartialScreenMiddle3, PartialScreenMiddle4,
        PartialScreenBottomLeft, PartialScreenBottom, PartialScreenBottomRight,
        PartialScreenTopLeft, PartialScreenTop, PartialScreenTopRight,
        PartialScreenLeft, PartialScreenRight, VRcamera }
    /// <summary>
    /// Disables all images in  UI_overlays except for given Texture2D
    /// </summary>
    /// <param name="whichImage">Image to keep enabled</param>
    public void EnableOnly(Texture2D whichImage) {
        DisableAllComponentsUI();
        EnableComponentUI(whichImage, true); }
    /// <summary>
    /// Sets Texture2D at given screen Position to the given Texture2D
    /// If override is true, it will replace any Texture2D already in the whichPosition
    /// If override is false and there is already a Texture2D at whichPosition, it will not replace the preexisting Texture2D
    /// </summary>
    /// <param name="whichPosition">Position to place image at</param>
    /// <param name="image">Texture2D to place at Position</param>
    /// <param name="overridePosition">Whether or not to override a preexisting Texture2D at whichPosition</param>
    public void SetPosition(Position whichPosition, Texture2D image, bool overridePosition=false) {
        if( UI_overlays[(int) whichPosition].texture == null || overridePosition)
            UI_overlays[(int) whichPosition].texture = image;
        else {
            Debug.Log("*** ERROR - UI Position " + whichPosition.ToString() +" already set. Use overridePosition:true or choose a different position"); }  
    }
    /// <summary>
    /// Sets Texture2D at given screen Position to the given Texture2D
    /// If override is true, it will replace any Texture2D already in the whichPosition
    /// If override is false and there is already a Texture2D at whichPosition, it will not replace the preexisting Texture2D
    /// </summary>
    /// <param name="whichPosition">Position to place image at</param>
    /// <param name="image">Texture2D to place at Position</param>
    /// <param name="overridePosition">Whether or not to override a preexisting Texture2D at whichPosition</param>
    public void SetPosition(int whichPosition, Texture2D image, bool overridePosition){SetPosition(Enum.Parse<Position>(Enum.GetName(typeof(Position), whichPosition)), image, overridePosition);}
    /// <summary>
    /// Disables whichImage if it's enabled
    /// Enables whichImage if it's disabled
    /// </summary>
    /// <param name="whichImage">Image to switch enabled of</param>
    public void FlipComponentUI(Texture2D whichImage) {
        foreach (var image in UI_overlays) {
            if (image.texture != null)
                if (image.texture.name == whichImage.name) {
                    image.enabled = !image.enabled; } } }
    /// <summary>
    /// Sets given image's enabled value to passed in enabled value
    /// </summary>
    /// <param name="whichImage">Image to set enable value of</param>
    /// <param name="enabled">Whether or not to enable whichImage</param>
    public void EnableComponentUI(Texture2D whichImage, bool enabled) {
        foreach (var image in UI_overlays) {
            if (image.texture != null)
                if(image.texture.name == whichImage.name) {
                    image.enabled = enabled;
                    break; } } }

    /// <summary>
    /// Sets image at given position's enabled value to passed in enabled value
    /// </summary>
    /// <param name="whichPosition">Position of image to set enable value of</param>
    /// <param name="enabled">Whether or not to enable image at whichPosition</param>
    public void EnableComponentUI(Position whichPosition, bool enabled) {
        foreach (var component in UI_overlays)
            if (component.name == whichPosition.ToString())
                component.enabled = enabled; } 
    
    /// <summary>
    /// Sets enabled value of image at whichPosition to true
    /// </summary>
    /// <param name="whichPosition">Position of image to enable</param>
    public void EnableComponentUI(Position whichPosition){ EnableComponentUI(whichPosition, true);}
    
    /// <summary>
    /// Disables all UI_overlays images
    /// </summary>
    public void DisableAllComponentsUI() {
        foreach (var image in UI_overlays)
            if( image != null && image.texture != null)
                    image.enabled = false; }
    /// <summary>
    /// Find out whether or not given image is enabled
    /// Returns false if image is not found on the UI
    /// </summary>
    /// <param name="whichImage">Image to find enabled value of</param>
    /// <returns>Returns true if the image is found and enabled, false otherwise</returns>
    public bool GetEnabled(Texture2D whichImage) {
        foreach (var image in UI_overlays)
            if (image.texture != null && image.texture.name == whichImage.name)
                return image.enabled;

        Debug.Log("*** Searched image does not appear to be on any active UI Component: " +whichImage.name); 
        return false; }

    public RawImage GetRawImageAtPosition(Position pos) {
        foreach (var component in UI_overlays)
            if (component.name == pos.ToString())
                return component;
        Debug.Log("No image found at " + pos);
        return null; }
    
    // Singleton initiated on Awake()
    public static UI_Handler Instance { get; private set; }
    private void Awake() {
        // Parse all UI_Handler components from Unity names
        var overlayComponents = gameObject.transform.Find("OutputCamera").GetComponentsInChildren<RawImage>();
        foreach (var component in overlayComponents) {
            if (component.name == "Finished")
                finished = component;
            else if (component.name == "PleaseWait")
                pleaseWait = component; 
            else if (component.name == "EyeError")
                eyeError = component; 
            else if (component.name == "EmergencyStop")
                emergencyStop = component; 
            else
                for (int i = 0; i < Enum.GetNames(typeof(Position)).Length; i++) {
                    if(component.name == Enum.GetValues(typeof(Position)).GetValue(i).ToString()) 
                        UI_overlays[i] = component; } } 
        
        var experimenterTextComponents = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        // Parse all UI_Handler experiment textboxes from Unity names 
        foreach (var textBox in experimenterTextComponents ) {
            textbox1 = textBox.name == "TextBox1" ? textBox : textbox1;
            textbox2 = textBox.name == "TextBox2" ? textBox : textbox2;
            textbox3 = textBox.name == "TextBox3" ? textBox : textbox3;
            textbox4 = textBox.name == "TextBox4" ? textBox : textbox4;
            label = textBox.name == "Label" ? textBox : label; }
        
        if ( Instance == null) { Instance = this; DontDestroyOnLoad(gameObject.transform.root); }
        else { Destroy(gameObject); } }

}
