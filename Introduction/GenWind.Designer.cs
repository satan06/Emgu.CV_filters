﻿namespace Introduction
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
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ValueTrackbar = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.SaturTrackbar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.HueTrackbar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightnessContrastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BrContrPanel = new System.Windows.Forms.Panel();
            this.BrContrCloseButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.ContrTrackbar = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.BrTrackbar = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.WindowFilterPanel = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.windowFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MatArg0 = new System.Windows.Forms.NumericUpDown();
            this.MatArg1 = new System.Windows.Forms.NumericUpDown();
            this.MatArg2 = new System.Windows.Forms.NumericUpDown();
            this.MatArg5 = new System.Windows.Forms.NumericUpDown();
            this.MatArg4 = new System.Windows.Forms.NumericUpDown();
            this.MatArg3 = new System.Windows.Forms.NumericUpDown();
            this.MatArg8 = new System.Windows.Forms.NumericUpDown();
            this.MatArg7 = new System.Windows.Forms.NumericUpDown();
            this.MatArg6 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.CartnFilterPanel = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.CartnFilterCloseButton = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.cartoonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CartFilterThreshold = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxRs)).BeginInit();
            this.HSVPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ValueTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaturTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HueTrackbar)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.BrContrPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContrTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrTrackbar)).BeginInit();
            this.WindowFilterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MatArg0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatArg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatArg2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatArg5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatArg4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatArg3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatArg8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatArg7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatArg6)).BeginInit();
            this.CartnFilterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CartFilterThreshold)).BeginInit();
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
            this.HSVPanel.BackColor = System.Drawing.SystemColors.Menu;
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
            this.HSVPanel.Visible = false;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(108, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hue";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "HSV";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.FToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(824, 24);
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
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenImage);
            // 
            // FToolStripMenuItem
            // 
            this.FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hSVToolStripMenuItem,
            this.brightnessContrastToolStripMenuItem,
            this.windowFilterToolStripMenuItem,
            this.cartoonToolStripMenuItem});
            this.FToolStripMenuItem.Name = "FToolStripMenuItem";
            this.FToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.FToolStripMenuItem.Text = "Filter";
            // 
            // hSVToolStripMenuItem
            // 
            this.hSVToolStripMenuItem.Name = "hSVToolStripMenuItem";
            this.hSVToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hSVToolStripMenuItem.Text = "HSV";
            this.hSVToolStripMenuItem.Click += new System.EventHandler(this.HSVFIlter);
            // 
            // brightnessContrastToolStripMenuItem
            // 
            this.brightnessContrastToolStripMenuItem.Name = "brightnessContrastToolStripMenuItem";
            this.brightnessContrastToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.brightnessContrastToolStripMenuItem.Text = "Brightness/Contrast";
            this.brightnessContrastToolStripMenuItem.Click += new System.EventHandler(this.BrContrFilter);
            // 
            // BrContrPanel
            // 
            this.BrContrPanel.BackColor = System.Drawing.SystemColors.Menu;
            this.BrContrPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BrContrPanel.Controls.Add(this.BrContrCloseButton);
            this.BrContrPanel.Controls.Add(this.label6);
            this.BrContrPanel.Controls.Add(this.ContrTrackbar);
            this.BrContrPanel.Controls.Add(this.label7);
            this.BrContrPanel.Controls.Add(this.BrTrackbar);
            this.BrContrPanel.Controls.Add(this.label8);
            this.BrContrPanel.Location = new System.Drawing.Point(670, 27);
            this.BrContrPanel.Name = "BrContrPanel";
            this.BrContrPanel.Size = new System.Drawing.Size(149, 274);
            this.BrContrPanel.TabIndex = 8;
            this.BrContrPanel.Visible = false;
            // 
            // BrContrCloseButton
            // 
            this.BrContrCloseButton.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BrContrCloseButton.FlatAppearance.BorderSize = 0;
            this.BrContrCloseButton.Location = new System.Drawing.Point(126, -1);
            this.BrContrCloseButton.Name = "BrContrCloseButton";
            this.BrContrCloseButton.Size = new System.Drawing.Size(22, 22);
            this.BrContrCloseButton.TabIndex = 7;
            this.BrContrCloseButton.Text = "X";
            this.BrContrCloseButton.UseVisualStyleBackColor = false;
            this.BrContrCloseButton.Click += new System.EventHandler(this.BrContrClose);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(95, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Contrast";
            // 
            // ContrTrackbar
            // 
            this.ContrTrackbar.Location = new System.Drawing.Point(1, 153);
            this.ContrTrackbar.Maximum = 25;
            this.ContrTrackbar.Minimum = -25;
            this.ContrTrackbar.Name = "ContrTrackbar";
            this.ContrTrackbar.Size = new System.Drawing.Size(143, 45);
            this.ContrTrackbar.SmallChange = 5;
            this.ContrTrackbar.TabIndex = 3;
            this.ContrTrackbar.Scroll += new System.EventHandler(this.ContrScroll);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(85, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Brightness";
            // 
            // BrTrackbar
            // 
            this.BrTrackbar.Location = new System.Drawing.Point(1, 84);
            this.BrTrackbar.Maximum = 25;
            this.BrTrackbar.Minimum = -25;
            this.BrTrackbar.Name = "BrTrackbar";
            this.BrTrackbar.Size = new System.Drawing.Size(143, 45);
            this.BrTrackbar.SmallChange = 5;
            this.BrTrackbar.TabIndex = 1;
            this.BrTrackbar.Scroll += new System.EventHandler(this.BrightScroll);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Brightness/Contrast";
            // 
            // WindowFilterPanel
            // 
            this.WindowFilterPanel.BackColor = System.Drawing.SystemColors.Menu;
            this.WindowFilterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WindowFilterPanel.Controls.Add(this.button6);
            this.WindowFilterPanel.Controls.Add(this.label9);
            this.WindowFilterPanel.Controls.Add(this.MatArg8);
            this.WindowFilterPanel.Controls.Add(this.MatArg7);
            this.WindowFilterPanel.Controls.Add(this.MatArg6);
            this.WindowFilterPanel.Controls.Add(this.MatArg5);
            this.WindowFilterPanel.Controls.Add(this.MatArg4);
            this.WindowFilterPanel.Controls.Add(this.MatArg3);
            this.WindowFilterPanel.Controls.Add(this.MatArg2);
            this.WindowFilterPanel.Controls.Add(this.MatArg1);
            this.WindowFilterPanel.Controls.Add(this.MatArg0);
            this.WindowFilterPanel.Controls.Add(this.button5);
            this.WindowFilterPanel.Controls.Add(this.button4);
            this.WindowFilterPanel.Controls.Add(this.label5);
            this.WindowFilterPanel.Controls.Add(this.button3);
            this.WindowFilterPanel.Controls.Add(this.button2);
            this.WindowFilterPanel.Controls.Add(this.label10);
            this.WindowFilterPanel.Location = new System.Drawing.Point(670, 27);
            this.WindowFilterPanel.Name = "WindowFilterPanel";
            this.WindowFilterPanel.Size = new System.Drawing.Size(149, 274);
            this.WindowFilterPanel.TabIndex = 9;
            this.WindowFilterPanel.Visible = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.Location = new System.Drawing.Point(126, -1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(22, 22);
            this.button2.TabIndex = 7;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.WinFilterClose);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Window Filter";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button3.Location = new System.Drawing.Point(18, 64);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(56, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Sharpen";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.WinFilterSharpen);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(79, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Templates";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button4.Location = new System.Drawing.Point(80, 64);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(56, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "Embos";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.WinFilterEmbos);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button5.Location = new System.Drawing.Point(47, 93);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(56, 23);
            this.button5.TabIndex = 11;
            this.button5.Text = "Edges";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.WinFilterEdges);
            // 
            // windowFilterToolStripMenuItem
            // 
            this.windowFilterToolStripMenuItem.Name = "windowFilterToolStripMenuItem";
            this.windowFilterToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.windowFilterToolStripMenuItem.Text = "Window Filter";
            this.windowFilterToolStripMenuItem.Click += new System.EventHandler(this.WinFilter);
            // 
            // MatArg0
            // 
            this.MatArg0.Location = new System.Drawing.Point(31, 162);
            this.MatArg0.Name = "MatArg0";
            this.MatArg0.Size = new System.Drawing.Size(30, 20);
            this.MatArg0.TabIndex = 12;
            // 
            // MatArg1
            // 
            this.MatArg1.Location = new System.Drawing.Point(67, 162);
            this.MatArg1.Name = "MatArg1";
            this.MatArg1.Size = new System.Drawing.Size(30, 20);
            this.MatArg1.TabIndex = 13;
            // 
            // MatArg2
            // 
            this.MatArg2.Location = new System.Drawing.Point(103, 162);
            this.MatArg2.Name = "MatArg2";
            this.MatArg2.Size = new System.Drawing.Size(30, 20);
            this.MatArg2.TabIndex = 14;
            // 
            // MatArg5
            // 
            this.MatArg5.Location = new System.Drawing.Point(103, 188);
            this.MatArg5.Name = "MatArg5";
            this.MatArg5.Size = new System.Drawing.Size(30, 20);
            this.MatArg5.TabIndex = 17;
            // 
            // MatArg4
            // 
            this.MatArg4.Location = new System.Drawing.Point(67, 188);
            this.MatArg4.Name = "MatArg4";
            this.MatArg4.Size = new System.Drawing.Size(30, 20);
            this.MatArg4.TabIndex = 16;
            // 
            // MatArg3
            // 
            this.MatArg3.Location = new System.Drawing.Point(31, 188);
            this.MatArg3.Name = "MatArg3";
            this.MatArg3.Size = new System.Drawing.Size(30, 20);
            this.MatArg3.TabIndex = 15;
            // 
            // MatArg8
            // 
            this.MatArg8.Location = new System.Drawing.Point(103, 214);
            this.MatArg8.Name = "MatArg8";
            this.MatArg8.Size = new System.Drawing.Size(30, 20);
            this.MatArg8.TabIndex = 20;
            // 
            // MatArg7
            // 
            this.MatArg7.Location = new System.Drawing.Point(67, 214);
            this.MatArg7.Name = "MatArg7";
            this.MatArg7.Size = new System.Drawing.Size(30, 20);
            this.MatArg7.TabIndex = 19;
            // 
            // MatArg6
            // 
            this.MatArg6.Location = new System.Drawing.Point(31, 214);
            this.MatArg6.Name = "MatArg6";
            this.MatArg6.Size = new System.Drawing.Size(30, 20);
            this.MatArg6.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(64, 132);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Custom Input";
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.Info;
            this.button6.Location = new System.Drawing.Point(80, 240);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(53, 23);
            this.button6.TabIndex = 22;
            this.button6.Text = "Send";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.WinFilerCustomMatSend);
            // 
            // CartnFilterPanel
            // 
            this.CartnFilterPanel.BackColor = System.Drawing.SystemColors.Menu;
            this.CartnFilterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CartnFilterPanel.Controls.Add(this.CartFilterThreshold);
            this.CartnFilterPanel.Controls.Add(this.label12);
            this.CartnFilterPanel.Controls.Add(this.CartnFilterCloseButton);
            this.CartnFilterPanel.Controls.Add(this.label13);
            this.CartnFilterPanel.Location = new System.Drawing.Point(670, 27);
            this.CartnFilterPanel.Name = "CartnFilterPanel";
            this.CartnFilterPanel.Size = new System.Drawing.Size(149, 146);
            this.CartnFilterPanel.TabIndex = 23;
            this.CartnFilterPanel.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "Threshold";
            // 
            // CartnFilterCloseButton
            // 
            this.CartnFilterCloseButton.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.CartnFilterCloseButton.FlatAppearance.BorderSize = 0;
            this.CartnFilterCloseButton.Location = new System.Drawing.Point(126, -1);
            this.CartnFilterCloseButton.Name = "CartnFilterCloseButton";
            this.CartnFilterCloseButton.Size = new System.Drawing.Size(22, 22);
            this.CartnFilterCloseButton.TabIndex = 7;
            this.CartnFilterCloseButton.Text = "X";
            this.CartnFilterCloseButton.UseVisualStyleBackColor = false;
            this.CartnFilterCloseButton.Click += new System.EventHandler(this.CartnFilterClose);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 11);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Cartoon";
            // 
            // cartoonToolStripMenuItem
            // 
            this.cartoonToolStripMenuItem.Name = "cartoonToolStripMenuItem";
            this.cartoonToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cartoonToolStripMenuItem.Text = "Cartoon";
            this.cartoonToolStripMenuItem.Click += new System.EventHandler(this.CartnFilter);
            // 
            // CartFilterThreshold
            // 
            this.CartFilterThreshold.Location = new System.Drawing.Point(83, 46);
            this.CartFilterThreshold.Name = "CartFilterThreshold";
            this.CartFilterThreshold.Size = new System.Drawing.Size(54, 20);
            this.CartFilterThreshold.TabIndex = 23;
            this.CartFilterThreshold.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.CartFilterThreshold.ValueChanged += new System.EventHandler(this.CartFilterThresholdChanged);
            // 
            // Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 319);
            this.Controls.Add(this.CartnFilterPanel);
            this.Controls.Add(this.WindowFilterPanel);
            this.Controls.Add(this.BrContrPanel);
            this.Controls.Add(this.HSVPanel);
            this.Controls.Add(this.imageBoxRs);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Filter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Effects";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxRs)).EndInit();
            this.HSVPanel.ResumeLayout(false);
            this.HSVPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ValueTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaturTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HueTrackbar)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.BrContrPanel.ResumeLayout(false);
            this.BrContrPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContrTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrTrackbar)).EndInit();
            this.WindowFilterPanel.ResumeLayout(false);
            this.WindowFilterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MatArg0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatArg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatArg2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatArg5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatArg4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatArg3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatArg8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatArg7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatArg6)).EndInit();
            this.CartnFilterPanel.ResumeLayout(false);
            this.CartnFilterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CartFilterThreshold)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hSVToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel BrContrPanel;
        private System.Windows.Forms.Button BrContrCloseButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar ContrTrackbar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar BrTrackbar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripMenuItem brightnessContrastToolStripMenuItem;
        private System.Windows.Forms.Panel WindowFilterPanel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripMenuItem windowFilterToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown MatArg8;
        private System.Windows.Forms.NumericUpDown MatArg7;
        private System.Windows.Forms.NumericUpDown MatArg6;
        private System.Windows.Forms.NumericUpDown MatArg5;
        private System.Windows.Forms.NumericUpDown MatArg4;
        private System.Windows.Forms.NumericUpDown MatArg3;
        private System.Windows.Forms.NumericUpDown MatArg2;
        private System.Windows.Forms.NumericUpDown MatArg1;
        private System.Windows.Forms.NumericUpDown MatArg0;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Panel CartnFilterPanel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button CartnFilterCloseButton;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ToolStripMenuItem cartoonToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown CartFilterThreshold;
    }
}

