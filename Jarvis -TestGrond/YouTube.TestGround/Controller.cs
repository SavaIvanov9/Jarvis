using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace YouTube.TestGround
{
    class Controller
    {
        public void playfile(int playlistindex)
        {
            if (listBox1.Items.Count <= 0)
            { return; }
            if (playlistindex < 0)
            {
                return;
            }
            WindowsMediaPlayer.settings.autoStart = true;
            WindowsMediaPlayer.URL = FilePath[playlistindex];
            WindowsMediaPlayer.Ctlcontrols.next();
            WindowsMediaPlayer.Ctlcontrols.play();
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            Startindex = 0;
            playnext = false;
            OpenFileDialog opnFileDlg = new OpenFileDialog();
            opnFileDlg.Multiselect = true;
            opnFileDlg.Filter = "(mp3,wav,mp4,mov,wmv,mpg,avi,3gp,flv)|*.mp3;*.wav;*.mp4;*.3gp;*.avi;*.mov;*.flv;*.wmv;*.mpg|all files|*.*";
            if (opnFileDlg.ShowDialog() == DialogResult.OK)
            {
                FileName = opnFileDlg.SafeFileNames;
                FilePath = opnFileDlg.FileNames;
                for (int i = 0; i <= FileName.Length - 1; i++)
                {
                    listBox1.Items.Add(FileName[i]);
                }
                Startindex = 0;
                playfile(0);
            }
        }
    }
}
