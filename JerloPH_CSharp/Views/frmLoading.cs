using JerloPH_CSharp.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static JerloPH_CSharp.Msg;

namespace JerloPH_CSharp.Views
{
    public partial class frmLoading : Form
    {
        public string Caption
        {
            get { return this.Text; }
            set { this.Text = value; }
        }
        public string Message
        {
            get { return lblMessage.Text; }
            set
            {
                if (lblMessage.InvokeRequired)
                {
                    BeginInvoke((Action)delegate
                    {
                        lblMessage.Text = value;
                    });
                }
                else
                    lblMessage.Text = value;
            }
        }
        public string ProgressText
        {
            get { return lblProgress.Text; }
            set
            {
                if (lblProgress.InvokeRequired)
                {
                    BeginInvoke((Action)delegate
                    {
                        lblProgress.Text = value;
                    });
                }
                else
                    lblProgress.Text = value;
            }
        }
        private long MaxProgressHidden = 0;
        public long MaxProgress
        {
            get { return MaxProgressHidden; }
            set { MaxProgressHidden = value; }
        }
        private long ProgressCountHidden = 0;
        public long ProgressCount
        {
            get { return ProgressCountHidden; }
            set { ProgressCountHidden = value; }
        }


        public frmLoading()
        {
            InitializeComponent();
        }
        public frmLoading(string message, string caption, long _maxprogress)
        {
            InitializeComponent();
            Message = message;
            Caption = caption;
            // TODO: Add Theme class
            //Themes.SetTheme(this);
            picLoading.BackColor = Color.Transparent;
            picLoading.BringToFront();
            if (_maxprogress > 0)
            {
                MaxProgressHidden = _maxprogress;
                this.BackgroundWorker.WorkerReportsProgress = true;
                this.BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            }
            lblProgress.Visible = (_maxprogress > 0);
            CenterToParent();
        }
        #region Public Functions
        public void UpdateProgress(long progress = 1)
        {
            try
            {
                ProgressCount += 1;
                decimal percent = ((decimal)ProgressCount / (decimal)MaxProgress) * 100m;
                if (ProgressCount == MaxProgress)
                    percent = 100m;

                //Logs.Debug($"Percent: [{percent.ToString("###.##")}], Progress: [{progress}], Max Progress: [{MaxProgress}]");
                BackgroundWorker.ReportProgress((int)percent, ProgressCount);
            }
            catch (Exception ex) { Logs.Err(ex); }
        }
        public void SetIcon(LoadIcons IconIndex)
        {
            
            try
            {
                Bitmap image = (Bitmap)Msg.GetImageIcon(IconIndex);
                if (picLoading.InvokeRequired)
                {
                    this.BeginInvoke((Action)delegate
                    {
                        picLoading.Image?.Dispose();
                        picLoading.Image = image;
                    });
                }
                else
                {
                    picLoading.Image?.Dispose();
                    picLoading.Image = image;
                }

            }
            catch (Exception ex) { Logs.Err(ex); }
        }
        private delegate void UpdateMessageThreadSafeDelegate(string message);
        public void UpdateMessage(string message)
        {
            if (lblMessage.InvokeRequired)
            {
                lblMessage.Invoke(new UpdateMessageThreadSafeDelegate(UpdateMessage), new object[] { message });
            }
            else
                lblMessage.Text = message;
        }
        #endregion

        private void frmLoading_Shown(object sender, EventArgs e)
        {
            if (BackgroundWorker.IsBusy)
                return;
            BackgroundWorker.RunWorkerAsync();
        }

        private void frmLoading_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BackgroundWorker.IsBusy)
                e.Cancel = true;

            picLoading.Image?.Dispose();
            Msg.RefocusParent(this);
            Dispose();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (MaxProgress < 1)
                Close();
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage > -1 && MaxProgress > 0)
            {
                if (long.TryParse(e.UserState.ToString(), out long val))
                {
                    ProgressCount = val;
                }
            }

            if (ProgressCount == MaxProgress)
            {
                ProgressText = $"Done loading!";
                SetIcon(LoadIcons.Check);
                Close();
            }
            else
                ProgressText = $"Progress: {ProgressCount} / {MaxProgress}";
        }

        private void frmLoading_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
