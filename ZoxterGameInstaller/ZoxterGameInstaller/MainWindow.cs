using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace ZoxterGameInstaller
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        enum DownloadStage { Unrar, Game, Decompress }

        DownloadStage dlStage;

        string game_url = "http://download1592.mediafire.com/4lg7fcbjckgg/sj9aygyd5n90ynw/Plazma+Burst+-+Early+Alpha+Demo.rar";
        string decompressor_url = "http://download1338.mediafire.com/ca29attc1cog/mr5all64xhjcjru/rardecompressor.exe";
        string donation_url = "https://www.paypal.com/donate/?token=rmRf4D4qO9NiOpmbrCZXPH81yvkiGn5c-dT_r26HQbYbTumF0wAcc1mkbtnKbqz8rhgM00&country.x=AR&locale.x=AR";
        string twitter_url = "https://twitter.com/gann4life";

        /// <summary>Name of the main installation folder</summary>
        string root_folder_name = "Zoxter Games";
        /// <summary>Subfolder where game versions are going to be installed</summary>
        string game_path = "\\versions";
        /// <summary>Configuration folder for the launcher to use (Unused)</summary>
        string config_path = "\\config";
        /// <summary>Return the choosen folder path to install stuff at</summary>
        string Install_Path(){ return folderBrowserDialog.SelectedPath; }
        /// <summary>Sets download progress description</summary>
        string downloadName = "";

        /// <summary>Return main installation folder</summary>
        string GetRootPath() {
            return Install_Path() + (Install_Path().Equals(@"C:\") ? "" : "\\") +  root_folder_name;
        }
        /// <summary>Return game versions installation folder</summary>
        string GetGamePath() { return GetRootPath() + game_path; }
        /// <summary>Return launcher config installation folder (Unused)</summary>
        string GetConfigPath() { return GetRootPath() + config_path; }

        /// <summary>Get extension of file in URL</summary>
        string GetURLExtension(string url)
        {
            string[] pieces = url.Split('.');
            return "." + pieces[pieces.Length-1];
        }
        /// <summary>Get filename plus its extension from the URL</summary>
        string GetURLFilename(string url)
        {
            string[] pieces = url.Split('/');
            return pieces[pieces.Length - 1];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var installpath = Environment.GetEnvironmentVariable("installpath", EnvironmentVariableTarget.Machine);

            if(installpath == null)
            {
                if (CanRegEdit())
                {
                    ChooseNewDirectory();
                    return;
                }
                DialogResult diag = MessageBox.Show($"The application will download and install files at '{GetRootPath()}'. \nIf you want to change it, we require you to run the app in admin mode. \n\nWould you like to use admin mode?", "App permissions", MessageBoxButtons.YesNo);
                switch(diag)
                {
                    case DialogResult.Yes:
                        OpenInAdminMode();
                        break;
                    case DialogResult.No:
                        DisplayInstallPath();
                        break;
                }
            }
            else
            {
                folderBrowserDialog.SelectedPath = installpath;
                DisplayInstallPath();
                if (!Directory.Exists(GetRootPath())) CreateDirectories();
                if (IsGameInstalled())
                {
                    btn_play.Enabled = true;
                }
            }
        }

        private void lbl_twitter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(twitter_url);
        }

        private void btn_directorySwitch_Click(object sender, EventArgs e)
        {
            ChooseNewDirectory();
        }

        private void btn_dl_Click(object sender, EventArgs e)
        {
            DoGameUpdate();
        }
        private void btn_dlCancel_Click(object sender, EventArgs e)
        {
            CancelDownload();

            btn_dlCancel.Text = "Cancelled";
            btn_dlCancel.Enabled = false;

            dlStage = DownloadStage.Unrar;
        }
        private void btn_quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btn_donate_Click(object sender, EventArgs e)
        {
            Process.Start(donation_url);
        }

        WebClient client;
        /// <summary>
        /// Downloads a file from <b>url</b> and saves it into <b>path</b>
        /// </summary>
        /// <param name="url">The <b>URL</b> to download from</param>
        /// <param name="path">The <b>PATH</b> to save the file at</param>
        void DownloadFile(string url, string path)
        {
            client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadProgressFinished);

            client.DownloadFileAsync(new Uri(url), path + "\\" + GetURLFilename(url));

            btn_dl.Enabled = false;

            btn_dlCancel.Enabled = true;
            btn_dlCancel.Text = "Cancel";
        }
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            pb_dlStatus.Value = e.ProgressPercentage;
            lbl_dlProgress.Text = downloadName + " " + e.ProgressPercentage + "%";

            float totalMb = float.Parse(e.TotalBytesToReceive.ToString()) / (1024*1024);
            float currentMb = float.Parse(e.BytesReceived.ToString()) / (1024 * 1024);
            lbl_bytes.Text = string.Format("Downloading {0}mb of {1}mb", currentMb.ToString("F2"), totalMb.ToString("F2"));
        }
        void client_DownloadProgressFinished(object sender, AsyncCompletedEventArgs e)
        {
            pb_dlStatus.Value = 0;
            btn_dl.Enabled = true;
            lbl_dlProgress.Text = downloadName + " ---";

            if (e.Cancelled)
            {
                MessageBox.Show("Download cancelled.");
                return;
            }
            else
            {
                if (dlStage == DownloadStage.Unrar)
                {
                    dlStage = DownloadStage.Game;
                    DoGameUpdate();
                    return;
                }
                else if (dlStage == DownloadStage.Game)
                {
                    dlStage = DownloadStage.Decompress;
                    DoGameUpdate();
                }
                else if (dlStage == DownloadStage.Decompress)
                {
                    DecompressFiles();
                    return;
                }
            }
        }
        /// <summary>
        /// Decompresses all rar files into versions folder by executing an .exe file
        /// </summary>
        void DecompressFiles()
        {
            lbl_dlProgress.Text = "Decompressing...";

            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.UseShellExecute = false;
            processInfo.WorkingDirectory = GetGamePath();
            processInfo.FileName = GetGamePath() + "\\rardecompressor.exe";

            Process p = new Process();
            p.StartInfo = processInfo;
            p.Start();
            p.WaitForExit();

            lbl_dlProgress.Text = "Done.";
            btn_play.Enabled = true;
        }
        void CancelDownload()
        {
            client.CancelAsync();
            client = null;
        }
        /// <summary>
        /// Checks if rar files are in versions folder
        /// </summary>
        /// <returns></returns>
        bool RarFilesExist()
        {
            string[] dirs = Directory.GetFiles(GetGamePath());
            bool foundRar = false;
            foreach(string file in dirs)
            {
                if (file.Contains(".rar"))
                {
                    foundRar = true;
                }
            }
            return foundRar;
        }
        /// <summary>
        /// Returns a .exe file path
        /// </summary>
        /// <returns></returns>
        string GetExeFile()
        {
            string file = "";
            IEnumerable<string> gameVersions = Directory.EnumerateDirectories(GetGamePath());
            foreach(string version in gameVersions)
            {
                IEnumerable<string> versionFiles = Directory.EnumerateFiles(version);
                foreach(string f in versionFiles)
                {
                    if (f.Contains(".exe"))
                    {
                        file = f;
                        break;
                    }
                }
            }
            return file;
        }
        /// <summary>
        /// Checks if the game is installed
        /// </summary>
        /// <returns></returns>
        bool IsGameInstalled()
        {
            try
            {
                IEnumerable<string> versionFolders = Directory.EnumerateDirectories(GetGamePath());
                return versionFolders.Any();
            }
            catch (DirectoryNotFoundException)
            {
                return false;
            }
        }
        /// <summary>
        /// Download required files and decompresses them if required
        /// </summary>
        void DoGameUpdate()
        {
            if (RarFilesExist()) // Decompress files
            {
                DecompressFiles();
                MessageBox.Show("Now you can play the game.", "Game installed", MessageBoxButtons.OK);
                return;
            }
            if (client != null && client.IsBusy) return;
            if (dlStage == DownloadStage.Unrar)
            {
                if (File.Exists(GetGamePath() + "\\rardecompressor.exe"))
                {
                    dlStage = DownloadStage.Game;
                    DoGameUpdate();
                    return;
                }
                downloadName = "Unrar tool:";
                DownloadFile(decompressor_url, GetGamePath());
            }
            else if (dlStage == DownloadStage.Game)
            {
                dlStage = DownloadStage.Decompress;
                downloadName = "Main game files";
                DownloadFile(game_url, GetGamePath());
            }
            else if (dlStage == DownloadStage.Decompress)
            {
                DecompressFiles();
                MessageBox.Show("Download finished successfully!");
            }
        }
        /// <summary>
        /// Prompt a window for the user to choose a new install directory and sets the new path into an Environment variable
        /// </summary>
        void ChooseNewDirectory()
        {
            if (!CanRegEdit())
            {
                DialogResult diag = MessageBox.Show("Amin permissions required to change install directory. \n\nWould you like to enable it?", "Permission denied", MessageBoxButtons.YesNo);
                switch(diag)
                {
                    case DialogResult.Yes:
                        OpenInAdminMode();
                        break;
                    case DialogResult.No:
                        return;
                }
            }

            folderBrowserDialog.ShowDialog();
            DisplayInstallPath();
            CreateDirectories();

            SaveInstallPath();
        }
        /// <summary>
        /// Creates required folders for proper installations
        /// </summary>
        void CreateDirectories()
        {
            Directory.CreateDirectory(GetRootPath());
            Directory.CreateDirectory(GetGamePath());
            Directory.CreateDirectory(GetConfigPath());
        }
        /// <summary>
        /// Saves the current installation path into an environment variable
        /// </summary>
        void SaveInstallPath()
        {
            try
            {
                Environment.SetEnvironmentVariable("installpath", Install_Path(), EnvironmentVariableTarget.Machine);
            }
            catch (System.Security.SecurityException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
        /// <summary>
        /// Checks if the application is able to modify reg files
        /// </summary>
        /// <returns></returns>
        bool CanRegEdit()
        {
            try
            {
                Environment.SetEnvironmentVariable("installpath", Install_Path(), EnvironmentVariableTarget.Machine);
                return true;
            }
            catch (System.Security.SecurityException)
            {
                return false;
            }
        }
        /// <summary>
        /// Open the application with administrator permissions
        /// </summary>
        void OpenInAdminMode()
        {
            ProcessStartInfo adminProcess = new ProcessStartInfo();
            adminProcess.UseShellExecute = true;
            adminProcess.WorkingDirectory = Environment.CurrentDirectory;
            adminProcess.FileName = Application.ExecutablePath;
            adminProcess.Verb = "runas";
            Process.Start(adminProcess);
            Application.Exit();
        }
        /// <summary>
        /// Displays the current installation directory into the UI
        /// </summary>
        void DisplayInstallPath() { lbl_installPath.Text = "Install path: " + GetRootPath(); }

        private void folderBrowserDialog_HelpRequest(object sender, EventArgs e)
        {

        }
        bool clicking;
        int clickPointX;
        int clickPointY;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            clicking = true;
            clickPointX = e.Location.X;
            clickPointY = e.Location.Y;
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            clicking = false;
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicking)
            {
                Point formPos = ActiveForm.Location;

                int cursorX = e.Location.X;
                int cursorY = e.Location.Y;

                formPos.X += cursorX - (clickPointX);
                formPos.Y += cursorY - (clickPointY);
                ActiveForm.Location = formPos;
            }
        }

        private void btn_play_Click(object sender, EventArgs e)
        {
            string gameExe = GetExeFile();
            if (gameExe == "" || gameExe == null)
                MessageBox.Show("Could not find any executable. Please install the game.", "Unavailable file", MessageBoxButtons.OK);
            else
                Process.Start(gameExe);
        }

        private void btn_openDirectory_Click(object sender, EventArgs e)
        {
            Process.Start(GetRootPath());
        }
    }
}
