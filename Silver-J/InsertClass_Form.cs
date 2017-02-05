#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="InsertClass_Form.cs" company="">
  
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
    public partial class InsertClass_Form : Form
    {
        TextEditorControl texteditor;
        public InsertClass_Form(TextEditorControl tx)
        {
            InitializeComponent();
            texteditor = tx;
        }

        public static Boolean isclassinvalidcharcontain = false;
        public String classinvalidchar = "";
        public static Boolean isextendsinvalidcharcontain = false;
        public String extendsinvalidchar = "";
        public static Boolean isimplementsinvalidcharcontain = false;
        public String implementsinvalidchar = "";


        public String getSelectedModifiers()
        {
            String str = "";
            if (publicRadioButton.Checked == true)
            {
                str = "public";
            }
            if (privateRadioButton.Checked == true)
            {
                str = "private";
            }
            if (protectedRadioButton.Checked == true)
            {
                str = "protected";
            }
            return str;
        }

        private void ClassTextBox_TextChanged(object sender, EventArgs e)
        {
            String[] invalidchars = { " ", "~", "`", "!", "@", "#", "%", "^", "&", "*", "(", ")", "-", "+", "=", "[", "]", "{", "}", ":", ";", "\"", "'", "|", "\\", "<", ">", ",", ".", "?", "/" };

            Insertbutton.Enabled = true;

            if (ClassTextBox.Text == "")
            {
                errorlabel.Text = "";
                Insertbutton.Enabled = false;
            }

            for (int i = 0; i < invalidchars.Length; i++)
            {
                if (ClassTextBox.Text.Contains(invalidchars[i]))
                {
                    isclassinvalidcharcontain = true;
                    classinvalidchar = invalidchars[i];
                    Insertbutton.Enabled = false;
                }
            }

            if (classinvalidchar != "")
            {
                if (isclassinvalidcharcontain == true)
                {
                    if (classinvalidchar == " ")
                    {
                        errorlabel.Text = "Space is not allowed";
                        isclassinvalidcharcontain = false;
                        Insertbutton.Enabled = false;
                    }
                    else
                    {
                        errorlabel.Text = classinvalidchar + " Invalid Character";
                        isclassinvalidcharcontain = false;
                        Insertbutton.Enabled = false;
                    }
                }
                else
                {
                    errorlabel.Text = "";
                    isclassinvalidcharcontain = false;
                    Insertbutton.Enabled = true;
                }
            }
        }



        private void extendsTextBox_TextChanged(object sender, EventArgs e)
        {
            String[] invalidchars = { " ", "~", "`", "!", "@", "#", "%", "^", "&", "*", "(", ")", "-", "+", "=", "[", "]", "{", "}", ":", ";", "\"", "'", "|", "\\", "<", ">", ",", ".", "?", "/" };

            Insertbutton.Enabled = true;

            if (extendsTextBox.Text == "")
            {
                errorlabel.Text = "";
                Insertbutton.Enabled = false;
            }

            for (int i = 0; i < invalidchars.Length; i++)
            {
                if (extendsTextBox.Text.Contains(invalidchars[i]))
                {
                    isextendsinvalidcharcontain = true;
                    extendsinvalidchar = invalidchars[i];
                    Insertbutton.Enabled = false;
                }
            }

            if (extendsinvalidchar != "")
            {
                if (isextendsinvalidcharcontain == true)
                {
                    if (extendsinvalidchar == " ")
                    {
                        errorlabel.Text = "Space is not allowed";
                        isextendsinvalidcharcontain = false;
                        Insertbutton.Enabled = false;
                    }
                    else
                    {
                        errorlabel.Text = extendsinvalidchar + " Invalid Character";
                        isextendsinvalidcharcontain = false;
                        Insertbutton.Enabled = false;
                    }
                }
                else
                {
                    errorlabel.Text = "";
                    isextendsinvalidcharcontain = false;
                    Insertbutton.Enabled = true;
                }
            }
        }



        private void implementsTextBox_TextChanged(object sender, EventArgs e)
        {
            String[] invalidchars = { " ", "~", "`", "!", "@", "#", "%", "^", "&", "*", "(", ")", "-", "+", "=", "[", "]", "{", "}", ":", ";", "\"", "'", "|", "\\", "<", ">", ",", ".", "?", "/" };

            Insertbutton.Enabled = true;

            if (implementsTextBox.Text == "")
            {
                errorlabel.Text = "";
                Insertbutton.Enabled = false;
            }

            for (int i = 0; i < invalidchars.Length; i++)
            {
                if (implementsTextBox.Text.Contains(invalidchars[i]))
                {
                    isimplementsinvalidcharcontain = true;
                    implementsinvalidchar = invalidchars[i];
                    Insertbutton.Enabled = false;
                }
            }

            if (implementsinvalidchar != "")
            {
                if (isimplementsinvalidcharcontain == true)
                {
                    if (implementsinvalidchar == " ")
                    {
                        errorlabel.Text = "Space is not allowed";
                        isimplementsinvalidcharcontain = false;
                        Insertbutton.Enabled = false;
                    }
                    else
                    {
                        errorlabel.Text = implementsinvalidchar + " Invalid Character";
                        isimplementsinvalidcharcontain = false;
                        Insertbutton.Enabled = false;
                    }
                }
                else
                {
                    errorlabel.Text = "";
                    isimplementsinvalidcharcontain = false;
                    Insertbutton.Enabled = true;
                }
            }
        }

        private void Insertbutton_Click(object sender, EventArgs e)
        {
            String modifier = getSelectedModifiers();
            String classtext = ClassTextBox.Text;
            String extends = extendsTextBox.Text;
            String implements = implementsTextBox.Text;

            if (classtext != "")
            {
                if (extends == "" && implements == "")
                {
                    texteditor.ActiveTextAreaControl.TextArea.InsertString("" + modifier + " class " + classtext + " {\n}");
                }
                else if (extends == "")
                {
                    texteditor.ActiveTextAreaControl.TextArea.InsertString("" + modifier + " class " + classtext + " implements " + implements + " {\n}");
                }
                else if (implements == "")
                {
                    texteditor.ActiveTextAreaControl.TextArea.InsertString("" + modifier + " class " + classtext + " extends " + extends + " {\n}");
                }
                else if (extends != "" && implements != "")
                {
                    texteditor.ActiveTextAreaControl.TextArea.InsertString("" + modifier + " class " + classtext + " extends " + extends + " implements " + implements + " {\n}");
                }
            }
            this.Close();
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
