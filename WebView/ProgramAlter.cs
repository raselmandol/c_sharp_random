using System;
using System.Windows.Forms;
using Microsoft.Toolkit.Forms.UI.Controls;

namespace WebViewApp
{
    public partial class MainForm : Form
    {
        private WebView webView;

        public MainForm()
        {
            InitializeComponent();

            webView = new WebView();
            webView.Dock = DockStyle.Fill;
            this.Controls.Add(webView);

            string url = "https://raselmandol.com"; //Replace with your desired URL//
            webView.Source = new Uri(url);
            webView.NavigationFailed += WebView_NavigationFailed;
        }

        private void WebView_NavigationFailed(object sender, WebViewControlNavigationFailedEventArgs e)
        {
            if (e.WebErrorStatus == WebViewControlNavigationErrorStatus.ConnectionFailure)
            {
                //Handle the connection failure, such as displaying an error message or showing a locally stored offline content//
                MessageBox.Show("No internet connection. Please check your network settings.");

                //Show another form and close the current one//
                OtherForm otherForm = new OtherForm();
                otherForm.Show();
                this.Close();
            }
        }
    }
}
