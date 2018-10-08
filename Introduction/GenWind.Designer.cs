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
            this.ImageLoad = new System.Windows.Forms.Button();
            this.textEvent = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxRs)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.imageBox.Location = new System.Drawing.Point(12, 12);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(319, 274);
            this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox.TabIndex = 2;
            this.imageBox.TabStop = false;
            // 
            // imageBoxRs
            // 
            this.imageBoxRs.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.imageBoxRs.Location = new System.Drawing.Point(371, 12);
            this.imageBoxRs.Name = "imageBoxRs";
            this.imageBoxRs.Size = new System.Drawing.Size(315, 274);
            this.imageBoxRs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBoxRs.TabIndex = 3;
            this.imageBoxRs.TabStop = false;
            // 
            // ImageLoad
            // 
            this.ImageLoad.Location = new System.Drawing.Point(692, 12);
            this.ImageLoad.Name = "ImageLoad";
            this.ImageLoad.Size = new System.Drawing.Size(75, 23);
            this.ImageLoad.TabIndex = 4;
            this.ImageLoad.Text = "Load";
            this.ImageLoad.UseVisualStyleBackColor = true;
            this.ImageLoad.Click += new System.EventHandler(this.OpenNewImage);
            // 
            // textEvent
            // 
            this.textEvent.Location = new System.Drawing.Point(693, 41);
            this.textEvent.Name = "textEvent";
            this.textEvent.Size = new System.Drawing.Size(75, 76);
            this.textEvent.TabIndex = 5;
            this.textEvent.Text = "Test Event";
            this.textEvent.UseVisualStyleBackColor = true;
            this.textEvent.Click += new System.EventHandler(this.TestEvent);
            // 
            // Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 298);
            this.Controls.Add(this.textEvent);
            this.Controls.Add(this.ImageLoad);
            this.Controls.Add(this.imageBoxRs);
            this.Controls.Add(this.imageBox);
            this.Name = "Filter";
            this.Text = "Image Effects";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxRs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox;
        private Emgu.CV.UI.ImageBox imageBoxRs;
        private System.Windows.Forms.Button ImageLoad;
        private System.Windows.Forms.Button textEvent;
    }
}

