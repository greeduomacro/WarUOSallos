// Decompiled with JetBrains decompiler
// Type: PlayUO.GraphicTranslation
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System.Collections.Generic;

namespace PlayUO
{
  public sealed class GraphicTranslation
  {
    private int updatedId;
    private int fallbackId;
    private int fallbackData;

    public int UpdatedId
    {
      get
      {
        return this.updatedId;
      }
    }

    public int FallbackId
    {
      get
      {
        return this.fallbackId;
      }
    }

    public int FallbackData
    {
      get
      {
        return this.fallbackData;
      }
    }

    public GraphicTranslation(int updatedId, int fallbackId, int fallbackData)
    {
      this.updatedId = updatedId;
      this.fallbackId = fallbackId;
      this.fallbackData = fallbackData;
    }

    public static IEnumerable<GraphicTranslation> Parse(string line)
    {
      int leftBrace = line.IndexOf('{');
      int rightBrace = line.IndexOf('}', leftBrace);
      if (leftBrace >= 0 && rightBrace >= 0)
      {
        string updatedIdString = line.Substring(0, leftBrace);
        string[] fallbackIdStrings = line.Substring(leftBrace + 1, rightBrace - leftBrace - 1).Split(',');
        string fallbackContextString = line.Substring(rightBrace + 1);
        int updatedId;
        int fallbackContext;
        if (int.TryParse(updatedIdString, out updatedId) && int.TryParse(fallbackContextString, out fallbackContext))
        {
          foreach (string s in fallbackIdStrings)
          {
            int fallbackId;
            if (int.TryParse(s, out fallbackId))
              yield return new GraphicTranslation(updatedId, fallbackId, fallbackContext);
          }
        }
      }
    }
  }
}
