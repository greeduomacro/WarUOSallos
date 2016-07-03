// Decompiled with JetBrains decompiler
// Type: PlayUO.BodyConverter
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.IO;

namespace PlayUO
{
  public class BodyConverter
  {
    private static int[] m_Table_LBR;
    private static int[] m_Table_AOS;
    private static int[] m_Table_AOW;
    private static int[] m_Table_ML;

    static BodyConverter()
    {
      string path = Engine.FileManager.ResolveMUL("bodyconv.def");
      if (!File.Exists(path))
        return;
      BodyConverter.m_Table_LBR = new int[2048];
      BodyConverter.m_Table_AOS = new int[2048];
      BodyConverter.m_Table_AOW = new int[2048];
      BodyConverter.m_Table_ML = new int[2048];
      for (int index = 0; index < BodyConverter.m_Table_LBR.Length; ++index)
        BodyConverter.m_Table_LBR[index] = -1;
      for (int index = 0; index < BodyConverter.m_Table_AOS.Length; ++index)
        BodyConverter.m_Table_AOS[index] = -1;
      for (int index = 0; index < BodyConverter.m_Table_AOW.Length; ++index)
        BodyConverter.m_Table_AOW[index] = -1;
      for (int index = 0; index < BodyConverter.m_Table_ML.Length; ++index)
        BodyConverter.m_Table_ML[index] = -1;
      using (StreamReader streamReader = new StreamReader(path))
      {
        string str1;
        while ((str1 = streamReader.ReadLine()) != null)
        {
          string str2;
          if ((str2 = str1.Trim()).Length != 0 && !str2.StartsWith("#"))
          {
            int length = str2.IndexOf('#');
            if (length >= 0)
              str2 = str2.Substring(0, length);
            try
            {
              string[] strArray = str2.Split('\t');
              if (strArray.Length >= 2)
              {
                int int32 = int.Parse(strArray[0]);
                int num1 = int.Parse(strArray[1]);
                int num2 = -1;
                int num3 = -1;
                int num4 = -1;
                try
                {
                  if (strArray.Length >= 3)
                      num2 = int.Parse(strArray[2]);
                }
                catch
                {
                }
                try
                {
                  if (strArray.Length >= 4)
                      num3 = int.Parse(strArray[3]);
                }
                catch
                {
                }
                try
                {
                  if (strArray.Length >= 5)
                      num4 = int.Parse(strArray[4]);
                }
                catch
                {
                }
                if (int32 >= 0 && int32 < BodyConverter.m_Table_LBR.Length)
                {
                  if (num1 == 68)
                    num1 = 122;
                  BodyConverter.m_Table_LBR[int32] = num1;
                }
                if (int32 >= 0 && int32 < BodyConverter.m_Table_AOS.Length)
                  BodyConverter.m_Table_AOS[int32] = num2;
                if (int32 >= 0 && int32 < BodyConverter.m_Table_AOW.Length)
                  BodyConverter.m_Table_AOW[int32] = num3;
                if (int32 >= 0)
                {
                  if (int32 < BodyConverter.m_Table_ML.Length)
                    BodyConverter.m_Table_ML[int32] = num4;
                }
              }
            }
            catch
            {
              Debug.Error("Bad def format");
            }
          }
        }
      }
    }

    public static bool Contains(int bodyID)
    {
      return BodyConverter.m_Table_LBR != null && bodyID >= 0 && (bodyID < BodyConverter.m_Table_LBR.Length && BodyConverter.m_Table_LBR[bodyID] != -1) || BodyConverter.m_Table_AOS != null && bodyID >= 0 && (bodyID < BodyConverter.m_Table_AOS.Length && BodyConverter.m_Table_AOS[bodyID] != -1) || (BodyConverter.m_Table_AOW != null && bodyID >= 0 && (bodyID < BodyConverter.m_Table_AOW.Length && BodyConverter.m_Table_AOW[bodyID] != -1) || BodyConverter.m_Table_ML != null && bodyID >= 0 && (bodyID < BodyConverter.m_Table_ML.Length && BodyConverter.m_Table_ML[bodyID] != -1));
    }

    public static int GetFileSet(int bodyID)
    {
      if (BodyConverter.m_Table_LBR != null && bodyID >= 0 && (bodyID < BodyConverter.m_Table_LBR.Length && BodyConverter.m_Table_LBR[bodyID] != -1))
        return 2;
      if (BodyConverter.m_Table_AOS != null && bodyID >= 0 && (bodyID < BodyConverter.m_Table_AOS.Length && BodyConverter.m_Table_AOS[bodyID] != -1))
        return 3;
      if (BodyConverter.m_Table_AOW != null && bodyID >= 0 && (bodyID < BodyConverter.m_Table_AOW.Length && BodyConverter.m_Table_AOW[bodyID] != -1))
        return 4;
      return BodyConverter.m_Table_ML != null && bodyID >= 0 && (bodyID < BodyConverter.m_Table_ML.Length && BodyConverter.m_Table_ML[bodyID] != -1) ? 5 : 1;
    }

    public static int Convert(ref int bodyID)
    {
      if (BodyConverter.m_Table_LBR != null && bodyID >= 0 && bodyID < BodyConverter.m_Table_LBR.Length)
      {
        int num = BodyConverter.m_Table_LBR[bodyID];
        if (num != -1)
        {
          bodyID = num;
          return 2;
        }
      }
      if (BodyConverter.m_Table_AOS != null && bodyID >= 0 && bodyID < BodyConverter.m_Table_AOS.Length)
      {
        int num = BodyConverter.m_Table_AOS[bodyID];
        if (num != -1)
        {
          bodyID = num;
          return 3;
        }
      }
      if (BodyConverter.m_Table_AOW != null && bodyID >= 0 && bodyID < BodyConverter.m_Table_AOW.Length)
      {
        int num = BodyConverter.m_Table_AOW[bodyID];
        if (num != -1)
        {
          bodyID = num;
          return 4;
        }
      }
      if (BodyConverter.m_Table_ML != null && bodyID >= 0 && bodyID < BodyConverter.m_Table_ML.Length)
      {
        int num = BodyConverter.m_Table_ML[bodyID];
        if (num != -1)
        {
          bodyID = num;
          return 5;
        }
      }
      return 1;
    }
  }
}
