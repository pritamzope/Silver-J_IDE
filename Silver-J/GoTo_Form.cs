#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="GoTo_Form.cs" company="">
  
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
using ICSharpCode.TextEditor;
namespace Silver_J
{
    public partial class GoTo_Form : Form
    {
        TextEditorControl texteditor;
        public GoTo_Form(TextEditorControl tx)
        {
            InitializeComponent();
            texteditor = tx;
        }


        private void GoTo_Form_Load(object sender, EventArgs e)
        {
            RichTextBox rtb = new RichTextBox();
            rtb.Text = texteditor.Text;
            int lines = rtb.Lines.Length;
            label1.Text = "Enter Line Number (1-" + lines.ToString() + ") :";

            int sel = texteditor.ActiveTextAreaControl.TextArea.Caret.Line + 1;
            textBox1.Text = sel.ToString();
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int sel;
            RichTextBox rtb = new RichTextBox();
            rtb.Text = texteditor.Text;
            int lines = rtb.Lines.Length;

            if (textBox1.Text == "")
            {
                GoToButton.Enabled = false;
            }
            else if (!int.TryParse(textBox1.Text, out sel))
            {
                GoToButton.Enabled = false;
            }
            else if (Int32.Parse(textBox1.Text) > rtb.Lines.Length)
            {
                GoToButton.Enabled = false;
            }
            else if (textBox1.Text == "0")
            {
                GoToButton.Enabled = false;
            }
            else
            {
                GoToButton.Enabled = true;
            }
        }




        private void GoToButton_Click(object sender, EventArgs e)
        {
            int line = Int32.Parse(textBox1.Text);
            texteditor.ActiveTextAreaControl.TextArea.Caret.Line = line - 1;
            texteditor.ActiveTextAreaControl.TextArea.ScrollToCaret();
            this.Close();
        }




        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
