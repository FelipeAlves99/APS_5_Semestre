using APS.ClientCommand;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Net;
using System.Windows.Forms;

namespace APS.ClientWindows.Views
{
    public partial class frmLogin : Form
    {
        public CMDClient Client { get; private set; }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public frmLogin()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void client_ConnectingFailed(object sender, EventArgs e)
            => MessageBox.Show("Falha ao conectar com o servidor.", "Erro de conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);

        private void client_ConnectingSuccessed(object sender, EventArgs e)
            => this.Client.SendCommand(new Command(ClientCommand.CommandType.IsNameExists, this.Client.IP, this.Client.NetworkName));

        void CommandReceived(object sender, CommandEventArgs e)
        {
            if (e.Command.CommandType == ClientCommand.CommandType.IsNameExists)
            {
                if (e.Command.MetaData.ToLower() == "true")
                {
                    MessageBox.Show("O nome de usuário já existe no servidor.", "Erro de conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Client.Disconnect();
                }
            }
        }

        private void LoginToServer()
        {
            if (this.txtUserName.Text.Trim() == "")
                MessageBox.Show("Nome de usuário está vazio.", "Erro de conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
            {
                this.Client = new CMDClient(GetIPAddress(), GetPortNumber(), "None");
                this.Client.CommandReceived += new CommandReceivedEventHandler(CommandReceived);
                this.Client.ConnectingSuccessed += new ConnectingSuccessedEventHandler(client_ConnectingSuccessed);
                this.Client.ConnectingFailed += new ConnectingFailedEventHandler(client_ConnectingFailed);

                this.Client.NetworkName = this.txtUserName.Text.Trim();
                this.Client.ConnectToServer();

                OpenMainChat();
            }
        }

        private void OpenMainChat()
        {
            this.Hide();
            frmGroupChat groupChat = new frmGroupChat();
            groupChat.Show();
        }

        private int GetPortNumber()
        {
            int.TryParse(txtPort.Text, out int portNumber);
            return portNumber;
        }

        private IPAddress GetIPAddress()
        {
            var stringIP = $"{txtIp1.Text}.{txtIp2.Text}.{txtIp3.Text}.{txtIp4.Text}";
            return IPAddress.Parse(stringIP);
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
