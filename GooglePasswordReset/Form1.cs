using System;
using System.Windows.Forms;

namespace GooglePasswordReset
{
    public partial class Form1 : Form
    {
        private string _pswTempalate;
        private bool _doReset = false;

        public Form1()
        {
            InitializeComponent();
            webBrowser1.Navigate("https://www.google.com/accounts/b/0/EditPasswd");
        }


        private void ResetPassword()
        {
            if (numericUpDown1.Value <= 0)
            {
                _doReset = false;
                //tbNewPassword.Visible = labelNewPwd.Visible = false;
                btStart.Enabled = true;
                return;
            }
            HtmlDocument document = webBrowser1.Document;
            HtmlElement element = null;

            element = document.GetElementById("OldPasswd");
            element.SetAttribute("value", tbOldPassword.Text);

            element = document.GetElementById("Passwd");
            element.SetAttribute("value", tbNewPassword.Text);

            element = document.GetElementById("PasswdAgain");
            element.SetAttribute("value", tbNewPassword.Text);

            element = document.GetElementById("save"); 
            element.InvokeMember("click");

            tbOldPassword.Text = tbNewPassword.Text;
            numericUpDown1.Value--;
            tbNewPassword.Text = _pswTempalate + numericUpDown1.Value.ToString();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (!_doReset)
                return;
            if (e.Url.AbsoluteUri == "https://accounts.google.com/b/0/EditPasswd")
            {
                ResetPassword();
            }
            //fallback to edit password page if something went wrong
            if (_doReset && e.Url.AbsoluteUri != "https://accounts.google.com/b/0/EditPasswd")
            {
                webBrowser1.Navigate("https://www.google.com/accounts/b/0/EditPasswd");
            }
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            //tbNewPassword.Visible = labelNewPwd.Visible = true;
            _pswTempalate = tbOldPassword.Text + "Reset";
            _doReset = true;
            btStart.Enabled = false;
            tbNewPassword.Text = _pswTempalate + numericUpDown1.Value.ToString();
            webBrowser1.Navigate("https://www.google.com/accounts/b/0/EditPasswd");
        }
    }
}
