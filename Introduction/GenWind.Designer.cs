namespace Introduction
{
    partial class Filter
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imageBox = new Emgu.CV.UI.ImageBox();
            this.imageBoxRs = new Emgu.CV.UI.ImageBox();
            this.HSVPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.HueTrackbar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SaturTrackbar = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.ValueTrackbar = new System.Windows.Forms.TrackBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxRs)).BeginInit();
            this.HSVPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HueTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaturTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValueTrackbar)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.imageBox.Location = new System.Drawing.Point(12, 27);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(319, 274);
            this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox.TabIndex = 2;
            this.imageBox.TabStop = false;
            // 
            // imageBoxRs
            // 
            this.imageBoxRs.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.imageBoxRs.Location = new System.Drawing.Point(349, 27);
            this.imageBoxRs.Name = "imageBoxRs";
            this.imageBoxRs.Size = new System.Drawing.Size(315, 274);
            this.imageBoxRs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBoxRs.TabIndex = 3;
            this.imageBoxRs.TabStop = false;
            // 
            // HSVPanel
            // 
            this.HSVPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HSVPanel.Controls.Add(this.button1);
            this.HSVPanel.Controls.Add(this.label4);
            this.HSVPanel.Controls.Add(this.ValueTrackbar);
            this.HSVPanel.Controls.Add(this.label3);
            this.HSVPanel.Controls.Add(this.SaturTrackbar);
            this.HSVPanel.Controls.Add(this.label2);
            this.HSVPanel.Controls.Add(this.HueTrackbar);
            this.HSVPanel.Controls.Add(this.label1);
            this.HSVPanel.Location = new System.Drawing.Point(670, 27);
            this.HSVPanel.Name = "HSVPanel";
            this.HSVPanel.Size = new System.Drawing.Size(149, 274);
            this.HSVPanel.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "HSV";
            // 
            // HueTrackbar
            // 
            this.HueTrackbar.Location = new System.Drawing.Point(3, 55);
            this.HueTrackbar.Maximum = 25;
            this.HueTrackbar.Minimum = -25;
            this.HueTrackbar.Name = "HueTrackbar";
            this.HueTrackbar.Size = new System.Drawing.Size(143, 45);
            this.HueTrackbar.TabIndex = 1;
            this.HueTrackbar.Scroll += new System.EventHandler(this.HueScroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(108, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hue";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Saturation";
            // 
            // SaturTrackbar
            // 
            this.SaturTrackbar.Location = new System.Drawing.Point(3, 124);
            this.SaturTrackbar.Maximum = 25;
            this.SaturTrackbar.Minimum = -25;
            this.SaturTrackbar.Name = "SaturTrackbar";
            this.SaturTrackbar.Size = new System.Drawing.Size(143, 45);
            this.SaturTrackbar.TabIndex = 3;
            this.SaturTrackbar.Scroll += new System.EventHandler(this.SaturScroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(101, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Value";
            // 
            // ValueTrackbar
            // 
            this.ValueTrackbar.Location = new System.Drawing.Point(0, 189);
            this.ValueTrackbar.Maximum = 25;
            this.ValueTrackbar.Minimum = -25;
            this.ValueTrackbar.Name = "ValueTrackbar";
            this.ValueTrackbar.Size = new System.Drawing.Size(143, 45);
            this.ValueTrackbar.TabIndex = 5;
            this.ValueTrackbar.Scroll += new System.EventHandler(this.ValueScroll);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.filterToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(831, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hSVToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // hSVToolStripMenuItem
            // 
            this.hSVToolStripMenuItem.Name = "hSVToolStripMenuItem";
            this.hSVToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hSVToolStripMenuItem.Text = "HSV";
            this.hSVToolStripMenuItem.Click += new System.EventHandler(this.HSVFIlter);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenImage);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Location = new System.Drawing.Point(126, -1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(22, 22);
            this.button1.TabIndex = 7;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.HSVClose);
            // 
            // Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 319);
            this.Controls.Add(this.HSVPanel);
            this.Controls.Add(this.imageBoxRs);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Filter";
            this.Text = "Image Effects";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxRs)).EndInit();
            this.HSVPanel.ResumeLayout(false);
            this.HSVPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HueTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaturTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValueTrackbar)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox;
        private Emgu.CV.UI.ImageBox imageBoxRs;
        private System.Windows.Forms.Panel HSVPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar ValueTrackbar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar SaturTrackbar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar HueTrackbar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hSVToolStripMenuItem;
        private System.Windows.Forms.Button button1;
    }
}

