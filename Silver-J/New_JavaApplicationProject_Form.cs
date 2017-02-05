#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="New_JavaApplicationProject_Form.cs" company="">
  
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
using System.Xml;
using System.Diagnostics;
namespace Silver_J
{
    public partial class New_JavaApplicationProject_Form : Form
    {
        public Form frm;
        public TreeView treeview;
        public MyTabControl mytabcontrol;

        public New_JavaApplicationProject_Form(Form F, TreeView trv, MyTabControl mytabcon)
        {
            InitializeComponent();
            frm = F;
            treeview = trv;
            mytabcontrol = mytabcon;
        }


        static Boolean isSaved = false;
        String projectfilename = "";
        static int a = 0;
        String createdfilename = "";
        public Boolean isfinished = false;

        public static Boolean isinvalidcharcontain = false;
        public String invalidchar = "";
        public static Boolean islocinvalidcharcontain = false;
        public String locinvalidchar = "";
        public static Boolean isclassinvalidcharcontain = false;
        public String classinvalidchar = "";
        String defaultprojfilepath = Application.StartupPath + "\\files\\defaultprojloc.slvjfile";

        public StreamReader strreader;
        public StreamWriter strwriter;


        public String getCreatedFileName()
        {
            if (createdfilename == "")
            {
                createdfilename = "";
            }
            return createdfilename;
        }


        public int CheckProjectType()
        {
            return a;
        }


        public Boolean IsFinished()
        {
            return isfinished;
        }


        public void CreateJavaProject()
        {
            String projloc = ProjectLocationTextBox.Text;
            String projfolder = projectfolderlabel.Text;
            String projectname = ProjectNameTextBox.Text;
            String projectfile;

            if (projloc != "" && projectname != "")
            {
                projectfile = projectname + ".slvjproj";
                if (checkBox1.Checked == false)
                {
                    if (Directory.Exists(projloc + "\\" + projfolder))
                    {
                        MessageBox.Show("Entered project name folder is already exists in current location","Error............");
                    }
                    else
                    {
                        //create project directory & project file
                        Directory.CreateDirectory(projloc + "\\" + projfolder);

                        //create classes directory
                        Directory.CreateDirectory(projloc + "\\" + projfolder + "\\classes");

                        //creating & writing slvjproj file 
                        using (XmlWriter xmlwriter = XmlWriter.Create(projloc + "\\" + projfolder + "\\" + projectfile))
                        {
                            xmlwriter.WriteStartDocument();
                            xmlwriter.WriteStartElement("SilverJProject");
                            xmlwriter.WriteString("\n");
                            xmlwriter.WriteComment("Silver-J (1.0) Java Application Project");
                            xmlwriter.WriteString("\n");
                            xmlwriter.WriteElementString("ProjectName", projectname);
                            xmlwriter.WriteString("\n");
                            xmlwriter.WriteElementString("ProjectLocationFolder", projloc + "\\" + projfolder);
                            xmlwriter.WriteString("\n");
                            xmlwriter.WriteElementString("ProjectLocationFile", projloc + "\\" + projfolder + "\\" + projectfile);
                            xmlwriter.WriteString("\n");
                            xmlwriter.WriteElementString("ProjectType", "ApplicationType");
                            xmlwriter.WriteEndElement();
                            xmlwriter.WriteEndDocument();
                            xmlwriter.Close();
                        }

                        String defaultprojfilepath = Application.StartupPath + "\\files\\defaultprojloc.slvjfile";

                        projectfilename = projloc + "\\" + projfolder + "\\" + projectfile;

                        XmlDocument doc = new XmlDocument();
                        doc.Load(defaultprojfilepath);
                        doc.SelectSingleNode("SilverJ/DefaultProjectLocation").InnerText = ProjectLocationTextBox.Text;
                        doc.SelectSingleNode("SilverJ/CurrentProjectName").InnerText = ProjectNameTextBox.Text;
                        doc.SelectSingleNode("SilverJ/CurrentProjectFileName").InnerText = projectfilename;
                        doc.SelectSingleNode("SilverJ/CurrentProjectType").InnerText = "ApplicationType";
                        doc.Save(defaultprojfilepath);

                        a = 1;

                        this.Close();
                        isfinished = true;
                    }
                }


                else if (checkBox1.Checked == true)
                {
                    if (JavaClassTextBox.Text != "")
                    {
                        String classname = JavaClassTextBox.Text;
                        if (classname.Contains(".java"))
                        {
                        }
                        else
                        {
                            classname = classname + ".java";
                        }

                        String javafilename = classname;

                        if (Directory.Exists(projloc + "\\" + projfolder))
                        {
                            MessageBox.Show("Entered project name folder is already exists in current location", "Error............");
                        }
                        else
                        {
                            //create project directory & project file
                            Directory.CreateDirectory(projloc + "\\" + projfolder);
                            //create srcclasses directory
                            Directory.CreateDirectory(projloc + "\\" + projfolder + "\\srcclasses");
                            //create classes directory
                            Directory.CreateDirectory(projloc + "\\" + projfolder + "\\classes");

                            String fname = projloc + "\\" + projfolder + "\\srcclasses\\" + javafilename;
                            createdfilename = fname;
                            String filename = fname.Substring(fname.LastIndexOf("\\") + 1);

                            //creating & writing slvjproj file 
                            using (XmlWriter xmlwriter = XmlWriter.Create(projloc + "\\" + projfolder + "\\" + projectfile))
                            {
                                xmlwriter.WriteStartDocument();
                                xmlwriter.WriteStartElement("SilverJProject");
                                xmlwriter.WriteString("\n");
                                xmlwriter.WriteComment("Silver-J (1.0) Java Application Project");
                                xmlwriter.WriteString("\n");
                                xmlwriter.WriteElementString("ProjectName", projectname);
                                xmlwriter.WriteString("\n");
                                xmlwriter.WriteElementString("ProjectLocationFolder", projloc + "\\" + projfolder);
                                xmlwriter.WriteString("\n");
                                xmlwriter.WriteElementString("ProjectLocationFile", projloc + "\\" + projfolder + "\\" + projectfile);
                                xmlwriter.WriteString("\n");
                                xmlwriter.WriteElementString("ProjectType", "ApplicationType");
                                xmlwriter.WriteString("\n");
                                xmlwriter.WriteElementString("MainClassFile", fname);
                                xmlwriter.WriteString("\n");
                                xmlwriter.WriteElementString("JavaClassFile", fname);
                                xmlwriter.WriteString("\n");
                                xmlwriter.WriteElementString("VisualFile", fname);
                                xmlwriter.WriteEndElement();
                                xmlwriter.WriteEndDocument();
                                xmlwriter.Close();
                            }

                            isSaved = true;

                            String defaultprojfilepath = Application.StartupPath + "\\files\\defaultprojloc.slvjfile";


                            projectfilename = projloc + "\\" + projfolder + "\\" + projectfile;

                            XmlDocument doc = new XmlDocument();
                            doc.Load(defaultprojfilepath);
                            doc.SelectSingleNode("SilverJ/DefaultProjectLocation").InnerText = ProjectLocationTextBox.Text;
                            doc.SelectSingleNode("SilverJ/CurrentProjectName").InnerText = ProjectNameTextBox.Text;
                            doc.SelectSingleNode("SilverJ/CurrentProjectFileName").InnerText = projectfilename;
                            doc.SelectSingleNode("SilverJ/CurrentProjectType").InnerText = "ApplicationType";
                            doc.Save(defaultprojfilepath);

                            a = 2;

                            this.Close();
                            isfinished = true;
                        }
                    }
                }
            }
        }





        //*****************************************************************************************
        //        New Java Application Project Form Load
        //*****************************************************************************************
        private void New_JavaApplicationProject_Form_Load(object sender, EventArgs e)
        {
            String projectfolder = "";
               using (XmlReader reader = XmlReader.Create(defaultprojfilepath))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name.ToString())
                            {
                                case "DefaultProjectLocation":
                                   projectfolder = reader.ReadString();
                                    break;
                            }
                        }
                    }
                }
                if (projectfolder!="")
                {
                    ProjectLocationTextBox.Text = projectfolder;
                    projectfolderlabel.Text = "";
                }
                else
                {
                    ProjectLocationTextBox.Text = "";
                    projectfolderlabel.Text = "No Project";
                }
        }


        //*****************************************************************************************
        //        Project Name TextBox Text Changed
        //*****************************************************************************************
        private void ProjectNameTextBox_TextChanged(object sender, EventArgs e)
        {
            String[] invalidchars = { " ","~", "`", "!", "@", "#", "%", "^", "&", "*", "(", ")", "-", "+", "=", "[", "]", "{", "}", ":", ";", "\"", "'", "|", "\\", "<", ">", ",", ".", "?", "/" };

            if(ProjectNameTextBox.Text=="")
            {
                errorlabel.Text = "";
                projectfolderlabel.Text = "No Project";
                Finishbutton.Enabled = false;
            }
            else
            {
                projectfolderlabel.Text = ProjectNameTextBox.Text;
                Finishbutton.Enabled = true;
            }

            for(int i=0;i<invalidchars.Length;i++)
            {
                if(ProjectNameTextBox.Text.Contains(invalidchars[i]))
                {
                    isinvalidcharcontain = true;
                    invalidchar = invalidchars[i];
                    Finishbutton.Enabled = false;
                }
            }

            if (invalidchar != "")
            {
                if (isinvalidcharcontain == true)
                {
                    if(invalidchar==" ")
                    {
                        errorlabel.Text = "Space is not allowed";
                        isinvalidcharcontain = false;
                        Finishbutton.Enabled = false;
                    }
                    else
                    {
                        errorlabel.Text = invalidchar + " Invalid Character";
                        isinvalidcharcontain = false;
                        Finishbutton.Enabled = false;
                    }
                }
                else
                {
                    errorlabel.Text = "";
                    projectfolderlabel.Text = ProjectNameTextBox.Text;
                    isinvalidcharcontain = false;
                    Finishbutton.Enabled = true;
                }
            }
        }

        //*****************************************************************************************
        //        Project Location TextBox Text Changed
        //*****************************************************************************************
        private void ProjectLocationTextBox_TextChanged(object sender, EventArgs e)
        {
            String[] invalidchars = {"*", " : ", "\"", "|", " \\", "<", ">", ".", "?" };

            if (ProjectLocationTextBox.Text == "")
            {
                errorlabel.Text = "";
            }

            for (int i = 0; i < invalidchars.Length; i++)
            {
                if (ProjectLocationTextBox.Text.Contains(invalidchars[i]))
                {
                    islocinvalidcharcontain = true;
                    locinvalidchar = invalidchars[i];
                }
            }

            if (locinvalidchar != "")
            {
                if (islocinvalidcharcontain == true)
                {
                        errorlabel.Text = locinvalidchar + " Invalid Character";
                        islocinvalidcharcontain = false;
                }
                else
                {
                    errorlabel.Text = "";
                    islocinvalidcharcontain = false;
                }
            }
        }


        //*****************************************************************************************
        //        checkBox1 Checked Changed
        //*****************************************************************************************
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                JavaClassTextBox.Enabled = true;
                Finishbutton.Enabled = true;
            }
            else if (checkBox1.Checked == false)
            {
                JavaClassTextBox.Enabled = false;
                errorlabel.Text = "";
                Finishbutton.Enabled = true;
            }
        }

        //*****************************************************************************************
        //        Java Class TextBox Text Changed
        //*****************************************************************************************

        private void JavaClassTextBox_TextChanged(object sender, EventArgs e)
        {
            String[] invalidchars = { " ", "~", "`", "!", "@", "#", "%", "^", "&", "*", "(", ")", "-", "+", "=", "[", "]", "{", "}", ":", ";", "\"", "'", "|", "\\", "<", ">", ",", ".", "?", "/" };

            Finishbutton.Enabled = true;

            if (JavaClassTextBox.Text == "")
            {
                errorlabel.Text = "";
                Finishbutton.Enabled = false;
            }

            for (int i = 0; i < invalidchars.Length; i++)
            {
                if (JavaClassTextBox.Text.Contains(invalidchars[i]))
                {
                    isclassinvalidcharcontain = true;
                    classinvalidchar = invalidchars[i];
                    Finishbutton.Enabled = false;
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
                        Finishbutton.Enabled = false;
                    }
                    else
                    {
                        errorlabel.Text = classinvalidchar + " Invalid Character";
                        isclassinvalidcharcontain = false;
                        Finishbutton.Enabled = false;
                    }
                }
                else
                {
                    errorlabel.Text = "";
                    projectfolderlabel.Text = ProjectNameTextBox.Text;
                    isclassinvalidcharcontain = false;
                    Finishbutton.Enabled = true;
                }
            }
        }


        //*****************************************************************************************
        //        Finish Button Click
        //*****************************************************************************************
        private void Finishbutton_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                if (ProjectNameTextBox.Text == "")
                {
                    errorlabel.Text = "Please Enter Project Name";
                    Finishbutton.Enabled = false;
                }
                else
                {
                    errorlabel.Text = "";
                    Finishbutton.Enabled = true;
                }
                if (ProjectLocationTextBox.Text == "")
                {
                    errorlabel.Text = "Please Select Project Location Folder";
                    Finishbutton.Enabled = false;
                }
                else
                {
                    errorlabel.Text = "";
                    Finishbutton.Enabled = true;
                }

                if(ProjectNameTextBox.Text!=""&&ProjectLocationTextBox.Text!="")
                {
                    CreateJavaProject();
                    this.Close();
                }
            }

            else
            {
                if (ProjectNameTextBox.Text == "")
                {
                    errorlabel.Text = "Please Enter Project Name";
                    Finishbutton.Enabled = false;
                }
                else
                {
                    errorlabel.Text = "";
                    Finishbutton.Enabled = true;
                }
                if (ProjectLocationTextBox.Text == "")
                {
                    errorlabel.Text = "Please Select Project Location Folder";
                    Finishbutton.Enabled = false;
                }
                else
                {
                    errorlabel.Text = "";
                    Finishbutton.Enabled = true;
                }
                if(JavaClassTextBox.Text=="")
                {
                    errorlabel.Text = "Please Enter Class Name";
                    Finishbutton.Enabled = false;
                }
                else
                {
                    Finishbutton.Enabled = true;
                }
                if (ProjectNameTextBox.Text != "" && ProjectLocationTextBox.Text != ""&&JavaClassTextBox.Text!="")
                {
                    CreateJavaProject();
                    this.Close();
                }
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
        //        Browse Button Click
        //*****************************************************************************************
        private void Browsebutton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                ProjectLocationTextBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        //*****************************************************************************************
        //         ProjectNameTextBox Key Down
        //*****************************************************************************************
        private void ProjectNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                Finishbutton_Click(sender, e);
            }
        }

        //*****************************************************************************************
        //        JavaClassTextBox Key Down
        //*****************************************************************************************
        private void JavaClassTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Finishbutton_Click(sender, e);
            }
        }
    }
}
