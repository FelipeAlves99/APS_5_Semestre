using APS.ClientCommand;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APS.ClientWindows.Views
{
    public partial class frmGroupChat : Form
    {
        private List<frmPrivateChat> privateChatList;
        private CMDClient _client;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public frmGroupChat()
        {
            InitializeComponent();
            privateChatList = new List<frmPrivateChat>();
        }

        private bool IsPrivateWindowOpened(string remoteName)
        {
            foreach (frmPrivateChat privateWindow in privateChatList)
                if (privateWindow.RemoteName == remoteName)
                    return true;
            return false;
        }

        private frmPrivateChat FindPrivateWindow(string remoteName)
        {
            foreach (frmPrivateChat privateWindow in privateChatList)
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
                        rtbMessages.Text += e.Command.SenderName + ": " + e.Command.MetaData + Environment.NewLine;
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
            ListViewItem item = lvUsersLoggedIn.FindItemWithText(name);
            if (item.Text != _client.IP.ToString())
                lvUsersLoggedIn.Items.Remove(item);


            frmPrivateChat target = FindPrivateWindow(name);
            if (target != null)
                target.Close();
        }

        private void frmGroupChat_Load(object sender, EventArgs e)
        {
            //PrivateFontCollection font = new PrivateFontCollection();
            //font.AddFontFile("../../Resources/ubuntumono-r.ttf");

            //btnClose.Font = new Font(font.Families[0], btnClose.Font.Size, FontStyle.Regular);
            //btnMinimize.Font = new Font(font.Families[0], btnMinimize.Font.Size, FontStyle.Regular);
            //txtMessage.Font = new Font(font.Families[0], txtMessage.Font.Size, FontStyle.Regular);
            //lvUsersLoggedIn.Font = new Font(font.Families[0], lvUsersLoggedIn.Font.Size, FontStyle.Regular);
            //rtbMessages.Font = new Font(font.Families[0], rtbMessages.Font.Size, FontStyle.Regular);
            //btnLogoff.BackColor = Color.FromArgb(140, 223, 132);
            //lvUsersLoggedIn.BackColor = Color.FromArgb(204, 237, 208);
            //btnLogoff.Font = new Font(font.Families[0], btnLogoff.Font.Size, FontStyle.Regular);
        }

        private void btnSend_Click(object sender, EventArgs e)
            => SendMessage();

        private void SendMessage()
        {
            if (_client.Connected && txtMessage.Text.Trim() != "")
            {
                _client.SendCommand(new Command(CommandType.Message, IPAddress.Broadcast, txtMessage.Text));
                rtbMessages.Text += _client.NetworkName + ": " + txtMessage.Text.Trim() + Environment.NewLine;
                txtMessage.Text = "";
                txtMessage.Focus();
            }
        }

        private void AddToList(string ip, string name)
        {
            ListViewItem newItem = lvUsersLoggedIn.Items.Add(ip);
            newItem.SubItems.Add(name);
        }

        private void OpenPrivateWindow(IPAddress remoteClientIP, string clientName)
        {
            if (_client.Connected)
            {
                if (!IsPrivateWindowOpened(clientName))
                {
                    frmPrivateChat privateWindow = new frmPrivateChat(_client, remoteClientIP, clientName);
                    privateChatList.Add(privateWindow);
                    privateWindow.FormClosed += new FormClosedEventHandler(privateWindow_FormClosed);
                    privateWindow.StartPosition = FormStartPosition.CenterParent;
                    privateWindow.Show(this);
                }
            }
        }

        private void OpenPrivateWindow(IPAddress remoteClientIP, string clientName, string initialMessage)
        {
            if (_client.Connected)
            {
                frmPrivateChat privateWindow = new frmPrivateChat(_client, remoteClientIP, clientName, initialMessage);
                privateChatList.Add(privateWindow);
                privateWindow.FormClosed += new FormClosedEventHandler(privateWindow_FormClosed);
                privateWindow.Show(this);
            }

            void privateWindow_FormClosed(object sender, FormClosedEventArgs e)
            {
                privateChatList.Remove((frmPrivateChat)sender);
            }
        }

        void privateWindow_FormClosed(object sender, FormClosedEventArgs e)
            => privateChatList.Remove((frmPrivateChat)sender);

        private void btnPrivate_Click(object sender, EventArgs e)
            => StartPrivateChat();

        private void StartPrivateChat()
        {
            if (lvUsersLoggedIn.SelectedItems.Count != 0)
                OpenPrivateWindow(IPAddress.Parse(lvUsersLoggedIn.SelectedItems[0].Text), lvUsersLoggedIn.SelectedItems[0].SubItems[1].Text);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
            => _client.Disconnect();


        #region Close button

        private void btnClose_Click(object sender, EventArgs e)
        {
            _client.Disconnect();
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

        #region Logoff button

        private void btnLogoff_Click(object sender, EventArgs e)
        {
            if (btnLogoff.Text == "Login")
            {
                frmLogin frmLogin = new frmLogin();
                frmLogin.ShowDialog();
                _client = frmLogin.Client;

                if (_client.Connected && _client != null)
                {
                    _client.CommandReceived += new CommandReceivedEventHandler(client_CommandReceived);
                    _client.SendCommand(new Command(CommandType.FreeCommand, IPAddress.Broadcast, _client.IP + ":" + _client.NetworkName));
                    _client.SendCommand(new Command(CommandType.SendClientList, _client.ServerIP));
                    AddToList(_client.IP.ToString(), _client.NetworkName);
                    btnLogoff.Text = "Log off";
                }
            }
            else
            {

                btnLogoff.Text = "Login";
                privateChatList.Clear();
                _client.Disconnect();
                lvUsersLoggedIn.Items.Clear();
                txtMessage.Clear();
                txtMessage.Focus();
            }
        }

        #endregion
    }
}

