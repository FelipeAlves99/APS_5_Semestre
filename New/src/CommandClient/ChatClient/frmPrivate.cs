using System;
using System.Drawing;
using System.Windows.Forms;
using CommandClient;
using System.Net;
using System.Runtime.InteropServices;

namespace ChatClient
{
    public partial class frmPrivate : Form
    {

        private CMDClient remoteClient;
        private IPAddress targetIP;
        private string remoteName;
        private bool activated;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public string RemoteName
        {
            get { return this.remoteName; }
        }
        protected override bool ProcessCmdKey(ref Message msg , Keys keyData)
        {
            if ( keyData == Keys.Enter )
                this.SendMessage();
            if ( this.txtMessages.Focused && !ShareUtils.IsValidKeyForReadOnlyFields(keyData) )
                return true;
            return base.ProcessCmdKey(ref msg , keyData);
        }
        public frmPrivate(CMDClient cmdClient,IPAddress friendIP,string name,string initialMessage)
        {
            InitializeComponent();
            remoteClient = cmdClient;
            targetIP = friendIP;
            remoteName = name;
            lblPrivateChatName.Text = name;
            txtMessages.Text = this.remoteName + ": " + initialMessage +Environment.NewLine;
            remoteClient.CommandReceived += new CommandReceivedEventHandler(private_CommandReceived);
        }

        public frmPrivate(CMDClient cmdClient , IPAddress friendIP , string name)
        {
            InitializeComponent();
            remoteClient = cmdClient;
            targetIP = friendIP;
            remoteName = name;
            lblPrivateChatName.Text = name;
            remoteClient.CommandReceived += new CommandReceivedEventHandler(private_CommandReceived);
        }

        private void private_CommandReceived(object sender , CommandEventArgs e)
        {
            switch ( e.Command.CommandType )
            {
                case ( CommandType.Message ):
                    if ( !e.Command.Target.Equals(IPAddress.Broadcast) && e.Command.SenderIP.Equals(this.targetIP))
                    {
                        this.txtMessages.Text += e.Command.SenderName + ": " + e.Command.MetaData + Environment.NewLine;
                    }
                    break;
            }    
        }

        private void btnSend_Click(object sender , EventArgs e)
        {
            this.SendMessage();
        }

        private void SendMessage()
        {
            if ( this.remoteClient.Connected && this.txtNewMessage.Text.Trim() != "")
            {
                this.remoteClient.SendCommand(new Command(CommandType.Message , this.targetIP , this.txtNewMessage.Text));
                this.txtMessages.Text += this.remoteClient.NetworkName + ": " + this.txtNewMessage.Text.Trim() + Environment.NewLine;
                this.txtNewMessage.Text = "";
                this.txtNewMessage.Focus();
            }
        }

        private void frmPrivate_FormClosing(object sender , FormClosingEventArgs e)
        {
            this.remoteClient.CommandReceived -= new CommandReceivedEventHandler(private_CommandReceived);
        }       

        private void frmPrivate_Load(object sender , EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width , Screen.PrimaryScreen.WorkingArea.Height - this.DesktopBounds.Height);
        }

        private void frmPrivate_Activated(object sender , EventArgs e)
        {
            this.activated = true;
        }

        private void frmPrivate_Deactivate(object sender , EventArgs e)
        {
            this.activated = false;
        }

        #region Close button

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
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