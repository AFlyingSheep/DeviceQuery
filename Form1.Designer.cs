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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceExplorer));
            this.LabelBindingIPAddress = new System.Windows.Forms.Label();
            this.LabelRemoteIPAddress = new System.Windows.Forms.Label();
            this.LabelBrowseMode = new System.Windows.Forms.Label();
            this.LabelRemoteSubnetMask = new System.Windows.Forms.Label();
            this.ComboBoxBrowseMode = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ipAddressTextBox1 = new FVD.Common.IPAddressTextBox();
            this.ButtonRemove = new System.Windows.Forms.Button();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.ListBoxPointToPointIPAddress = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.设备属性ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
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
            this.LabelTimeSet = new System.Windows.Forms.Label();
            this.TextBoxTimeSet = new System.Windows.Forms.TextBox();
            this.ButtonTimeSet = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.delayBox = new System.Windows.Forms.TextBox();
            this.DelayButtom = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.RSM = new FVD.Common.IPAddressTextBox();
            this.RIA = new FVD.Common.IPAddressTextBox();
            this.BIA = new FVD.Common.IPAddressTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
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
            this.LabelRemoteIPAddress.Location = new System.Drawing.Point(748, 25);
            this.LabelRemoteIPAddress.Name = "LabelRemoteIPAddress";
            this.LabelRemoteIPAddress.Size = new System.Drawing.Size(151, 15);
            this.LabelRemoteIPAddress.TabIndex = 1;
            this.LabelRemoteIPAddress.Text = "Remote IP Address:\r\n";
            // 
            // LabelBrowseMode
            // 
            this.LabelBrowseMode.AutoSize = true;
            this.LabelBrowseMode.Location = new System.Drawing.Point(426, 33);
            this.LabelBrowseMode.Name = "LabelBrowseMode";
            this.LabelBrowseMode.Size = new System.Drawing.Size(103, 15);
            this.LabelBrowseMode.TabIndex = 2;
            this.LabelBrowseMode.Text = "Browse Mode:";
            // 
            // LabelRemoteSubnetMask
            // 
            this.LabelRemoteSubnetMask.AutoSize = true;
            this.LabelRemoteSubnetMask.Location = new System.Drawing.Point(748, 55);
            this.LabelRemoteSubnetMask.Name = "LabelRemoteSubnetMask";
            this.LabelRemoteSubnetMask.Size = new System.Drawing.Size(159, 15);
            this.LabelRemoteSubnetMask.TabIndex = 3;
            this.LabelRemoteSubnetMask.Text = "Remote Subnet Mask:";
            // 
            // ComboBoxBrowseMode
            // 
            this.ComboBoxBrowseMode.Cursor = System.Windows.Forms.Cursors.Default;
            this.ComboBoxBrowseMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxBrowseMode.FormattingEnabled = true;
            this.ComboBoxBrowseMode.Items.AddRange(new object[] {
            "本地广播",
            "本地P2P访问",
            "远程"});
            this.ComboBoxBrowseMode.Location = new System.Drawing.Point(535, 30);
            this.ComboBoxBrowseMode.Name = "ComboBoxBrowseMode";
            this.ComboBoxBrowseMode.Size = new System.Drawing.Size(193, 23);
            this.ComboBoxBrowseMode.TabIndex = 4;
            this.ComboBoxBrowseMode.SelectedIndexChanged += new System.EventHandler(this.ComboBoxBrowseMode_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ipAddressTextBox1);
            this.panel1.Controls.Add(this.ButtonRemove);
            this.panel1.Controls.Add(this.ButtonAdd);
            this.panel1.Controls.Add(this.ListBoxPointToPointIPAddress);
            this.panel1.Location = new System.Drawing.Point(43, 121);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 426);
            this.panel1.TabIndex = 5;
            // 
            // ipAddressTextBox1
            // 
            this.ipAddressTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressTextBox1.Location = new System.Drawing.Point(16, 27);
            this.ipAddressTextBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ipAddressTextBox1.Name = "ipAddressTextBox1";
            this.ipAddressTextBox1.Size = new System.Drawing.Size(167, 35);
            this.ipAddressTextBox1.TabIndex = 13;
            this.ipAddressTextBox1.Value = ((System.Net.IPAddress)(resources.GetObject("ipAddressTextBox1.Value")));
            // 
            // ButtonRemove
            // 
            this.ButtonRemove.Location = new System.Drawing.Point(190, 69);
            this.ButtonRemove.Name = "ButtonRemove";
            this.ButtonRemove.Size = new System.Drawing.Size(70, 34);
            this.ButtonRemove.TabIndex = 2;
            this.ButtonRemove.Text = "Remove";
            this.ButtonRemove.UseVisualStyleBackColor = true;
            this.ButtonRemove.Click += new System.EventHandler(this.ButtonRemove_Click);
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.Location = new System.Drawing.Point(190, 27);
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(70, 35);
            this.ButtonAdd.TabIndex = 1;
            this.ButtonAdd.Text = "Add";
            this.ButtonAdd.UseVisualStyleBackColor = true;
            this.ButtonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // ListBoxPointToPointIPAddress
            // 
            this.ListBoxPointToPointIPAddress.FormattingEnabled = true;
            this.ListBoxPointToPointIPAddress.ItemHeight = 15;
            this.ListBoxPointToPointIPAddress.Location = new System.Drawing.Point(16, 69);
            this.ListBoxPointToPointIPAddress.Name = "ListBoxPointToPointIPAddress";
            this.ListBoxPointToPointIPAddress.Size = new System.Drawing.Size(163, 334);
            this.ListBoxPointToPointIPAddress.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Controls.Add(this.ButtonSave);
            this.panel2.Controls.Add(this.RefreshButton);
            this.panel2.Controls.Add(this.treeView1);
            this.panel2.Controls.Add(this.LabelDevicesNumber);
            this.panel2.Controls.Add(this.LabelDevicesFound);
            this.panel2.Controls.Add(this.ButtonClear);
            this.panel2.Controls.Add(this.ButtonStop);
            this.panel2.Controls.Add(this.ButtonStart);
            this.panel2.Controls.Add(this.ListBoxDevices);
            this.panel2.Location = new System.Drawing.Point(429, 121);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(697, 426);
            this.panel2.TabIndex = 6;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(526, 150);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(101, 19);
            this.checkBox1.TabIndex = 22;
            this.checkBox1.Text = "Auto find";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(526, 349);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(152, 34);
            this.ButtonSave.TabIndex = 18;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Enabled = false;
            this.RefreshButton.Location = new System.Drawing.Point(526, 68);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(152, 33);
            this.RefreshButton.TabIndex = 7;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(20, 27);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(499, 356);
            this.treeView1.TabIndex = 6;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设备属性ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(139, 28);
            // 
            // 设备属性ToolStripMenuItem
            // 
            this.设备属性ToolStripMenuItem.Name = "设备属性ToolStripMenuItem";
            this.设备属性ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.设备属性ToolStripMenuItem.Text = "设备属性";
            this.设备属性ToolStripMenuItem.Click += new System.EventHandler(this.设备属性ToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "主机");
            this.imageList1.Images.SetKeyName(1, "设备");
            // 
            // LabelDevicesNumber
            // 
            this.LabelDevicesNumber.AutoSize = true;
            this.LabelDevicesNumber.Location = new System.Drawing.Point(141, 396);
            this.LabelDevicesNumber.Name = "LabelDevicesNumber";
            this.LabelDevicesNumber.Size = new System.Drawing.Size(15, 15);
            this.LabelDevicesNumber.TabIndex = 5;
            this.LabelDevicesNumber.Text = "0";
            // 
            // LabelDevicesFound
            // 
            this.LabelDevicesFound.AutoSize = true;
            this.LabelDevicesFound.Location = new System.Drawing.Point(16, 396);
            this.LabelDevicesFound.Name = "LabelDevicesFound";
            this.LabelDevicesFound.Size = new System.Drawing.Size(119, 15);
            this.LabelDevicesFound.TabIndex = 4;
            this.LabelDevicesFound.Text = "Devices Found:";
            // 
            // ButtonClear
            // 
            this.ButtonClear.Location = new System.Drawing.Point(606, 110);
            this.ButtonClear.Name = "ButtonClear";
            this.ButtonClear.Size = new System.Drawing.Size(72, 34);
            this.ButtonClear.TabIndex = 3;
            this.ButtonClear.Text = "Clear";
            this.ButtonClear.UseVisualStyleBackColor = true;
            this.ButtonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            // 
            // ButtonStop
            // 
            this.ButtonStop.Enabled = false;
            this.ButtonStop.Location = new System.Drawing.Point(525, 109);
            this.ButtonStop.Name = "ButtonStop";
            this.ButtonStop.Size = new System.Drawing.Size(75, 35);
            this.ButtonStop.TabIndex = 2;
            this.ButtonStop.Text = "Stop";
            this.ButtonStop.UseVisualStyleBackColor = true;
            this.ButtonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // ButtonStart
            // 
            this.ButtonStart.Location = new System.Drawing.Point(525, 27);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(153, 35);
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
            this.ListBoxDevices.Size = new System.Drawing.Size(480, 289);
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
            // LabelTimeSet
            // 
            this.LabelTimeSet.AutoSize = true;
            this.LabelTimeSet.Location = new System.Drawing.Point(426, 81);
            this.LabelTimeSet.Name = "LabelTimeSet";
            this.LabelTimeSet.Size = new System.Drawing.Size(103, 15);
            this.LabelTimeSet.TabIndex = 15;
            this.LabelTimeSet.Text = "TimeSet(ms):";
            // 
            // TextBoxTimeSet
            // 
            this.TextBoxTimeSet.Location = new System.Drawing.Point(535, 78);
            this.TextBoxTimeSet.Name = "TextBoxTimeSet";
            this.TextBoxTimeSet.Size = new System.Drawing.Size(100, 25);
            this.TextBoxTimeSet.TabIndex = 16;
            this.TextBoxTimeSet.Text = "5000";
            // 
            // ButtonTimeSet
            // 
            this.ButtonTimeSet.Location = new System.Drawing.Point(653, 80);
            this.ButtonTimeSet.Name = "ButtonTimeSet";
            this.ButtonTimeSet.Size = new System.Drawing.Size(75, 23);
            this.ButtonTimeSet.TabIndex = 17;
            this.ButtonTimeSet.Text = "SetTime";
            this.ButtonTimeSet.UseVisualStyleBackColor = true;
            this.ButtonTimeSet.Click += new System.EventHandler(this.ButtonTimeSet_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(749, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "Maximum delay：";
            // 
            // delayBox
            // 
            this.delayBox.Location = new System.Drawing.Point(910, 86);
            this.delayBox.Name = "delayBox";
            this.delayBox.Size = new System.Drawing.Size(51, 25);
            this.delayBox.TabIndex = 19;
            this.delayBox.Text = "100";
            // 
            // DelayButtom
            // 
            this.DelayButtom.Location = new System.Drawing.Point(1006, 86);
            this.DelayButtom.Name = "DelayButtom";
            this.DelayButtom.Size = new System.Drawing.Size(71, 25);
            this.DelayButtom.TabIndex = 20;
            this.DelayButtom.Text = "Set";
            this.DelayButtom.UseVisualStyleBackColor = true;
            this.DelayButtom.Click += new System.EventHandler(this.DelayButtom_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(967, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 15);
            this.label2.TabIndex = 21;
            this.label2.Text = "ms";
            // 
            // RSM
            // 
            this.RSM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.RSM.Location = new System.Drawing.Point(910, 51);
            this.RSM.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RSM.Name = "RSM";
            this.RSM.Size = new System.Drawing.Size(167, 28);
            this.RSM.TabIndex = 14;
            this.RSM.Value = ((System.Net.IPAddress)(resources.GetObject("RSM.Value")));
            // 
            // RIA
            // 
            this.RIA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.RIA.Location = new System.Drawing.Point(910, 18);
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
            this.BIA.Size = new System.Drawing.Size(167, 29);
            this.BIA.TabIndex = 11;
            this.BIA.Value = ((System.Net.IPAddress)(resources.GetObject("BIA.Value")));
            // 
            // DeviceExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 594);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DelayButtom);
            this.Controls.Add(this.delayBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ButtonTimeSet);
            this.Controls.Add(this.TextBoxTimeSet);
            this.Controls.Add(this.LabelTimeSet);
            this.Controls.Add(this.RSM);
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
            this.Load += new System.EventHandler(this.DeviceExplorer_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
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
        private FVD.Common.IPAddressTextBox ipAddressTextBox1;
        private FVD.Common.IPAddressTextBox RSM;
        private Label LabelTimeSet;
        private TextBox TextBoxTimeSet;
        private Button ButtonTimeSet;
        private TreeView treeView1;
        private ImageList imageList1;
        private Button RefreshButton;
        private System.Windows.Forms.Timer timer1;
        private Button ButtonSave;
        private Label label1;
        private TextBox delayBox;
        private Button DelayButtom;
        private Label label2;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 设备属性ToolStripMenuItem;
        private CheckBox checkBox1;
    }
}

