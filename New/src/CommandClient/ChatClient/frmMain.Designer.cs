namespace ChatClient
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.txtNewMessage = new Proshot.UtilityLib.TextBox();
            this.btnSend = new Proshot.UtilityLib.Button();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.btnPrivate = new Proshot.UtilityLib.Button();
            this.lstViwUsers = new Proshot.UtilityLib.ListView();
            this.colIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUserName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtMessages = new System.Windows.Forms.RichTextBox();
            this.pnlControlBox = new System.Windows.Forms.Panel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlControlBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNewMessage
            // 
            this.txtNewMessage.BorderWidth = 1F;
            this.txtNewMessage.FloatValue = 0D;
            this.txtNewMessage.Location = new System.Drawing.Point(163, 458);
            this.txtNewMessage.Multiline = true;
            this.txtNewMessage.Name = "txtNewMessage";
            this.txtNewMessage.Size = new System.Drawing.Size(218, 36);
            this.txtNewMessage.TabIndex = 1;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.Transparent;
            this.btnSend.BackgroundImage = global::ChatClient.Properties.Resources.forward;
            this.btnSend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSend.ImageKey = "(nenhum/a)";
            this.btnSend.ImageList = this.imgList;
            this.btnSend.Location = new System.Drawing.Point(387, 458);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(48, 36);
            this.btnSend.TabIndex = 3;
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "Smiely.png");
            this.imgList.Images.SetKeyName(1, "Private.ico");
            this.imgList.Images.SetKeyName(2, "SendMessage.ico");
            this.imgList.Images.SetKeyName(3, "Enter.ico");
            this.imgList.Images.SetKeyName(4, "Exit.ico");
            // 
            // btnPrivate
            // 
            this.btnPrivate.BackColor = System.Drawing.Color.Transparent;
            this.btnPrivate.FlatAppearance.BorderSize = 0;
            this.btnPrivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrivate.ImageKey = "(nenhum/a)";
            this.btnPrivate.ImageList = this.imgList;
            this.btnPrivate.Location = new System.Drawing.Point(12, 458);
            this.btnPrivate.Name = "btnPrivate";
            this.btnPrivate.Size = new System.Drawing.Size(145, 36);
            this.btnPrivate.TabIndex = 6;
            this.btnPrivate.Text = "Chat privado";
            this.btnPrivate.UseVisualStyleBackColor = false;
            this.btnPrivate.Click += new System.EventHandler(this.btnPrivate_Click);
            // 
            // lstViwUsers
            // 
            this.lstViwUsers.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lstViwUsers.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lstViwUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIcon,
            this.colUserName});
            this.lstViwUsers.FullRowSelect = true;
            this.lstViwUsers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstViwUsers.HideSelection = false;
            this.lstViwUsers.LabelWrap = false;
            this.lstViwUsers.Location = new System.Drawing.Point(12, 48);
            this.lstViwUsers.MultiSelect = false;
            this.lstViwUsers.Name = "lstViwUsers";
            this.lstViwUsers.RightToLeftLayout = true;
            this.lstViwUsers.Size = new System.Drawing.Size(145, 404);
            this.lstViwUsers.SmallImageList = this.imgList;
            this.lstViwUsers.TabIndex = 10;
            this.lstViwUsers.UseCompatibleStateImageBehavior = false;
            this.lstViwUsers.View = System.Windows.Forms.View.Details;
            // 
            // colIcon
            // 
            this.colIcon.Text = "";
            this.colIcon.Width = 23;
            // 
            // colUserName
            // 
            this.colUserName.Text = "";
            this.colUserName.Width = 85;
            // 
            // txtMessages
            // 
            this.txtMessages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessages.Location = new System.Drawing.Point(163, 48);
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.Size = new System.Drawing.Size(272, 404);
            this.txtMessages.TabIndex = 11;
            this.txtMessages.Text = "";
            // 
            // pnlControlBox
            // 
            this.pnlControlBox.BackColor = System.Drawing.Color.Transparent;
            this.pnlControlBox.Controls.Add(this.btnLogin);
            this.pnlControlBox.Controls.Add(this.pictureBox1);
            this.pnlControlBox.Controls.Add(this.btnMinimize);
            this.pnlControlBox.Controls.Add(this.btnClose);
            this.pnlControlBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControlBox.Location = new System.Drawing.Point(0, 0);
            this.pnlControlBox.Name = "pnlControlBox";
            this.pnlControlBox.Size = new System.Drawing.Size(448, 30);
            this.pnlControlBox.TabIndex = 12;
            this.pnlControlBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlControlBox_MouseDown);
            // 
            // btnLogin
            // 
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(38, 0);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(0);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(95, 30);
            this.btnLogin.TabIndex = 10;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ChatClient.Properties.Resources.logo_transparent;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMinimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimize.ForeColor = System.Drawing.Color.White;
            this.btnMinimize.Location = new System.Drawing.Point(386, 0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(31, 30);
            this.btnMinimize.TabIndex = 1;
            this.btnMinimize.Text = "-";
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            this.btnMinimize.MouseEnter += new System.EventHandler(this.btnMinimize_MouseEnter);
            this.btnMinimize.MouseLeave += new System.EventHandler(this.btnMinimize_MouseLeave);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Red;
            this.btnClose.Location = new System.Drawing.Point(417, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(31, 30);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::ChatClient.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(448, 498);
            this.ControlBox = false;
            this.Controls.Add(this.pnlControlBox);
            this.Controls.Add(this.lstViwUsers);
            this.Controls.Add(this.txtMessages);
            this.Controls.Add(this.btnPrivate);
            this.Controls.Add(this.txtNewMessage);
            this.Controls.Add(this.btnSend);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnlControlBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Proshot.UtilityLib.TextBox txtNewMessage;
        private Proshot.UtilityLib.Button btnSend;
        private Proshot.UtilityLib.Button btnPrivate;
        private System.Windows.Forms.ImageList imgList;
        private Proshot.UtilityLib.ListView lstViwUsers;
        private System.Windows.Forms.ColumnHeader colIcon;
        private System.Windows.Forms.ColumnHeader colUserName;
        private System.Windows.Forms.RichTextBox txtMessages;
        private System.Windows.Forms.Panel pnlControlBox;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnClose;
    }
}

