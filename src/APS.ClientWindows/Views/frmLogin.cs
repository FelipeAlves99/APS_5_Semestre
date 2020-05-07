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
        private const int port = 11000;
        private static ManualResetEvent connectDone =
        new ManualResetEvent(false);

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
            //ConnectToServer();
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(txtHostname.Text);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.  
                Socket client = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.  
                client.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        #endregion

    }

    // State object for receiving data from remote device.  
    public class StateObject
    {
        // Client socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 256;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
    }
}
