using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Proshot.UtilityLib.CommonDialogs;
using CommandClient;
using System.Runtime.InteropServices;

namespace ChatClient
{
    public partial class frmLogin : Form
    {
        private bool canClose;
        private CMDClient client;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private int serverPort = 10220;
        private IPAddress serverIP = IPAddress.Parse("127.0.0.1");
        public CMDClient Client
        {
            get { return client; }
        }
        public frmLogin()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void client_ConnectingFailed(object sender, EventArgs e)
            => MessageBox.Show("Falha ao conectar com o servidor.", "Erro de conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);

        private void client_ConnectingSuccessed(object sender, EventArgs e)
            => client.SendCommand(new Command(CommandClient.CommandType.IsNameExists, this.client.IP, this.client.NetworkName));

        void CommandReceived(object sender, CommandEventArgs e)
        {
            if (e.Command.CommandType == CommandClient.CommandType.IsNameExists)
            {
                if (e.Command.MetaData.ToLower() == "true")
                {
                    MessageBox.Show("O nome de usuário já existe no servidor.", "Erro de conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Client.Disconnect();
                }
                else
                    this.Close();
            }
        }

        private void LoginToServer()
        {
            if (this.txtUserName.Text.Trim() == "")
                MessageBox.Show("Nome de usuário está vazio.", "Erro de conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
            {
                client = new CMDClient(GetIPAddress(), GetPortNumber(), "None");
                client.CommandReceived += new CommandReceivedEventHandler(CommandReceived);
                client.ConnectingSuccessed += new ConnectingSuccessedEventHandler(client_ConnectingSuccessed);
                client.ConnectingFailed += new ConnectingFailedEventHandler(client_ConnectingFailed);

                client.NetworkName = this.txtUserName.Text.Trim();
                client.ConnectToServer();
            }
        }

        private int GetPortNumber()
        {
            int.TryParse(txtPort.Text, out int portNumber);
            return portNumber;
        }

        private IPAddress GetIPAddress()
        {
            var stringIP = $"{txtIp1.Text}.{txtIp2.Text}.{txtIp3.Text}.{txtIp4.Text}";
            IPAddress.TryParse(stringIP, out IPAddress address);
            return address;
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
            => client.CommandReceived -= new CommandReceivedEventHandler(CommandReceived);

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

        #region Connect button

        private void btnConnect_Click(object sender, EventArgs e)
            => LoginToServer();

        #endregion

        #region Info button

        private void btnInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Caso esteja acessando localmente, use a seguinte informação para Ip: 127.0.0.1." +
                $"{Environment.NewLine}Caso esteja acessando remotamente, use o ip da máquina do servidor.",
                "Informações de conexão",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnInfo_MouseEnter(object sender, EventArgs e)
            => btnInfo.BackColor = Color.FromArgb(70, 113, 107);

        private void btnInfo_MouseLeave(object sender, EventArgs e)
            => btnInfo.BackColor = Color.FromArgb(64, 102, 97);

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

        private void frmLogin_Load(object sender, EventArgs e)
            => btnConnect.BackColor = Color.FromArgb(140, 223, 132);

    }
}