// Decompiled with JetBrains decompiler
// Type: PlayUO.Skills
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.IO;
using System.Text;

namespace PlayUO
{
  public class Skills
  {
    public const int Count = 256;
    public const int Mask = 255;
    private Skill[] m_Skills;
    private SkillGroup[] m_Groups;

    public SkillGroup[] Groups
    {
      get
      {
        return this.m_Groups;
      }
    }

    public Skill this[SkillName name]
    {
      get
      {
        int index = (int) name;
        if (index < 0 || index >= this.m_Skills.Length)
          return (Skill) null;
        return this.m_Skills[index];
      }
    }

    public Skill this[int SkillID]
    {
      get
      {
        if (SkillID < 0 || SkillID >= this.m_Skills.Length)
          return (Skill) null;
        return this.m_Skills[SkillID];
      }
      set
      {
        this.m_Skills[SkillID] = value;
      }
    }

    public unsafe Skills()
    {
      byte[] buffer1 = new byte[3072];
      Stream stream1 = Engine.FileManager.OpenMUL(Files.SkillIdx);
      UnsafeMethods.ReadFile((FileStream) stream1, buffer1, 0, 3072);
      stream1.Close();
      Stream stream2 = Engine.FileManager.OpenMUL(Files.SkillMul);
      byte[] buffer2 = new byte[stream2.Length];
      UnsafeMethods.ReadFile((FileStream) stream2, buffer2, 0, buffer2.Length);
      stream2.Close();
      fixed (byte* numPtr1 = buffer1)
        fixed (byte* numPtr2 = buffer2)
        {
          this.m_Skills = new Skill[256];
          int id = 0;
          while (id < 256)
          {
            int num1 = *(int*) numPtr1;
            if (num1 < 0)
            {
              (int*) numPtr1 += 3;
              ++id;
            }
            else
            {
              byte* numPtr3 = numPtr2 + num1;
              int num2 = ((int*) numPtr1)[1];
              if (num2 < 1)
              {
                (int*) numPtr1 += 3;
                ++id;
              }
              else
              {
                byte* numPtr4 = numPtr3;
                IntPtr num3 = new IntPtr(1);
                byte* numPtr5 = numPtr4 + num3.ToInt64();
                bool action = (int) *numPtr4 != 0;
                StringBuilder stringBuilder;
                if (num2 < 1)
                {
                  stringBuilder = new StringBuilder();
                }
                else
                {
                  int capacity = num2 - 1;
                  stringBuilder = new StringBuilder(capacity);
                  for (int index = 0; index < capacity && (int) numPtr5[index] != 0; ++index)
                    stringBuilder.Append((char) numPtr5[index]);
                }
                this.m_Skills[id] = new Skill(id, action, stringBuilder.ToString());
                (int*) numPtr1 += 3;
                ++id;
              }
            }
          }
        }
      string path = Engine.FileManager.ResolveMUL("SkillGrp.mul");
      if (File.Exists(path))
      {
        FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        BinaryReader binaryReader = new BinaryReader((Stream) fileStream);
        int length = binaryReader.ReadInt32();
        bool flag = false;
        if (length == -1)
        {
          flag = true;
          length = binaryReader.ReadInt32();
        }
        this.m_Groups = new SkillGroup[length];
        this.m_Groups[0] = new SkillGroup("Miscellaneous", 0);
        for (int groupID = 1; groupID < length; ++groupID)
        {
          fileStream.Seek((long) ((flag ? 8 : 4) + (groupID - 1) * (flag ? 34 : 17)), SeekOrigin.Begin);
          StringBuilder stringBuilder = new StringBuilder(18);
          if (flag)
          {
            int num;
            while ((num = (int) binaryReader.ReadInt16()) != 0)
              stringBuilder.Append((char) num);
          }
          else
          {
            int num;
            while ((num = (int) binaryReader.ReadByte()) != 0)
              stringBuilder.Append((char) num);
          }
          this.m_Groups[groupID] = new SkillGroup(stringBuilder.ToString(), groupID);
        }
        fileStream.Seek((long) ((flag ? 8 : 4) + (length - 1) * (flag ? 34 : 17)), SeekOrigin.Begin);
        for (int index1 = 0; index1 < 256; ++index1)
        {
          Skill skill = this.m_Skills[index1];
          if (skill != null)
          {
            try
            {
              int index2 = binaryReader.ReadInt32();
              skill.Group = this.m_Groups[index2];
              skill.Group.Skills.Add((object) skill);
            }
            catch
            {
              break;
            }
          }
          else
            break;
        }
        binaryReader.Close();
      }
      else
      {
        this.m_Groups = new SkillGroup[1];
        this.m_Groups[0] = new SkillGroup("Skills", 0);
        for (int index = 0; index < 256; ++index)
          this.m_Skills[index].Group = this.m_Groups[0];
      }
    }

    public int GetSkill(string Name)
    {
      int index = -1;
      while (++index < 256)
      {
        if (this.m_Skills[index].Name == Name)
          return index;
      }
      return -1;
    }
  }
}
