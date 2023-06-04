using WMPLib;
private WindowsMediaPlayer player;

public MainForm()
{
    InitializeComponent();

    player = new WindowsMediaPlayer();
    player.URL = "path_to_your_video_file"; //replace with the actual path to your video file
    player.settings.setMode("loop", true);
    player.settings.autoStart = true;
}
