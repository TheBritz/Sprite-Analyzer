namespace Sprite_Analyzer
{
  partial class StartupView
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.m_startGroupbox = new System.Windows.Forms.GroupBox();
      this.m_labelOr = new System.Windows.Forms.Label();
      this.m_startGroupbox.SuspendLayout();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.button1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.button1.Location = new System.Drawing.Point(9, 65);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(113, 48);
      this.button1.TabIndex = 0;
      this.button1.Text = "Start New Analysis Set";
      this.button1.UseVisualStyleBackColor = true;
      // 
      // button2
      // 
      this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.button2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.button2.Location = new System.Drawing.Point(194, 65);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(113, 48);
      this.button2.TabIndex = 1;
      this.button2.Text = "Load Analysis Set";
      this.button2.UseVisualStyleBackColor = true;
      // 
      // m_startGroupbox
      // 
      this.m_startGroupbox.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.m_startGroupbox.Controls.Add(this.m_labelOr);
      this.m_startGroupbox.Controls.Add(this.button2);
      this.m_startGroupbox.Controls.Add(this.button1);
      this.m_startGroupbox.Location = new System.Drawing.Point(21, 15);
      this.m_startGroupbox.Name = "m_startGroupbox";
      this.m_startGroupbox.Size = new System.Drawing.Size(315, 187);
      this.m_startGroupbox.TabIndex = 2;
      this.m_startGroupbox.TabStop = false;
      // 
      // m_labelOr
      // 
      this.m_labelOr.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.m_labelOr.AutoSize = true;
      this.m_labelOr.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_labelOr.Location = new System.Drawing.Point(138, 74);
      this.m_labelOr.Name = "m_labelOr";
      this.m_labelOr.Size = new System.Drawing.Size(40, 29);
      this.m_labelOr.TabIndex = 2;
      this.m_labelOr.Text = "Or";
      // 
      // StartupView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.m_startGroupbox);
      this.Name = "StartupView";
      this.Size = new System.Drawing.Size(359, 217);
      this.m_startGroupbox.ResumeLayout(false);
      this.m_startGroupbox.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.GroupBox m_startGroupbox;
    private System.Windows.Forms.Label m_labelOr;
  }
}
