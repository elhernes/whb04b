using System;
//using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

class Whb04bNative
{
    private delegate int EventCallback(byte b1, byte b2, byte axis, byte stepcon, int count);
    private EventCallback _eventDelegate;
    private IntPtr _native;

    private const string _whb04b_dll = @"\\vmware-host\Shared Folders\Documents\Visual Studio 2015\Projects\whb04b\x64\Debug\whb04b.dll";
    //    private const string _whb04b_dll = @"whb04b.dll";
    /*
    [DllImport(_whb04b_dll, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr whb04b_create(EventCallback fn);
    [DllImport(_whb04b_dll, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern void whb04b_destroy(IntPtr vp);
    [DllImport(_whb04b_dll, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern void whb04b_display(IntPtr vp, bool wm_coord, double axis1, double axis2, double axis3, double feed, double spindle);
    */
    
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate IntPtr whb04b_create_c(EventCallback fn);
    whb04b_create_c whb04b_create;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void whb04b_destroy_c(IntPtr instance);
    whb04b_destroy_c whb04b_destroy;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate bool whb04b_display_c(IntPtr vp, bool wm_coord, double axis1, double axis2, double axis3, double feed, double spindle);
    whb04b_display_c whb04b_display;


    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr LoadLibrary(string dllToLoad);
    [DllImport("kernel32.dll")]
    public static extern int GetLastError();
    [DllImport("kernel32.dll")]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);
    [DllImport("kernel32.dll")]
    public static extern bool FreeLibrary(IntPtr hModule);

    private int PendantEvent(byte b1, byte b2, byte axis, byte stepcon, int count)
    {
        Console.WriteLine("Pendant Event: (b1 {0}) (b2 {1}) (axis {2}) (stepcon {3}) (count {4})", b1, b2, axis, stepcon, count);
        return 1;
    }

    public Whb04bNative()
    {
        IntPtr pDll = LoadLibrary(_whb04b_dll);
        if (pDll == IntPtr.Zero)
        {
            Console.WriteLine("LoadLibrary({0}): {1}", _whb04b_dll, Marshal.GetLastWin32Error().ToString());
        }
        else
        {
            IntPtr createAddr = GetProcAddress(pDll, "whb04b_create");
            whb04b_create = (whb04b_create_c)Marshal.GetDelegateForFunctionPointer(createAddr, typeof(whb04b_create_c));

            IntPtr destroyAddr = GetProcAddress(pDll, "whb04b_destroy");
            whb04b_destroy = (whb04b_destroy_c)Marshal.GetDelegateForFunctionPointer(destroyAddr, typeof(whb04b_destroy_c));

            IntPtr displayAddr = GetProcAddress(pDll, "whb04b_display");
            whb04b_display = (whb04b_display_c)Marshal.GetDelegateForFunctionPointer(displayAddr, typeof(whb04b_display_c));

            _eventDelegate = new EventCallback(PendantEvent);
            _native = whb04b_create(_eventDelegate);
        }
    }

    ~Whb04bNative()
    {
        if (_native != IntPtr.Zero)
        {
           whb04b_destroy(_native);
        }
    }
    public void display(bool wm_coord, double axis1, double axis2, double axis3, double feed, double spindle)
    {
 //       whb04b_display(_native, wm_coord, axis1, axis2, axis3, feed, spindle);
    }
}

namespace test_csharp
{
    class Program
    {
            static void Main(string[] args)
        {
            Whb04bNative whb04b = new Whb04bNative();
                //	        whb04b_display(instance, true, 12.34, 23.45, 34.56, 3785, 19783);

                for (;;)
                {
                Thread.Sleep(2000);
                }
            
        }
    }
}
