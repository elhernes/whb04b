using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

static class NativeMethods
{
    [DllImport("kernel32.dll")]
    public static extern IntPtr LoadLibrary(string dllToLoad);
    [DllImport("kernel32.dll")]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);
    [DllImport("kernel32.dll")]
    public static extern bool FreeLibrary(IntPtr hModule);
}

namespace test_csharp
{
    class Program
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr whb04b_create_c();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void whb04b_destroy_c(IntPtr instance);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate bool whb04b_display_c(IntPtr vp, bool wm_coord, double axis1, double axis2, double axis3, double feed, double spindle);

        static void Main(string[] args)
        {

	IntPtr pDll = NativeMethods.LoadLibrary(@"\\vmware-host\Shared Folders\Documents\Visual Studio 2015\Projects\whb04b\whb04b\Debug\whb04b.dll");
//            IntPtr pDll = NativeMethods.LoadLibrary(@"whb04b.dll");

	    if (pDll == IntPtr.Zero) {
	    } else {
            IntPtr createAddr = NativeMethods.GetProcAddress(pDll, "whb04b_create");
	    whb04b_create_c whb04b_create = (whb04b_create_c)Marshal.GetDelegateForFunctionPointer(createAddr, typeof(whb04b_create_c));
	    
            IntPtr destroyAddr = NativeMethods.GetProcAddress(pDll, "whb04b_destroy");
	    whb04b_destroy_c whb04b_destroy = (whb04b_destroy_c)Marshal.GetDelegateForFunctionPointer(destroyAddr, typeof(whb04b_destroy_c));
	    
            IntPtr displayAddr = NativeMethods.GetProcAddress(pDll, "whb04b_display");
	    whb04b_display_c whb04b_display = (whb04b_display_c)Marshal.GetDelegateForFunctionPointer(displayAddr, typeof(whb04b_display_c));

	    IntPtr instance = whb04b_create();
	    whb04b_display(instance, true, 12.34, 23.45, 34.56, 3785, 19783);

	    whb04b_destroy(instance);

            //if(pAddressOfFunctionToCall == IntPtr.Zero)

            bool result = NativeMethods.FreeLibrary(pDll);
            //remaining code here
//            Console.WriteLine(theResult);
        }
    }
}
}
