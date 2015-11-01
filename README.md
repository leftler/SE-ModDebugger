# SE-ModDebugger
Modifies the Space Engineers IL Checker to aid in the debugging of mods

## Use
To use launch Space Engineers with the command line `-plugin C:\Path\To\Plugin\ModDebugger.dll`

In your mods you can add the included  [ModDebugger.cs](ModDebugger.cs) file to your projects, this will add the class `ModDebugger` which exposes proxy methods to all `System.Diagnostics.Debugger` methods which are safe to call both with and without the ModDebugger loaded.

## Cloning
When cloning this repository copy [`user.props.example`](user.props.example) and name it `user.props` and adjust the variables within to match your local enviorment.
