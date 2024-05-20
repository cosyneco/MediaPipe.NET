// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Mediapipe.PInvoke;
using Mediapipe.PInvoke.Native;
using System;

namespace Mediapipe.External
{
  public static class Protobuf
  {
    public delegate void LogHandler(int level, string filename, int line, string message);
    public static readonly LogHandler DefaultLogHandler = LogProtobufMessage;

    public static void SetLogHandler(LogHandler logHandler)
    {
      UnsafeNativeMethods.google_protobuf__SetLogHandler__PF(logHandler).Assert();
    }

    /// <summary>
    ///   Reset the <see cref="LogHandler" />.
    ///   If <see cref="SetLogHandler" /> is called, this method should be called before the program exits.
    /// </summary>
    public static void ResetLogHandler()
    {
      UnsafeNativeMethods.google_protobuf__ResetLogHandler().Assert();
    }

        
    //[AOT.MonoPInvokeCallback(typeof(LogHandler))]
    private static void LogProtobufMessage(int level, string filename, int line, string message)
    {
      switch (level)
      {
        case 1:
          {
                        Console.Error.WriteLine($"[libprotobuf WARNING {filename}:{line}] {message}");
            return;
          }
        case 2:
          {
                        Console.Error.WriteLine($"[libprotobuf ERROR {filename}:{line}] {message}");
            return;
          }
        case 3:
          {
                        Console.Error.WriteLine($"[libprotobuf FATAL {filename}:{line}] {message}");
            return;
          }
        default:
          {
                        Console.Error.WriteLine($"[libprotobuf INFO {filename}:{line}] {message}");
            return;
          }
      }
    }
  }
}
