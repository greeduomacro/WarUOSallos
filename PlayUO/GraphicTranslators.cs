// Decompiled with JetBrains decompiler
// Type: PlayUO.GraphicTranslators
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

namespace PlayUO
{
  public static class GraphicTranslators
  {
    public static readonly GraphicTranslator Art = new GraphicTranslator("art.def");
    public static readonly GraphicTranslator Bodies = new GraphicTranslator("body.def");
    public static readonly GraphicTranslator Corpse = new GraphicTranslator("corpse.def");
    public static readonly GraphicTranslator Gumps = new GraphicTranslator("gump.def");
    public static readonly GraphicTranslator Sound = new GraphicTranslator("sound.def");
    public static readonly GraphicTranslator Music = new GraphicTranslator("music.def");
    public static readonly GraphicTranslator Textures = new GraphicTranslator("texterr.def");
    public static readonly GraphicTranslator[] Actions = new GraphicTranslator[5]{ new GraphicTranslator("anim1.def"), new GraphicTranslator("anim2.def"), new GraphicTranslator("anim3.def"), new GraphicTranslator("anim4.def"), new GraphicTranslator("anim5.def") };
  }
}
