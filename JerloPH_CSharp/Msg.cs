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

        public enum LoadIcons
        {
            Default,
            Info,
            Check,
            Warning,
            Error,
            Question
        }

        public static void Initialize(string _logPath, string _defaultCaption)
        {
            PATH_LOG = _logPath;
            CAPTION_DIALOG = _defaultCaption;
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

        public static Form Load(string message, string caption, long maxProgress)
        {
            var form = new frmLoading(message, (!String.IsNullOrWhiteSpace(caption) ? caption : CAPTION_DIALOG), maxProgress);
            return form;
        }
        public static Form Load(string message, string caption)
        {
            var form = new frmLoading(message, (!String.IsNullOrWhiteSpace(caption) ? caption : CAPTION_DIALOG), 0);
            return form;
        }

        public static Form ShowNoParent(string msg, string caption)
        {
            var form = new frmAlert(msg, (!String.IsNullOrWhiteSpace(caption) ? caption : CAPTION_DIALOG), 0, null, LoadIcons.Default);
            form.TopMost = true;
            return form;
        }
        public static void ShowCustomMessage(string msg, string caption, Form parent, LoadIcons icon)
        {
            try
            {
                if (parent == null)
                {
                    ShowNoParent(msg, caption);
                    return;
                }
                if (parent.InvokeRequired)
                {
                    parent.BeginInvoke((Action)delegate
                    {
                        var form = new frmAlert(msg, caption, 0, parent, icon);
                        form.ShowDialog(parent);
                    });
                }
                else
                {
                    var form = new frmAlert(msg, caption, 0, parent, icon);
                    form.ShowDialog(parent);
                }
            }
            catch (Exception ex)
            {
                Logs.Err(ex);
                ShowNoParent(msg, caption);
            }
        }
        public static void ShowInfo(string msg, string caption = "", Form parent = null)
        {
            ShowCustomMessage(msg, caption, parent, LoadIcons.Info);
        }
        public static void ShowWarning(string msg, string caption = "", Form parent = null)
        {
            ShowCustomMessage(msg, caption, parent, LoadIcons.Warning);
        }
        public static void ShowError(string codeFrom, string msg, Form parent, Exception error, bool openLog)
        {
            Logs.Err(error);
            bool ShowAMsg = (!String.IsNullOrWhiteSpace(msg));
            if (ShowAMsg)
            {
                string message = "An error occured!";
                ShowCustomMessage($"{message}\nReport on project site\nand submit 'logs' subfolder.", "Error occured!", parent, LoadIcons.Error);
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
                return (new frmAlert(msg, CAPTION_DIALOG, 1, caller, LoadIcons.Question).ShowDialog(caller) == DialogResult.Yes);
            }
            catch (Exception ex)
            {
                ShowError("GlobalVars-ShowYesNo", ex, "Cannot show prompt!\nTry again.");
            }
            return false;
        }

        // Get string from InputBox
        public static string GetStringInputBox(string caption, string defaultVal)
        {
            var form = new frmInputBox(caption, null, defaultVal);
            form.ShowDialog();
            string value = (!String.IsNullOrWhiteSpace(form.Result)) ? form.Result.Trim() : String.Empty;
            form.Dispose();
            return value;
        }
        public static string GetStringInputBox(List<string> items, List<string> vals, string caption)
        {
            var form = new frmInputBox(caption, items, "");
            form.Values = vals;
            form.ShowDialog();
            string value = (!String.IsNullOrWhiteSpace(form.Result)) ? form.Result.Trim() : String.Empty;
            form.Dispose();
            return value;
        }
    }
}
