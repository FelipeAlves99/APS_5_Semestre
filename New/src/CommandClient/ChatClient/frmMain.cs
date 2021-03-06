using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using CommandClient;
using System.Runtime.InteropServices;

namespace ChatClient
{
    public partial class frmMain : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private CMDClient client;
        private List<frmPrivate> privateWindowsList;
        public frmMain()
        {
            InitializeComponent();
            this.privateWindowsList = new List<frmPrivate>();
            this.client = new CMDClient(IPAddress.Parse("127.0.0.1"), 10220, "None");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
                this.SendMessage();
            if (this.txtMessages.Focused && !ShareUtils.IsValidKeyForReadOnlyFields(keyData))
                return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private bool IsPrivateWindowOpened(string remoteName)
        {
            foreach (frmPrivate privateWindow in this.privateWindowsList)
                if (privateWindow.RemoteName == remoteName)
                    return true;
            return false;
        }
        private frmPrivate FindPrivateWindow(string remoteName)
        {
            foreach (frmPrivate privateWindow in this.privateWindowsList)
                if (privateWindow.RemoteName == remoteName)
                    return privateWindow;
            return null;
        }
        void client_CommandReceived(object sender, CommandEventArgs e)
        {
            switch (e.Command.CommandType)
            {
                case (CommandType.Message):
                    if (e.Command.Target.Equals(IPAddress.Broadcast))
                        this.txtMessages.Text += e.Command.SenderName + ": " + e.Command.MetaData + Environment.NewLine;
                    else if (!this.IsPrivateWindowOpened(e.Command.SenderName))
                    {
                        this.OpenPrivateWindow(e.Command.SenderIP, e.Command.SenderName, e.Command.MetaData);
                        ShareUtils.PlaySound(ShareUtils.SoundType.NewMessageWithPow);
                    }
                    break;

                case (CommandType.FreeCommand):
                    string[] newInfo = e.Command.MetaData.Split(new char[] { ':' });
                    this.AddToList(newInfo[0], newInfo[1]);
                    ShareUtils.PlaySound(ShareUtils.SoundType.NewClientEntered);
                    break;
                case (CommandType.SendClientList):
                    string[] clientInfo = e.Command.MetaData.Split(new char[] { ':' });
                    this.AddToList(clientInfo[0], clientInfo[1]);
                    break;
                case (CommandType.ClientLogOffInform):
                    this.RemoveFromList(e.Command.SenderName);
                    break;
            }
        }

        private void RemoveFromList(string name)
        {
            ListViewItem item = this.lstViwUsers.FindItemWithText(name);
            if (item.Text != this.client.IP.ToString())
            {
                this.lstViwUsers.Items.Remove(item);
                ShareUtils.PlaySound(ShareUtils.SoundType.ClientExit);
            }

            frmPrivate target = this.FindPrivateWindow(name);
            if (target != null)
                target.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
            => this.SendMessage();

        private void SendMessage()
        {
            if (this.client.Connected && this.txtNewMessage.Text.Trim() != "")
            {
                this.client.SendCommand(new Command(CommandType.Message, IPAddress.Broadcast, this.txtNewMessage.Text));
                this.txtMessages.Text += this.client.NetworkName + ": " + this.txtNewMessage.Text.Trim() + Environment.NewLine;
                this.txtNewMessage.Text = "";
                this.txtNewMessage.Focus();
            }
        }

        private void AddToList(string ip, string name)
        {
            ListViewItem newItem = this.lstViwUsers.Items.Add(ip);
            newItem.ImageKey = "Smiely.png";
            newItem.SubItems.Add(name);
        }

        private void OpenPrivateWindow(IPAddress remoteClientIP, string clientName)
        {
            if (this.client.Connected)
            {
                if (!this.IsPrivateWindowOpened(clientName))
                {
                    frmPrivate privateWindow = new frmPrivate(this.client, remoteClientIP, clientName);
                    this.privateWindowsList.Add(privateWindow);
                    privateWindow.FormClosed += new FormClosedEventHandler(privateWindow_FormClosed);
                    privateWindow.StartPosition = FormStartPosition.CenterScreen;
                    privateWindow.Show(this);
                }
            }
        }

        private void OpenPrivateWindow(IPAddress remoteClientIP, string clientName, string initialMessage)
        {
            if (this.client.Connected)
            {
                frmPrivate privateWindow = new frmPrivate(this.client, remoteClientIP, clientName, initialMessage);
                this.privateWindowsList.Add(privateWindow);
                privateWindow.FormClosed += new FormClosedEventHandler(privateWindow_FormClosed);
                privateWindow.Show(this);
            }
        }

        void privateWindow_FormClosed(object sender, FormClosedEventArgs e)
            => this.privateWindowsList.Remove((frmPrivate)sender);

        private void btnPrivate_Click(object sender, EventArgs e)
            => this.StartPrivateChat();

        private void StartPrivateChat()
        {
            if (this.lstViwUsers.SelectedItems.Count != 0)
                this.OpenPrivateWindow(IPAddress.Parse(this.lstViwUsers.SelectedItems[0].Text), this.lstViwUsers.SelectedItems[0].SubItems[1].Text);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Proshot.LanguageManager.LanguageActions.ChangeLanguageToEnglish();
            this.client.Disconnect();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (this.btnLogin.Text == "Login")
            {
                frmLogin dlg = new frmLogin();
                dlg.ShowDialog();
                this.client = dlg.Client;

                if (this.client.Connected)
                {
                    this.client.CommandReceived += new CommandReceivedEventHandler(client_CommandReceived);
                    this.client.SendCommand(new Command(CommandType.FreeCommand, IPAddress.Broadcast, this.client.IP + ":" + this.client.NetworkName));
                    this.client.SendCommand(new Command(CommandType.SendClientList, this.client.ServerIP));
                    this.AddToList(this.client.IP.ToString(), this.client.NetworkName);
                    this.btnLogin.Text = "Log Off";
                }
            }
            else
            {
                this.btnLogin.Text = "Login";
                this.privateWindowsList.Clear();
                this.client.Disconnect();
                this.lstViwUsers.Items.Clear();
                this.txtNewMessage.Clear();
                this.txtNewMessage.Focus();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.FromArgb(140, 223, 132);
            btnPrivate.BackColor = Color.FromArgb(140, 223, 132);
            lstViwUsers.BackColor = Color.FromArgb(204, 237, 208);
        }

        #region Close button

        private void btnClose_Click(object sender, EventArgs e)
        {
            client.Disconnect();
            Application.Exit();
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
            => btnClose.BackColor = Color.FromArgb(70, 113, 107);

        private void btnClose_MouseLeave(object sender, EventArgs e)
            => btnClose.BackColor = Color.FromArgb(64, 102, 97);

        #endregion

        #region Minimize button

        private void btnMinimize_Click(object sender, EventArgs e)
            => this.WindowState = FormWindowState.Minimized;

        private void btnMinimize_MouseEnter(object sender, EventArgs e)
            => btnMinimize.BackColor = Color.FromArgb(70, 113, 107);

        private void btnMinimize_MouseLeave(object sender, EventArgs e)
            => btnMinimize.BackColor = Color.FromArgb(64, 102, 97);

        #endregion

        #region Control Panel

        private void pnlControlBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }


        #endregion
    }
}