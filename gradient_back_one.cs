private void Form1_Load(object sender, EventArgs e)
{
    //creating a linear gradient brush
    LinearGradientBrush brush = new LinearGradientBrush(
        new Point(0, 0), new Point(0, this.Height),
        Color.Red, Color.Yellow);

    //setting the background of the form to the gradient brush
    this.BackgroundImage = new Bitmap(this.Width, this.Height);
    Graphics.FromImage(this.BackgroundImage).FillRectangle(brush, 0, 0, this.Width, this.Height);
}
