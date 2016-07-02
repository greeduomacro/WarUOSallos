// Decompiled with JetBrains decompiler
// Type: PlayUO.GraphicTranslator
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.Collections.Generic;
using System.IO;

namespace PlayUO
{
  public sealed class GraphicTranslator
  {
    private Dictionary<int, GraphicTranslation> table;

    public Dictionary<int, GraphicTranslation> Table
    {
      get
      {
        return this.table;
      }
    }

    public GraphicTranslation this[int graphicId]
    {
      get
      {
        GraphicTranslation graphicTranslation;
        this.table.TryGetValue(graphicId, out graphicTranslation);
        return graphicTranslation;
      }
    }

    public GraphicTranslator(string filePath)
    {
      this.table = new Dictionary<int, GraphicTranslation>();
      this.ReadFromFile(filePath);
    }

    private void ReadFromFile(string filePath)
    {
      Debug.Try("Reading {0}...", (object) filePath);
      try
      {
        filePath = Engine.FileManager.ResolveMUL(filePath);
        if (File.Exists(filePath))
        {
          using (StreamReader streamReader = new StreamReader(filePath))
          {
            string str;
            while ((str = streamReader.ReadLine()) != null)
            {
              string line = str.Trim();
              if (line.Length != 0 && !line.StartsWith("#", StringComparison.Ordinal))
              {
                foreach (GraphicTranslation graphicTranslation in GraphicTranslation.Parse(line))
                {
                  if (!this.table.ContainsKey(graphicTranslation.UpdatedId))
                    this.table.Add(graphicTranslation.UpdatedId, graphicTranslation);
                }
              }
            }
          }
        }
        Debug.EndTry();
      }
      catch (Exception ex)
      {
        Debug.FailTry();
        Debug.Error(ex);
      }
    }

    public int Convert(int graphicId)
    {
      GraphicTranslation graphicTranslation = this[graphicId];
      if (graphicTranslation != null)
        graphicId = graphicTranslation.FallbackId;
      return graphicId;
    }
  }
}
