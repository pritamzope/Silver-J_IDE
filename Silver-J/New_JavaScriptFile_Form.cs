#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="New_JavaScriptFile_Form.cs" company="">
  
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
    public partial class New_JavaScriptFile_Form : Form
    {
        public MyTabPage mytabpage;
        public MyTabControl mytabcontrol;
        public New_JavaScriptFile_Form(MyTabPage tb, MyTabControl mytabctrl)
        {
            InitializeComponent();
            mytabpage = tb;
            mytabcontrol = mytabctrl;
        }
        String createdfilename = "";
        static Boolean isFinished = false;
        String defaultprojfilepath = Application.StartupPath + "\\files\\defaultprojloc.slvjfile";
        public static Boolean isjavascriptfileinvalidcharcontain = false;
        public String javascriptfileinvalidchar = "";


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


        //*****************************************************************************************
        //        CreateJavaScriptFile()
        //*****************************************************************************************
        public void CreateJavaScriptFile()
        {
            String jsname = JavaScriptTextBox.Text;
            String projectlocation = getCurrentProjectLocationFolder();

            if (File.Exists(ReadCurrentProjectFileName()))
            {
                if (jsname != "")
                {
                    String jsfilename = jsname;
                    if (jsfilename.Contains(".js"))
                    { }
                    else
                    {
                        jsfilename = jsfilename + ".js";
                    }
                    projectlocation = projectlocation + "\\srcclasses";

                    String txt = "";

                    if (Directory.Exists(projectlocation))
                    {
                        String filename = projectlocation + "\\" + jsfilename;

                        if (File.Exists(filename))
                        {
                            MessageBox.Show("The file name you entered is already exists in the folder or already added to your project", "Error......");
                        }
                        else
                        {
                            createdfilename = filename;
                            if (mytabcontrol.TabPages.Count >= 0)
                            {
                                mytabpage.textEditor.Text = txt;
                                mytabpage.Text = jsfilename;
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
                        String filename = projectlocation + "\\" + jsfilename;

                        if (File.Exists(filename))
                        {
                            MessageBox.Show("The file name you entered is already exists in the folder or already added to your project", "Error......");
                        }
                        else
                        {
                            createdfilename = filename;
                            if (mytabcontrol.TabPages.Count >= 0)
                            {
                                mytabpage.textEditor.Text = txt;
                                mytabpage.Text = jsfilename;
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
        //        JavaScriptTextBox Text Changed
        //*****************************************************************************************
        private void JavaScriptTextBox_TextChanged(object sender, EventArgs e)
        {
            String[] invalidchars = { " ", "~", "`", "!", "@", "#", "%", "^", "&", "*", "(", ")", "-", "+", "=", "[", "]", "{", "}", ":", ";", "\"", "'", "|", "\\", "<", ">", ",", ".", "?", "/" };

            Finishbutton.Enabled = true;

            if (JavaScriptTextBox.Text == "")
            {
                errorlabel.Text = "";
                Finishbutton.Enabled = false;
            }

            for (int i = 0; i < invalidchars.Length; i++)
            {
                if (JavaScriptTextBox.Text.Contains(invalidchars[i]))
                {
                    isjavascriptfileinvalidcharcontain = true;
                    javascriptfileinvalidchar = invalidchars[i];
                    Finishbutton.Enabled = false;
                }
            }

            if (javascriptfileinvalidchar != "")
            {
                if (isjavascriptfileinvalidcharcontain == true)
                {
                    if (javascriptfileinvalidchar == " ")
                    {
                        errorlabel.Text = "Space is not allowed";
                        isjavascriptfileinvalidcharcontain = false;
                        Finishbutton.Enabled = false;
                    }
                    else
                    {
                        errorlabel.Text = javascriptfileinvalidchar + " Invalid Character";
                        isjavascriptfileinvalidcharcontain = false;
                        Finishbutton.Enabled = false;
                    }
                }
                else
                {
                    errorlabel.Text = "";
                    isjavascriptfileinvalidcharcontain = false;
                    Finishbutton.Enabled = true;
                }
            }
        }

        //*****************************************************************************************
        //        Finish Button Click
        //*****************************************************************************************
        private void Finishbutton_Click(object sender, EventArgs e)
        {
            if (JavaScriptTextBox.Text == "")
            {
                errorlabel.Text = "Please Enter JavaScript File Name";
                Finishbutton.Enabled = false;
            }
            else
            {
                errorlabel.Text = "";
                Finishbutton.Enabled = true;
            }

            if (JavaScriptTextBox.Text != "")
            {
                CreateJavaScriptFile();
            }
        }

        //*****************************************************************************************
        //        Cancel Button Click
        //*****************************************************************************************
        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //*****************************************************************************************
        //        Help Button Click
        //*****************************************************************************************
        private void Helpbutton_Click(object sender, EventArgs e)
        {
            String file = Application.StartupPath + "\\Help\\silverjhelp.chm";
            if (File.Exists(file))
            {
                Process.Start(file);
            }
        }

        //*****************************************************************************************
        //        JavaScriptTextBox KeyDown
        //*****************************************************************************************
        private void JavaScriptTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Finishbutton_Click(sender, e);
            }
        }
    }
}
