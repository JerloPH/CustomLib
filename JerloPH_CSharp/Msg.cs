using JerloPH_CSharp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JerloPH_CSharp
{
    public static class Msg
    {
        public enum LoadIcons
        {
            Default,
            Check,
            Warning,
            Error,
            Question
        }

        public static Form Load(string message, string caption, long maxProgress)
        {
            var form = new frmLoading(message, caption, maxProgress);
            return form;
        }
        public static Form Load(string message, string caption)
        {
            var form = new frmLoading(message, caption, 0);
            return form;
        }
    }
}
