using Mediapipe.PInvoke.Native;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mediapipe.PInvoke.Native
{
    internal static partial class SafeNativeMethods
    {
        [Pure, DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_StatusOrPoller__ok(IntPtr statusOrPoller);
    }
}
