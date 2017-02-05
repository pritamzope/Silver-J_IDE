#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="Replace_Form.cs" company="">
  
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
namespace Silver_J
{
    public partial class Replace_Form : Form
    {
        TextEditorControl texteditor;
        public Replace_Form(TextEditorControl tx)
        {
            InitializeComponent();
            texteditor = tx;
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                ReplaceAllButton.Enabled = false;
            }
            else
            {
                ReplaceAllButton.Enabled = true;
            }
        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                ReplaceAllButton.Enabled = false;
            }
            else
            {
                ReplaceAllButton.Enabled = true;
            }
        }



        private void ReplaceAllButton_Click(object sender, EventArgs e)
        {
            String findtext = textBox1.Text;
            String replacetext = textBox2.Text;
            if (findtext != "" && replacetext != "")
            {
                RichTextBox rtb = new RichTextBox();
                rtb.Text = texteditor.Text;
                rtb.Text = rtb.Text.Replace(findtext, replacetext);
                texteditor.Text = rtb.Text;
            }
        }



        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void Replace_Form_Load(object sender, EventArgs e)
        {
            ReplaceAllButton.Enabled = false;
        }
    }
}
