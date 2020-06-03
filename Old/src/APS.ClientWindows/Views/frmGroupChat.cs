using APS.ClientCommand;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APS.ClientWindows.Views
{
    public partial class frmGroupChat : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private CMDClient client;
        private List<frmPrivateChat> privateWindowsList;

        public frmGroupChat()
        {
            InitializeComponent();
            privateWindowsList = new List<frmPrivateChat>();
            client = new CMDClient(IPAddress.Parse("127.0.0.1"), 10220, "None");
        }

        private bool IsPrivateWindowOpened(string remoteName)
        {
            foreach (frmPrivateChat privateWindow in privateWindowsList)
                if (privateWindow.RemoteName == remoteName)
                    return true;
            return false;
        }

        private frmPrivateChat FindPrivateWindow(string remoteName)
        {
            foreach (frmPrivateChat privateWindow in privateWindowsList)
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
                        txtMessages.Text += e.Command.SenderName + ": " + e.Command.MetaData + Environment.NewLine;
                    else if (!IsPrivateWindowOpened(e.Command.SenderName))
                        OpenPrivateWindow(e.Command.SenderIP, e.Command.SenderName, e.Command.MetaData);
                    
                    break;

                case (CommandType.FreeCommand):
                    string[] newInfo = e.Command.MetaData.Split(new char[] { ':' });
                    AddToList(newInfo[0], newInfo[1]);
                    break;
                case (CommandType.SendClientList):
                    string[] clientInfo = e.Command.MetaData.Split(new char[] { ':' });
                    AddToList(clientInfo[0], clientInfo[1]);
                    break;
                case (CommandType.ClientLogOffInform):
                    RemoveFromList(e.Command.SenderName);
                    break;
            }
        }

        private void RemoveFromList(string name)
        {
            ListViewItem item = lstViwUsers.FindItemWithText(name);
            if (item.Text != client.IP.ToString())
                lstViwUsers.Items.Remove(item);

            frmPrivateChat target = FindPrivateWindow(name);
            if (target != null)
                target.Close();
        }

        private void frmGroupChat_Load(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.FromArgb(140, 223, 132);
            btnPrivate.BackColor = Color.FromArgb(140, 223, 132);
            lstViwUsers.BackColor = Color.FromArgb(204, 237, 208);
        }

        private void btnSend_Click(object sender, EventArgs e)
            => SendMessage();

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
            ListViewItem newItem = lstViwUsers.Items.Add(ip);
            newItem.ImageKey = "Smiely.png";
            newItem.SubItems.Add(name);
        }

        private void OpenPrivateWindow(IPAddress remoteClientIP, string clientName)
        {
            if (this.client.Connected)
            {
                if (!this.IsPrivateWindowOpened(clientName))
                {
                    frmPrivateChat privateWindow = new frmPrivateChat(this.client, remoteClientIP, clientName);
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
                frmPrivateChat privateWindow = new frmPrivateChat(this.client, remoteClientIP, clientName, initialMessage);
                this.privateWindowsList.Add(privateWindow);
                privateWindow.FormClosed += new FormClosedEventHandler(privateWindow_FormClosed);
                privateWindow.Show(this);
            }
        }

        void privateWindow_FormClosed(object sender, FormClosedEventArgs e)
            => privateWindowsList.Remove((frmPrivateChat)sender);

        private void btnPrivate_Click(object sender, EventArgs e)
            => StartPrivateChat();

        private void StartPrivateChat()
        {
            if (lstViwUsers.SelectedItems.Count != 0)
                OpenPrivateWindow(IPAddress.Parse(this.lstViwUsers.SelectedItems[0].Text), this.lstViwUsers.SelectedItems[0].SubItems[1].Text);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
            => client.Disconnect();

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

        #region Login button

        private void btnLogoff_Click(object sender, EventArgs e)
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

        #endregion
    }
}

