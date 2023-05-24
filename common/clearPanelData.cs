//default view
private string defaultData = "Device and Username, Power Details";

private void MainForm_Load(object sender, EventArgs e)
{
    ShowDefaultData();
}

private void ShowDefaultData()
{
    MainView.Controls.Clear();
    Label label = new Label();
    label.Text = defaultData;
    MainView.Controls.Add(label);
}

private void HomeButton_Click(object sender, EventArgs e)
{
    ShowDefaultData();
}

private void DeviceInfoButton_Click(object sender, EventArgs e)
{
    MainView.Controls.Clear();
    Label label = new Label();
    label.Text = "Device Information";
    MainView.Controls.Add(label);
}

private void NetworkDetailsButton_Click(object sender, EventArgs e)
{
    //clear the mainView Panel data
    MainView.Controls.Clear();
    Label label = new Label();
    label.Text = "Network Details";
    MainView.Controls.Add(label);
}
