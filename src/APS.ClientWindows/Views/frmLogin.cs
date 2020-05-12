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
        private ClientManager _client;

        public frmLogin()
        {
            InitializeComponent();            
        }

        private void frmServerConfig_Load(object sender, EventArgs e)
        {
            PrivateFontCollection font = new PrivateFontCollection();
            font.AddFontFile("../../Resources/ubuntumono-r.ttf");

            lblUserName.Font = new Font(font.Families[0], lblUserName.Font.Size, FontStyle.Regular);
            txtUserName.Font = new Font(font.Families[0], txtUserName.Font.Size, FontStyle.Regular);
            lblHostname.Font = new Font(font.Families[0], lblHostname.Font.Size, FontStyle.Regular);
            txtHostname.Font = new Font(font.Families[0], txtHostname.Font.Size, FontStyle.Regular);
            btnConnect.Font = new Font(font.Families[0], btnConnect.Font.Size, FontStyle.Regular);
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
            _client = new ClientManager(txtUserName.Text, txtHostname.Text);

            try
            {
                _client.ConnectToServer();
                frmGroupChat groupChat = new frmGroupChat();
                groupChat.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

    }
}
