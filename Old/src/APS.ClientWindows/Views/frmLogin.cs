using APS.ClientCommand;
using System;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APS.ClientWindows.Views
{
    public partial class frmLogin : Form
    {
        public CMDClient Client { get; private set; }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public frmLogin()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void client_ConnectingFailed(object sender, EventArgs e)
            => MessageBox.Show("Falha ao conectar com o servidor.", "Erro de conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);

        private void client_ConnectingSuccessed(object sender, EventArgs e)
            => Client.SendCommand(new Command(CommandType.IsNameExists, Client.IP, Client.NetworkName));

        void CommandReceived(object sender, CommandEventArgs e)
        {
            if (e.Command.CommandType == CommandType.IsNameExists)
            {
                if (e.Command.MetaData.ToLower() == "true")
                {
                    MessageBox.Show("O nome de usuário já existe no servidor.", "Erro de conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Client.Disconnect();
                }
                else
                    Close();
            }
        }

        private void LoginToServer()
        {
            if (txtUserName.Text.Trim() == "")
                MessageBox.Show("Nome de usuário está vazio.", "Erro de conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
            {
                Client = new CMDClient(GetIPAddress(), GetPortNumber(), "None");
                Client.CommandReceived += new CommandReceivedEventHandler(CommandReceived);
                Client.ConnectingSuccessed += new ConnectingSuccessedEventHandler(client_ConnectingSuccessed);
                Client.ConnectingFailed += new ConnectingFailedEventHandler(client_ConnectingFailed);

                Client.NetworkName = txtUserName.Text.Trim();
                Client.ConnectToServer();
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

        private void frmLogin_Load(object sender, EventArgs e)
            => btnConnect.BackColor = Color.FromArgb(140, 223, 132);

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
            => Client.CommandReceived -= new CommandReceivedEventHandler(CommandReceived);

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
            => WindowState = FormWindowState.Minimized;

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

    }
}
