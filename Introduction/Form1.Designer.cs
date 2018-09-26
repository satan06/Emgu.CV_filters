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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rGBCombineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blackWhiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sepiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transferModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doubleExposureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cartoonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightnessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contrastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxRs)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.imageBox.Location = new System.Drawing.Point(12, 37);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(401, 348);
            this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox.TabIndex = 2;
            this.imageBox.TabStop = false;
            // 
            // imageBoxRs
            // 
            this.imageBoxRs.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.imageBoxRs.Location = new System.Drawing.Point(439, 37);
            this.imageBoxRs.Name = "imageBoxRs";
            this.imageBoxRs.Size = new System.Drawing.Size(401, 348);
            this.imageBoxRs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBoxRs.TabIndex = 3;
            this.imageBoxRs.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.filterToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(856, 24);
            this.menuStrip1.TabIndex = 4;
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
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenFileUI);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.channelToolStripMenuItem,
            this.blackWhiteToolStripMenuItem,
            this.sepiaToolStripMenuItem,
            this.transferModeToolStripMenuItem,
            this.brightnessToolStripMenuItem,
            this.contrastToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // channelToolStripMenuItem
            // 
            this.channelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.redToolStripMenuItem,
            this.greenToolStripMenuItem,
            this.blueToolStripMenuItem,
            this.rGBCombineToolStripMenuItem});
            this.channelToolStripMenuItem.Name = "channelToolStripMenuItem";
            this.channelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.channelToolStripMenuItem.Text = "Channel";
            // 
            // redToolStripMenuItem
            // 
            this.redToolStripMenuItem.Name = "redToolStripMenuItem";
            this.redToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.redToolStripMenuItem.Text = "Red";
            this.redToolStripMenuItem.Click += new System.EventHandler(this.RedChUI);
            // 
            // greenToolStripMenuItem
            // 
            this.greenToolStripMenuItem.Name = "greenToolStripMenuItem";
            this.greenToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.greenToolStripMenuItem.Text = "Green";
            this.greenToolStripMenuItem.Click += new System.EventHandler(this.GreenChUI);
            // 
            // blueToolStripMenuItem
            // 
            this.blueToolStripMenuItem.Name = "blueToolStripMenuItem";
            this.blueToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.blueToolStripMenuItem.Text = "Blue";
            this.blueToolStripMenuItem.Click += new System.EventHandler(this.BlueChUI);
            // 
            // rGBCombineToolStripMenuItem
            // 
            this.rGBCombineToolStripMenuItem.Name = "rGBCombineToolStripMenuItem";
            this.rGBCombineToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.rGBCombineToolStripMenuItem.Text = "RGB (Combine)";
            // 
            // blackWhiteToolStripMenuItem
            // 
            this.blackWhiteToolStripMenuItem.Name = "blackWhiteToolStripMenuItem";
            this.blackWhiteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.blackWhiteToolStripMenuItem.Text = "Black/White";
            this.blackWhiteToolStripMenuItem.Click += new System.EventHandler(this.BWToolUI);
            // 
            // sepiaToolStripMenuItem
            // 
            this.sepiaToolStripMenuItem.Name = "sepiaToolStripMenuItem";
            this.sepiaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sepiaToolStripMenuItem.Text = "Sepia";
            this.sepiaToolStripMenuItem.Click += new System.EventHandler(this.SepiaUI);
            // 
            // transferModeToolStripMenuItem
            // 
            this.transferModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hSVToolStripMenuItem,
            this.doubleExposureToolStripMenuItem,
            this.blurToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.paintToolStripMenuItem,
            this.cartoonToolStripMenuItem});
            this.transferModeToolStripMenuItem.Name = "transferModeToolStripMenuItem";
            this.transferModeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.transferModeToolStripMenuItem.Text = "Image Effects";
            // 
            // hSVToolStripMenuItem
            // 
            this.hSVToolStripMenuItem.Name = "hSVToolStripMenuItem";
            this.hSVToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.hSVToolStripMenuItem.Text = "HSV";
            // 
            // doubleExposureToolStripMenuItem
            // 
            this.doubleExposureToolStripMenuItem.Name = "doubleExposureToolStripMenuItem";
            this.doubleExposureToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.doubleExposureToolStripMenuItem.Text = "Double Exposure";
            // 
            // blurToolStripMenuItem
            // 
            this.blurToolStripMenuItem.Name = "blurToolStripMenuItem";
            this.blurToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.blurToolStripMenuItem.Text = "Blur";
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // paintToolStripMenuItem
            // 
            this.paintToolStripMenuItem.Name = "paintToolStripMenuItem";
            this.paintToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.paintToolStripMenuItem.Text = "Paint";
            // 
            // cartoonToolStripMenuItem
            // 
            this.cartoonToolStripMenuItem.Name = "cartoonToolStripMenuItem";
            this.cartoonToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.cartoonToolStripMenuItem.Text = "Cartoon";
            // 
            // brightnessToolStripMenuItem
            // 
            this.brightnessToolStripMenuItem.Name = "brightnessToolStripMenuItem";
            this.brightnessToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.brightnessToolStripMenuItem.Text = "Brightness";
            this.brightnessToolStripMenuItem.Click += new System.EventHandler(this.BrightToolUI);
            // 
            // contrastToolStripMenuItem
            // 
            this.contrastToolStripMenuItem.Name = "contrastToolStripMenuItem";
            this.contrastToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.contrastToolStripMenuItem.Text = "Contrast";
            this.contrastToolStripMenuItem.Click += new System.EventHandler(this.ContrastUI);
            // 
            // Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 404);
            this.Controls.Add(this.imageBoxRs);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Filter";
            this.Text = "Image Effects";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxRs)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox;
        private Emgu.CV.UI.ImageBox imageBoxRs;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blackWhiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sepiaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transferModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doubleExposureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blurToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cartoonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rGBCombineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brightnessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contrastToolStripMenuItem;
    }
}

