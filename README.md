# OAT-StaticCam
An accessory program for OnAirTap

This tool sends static camera positions into shared memory, so that people using physical cameras
can experience some of the benefits of OnAirTap.

## The Config File
Values are read from a file called `externalcamera.cfg`, which should be in the working directory.
That's usually the folder containing the executable.

The format of that file is based on a similarly-named file used with the SteamVR Unity Plugin, Vivecraft, and LIV.

Example:
```
x=0
y=1
z=-0.1
rx=0
ry=45
rz=0
fov=60
```
Providing the transform as a matrix via the `m=0,1,0.5, ...` line is not supported.

Tracked cameras are not supported. It is assumed that co-ordinates are relative to your playspace origin.

## Usage

 - Put the cfg file in the right place
 - Run the .exe
   - This is a console program, so it should open up a console window.
   - If you're on Linux, make sure you run this inside a terminal. **Most file explorers will not open one for you.**
 - With the window focussed, press R to load the config.
   - This program does not write anything to the MMF on startup. If you never press R,
   the mod will proceed with whatever was last written.
 - Press E to enable the camera.
 - You're rendering (hopefully)

You can press R again, at any time, to reload the file.
