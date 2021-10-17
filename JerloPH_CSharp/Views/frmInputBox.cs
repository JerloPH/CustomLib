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
    public partial class frmInputBox : Form
    {
        public string Result { get; set; } = "";
        public List<String> Values { get; set; } = null; // Values already existing

        public frmInputBox(string message, List<String> contents, string defValue)
        {
            InitializeComponent();
            // Initialized Properties
            this.Text = Msg.CAPTION_DIALOG;
            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
            // TODO: Add Theme
            //Themes.SetTheme(this); // Themes

            cbContents.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbContents.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbContents.DropDownStyle = ComboBoxStyle.DropDown;
            //Themes.SetButtons(new List<Button>() { btnOk, btnCancel });

            // Initialize values
            lblMessage.Text = message;
            if (contents != null)
            {
                foreach (var item in contents)
                {
                    cbContents.Items.Add(item);
                }
                txtInput.Enabled = txtInput.Visible = false;
            }
            else
            {
                txtInput.Top = cbContents.Top;
                cbContents.Enabled = cbContents.Visible = false;
                txtInput.Text = defValue;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Values != null)
            {
                if (Values.Contains(cbContents.Text))
                {
                    Msg.ShowWarning("Value is already existing!", "", this);
                    return;
                }
                Result = cbContents.Text;
            }
            else
            {
                if (String.IsNullOrWhiteSpace(txtInput.Text))
                {
                    Msg.ShowWarning("Invalid value!", "", this);
                    txtInput.Focus();
                    return;
                }
                Result = txtInput.Text;
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbContents_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk.PerformClick();
            }
        }

        private void frmInputBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            Msg.RefocusParent(this);
        }
    }
}
