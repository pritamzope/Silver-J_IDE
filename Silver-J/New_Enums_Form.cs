#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="New_Enums_Form.cs" company="">
  
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
using System.Xml;
using System.IO;
using System.Diagnostics;
namespace Silver_J
{
    public partial class New_Enums_Form : Form
    {
        public MyTabPage mytabpage;
        public MyTabControl mytabcontrol;
        public New_Enums_Form(MyTabPage tb, MyTabControl mytabctrl)
        {
            InitializeComponent();
            mytabpage = tb;
            mytabcontrol = mytabctrl;
        }
        String createdfilename = "";
        static Boolean isFinished = false;
        String defaultprojfilepath = Application.StartupPath + "\\files\\defaultprojloc.slvjfile";
        public static Boolean isenumsinvalidcharcontain = false;
        public String enumsinvalidchar = "";


        public String ReadCurrentProjectFileName()
        {
            String s = "";
            using (XmlReader reader = XmlReader.Create(defaultprojfilepath))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "CurrentProjectFileName":
                                s = reader.ReadString();
                                break;
                        }
                    }
                }
            }
            return s;
        }



        public String getCurrentProjectLocationFolder()
        {
            String projectfile = ReadCurrentProjectFileName();
            String projectlocationfolder = "";
            if (File.Exists(projectfile))
            {
                using (XmlReader reader = XmlReader.Create(projectfile))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name.ToString())
                            {
                                case "ProjectLocationFolder":
                                    projectlocationfolder = reader.ReadString();
                                    break;
                            }
                        }
                    }
                }
            }
            return projectlocationfolder;
        }

        public Boolean IsFinished()
        {
            return isFinished;
        }


        public String getCreatedFileName()
        {
            if (createdfilename == "")
            {
                createdfilename = "";
            }
            return createdfilename;
        }


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

        //*****************************************************************************************
        //        CreateEnums()
        //*****************************************************************************************
        public void CreateEnums()
        {
            String enumsname = EnumsTextBox.Text;
            String modifier = getSelectedModifiers();
            String projectlocation = getCurrentProjectLocationFolder();

            if (File.Exists(ReadCurrentProjectFileName()))
            {
                if (enumsname != "")
                {
                    String enumsfilename = enumsname;
                    if (enumsfilename.Contains(".java"))
                    { }
                    else
                    {
                        enumsfilename = enumsfilename + ".java";
                    }
                    projectlocation = projectlocation + "\\srcclasses";

                    if (Directory.Exists(projectlocation))
                    {
                        String filename = projectlocation + "\\" + enumsfilename;

                        if (File.Exists(filename))
                        {
                            MessageBox.Show("The file name you entered is already exists in the folder or already added to your project", "Error......");
                        }
                        else
                        {
                            createdfilename = filename;
                            String txt = "" + modifier + " enum " + enumsname + "  { \n                                                               \n}";
                            if (mytabcontrol.TabPages.Count >= 0)
                            {
                                mytabpage.textEditor.Text = txt;
                                mytabpage.Text = enumsfilename;
                                if (mytabpage.Text.Contains("*"))
                                {
                                    mytabpage.Text = mytabpage.Text.Remove(mytabpage.Text.LastIndexOf("*"));
                                }
                                mytabcontrol.TabPages.Add(mytabpage);
                                mytabcontrol.SelectedTab = mytabpage;
                                isFinished = true;
                                this.Close();
                            }
                        }
                    }

                    else
                    {
                        Directory.CreateDirectory(projectlocation);
                        String filename = projectlocation + "\\" + enumsfilename;

                        if (File.Exists(filename))
                        {
                            MessageBox.Show("The file name you entered is already exists in the folder or already added to your project", "Error......");
                        }
                        else
                        {
                            createdfilename = filename;
                            String txt = "" + modifier + " enum " + enumsname + "  { \n                                                               \n}";
                            if (mytabcontrol.TabPages.Count >= 0)
                            {
                                mytabpage.textEditor.Text = txt;
                                mytabpage.Text = enumsfilename;
                                if (mytabpage.Text.Contains("*"))
                                {
                                    mytabpage.Text = mytabpage.Text.Remove(mytabpage.Text.LastIndexOf("*"));
                                }
                                mytabcontrol.TabPages.Add(mytabpage);
                                mytabcontrol.SelectedTab = mytabpage;
                                isFinished = true;
                                this.Close();
                            }
                        }
                    }
                }
            }
        }


        //*****************************************************************************************
        //        EnumsTextBox Text Changed
        //*****************************************************************************************
        private void EnumsTextBox_TextChanged(object sender, EventArgs e)
        {
            String[] invalidchars = { " ", "~", "`", "!", "@", "#", "%", "^", "&", "*", "(", ")", "-", "+", "=", "[", "]", "{", "}", ":", ";", "\"", "'", "|", "\\", "<", ">", ",", ".", "?", "/" };

            Finishbutton.Enabled = true;

            if (EnumsTextBox.Text == "")
            {
                errorlabel.Text = "";
                Finishbutton.Enabled = false;
            }

            for (int i = 0; i < invalidchars.Length; i++)
            {
                if (EnumsTextBox.Text.Contains(invalidchars[i]))
                {
                    isenumsinvalidcharcontain = true;
                    enumsinvalidchar = invalidchars[i];
                    Finishbutton.Enabled = false;
                }
            }

            if (enumsinvalidchar != "")
            {
                if (isenumsinvalidcharcontain == true)
                {
                    if (enumsinvalidchar == " ")
                    {
                        errorlabel.Text = "Space is not allowed";
                        isenumsinvalidcharcontain = false;
                        Finishbutton.Enabled = false;
                    }
                    else
                    {
                        errorlabel.Text = enumsinvalidchar + " Invalid Character";
                        isenumsinvalidcharcontain = false;
                        Finishbutton.Enabled = false;
                    }
                }
                else
                {
                    errorlabel.Text = "";
                    isenumsinvalidcharcontain = false;
                    Finishbutton.Enabled = true;
                }
            }
        }

        //*****************************************************************************************
        //        Finish Button Click
        //*****************************************************************************************
        private void Finishbutton_Click(object sender, EventArgs e)
        {
            if(EnumsTextBox.Text=="")
            {
                errorlabel.Text = "Please Enter Enums Name";
                Finishbutton.Enabled = false;
            }
            else
            {
                errorlabel.Text = "";
                Finishbutton.Enabled = true;
            }

            if(EnumsTextBox.Text!="")
            {
                CreateEnums();
            }
        }

        //*****************************************************************************************
        //        Cancel Button Click
        //*****************************************************************************************
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //*****************************************************************************************
        //        Help Button Click
        //*****************************************************************************************
        private void HelpButton_Click(object sender, EventArgs e)
        {
            String file = Application.StartupPath + "\\Help\\silverjhelp.chm";
            if (File.Exists(file))
            {
                Process.Start(file);
            }
        }

        //*****************************************************************************************
        //        EnumsTextBox KeyDown
        //*****************************************************************************************
        private void EnumsTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Finishbutton_Click(sender, e);
            }
        }
    }
}
