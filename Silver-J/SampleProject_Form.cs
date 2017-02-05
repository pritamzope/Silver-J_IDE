#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="SampleProject_Form.cs" company="">
  
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
using System.IO;
using System.Xml;

namespace Silver_J
{
    public partial class SampleProject_Form : Form
    {
        public SampleProject_Form()
        {
            InitializeComponent();
        }

        public static String project_file = "";
        public static String project_folder = "";
        public static String project_name = "";
        public static Boolean is_project_created = false;


        public String getProjectFile()
        {
            return project_file;
        }

        public Boolean isProjectCreated()
        {
            return is_project_created;
        }


        private void SampleProject_Form_Load(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = 0;
        }


        String defaultprojfilepath = Application.StartupPath + "\\files\\defaultprojloc.slvjfile";

        String[] basic_controls_demo_files = {
                                             Application.StartupPath+"\\Samples\\BasicControlsDemo\\BasicControlsDemo.slvjfile",
                                             Application.StartupPath+"\\Samples\\BasicControlsDemo\\ButtonsDemo.slvjfile",
                                             Application.StartupPath+"\\Samples\\BasicControlsDemo\\CheckBoxDemo.slvjfile",
                                             Application.StartupPath+"\\Samples\\BasicControlsDemo\\ComboBoxDemo.slvjfile",
                                             Application.StartupPath+"\\Samples\\BasicControlsDemo\\TabbedPaneDemo.slvjfile",
                                             Application.StartupPath+"\\Samples\\BasicControlsDemo\\TableDemo.slvjfile",
                                             Application.StartupPath+"\\Samples\\BasicControlsDemo\\TextAreaDemo.slvjfile",
                                             Application.StartupPath+"\\Samples\\BasicControlsDemo\\TextFieldDemo.slvjfile",
                                             Application.StartupPath+"\\Samples\\BasicControlsDemo\\TreeDemo.slvjfile"
                                             };

        String[] basic_controls_demo_2 ={
                                           "BasicControlsDemo.java",
                                           "ButtonsDemo.java",
                                           "CheckBoxDemo.java",
                                           "ComboBoxDemo.java",
                                           "TabbedPaneDemo.java",
                                           "TableDemo.java",
                                           "TextAreaDemo.java",
                                           "TextFieldDemo.java",
                                           "TreeDemo.java"
                                       };


        String[] bounceball_files = {
                                             Application.StartupPath+"\\Samples\\BounceBall\\BounceBall.slvjfile"
                                             };

        String[] bounceball_2 ={
                                           "BounceBall.java",
                                       };

        String[] buttons_demo_files = {
                                             Application.StartupPath+"\\Samples\\ButtonsDemo\\ButtonsDemo.slvjfile"
                                             };

        String[] buttons_demo_2 ={
                                           "ButtonsDemo.java",
                                       };

        String[] calculator_files = {
                                             Application.StartupPath+"\\Samples\\Calculator\\Calculator.slvjfile"
                                             };

        String[] calculator_2 ={
                                           "Calculator.java",
                                       };

        String[] fontstest_files = {
                                             Application.StartupPath+"\\Samples\\FontsTest\\FontsTest.slvjfile"
                                             };

        String[] fontstest_2 ={
                                           "FontsTest.java"
                                       };


        String[] graphicstest_files = {
                                             Application.StartupPath+"\\Samples\\GraphicsTest\\GraphicsTest.slvjfile",
                                             Application.StartupPath+"\\Samples\\GraphicsTest\\abc.jpg"
                                             };

        String[] graphicstest_2 ={
                                           "GraphicsTest.java"
                                       };


        String[] javahtmleditor_files = {
                                             Application.StartupPath+"\\Samples\\JavaHTMLEditor\\JavaHTMLEditor.slvjfile"
                                             };

        String[] javahtmleditor_2 ={
                                           "JavaHTMLEditor.java"
                                       };


        String[] lookandfeeldemo_demo_files = {
                                             Application.StartupPath+"\\Samples\\LookAndFeelDemo\\JavaBlueTheme.slvjfile",
                                             Application.StartupPath+"\\Samples\\LookAndFeelDemo\\JavaGreenTheme.slvjfile",
                                             Application.StartupPath+"\\Samples\\LookAndFeelDemo\\JavaRedTheme.slvjfile",
                                             Application.StartupPath+"\\Samples\\LookAndFeelDemo\\LookAndFeelDemo.slvjfile",
                                             Application.StartupPath+"\\Samples\\LookAndFeelDemo\\TestFrame.slvjfile",
                                             };

        String[] lookandfeeldemo_demo_2 ={
                                           "JavaBlueTheme.java",
                                           "JavaGreenTheme.java",
                                           "JavaRedTheme.java",
                                           "LookAndFeelDemo.java",
                                           "TestFrame.java",
                                       };

        String[] notepad_files = {
                                             Application.StartupPath+"\\Samples\\Notepad\\Notepad.slvjfile"
                                             };

        String[] notepad_2 ={
                                           "Notepad.java"
                                       };

        String[] paintapp_files = {
                                             Application.StartupPath+"\\Samples\\PaintApp\\PaintApp.slvjfile"
                                             };

        String[] paintapp_2 ={
                                           "PaintApp.java"
                                       };



        public String ProjectLocaionFoder()
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

            return projectfolder;
        }



        public void LoadProject()
        {
            if (ProjectLocaionFoder() == "" || ProjectLocaionFoder() == "null" || ProjectLocaionFoder() == "\n  ")
            {
                DialogResult dg = MessageBox.Show("Project Folder is not specified\nClick 'Yes' to select your project folder", "Error to Load", MessageBoxButtons.YesNo);
                if (dg == DialogResult.Yes)
                {
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        String path = folderBrowserDialog1.SelectedPath;

                        XmlDocument doc = new XmlDocument();
                        doc.Load(defaultprojfilepath);
                        doc.SelectSingleNode("SilverJ/DefaultProjectLocation").InnerText = path;
                        doc.Save(defaultprojfilepath);
                    }
                }
            }

            else if (ProjectLocaionFoder() != "" || ProjectLocaionFoder() != "null" || ProjectLocaionFoder() != " \n ")
            {

                String selected_item = listBox1.SelectedItem.ToString();
                String projectfolder = ProjectLocaionFoder();

                if (selected_item == "Basic Controls Demo")
                {
                    project_name = "BasicControlsDemo";

                    String directory = projectfolder + "\\BasicControlsDemo";
                    String projectfile = "BasicControlsDemo.slvjproj";

                    if (Directory.Exists(directory))
                    {
                        MessageBox.Show("The selected project is already exists in current location", "Error............");
                    }

                    else
                    {

                        Directory.CreateDirectory(directory);

                        if (Directory.Exists(directory))
                        {
                            project_folder = directory;

                            //File.Create(directory + "\\" + projectfile);

                            project_file = directory + "\\" + projectfile;
                        }

                        foreach (String item in basic_controls_demo_files)
                        {
                            if (File.Exists(item))
                            {
                                String filename = item;
                                String fname = filename.Substring(filename.LastIndexOf("\\") + 1);
                                fname = fname.Remove(fname.Length - 9);
                                fname = fname + ".java";

                                if (Directory.Exists(directory))
                                {
                                    String src = directory + "\\src";
                                    String srcclasses = directory + "\\srcclasses";
                                    String classes = directory + "\\classes";

                                    Directory.CreateDirectory(src);
                                    Directory.CreateDirectory(srcclasses);
                                    Directory.CreateDirectory(classes);

                                    String content = "";

                                    content = File.ReadAllText(item);

                                    if (Directory.Exists(srcclasses))
                                    {
                                        StreamWriter strw = new StreamWriter(File.Create(srcclasses + "\\" + fname));
                                        strw.Write(content);
                                        strw.Close();
                                        strw.Dispose();
                                    }

                                    //creating & writing slvjproj file 
                                    using (XmlWriter xmlwriter = XmlWriter.Create(directory + "\\" + projectfile))
                                    {
                                        xmlwriter.WriteStartDocument();
                                        xmlwriter.WriteStartElement("SilverJProject");
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteComment("Silver-J (1.0) Java Application Project");
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("ProjectName", "BasicControlsDemo");
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("ProjectLocationFolder", directory);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("ProjectLocationFile", directory + "\\" + projectfile);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("ProjectType", "ApplicationType");
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("MainClassFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[0]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[0]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[0]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[1]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[1]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[2]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[2]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[3]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[3]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[4]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[4]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[5]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[5]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[6]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[6]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[7]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[7]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[8]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + basic_controls_demo_2[8]);
                                        xmlwriter.WriteEndElement();
                                        xmlwriter.WriteEndDocument();
                                        xmlwriter.Close();
                                    }

                                }
                            }
                        }

                        is_project_created = true;
                    }
                }



                else if (selected_item == "BounceBall")
                {
                    project_name = "BounceBall";

                    String directory = projectfolder + "\\BounceBall";
                    String projectfile = "BounceBall.slvjproj";

                    if (Directory.Exists(directory))
                    {
                        MessageBox.Show("The selected project is already exists in current location", "Error............");
                    }

                    else
                    {

                        Directory.CreateDirectory(directory);

                        if (Directory.Exists(directory))
                        {
                            project_folder = directory;

                            project_file = directory + "\\" + projectfile;
                        }

                        if (File.Exists(bounceball_files[0]))
                        {
                            String filename = bounceball_files[0];
                            String fname = filename.Substring(filename.LastIndexOf("\\") + 1);
                            fname = fname.Remove(fname.Length - 9);
                            fname = fname + ".java";

                            if (Directory.Exists(directory))
                            {
                                String src = directory + "\\src";
                                String srcclasses = directory + "\\srcclasses";
                                String classes = directory + "\\classes";

                                Directory.CreateDirectory(src);
                                Directory.CreateDirectory(srcclasses);
                                Directory.CreateDirectory(classes);

                                String content = "";

                                content = File.ReadAllText(bounceball_files[0]);

                                if (Directory.Exists(srcclasses))
                                {
                                    StreamWriter strw = new StreamWriter(File.Create(srcclasses + "\\" + fname));
                                    strw.Write(content);
                                    strw.Close();
                                    strw.Dispose();
                                }

                                //creating & writing slvjproj file 
                                using (XmlWriter xmlwriter = XmlWriter.Create(directory + "\\" + projectfile))
                                {
                                    xmlwriter.WriteStartDocument();
                                    xmlwriter.WriteStartElement("SilverJProject");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteComment("Silver-J (1.0) Java Application Project");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectName", "BounceBall");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectLocationFolder", directory);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectLocationFile", directory + "\\" + projectfile);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectType", "ApplicationType");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("MainClassFile", directory + "\\srcclasses" + "\\" + bounceball_2[0]);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + bounceball_2[0]);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + bounceball_2[0]);
                                    xmlwriter.WriteEndElement();
                                    xmlwriter.WriteEndDocument();
                                    xmlwriter.Close();
                                }

                            }
                        }

                        is_project_created = true;
                    }
                }



                else if (selected_item == "ButtonsDemo")
                {
                    project_name = "ButtonsDemo";

                    String directory = projectfolder + "\\ButtonsDemo";
                    String projectfile = "ButtonsDemo.slvjproj";

                    if (Directory.Exists(directory))
                    {
                        MessageBox.Show("The selected project is already exists in current location", "Error............");
                    }

                    else
                    {

                        Directory.CreateDirectory(directory);

                        if (Directory.Exists(directory))
                        {
                            project_folder = directory;

                            project_file = directory + "\\" + projectfile;
                        }

                        if (File.Exists(buttons_demo_files[0]))
                        {
                            String filename = buttons_demo_files[0];
                            String fname = filename.Substring(filename.LastIndexOf("\\") + 1);
                            fname = fname.Remove(fname.Length - 9);
                            fname = fname + ".java";

                            if (Directory.Exists(directory))
                            {
                                String src = directory + "\\src";
                                String srcclasses = directory + "\\srcclasses";
                                String classes = directory + "\\classes";

                                Directory.CreateDirectory(src);
                                Directory.CreateDirectory(srcclasses);
                                Directory.CreateDirectory(classes);

                                String content = "";

                                content = File.ReadAllText(buttons_demo_files[0]);

                                if (Directory.Exists(srcclasses))
                                {
                                    StreamWriter strw = new StreamWriter(File.Create(srcclasses + "\\" + fname));
                                    strw.Write(content);
                                    strw.Close();
                                    strw.Dispose();
                                }

                                //creating & writing slvjproj file 
                                using (XmlWriter xmlwriter = XmlWriter.Create(directory + "\\" + projectfile))
                                {
                                    xmlwriter.WriteStartDocument();
                                    xmlwriter.WriteStartElement("SilverJProject");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteComment("Silver-J (1.0) Java Application Project");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectName", "ButtonsDemo");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectLocationFolder", directory);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectLocationFile", directory + "\\" + projectfile);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectType", "ApplicationType");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("MainClassFile", directory + "\\srcclasses" + "\\" + buttons_demo_2[0]);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + buttons_demo_2[0]);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + buttons_demo_2[0]);
                                    xmlwriter.WriteEndElement();
                                    xmlwriter.WriteEndDocument();
                                    xmlwriter.Close();
                                }

                            }
                        }

                        is_project_created = true;
                    }
                }




                else if (selected_item == "Calculator")
                {
                    project_name = "Calculator";

                    String directory = projectfolder + "\\Calculator";
                    String projectfile = "Calculator.slvjproj";

                    if (Directory.Exists(directory))
                    {
                        MessageBox.Show("The selected project is already exists in current location", "Error............");
                    }

                    else
                    {

                        Directory.CreateDirectory(directory);

                        if (Directory.Exists(directory))
                        {
                            project_folder = directory;

                            project_file = directory + "\\" + projectfile;
                        }

                        if (File.Exists(calculator_files[0]))
                        {
                            String filename = calculator_files[0];
                            String fname = filename.Substring(filename.LastIndexOf("\\") + 1);
                            fname = fname.Remove(fname.Length - 9);
                            fname = fname + ".java";

                            if (Directory.Exists(directory))
                            {
                                String src = directory + "\\src";
                                String srcclasses = directory + "\\srcclasses";
                                String classes = directory + "\\classes";

                                Directory.CreateDirectory(src);
                                Directory.CreateDirectory(srcclasses);
                                Directory.CreateDirectory(classes);

                                String content = "";

                                content = File.ReadAllText(calculator_files[0]);

                                if (Directory.Exists(srcclasses))
                                {
                                    StreamWriter strw = new StreamWriter(File.Create(srcclasses + "\\" + fname));
                                    strw.Write(content);
                                    strw.Close();
                                    strw.Dispose();
                                }

                                //creating & writing slvjproj file 
                                using (XmlWriter xmlwriter = XmlWriter.Create(directory + "\\" + projectfile))
                                {
                                    xmlwriter.WriteStartDocument();
                                    xmlwriter.WriteStartElement("SilverJProject");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteComment("Silver-J (1.0) Java Application Project");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectName", "Calculator");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectLocationFolder", directory);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectLocationFile", directory + "\\" + projectfile);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectType", "ApplicationType");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("MainClassFile", directory + "\\srcclasses" + "\\" + calculator_2[0]);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + calculator_2[0]);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + calculator_2[0]);
                                    xmlwriter.WriteEndElement();
                                    xmlwriter.WriteEndDocument();
                                    xmlwriter.Close();
                                }

                            }
                        }

                        is_project_created = true;
                    }
                }



                else if (selected_item == "FontsTest")
                {
                    project_name = "FontsTest";

                    String directory = projectfolder + "\\FontsTest";
                    String projectfile = "FontsTest.slvjproj";

                    if (Directory.Exists(directory))
                    {
                        MessageBox.Show("The selected project is already exists in current location", "Error............");
                    }

                    else
                    {

                        Directory.CreateDirectory(directory);

                        if (Directory.Exists(directory))
                        {
                            project_folder = directory;

                            project_file = directory + "\\" + projectfile;
                        }

                        if (File.Exists(fontstest_files[0]))
                        {
                            String filename = fontstest_files[0];
                            String fname = filename.Substring(filename.LastIndexOf("\\") + 1);
                            fname = fname.Remove(fname.Length - 9);
                            fname = fname + ".java";

                            if (Directory.Exists(directory))
                            {
                                String src = directory + "\\src";
                                String srcclasses = directory + "\\srcclasses";
                                String classes = directory + "\\classes";

                                Directory.CreateDirectory(src);
                                Directory.CreateDirectory(srcclasses);
                                Directory.CreateDirectory(classes);

                                String content = "";

                                content = File.ReadAllText(fontstest_files[0]);

                                if (Directory.Exists(srcclasses))
                                {
                                    StreamWriter strw = new StreamWriter(File.Create(srcclasses + "\\" + fname));
                                    strw.Write(content);
                                    strw.Close();
                                    strw.Dispose();

                                }

                                //creating & writing slvjproj file 
                                using (XmlWriter xmlwriter = XmlWriter.Create(directory + "\\" + projectfile))
                                {
                                    xmlwriter.WriteStartDocument();
                                    xmlwriter.WriteStartElement("SilverJProject");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteComment("Silver-J (1.0) Java Application Project");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectName", "FontsTest");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectLocationFolder", directory);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectLocationFile", directory + "\\" + projectfile);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectType", "ApplicationType");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("MainClassFile", directory + "\\srcclasses" + "\\" + fontstest_2[0]);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + fontstest_2[0]);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + fontstest_2[0]);
                                    xmlwriter.WriteEndElement();
                                    xmlwriter.WriteEndDocument();
                                    xmlwriter.Close();
                                }

                            }
                        }

                        is_project_created = true;
                    }
                }




                else if (selected_item == "GraphicsTest")
                {
                    project_name = "GraphicsTest";

                    String directory = projectfolder + "\\GraphicsTest";
                    String projectfile = "GraphicsTest.slvjproj";

                    if (Directory.Exists(directory))
                    {
                        MessageBox.Show("The selected project is already exists in current location", "Error............");
                    }

                    else
                    {

                        Directory.CreateDirectory(directory);

                        if (Directory.Exists(directory))
                        {
                            project_folder = directory;

                            project_file = directory + "\\" + projectfile;
                        }

                        if (File.Exists(graphicstest_files[0]))
                        {
                            String filename = graphicstest_files[0];
                            String fname = filename.Substring(filename.LastIndexOf("\\") + 1);
                            fname = fname.Remove(fname.Length - 9);
                            fname = fname + ".java";

                            if (Directory.Exists(directory))
                            {
                                String src = directory + "\\src";
                                String srcclasses = directory + "\\srcclasses";
                                String classes = directory + "\\classes";

                                Directory.CreateDirectory(src);
                                Directory.CreateDirectory(srcclasses);
                                Directory.CreateDirectory(classes);

                                String content = "";

                                content = File.ReadAllText(graphicstest_files[0]);

                                if (Directory.Exists(srcclasses))
                                {
                                    StreamWriter strw = new StreamWriter(File.Create(srcclasses + "\\" + fname));
                                    strw.Write(content);
                                    strw.Close();
                                    strw.Dispose();

                                    if (File.Exists(graphicstest_files[1]))
                                    {
                                        File.Copy(graphicstest_files[1], srcclasses + "\\abc.jpg");
                                    }

                                }

                                //creating & writing slvjproj file 
                                using (XmlWriter xmlwriter = XmlWriter.Create(directory + "\\" + projectfile))
                                {
                                    xmlwriter.WriteStartDocument();
                                    xmlwriter.WriteStartElement("SilverJProject");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteComment("Silver-J (1.0) Java Application Project");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectName", "GraphicsTest");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectLocationFolder", directory);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectLocationFile", directory + "\\" + projectfile);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectType", "ApplicationType");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("MainClassFile", directory + "\\srcclasses" + "\\" + graphicstest_2[0]);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + graphicstest_2[0]);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + graphicstest_2[0]);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("OtherFile", directory + "\\srcclasses" + "\\abc.jpg");
                                    xmlwriter.WriteEndElement();
                                    xmlwriter.WriteEndDocument();
                                    xmlwriter.Close();
                                }

                            }
                        }

                        is_project_created = true;
                    }
                }



                else if (selected_item == "HTML Editor in Java")
                {
                    project_name = "JavaHTMLEditor";

                    String directory = projectfolder + "\\JavaHTMLEditor";
                    String projectfile = "JavaHTMLEditor.slvjproj";

                    if (Directory.Exists(directory))
                    {
                        MessageBox.Show("The selected project is already exists in current location", "Error............");
                    }

                    else
                    {

                        Directory.CreateDirectory(directory);

                        if (Directory.Exists(directory))
                        {
                            project_folder = directory;

                            project_file = directory + "\\" + projectfile;
                        }

                        if (File.Exists(javahtmleditor_files[0]))
                        {
                            String filename = javahtmleditor_files[0];
                            String fname = filename.Substring(filename.LastIndexOf("\\") + 1);
                            fname = fname.Remove(fname.Length - 9);
                            fname = fname + ".java";

                            if (Directory.Exists(directory))
                            {
                                String src = directory + "\\src";
                                String srcclasses = directory + "\\srcclasses";
                                String classes = directory + "\\classes";

                                Directory.CreateDirectory(src);
                                Directory.CreateDirectory(srcclasses);
                                Directory.CreateDirectory(classes);

                                String content = "";

                                content = File.ReadAllText(javahtmleditor_files[0]);

                                if (Directory.Exists(srcclasses))
                                {
                                    StreamWriter strw = new StreamWriter(File.Create(srcclasses + "\\" + fname));
                                    strw.Write(content);
                                    strw.Close();
                                    strw.Dispose();

                                }

                                //creating & writing slvjproj file 
                                using (XmlWriter xmlwriter = XmlWriter.Create(directory + "\\" + projectfile))
                                {
                                    xmlwriter.WriteStartDocument();
                                    xmlwriter.WriteStartElement("SilverJProject");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteComment("Silver-J (1.0) Java Application Project");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectName", "JavaHTMLEditor");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectLocationFolder", directory);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectLocationFile", directory + "\\" + projectfile);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectType", "ApplicationType");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("MainClassFile", directory + "\\srcclasses" + "\\" + javahtmleditor_2[0]);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + javahtmleditor_2[0]);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + javahtmleditor_2[0]);
                                    xmlwriter.WriteEndElement();
                                    xmlwriter.WriteEndDocument();
                                    xmlwriter.Close();
                                }

                            }
                        }

                        is_project_created = true;
                    }
                }


                else if (selected_item == "Look and Feel Demo")
                {
                    project_name = "LookAndFeelDemo";

                    String directory = projectfolder + "\\LookAndFeelDemo";
                    String projectfile = "LookAndFeelDemo.slvjproj";

                    if (Directory.Exists(directory))
                    {
                        MessageBox.Show("The selected project is already exists in current location", "Error............");
                    }

                    else
                    {
                        Directory.CreateDirectory(directory);

                        if (Directory.Exists(directory))
                        {
                            project_folder = directory;

                            //File.Create(directory + "\\" + projectfile);

                            project_file = directory + "\\" + projectfile;
                        }

                        foreach (String item in lookandfeeldemo_demo_files)
                        {
                            if (File.Exists(item))
                            {
                                String filename = item;
                                String fname = filename.Substring(filename.LastIndexOf("\\") + 1);
                                fname = fname.Remove(fname.Length - 9);
                                fname = fname + ".java";

                                if (Directory.Exists(directory))
                                {
                                    String src = directory + "\\src";
                                    String srcclasses = directory + "\\srcclasses";
                                    String classes = directory + "\\classes";

                                    Directory.CreateDirectory(src);
                                    Directory.CreateDirectory(srcclasses);
                                    Directory.CreateDirectory(classes);

                                    String content = "";

                                    content = File.ReadAllText(item);

                                    if (Directory.Exists(srcclasses))
                                    {
                                        StreamWriter strw = new StreamWriter(File.Create(srcclasses + "\\" + fname));
                                        strw.Write(content);
                                        strw.Close();
                                        strw.Dispose();
                                    }

                                    //creating & writing slvjproj file 
                                    using (XmlWriter xmlwriter = XmlWriter.Create(directory + "\\" + projectfile))
                                    {
                                        xmlwriter.WriteStartDocument();
                                        xmlwriter.WriteStartElement("SilverJProject");
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteComment("Silver-J (1.0) Java Application Project");
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("ProjectName", "LookAndFeelDemo");
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("ProjectLocationFolder", directory);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("ProjectLocationFile", directory + "\\" + projectfile);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("ProjectType", "ApplicationType");
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("MainClassFile", directory + "\\srcclasses" + "\\" + lookandfeeldemo_demo_2[3]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + lookandfeeldemo_demo_2[0]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + lookandfeeldemo_demo_2[0]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + lookandfeeldemo_demo_2[1]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + lookandfeeldemo_demo_2[1]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + lookandfeeldemo_demo_2[2]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + lookandfeeldemo_demo_2[2]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + lookandfeeldemo_demo_2[3]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + lookandfeeldemo_demo_2[3]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + lookandfeeldemo_demo_2[4]);
                                        xmlwriter.WriteString("\n");
                                        xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + lookandfeeldemo_demo_2[4]);
                                        xmlwriter.WriteEndElement();
                                        xmlwriter.WriteEndDocument();
                                        xmlwriter.Close();
                                    }

                                }
                            }
                        }

                        is_project_created = true;
                    }
                }


                else if (selected_item == "Notepad")
                {
                    project_name = "Notepad";

                    String directory = projectfolder + "\\Notepad";
                    String projectfile = "Notepad.slvjproj";

                    if (Directory.Exists(directory))
                    {
                        MessageBox.Show("The selected project is already exists in current location", "Error............");
                    }

                    else
                    {

                        Directory.CreateDirectory(directory);

                        if (Directory.Exists(directory))
                        {
                            project_folder = directory;

                            project_file = directory + "\\" + projectfile;
                        }

                        if (File.Exists(notepad_files[0]))
                        {
                            String filename = notepad_files[0];
                            String fname = filename.Substring(filename.LastIndexOf("\\") + 1);
                            fname = fname.Remove(fname.Length - 9);
                            fname = fname + ".java";

                            if (Directory.Exists(directory))
                            {
                                String src = directory + "\\src";
                                String srcclasses = directory + "\\srcclasses";
                                String classes = directory + "\\classes";

                                Directory.CreateDirectory(src);
                                Directory.CreateDirectory(srcclasses);
                                Directory.CreateDirectory(classes);

                                String content = "";

                                content = File.ReadAllText(notepad_files[0]);

                                if (Directory.Exists(srcclasses))
                                {
                                    StreamWriter strw = new StreamWriter(File.Create(srcclasses + "\\" + fname));
                                    strw.Write(content);
                                    strw.Close();
                                    strw.Dispose();
                                }

                                //creating & writing slvjproj file 
                                using (XmlWriter xmlwriter = XmlWriter.Create(directory + "\\" + projectfile))
                                {
                                    xmlwriter.WriteStartDocument();
                                    xmlwriter.WriteStartElement("SilverJProject");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteComment("Silver-J (1.0) Java Application Project");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectName", "Notepad");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectLocationFolder", directory);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectLocationFile", directory + "\\" + projectfile);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectType", "ApplicationType");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("MainClassFile", directory + "\\srcclasses" + "\\" + notepad_2[0]);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + notepad_2[0]);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + notepad_2[0]);
                                    xmlwriter.WriteEndElement();
                                    xmlwriter.WriteEndDocument();
                                    xmlwriter.Close();
                                }

                            }
                        }

                        is_project_created = true;
                    }
                }


                else if (selected_item == "PaintApp")
                {
                    project_name = "PaintApp";

                    String directory = projectfolder + "\\PaintApp";
                    String projectfile = "PaintApp.slvjproj";

                    if (Directory.Exists(directory))
                    {
                        MessageBox.Show("The selected project is already exists in current location", "Error............");
                    }

                    else
                    {

                        Directory.CreateDirectory(directory);

                        if (Directory.Exists(directory))
                        {
                            project_folder = directory;

                            project_file = directory + "\\" + projectfile;
                        }

                        if (File.Exists(paintapp_files[0]))
                        {
                            String filename = paintapp_files[0];
                            String fname = filename.Substring(filename.LastIndexOf("\\") + 1);
                            fname = fname.Remove(fname.Length - 9);
                            fname = fname + ".java";

                            if (Directory.Exists(directory))
                            {
                                String src = directory + "\\src";
                                String srcclasses = directory + "\\srcclasses";
                                String classes = directory + "\\classes";

                                Directory.CreateDirectory(src);
                                Directory.CreateDirectory(srcclasses);
                                Directory.CreateDirectory(classes);

                                String content = "";

                                content = File.ReadAllText(paintapp_files[0]);

                                if (Directory.Exists(srcclasses))
                                {
                                    StreamWriter strw = new StreamWriter(File.Create(srcclasses + "\\" + fname));
                                    strw.Write(content);
                                    strw.Close();
                                    strw.Dispose();
                                }

                                //creating & writing slvjproj file 
                                using (XmlWriter xmlwriter = XmlWriter.Create(directory + "\\" + projectfile))
                                {
                                    xmlwriter.WriteStartDocument();
                                    xmlwriter.WriteStartElement("SilverJProject");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteComment("Silver-J (1.0) Java Application Project");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectName", "PaintApp");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectLocationFolder", directory);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectLocationFile", directory + "\\" + projectfile);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("ProjectType", "ApplicationType");
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("MainClassFile", directory + "\\srcclasses" + "\\" + paintapp_2[0]);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("JavaClassFile", directory + "\\srcclasses" + "\\" + paintapp_2[0]);
                                    xmlwriter.WriteString("\n");
                                    xmlwriter.WriteElementString("VisualFile", directory + "\\srcclasses" + "\\" + paintapp_2[0]);
                                    xmlwriter.WriteEndElement();
                                    xmlwriter.WriteEndDocument();
                                    xmlwriter.Close();
                                }

                            }
                        }

                        is_project_created = true;
                    }
                }

            }

            else
            {
                DialogResult dg = MessageBox.Show("Project Folder is not specified\nClick 'Yes' to select your project folder", "Error to Load", MessageBoxButtons.YesNo);
                if (dg == DialogResult.Yes)
                {
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        String path = folderBrowserDialog1.SelectedPath;

                        XmlDocument doc = new XmlDocument();
                        doc.Load(defaultprojfilepath);
                        doc.SelectSingleNode("SilverJ/DefaultProjectLocation").InnerText = path;
                        doc.Save(defaultprojfilepath);
                    }
                }
            }


        }





        private void OKbutton_Click(object sender, EventArgs e)
        {

            LoadProject();
            this.Close();
        }


        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
