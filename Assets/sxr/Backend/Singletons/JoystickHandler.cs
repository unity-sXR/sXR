using System;
using UnityEngine;

/// <summary>
/// [Singleton] Used to access joystick input
/// Contains: 
///     JoyStickDirection GetDirection()
///     void DisplayDirectionUI()
/// </summary>
public class JoystickHandler : MonoBehaviour
{
    public enum JoyStickDirection {
        Left,
        Right,
        Up,
        Down,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight,
        None }

    /// <summary>
    /// Returns the direction the joystick is being pressed 
    /// </summary>
    /// <returns></returns>
    public JoyStickDirection GetDirection() {
        float horizontal_input = Input.GetAxis("Horizontal");
        float vertical_input = Input.GetAxis("Vertical");
        if (horizontal_input > .5 & Math.Abs(vertical_input) < .2) return JoyStickDirection.Right;
        if (horizontal_input > .5 & vertical_input < -.5) return JoyStickDirection.DownRight;
        if (Math.Abs(horizontal_input) < .2 & vertical_input < -.5) return JoyStickDirection.Down;
        if (horizontal_input < -.5 & vertical_input < -.5 ) return JoyStickDirection.DownLeft;
        if (horizontal_input < -.5 & Math.Abs(vertical_input) < .2) return JoyStickDirection.Left;
        if (horizontal_input < -.5 & vertical_input > .5) return JoyStickDirection.UpLeft;
        if (Math.Abs(horizontal_input) < .2 & vertical_input > .5) return JoyStickDirection.Up;
        if (horizontal_input > .5 & vertical_input > .5) return JoyStickDirection.UpRight;
        
        return JoyStickDirection.None; }
    
    /// <summary>
    /// Enables UI_Handler elements at the sides/corners of the screen if the joystick is
    /// pushed in that direction
    /// </summary>
    public void DisplayDirectionUI() {
        float horizontal_input = Input.GetAxis("Horizontal");
        float vertical_input = Input.GetAxis("Vertical");
        
        UI_Handler.Instance.EnableComponentUI(UI_Handler.Position.PartialScreenRight, enabled:false);
        UI_Handler.Instance.EnableComponentUI(UI_Handler.Position.PartialScreenBottomRight, enabled:false);
        UI_Handler.Instance.EnableComponentUI(UI_Handler.Position.PartialScreenBottom, enabled:false);
        UI_Handler.Instance.EnableComponentUI(UI_Handler.Position.PartialScreenBottomLeft, enabled:false);
        UI_Handler.Instance.EnableComponentUI(UI_Handler.Position.PartialScreenLeft, enabled:false);
        UI_Handler.Instance.EnableComponentUI(UI_Handler.Position.PartialScreenTopLeft, enabled:false);
        UI_Handler.Instance.EnableComponentUI(UI_Handler.Position.PartialScreenTop, enabled:false);
        UI_Handler.Instance.EnableComponentUI(UI_Handler.Position.PartialScreenTopRight, enabled:false);
        
        if (horizontal_input > .5 & Math.Abs(vertical_input) < .2) UI_Handler.Instance.EnableComponentUI(UI_Handler.Position.PartialScreenRight);
        else if (horizontal_input > .5 & vertical_input < -.5)  UI_Handler.Instance.EnableComponentUI(UI_Handler.Position.PartialScreenBottomRight);
        else if (Math.Abs(horizontal_input) < .2 & vertical_input < -.5) UI_Handler.Instance.EnableComponentUI(UI_Handler.Position.PartialScreenBottom);
        else if (horizontal_input < -.5 & vertical_input < -.5 )UI_Handler.Instance.EnableComponentUI(UI_Handler.Position.PartialScreenBottomLeft);
        else if (horizontal_input < -.5 & Math.Abs(vertical_input) < .2) UI_Handler.Instance.EnableComponentUI(UI_Handler.Position.PartialScreenLeft);
        else if (horizontal_input < -.5 & vertical_input > .5) UI_Handler.Instance.EnableComponentUI(UI_Handler.Position.PartialScreenTopLeft);
        else if (Math.Abs(horizontal_input) < .2 & vertical_input > .5) UI_Handler.Instance.EnableComponentUI(UI_Handler.Position.PartialScreenTop);
        else if (horizontal_input > .5 & vertical_input > .5) UI_Handler.Instance.EnableComponentUI(UI_Handler.Position.PartialScreenTopRight); }

    // Singleton initiated on Awake()
    public static JoystickHandler Instance; 
    void Awake() {
        if ( Instance == null) {Instance = this;  DontDestroyOnLoad(gameObject.transform.root);}
        else Destroy(gameObject); }
}
