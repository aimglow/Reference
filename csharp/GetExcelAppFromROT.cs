using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

using Excel = Microsoft.Office.Interop.Excel.Application;

[DllImport("ole32.dll")]
private static extern int GetRunningObjectTable(int reserved, out IRunningObjectTable pprot);

[DllImport("ole32.dll")]
private static extern void CreateBindCtx(int reserved, out IBindCtx ppbc);

[DllImport("ole32.dll")]
	private static extern int ProgIDFromCLSID([In] ref Guid clsid, [MarshalAs(UnmanagedType.LPWStr)] out string lplpszProgID);

public static void Main(string[] args)
{
    if (GetRunningObjectTable(0, out IRunningObjectTable pprot) == 0)
    {
        pprot.EnumRunning(out IEnumMoniker ppenumMoniker);
        ppenumMoniker.Reset();

        var moniker = new IMoniker[1];

        while (ppenumMoniker.Next(1, moniker, IntPtr.Zero) == 0)
        {
            CreateBindCtx(0, out IBindCtx ppbc);
            pprot.GetObject(moniker[0], out object ppunkObject);

            moniker[0].GetDisplayName(ppbc, null, out string ppszDisplayName);
            moniker[0].GetClassID(out Guid pClassID);

            ProgIDFromCLSID(ref pClassID, out string lplpszProgID);
        }
    }

}

