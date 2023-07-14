webView.NavigationFailed += WebView_NavigationFailed;

//...//...//

private void WebView_NavigationFailed(object sender, WebViewControlNavigationFailedEventArgs e)
{
    if (e.WebErrorStatus == WebViewControlNavigationErrorStatus.ConnectionFailure)
    {
        //Handle the connection failure, such as displaying an error message or showing a locally stored offline content
    }
}
