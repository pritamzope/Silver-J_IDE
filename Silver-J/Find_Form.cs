#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="Find_Form.cs" company="">
  
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
    public partial class Find_Form : Form
    {
        TextEditorControl texteditor;
        public Find_Form(TextEditorControl tx)
        {
            InitializeComponent();
            texteditor = tx;
        }


        RichTextBox rtb = new RichTextBox();
        RichTextBox richTextBox1 = new RichTextBox();
        RichTextBox richTextBox2 = new RichTextBox();



        //**************************************************************************************************************
        //     GetLines()
        //**************************************************************************************************************
        void GetLines()
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            int i;
            rtb.Text = texteditor.Text;
            String s = textBox1.Text;
            String[] lines = rtb.Lines;
            for (i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(s))
                {
                    int b = i + 1;
                    richTextBox1.Text = richTextBox1.Text.Insert(0, "" + b.ToString() + "\n");
                }
            }
            String[] lines2 = richTextBox1.Lines;
            for (int j = 0; j < lines2.Length; j++)
            {
                if (lines2[j] == "") { }
                else
                {
                    richTextBox2.Text = richTextBox2.Text.Insert(0, "" + lines2[j] + "\n");
                }
            }
        }

        static int a = 0;


        //**************************************************************************************************************
        //      textBox1 Text Changed
        //**************************************************************************************************************
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            a = 0;
            label2.Text = "";
            if (textBox1.Text == "")
            {
                FindNextButton.Enabled = false;
            }
            else
            {
                FindNextButton.Enabled = true;
            }
        }



        //**************************************************************************************************************
        //      Next Button
        //**************************************************************************************************************
        private void FindNextButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                FindNextButton.Enabled = false;
            }
            else
            {
                FindNextButton.Enabled = true;
                try
                {
                    GetLines();
                    String[] lines = richTextBox2.Lines;
                    if (lines[a] == "")
                    {
                    }
                    else
                    {
                        int line = Convert.ToInt32(lines[a]);
                        texteditor.ActiveTextAreaControl.TextArea.Caret.Line = line - 1;
                        label2.Text = "Found at Line No : " + (line).ToString();
                        a = a + 1;
                    }
                }
                catch { }
            }
        }



        //**************************************************************************************************************
        //      Cancel Button
        //**************************************************************************************************************
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
            a = 0;
        }



        //**************************************************************************************************************
        //      Find_Form Closing
        //**************************************************************************************************************
        private void Find_Form_Closing(object sender, FormClosingEventArgs e)
        {
            a = 0;
        }
    }
}
