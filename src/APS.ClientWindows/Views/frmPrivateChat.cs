﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APS.ClientWindows
{
    public partial class frmPrivateChat : Form
    {
        ClientManager _client;

        public frmPrivateChat(ClientManager client)
        {
            InitializeComponent();
            _client = client;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {            
        }
    }
}
