
namespace JerloPH_CSharp.Views
{
    partial class frmLoading
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
            this.BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.lblMessage = new System.Windows.Forms.Label();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.lblProgress = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // BackgroundWorker
            // 
            this.BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMessage.Location = new System.Drawing.Point(0, 11);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(475, 62);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "INSERT MESSAGE HERE";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picLoading
            // 
            this.picLoading.BackColor = System.Drawing.Color.Transparent;
            this.picLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picLoading.Image = global::JerloPH_CSharp.Properties.Resources.LoadingColored;
            this.picLoading.Location = new System.Drawing.Point(145, 77);
            this.picLoading.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(182, 160);
            this.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLoading.TabIndex = 3;
            this.picLoading.TabStop = false;
            // 
            // lblProgress
            // 
            this.lblProgress.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblProgress.Location = new System.Drawing.Point(0, 243);
            this.lblProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(475, 62);
            this.lblProgress.TabIndex = 4;
            this.lblProgress.Text = "Progress: 1/1";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmLoading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(487, 311);
            this.ControlBox = false;
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.lblMessage);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoading";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "main";
            this.Text = "frmLoading";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLoading_FormClosing);
            this.Shown += new System.EventHandler(this.frmLoading_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLoading_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox picLoading;
        private System.Windows.Forms.Label lblMessage;
        public System.ComponentModel.BackgroundWorker BackgroundWorker;
        private System.Windows.Forms.Label lblProgress;
    }
}