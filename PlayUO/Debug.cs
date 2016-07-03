// Decompiled with JetBrains decompiler
// Type: PlayUO.Debug
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using SharpDX;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace PlayUO
{
  public class Debug
  {
    public static Stopwatch _stopwatch = new Stopwatch();
    private static StreamWriter m_Logger;
    private static int m_Indent;
    private static bool m_Time;

    public static int Indent
    {
      get
      {
        return Debug.m_Indent;
      }
      set
      {
        Debug.m_Indent = value;
      }
    }

    public static void Dispose()
    {
      if (Debug.m_Logger == null)
        return;
      Debug.m_Logger.Flush();
      Debug.m_Logger.Close();
      Debug.m_Logger = (StreamWriter) null;
    }

    private static void Print()
    {
      Debug.m_Logger.WriteLine();
    }

    private static void Print(string ToWrite)
    {
      Debug.GetLogger();
      Debug.m_Logger.WriteLine(ToWrite);
    }

    private static void Print(string Format, object Obj0)
    {
      Debug.Print(string.Format(Format, Obj0));
    }

    private static void Print(string Format, object Obj0, object Obj1)
    {
      Debug.Print(string.Format(Format, Obj0, Obj1));
    }

    private static void Print(string Format, object Obj0, object Obj1, object Obj2)
    {
      Debug.Print(string.Format(Format, Obj0, Obj1, Obj2));
    }

    private static void Print(string Format, params object[] Params)
    {
      Debug.Print(string.Format(Format, Params));
    }

    public static void Trace(string Message)
    {
      Debug.GetLogger();
      Debug.m_Logger.WriteLine(new string(' ', Debug.Indent * 3) + Message);
      Debug.m_Logger.Flush();
    }

    public static void Trace(string Format, object Obj0)
    {
      Debug.Trace(string.Format(Format, Obj0));
    }

    public static void Trace(string Format, object Obj0, object Obj1)
    {
      Debug.Trace(string.Format(Format, Obj0, Obj1));
    }

    public static void Trace(string Format, object Obj0, object Obj1, object Obj2)
    {
      Debug.Trace(string.Format(Format, Obj0, Obj1, Obj2));
    }

    public static void Trace(string Format, params object[] Params)
    {
      Debug.Trace(string.Format(Format, Params));
    }

    public static void Try(string Name)
    {
      Debug.GetLogger();
      Debug.m_Logger.Write("{0}{1}...", (object) new string(' ', Debug.Indent * 3), (object) Name);
    }

    public static void Try(string Format, object Obj0)
    {
      Debug.Try(string.Format(Format, Obj0));
    }

    public static void Try(string Format, object Obj0, object Obj1)
    {
      Debug.Try(string.Format(Format, Obj0, Obj1));
    }

    public static void Try(string Format, object Obj0, object Obj1, object Obj2)
    {
      Debug.Try(string.Format(Format, Obj0, Obj1, Obj2));
    }

    public static void Try(string Format, params object[] Params)
    {
      Debug.Try(string.Format(Format, Params));
    }

    public static void FailTry(string msg)
    {
      Debug.GetLogger();
      Debug.m_Logger.WriteLine("failed {0}", (object) msg);
    }

    public static void FailTry()
    {
      Debug.GetLogger();
      Debug.m_Logger.WriteLine("failed");
    }

    public static void EndTry(string msg)
    {
      Debug.GetLogger();
      Debug.m_Logger.WriteLine("done {0}", (object) msg);
    }

    public static void EndTry(string Format, object Obj0)
    {
      Debug.EndTry(string.Format(Format, Obj0));
    }

    public static void EndTry(string Format, object Obj0, object Obj1)
    {
      Debug.EndTry(string.Format(Format, Obj0, Obj1));
    }

    public static void EndTry(string Format, object Obj0, object Obj1, object Obj2)
    {
      Debug.EndTry(string.Format(Format, Obj0, Obj1, Obj2));
    }

    public static void EndTry(string Format, params object[] Params)
    {
      Debug.EndTry(string.Format(Format, Params));
    }

    private static void GetLogger()
    {
      if (Debug.m_Logger != null)
        return;
      Engine.WantDirectory("data/ultima/logs/");
      Debug.m_Logger = new StreamWriter((Stream) Engine.FileManager.CreateUnique("data/ultima/logs/playuo", ".log"));
      Debug.m_Logger.AutoFlush = true;
    }

    public static void EndTry()
    {
      Debug.GetLogger();
      Debug.m_Logger.WriteLine("done");
    }

    [DebuggerHidden]
    public static void Break()
    {
      Debugger.Break();
    }

    public static void TimeBlock(string Name)
    {
      Debug.Try(Name);
      Debug.m_Time = true;
      Debug._stopwatch.Start();
    }

    public static void Block(string Name)
    {
      Debug.Trace("{0}..", (object) Name);
      ++Debug.Indent;
    }

    public static void FailBlock()
    {
      if (Debug.m_Time)
      {
        Debug._stopwatch.Stop();
        Debug.m_Time = false;
        Debug.EndTry("( {0} )", (object) Debug._stopwatch.Elapsed);
        Debug._stopwatch.Reset();
      }
      else
      {
        --Debug.Indent;
        Debug.Trace("Failed");
      }
    }

    public static void EndBlock()
    {
      if (Debug.m_Time)
      {
        Debug._stopwatch.Stop();
        Debug.m_Time = false;
        Debug.EndTry("( {0} )", (object) Debug._stopwatch.Elapsed);
        Debug._stopwatch.Reset();
      }
      else
      {
        --Debug.Indent;
        Debug.Trace("Done");
      }
    }

    public static void Error(Exception ex)
    {
      if (ex is SharpDXException)
      {
        SharpDXException sharpDxException = (SharpDXException) ex;
        Debug.Trace("Error Code -> {0}", (object) sharpDxException.ResultCode.ToString());
        Debug.Trace("Error String -> {0}", (object) ((object) sharpDxException.Descriptor).ToString());
      }
      Debug.Trace("Type -> {0}", (object) ex.GetType());
      Debug.Trace("Message -> {0}", (object) ex.Message);
      Debug.Trace("Source -> {0}", (object) ex.Source);
      Debug.Trace("Target -> {0}", (object) ex.TargetSite);
      Debug.Trace("Inner -> {0}", (object) ex.InnerException);
      Debug.Trace("Stack ->");
      Debug.Trace(ex.StackTrace);
    }

    public static void Error(string Message)
    {
      StackTrace stackTrace = new StackTrace(true);
      bool flag = false;
      MethodBase methodBase = (MethodBase) null;
      int frameCount = stackTrace.FrameCount;
      for (int index = 0; index < frameCount; ++index)
      {
        MethodBase method = stackTrace.GetFrame(index).GetMethod();
        if (method.DeclaringType == typeof (Debug) && method.Name == "Error")
          flag = true;
        else if (flag)
        {
          methodBase = method;
          break;
        }
      }
      if (methodBase == (MethodBase) null)
      {
        Debug.Print("Error in unknown module:");
        Debug.Print(" - {0}", (object) Message.Replace("\n", "\r\n - "));
        Debug.Print(" - Stack Trace ->");
        Debug.Print(stackTrace.ToString());
        Debug.Print();
      }
      else
      {
        Debug.Print("Error in '{0}.{1}':", (object) methodBase.DeclaringType.Name, (object) methodBase.Name);
        Debug.Print(" - {0}", (object) Message.Replace("\n", "\r\n - "));
        Debug.Print(" - Stack Trace ->");
        Debug.Print(stackTrace.ToString());
        Debug.Print();
      }
    }
  }
}
