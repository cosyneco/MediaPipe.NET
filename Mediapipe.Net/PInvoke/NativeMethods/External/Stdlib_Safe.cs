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
    internal unsafe partial class SafeNativeMethods
    {
        [Pure, DllImport(NativeMethods.MediaPipeLibrary, ExactSpelling = true)]
        public static extern byte mp_StatusOrString__ok(IntPtr statusOrString);
    }
}
