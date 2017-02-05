#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="New_Class_Form.cs" company="">
  
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
    public partial class New_Class_Form : Form
    {
        public Form frm;
        public TreeView treeview;
        public MyTabControl mytabcontrol;
        public MyTabPage mytabpage;
        public New_Class_Form(MyTabPage tb, MyTabControl mytabctrl)
        {
            InitializeComponent();
            mytabpage = tb;
            mytabcontrol = mytabctrl;
        }

        static int a = 0;
        String createdfilename = "";
        static Boolean isFinished = false;

        public static Boolean issuperinvalidcharcontain = false;
        public String superinvalidchar = "";
        public static Boolean isinterfaceinvalidcharcontain = false;
        public String interfaceinvalidchar = "";
        public static Boolean isclassinvalidcharcontain = false;
        public String classinvalidchar = "";
        String defaultprojfilepath = Application.StartupPath + "\\files\\defaultprojloc.slvjfile";

        public StreamReader strreader;
        public StreamWriter strwriter;


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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public String getSelectedTheme()
        {
            String thm = "";
            if (CreateClasswithThemesCheckBox.Checked == true)
            {
                if (MetalThemeCheckBox.Checked == true)
                {
                    thm = "metal";
                }
                else if (MotifThemeCheckBox.Checked == true)
                {
                    thm = "motif";
                }
                else if (NimbusThemeCheckBox.Checked == true)
                {
                    thm = "nimbus";
                }
                else if (WindowsThemeCheckBox.Checked == true)
                {
                    thm = "windows";
                }
                else if (WindowsClassicThemeCheckBox.Checked == true)
                {
                    thm = "windowsclassic";
                }
            }
            return thm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public String ReadCurrentProjectName()
        {
            String str2 = "";
            using (XmlReader reader = XmlReader.Create(defaultprojfilepath))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "CurrentProjectName":
                                str2 = reader.ReadString();
                                break;
                        }
                    }
                }
            }

            return str2;
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

        public String getAbstractStaticFinalChoice()
        {
            String str = " ";
            if (AbstractCheckBox.Checked == true)
            {
                str = "abstract";
            }
            if (staticCheckBox.Checked == true)
            {
                str = "static";
            }
            if (FinalCheckBox.Checked == true)
            {
                str = "final";
            }
            return str;
        }

        public String IsMainMethodSelected()
        {
            String str = "";
            if (JavaMainMethodCheckBox.Checked == true)
            {
                str = "public static void main(String[] args)";
            }
            return str;
        }

        public String IsConstructorSelected()
        {
            String str = "";
            if (ConstructorCheckBox.Checked == true)
            {
                String s = getSelectedModifiers();
                str = " " + s + " " + ClassTextBox.Text + "()";
            }
            return str;
        }



        //*****************************************************************************************
        //        CreateClass()
        //*****************************************************************************************
        public void CreateClass()
        {
            String classname = ClassTextBox.Text;
            String modifiers = getSelectedModifiers();
            String abststatfinal = getAbstractStaticFinalChoice();
            String superclass = SuperClassTextBox.Text;
            String interfacename = InterfaceTextBox.Text;
            String mainmethod = IsMainMethodSelected();
            String constructor = IsConstructorSelected();
            String projectlocation = getCurrentProjectLocationFolder();
            String prjname = ReadCurrentProjectName();
            String txt = "";
            if (File.Exists(ReadCurrentProjectFileName()))
            {
                if (classname != "")
                {
                    if (CreateClasswithThemesCheckBox.Checked == false)
                    {
                        String classfilename = classname;
                        if (classfilename.Contains(".java"))
                        { }
                        else
                        {
                            classfilename = classfilename + ".java";
                        }
                        projectlocation = projectlocation + "\\srcclasses";

                        if (Directory.Exists(projectlocation))
                        {
                            String filename = projectlocation + "\\" + classfilename;

                            if (File.Exists(filename))
                            {
                                MessageBox.Show("The file name you entered is already exists in the folder or already added to your project", "Error......");
                            }
                            else
                            {
                                createdfilename = filename;

                                if (JavaMainMethodCheckBox.Checked == true)
                                {
                                    if (superclass == "")
                                    {
                                        txt = "/*******************************\n                    "+prjname+"     \n*********************************/"
                                         +"\n"+modifiers + " " + abststatfinal + " class " + classname + "   {\n          " + mainmethod + "   {\n                \n      }  \n}";
                                    }
                                    else if (superclass != "")
                                    {
                                        txt = "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "  extends " + superclass + "  {\n      " + mainmethod + "     {\n        } \n";
                                    }
                                    else if (interfacename == "")
                                    {
                                        txt = "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "   {\n          " + mainmethod + "   {\n         }  \n}";
                                    }
                                    if (interfacename != "")
                                    {
                                        txt = "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "  implements " + interfacename + "   {\n          " + mainmethod + "   {\n         }  \n}";
                                    }
                                    if (superclass != "" && interfacename != "")
                                    {
                                        txt = "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + " extends " + superclass + "  implements " + interfacename + "   {\n          " + mainmethod + "   {\n         }  \n}";
                                    }
                                }

                                else if (ConstructorCheckBox.Checked == true)
                                {
                                    if (superclass == "")
                                    {
                                        txt = "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "  { \n                    " + modifiers + " " + classname + "()    {\n                  }\n}";
                                    }
                                    else if (superclass != "")
                                    {
                                        txt = "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "  extends " + superclass + "  { \n                    " + modifiers + " " + classname + "()    {\n                  }\n}";
                                    }
                                    else if (interfacename == "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "  { \n                    " + modifiers + " " + classname + "()    {\n                  }\n}";
                                    }
                                    if (interfacename != "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "  implements " + interfacename + "  { \n                    " + modifiers + " " + classname + "()    {\n                  }\n}";
                                    }
                                    if (superclass != "" && interfacename != "")
                                    {
                                        txt = "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + " extends " + superclass + "  implements " + interfacename + "  { \n                    " + modifiers + " " + classname + "()    {\n                  }\n}";
                                    }
                                }
                                else
                                {
                                    if (superclass == "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "   {\n}";
                                    }
                                    else if (superclass != "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "  extends " + superclass + "  {\n}";
                                    }
                                    else if (interfacename == "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "   {\n}";
                                    }
                                    if (interfacename != "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "  implements " + interfacename + "  {\n}";
                                    }
                                    if (superclass != "" && interfacename != "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + " extends " + superclass + "  implements " + interfacename + "  {\n}";
                                    }
                                }
                                if (mytabcontrol.TabPages.Count >= 0)
                                {
                                    mytabpage.textEditor.Text = txt;
                                    mytabpage.Text = classfilename;
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
                            String filename = projectlocation + "\\" + classfilename;
                            createdfilename = filename;

                            if (File.Exists(filename))
                            {
                                MessageBox.Show("The file name you entered is already exists in the folder or already added to your project", "Error......");
                            }
                            else
                            {
                                if (JavaMainMethodCheckBox.Checked == true)
                                {
                                    if (superclass == "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "   {\n          " + mainmethod + "   {\n                \n      }  \n}";
                                    }
                                    else if (superclass != "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "  extends " + superclass + "  {\n      " + mainmethod + "     {\n        } \n";
                                    }
                                    else if (interfacename == "")
                                    {
                                        txt = "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "   {\n          " + mainmethod + "   {\n         }  \n}";
                                    }
                                    if (interfacename != "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "  implements " + interfacename + "   {\n          " + mainmethod + "   {\n         }  \n}";
                                    }
                                    if (superclass != "" && interfacename != "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + " extends " + superclass + "  implements " + interfacename + "   {\n          " + mainmethod + "   {\n         }  \n}";
                                    }
                                }

                                else if (ConstructorCheckBox.Checked == true)
                                {
                                    if (superclass == "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "  { \n                    " + modifiers + " " + classname + "()    {\n                  }\n}";
                                    }
                                    else if (superclass != "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "  extends " + superclass + "  { \n                    " + modifiers + " " + classname + "()    {\n                  }\n}";
                                    }
                                    else if (interfacename == "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "  { \n                    " + modifiers + " " + classname + "()    {\n                  }\n}";
                                    }
                                    if (interfacename != "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "  implements " + interfacename + "  { \n                    " + modifiers + " " + classname + "()    {\n                  }\n}";
                                    }
                                    if (superclass != "" && interfacename != "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + " extends " + superclass + "  implements " + interfacename + "  { \n                    " + modifiers + " " + classname + "()    {\n                  }\n}";
                                    }
                                }
                                else
                                {
                                    if (superclass == "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "   {\n}";
                                    }
                                    else if (superclass != "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "  extends " + superclass + "  {\n}";
                                    }
                                    else if (interfacename == "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "   {\n}";
                                    }
                                    if (interfacename != "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + "  implements " + interfacename + "  {\n}";
                                    }
                                    if (superclass != "" && interfacename != "")
                                    {
                                        txt =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                        +"\n"+modifiers + " " + abststatfinal + " class " + classname + " extends " + superclass + "  implements " + interfacename + "  {\n}";
                                    }
                                }
                                if (mytabcontrol.TabPages.Count >= 0)
                                {
                                    mytabpage.textEditor.Text = txt;
                                    mytabpage.Text = classfilename;
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

                    else
                    {
                        String classfilename = classname;
                        String themename = "";

                        if (classfilename.Contains(".java")) { }
                        else
                        {
                            classfilename = classfilename + ".java";
                        }
                        projectlocation = projectlocation + "\\srcclasses";

                        if (Directory.Exists(projectlocation))
                        {
                            String filename = projectlocation + "\\" + classfilename;

                            if (File.Exists(filename))
                            {
                                MessageBox.Show("The file name you entered is already exists in the folder or already added to your project", "Error......");
                            }
                            else
                            {
                                createdfilename = filename;
                                String themesyntax = "";
                                String themetext = getSelectedTheme();
                                if (themetext == "")
                                {
                                    MessageBox.Show("Please select any one of the choce", "Select");
                                }
                                else
                                {
                                    if (themetext == "metal")
                                    {
                                        themesyntax = "javax.swing.plaf.metal.MetalLookAndFeel";
                                        themename = "Metal theme";
                                    }
                                    else if (themetext == "motif")
                                    {
                                        themesyntax = "com.sun.java.swing.plaf.motif.MotifLookAndFeel";
                                        themename = "Motif theme";
                                    }
                                    else if (themetext == "nimbus")
                                    {
                                        themesyntax = "com.sun.java.swing.plaf.nimbus.NimbusLookAndFeel";
                                        themename = "Nimbus theme";
                                    }
                                    else if (themetext == "windows")
                                    {
                                        themesyntax = "com.sun.java.swing.plaf.windows.WindowsLookAndFeel";
                                        themename = "Windows theme";
                                    }
                                    else if (themetext == "windowsclassic")
                                    {
                                        themesyntax = "com.sun.java.swing.plaf.windows.WindowsClassicLookAndFeel";
                                        themename = "Windows Classic theme";
                                    }
                                    if (themesyntax != "")
                                    {
                                        RichTextBox rtb = new RichTextBox();
                                        StreamReader strreader = new StreamReader(Application.StartupPath + "\\files\\themesfile.slvjthemefile");
                                        rtb.Text = strreader.ReadToEnd();
                                        strreader.Close();
                                        strreader.Dispose();
                                        rtb.Text = rtb.Text.Replace("modifier", modifiers);
                                        rtb.Text = rtb.Text.Replace("classname", classname);
                                        rtb.Text = rtb.Text.Replace("themetype", themesyntax);
                                        rtb.Text = rtb.Text.Replace("projectname", prjname);
                                        rtb.Text = rtb.Text.Replace("themename", themename);


                                        if (mytabcontrol.TabPages.Count >= 0)
                                        {
                                            mytabpage.textEditor.Text = rtb.Text;
                                            mytabpage.Text = classfilename;
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

                        else
                        {

                            Directory.CreateDirectory(projectlocation);
                            String filename = projectlocation + "\\" + classfilename;
                            createdfilename = filename;

                            if (File.Exists(filename))
                            {
                                MessageBox.Show("The file name you entered is already exists in the folder or already added to your project", "Error......");
                            }

                            else
                            {

                                String filename2 = projectlocation + "\\" + classfilename;
                                createdfilename = filename2;
                                String themesyntax = "";
                                String themetext = getSelectedTheme();
                                if (themetext == "")
                                {
                                    MessageBox.Show("Please select any one of the choce", "Select");
                                }
                                else
                                {
                                    if (themetext == "metal")
                                    {
                                        themesyntax = "javax.swing.plaf.metal.MetalLookAndFeel";
                                    }
                                    else if (themetext == "motif")
                                    {
                                        themesyntax = "com.sun.java.swing.plaf.motif.MotifLookAndFeel";
                                    }
                                    else if (themetext == "nimbus")
                                    {
                                        themesyntax = "com.sun.java.swing.plaf.nimbus.NimbusLookAndFeel";
                                    }
                                    else if (themetext == "windows")
                                    {
                                        themesyntax = "com.sun.java.swing.plaf.windows.WindowsLookAndFeel";
                                    }
                                    else if (themetext == "windowsclassic")
                                    {
                                        themesyntax = "com.sun.java.swing.plaf.windows.WindowsClassicLookAndFeel";
                                    }
                                    if (themesyntax != "")
                                    {
                                        RichTextBox rtb = new RichTextBox();
                                        StreamReader strreader = new StreamReader(Application.StartupPath + "\\files\\themesfile.slvjthemefile");
                                        rtb.Text = strreader.ReadToEnd();
                                        strreader.Close();
                                        strreader.Dispose();
                                        rtb.Text = rtb.Text.Replace("modifier", modifiers);
                                        rtb.Text = rtb.Text.Replace("classname", classname);
                                        rtb.Text = rtb.Text.Replace("themetype", themesyntax);

                                        if (mytabcontrol.TabPages.Count >= 0)
                                        {
                                            mytabpage.textEditor.Text = rtb.Text;
                                            mytabpage.Text = classfilename;
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
                }
            }
        }


        //*****************************************************************************************
        //        Class TextBox Text Changed
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
        //        Super Class TextBox Text Changed
        //*****************************************************************************************
        private void SuperClassTextBox_TextChanged(object sender, EventArgs e)
        {
            String[] invalidchars = { " ", "~", "`", "!", "@", "#", "%", "^", "&", "*", "(", ")", "-", "+", "=", "[", "]", "{", "}", ":", ";", "\"", "'", "|", "\\", "<", ">", ",", ".", "?", "/" };

            Finishbutton.Enabled = true;

            if (SuperClassTextBox.Text == "")
            {
                errorlabel.Text = "";
                Finishbutton.Enabled = false;
            }

            for (int i = 0; i < invalidchars.Length; i++)
            {
                if (SuperClassTextBox.Text.Contains(invalidchars[i]))
                {
                    issuperinvalidcharcontain = true;
                    superinvalidchar = invalidchars[i];
                    Finishbutton.Enabled = false;
                }
            }

            if (superinvalidchar != "")
            {
                if (issuperinvalidcharcontain == true)
                {
                    if (superinvalidchar == " ")
                    {
                        errorlabel.Text = "Space is not allowed";
                        issuperinvalidcharcontain = false;
                        Finishbutton.Enabled = false;
                    }
                    else
                    {
                        errorlabel.Text = superinvalidchar + " Invalid Character";
                        issuperinvalidcharcontain = false;
                        Finishbutton.Enabled = false;
                    }
                }
                else
                {
                    errorlabel.Text = "";
                    issuperinvalidcharcontain = false;
                    Finishbutton.Enabled = true;
                }
            }
        }

        //*****************************************************************************************
        //        Interface TextBox Text Changed
        //*****************************************************************************************
        private void InterfaceTextBox_TextChanged(object sender, EventArgs e)
        {
            String[] invalidchars = { " ", "~", "`", "!", "@", "#", "%", "^", "&", "*", "(", ")", "-", "+", "=", "[", "]", "{", "}", ":", ";", "\"", "'", "|", "\\", "<", ">", ",", ".", "?", "/" };

            Finishbutton.Enabled = true;

            if (InterfaceTextBox.Text == "")
            {
                errorlabel.Text = "";
                Finishbutton.Enabled = false;
            }

            for (int i = 0; i < invalidchars.Length; i++)
            {
                if (InterfaceTextBox.Text.Contains(invalidchars[i]))
                {
                    isinterfaceinvalidcharcontain = true;
                    interfaceinvalidchar = invalidchars[i];
                    Finishbutton.Enabled = false;
                }
            }

            if (interfaceinvalidchar != "")
            {
                if (isinterfaceinvalidcharcontain == true)
                {
                    if (interfaceinvalidchar == " ")
                    {
                        errorlabel.Text = "Space is not allowed";
                        isinterfaceinvalidcharcontain = false;
                        Finishbutton.Enabled = false;
                    }
                    else
                    {
                        errorlabel.Text = interfaceinvalidchar + " Invalid Character";
                        isinterfaceinvalidcharcontain = false;
                        Finishbutton.Enabled = false;
                    }
                }
                else
                {
                    errorlabel.Text = "";
                    isinterfaceinvalidcharcontain = false;
                    Finishbutton.Enabled = true;
                }
            }
        }


        //*****************************************************************************************
        //        Create Class with Themes Check Box Checked Changed
        //*****************************************************************************************
        private void CreateClasswithThemesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CreateClasswithThemesCheckBox.Checked == true)
            {
                MetalThemeCheckBox.Enabled = true;
                MotifThemeCheckBox.Enabled = true;
                NimbusThemeCheckBox.Enabled = true;
                WindowsThemeCheckBox.Enabled = true;
                WindowsClassicThemeCheckBox.Enabled = true;
                AbstractCheckBox.Enabled = false;
                staticCheckBox.Enabled = false;
                FinalCheckBox.Enabled = false;
                SuperClassTextBox.Enabled = false;
                InterfaceTextBox.Enabled = false;
                JavaMainMethodCheckBox.Enabled = false;
                ConstructorCheckBox.Enabled = false;
            }
            else if (CreateClasswithThemesCheckBox.Checked == false)
            {
                MetalThemeCheckBox.Enabled = false;
                MotifThemeCheckBox.Enabled = false;
                NimbusThemeCheckBox.Enabled = false;
                WindowsThemeCheckBox.Enabled = false;
                WindowsClassicThemeCheckBox.Enabled = false;
                AbstractCheckBox.Enabled = true;
                staticCheckBox.Enabled = true;
                FinalCheckBox.Enabled = true;
                SuperClassTextBox.Enabled = true;
                InterfaceTextBox.Enabled = true;
                JavaMainMethodCheckBox.Enabled = true;
                ConstructorCheckBox.Enabled = true;
            }
        }


        //*****************************************************************************************
        //        Abstract Check Box Checked Changed
        //*****************************************************************************************
        private void AbstractCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AbstractCheckBox.Checked == true)
            {
                staticCheckBox.Checked = false;
                FinalCheckBox.Checked = false;
            }
        }

        //*****************************************************************************************
        //        static Check Box Checked Changed
        //*****************************************************************************************
        private void staticCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (staticCheckBox.Checked == true)
            {
                AbstractCheckBox.Checked = false;
                FinalCheckBox.Checked = false;
            }
        }

        //*****************************************************************************************
        //        Final Check Box Checked Changed
        //*****************************************************************************************
        private void FinalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (FinalCheckBox.Checked == true)
            {
                AbstractCheckBox.Checked = false;
                staticCheckBox.Checked = false;
            }
        }

        //*****************************************************************************************
        //        Java Main Medthod Check Box Checked Changed
        //*****************************************************************************************
        private void JavaMainMethodCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (JavaMainMethodCheckBox.Checked == true)
            {
                ConstructorCheckBox.Checked = false;
            }
        }


        //*****************************************************************************************
        //        Constructor Check Box Checked Changed
        //*****************************************************************************************
        private void ConstructorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ConstructorCheckBox.Checked == true)
            {
                JavaMainMethodCheckBox.Checked = false;
            }
        }

        //*****************************************************************************************
        //        Metal Theme Check Box Checked Changed
        //*****************************************************************************************
        private void MetalThemeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MetalThemeCheckBox.Checked == true)
            {
                MotifThemeCheckBox.Checked = false;
                NimbusThemeCheckBox.Checked = false;
                WindowsThemeCheckBox.Checked = false;
                WindowsClassicThemeCheckBox.Checked = false;
            }
        }

        //*****************************************************************************************
        //        Motif Theme Check Box Checked Changed
        //*****************************************************************************************
        private void MotifThemeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MotifThemeCheckBox.Checked == true)
            {
                MetalThemeCheckBox.Checked = false;
                NimbusThemeCheckBox.Checked = false;
                WindowsThemeCheckBox.Checked = false;
                WindowsClassicThemeCheckBox.Checked = false;
            }
        }

        //*****************************************************************************************
        //        Nimbus Theme Check Box Checked Changed
        //*****************************************************************************************
        private void NimbusThemeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (NimbusThemeCheckBox.Checked == true)
            {
                MetalThemeCheckBox.Checked = false;
                MotifThemeCheckBox.Checked = false;
                WindowsThemeCheckBox.Checked = false;
                WindowsClassicThemeCheckBox.Checked = false;
            }
        }

        //*****************************************************************************************
        //        Windows Theme Check Box Checked Changed
        //*****************************************************************************************
        private void WindowsThemeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (WindowsThemeCheckBox.Checked == true)
            {
                MetalThemeCheckBox.Checked = false;
                MotifThemeCheckBox.Checked = false;
                NimbusThemeCheckBox.Checked = false;
                WindowsClassicThemeCheckBox.Checked = false;
            }
        }

        //*****************************************************************************************
        //        Windows Classic Check Box Checked Changed
        //*****************************************************************************************
        private void WindowsClassicThemeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (WindowsClassicThemeCheckBox.Checked == true)
            {
                MetalThemeCheckBox.Checked = false;
                MotifThemeCheckBox.Checked = false;
                NimbusThemeCheckBox.Checked = false;
                WindowsThemeCheckBox.Checked = false;
            }
        }

        //*****************************************************************************************
        //        Finish Button Click
        //*****************************************************************************************
        private void Finishbutton_Click(object sender, EventArgs e)
        {
            if(CreateClasswithThemesCheckBox.Checked==false)
            {
                if(ClassTextBox.Text=="")
                {
                    errorlabel.Text = "Please Enter Class Name";
                    Finishbutton.Enabled = false;
                }
                else
                {
                    errorlabel.Text = "";
                    Finishbutton.Enabled = true;
                }

                if(ClassTextBox.Text!="")
                {
                    CreateClass();
                    this.Close();
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

                if (ClassTextBox.Text != "")
                {
                    CreateClass();
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
        //        ClassTextBox KeyDown
        //*****************************************************************************************
        private void ClassTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Finishbutton_Click(sender, e);
            }
        }

        //*****************************************************************************************
        //        SuperClassTextBox KeyDown
        //*****************************************************************************************
        private void SuperClassTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Finishbutton_Click(sender, e);
            }
        }

        //*****************************************************************************************
        //        InterfaceTextBox KeyDown
        //*****************************************************************************************
        private void InterfaceTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Finishbutton_Click(sender, e);
            }
        }
    }
}
