<h1 align="center">Terminator</h1>

<p align="center">
<img src="https://github.com/Chainski/Terminator/assets/96607632/6deac6c5-9df1-4143-a8ab-9aecb811de34", width="200", height="200">
</p>


Terminator is a compact utility programmed in C#, designed to end processes that have ```RtlSetProcessIsCritical``` activated see [bsod.cs]()

### Requirements
- An administrator account with UAC enabled.
- UAC level is set to default.
- Tested on ```Windows 10.19045 (English) x64```

### Usage
- Download and the program from the [releases](https://github.com/Chainski/Terminator/releases).
- Run ```Terminator.exe```

Proof of Concept 👁




### How it Works
Terminator defines a method called ```UnProtectAndTerminate``` that aims to terminate a specified process after removing its protection. It utilizes platform invoke to call functions from system DLLs.
First, it imports functions from ```ntdll.dll``` and ```kernel32.dll``` using DllImport attribute. These functions are ```NtSetInformationProcess``` to alter process information and ```TerminateProcess``` to forcibly terminate a process.
Within the ```UnProtectAndTerminate``` method, it enters debug mode using ```Process.EnterDebugMode()```. Then it attempts to open the target process using ```OpenProcess```, passing necessary parameters including the process ID.
Afterward, it calls ```NtSetInformationProcess``` with appropriate arguments to remove the process protection. If successful, it terminates the process using ```TerminateProcess``` with an exit code of 0.
Terminator also provides feedback via console output, indicating success or failure in terminating the process.


### License
This project is licensed under the GNU License - see the [LICENSE](https://github.com/chainski/Terminator/blob/main/LICENSE) file for details