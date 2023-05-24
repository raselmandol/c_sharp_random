private void MainForm_KeyDown(object sender, KeyEventArgs e)
{
    //Ctrl, Shift, and C shortcut to perform a hiddenButton click
    if (e.Control && e.Shift && e.KeyCode == Keys.C)
    {
        // Programmatically click the hidden button
        hiddenButton.PerformClick();
    }
}
