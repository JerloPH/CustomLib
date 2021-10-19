
namespace Test
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
            this.btnTestLoading = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTestLoading
            // 
            this.btnTestLoading.Location = new System.Drawing.Point(28, 46);
            this.btnTestLoading.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnTestLoading.Name = "btnTestLoading";
            this.btnTestLoading.Size = new System.Drawing.Size(234, 73);
            this.btnTestLoading.TabIndex = 0;
            this.btnTestLoading.Text = "Test Loading Form";
            this.btnTestLoading.UseVisualStyleBackColor = true;
            this.btnTestLoading.Click += new System.EventHandler(this.btnTestLoading_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 321);
            this.Controls.Add(this.btnTestLoading);
            this.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTestLoading;
    }
}

