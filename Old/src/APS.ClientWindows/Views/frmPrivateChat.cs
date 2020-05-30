using APS.ClientCommand;
using System;
using System.Net;
using System.Windows.Forms;

namespace APS.ClientWindows
{
    public partial class frmPrivateChat : Form
    {

        private CMDClient remoteClient;
        private IPAddress targetIP;        
        public string RemoteName { get; }

        public frmPrivateChat(CMDClient cmdClient, IPAddress friendIP, string name, string initialMessage)
        {
            InitializeComponent();
            remoteClient = cmdClient;
            targetIP = friendIP;
            RemoteName = name;
            lblPrivateChatName.Text = name;
            rtbMessages.Text = RemoteName + ": " + initialMessage + Environment.NewLine;
            remoteClient.CommandReceived += new CommandReceivedEventHandler(private_CommandReceived);
        }

        public frmPrivateChat(CMDClient cmdClient, IPAddress friendIP, string name)
        {
            InitializeComponent();
            remoteClient = cmdClient;
            targetIP = friendIP;
            RemoteName = name;
            Text += " With " + name;
            remoteClient.CommandReceived += new CommandReceivedEventHandler(private_CommandReceived);
        }

        private void private_CommandReceived(object sender, CommandEventArgs e)
        {
            switch (e.Command.CommandType)
            {
                case (ClientCommand.CommandType.Message):
                    if (!e.Command.Target.Equals(IPAddress.Broadcast) && e.Command.SenderIP.Equals(targetIP))
                        rtbMessages.Text += e.Command.SenderName + ": " + e.Command.MetaData + Environment.NewLine;

                    break;
            }
        }


        private void btnSend_Click(object sender, EventArgs e)
            => SendMessage();

        private void SendMessage()
        {
            if (remoteClient.Connected && txtMessage.Text.Trim() != "")
            {
                remoteClient.SendCommand(new Command(ClientCommand.CommandType.Message, targetIP, txtMessage.Text));
                rtbMessages.Text += remoteClient.NetworkName + ": " + txtMessage.Text.Trim() + Environment.NewLine;
                txtMessage.Text = "";
                txtMessage.Focus();
            }
        }
    }
}
