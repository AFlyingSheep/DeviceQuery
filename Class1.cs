using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;

namespace FVD.Common
{
    public enum IPType : byte { A, B, C, D, E };

    public class IPAddressTextBox : UserControl
    {
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 2);
            this.textBox1.MaxLength = 3;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(25, 21);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPv4TextBox_KeyPress);
            this.textBox1.Text = "127";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(33, 2);
            this.textBox2.MaxLength = 3;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(25, 21);
            this.textBox2.TabIndex = 1;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPv4TextBox_KeyPress);
            this.textBox2.Text = "0";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(67, 2);
            this.textBox3.MaxLength = 3;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(25, 21);
            this.textBox3.TabIndex = 2;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPv4TextBox_KeyPress);
            this.textBox3.Text = "0";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(100, 2);
            this.textBox4.MaxLength = 3;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(25, 21);
            this.textBox4.TabIndex = 3;
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPv4TextBox_KeyPress);
            this.textBox4.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(25, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = ".";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(57, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = ".";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(92, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = ".";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IPAddressTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "IPAddressTextBox";
            this.Size = new System.Drawing.Size(125, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public IPAddressTextBox()
        {
            InitializeComponent();
        }

        private void IPv4TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char KeyChar = e.KeyChar;
            int TextLength = ((TextBox)sender).TextLength;

            if (KeyChar == '.' || KeyChar == '。' || KeyChar == ' ')
            {
                if ((((TextBox)sender).SelectedText.Length == 0) && (TextLength > 0) && (((TextBox)sender) != textBox4))
                {   // 进入下一个文本框
                    SendKeys.Send("{Tab}");
                }

                e.Handled = true;
            }

            if (Regex.Match(KeyChar.ToString(), "[0-9]").Success)
            {
                if (TextLength == 2)
                {
                    if (int.Parse(((TextBox)sender).Text + e.KeyChar.ToString()) > 255)
                    {
                        ((TextBox)sender).Text = "255";
                    }
                }
                else if (TextLength == 1&& ((TextBox)sender).Text=="0")
                {
                    e.Handled = true;
                }
            }
            else
            {   // 回删操作
                if (KeyChar == '\b')
                {
                    if (TextLength == 0)
                    {
                        if (((TextBox)sender) != textBox1)
                        {   // 回退到上一个文本框 Shift+Tab
                            SendKeys.Send("+{TAB}{End}");
                        }
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// string类型的IP地址
        /// </summary>
        override public string Text
        {
            get
            {
                return this.Value.ToString();
            }
            set
            {
                IPAddress address;
                if (IPAddress.TryParse(value, out address))
                {
                    byte[] bytes = address.GetAddressBytes();
                    for (int i = 1; i <= 4; i++)
                    {
                        this.Controls["textBox" + i.ToString()].Text = bytes[i - 1].ToString("D");
                    }
                }
            }
        }

        /// <summary>
        /// IP地址
        /// </summary>
        public IPAddress Value
        {
            get
            {
                IPAddress address;
                string ipString = textBox1.Text + "." + textBox2.Text + "." + textBox3.Text + "." + textBox4.Text;

                if (IPAddress.TryParse(ipString, out address))
                {
                    return address;
                }
                else
                {
                    return new IPAddress(0);
                }
            }
            set
            {
                byte[] bytes = value.GetAddressBytes();
                for (int i = 1; i <= 4; i++)
                {
                    this.Controls["textBox" + i.ToString()].Text = bytes[i - 1].ToString("D");
                }
            }
        }

        /// <summary>
        /// IP地址分类
        /// </summary>
        public IPType Type
        {
            get
            {
                byte[] bytes = this.Value.GetAddressBytes();
                int FirstByte = bytes[0];

                if (FirstByte < 128)
                {
                    return IPType.A;
                }
                else if (FirstByte < 192)
                {
                    return IPType.B;
                }
                else if (FirstByte < 224)
                {
                    return IPType.C;
                }
                else if (FirstByte < 240)
                {
                    return IPType.D;
                }
                else
                {
                    return IPType.E;    // 保留做研究用 
                }
            }
        }

        public bool ValidateIP()
        {
            IPAddress address;
            string ipString = textBox1.Text + "." + textBox2.Text + "." + textBox3.Text + "." + textBox4.Text;

            return IPAddress.TryParse(ipString, out address);
        }

        /// <summary>
        /// 控件的边框样式
        /// </summary>
        new public BorderStyle BorderStyle
        {
            get
            {
                return this.textBox1.BorderStyle;
            }
            set
            {
                for (int i = 1; i <= 4; i++)
                {
                    ((TextBox)this.Controls["textBox" + i.ToString()]).BorderStyle = value;
                }
            }
        }
    }
}