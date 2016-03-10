namespace WindowsFormsApplication1
{
    partial class Form2
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
            this.controllerType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.machineID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.protocol = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.IP = new System.Windows.Forms.TextBox();
            this.loginName = new System.Windows.Forms.TextBox();
            this.loginPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.connConfirm = new System.Windows.Forms.TextBox();
            this.isDisplay = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.OK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.deleteConn = new System.Windows.Forms.Button();
            this.cbConns = new System.Windows.Forms.ComboBox();
            this.alter = new System.Windows.Forms.Button();
            this.orderNumber = new System.Windows.Forms.TextBox();
            this.userID = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // controllerType
            // 
            this.controllerType.Location = new System.Drawing.Point(35, 49);
            this.controllerType.Name = "controllerType";
            this.controllerType.Size = new System.Drawing.Size(100, 21);
            this.controllerType.TabIndex = 0;
            this.controllerType.Text = "gefranVedo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "控制器类型";
            // 
            // machineID
            // 
            this.machineID.Location = new System.Drawing.Point(200, 50);
            this.machineID.Name = "machineID";
            this.machineID.Size = new System.Drawing.Size(100, 21);
            this.machineID.TabIndex = 0;
            this.machineID.Text = "gefranVedo001";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "机器名";
            // 
            // protocol
            // 
            this.protocol.Location = new System.Drawing.Point(363, 50);
            this.protocol.Name = "protocol";
            this.protocol.Size = new System.Drawing.Size(100, 21);
            this.protocol.TabIndex = 0;
            this.protocol.Text = "telnet";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(362, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "协议";
            // 
            // IP
            // 
            this.IP.Location = new System.Drawing.Point(35, 103);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(100, 21);
            this.IP.TabIndex = 0;
            this.IP.Text = "192.168.8.211";
            // 
            // loginName
            // 
            this.loginName.Location = new System.Drawing.Point(200, 104);
            this.loginName.Name = "loginName";
            this.loginName.Size = new System.Drawing.Size(100, 21);
            this.loginName.TabIndex = 0;
            this.loginName.Text = "telnet";
            // 
            // loginPassword
            // 
            this.loginPassword.Location = new System.Drawing.Point(363, 104);
            this.loginPassword.Name = "loginPassword";
            this.loginPassword.Size = new System.Drawing.Size(100, 21);
            this.loginPassword.TabIndex = 0;
            this.loginPassword.Text = "gefranseven";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "IP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(199, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "登陆名";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(362, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "登陆密码";
            // 
            // connConfirm
            // 
            this.connConfirm.Location = new System.Drawing.Point(35, 153);
            this.connConfirm.Name = "connConfirm";
            this.connConfirm.Size = new System.Drawing.Size(100, 21);
            this.connConfirm.TabIndex = 0;
            this.connConfirm.Text = "1";
            // 
            // isDisplay
            // 
            this.isDisplay.Location = new System.Drawing.Point(200, 154);
            this.isDisplay.Name = "isDisplay";
            this.isDisplay.Size = new System.Drawing.Size(100, 21);
            this.isDisplay.TabIndex = 0;
            this.isDisplay.Text = "1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "是否连接";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(199, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "是否显示";
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(109, 277);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 2;
            this.OK.Text = "增加";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.userID);
            this.panel1.Controls.Add(this.orderNumber);
            this.panel1.Controls.Add(this.isDisplay);
            this.panel1.Controls.Add(this.controllerType);
            this.panel1.Controls.Add(this.IP);
            this.panel1.Controls.Add(this.connConfirm);
            this.panel1.Controls.Add(this.machineID);
            this.panel1.Controls.Add(this.loginName);
            this.panel1.Controls.Add(this.protocol);
            this.panel1.Controls.Add(this.loginPassword);
            this.panel1.Location = new System.Drawing.Point(68, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(532, 246);
            this.panel1.TabIndex = 3;
            // 
            // deleteConn
            // 
            this.deleteConn.Location = new System.Drawing.Point(448, 277);
            this.deleteConn.Name = "deleteConn";
            this.deleteConn.Size = new System.Drawing.Size(75, 23);
            this.deleteConn.TabIndex = 4;
            this.deleteConn.Text = "删除";
            this.deleteConn.UseVisualStyleBackColor = true;
            this.deleteConn.Click += new System.EventHandler(this.deleteConn_Click);
            // 
            // cbConns
            // 
            this.cbConns.FormattingEnabled = true;
            this.cbConns.Location = new System.Drawing.Point(13, 13);
            this.cbConns.Name = "cbConns";
            this.cbConns.Size = new System.Drawing.Size(121, 20);
            this.cbConns.TabIndex = 5;
            // 
            // alter
            // 
            this.alter.Location = new System.Drawing.Point(274, 277);
            this.alter.Name = "alter";
            this.alter.Size = new System.Drawing.Size(75, 23);
            this.alter.TabIndex = 6;
            this.alter.Text = "修改";
            this.alter.UseVisualStyleBackColor = true;
            this.alter.Click += new System.EventHandler(this.alter_Click);
            // 
            // orderNumber
            // 
            this.orderNumber.Location = new System.Drawing.Point(363, 153);
            this.orderNumber.Name = "orderNumber";
            this.orderNumber.Size = new System.Drawing.Size(100, 21);
            this.orderNumber.TabIndex = 1;
            // 
            // userID
            // 
            this.userID.Location = new System.Drawing.Point(35, 198);
            this.userID.Name = "userID";
            this.userID.Size = new System.Drawing.Size(100, 21);
            this.userID.TabIndex = 1;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 372);
            this.Controls.Add(this.alter);
            this.Controls.Add(this.cbConns);
            this.Controls.Add(this.deleteConn);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.panel1);
            this.Name = "Form2";
            this.Text = "连接选项";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox controllerType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox machineID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox protocol;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox IP;
        private System.Windows.Forms.TextBox loginName;
        private System.Windows.Forms.TextBox loginPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox connConfirm;
        private System.Windows.Forms.TextBox isDisplay;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button deleteConn;
        private System.Windows.Forms.ComboBox cbConns;
        private System.Windows.Forms.Button alter;
        private System.Windows.Forms.TextBox userID;
        private System.Windows.Forms.TextBox orderNumber;
    }
}