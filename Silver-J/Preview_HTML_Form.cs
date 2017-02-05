#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="Preview_HTML_Form.cs" company="">
  
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Silver_J
{
    public partial class Preview_HTML_Form : Form
    {
        TabPage tabpage;
        MainForm F;
        String htmlfile;
        public Preview_HTML_Form(TabPage tb, MainForm frm, String otherhtmlfilename)
        {
            InitializeComponent();
            tabpage = tb;
            F = frm;
            htmlfile = otherhtmlfilename;
        }



        private void Reloadbutton_Click(object sender, EventArgs e)
        {
            if(FileTextBox.Text!="")
            {
                if(FileTextBox.Text.Contains(".html"))
                {
                    webBrowser1.Navigate(FileTextBox.Text);
                }
            }
        }


        private void Preview_HTML_Form_Load_1(object sender, EventArgs e)
        {
            if (F.Text == "Silver-J")
            {
                if (tabpage.Text.Contains(".html"))
                {
                    webBrowser1.Navigate(htmlfile);
                }
            }
            else
            {
                String s = File.ReadAllText(Application.StartupPath + "\\files\\files.slvjfile");
                RichTextBox rtb = new RichTextBox();
                rtb.Text = s;
                String[] lines = rtb.Lines;

                foreach (String line in lines)
                {
                    if (line != "")
                    {
                        if (line.Contains(tabpage.Text))
                        {
                            webBrowser1.Navigate(line);
                        }
                    }
                }
            }
        }



    }
}
