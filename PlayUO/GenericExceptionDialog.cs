// Decompiled with JetBrains decompiler
// Type: PlayUO.GenericExceptionDialog
// Assembly: Ultima.Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=6cc7e8bd89c5c6bf
// MVID: 0CAC2BC7-B53A-42C2-916C-A40DD9E7563D
// Assembly location: C:\Program Files (x86)\Electronic Arts\Ultima Online Classic\Ultima.Client.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PlayUO
{
  public class GenericExceptionDialog : Form
  {
    private IContainer components;
    private Button cmdToggleDetails;
    private Button cmdOkay;
    private Label lblMessage;
    private TextBox txtDetails;

    public GenericExceptionDialog()
    {
      this.InitializeComponent();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      e.Graphics.DrawIcon(SystemIcons.Error, 18, 16);
    }

    public void SetExceptionInfo(Exception ex, string fmt, params object[] args)
    {
      this.lblMessage.Text = string.Format(fmt, args);
      this.txtDetails.Text = ex.ToString();
    }

    private void cmdToggleDetails_Click(object sender, EventArgs e)
    {
      this.SuspendLayout();
      if (this.txtDetails.Visible)
      {
        this.txtDetails.Visible = false;
        this.Height -= 192;
        this.cmdOkay.Top -= 192;
        this.cmdToggleDetails.Top -= 192;
        this.cmdToggleDetails.Text = "Show &Details";
      }
      else
      {
        this.txtDetails.Visible = true;
        this.Height += 192;
        this.cmdOkay.Top += 192;
        this.cmdToggleDetails.Top += 192;
        this.cmdToggleDetails.Text = "Hide &Details";
      }
      this.ResumeLayout();
    }

    private void txtDetails_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.A || !e.Control)
        return;
      this.txtDetails.SelectAll();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.cmdToggleDetails = new Button();
      this.cmdOkay = new Button();
      this.lblMessage = new Label();
      this.txtDetails = new TextBox();
      this.SuspendLayout();
      this.cmdToggleDetails.Location = new System.Drawing.Point(292, 58);
      this.cmdToggleDetails.Name = "cmdToggleDetails";
      this.cmdToggleDetails.Size = new Size(88, 23);
      this.cmdToggleDetails.TabIndex = 16;
      this.cmdToggleDetails.Text = "Show &Details";
      this.cmdToggleDetails.Click += new EventHandler(this.cmdToggleDetails_Click);
      this.cmdOkay.DialogResult = DialogResult.OK;
      this.cmdOkay.Location = new System.Drawing.Point(206, 58);
      this.cmdOkay.Name = "cmdOkay";
      this.cmdOkay.Size = new Size(80, 23);
      this.cmdOkay.TabIndex = 13;
      this.cmdOkay.Text = "&Okay";
      this.lblMessage.Location = new System.Drawing.Point(50, 9);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new Size(330, 46);
      this.lblMessage.TabIndex = 12;
      this.txtDetails.Font = new System.Drawing.Font("Courier New", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txtDetails.Location = new System.Drawing.Point(4, 60);
      this.txtDetails.Multiline = true;
      this.txtDetails.Name = "txtDetails";
      this.txtDetails.ReadOnly = true;
      this.txtDetails.ScrollBars = ScrollBars.Both;
      this.txtDetails.Size = new Size(376, 184);
      this.txtDetails.TabIndex = 15;
      this.txtDetails.Visible = false;
      this.txtDetails.WordWrap = false;
      this.txtDetails.KeyDown += new KeyEventHandler(this.txtDetails_KeyDown);
      this.AcceptButton = (IButtonControl) this.cmdOkay;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(384, 85);
      this.Controls.Add((Control) this.cmdToggleDetails);
      this.Controls.Add((Control) this.cmdOkay);
      this.Controls.Add((Control) this.lblMessage);
      this.Controls.Add((Control) this.txtDetails);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "GenericExceptionDialog";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Ultima Online - Sallos";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
