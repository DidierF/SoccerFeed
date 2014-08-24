namespace WindowsFormsApplication1
{
    partial class StartWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OpenBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.team1 = new System.Windows.Forms.ComboBox();
            this.team2 = new System.Windows.Forms.ComboBox();
            this.Team1Players = new System.Windows.Forms.TextBox();
            this.Start = new System.Windows.Forms.Button();
            this.Stadium = new System.Windows.Forms.TextBox();
            this.Date = new System.Windows.Forms.TextBox();
            this.Team2Players = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // OpenBtn
            // 
            this.OpenBtn.Location = new System.Drawing.Point(13, 13);
            this.OpenBtn.Name = "OpenBtn";
            this.OpenBtn.Size = new System.Drawing.Size(75, 23);
            this.OpenBtn.TabIndex = 0;
            this.OpenBtn.Text = "Open";
            this.OpenBtn.UseVisualStyleBackColor = true;
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(13, 43);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveBtn.TabIndex = 1;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = true;
            // 
            // team1
            // 
            this.team1.FormattingEnabled = true;
            this.team1.Location = new System.Drawing.Point(12, 104);
            this.team1.Name = "team1";
            this.team1.Size = new System.Drawing.Size(184, 21);
            this.team1.TabIndex = 2;
            this.team1.SelectedIndexChanged += new System.EventHandler(this.team1_SelectedIndexChanged);
            // 
            // team2
            // 
            this.team2.FormattingEnabled = true;
            this.team2.Location = new System.Drawing.Point(247, 104);
            this.team2.Name = "team2";
            this.team2.Size = new System.Drawing.Size(184, 21);
            this.team2.TabIndex = 3;
            // 
            // Team1Players
            // 
            this.Team1Players.Location = new System.Drawing.Point(12, 151);
            this.Team1Players.Multiline = true;
            this.Team1Players.Name = "Team1Players";
            this.Team1Players.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Team1Players.Size = new System.Drawing.Size(184, 328);
            this.Team1Players.TabIndex = 4;
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(306, 492);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(125, 42);
            this.Start.TabIndex = 5;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            // 
            // Stadium
            // 
            this.Stadium.Location = new System.Drawing.Point(13, 514);
            this.Stadium.Name = "Stadium";
            this.Stadium.Size = new System.Drawing.Size(123, 20);
            this.Stadium.TabIndex = 6;
            this.Stadium.Text = "Stadium";
            this.Stadium.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Date
            // 
            this.Date.Location = new System.Drawing.Point(177, 514);
            this.Date.Name = "Date";
            this.Date.Size = new System.Drawing.Size(123, 20);
            this.Date.TabIndex = 7;
            this.Date.Text = "Date";
            this.Date.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Team2Players
            // 
            this.Team2Players.Location = new System.Drawing.Point(247, 151);
            this.Team2Players.Multiline = true;
            this.Team2Players.Name = "Team2Players";
            this.Team2Players.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Team2Players.Size = new System.Drawing.Size(184, 328);
            this.Team2Players.TabIndex = 8;
            // 
            // StartWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 555);
            this.Controls.Add(this.Team2Players);
            this.Controls.Add(this.Date);
            this.Controls.Add(this.Stadium);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.Team1Players);
            this.Controls.Add(this.team2);
            this.Controls.Add(this.team1);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.OpenBtn);
            this.Name = "StartWindow";
            this.Text = "StartWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.ComboBox team1;
        private System.Windows.Forms.ComboBox team2;
        private System.Windows.Forms.TextBox Team1Players;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.TextBox Stadium;
        private System.Windows.Forms.TextBox Date;
        private System.Windows.Forms.TextBox Team2Players;
    }
}