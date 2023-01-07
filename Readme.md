![sXR Logo](https://github.com/unity-sXR/sXR/blob/master/Assets/sxr/Resources/sxrlogo.png)

6 January 2023 - Version 0.0.0

# Background
simpleXR (sXR) is a software package designed to facilitate rapid development of XR experiments. Researchers in many different fields are starting to use virtual/augmented reality for studying things like learning, navigation, vision, or fear. However, the packages previously available for developing in XR were directed at computer scientists or people with a strong background in programming. sXR makes programming as simple as possible by providing one easy to use library with single line commands for more complicated tasks. The package is built for Unity and can be downloaded as a template project or added to previous projects with little effort. Just replace the scene's camera with the sXR_prefab and you'll gain access to multiple user interfaces and a plethora of commands that will allow you to start gathering data in days instead, not months. Extended reality is hard...  simpleXR is simple.

# For Beginners
While sXR makes Unity much simpler, it can still be complicated if you're just starting out. The project contains a sample experiment with a step-by-step video walkthrough [(youtube link)](https://youtu.be/NZE6ZiD2sPA). If you don't understand the ExperimentScript.cs file of the sample experiment, I recommend watching the entire video as it breaks down the entire development process. Feel free to reach out if you get stuck!

# The Basics...
The majority of sXR can be used by just typing "sxr." + whatever it is you are trying to do. Any modern IDE will suggest methods to use if you use the correct keyword. For example, typing "sxr.file" will bring up the commands sxr.WriteHeaderToTaggedFile() and sxr.WriteToTaggedFile(). The package contains thorough documentation for all the commands in the main sxr class, and descriptions of what each command done will be available in the docstring. In Unity, the only requirement for using sXR commands is replacing the main camera with the sxr_prefab object (found in the prefabs folder). 
