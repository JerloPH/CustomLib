using JerloPH_CSharp.Properties;
using JerloPH_CSharp.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JerloPH_CSharp
{
    public static class Msg
    {
        private static string PATH_LOG = "";
        private static string CAPTION_DIALOG = "";
        private static Form DEF_PARENT = null;

        public enum LoadIcons
        {
            Default,
            Info,
            Check,
            Warning,
            Error,
            Question
        }

        public enum MsgType
        {
            Default = 0,
            YesNo = 1
        }

        public static void Initialize(string _logPath, string _defaultCaption)
        {
            PATH_LOG = _logPath;
            CAPTION_DIALOG = _defaultCaption;
        }

        public static void SetDefaultParent(Form _parent)
        {
            DEF_PARENT = _parent;
        }

        public static Image GetImageIcon(LoadIcons IconIndex)
        {
            return IconIndex switch
            {
                LoadIcons.Info => Resources.IconInfo,
                LoadIcons.Check => Resources.IconCheckmark,
                LoadIcons.Warning => Resources.IconWarning,
                LoadIcons.Question => Resources.IconQuestion,
                LoadIcons.Error => Resources.IconError,
                _ => Resources.LoadingColored,
            };
        }

        public static void RefocusParent(Form caller)
        {
            try
            {
                caller.Parent?.Focus();
            }
            catch { }
        }

        public static frmLoading Load(string message, string caption, long maxProgress)
        {
            var form = new frmLoading(message, (!String.IsNullOrWhiteSpace(caption) ? caption : CAPTION_DIALOG), maxProgress);
            return form;
        }
        public static frmLoading Load(string message, string caption)
        {
            var form = new frmLoading(message, (!String.IsNullOrWhiteSpace(caption) ? caption : CAPTION_DIALOG), 0);
            return form;
        }

        public static frmAlert ShowNoParent(string msg, string caption)
        {
            var form = new frmAlert(msg, caption, MsgType.Default, null, LoadIcons.Default);
            form.TopMost = true;
            return form;
        }
        public static DialogResult ShowCustomMessage(string msg, string caption, Form caller, LoadIcons icon, MsgType btnType)
        {
            DialogResult diagres = DialogResult.Cancel;
            string captiondef = (!String.IsNullOrWhiteSpace(caption) ? caption : CAPTION_DIALOG);
            Form parent = (caller != null) ? caller : DEF_PARENT;
            try
            {
                if (parent == null)
                {
                    diagres = ShowNoParent(msg, captiondef).ShowDialog();
                }
                else
                {
                    if (parent.InvokeRequired)
                    {
                        parent.BeginInvoke((Action)delegate
                        {
                            var form = new frmAlert(msg, captiondef, btnType, parent, icon);
                            diagres = form.ShowDialog(parent);
                        });
                    }
                    else
                    {
                        var form = new frmAlert(msg, captiondef, btnType, parent, icon);
                        diagres = form.ShowDialog(parent);
                    }
                }
                return diagres;
            }
            catch (Exception ex)
            {
                Logs.Err(ex);
                return ShowNoParent(msg, captiondef).ShowDialog();
            }
        }
        public static void ShowInfo(string msg, string caption = "", Form parent = null)
        {
            ShowCustomMessage(msg, caption, parent, LoadIcons.Info, MsgType.Default);
        }
        public static void ShowWarning(string msg, string caption = "", Form parent = null)
        {
            ShowCustomMessage(msg, caption, parent, LoadIcons.Warning, MsgType.Default);
        }
        public static void ShowError(string codeFrom, string msg, Form parent, Exception error, bool openLog)
        {
            Logs.Err(error);
            bool ShowAMsg = (!String.IsNullOrWhiteSpace(msg));
            if (ShowAMsg)
            {
                string message = "An error occured!";
                ShowCustomMessage($"{message}\nReport on project site\nand submit 'logs' subfolder.", "Error occured!", parent, LoadIcons.Error, 0);
                // Open file in explorer
                if (openLog)
                {
                    try { Process.Start("explorer.exe", PATH_LOG); }
                    catch { }
                }
            }
        }
        public static void ShowError(string codeFrom, Exception error)
        {
            ShowError(codeFrom, "", null, error, false);
        }
        public static void ShowError(string codeFrom, Exception error, string msg)
        {
            ShowError(codeFrom, msg, null, error, false);
        }
        public static void ShowError(string codeFrom, Exception error, string msg, Form parent)
        {
            ShowError(codeFrom, msg, parent, error, false);
        }
        public static bool ShowYesNo(string msg, Form caller = null)
        {
            try
            {
                var result = ShowCustomMessage(msg, "", caller, LoadIcons.Question, MsgType.YesNo);
                return (result == DialogResult.Yes);
            }
            catch (Exception ex)
            {
                ShowError("GlobalVars-ShowYesNo", ex, "Cannot show prompt!\nTry again.");
            }
            return false;
        }

        // Get string from InputBox
        public static string GetStringInputBox(string msg, string caption, string defVal, List<string> items, List<string> vals)
        {
            var form = new frmInputBox(msg, items, defVal, (!String.IsNullOrWhiteSpace(caption) ? caption : CAPTION_DIALOG));
            form.Values = vals;
            form.ShowDialog();
            string value = (!String.IsNullOrWhiteSpace(form.Result)) ? form.Result.Trim() : String.Empty;
            form.Dispose();
            return value;
        }
        public static string GetStringInputBox(string msg, string defaultVal, string caption)
        {
            return GetStringInputBox(msg, caption, defaultVal, null, null);
        }
        public static string GetStringInputBox(string msg, string defaultVal)
        {
            return GetStringInputBox(msg, "", defaultVal, null, null);
        }
    }
}
