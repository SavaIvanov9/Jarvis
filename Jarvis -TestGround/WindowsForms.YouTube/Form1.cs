using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShockwaveFlashObjects;
using WMPLib;

namespace WindowsForms.YouTube
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //private void frmMain_Load(object sender, EventArgs e)
        //{
        //    LoadPlayerControl(0);
        //}
        //private void LoadPlayerControl(int playerType)
        //{
        //    pnlMain.Controls.Clear();

        //    if (playerType == 0)
        //    {
        //        SHANUAudioVedioPlayListPlayer.PlayerControls.AudioVedioCntl objAudioVideo = new PlayerControls.AudioVedioCntl();
        //        pnlMain.Controls.Add(objAudioVideo);
        //        objAudioVideo.Dock = DockStyle.Fill;
        //    }
        //    else
        //    {
        //        PlayerControls.YouTubeCntl objYouTube = new PlayerControls.YouTubeCntl();
        //        pnlMain.Controls.Add(objYouTube);
        //        objYouTube.Dock = DockStyle.Fill;
        //    }
        //}

        //private void btnYoutube_Click(object sender, EventArgs e)
        //{
        //    ShockwaveFlash.Movie = txtUtube.Text.Trim();
        //}

        //private void WindowsMediaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        //{
        //    int statuschk = e.newState;  // here the Status return the windows Media Player status where the 8 is the Song or Vedio is completed the playing . 

        //    // Now here i check if the song is completed then i Increment to play the next song 

        //    if (statuschk == 8)
        //    {
        //        statuschk = e.newState;

        //        if (Startindex == listBox1.Items.Count - 1)
        //        {
        //            Startindex = 0;
        //        }
        //        else if (Startindex >= 0 && Startindex < listBox1.Items.Count - 1)
        //        {
        //            Startindex = Startindex + 1;
        //        }
        //        playnext = true;
        //    }
        //}

        //public void playfile(int playlistindex)
        //{
        //    if (listBox1.Items.Count <= 0)
        //    { return; }
        //    if (playlistindex < 0)
        //    {
        //        return;
        //    }
        //    WindowsMediaPlayer.settings.autoStart = true;
        //    WindowsMediaPlayer.URL = FilePath[playlistindex];
        //    WindowsMediaPlayer.Ctlcontrols.next();
        //    WindowsMediaPlayer.Ctlcontrols.play();
        //}

        ////Load Audio or Vedio files  
        //private void btnLoadFile_Click(object sender, EventArgs e)
        //{
        //    Startindex = 0;
        //    playnext = false;
        //    OpenFileDialog opnFileDlg = new OpenFileDialog();
        //    opnFileDlg.Multiselect = true;
        //    opnFileDlg.Filter = "(mp3,wav,mp4,mov,wmv,mpg,avi,3gp,flv)|*.mp3;*.wav;*.mp4;*.3gp;*.avi;*.mov;*.flv;*.wmv;*.mpg|all files|*.*";
        //    if (opnFileDlg.ShowDialog() == DialogResult.OK)
        //    {
        //        FileName = opnFileDlg.SafeFileNames;
        //        FilePath = opnFileDlg.FileNames;
        //        for (int i = 0; i <= FileName.Length - 1; i++)
        //        {
        //            listBox1.Items.Add(FileName[i]);
        //        }
        //        Startindex = 0;
        //        playfile(0);
        //    }
        //}
    }
}
