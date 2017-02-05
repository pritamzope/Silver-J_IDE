#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="InsertEvents_Form.cs" company="">
  
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
    public partial class InsertEvents_Form : Form
    {
        TextEditorControl texteditor;
        public InsertEvents_Form(TextEditorControl tx)
        {
            InitializeComponent();
            texteditor = tx;
            richTextBox1.WordWrap = true;
        }
        RichTextBox richTextBox1 = new RichTextBox();

        private void Insertbutton_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem!=null)
            {
                richTextBox1.Text = "";
                ListBox.SelectedObjectCollection listselcoll = listBox1.SelectedItems;
                foreach(var item in listselcoll)
                {
                    richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, "\n"+item.ToString());
                }

                String[] lines = richTextBox1.Lines;
                int sel = texteditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                texteditor.ActiveTextAreaControl.TextArea.InsertString(richTextBox1.Text);
            }

            this.Close();
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
