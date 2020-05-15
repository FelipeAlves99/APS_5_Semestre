using APS.ClientCommand;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APS.ClientWindows.Views
{
    public partial class frmLogin : Form
    {
        private bool canClose;
        private int serverPort = 10220;
        private IPAddress serverIP = IPAddress.Parse("127.0.0.1");
        private CMDClient client;
        public CMDClient Client
        {
            get { return client; }
        }

        public frmLogin()
        {
            InitializeComponent();
            this.canClose = false;
            Control.CheckForIllegalCrossThreadCalls = false;
            this.client = new CMDClient(serverIP, serverPort, "None");
            this.client.CommandReceived += new CommandReceivedEventHandler(CommandReceived);
            this.client.ConnectingSuccessed += new ConnectingSuccessedEventHandler(client_ConnectingSuccessed);
            this.client.ConnectingFailed += new ConnectingFailedEventHandler(client_ConnectingFailed);
        }

        private void client_ConnectingFailed(object sender, EventArgs e)
            => MessageBox.Show("Falha ao conectar com o servidor.", "Erro de conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);

        private void client_ConnectingSuccessed(object sender, EventArgs e)
            => this.client.SendCommand(new Command(ClientCommand.CommandType.IsNameExists, this.client.IP, this.client.NetworkName));


        void CommandReceived(object sender, CommandEventArgs e)
        {
            if (e.Command.CommandType == ClientCommand.CommandType.IsNameExists)
            {
                if (e.Command.MetaData.ToLower() == "true")
                {
                    MessageBox.Show("O nome de usuário já existe no servidor.", "Erro de conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.client.Disconnect();
                }
                else
                {
                    this.canClose = true;
                    this.Close();
                }
            }
        }

        private void LoginToServer()
        {
            if (this.txtUserName.Text.Trim() == "")
                MessageBox.Show("Nome de usuário está vazio.", "Erro de conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
            {
                this.client.NetworkName = this.txtUserName.Text.Trim();
                this.client.ConnectToServer();                
            }
        }

        private void frmServerConfig_Load(object sender, EventArgs e)
        {
            PrivateFontCollection font = new PrivateFontCollection();
            font.AddFontFile("../../Resources/ubuntumono-r.ttf");

            lblUserName.Font = new Font(font.Families[0], lblUserName.Font.Size, FontStyle.Regular);
            lblIp.Font = new Font(font.Families[0], lblIp.Font.Size, FontStyle.Regular);
            lblPort.Font = new Font(font.Families[0], lblPort.Font.Size, FontStyle.Regular);
            txtUserName.Font = new Font(font.Families[0], txtUserName.Font.Size, FontStyle.Regular);
            txtIp1.Font = new Font(font.Families[0], txtIp1.Font.Size, FontStyle.Regular);
            txtIp2.Font = new Font(font.Families[0], txtIp2.Font.Size, FontStyle.Regular);
            txtIp3.Font = new Font(font.Families[0], txtIp3.Font.Size, FontStyle.Regular);
            txtIp4.Font = new Font(font.Families[0], txtIp4.Font.Size, FontStyle.Regular);
            txtPort.Font = new Font(font.Families[0], txtPort.Font.Size, FontStyle.Regular);
            btnConnect.Font = new Font(font.Families[0], btnConnect.Font.Size, FontStyle.Regular);
            btnClose.Font = new Font(font.Families[0], btnClose.Font.Size, FontStyle.Regular);
            btnMinimize.Font = new Font(font.Families[0], btnMinimize.Font.Size, FontStyle.Regular);
            btnConnect.BackColor = Color.FromArgb(140, 223, 132);
        }

        #region Close button

        private void btnClose_Click(object sender, EventArgs e)
            => Application.Exit();

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
        {
            LoginToServer();            
        }

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
    }
}
