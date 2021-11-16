using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace DeviceExplorer
{
    partial class DeviceExplorer
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceExplorer));
            this.LabelBindingIPAddress = new System.Windows.Forms.Label();
            this.LabelRemoteIPAddress = new System.Windows.Forms.Label();
            this.LabelBrowseMode = new System.Windows.Forms.Label();
            this.LabelRemoteSubnetMask = new System.Windows.Forms.Label();
            this.ComboBoxBrowseMode = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ButtonRemove = new System.Windows.Forms.Button();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.ListBoxPointToPointIPAddress = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LabelDevicesNumber = new System.Windows.Forms.Label();
            this.LabelDevicesFound = new System.Windows.Forms.Label();
            this.ButtonClear = new System.Windows.Forms.Button();
            this.ButtonStop = new System.Windows.Forms.Button();
            this.ButtonStart = new System.Windows.Forms.Button();
            this.ListBoxDevices = new System.Windows.Forms.ListBox();
            this.LabelPointToPoint = new System.Windows.Forms.Label();
            this.LabelBrowse = new System.Windows.Forms.Label();
            this.LabelStatus = new System.Windows.Forms.Label();
            this.LabelStatusText = new System.Windows.Forms.Label();
            this.RIA = new FVD.Common.IPAddressTextBox();
            this.BIA = new FVD.Common.IPAddressTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelBindingIPAddress
            // 
            this.LabelBindingIPAddress.AutoSize = true;
            this.LabelBindingIPAddress.Location = new System.Drawing.Point(40, 33);
            this.LabelBindingIPAddress.Name = "LabelBindingIPAddress";
            this.LabelBindingIPAddress.Size = new System.Drawing.Size(159, 15);
            this.LabelBindingIPAddress.TabIndex = 0;
            this.LabelBindingIPAddress.Text = "Binding IP Address:";
            // 
            // LabelRemoteIPAddress
            // 
            this.LabelRemoteIPAddress.AutoSize = true;
            this.LabelRemoteIPAddress.Location = new System.Drawing.Point(40, 66);
            this.LabelRemoteIPAddress.Name = "LabelRemoteIPAddress";
            this.LabelRemoteIPAddress.Size = new System.Drawing.Size(151, 15);
            this.LabelRemoteIPAddress.TabIndex = 1;
            this.LabelRemoteIPAddress.Text = "Remote IP Address:\r\n";
            // 
            // LabelBrowseMode
            // 
            this.LabelBrowseMode.AutoSize = true;
            this.LabelBrowseMode.Location = new System.Drawing.Point(425, 33);
            this.LabelBrowseMode.Name = "LabelBrowseMode";
            this.LabelBrowseMode.Size = new System.Drawing.Size(103, 15);
            this.LabelBrowseMode.TabIndex = 2;
            this.LabelBrowseMode.Text = "Browse Mode:";
            // 
            // LabelRemoteSubnetMask
            // 
            this.LabelRemoteSubnetMask.AutoSize = true;
            this.LabelRemoteSubnetMask.Location = new System.Drawing.Point(425, 66);
            this.LabelRemoteSubnetMask.Name = "LabelRemoteSubnetMask";
            this.LabelRemoteSubnetMask.Size = new System.Drawing.Size(159, 15);
            this.LabelRemoteSubnetMask.TabIndex = 3;
            this.LabelRemoteSubnetMask.Text = "Remote Subnet Mask:";
            // 
            // ComboBoxBrowseMode
            // 
            this.ComboBoxBrowseMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxBrowseMode.FormattingEnabled = true;
            this.ComboBoxBrowseMode.Items.AddRange(new object[] {
            "本地广播",
            "本地p2p",
            "远程"});
            this.ComboBoxBrowseMode.Location = new System.Drawing.Point(602, 33);
            this.ComboBoxBrowseMode.Name = "ComboBoxBrowseMode";
            this.ComboBoxBrowseMode.Size = new System.Drawing.Size(157, 23);
            this.ComboBoxBrowseMode.TabIndex = 4;
            this.ComboBoxBrowseMode.SelectedIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ButtonRemove);
            this.panel1.Controls.Add(this.ButtonAdd);
            this.panel1.Controls.Add(this.ListBoxPointToPointIPAddress);
            this.panel1.Location = new System.Drawing.Point(43, 121);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 426);
            this.panel1.TabIndex = 5;
            // 
            // ButtonRemove
            // 
            this.ButtonRemove.Location = new System.Drawing.Point(166, 64);
            this.ButtonRemove.Name = "ButtonRemove";
            this.ButtonRemove.Size = new System.Drawing.Size(75, 34);
            this.ButtonRemove.TabIndex = 2;
            this.ButtonRemove.Text = "Remove";
            this.ButtonRemove.UseVisualStyleBackColor = true;
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.Location = new System.Drawing.Point(166, 25);
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(70, 33);
            this.ButtonAdd.TabIndex = 1;
            this.ButtonAdd.Text = "Add";
            this.ButtonAdd.UseVisualStyleBackColor = true;
            // 
            // ListBoxPointToPointIPAddress
            // 
            this.ListBoxPointToPointIPAddress.FormattingEnabled = true;
            this.ListBoxPointToPointIPAddress.ItemHeight = 15;
            this.ListBoxPointToPointIPAddress.Location = new System.Drawing.Point(19, 64);
            this.ListBoxPointToPointIPAddress.Name = "ListBoxPointToPointIPAddress";
            this.ListBoxPointToPointIPAddress.Size = new System.Drawing.Size(137, 319);
            this.ListBoxPointToPointIPAddress.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.LabelDevicesNumber);
            this.panel2.Controls.Add(this.LabelDevicesFound);
            this.panel2.Controls.Add(this.ButtonClear);
            this.panel2.Controls.Add(this.ButtonStop);
            this.panel2.Controls.Add(this.ButtonStart);
            this.panel2.Controls.Add(this.ListBoxDevices);
            this.panel2.Location = new System.Drawing.Point(429, 121);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(329, 426);
            this.panel2.TabIndex = 6;
            // 
            // LabelDevicesNumber
            // 
            this.LabelDevicesNumber.AutoSize = true;
            this.LabelDevicesNumber.Location = new System.Drawing.Point(170, 395);
            this.LabelDevicesNumber.Name = "LabelDevicesNumber";
            this.LabelDevicesNumber.Size = new System.Drawing.Size(55, 15);
            this.LabelDevicesNumber.TabIndex = 5;
            this.LabelDevicesNumber.Text = "114514";
            // 
            // LabelDevicesFound
            // 
            this.LabelDevicesFound.AutoSize = true;
            this.LabelDevicesFound.Location = new System.Drawing.Point(17, 395);
            this.LabelDevicesFound.Name = "LabelDevicesFound";
            this.LabelDevicesFound.Size = new System.Drawing.Size(119, 15);
            this.LabelDevicesFound.TabIndex = 4;
            this.LabelDevicesFound.Text = "Devices Found:";
            // 
            // ButtonClear
            // 
            this.ButtonClear.Location = new System.Drawing.Point(231, 109);
            this.ButtonClear.Name = "ButtonClear";
            this.ButtonClear.Size = new System.Drawing.Size(75, 23);
            this.ButtonClear.TabIndex = 3;
            this.ButtonClear.Text = "Clear";
            this.ButtonClear.UseVisualStyleBackColor = true;
            this.ButtonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            // 
            // ButtonStop
            // 
            this.ButtonStop.Location = new System.Drawing.Point(231, 64);
            this.ButtonStop.Name = "ButtonStop";
            this.ButtonStop.Size = new System.Drawing.Size(75, 23);
            this.ButtonStop.TabIndex = 2;
            this.ButtonStop.Text = "Stop";
            this.ButtonStop.UseVisualStyleBackColor = true;
            this.ButtonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // ButtonStart
            // 
            this.ButtonStart.Location = new System.Drawing.Point(231, 25);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(75, 23);
            this.ButtonStart.TabIndex = 1;
            this.ButtonStart.Text = "Start";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // ListBoxDevices
            // 
            this.ListBoxDevices.FormattingEnabled = true;
            this.ListBoxDevices.ItemHeight = 15;
            this.ListBoxDevices.Location = new System.Drawing.Point(20, 27);
            this.ListBoxDevices.Name = "ListBoxDevices";
            this.ListBoxDevices.Size = new System.Drawing.Size(187, 349);
            this.ListBoxDevices.TabIndex = 0;
            // 
            // LabelPointToPoint
            // 
            this.LabelPointToPoint.AutoSize = true;
            this.LabelPointToPoint.Location = new System.Drawing.Point(59, 112);
            this.LabelPointToPoint.Name = "LabelPointToPoint";
            this.LabelPointToPoint.Size = new System.Drawing.Size(207, 15);
            this.LabelPointToPoint.TabIndex = 7;
            this.LabelPointToPoint.Text = "Point-to-Point IP Address";
            // 
            // LabelBrowse
            // 
            this.LabelBrowse.AutoSize = true;
            this.LabelBrowse.Location = new System.Drawing.Point(446, 112);
            this.LabelBrowse.Name = "LabelBrowse";
            this.LabelBrowse.Size = new System.Drawing.Size(55, 15);
            this.LabelBrowse.TabIndex = 8;
            this.LabelBrowse.Text = "Browse";
            // 
            // LabelStatus
            // 
            this.LabelStatus.AutoSize = true;
            this.LabelStatus.Location = new System.Drawing.Point(43, 569);
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(63, 15);
            this.LabelStatus.TabIndex = 9;
            this.LabelStatus.Text = "Status:";
            // 
            // LabelStatusText
            // 
            this.LabelStatusText.AutoSize = true;
            this.LabelStatusText.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LabelStatusText.Location = new System.Drawing.Point(152, 569);
            this.LabelStatusText.Name = "LabelStatusText";
            this.LabelStatusText.Size = new System.Drawing.Size(183, 15);
            this.LabelStatusText.TabIndex = 10;
            this.LabelStatusText.Text = "Browse is not started.";
            // 
            // RIA
            // 
            this.RIA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.RIA.Location = new System.Drawing.Point(203, 59);
            this.RIA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RIA.Name = "RIA";
            this.RIA.Size = new System.Drawing.Size(167, 35);
            this.RIA.TabIndex = 12;
            this.RIA.Value = ((System.Net.IPAddress)(resources.GetObject("RIA.Value")));
            // 
            // BIA
            // 
            this.BIA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.BIA.Location = new System.Drawing.Point(203, 27);
            this.BIA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BIA.Name = "BIA";
            this.BIA.Size = new System.Drawing.Size(167, 35);
            this.BIA.TabIndex = 11;
            this.BIA.Value = ((System.Net.IPAddress)(resources.GetObject("BIA.Value")));
            // 
            // DeviceExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 596);
            this.Controls.Add(this.RIA);
            this.Controls.Add(this.BIA);
            this.Controls.Add(this.LabelStatusText);
            this.Controls.Add(this.LabelStatus);
            this.Controls.Add(this.LabelBrowse);
            this.Controls.Add(this.LabelPointToPoint);
            this.Controls.Add(this.ComboBoxBrowseMode);
            this.Controls.Add(this.LabelRemoteSubnetMask);
            this.Controls.Add(this.LabelBrowseMode);
            this.Controls.Add(this.LabelRemoteIPAddress);
            this.Controls.Add(this.LabelBindingIPAddress);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeviceExplorer";
            this.Text = "Device Explorer";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelBindingIPAddress;
        private System.Windows.Forms.Label LabelRemoteIPAddress;
        private System.Windows.Forms.Label LabelBrowseMode;
        private System.Windows.Forms.Label LabelRemoteSubnetMask;
        private System.Windows.Forms.ComboBox ComboBoxBrowseMode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox ListBoxPointToPointIPAddress;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label LabelPointToPoint;
        private System.Windows.Forms.Button ButtonRemove;
        private System.Windows.Forms.Button ButtonAdd;
        private System.Windows.Forms.Label LabelDevicesNumber;
        private System.Windows.Forms.Label LabelDevicesFound;
        private System.Windows.Forms.Button ButtonClear;
        private System.Windows.Forms.Button ButtonStop;
        private System.Windows.Forms.Button ButtonStart;
        private System.Windows.Forms.ListBox ListBoxDevices;
        private System.Windows.Forms.Label LabelBrowse;
        private System.Windows.Forms.Label LabelStatus;
        private System.Windows.Forms.Label LabelStatusText;
        private FVD.Common.IPAddressTextBox BIA;
        private FVD.Common.IPAddressTextBox RIA;
    }
}

