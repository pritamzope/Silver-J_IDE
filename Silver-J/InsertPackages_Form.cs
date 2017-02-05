#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="InsertPackages_Form.cs" company="">
  
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
using System.Xml;
using ICSharpCode.TextEditor;
namespace Silver_J
{
    public partial class InsertPackages_Form : Form
    {
        TextEditorControl texteditor;
        public InsertPackages_Form(TextEditorControl tx)
        {
            InitializeComponent();
            texteditor = tx;
        }

        RichTextBox richTextBox1 = new RichTextBox();

        private void InsertPackages_Form_Load(object sender, EventArgs e)
        {
            List<String> mylist = new List<String> { };
            String defaultprojfilepath = Application.StartupPath + "\\files\\jpackages.slvjfile";
            using (XmlReader reader = XmlReader.Create(defaultprojfilepath))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "JPackage":
                                mylist.Add(reader.ReadString());
                                break;
                        }
                    }
                }
            }

            foreach(String item in mylist)
            {
                listBox1.Items.Add("          "+item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                richTextBox1.Text = "";
                ListBox.SelectedObjectCollection listselcoll = listBox1.SelectedItems;
                foreach (var item in listselcoll)
                {
                    String str = item.ToString().Trim();
                    richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, "\nimport " + str+".*;");
                }

                texteditor.ActiveTextAreaControl.TextArea.InsertString(richTextBox1.Text);
            }

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
