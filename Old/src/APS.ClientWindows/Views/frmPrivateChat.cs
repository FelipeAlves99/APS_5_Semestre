using APS.ClientCommand;
using System;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APS.ClientWindows
{
    public partial class frmPrivateChat : Form
    {
        private CMDClient remoteClient;
        private IPAddress targetIP;
        private bool activated;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public string RemoteName { get; }

        public frmPrivateChat(CMDClient cmdClient, IPAddress friendIP, string name, string initialMessage)
        {
            InitializeComponent();
            remoteClient = cmdClient;
            targetIP = friendIP;
            RemoteName = name;
            lblPrivateChatName.Text = name;
            txtMessages.Text = RemoteName + ": " + initialMessage + Environment.NewLine;
            remoteClient.CommandReceived += new CommandReceivedEventHandler(private_CommandReceived);
        }

        public frmPrivateChat(CMDClient cmdClient, IPAddress friendIP, string name)
        {
            InitializeComponent();
            remoteClient = cmdClient;
            targetIP = friendIP;
            RemoteName = name;
            lblPrivateChatName.Text = name;
            remoteClient.CommandReceived += new CommandReceivedEventHandler(private_CommandReceived);
        }

        private void private_CommandReceived(object sender, CommandEventArgs e)
        {
            switch (e.Command.CommandType)
            {
                case (CommandType.Message):
                    if (!e.Command.Target.Equals(IPAddress.Broadcast) && e.Command.SenderIP.Equals(targetIP))
                        txtMessages.Text += e.Command.SenderName + ": " + e.Command.MetaData + Environment.NewLine;

                    break;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
            => SendMessage();


        private void SendMessage()
        {
            if (remoteClient.Connected && txtNewMessage.Text.Trim() != "")
            {
                remoteClient.SendCommand(new Command(CommandType.Message, targetIP, txtNewMessage.Text));
                txtMessages.Text += remoteClient.NetworkName + ": " + txtNewMessage.Text.Trim() + Environment.NewLine;
                txtNewMessage.Text = "";
                txtNewMessage.Focus();
            }
        }

        private void frmPrivate_FormClosing(object sender, FormClosingEventArgs e)
            => remoteClient.CommandReceived -= new CommandReceivedEventHandler(private_CommandReceived);


        private void frmPrivate_Activated(object sender, EventArgs e)
            => activated = true;


        private void frmPrivate_Deactivate(object sender, EventArgs e)
            => activated = false;


        #region Close button

        private void btnClose_Click(object sender, EventArgs e)
            => Close();

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
