#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="New_JavaAppletProject_Form.Designer.cs" company="">
  
 {-  Program Name = Silver-J
      An Integrated Development Environment(IDE) for Java Programming
      Language written In C#   -}  
 
   This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
  
   Please credit me if you reuse, don't sell it under your own name, don't pretend you're me
  </copyright>
  * ****************************************************************************************/
#endregion

namespace Silver_J
{
    partial class New_JavaAppletProject_Form
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(New_JavaAppletProject_Form));
            this.labelZ1 = new Silver_J.LabelZ();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.Helpbutton = new System.Windows.Forms.Button();
            this.Cancelbutton = new System.Windows.Forms.Button();
            this.Finishbutton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.JavaClassTextBox = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Browsebutton = new System.Windows.Forms.Button();
            this.ProjectLocationTextBox = new System.Windows.Forms.TextBox();
            this.ProjectNameTextBox = new System.Windows.Forms.TextBox();
            this.HTMLFileTextBox = new System.Windows.Forms.TextBox();
            this.projectfolderlabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.errorlabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelZ1
            // 
            this.labelZ1.AutoSize = true;
            this.labelZ1.BackColor = System.Drawing.Color.Transparent;
            this.labelZ1.DisplayText = "Applet";
            this.labelZ1.EndColor = System.Drawing.Color.DarkBlue;
            this.labelZ1.Font = new System.Drawing.Font("Microsoft YaHei UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelZ1.ForeColor = System.Drawing.Color.Transparent;
            this.labelZ1.GradientAngle = 45;
            this.labelZ1.Location = new System.Drawing.Point(1, 0);
            this.labelZ1.Name = "labelZ1";
            this.labelZ1.Size = new System.Drawing.Size(110, 38);
            this.labelZ1.StartColor = System.Drawing.Color.Red;
            this.labelZ1.TabIndex = 0;
            this.labelZ1.Text = "Applet";
            this.labelZ1.Transparent1 = 255;
            this.labelZ1.Transparent2 = 255;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Tag = "";
            // 
            // Helpbutton
            // 
            this.Helpbutton.Location = new System.Drawing.Point(575, 297);
            this.Helpbutton.Name = "Helpbutton";
            this.Helpbutton.Size = new System.Drawing.Size(75, 25);
            this.Helpbutton.TabIndex = 31;
            this.Helpbutton.Text = "Help";
            this.toolTip1.SetToolTip(this.Helpbutton, "Help");
            this.Helpbutton.UseVisualStyleBackColor = true;
            this.Helpbutton.Click += new System.EventHandler(this.Helpbutton_Click);
            // 
            // Cancelbutton
            // 
            this.Cancelbutton.Location = new System.Drawing.Point(494, 297);
            this.Cancelbutton.Name = "Cancelbutton";
            this.Cancelbutton.Size = new System.Drawing.Size(75, 25);
            this.Cancelbutton.TabIndex = 30;
            this.Cancelbutton.Text = "Cancel";
            this.toolTip1.SetToolTip(this.Cancelbutton, "Cancel");
            this.Cancelbutton.UseVisualStyleBackColor = true;
            this.Cancelbutton.Click += new System.EventHandler(this.Cancelbutton_Click);
            // 
            // Finishbutton
            // 
            this.Finishbutton.Enabled = false;
            this.Finishbutton.Location = new System.Drawing.Point(413, 297);
            this.Finishbutton.Name = "Finishbutton";
            this.Finishbutton.Size = new System.Drawing.Size(75, 25);
            this.Finishbutton.TabIndex = 29;
            this.Finishbutton.Text = "Finish";
            this.toolTip1.SetToolTip(this.Finishbutton, "Finish");
            this.Finishbutton.UseVisualStyleBackColor = true;
            this.Finishbutton.Click += new System.EventHandler(this.Finishbutton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Location = new System.Drawing.Point(0, 280);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(662, 1);
            this.panel1.TabIndex = 28;
            // 
            // JavaClassTextBox
            // 
            this.JavaClassTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JavaClassTextBox.Location = new System.Drawing.Point(143, 233);
            this.JavaClassTextBox.Name = "JavaClassTextBox";
            this.JavaClassTextBox.Size = new System.Drawing.Size(507, 23);
            this.JavaClassTextBox.TabIndex = 27;
            this.toolTip1.SetToolTip(this.JavaClassTextBox, "Enter Class Name");
            this.JavaClassTextBox.TextChanged += new System.EventHandler(this.JavaClassTextBox_TextChanged);
            this.JavaClassTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.JavaClassTextBox_KeyDown);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(8, 235);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(128, 21);
            this.checkBox1.TabIndex = 26;
            this.checkBox1.Text = "Create Java Class";
            this.toolTip1.SetToolTip(this.checkBox1, "Create and add Java Class to Project");
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Browsebutton
            // 
            this.Browsebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Browsebutton.Location = new System.Drawing.Point(564, 131);
            this.Browsebutton.Name = "Browsebutton";
            this.Browsebutton.Size = new System.Drawing.Size(86, 24);
            this.Browsebutton.TabIndex = 23;
            this.Browsebutton.Text = "Browse";
            this.toolTip1.SetToolTip(this.Browsebutton, "Select Folder");
            this.Browsebutton.UseVisualStyleBackColor = true;
            this.Browsebutton.Click += new System.EventHandler(this.Browsebutton_Click);
            // 
            // ProjectLocationTextBox
            // 
            this.ProjectLocationTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectLocationTextBox.Location = new System.Drawing.Point(123, 131);
            this.ProjectLocationTextBox.Name = "ProjectLocationTextBox";
            this.ProjectLocationTextBox.Size = new System.Drawing.Size(435, 23);
            this.ProjectLocationTextBox.TabIndex = 22;
            this.toolTip1.SetToolTip(this.ProjectLocationTextBox, "Enter Project Location Folder");
            this.ProjectLocationTextBox.TextChanged += new System.EventHandler(this.ProjectLocationTextBox_TextChanged);
            // 
            // ProjectNameTextBox
            // 
            this.ProjectNameTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectNameTextBox.Location = new System.Drawing.Point(123, 98);
            this.ProjectNameTextBox.Name = "ProjectNameTextBox";
            this.ProjectNameTextBox.Size = new System.Drawing.Size(435, 23);
            this.ProjectNameTextBox.TabIndex = 19;
            this.toolTip1.SetToolTip(this.ProjectNameTextBox, "Enter Project Name");
            this.ProjectNameTextBox.TextChanged += new System.EventHandler(this.ProjectNameTextBox_TextChanged);
            this.ProjectNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProjectNameTextBox_KeyDown);
            // 
            // HTMLFileTextBox
            // 
            this.HTMLFileTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HTMLFileTextBox.Location = new System.Drawing.Point(123, 195);
            this.HTMLFileTextBox.Name = "HTMLFileTextBox";
            this.HTMLFileTextBox.Size = new System.Drawing.Size(431, 23);
            this.HTMLFileTextBox.TabIndex = 33;
            this.toolTip1.SetToolTip(this.HTMLFileTextBox, "Enter HTML File Name");
            this.HTMLFileTextBox.TextChanged += new System.EventHandler(this.HTMLFileTextBox_TextChanged);
            this.HTMLFileTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HTMLFileTextBox_KeyDown);
            // 
            // projectfolderlabel
            // 
            this.projectfolderlabel.AutoSize = true;
            this.projectfolderlabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectfolderlabel.Location = new System.Drawing.Point(120, 165);
            this.projectfolderlabel.Name = "projectfolderlabel";
            this.projectfolderlabel.Size = new System.Drawing.Size(70, 17);
            this.projectfolderlabel.TabIndex = 25;
            this.projectfolderlabel.Text = "No Project";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 17);
            this.label3.TabIndex = 21;
            this.label3.Text = "Project Location : ";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.labelZ1);
            this.panel2.Controls.Add(this.errorlabel);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(662, 75);
            this.panel2.TabIndex = 20;
            // 
            // errorlabel
            // 
            this.errorlabel.AutoSize = true;
            this.errorlabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorlabel.ForeColor = System.Drawing.Color.Red;
            this.errorlabel.Location = new System.Drawing.Point(355, 39);
            this.errorlabel.Name = "errorlabel";
            this.errorlabel.Size = new System.Drawing.Size(0, 16);
            this.errorlabel.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(4, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Create New Java Applet Project";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 17);
            this.label4.TabIndex = 24;
            this.label4.Text = "Project Folder : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 18;
            this.label2.Text = "Project Name : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 17);
            this.label5.TabIndex = 32;
            this.label5.Text = "HTML File Name : ";
            // 
            // New_JavaAppletProject_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 334);
            this.Controls.Add(this.HTMLFileTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Helpbutton);
            this.Controls.Add(this.Cancelbutton);
            this.Controls.Add(this.Finishbutton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.JavaClassTextBox);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.projectfolderlabel);
            this.Controls.Add(this.Browsebutton);
            this.Controls.Add(this.ProjectLocationTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ProjectNameTextBox);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "New_JavaAppletProject_Form";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Java Applet Project";
            this.Load += new System.EventHandler(this.New_JavaAppletProject_Form_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LabelZ labelZ1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button Helpbutton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button Cancelbutton;
        private System.Windows.Forms.Button Finishbutton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox JavaClassTextBox;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button Browsebutton;
        private System.Windows.Forms.TextBox ProjectLocationTextBox;
        private System.Windows.Forms.TextBox ProjectNameTextBox;
        private System.Windows.Forms.Label projectfolderlabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label errorlabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox HTMLFileTextBox;
    }
}