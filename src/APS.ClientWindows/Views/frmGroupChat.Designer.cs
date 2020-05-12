namespace APS.ClientWindows.Views
{
    partial class frmGroupChat
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
            if (disposing && (components != null))
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
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lvUsersLoggedIn = new System.Windows.Forms.ListView();
            this.rtbMessages = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(12, 441);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(438, 53);
            this.txtMessage.TabIndex = 0;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(457, 441);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(108, 53);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Enviar";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lvUsersLoggedIn
            // 
            this.lvUsersLoggedIn.HideSelection = false;
            this.lvUsersLoggedIn.Location = new System.Drawing.Point(12, 12);
            this.lvUsersLoggedIn.Name = "lvUsersLoggedIn";
            this.lvUsersLoggedIn.Size = new System.Drawing.Size(121, 423);
            this.lvUsersLoggedIn.TabIndex = 2;
            this.lvUsersLoggedIn.UseCompatibleStateImageBehavior = false;
            // 
            // rtbMessages
            // 
            this.rtbMessages.Location = new System.Drawing.Point(139, 12);
            this.rtbMessages.Name = "rtbMessages";
            this.rtbMessages.Size = new System.Drawing.Size(426, 423);
            this.rtbMessages.TabIndex = 3;
            this.rtbMessages.Text = "";
            // 
            // frmGroupChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 506);
            this.Controls.Add(this.rtbMessages);
            this.Controls.Add(this.lvUsersLoggedIn);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessage);
            this.Name = "frmGroupChat";
            this.Text = "frmGroupChat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ListView lvUsersLoggedIn;
        private System.Windows.Forms.RichTextBox rtbMessages;
    }
}