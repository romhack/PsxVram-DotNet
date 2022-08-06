PsxVram-DotNet
===========

Viewer for displaying PSX VRAM dumps.

Start
-----

```
PsxVram-DotNet [InputFile]
```

***InputFile*** is raw binary 1MB PSX VRAM dump. This be obtained from most PSX debugging emulators, like PCSX or No$PSX.
Application handles input file the following way:

- If a `vram.bin` file is found in current directory, it will be loaded immediately.
- (Else) if `InputFile` command line argument is set, it will be loaded instead. Which also allows you just to drag&drop files on the app icon in windows explorer.
- (Else) shows open file dialog to specify input file by user.

Usage
-------

Base case scenario, it is assumed that you find the place you are interested in the game, and save the emulator dump into a specific folder. You can copy the application to this folder and it will open vram.bin every time it starts. Or each time you specify a dump by running the application.  
Then, you can view the entire dump in all supported PSX modes. Left mouse button moves rectangle, right mouse button moves CLUT line. Mouse button places rectangle with default padding (which must fit in most cases). If needed, rectangle and CLUT position can be adjusted with hotkeys or corresponding form controls. 
You can then move through the code in the emulator, rewrite the updated dump over the old one, update the application with the button and see the last changes in the PSX video memory.
In the status bar, you can see the necessary coordinates and addresses of the pieces of graphics you are interested in for further work in the debugger.
It is possible to save mode window contents to BMP file in VRAM dump folder. Bitmap mode and palette are transferred to BMP according to current mode.

Hotkeys
-------
- W/S/A/D: Move view rectangle
- Ctrl + (W/S/A/D) Move CLUT line
- Shift +(W/S/A/D) direction: Change rectangle size

History
--------
Initially 'Psx V-Ram' was created by Agemo as pure Windows API application, then rewritten by Griever into 'PsxVram-SDL' for multiplatform purposes, which was then rewritten into this application for further development with normal GUI features possible.

Licence
-------

For license information please see LICENSE.md

Credits
-------

- Original idea by Agemo http://www.romhacking.net/community/737/
- https://github.com/romhack/
- https://github.com/aybe/
