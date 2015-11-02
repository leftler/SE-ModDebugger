# Mod Debugger v1.0
Created By: Scott Chamberlain

Loading this plugin in to Space Engineeers will allow mods to use the 
System.Diagnostics.Debugger class. It also changes the way mods are compiled 
so now it is possible to step through them with a debugger.

## HOW TO USE

Tutorial Video: https://youtu.be/qO3kIz7uqFY

To prevent problems when using this plugin, before unziping the release be 
sure to right click on zip file open the properties and "Unblock" it if windows 
has decided the zip file is a non-trusted file.

Extract the zip to a directory on your disk, be sure that the directory is
writeable by the user that Space Engineers will be running as. Then when 
launching Space Engineeers pass the command line parameter:

    -plugin %PluginPath%\ModDebugger.dll

Where `%PluginPath%` is the directory you put the file. So for example if you 
had the file located at `C:\Mod Debugger\ModDebugger.dll` then you would launch 
space engineers using the arguments

    -plugin "C:\Mod Debugger\ModDebugger.dll"

Once loaded you will now be able to make calls to System.Diagnostics.Debugger
from inside mods and scripts. Included is a [ModDebugger.cs](ModDebugger.cs) file 
which provides a wrapper class that you can safely with or without the plugin
loaded. When the plugin is not loaded All the methods of ModDebugger do 
nothing and all return false, or a empty string.

From inside your mod at some point call `ModDebugger.Launch()` this will cause
Space Engineers to launch the Just-In-Time Debugger screen. From there you can
choose to launch a new instance of Visual Studio or attach to an existing one.

***IMPORTANT NOTE:*** The debugger will be using files generated by the Space 
Engineers compiler, breakpoints in your mod code's original files will not 
work! You must put breakpoints in the temporary files the compiler generated 
which will be located at `%PluginPath%\TempFiles\` with random file names for 
the files. These files will be deleted after Space Engineers closes.

## Cloning and Building
When cloning this repository copy [`user.props.example`](user.props.example) and name it `user.props` and adjust the variables within to match your local enviorment.

This project uses features introduced in C# 6.0 and must be compiled in Visual Studio 2015 or newer.
