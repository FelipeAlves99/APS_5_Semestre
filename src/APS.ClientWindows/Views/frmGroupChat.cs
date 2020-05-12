using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APS.ClientWindows.Views
{
    public partial class frmGroupChat : Form
    {
        private List<frmPrivateChat> privateChatList;

        public frmGroupChat()
        {
            InitializeComponent();
            this.privateChatList = new List<frmPrivateChat>();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

        }

    }
}
        
