﻿namespace WindowsFormsApplication1
{
    partial class IMCapture
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.sample = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.option = new System.Windows.Forms.Button();
            this.viewHXData = new System.Windows.Forms.Button();
            this.tbText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // sample
            // 
            this.sample.Location = new System.Drawing.Point(36, 25);
            this.sample.Name = "sample";
            this.sample.Size = new System.Drawing.Size(75, 23);
            this.sample.TabIndex = 3;
            this.sample.Text = "采样";
            this.sample.UseVisualStyleBackColor = true;
            this.sample.Click += new System.EventHandler(this.sample_click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(93, 67);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "3000";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(118, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "停止采样";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.stopSample_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "采样周期";
            // 
            // option
            // 
            this.option.Location = new System.Drawing.Point(36, 122);
            this.option.Name = "option";
            this.option.Size = new System.Drawing.Size(75, 23);
            this.option.TabIndex = 7;
            this.option.Text = "连接选项";
            this.option.UseVisualStyleBackColor = true;
            this.option.Click += new System.EventHandler(this.option_Click);
            // 
            // viewHXData
            // 
            this.viewHXData.Location = new System.Drawing.Point(118, 122);
            this.viewHXData.Name = "viewHXData";
            this.viewHXData.Size = new System.Drawing.Size(75, 23);
            this.viewHXData.TabIndex = 8;
            this.viewHXData.Text = "宏讯OPC";
            this.viewHXData.UseVisualStyleBackColor = true;
            this.viewHXData.Click += new System.EventHandler(this.viewHXData_Click);
            // 
            // tbText
            // 
            this.tbText.Location = new System.Drawing.Point(224, 67);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(100, 21);
            this.tbText.TabIndex = 9;
            // 
            // IMCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 176);
            this.Controls.Add(this.tbText);
            this.Controls.Add(this.viewHXData);
            this.Controls.Add(this.option);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.sample);
            this.Name = "IMCapture";
            this.Text = "数据采集";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IMCapture_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sample;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button option;
        private System.Windows.Forms.Button viewHXData;
        private System.Windows.Forms.TextBox tbText;
    }
}

