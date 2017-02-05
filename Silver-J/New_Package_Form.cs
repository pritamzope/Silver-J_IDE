#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="New_Package_Form.cs" company="">
  
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
    public partial class New_Package_Form : Form
    {
        public MyTabPage mytabpage;
        public MyTabControl mytabcontrol;
        public String createdclassfilename;
        public String createdpackagename;
        public String packagefolderpath;
        public static Boolean isformclosed = false;
        public static int packagetype = 1;
        public Boolean isfinished = false;
        String defaultprojfilepath = Application.StartupPath + "\\files\\defaultprojloc.slvjfile";

        public static Boolean ispackageinvalidcharcontain = false;
        public String packageinvalidchar = "";
        public static Boolean isclassinvalidcharcontain = false;
        public String classinvalidchar = "";
        public static String s1="", s2 = "";
        public static Boolean b = false;

        public New_Package_Form(MyTabPage tb, MyTabControl tabctrl)
        {
            InitializeComponent();
            mytabpage = tb;
            mytabcontrol = tabctrl;
        }


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


        public String getCreatedPackageName()
        {
            return createdpackagename;
        }


        public String getCreatedPackageFolderPath()
        {
            return packagefolderpath;
        }


        public String getCreatedClassFileName()
        {
            if (createdclassfilename == "")
            {
                createdclassfilename = "";
            }
            return createdclassfilename;
        }


        public int TypeOfPackage()
        {
            return packagetype;
        }

        public Boolean IsFormClosed()
        {
            return isformclosed;
        }

        public Boolean IsFinished()
        {
            return isfinished;
        }


        //*****************************************************************************************
        //        CreatePackage()
        //*****************************************************************************************
        public void CreatePackage()
        {
            String packagename = PackageTextBox.Text;
            String classname = ClassTextBox.Text;
            String projectlocation = getCurrentProjectLocationFolder();
            if (File.Exists(ReadCurrentProjectFileName()))
            {
                if (checkBox1.Checked == false)
                {
                    if (classname != "" && packagename != "")
                    {
                        String classfilename = classname;
                        if (classfilename.Contains(".java"))
                        { }
                        else
                        {
                            classfilename = classfilename + ".java";
                        }

                        if (packagename.Contains("."))
                        {
                            packagename = packagename.Replace(".", "\\");
                        }

                        projectlocation = projectlocation + "\\srcclasses";

                        if (Directory.Exists(projectlocation))
                        {
                            if (Directory.Exists(projectlocation + "\\" + packagename))
                            {
                                MessageBox.Show("The package name you entered is already exists in the folder or already added to your project", "Error......");
                            }
                            else
                            {
                                Directory.CreateDirectory(projectlocation + "\\" + packagename);

                                createdclassfilename = projectlocation + "\\" + packagename + "\\" + classfilename;

                                String packagenamefolderpath = projectlocation + "\\" + packagename;
                                packagefolderpath = packagenamefolderpath;
                                createdpackagename = packagename;

                                if (mytabcontrol.TabPages.Count >= 0)
                                {
                                    if (packagename.Contains("\\"))
                                    {
                                        packagename = packagename.Replace("\\", ".");
                                    }
                                    mytabpage.textEditor.Text = "package " + packagename + ";\n" + "public class " + classname + "   {\n                                               \n}";
                                    mytabpage.Text = classfilename;
                                    if (mytabpage.Text.Contains("*"))
                                    {
                                        mytabpage.Text = mytabpage.Text.Remove(mytabpage.Text.LastIndexOf("*"));
                                    }
                                    mytabcontrol.TabPages.Add(mytabpage);
                                    mytabcontrol.SelectedTab = mytabpage;
                                    this.Close();
                                    isformclosed = true;
                                    isfinished = true;
                                    packagetype = 1;
                                }
                            }
                        }

                        else
                        {
                            Directory.CreateDirectory(projectlocation);

                            if (Directory.Exists(projectlocation + "\\" + packagename))
                            {
                                MessageBox.Show("The package name you entered is already exists in the folder or already added to your project", "Error......");
                            }
                            else
                            {
                                Directory.CreateDirectory(projectlocation + "\\" + packagename);

                                createdclassfilename = projectlocation + "\\" + packagename + "\\" + classfilename;

                                String packagenamefolderpath = projectlocation + "\\" + packagename;
                                packagefolderpath = packagenamefolderpath;
                                createdpackagename = packagename;

                                if (mytabcontrol.TabPages.Count >= 0)
                                {
                                    if (packagename.Contains("\\"))
                                    {
                                        packagename = packagename.Replace("\\", ".");
                                    }
                                    mytabpage.textEditor.Text = "package " + packagename + ";\n" + "public class " + classname + "   {\n                                               \n}";
                                    mytabpage.Text = classfilename;
                                    if (mytabpage.Text.Contains("*"))
                                    {
                                        mytabpage.Text = mytabpage.Text.Remove(mytabpage.Text.LastIndexOf("*"));
                                    }
                                    mytabcontrol.TabPages.Add(mytabpage);
                                    mytabcontrol.SelectedTab = mytabpage;
                                    this.Close();
                                    isformclosed = true;
                                    isfinished = true;
                                    packagetype = 1;
                                }
                            }
                        }
                    }
                }

                else
                {
                    if (classname != "" && listBox1.SelectedItem != null)
                    {
                        String selectedpackagefoldername = listBox1.SelectedItem.ToString();
                        String classfilename = classname;
                        if (classfilename.Contains(".java"))
                        { }
                        else
                        {
                            classfilename = classfilename + ".java";
                        }

                        if (selectedpackagefoldername.Contains("."))
                        {
                            selectedpackagefoldername = selectedpackagefoldername.Replace(".", "\\");
                        }

                        projectlocation = projectlocation + "\\srcclasses";

                        if (Directory.Exists(projectlocation))
                        {
                           // Directory.CreateDirectory(projectlocation + "\\" + packagename);

                            createdclassfilename = projectlocation + "\\" + selectedpackagefoldername + "\\" + classfilename;

                            if (mytabcontrol.TabPages.Count >= 0)
                            {
                                if (selectedpackagefoldername.Contains("\\"))
                                {
                                    selectedpackagefoldername = selectedpackagefoldername.Replace("\\", ".");
                                }
                                mytabpage.textEditor.Text = "package " + selectedpackagefoldername + ";\n" + "public class " + classname + "   {\n                                               \n}";
                                mytabpage.Text = classfilename;
                                if (mytabpage.Text.Contains("*"))
                                {
                                    mytabpage.Text = mytabpage.Text.Remove(mytabpage.Text.LastIndexOf("*"));
                                }
                                mytabcontrol.TabPages.Add(mytabpage);
                                mytabcontrol.SelectedTab = mytabpage;
                                this.Close();
                                isformclosed = true;
                                isfinished = true;
                                packagetype = 2;
                            }
                        }
                    }
                }
            }
        }


        //*****************************************************************************************
        //        PackageTextBox Text Changed
        //*****************************************************************************************
        private void PackageTextBox_TextChanged(object sender, EventArgs e)
        {
            String[] invalidchars = { " ", "~", "`", "!", "@", "#", "%", "^", "&", "*", "(", ")", "-", "+", "=", "[", "]", "{", "}", ":", ";", "\"", "'", "|", "\\", "<", ">", ",", ". "," .", "?", "/" };

            Finishbutton.Enabled = true;
            s1 = "";
            s2 = "";
            errorlabel.Text = "";

            if (PackageTextBox.Text == "")
            {
                errorlabel.Text = "";
                Finishbutton.Enabled = false;
            }

            for (int i = 0; i < invalidchars.Length; i++)
            {
                if (PackageTextBox.Text.Contains(invalidchars[i]))
                {
                    ispackageinvalidcharcontain = true;
                    packageinvalidchar = invalidchars[i];
                    Finishbutton.Enabled = false;
                }
            }

            if (packageinvalidchar != "")
            {
                if (ispackageinvalidcharcontain == true)
                {
                    if (packageinvalidchar == " ")
                    {
                        errorlabel.Text = "Space is not allowed";
                        ispackageinvalidcharcontain = false;
                        Finishbutton.Enabled = false;
                    }
                    else
                    {
                        errorlabel.Text = packageinvalidchar + " Invalid Character";
                        ispackageinvalidcharcontain = false;
                        Finishbutton.Enabled = false;
                    }
                }
                else
                {
                    errorlabel.Text = "";
                    ispackageinvalidcharcontain = false;
                    Finishbutton.Enabled = true;
                }
            }
        }

        //*****************************************************************************************
        //        ClassTextBox Text Changed
        //*****************************************************************************************
        private void ClassTextBox_TextChanged(object sender, EventArgs e)
        {
            String[] invalidchars = { " ", "~", "`", "!", "@", "#", "%", "^", "&", "*", "(", ")", "-", "+", "=", "[", "]", "{", "}", ":", ";", "\"", "'", "|", "\\", "<", ">", ",", ".", "?", "/" };

            Finishbutton.Enabled = true;

            if (ClassTextBox.Text == "")
            {
                errorlabel.Text = "";
                Finishbutton.Enabled = false;
            }

            for (int i = 0; i < invalidchars.Length; i++)
            {
                if (ClassTextBox.Text.Contains(invalidchars[i]))
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
                    isclassinvalidcharcontain = false;
                    Finishbutton.Enabled = true;
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
                PackageTextBox.Enabled = false;
                listBox1.Enabled = true;
            }
            else
            {
                PackageTextBox.Enabled = true;
                listBox1.Enabled = false;
            }
        }

        //*****************************************************************************************
        //        Finish button Click
        //*****************************************************************************************
        private void Finishbutton_Click(object sender, EventArgs e)
        {
            if(checkBox1.Checked==false)
            {
                if(PackageTextBox.Text=="")
                {
                    errorlabel.Text = "Please Enter Package Name";
                    Finishbutton.Enabled = false;
                }
                else if(PackageTextBox.Text.Contains("."))
                {
                    s1 = PackageTextBox.Text[0].ToString();
                    s2 = PackageTextBox.Text[PackageTextBox.Text.Length - 1].ToString();

                    if(s1=="." || s2==".")
                    {
                        errorlabel.Text = "Package Name cannot Start or End with dot(.) operator";
                        Finishbutton.Enabled = false;
                    }
                    else
                    {
                        errorlabel.Text = "";
                        Finishbutton.Enabled = true;
                    }
                }
                else
                {
                    errorlabel.Text = "";
                    Finishbutton.Enabled = true;
                }
                if (ClassTextBox.Text == "")
                {
                    errorlabel.Text = "Please Enter Class Name";
                    Finishbutton.Enabled = false;
                }
                else
                {
                    errorlabel.Text = "";
                    Finishbutton.Enabled = true;
                }

                if(PackageTextBox.Text!=""&&ClassTextBox.Text!="")
                {
                    CreatePackage();
                }
            }

            else
            {
                if (ClassTextBox.Text == "")
                {
                    errorlabel.Text = "Please Enter Class Name";
                    Finishbutton.Enabled = false;
                }
                else
                {
                    errorlabel.Text = "";
                    Finishbutton.Enabled = true;
                }

                if(listBox1.SelectedItem==null)
                {
                    errorlabel.Text = "Please Select Package Name";
                    Finishbutton.Enabled = false;
                }
                else
                {
                    errorlabel.Text = "";
                    Finishbutton.Enabled = true;
                }

                if(ClassTextBox.Text!=""&&listBox1.SelectedItem!=null)
                {
                    CreatePackage();
                }
            }
        }

        //*****************************************************************************************
        //        Cancel button Click
        //*****************************************************************************************
        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //*****************************************************************************************
        //        Help button Click
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
        //        Package Form Load
        //*****************************************************************************************
        private void New_Package_Form_Load(object sender, EventArgs e)
        {
            if (File.Exists(ReadCurrentProjectFileName()))
            {
                if (ReadCurrentProjectFileName() != "")
                {
                    String projectfile = ReadCurrentProjectFileName();
                    RichTextBox rtb = new RichTextBox();
                    using (XmlReader xmlreader = XmlReader.Create(projectfile))
                    {
                        while (xmlreader.Read())
                        {
                            if (xmlreader.IsStartElement())
                            {
                                switch (xmlreader.Name.ToString())
                                {
                                    case "PackageName":
                                        rtb.Text = rtb.Text.Insert(0, xmlreader.ReadString() + "\n");
                                        break;
                                }
                            }
                        }
                        xmlreader.Close();
                    }

                    String[] lines = rtb.Lines;
                    String str = "";
                    foreach (String line in lines)
                    {
                        if (line != "")
                        {
                            if(line.Contains("\\"))
                            {
                                str = line.Replace("\\", ".");
                            }
                            else
                            {
                                str = line;
                            }
                            listBox1.Items.Add(str);
                        }
                    }
                }
            }
        }


        //*****************************************************************************************
        //        listBox1 Mouse Click
        //*****************************************************************************************
        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Finishbutton.Enabled = true;
        }

        //*****************************************************************************************
        //        Package TextBox Key Down
        //*****************************************************************************************
        private void PackageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                Finishbutton_Click(sender, e);
            }
        }

        //*****************************************************************************************
        //        ClassTextBox Key Down
        //*****************************************************************************************
        private void ClassTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Finishbutton_Click(sender, e);
            }
        }
    }
}
