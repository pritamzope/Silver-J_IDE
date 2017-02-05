#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="Options_Form.Designer.cs" company="">
  
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
    partial class Options_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options_Form));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ShowStartPageCheckBox = new System.Windows.Forms.CheckBox();
            this.ShowErrorDialogCheckBox = new System.Windows.Forms.CheckBox();
            this.MethodsViewCheckBox = new System.Windows.Forms.CheckBox();
            this.ErrorListCheckBox = new System.Windows.Forms.CheckBox();
            this.ToolStripCheckBox = new System.Windows.Forms.CheckBox();
            this.ClassesViewCheckBox = new System.Windows.Forms.CheckBox();
            this.ProjectExplorerCheckBox = new System.Windows.Forms.CheckBox();
            this.StatusStripCheckBox = new System.Windows.Forms.CheckBox();
            this.BottomRadioButton = new System.Windows.Forms.RadioButton();
            this.TopRadioButton = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SelectWebBrowserButton = new System.Windows.Forms.Button();
            this.WebBrowserTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SelectJDKButton = new System.Windows.Forms.Button();
            this.JDKPathTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.AutoCompileJavaCheckBox = new System.Windows.Forms.CheckBox();
            this.VisibleSpacesCheckBox = new System.Windows.Forms.CheckBox();
            this.EndOfLineMarkerCheckBox = new System.Windows.Forms.CheckBox();
            this.InvalidLinesCheckBox = new System.Windows.Forms.CheckBox();
            this.AutoCompleteBracesCheckBox = new System.Windows.Forms.CheckBox();
            this.BracesMatchingCheckBox = new System.Windows.Forms.CheckBox();
            this.LineHighlighterCheckBox = new System.Windows.Forms.CheckBox();
            this.LineNumbersCheckBox = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.FontSizeListBox = new System.Windows.Forms.ListBox();
            this.FontListBox = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.AutoCompleteCheckBox = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(1, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(14, 3);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(602, 324);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ShowStartPageCheckBox);
            this.tabPage1.Controls.Add(this.ShowErrorDialogCheckBox);
            this.tabPage1.Controls.Add(this.MethodsViewCheckBox);
            this.tabPage1.Controls.Add(this.ErrorListCheckBox);
            this.tabPage1.Controls.Add(this.ToolStripCheckBox);
            this.tabPage1.Controls.Add(this.ClassesViewCheckBox);
            this.tabPage1.Controls.Add(this.ProjectExplorerCheckBox);
            this.tabPage1.Controls.Add(this.StatusStripCheckBox);
            this.tabPage1.Controls.Add(this.BottomRadioButton);
            this.tabPage1.Controls.Add(this.TopRadioButton);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.SelectWebBrowserButton);
            this.tabPage1.Controls.Add(this.WebBrowserTextBox);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.SelectJDKButton);
            this.tabPage1.Controls.Add(this.JDKPathTextBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(594, 295);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ShowStartPageCheckBox
            // 
            this.ShowStartPageCheckBox.AutoSize = true;
            this.ShowStartPageCheckBox.Location = new System.Drawing.Point(359, 252);
            this.ShowStartPageCheckBox.Name = "ShowStartPageCheckBox";
            this.ShowStartPageCheckBox.Size = new System.Drawing.Size(174, 20);
            this.ShowStartPageCheckBox.TabIndex = 21;
            this.ShowStartPageCheckBox.Text = "Show Start Page on Start Up";
            this.ShowStartPageCheckBox.UseVisualStyleBackColor = true;
            // 
            // ShowErrorDialogCheckBox
            // 
            this.ShowErrorDialogCheckBox.AutoSize = true;
            this.ShowErrorDialogCheckBox.Location = new System.Drawing.Point(128, 253);
            this.ShowErrorDialogCheckBox.Name = "ShowErrorDialogCheckBox";
            this.ShowErrorDialogCheckBox.Size = new System.Drawing.Size(121, 20);
            this.ShowErrorDialogCheckBox.TabIndex = 20;
            this.ShowErrorDialogCheckBox.Text = "Show Error Dialog";
            this.ShowErrorDialogCheckBox.UseVisualStyleBackColor = true;
            // 
            // MethodsViewCheckBox
            // 
            this.MethodsViewCheckBox.AutoSize = true;
            this.MethodsViewCheckBox.Location = new System.Drawing.Point(359, 226);
            this.MethodsViewCheckBox.Name = "MethodsViewCheckBox";
            this.MethodsViewCheckBox.Size = new System.Drawing.Size(102, 20);
            this.MethodsViewCheckBox.TabIndex = 19;
            this.MethodsViewCheckBox.Text = "Methods View";
            this.MethodsViewCheckBox.UseVisualStyleBackColor = true;
            // 
            // ErrorListCheckBox
            // 
            this.ErrorListCheckBox.AutoSize = true;
            this.ErrorListCheckBox.Location = new System.Drawing.Point(359, 200);
            this.ErrorListCheckBox.Name = "ErrorListCheckBox";
            this.ErrorListCheckBox.Size = new System.Drawing.Size(78, 20);
            this.ErrorListCheckBox.TabIndex = 18;
            this.ErrorListCheckBox.Text = "Errors List";
            this.ErrorListCheckBox.UseVisualStyleBackColor = true;
            // 
            // ToolStripCheckBox
            // 
            this.ToolStripCheckBox.AutoSize = true;
            this.ToolStripCheckBox.Location = new System.Drawing.Point(359, 174);
            this.ToolStripCheckBox.Name = "ToolStripCheckBox";
            this.ToolStripCheckBox.Size = new System.Drawing.Size(74, 20);
            this.ToolStripCheckBox.TabIndex = 17;
            this.ToolStripCheckBox.Text = "ToolStrip";
            this.ToolStripCheckBox.UseVisualStyleBackColor = true;
            // 
            // ClassesViewCheckBox
            // 
            this.ClassesViewCheckBox.AutoSize = true;
            this.ClassesViewCheckBox.Location = new System.Drawing.Point(128, 226);
            this.ClassesViewCheckBox.Name = "ClassesViewCheckBox";
            this.ClassesViewCheckBox.Size = new System.Drawing.Size(92, 20);
            this.ClassesViewCheckBox.TabIndex = 16;
            this.ClassesViewCheckBox.Text = "Classes View";
            this.ClassesViewCheckBox.UseVisualStyleBackColor = true;
            // 
            // ProjectExplorerCheckBox
            // 
            this.ProjectExplorerCheckBox.AutoSize = true;
            this.ProjectExplorerCheckBox.Location = new System.Drawing.Point(128, 200);
            this.ProjectExplorerCheckBox.Name = "ProjectExplorerCheckBox";
            this.ProjectExplorerCheckBox.Size = new System.Drawing.Size(110, 20);
            this.ProjectExplorerCheckBox.TabIndex = 15;
            this.ProjectExplorerCheckBox.Text = "Project Explorer";
            this.ProjectExplorerCheckBox.UseVisualStyleBackColor = true;
            // 
            // StatusStripCheckBox
            // 
            this.StatusStripCheckBox.AutoSize = true;
            this.StatusStripCheckBox.Location = new System.Drawing.Point(128, 174);
            this.StatusStripCheckBox.Name = "StatusStripCheckBox";
            this.StatusStripCheckBox.Size = new System.Drawing.Size(83, 20);
            this.StatusStripCheckBox.TabIndex = 14;
            this.StatusStripCheckBox.Text = "StatusStrip";
            this.StatusStripCheckBox.UseVisualStyleBackColor = true;
            // 
            // BottomRadioButton
            // 
            this.BottomRadioButton.AutoSize = true;
            this.BottomRadioButton.Location = new System.Drawing.Point(368, 124);
            this.BottomRadioButton.Name = "BottomRadioButton";
            this.BottomRadioButton.Size = new System.Drawing.Size(65, 20);
            this.BottomRadioButton.TabIndex = 13;
            this.BottomRadioButton.TabStop = true;
            this.BottomRadioButton.Text = "Bottom";
            this.BottomRadioButton.UseVisualStyleBackColor = true;
            // 
            // TopRadioButton
            // 
            this.TopRadioButton.AutoSize = true;
            this.TopRadioButton.Location = new System.Drawing.Point(194, 124);
            this.TopRadioButton.Name = "TopRadioButton";
            this.TopRadioButton.Size = new System.Drawing.Size(46, 20);
            this.TopRadioButton.TabIndex = 12;
            this.TopRadioButton.TabStop = true;
            this.TopRadioButton.Text = "Top";
            this.TopRadioButton.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Tabs Alignment : ";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Location = new System.Drawing.Point(0, 118);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(594, 1);
            this.panel3.TabIndex = 10;
            // 
            // SelectWebBrowserButton
            // 
            this.SelectWebBrowserButton.Location = new System.Drawing.Point(513, 86);
            this.SelectWebBrowserButton.Name = "SelectWebBrowserButton";
            this.SelectWebBrowserButton.Size = new System.Drawing.Size(75, 23);
            this.SelectWebBrowserButton.TabIndex = 9;
            this.SelectWebBrowserButton.Text = "Select";
            this.SelectWebBrowserButton.UseVisualStyleBackColor = true;
            this.SelectWebBrowserButton.Click += new System.EventHandler(this.SelectWebBrowserButton_Click);
            // 
            // WebBrowserTextBox
            // 
            this.WebBrowserTextBox.Location = new System.Drawing.Point(99, 86);
            this.WebBrowserTextBox.Name = "WebBrowserTextBox";
            this.WebBrowserTextBox.Size = new System.Drawing.Size(407, 21);
            this.WebBrowserTextBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Web Browser : ";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Location = new System.Drawing.Point(1, 76);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(593, 1);
            this.panel2.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Location = new System.Drawing.Point(1, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(591, 1);
            this.panel1.TabIndex = 5;
            // 
            // SelectJDKButton
            // 
            this.SelectJDKButton.Location = new System.Drawing.Point(513, 45);
            this.SelectJDKButton.Name = "SelectJDKButton";
            this.SelectJDKButton.Size = new System.Drawing.Size(75, 23);
            this.SelectJDKButton.TabIndex = 4;
            this.SelectJDKButton.Text = "Select";
            this.SelectJDKButton.UseVisualStyleBackColor = true;
            this.SelectJDKButton.Click += new System.EventHandler(this.SelectJDKButton_Click);
            // 
            // JDKPathTextBox
            // 
            this.JDKPathTextBox.Location = new System.Drawing.Point(76, 45);
            this.JDKPathTextBox.Name = "JDKPathTextBox";
            this.JDKPathTextBox.Size = new System.Drawing.Size(431, 21);
            this.JDKPathTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "JDK Path : ";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Default",
            "System",
            "Light",
            "Dark",
            "Night"});
            this.comboBox1.Location = new System.Drawing.Point(238, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(172, 24);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Appearance : ";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.AutoCompleteCheckBox);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.AutoCompileJavaCheckBox);
            this.tabPage2.Controls.Add(this.VisibleSpacesCheckBox);
            this.tabPage2.Controls.Add(this.EndOfLineMarkerCheckBox);
            this.tabPage2.Controls.Add(this.InvalidLinesCheckBox);
            this.tabPage2.Controls.Add(this.AutoCompleteBracesCheckBox);
            this.tabPage2.Controls.Add(this.BracesMatchingCheckBox);
            this.tabPage2.Controls.Add(this.LineHighlighterCheckBox);
            this.tabPage2.Controls.Add(this.LineNumbersCheckBox);
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.FontSizeListBox);
            this.tabPage2.Controls.Add(this.FontListBox);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(594, 295);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Editor";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(3, 276);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(173, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "You must Re Open your Project";
            // 
            // AutoCompileJavaCheckBox
            // 
            this.AutoCompileJavaCheckBox.AutoSize = true;
            this.AutoCompileJavaCheckBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AutoCompileJavaCheckBox.Location = new System.Drawing.Point(365, 227);
            this.AutoCompileJavaCheckBox.Name = "AutoCompileJavaCheckBox";
            this.AutoCompileJavaCheckBox.Size = new System.Drawing.Size(147, 20);
            this.AutoCompileJavaCheckBox.TabIndex = 19;
            this.AutoCompileJavaCheckBox.Text = "Auto Compile Program";
            this.AutoCompileJavaCheckBox.UseVisualStyleBackColor = true;
            // 
            // VisibleSpacesCheckBox
            // 
            this.VisibleSpacesCheckBox.AutoSize = true;
            this.VisibleSpacesCheckBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VisibleSpacesCheckBox.Location = new System.Drawing.Point(116, 228);
            this.VisibleSpacesCheckBox.Name = "VisibleSpacesCheckBox";
            this.VisibleSpacesCheckBox.Size = new System.Drawing.Size(100, 20);
            this.VisibleSpacesCheckBox.TabIndex = 18;
            this.VisibleSpacesCheckBox.Text = "Visible Spaces";
            this.VisibleSpacesCheckBox.UseVisualStyleBackColor = true;
            // 
            // EndOfLineMarkerCheckBox
            // 
            this.EndOfLineMarkerCheckBox.AutoSize = true;
            this.EndOfLineMarkerCheckBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndOfLineMarkerCheckBox.Location = new System.Drawing.Point(365, 201);
            this.EndOfLineMarkerCheckBox.Name = "EndOfLineMarkerCheckBox";
            this.EndOfLineMarkerCheckBox.Size = new System.Drawing.Size(126, 20);
            this.EndOfLineMarkerCheckBox.TabIndex = 17;
            this.EndOfLineMarkerCheckBox.Text = "End of Line Marker";
            this.EndOfLineMarkerCheckBox.UseVisualStyleBackColor = true;
            // 
            // InvalidLinesCheckBox
            // 
            this.InvalidLinesCheckBox.AutoSize = true;
            this.InvalidLinesCheckBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InvalidLinesCheckBox.Location = new System.Drawing.Point(116, 201);
            this.InvalidLinesCheckBox.Name = "InvalidLinesCheckBox";
            this.InvalidLinesCheckBox.Size = new System.Drawing.Size(92, 20);
            this.InvalidLinesCheckBox.TabIndex = 16;
            this.InvalidLinesCheckBox.Text = "Invalid Lines";
            this.InvalidLinesCheckBox.UseVisualStyleBackColor = true;
            // 
            // AutoCompleteBracesCheckBox
            // 
            this.AutoCompleteBracesCheckBox.AutoSize = true;
            this.AutoCompleteBracesCheckBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AutoCompleteBracesCheckBox.Location = new System.Drawing.Point(365, 174);
            this.AutoCompleteBracesCheckBox.Name = "AutoCompleteBracesCheckBox";
            this.AutoCompleteBracesCheckBox.Size = new System.Drawing.Size(143, 20);
            this.AutoCompleteBracesCheckBox.TabIndex = 15;
            this.AutoCompleteBracesCheckBox.Text = "Auto Complete Braces";
            this.AutoCompleteBracesCheckBox.UseVisualStyleBackColor = true;
            // 
            // BracesMatchingCheckBox
            // 
            this.BracesMatchingCheckBox.AutoSize = true;
            this.BracesMatchingCheckBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BracesMatchingCheckBox.Location = new System.Drawing.Point(116, 174);
            this.BracesMatchingCheckBox.Name = "BracesMatchingCheckBox";
            this.BracesMatchingCheckBox.Size = new System.Drawing.Size(115, 20);
            this.BracesMatchingCheckBox.TabIndex = 14;
            this.BracesMatchingCheckBox.Text = "Braces Matching";
            this.BracesMatchingCheckBox.UseVisualStyleBackColor = true;
            // 
            // LineHighlighterCheckBox
            // 
            this.LineHighlighterCheckBox.AutoSize = true;
            this.LineHighlighterCheckBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LineHighlighterCheckBox.Location = new System.Drawing.Point(365, 148);
            this.LineHighlighterCheckBox.Name = "LineHighlighterCheckBox";
            this.LineHighlighterCheckBox.Size = new System.Drawing.Size(112, 20);
            this.LineHighlighterCheckBox.TabIndex = 13;
            this.LineHighlighterCheckBox.Text = "Line Highlighter";
            this.LineHighlighterCheckBox.UseVisualStyleBackColor = true;
            // 
            // LineNumbersCheckBox
            // 
            this.LineNumbersCheckBox.AutoSize = true;
            this.LineNumbersCheckBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LineNumbersCheckBox.Location = new System.Drawing.Point(116, 148);
            this.LineNumbersCheckBox.Name = "LineNumbersCheckBox";
            this.LineNumbersCheckBox.Size = new System.Drawing.Size(100, 20);
            this.LineNumbersCheckBox.TabIndex = 12;
            this.LineNumbersCheckBox.Text = "Line Numbers";
            this.LineNumbersCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Silver;
            this.panel4.Location = new System.Drawing.Point(0, 128);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(594, 1);
            this.panel4.TabIndex = 3;
            // 
            // FontSizeListBox
            // 
            this.FontSizeListBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FontSizeListBox.FormattingEnabled = true;
            this.FontSizeListBox.ItemHeight = 17;
            this.FontSizeListBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "105",
            "106",
            "107",
            "108",
            "109",
            "110",
            "111",
            "112",
            "113",
            "114",
            "115",
            "116",
            "117",
            "118",
            "119",
            "120"});
            this.FontSizeListBox.Location = new System.Drawing.Point(378, 6);
            this.FontSizeListBox.Name = "FontSizeListBox";
            this.FontSizeListBox.Size = new System.Drawing.Size(87, 106);
            this.FontSizeListBox.TabIndex = 2;
            // 
            // FontListBox
            // 
            this.FontListBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FontListBox.FormattingEnabled = true;
            this.FontListBox.ItemHeight = 17;
            this.FontListBox.Items.AddRange(new object[] {
            "Agency FB",
            "Alaska",
            "Algerian",
            "Almonte",
            "Arial",
            "Arial Black",
            "Arial Narrow",
            "Baskerville Old Face",
            "Biting My Nails",
            "Book Antique",
            "Calibri",
            "Castellar",
            "Century",
            "Constantia",
            "Goudy Old Style",
            "Javanese Text",
            "Kruti Dev 040 Wide",
            "Lucida",
            "Lucida Bright",
            "Lucida Handwriting",
            "Lucida Console",
            "Lucida Fax",
            "Magneto",
            "Microsoft Sans Serif",
            "Microsoft YaHei UI",
            "Modern No. 20",
            "Modern",
            "Monospaced",
            "MS Outlook",
            "MT Extra",
            "Segoe UI",
            "SansSerif",
            "Serif",
            "Stencil",
            "Wide Latin "});
            this.FontListBox.Location = new System.Drawing.Point(116, 6);
            this.FontListBox.Name = "FontListBox";
            this.FontListBox.Size = new System.Drawing.Size(220, 106);
            this.FontListBox.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Font  :  ";
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(443, 336);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(524, 336);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // AutoCompleteCheckBox
            // 
            this.AutoCompleteCheckBox.AutoSize = true;
            this.AutoCompleteCheckBox.Location = new System.Drawing.Point(365, 253);
            this.AutoCompleteCheckBox.Name = "AutoCompleteCheckBox";
            this.AutoCompleteCheckBox.Size = new System.Drawing.Size(117, 20);
            this.AutoCompleteCheckBox.TabIndex = 21;
            this.AutoCompleteCheckBox.Text = "Auto Completion";
            this.AutoCompleteCheckBox.UseVisualStyleBackColor = true;
            // 
            // Options_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 371);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options_Form";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Form_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox JDKPathTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SelectJDKButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SelectWebBrowserButton;
        private System.Windows.Forms.TextBox WebBrowserTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton BottomRadioButton;
        private System.Windows.Forms.RadioButton TopRadioButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox ShowErrorDialogCheckBox;
        private System.Windows.Forms.CheckBox MethodsViewCheckBox;
        private System.Windows.Forms.CheckBox ErrorListCheckBox;
        private System.Windows.Forms.CheckBox ToolStripCheckBox;
        private System.Windows.Forms.CheckBox ClassesViewCheckBox;
        private System.Windows.Forms.CheckBox ProjectExplorerCheckBox;
        private System.Windows.Forms.CheckBox StatusStripCheckBox;
        private System.Windows.Forms.CheckBox ShowStartPageCheckBox;
        private System.Windows.Forms.ListBox FontSizeListBox;
        private System.Windows.Forms.ListBox FontListBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox AutoCompileJavaCheckBox;
        private System.Windows.Forms.CheckBox VisibleSpacesCheckBox;
        private System.Windows.Forms.CheckBox EndOfLineMarkerCheckBox;
        private System.Windows.Forms.CheckBox InvalidLinesCheckBox;
        private System.Windows.Forms.CheckBox AutoCompleteBracesCheckBox;
        private System.Windows.Forms.CheckBox BracesMatchingCheckBox;
        private System.Windows.Forms.CheckBox LineHighlighterCheckBox;
        private System.Windows.Forms.CheckBox LineNumbersCheckBox;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox AutoCompleteCheckBox;

    }
}