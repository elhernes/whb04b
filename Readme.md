Library and test programs for WHB04B CNC Pendant on Windows

The whb04b.cpp and whb04.h are portable C++ using libusb/signal 11's
hidapi library.  They have been used/tested more on macOS and Linux.
This repo/library was specificially developed as a request to make the
pendant usable on a Windows platform via a DLL to C#.

Note: This is currently incomplete in that the callbacks into the DLL
host app are not connected.  On Windows the methods to write display
data return an error.  I've seen other coments of this problem on
windows using the hidapi library.

Overall design is that ```class WHB04B``` provides an interface to
send data to the WHB04B display and receive events from the buttons
and encoders.  The events are returned to the application through a
Delegate pattern.  The application subclasses the WHB04B::Delegte and
passes a reference into the WHB04B constructor.

In the Windows DLL interface, this needs to be done through "C" style
functions and pointers.  The whb04b-win32.h interface sets up this
wrapper.



