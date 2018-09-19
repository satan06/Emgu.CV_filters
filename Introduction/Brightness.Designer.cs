namespace Introduction
{
    partial class Brightness
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
            this.BrTrackBarUI = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BrTrackBarUI)).BeginInit();
            this.SuspendLayout();
            // 
            // BrTrackBarUI
            // 
            this.BrTrackBarUI.Location = new System.Drawing.Point(12, 44);
            this.BrTrackBarUI.Name = "BrTrackBarUI";
            this.BrTrackBarUI.Size = new System.Drawing.Size(129, 45);
            this.BrTrackBarUI.TabIndex = 0;
            this.BrTrackBarUI.Scroll += new System.EventHandler(this.BrTrackBarUI_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Brightness value";
            // 
            // Brightness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(149, 81);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BrTrackBarUI);
            this.Name = "Brightness";
            this.Text = "Brightness";
            ((System.ComponentModel.ISupportInitialize)(this.BrTrackBarUI)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar BrTrackBarUI;
        private System.Windows.Forms.Label label1;
    }
}