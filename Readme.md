![sXR Logo](https://github.com/unity-sXR/sXR/blob/master/Assets/sxr/Resources/sxrlogo.png)

[Background](#background)

[Beginners](#for-beginners)

[The Basics](#the-basics)

[sXR_prefab](#sxr_prefab)

[Commands List](#commands-list)

[Coming Soon...](#coming-soon)

[Requested Features](#requested-features)

[Version History](#version-history)


# Background
simpleXR (sXR) is a software package designed to facilitate rapid development of XR experiments. Researchers in many different fields are starting to use virtual/augmented reality for studying things like learning, navigation, vision, or fear. However, the packages previously available for developing in XR were directed at computer scientists or people with a strong background in programming. sXR makes programming as simple as possible by providing one easy to use library with single line commands for more complicated tasks. The package is built for Unity and can be downloaded as a template project or added to previous projects with little effort. Just replace the scene's camera with the sXR_prefab and you'll gain access to multiple user interfaces and a plethora of commands that will allow you to start gathering data in days instead, not months. Extended reality is hard...  simpleXR is simple.

# For Beginners
While sXR makes Unity much simpler, it can still be complicated if you're just starting out. The project contains a sample experiment with a step-by-step video walkthrough [(youtube link)](https://youtu.be/NZE6ZiD2sPA). If you don't understand the ExperimentScript.cs file of the sample experiment, I recommend watching the entire video as it breaks down the entire development process. Feel free to reach out if you get stuck!

# The Basics...
The majority of sXR can be used by just typing "sxr." + whatever it is you are trying to do. Any modern IDE will suggest methods to use if you use the correct keyword. For example, typing "sxr.file" will bring up the commands sxr.WriteHeaderToTaggedFile() and sxr.WriteToTaggedFile(). The package contains thorough documentation for all the commands in the main sxr class, and descriptions of what each command does will be available in the docstring. 

sXR is designed to be "string based", meaning you can just enter the name of an object/resource for most functions. For example, starting a timer with sxr.StartTimer("myTimer") will create a timer named "myTimer" that you can access later with things like sxr.TimePassed("myTimer") or sxr.RestartTimer("myTimer"). Resources located in any "Resources" folder can also be accessed by the filename without the extension. If you have a file named "mySound.mp3", using sxr.PlaySound("mySound") will play the sound (note you do not include the file extension '.mp3' in the PlaySound() command). You can also access UI images, shaders, and game objects by providing the name into the proper function. For most users, searching for objects every frame won't effect performance. However, for more complicated environments you can also pass in the GameObject directly (e.g. sxr.MoveObject(MyObjectGameObject, 1, 0, 0)).  

In Unity, the only requirement for using sXR commands is replacing the main camera with the sxr_prefab object (found in the prefabs folder). 

# sXR_prefab
The prefab can be found in "Assets/sxr/Prefabs". The prefab parent object contains the "sxrSettings.cs" singleton. These settings will automatically be overwritten by whatever is selected in the sXR pop-up (accessed by clicking the sXR tab on the toolbar at the top of the window). The prefab has 5 child objects. "ExperimenterScreen", "OutputCameraAssembly", "vrCameraAssembly, "sxrBackend", and "EventSystem".  

## vrCameraAssembly ![vrCameraAssembly](https://github.com/unity-sXR/ReadmeImages/blob/main/vrCameraAssembly.png)
The vrCameraAssembly contains the vrCamera (the camera that tracks the movement of the HMD) and two controllers that are displayed as semi-transparent capsules. To change the controller objects, simply replace the "LeftController" and "RightController" with objects named identically. sXR will automatically look for the "LeftController" and "RightController" objects and use them for grabbing objects. The vrCamera object contains the audio listener and audio source for playing sounds using sxr.PlaySound(). These are attached to the camera so sounds played in the virtual environment still get detected by the scene's audio listener.  By default, a CapsuleCollider is attached to the vrCamera. You can check if the participant is colliding with objects by using sxr.CheckCollision("vrCamera", "[other object name]"). The vrCamera output is not sent directly to the screen, instead it is passed to the OutputCameraAssembly" for further processing.

## OutputCameraAssembly ![outputCameraAssembly](https://github.com/unity-sXR/ReadmeImages/blob/main/outputCameraAssembly.png)
The screen captured by the vrCamera is sent to the main canvas of the OutputCameraAssembly. This is done so any fullscreen shader effects used on the vrCamera input won't be applied to any of the UI canvases. OutputCameraAssembly's camera has two canvases, the MainCanvas (containing the primary UI which has the VRcamera view) and the InputCanvas. Images can be placed in any of the MainCanvas's  designated locations by using sxr.DisplayImage("MyImage", sxr.UI_Position.[whichPosition]). Similarly, text can be displayed with sxr.DisplayText("Text to display", sxr.TextPosition.[whichPosition]). The second canvas, InputCanvas, allows the user to use the LeftLaser and RightLaser to answer prompts initiated with sxr.InputSlider() and sxr.InputDropdown() (see sample experiment for proper implementation).  

## ExperimenterScreen ![experimenterScreen](https://github.com/unity-sXR/ReadmeImages/blob/main/experimenterScreen.png)
The ExperimenterScreen object contains things that will be displayed on the computer monitor, but not passed to the headset. This includes the StartScreen, along with 5 textboxes.  By default, the first 3 boxes are already used. The first textbox contains "[Phase] Block - Trial(Step)" to keep track of what step of the experiment the participant is on. The second textbox contains the player's position, and the third textbox contains the time remaining in the default trial timer (accessed by using Timer commands without including a timer name, e.g. 'sxr.StartTimer()'). To enable/disable the default textboxes, use DefaultExperimenterScreenTextboxes(true/false).  

## sxrBackend ![sxrBackend](https://github.com/unity-sXR/ReadmeImages/blob/main/sxrBackend.png)
sxrBackend contains most of the Singletons required to run sXR. By clicking into these scripts in the Inspector, you can change any of the public variables... This can be useful if you're trying to debug a certain phase/block since you can click into the ExperimentHandler and directly edit the experiment flow information. 

## EventSystem ![eventSystem](https://github.com/unity-sXR/ReadmeImages/blob/main/eventSystem.png)
The EventSystem that comes in the sxr_prefab is set up to accept mouse input and VR input for the UI. This InputSystem uses bindings in the action map (located in the main sxr folder). To create custom commands, bind the correct buttons with the action map and use the EventSystem's InputSystem to specify what that action will do in the UI. There's no need to create bindings for normal VR controls, as 'sxr.CheckController(sxr.ControllerButton.[whichButton])' will check all of the major VR controller bindings.

# Commands List
[Experiment Flow](#experiment-flow)

[Data Recording](#data-recording)

[User Interface](#user-interface)

[Object Manipulation](#object-manipulation)

[Extras](#extras)

## Experiment Flow

## Data Recording

## User Interface
InputSlider() - Creates and displays a slider that the participant can manipulate with the controller laser

InputDropdown() - Creates and displays a dropdown menu that the participant can manipulate with the controller laser 

ParseInputUI() - Gets the response from an open InputSlider or InputDropdown

DisplayText() - Displays text to the VR headset at the specified position

HideText() - Hides text on the VR headset at the specified position

HideAllText() - Hides all text locations displayed on the VR headset

DisplayImage() - Displays an image at the specified location on the VR headset

HideImageUI() - Hides an image at the specified location on the VR headset

HideImagesUI() - Hides all images displayed on the VR headset

## Input Devices
GetTrigger() - Returns true if the VR controller trigger, a joystick trigger, or the keyboard's spacebar are pressed

KeyHeld() - Returns true continuously with the specified delay 


## Object Manipulation

## Extras



# Coming Soon...
Eye-tracking/shader tutorial + sxr.commands for assigning shaders to objects
Gaze-ray objects - record when the user looks at certain objects
Command Recorder - record when the user presses buttons (for replaying scene)
Playback mode - Replay the participants view and highlight their gaze
VR Controller "Touch" option for buttons (i.e. when the controller button is  touched but not pressed)

# Requested Features
N/a

# Version History
6 January 2023 - Version 0.0.0: Initial release 
