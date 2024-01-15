using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebKit;
using WebKit.Interop;

namespace WebBrowser_SecGoogle_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateNewTab();
        }
        int tabCount = 1;

        private void CreateNewTab()
        {
            TabC.TabPages.Add("New Tab");
            //create web browser object
            WebKitBrowser web = new WebKitBrowser();
            web.Dock = DockStyle.Fill;
            //web.ProgressChanged += Web_ProgressChanged;
            web.Navigated += Web_Navigated;
            web.DocumentTitleChanged += Web_DocumentTitleChanged;
            web.DocumentCompleted += Web_DocumentCompleted;
            //web.ScriptErrorsSuppressed = true;

            web.Navigate("https://www.google.com");

            if (tabCount == 1)
            {
                TabC.SelectedTab.Controls.Add(web);
                tabCount++;
            }
            else if (tabCount >1 )
            {
                TabC.SelectTab(tabCount - 1);
                TabC.SelectedTab.Controls.Add(web);
                tabCount++;
            }
        }

        private void Web_DocumentTitleChanged(object sender, EventArgs e)
        {
            string txt = (sender as WebKitBrowser).DocumentTitle;
            if (txt.Length > 20)
                TabC.SelectedTab.Text = txt.Substring(0, 16) + "...";
            else
                TabC.SelectedTab.Text = txt;
        }

        private void Web_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            txtURL.Text = e.Url.AbsoluteUri;
        }

        private void Web_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            progBar.Maximum = (int)e.MaximumProgress;
            if (e.CurrentProgress == -1) return;
            if (e.CurrentProgress < e.MaximumProgress)
            {
                progBar.Value = (int)e.CurrentProgress;
            }
        }

        private void Web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string txt = (sender as WebKitBrowser).DocumentTitle;
            if (txt.Length > 20)
                TabC.SelectedTab.Text = txt.Substring(0, 16) + "...";
            else
                TabC.SelectedTab.Text = txt;
            txtURL.Text = e.Url.ToString();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewTab();
        }

        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int indx = TabC.SelectedIndex;
            if (TabC.SelectedIndex != -1)
            {
                TabC.TabPages.RemoveAt(TabC.SelectedIndex);
                tabCount--;
                if (indx != 0)
                    TabC.SelectTab(indx - 1);
                if (tabCount == 1) CreateNewTab();
                
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            (TabC.SelectedTab.Controls[0] as WebKitBrowser).GoBack();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            (TabC.SelectedTab.Controls[0] as WebKitBrowser).GoForward();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            (TabC.SelectedTab.Controls[0] as WebKitBrowser).Refresh();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            (TabC.SelectedTab.Controls[0] as WebKitBrowser).Stop();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            (TabC.SelectedTab.Controls[0] as WebKitBrowser).Navigate(txtURL.Text);
        }

        private void closeAllTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TabPage item in TabC.TabPages)
            {
                TabC.TabPages.Remove(item);
            }
            tabCount = 1;
            newToolStripMenuItem.PerformClick();
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
