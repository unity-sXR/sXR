using System;
using UnityEngine;
/// <summary>
/// Parent class for controllers, contains the last time each button
/// returns "true" and if the button is currently pressed
/// </summary>
public class ControllerVR : MonoBehaviour
{
    public float[] buttonTimers = new float[Enum.GetNames(typeof(ControllerButton)).Length];
    public bool[] buttonPressed = new bool[Enum.GetNames(typeof(ControllerButton)).Length];
    
    public bool useController;

    public void EnableControllers() { useController = true; }
}
