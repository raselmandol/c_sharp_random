using System;
using System.Windows.Forms;
using Microsoft.Toolkit.Forms.UI.Controls;

namespace WebViewApp
{
    public partial class MainForm : Form
    {
        private WebView webView;
        private Panel noInternetPanel;

        public MainForm()
        {
            InitializeComponent();

            //Create WebView control
            webView = new WebView();
            webView.Dock = DockStyle.Fill;
            this.Controls.Add(webView);

            //Create panel for no internet connection
            noInternetPanel = new Panel();
            noInternetPanel.Dock = DockStyle.Fill;
            noInternetPanel.BackColor = SystemColors.Control;
            Label lblNoInternet = new Label();
            lblNoInternet.Text = "No internet connection. Please check your network settings.";
            lblNoInternet.AutoSize = true;
            lblNoInternet.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            lblNoInternet.Location = new System.Drawing.Point(10, 10);
            noInternetPanel.Controls.Add(lblNoInternet);
            noInternetPanel.Visible = false;
            this.Controls.Add(noInternetPanel);

            //Load URL in WebView control
            string url = "https://raselmandol.com"; //Replace with your desired URL
            webView.Source = new Uri(url);
            webView.NavigationFailed += WebView_NavigationFailed;
        }

        private void WebView_NavigationFailed(object sender, WebViewControlNavigationFailedEventArgs e)
        {
            if (e.WebErrorStatus == WebViewControlNavigationErrorStatus.ConnectionFailure)
            {
                //Handle the connection failure, such as displaying an error message or showing a locally stored offline content
                noInternetPanel.Visible = true;
            }
        }
    }
}
