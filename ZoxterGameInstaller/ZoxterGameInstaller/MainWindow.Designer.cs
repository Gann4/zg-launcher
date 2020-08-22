namespace ZoxterGameInstaller
{
    partial class MainWindow
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.btn_dl = new System.Windows.Forms.Button();
            this.btn_donate = new System.Windows.Forms.Button();
            this.btn_dlCancel = new System.Windows.Forms.Button();
            this.btn_quit = new System.Windows.Forms.Button();
            this.pb_dlStatus = new System.Windows.Forms.ProgressBar();
            this.lbl_installPath = new System.Windows.Forms.Label();
            this.lbl_dlProgress = new System.Windows.Forms.Label();
            this.btn_directorySwitch = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.lbl_twitter = new System.Windows.Forms.LinkLabel();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btn_play = new System.Windows.Forms.Button();
            this.lbl_bytes = new System.Windows.Forms.Label();
            this.btn_openDirectory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_dl
            // 
            this.btn_dl.Location = new System.Drawing.Point(12, 12);
            this.btn_dl.Name = "btn_dl";
            this.btn_dl.Size = new System.Drawing.Size(221, 103);
            this.btn_dl.TabIndex = 0;
            this.btn_dl.Text = "Download";
            this.btn_dl.UseVisualStyleBackColor = true;
            this.btn_dl.Click += new System.EventHandler(this.btn_dl_Click);
            // 
            // btn_donate
            // 
            this.btn_donate.Location = new System.Drawing.Point(591, 485);
            this.btn_donate.Name = "btn_donate";
            this.btn_donate.Size = new System.Drawing.Size(221, 103);
            this.btn_donate.TabIndex = 1;
            this.btn_donate.Text = "Donate";
            this.btn_donate.UseVisualStyleBackColor = true;
            this.btn_donate.Click += new System.EventHandler(this.btn_donate_Click);
            // 
            // btn_dlCancel
            // 
            this.btn_dlCancel.Enabled = false;
            this.btn_dlCancel.Location = new System.Drawing.Point(469, 28);
            this.btn_dlCancel.Name = "btn_dlCancel";
            this.btn_dlCancel.Size = new System.Drawing.Size(96, 23);
            this.btn_dlCancel.TabIndex = 2;
            this.btn_dlCancel.Text = "Cancel";
            this.btn_dlCancel.UseVisualStyleBackColor = true;
            this.btn_dlCancel.Click += new System.EventHandler(this.btn_dlCancel_Click);
            // 
            // btn_quit
            // 
            this.btn_quit.Location = new System.Drawing.Point(12, 485);
            this.btn_quit.Name = "btn_quit";
            this.btn_quit.Size = new System.Drawing.Size(221, 103);
            this.btn_quit.TabIndex = 3;
            this.btn_quit.Text = "Quit";
            this.btn_quit.UseVisualStyleBackColor = true;
            this.btn_quit.Click += new System.EventHandler(this.btn_quit_Click);
            // 
            // pb_dlStatus
            // 
            this.pb_dlStatus.Location = new System.Drawing.Point(242, 28);
            this.pb_dlStatus.Name = "pb_dlStatus";
            this.pb_dlStatus.Size = new System.Drawing.Size(221, 23);
            this.pb_dlStatus.TabIndex = 4;
            // 
            // lbl_installPath
            // 
            this.lbl_installPath.AutoSize = true;
            this.lbl_installPath.Location = new System.Drawing.Point(239, 102);
            this.lbl_installPath.Name = "lbl_installPath";
            this.lbl_installPath.Size = new System.Drawing.Size(190, 13);
            this.lbl_installPath.TabIndex = 5;
            this.lbl_installPath.Text = "Installation path: C:\\idk\\pbgame\\game";
            // 
            // lbl_dlProgress
            // 
            this.lbl_dlProgress.AutoSize = true;
            this.lbl_dlProgress.Location = new System.Drawing.Point(239, 12);
            this.lbl_dlProgress.Name = "lbl_dlProgress";
            this.lbl_dlProgress.Size = new System.Drawing.Size(21, 13);
            this.lbl_dlProgress.TabIndex = 6;
            this.lbl_dlProgress.Text = "0%";
            // 
            // btn_directorySwitch
            // 
            this.btn_directorySwitch.Location = new System.Drawing.Point(242, 57);
            this.btn_directorySwitch.Name = "btn_directorySwitch";
            this.btn_directorySwitch.Size = new System.Drawing.Size(162, 23);
            this.btn_directorySwitch.TabIndex = 7;
            this.btn_directorySwitch.Text = "Choose directory...";
            this.btn_directorySwitch.UseVisualStyleBackColor = true;
            this.btn_directorySwitch.Click += new System.EventHandler(this.btn_directorySwitch_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(12, 121);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(800, 358);
            this.webBrowser.TabIndex = 8;
            // 
            // lbl_twitter
            // 
            this.lbl_twitter.AutoSize = true;
            this.lbl_twitter.Location = new System.Drawing.Point(745, 12);
            this.lbl_twitter.Name = "lbl_twitter";
            this.lbl_twitter.Size = new System.Drawing.Size(67, 13);
            this.lbl_twitter.TabIndex = 9;
            this.lbl_twitter.TabStop = true;
            this.lbl_twitter.Text = "@Gann4Life";
            this.lbl_twitter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbl_twitter_LinkClicked);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialog.SelectedPath = "C:\\";
            this.folderBrowserDialog.HelpRequest += new System.EventHandler(this.folderBrowserDialog_HelpRequest);
            // 
            // btn_play
            // 
            this.btn_play.Enabled = false;
            this.btn_play.Location = new System.Drawing.Point(302, 485);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(221, 103);
            this.btn_play.TabIndex = 10;
            this.btn_play.Text = "Play";
            this.btn_play.UseVisualStyleBackColor = true;
            this.btn_play.Click += new System.EventHandler(this.btn_play_Click);
            // 
            // lbl_bytes
            // 
            this.lbl_bytes.AutoSize = true;
            this.lbl_bytes.Location = new System.Drawing.Point(250, 33);
            this.lbl_bytes.Name = "lbl_bytes";
            this.lbl_bytes.Size = new System.Drawing.Size(40, 13);
            this.lbl_bytes.TabIndex = 11;
            this.lbl_bytes.Text = "{bytes}";
            // 
            // btn_openDirectory
            // 
            this.btn_openDirectory.Location = new System.Drawing.Point(410, 57);
            this.btn_openDirectory.Name = "btn_openDirectory";
            this.btn_openDirectory.Size = new System.Drawing.Size(162, 23);
            this.btn_openDirectory.TabIndex = 12;
            this.btn_openDirectory.Text = "Open directory...";
            this.btn_openDirectory.UseVisualStyleBackColor = true;
            this.btn_openDirectory.Click += new System.EventHandler(this.btn_openDirectory_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(824, 600);
            this.ControlBox = false;
            this.Controls.Add(this.btn_openDirectory);
            this.Controls.Add(this.lbl_bytes);
            this.Controls.Add(this.btn_play);
            this.Controls.Add(this.lbl_twitter);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.btn_directorySwitch);
            this.Controls.Add(this.lbl_dlProgress);
            this.Controls.Add(this.lbl_installPath);
            this.Controls.Add(this.pb_dlStatus);
            this.Controls.Add(this.btn_quit);
            this.Controls.Add(this.btn_dlCancel);
            this.Controls.Add(this.btn_donate);
            this.Controls.Add(this.btn_dl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_dl;
        private System.Windows.Forms.Button btn_donate;
        private System.Windows.Forms.Button btn_dlCancel;
        private System.Windows.Forms.Button btn_quit;
        private System.Windows.Forms.ProgressBar pb_dlStatus;
        private System.Windows.Forms.Label lbl_installPath;
        private System.Windows.Forms.Label lbl_dlProgress;
        private System.Windows.Forms.Button btn_directorySwitch;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.LinkLabel lbl_twitter;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btn_play;
        private System.Windows.Forms.Label lbl_bytes;
        private System.Windows.Forms.Button btn_openDirectory;
    }
}

