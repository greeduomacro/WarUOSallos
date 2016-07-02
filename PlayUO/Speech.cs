// Decompiled with JetBrains decompiler
// Type: PlayUO.Speech
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections;
using System.IO;

namespace PlayUO
{
  public static class Speech
  {
    private static SpeechEntry[] m_Speech;

    public static SpeechEntry[] GetKeywords(string text)
    {
      if (Speech.m_Speech == null)
        Speech.LoadSpeechTable();
      text = text.ToLower();
      ArrayList dataStore = Engine.GetDataStore();
      SpeechEntry[] speechEntryArray1 = Speech.m_Speech;
      int length = speechEntryArray1.Length;
      for (int index = 0; index < length; ++index)
      {
        SpeechEntry speechEntry = speechEntryArray1[index];
        if (Speech.IsMatch(text, speechEntry.m_Keywords))
          dataStore.Add((object) speechEntry);
      }
      dataStore.Sort();
      SpeechEntry[] speechEntryArray2 = (SpeechEntry[]) dataStore.ToArray(typeof (SpeechEntry));
      Engine.ReleaseDataStore(dataStore);
      return speechEntryArray2;
    }

    public static bool IsMatch(string input, string[] split)
    {
      int startIndex = 0;
      for (int index = 0; index < split.Length; ++index)
      {
        if (split[index].Length > 0)
        {
          int num = input.IndexOf(split[index], startIndex);
          if (num > 0 && index == 0 || num < 0)
            return false;
          startIndex = num + split[index].Length;
        }
      }
      if (split[split.Length - 1].Length > 0)
        return startIndex == input.Length;
      return true;
    }

    public static unsafe void LoadSpeechTable()
    {
      string path = Engine.FileManager.ResolveMUL("Speech.mul");
      if (!File.Exists(path))
      {
        Speech.m_Speech = new SpeechEntry[0];
        Debug.Trace("File '{0}' not found, speech will not be encoded.", (object) path);
      }
      else
      {
        fixed (byte* numPtr = new byte[1024])
        {
          ArrayList arrayList = new ArrayList();
          FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
          while (UnsafeMethods.ReadFile(file, (void*) numPtr, 4) > 0)
          {
            int idKeyword = (int) numPtr[1] | (int) numPtr[0] << 8;
            int num = (int) numPtr[3] | (int) numPtr[2] << 8;
            if (num > 0)
            {
              UnsafeMethods.ReadFile(file, (void*) numPtr, num);
              arrayList.Add((object) new SpeechEntry(idKeyword, new string((sbyte*) numPtr, 0, num)));
            }
          }
          file.Close();
          Speech.m_Speech = (SpeechEntry[]) arrayList.ToArray(typeof (SpeechEntry));
        }
      }
    }

    public static void Dispose()
    {
      Speech.m_Speech = (SpeechEntry[]) null;
    }
  }
}
