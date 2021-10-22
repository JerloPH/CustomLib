using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JerloPH_CSharp.Views
{
    public partial class frmAlert : Form
    {
        public frmAlert()
        {
            InitializeComponent();
        }
        public frmAlert(string message, string caption, Msg.MsgType button, Form parent, Msg.LoadIcons icon)
        {
            InitializeComponent();
            var centerForm = Width / 2; // center point of form
            int adjlbl = 0; // Adjust when there's an icon
            // TODO: Add Theme
            //Themes.SetTheme(this); // Themes
            Text = caption;
            TopMost = true;
            //TopLevel = false;
            //Parent = (Form)Program.FormMain;
            if (parent != null)
                CenterToParent();
            else
                CenterToScreen();

            lblMessage.Text = message;

            // Disable all buttons first
            btnOk.Enabled = btnOk.Visible = false;
            btnYes.Enabled = btnYes.Visible = false;
            btnNo.Enabled = btnNo.Visible = false;

            // Assign Icon
            picIcon.BackColor = Color.Transparent;
            picIcon.BringToFront();
            if (icon == Msg.LoadIcons.Default)
            {
                // No Icon
                picIcon.Visible = false;
                lblMessage.Left = picIcon.Left;
                // Center label message
                lblMessage.Left = ((ClientRectangle.Width - lblMessage.Width) / 2) - 8 + adjlbl;
            }
            else
            {
                Bitmap image = (Bitmap)Msg.GetImageIcon(icon);
                if (picIcon.InvokeRequired)
                {
                    this.BeginInvoke((Action)delegate
                    {
                        picIcon.Image?.Dispose();
                        picIcon.Image = image;
                    });
                }
                else
                {
                    picIcon.Image?.Dispose();
                    picIcon.Image = image;
                }
                if (lblMessage.Height < picIcon.Height)
                {
                    lblMessage.AutoSize = false;
                    lblMessage.TextAlign = ContentAlignment.TopLeft;
                    lblMessage.Width = (ClientRectangle.Width - lblMessage.Left - 8);
                    lblMessage.Height += picIcon.Height - lblMessage.Height + 8;
                }
            }
            // Adjust form height, with label message height
            Height = lblMessage.Bottom + btnOk.Height + 64;

            // Switch type of prompt message
            switch (button)
            {
                case Msg.MsgType.YesNo: // Yes, No
                {
                    btnYes.Enabled = btnYes.Visible = true;
                    btnNo.Enabled = btnNo.Visible = true;
                    btnYes.Top = this.Height - (btnYes.Height + 48);
                    btnNo.Top = btnYes.Top;
                    btnYes.Left = centerForm - (int)(btnYes.Width * 1.5);
                    btnNo.Left = centerForm + (btnNo.Width / 2);
                    btnYes.DialogResult = DialogResult.Yes;
                    btnNo.DialogResult = DialogResult.No;
                    btnYes.ForeColor = Color.Black;
                    btnNo.ForeColor = Color.Black;
                    break;
                }
                default: // Simple messagebox
                {
                    btnOk.Enabled = btnOk.Visible = true;
                    btnOk.Top = ClientRectangle.Height - (btnOk.Height + 8);
                    btnOk.Left = centerForm - (btnOk.Width / 2) - 10;
                    if (parent != null)
                    {
                        btnOk.ForeColor = Color.Black;
                    }
                    else
                    {
                        // For use in Program.cs
                        btnOk.ForeColor = Color.White;
                        btnOk.BackColor = Color.Black;
                    }
                    btnOk.DialogResult = DialogResult.OK;
                    break;
                }
            }
        }

        private void frmAlert_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (picIcon.Visible)
                picIcon.Image?.Dispose();

            Msg.RefocusParent(this);
            Dispose(); // Finally, dispose
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
