namespace WinFormServer
{
  partial class Form1
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnStop = new Button();
            btnStart = new Button();
            Clients = new ListBox();
            label2 = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnStop
            // 
            btnStop.Location = new Point(124, 53);
            btnStop.Margin = new Padding(2, 3, 2, 3);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(105, 39);
            btnStop.TabIndex = 20;
            btnStop.Text = "STOP";
            btnStop.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(12, 53);
            btnStart.Margin = new Padding(2, 3, 2, 3);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(105, 39);
            btnStart.TabIndex = 19;
            btnStart.Text = "RUN";
            btnStart.UseVisualStyleBackColor = true;
            // 
            // Clients
            // 
            Clients.FormattingEnabled = true;
            Clients.ItemHeight = 15;
            Clients.Location = new Point(12, 128);
            Clients.Margin = new Padding(2, 3, 2, 3);
            Clients.Name = "Clients";
            Clients.Size = new Size(215, 274);
            Clients.TabIndex = 17;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 104);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(59, 21);
            label2.TabIndex = 15;
            label2.Text = "Clients";
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(35, 35, 50);
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("맑은 고딕", 14F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(241, 241, 241);
            label1.Location = new Point(0, 0);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(239, 40);
            label1.TabIndex = 14;
            label1.Text = " Liar Game Server";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(239, 414);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(Clients);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(2, 3, 2, 3);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

    private Button btnStop;
    private Button btnStart;
    private ListBox Clients;
    private Label label2;
    private Label label1;
  }
}