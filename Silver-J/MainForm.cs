#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="MainForm.cs" company="">
  
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
using System.Collections;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using System.Xml;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using ICSharpCode.TextEditor.Document;
namespace Silver_J
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// packagelist - Global variable to hold 'PackageFolderName' read from projct file
        /// openfileslist - Global variable to hold file names for File->Open Files menu option
        /// process - Global variable to start Browser with html file(View->View In Web Browser)
        ///           Start the process for Compile main java class file
        ///           Start the process for Run,Run Applet,Run with Parameter options
        /// ErrorReader - Global variable to read the output from started process
        /// showErrorDialog - Variable to identify whether to show error dialog when 
        ///                   compile,run process output is read
        /// </summary>
        public List<String> packagelist = new List<String> { };
        public List<String> openfileslist = new List<String> { };
        public Process process = new Process();
        public string javaPath;
        public StreamReader ErrorReader;
        public StreamReader OutputReader;
        public static Boolean showErrorDialog = true;



        //**************************************************************************************************************
        //      Read Current Project File Name
        //**************************************************************************************************************
        /// <summary>
        /// Reads a 'CurrentProjectFileName' tag from file \files\defaultprojloc.slvjfile
        /// </summary>
        /// <returns>String as a current opened project file name with full path</returns>
        public String ReadCurrentProjectFileName()
        {
            String s = "";
            String defaultprojfilepath = Application.StartupPath + "\\files\\defaultprojloc.slvjfile";
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


        //**************************************************************************************************************
        //      get Current Project Type
        //**************************************************************************************************************
        /// <summary>
        /// Reads a current opened project file name
        /// match tag with 'ProjectType'
        /// it could be 'ApplicationType' or 'AppletType'
        /// </summary>
        /// <returns>Type as a String</returns>
        public String getCurrentProjectType()
        {
            String s = "";

            using (XmlReader reader = XmlReader.Create(ReadCurrentProjectFileName()))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "ProjectType":
                                s = reader.ReadString();
                                break;
                        }
                    }
                }
            }
            return s;
        }


        //**************************************************************************************************************
        //      get Current Project Location Folder
        //**************************************************************************************************************
        /// <summary>
        /// Reads a current project file name
        /// match tag with 'ProjectLocationFolder'
        /// </summary>
        /// <returns>Project folder path as string</returns>
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


        //**************************************************************************************************************
        //      get Current Project Name
        //**************************************************************************************************************
        /// <summary>
        /// Reads current project file name
        /// match tag with 'ProjectName'
        /// </summary>
        /// <returns>Project name as String</returns>
        public String getCurrentProjectName()
        {
            String projectfile = ReadCurrentProjectFileName();
            String projectname = "";
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
                                case "ProjectName":
                                    projectname = reader.ReadString();
                                    break;
                            }
                        }
                    }
                }
            }
            return projectname;
        }



        //**************************************************************************************************************
        //      Add Files To ProjectExplorerTreeView
        //**************************************************************************************************************
        /// <summary>
        /// Adding file names to 'ProjectExplorerTreeView' by reading 
        /// current opened project file
        /// </summary>
        public void AddFilesToProjectExplorerTreeView()
        {
            // get current opened project file name
            String projectfile = ReadCurrentProjectFileName();
            
            //variable to hold current opened project name
            String prjname = "";
            
            //variables to hold filenames(mylist), 
            //created java packages file names(packagejavafilelist)
            // and added files to project names(otherfileslist)
            List<String> mylist = new List<String> { };
            List<String> packagejavafilelist = new List<String> { };
            List<String> otherfileslist = new List<String> { };
            String packagename = "";
            //clear the package list
            packagelist.Clear();

            //first check current project file exists or not
            //if exists read each tag using XmlReader and add files to created variables 
            //  according to their tag names
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
                                case "ProjectName":
                                    prjname = reader.ReadString();
                                    break;

                                case "JavaClassFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "HTMLFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "CSSFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "TextFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "JavaScriptFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "SQLFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "XMLFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "NewFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "PackageName":
                                    packagename = reader.ReadString();
                                    break;

                                case "PackageFolderName":
                                    packagelist.Add(reader.ReadString());
                                    break;

                                case "PackageJavaClassFile":
                                    packagejavafilelist.Add(reader.ReadString());
                                    break;

                                case "OtherFile":
                                    otherfileslist.Add(reader.ReadString());
                                    break;
                            }
                        }
                    }
                }
                //check project name != ""
                if (prjname != "")
                {
                    //creating tree node,adding image index to it & adding it to 
                    // projectExplorerTreeView
                    //first add the project name
                    ProjectExplorerTreeView.Nodes.Clear();
                    TreeNode prjnode = new TreeNode();
                    prjnode.Text = prjname;
                    prjnode.ImageIndex = 9;
                    prjnode.SelectedImageIndex = 9;
                    ProjectExplorerTreeView.Nodes.Add(prjnode);

                    //getting each file name from added file names list for
                    //reading their extension for adding/setting image index
                    foreach (String line in mylist)
                    {
                        if (line != "")
                        {
                            String strline = line.Substring(line.LastIndexOf("\\") + 1);
                            int imgindex = 0;
                            if (strline.Contains(".java"))
                            {
                                imgindex = 5;
                            }
                            else if (strline.Contains(".html"))
                            {
                                imgindex = 2;
                            }
                            else if (strline.Contains(".css"))
                            {
                                imgindex = 1;
                            }
                            else if (strline.Contains(".js"))
                            {
                                imgindex = 6;
                            }
                            else if (strline.Contains(".txt"))
                            {
                                imgindex = 11;
                            }
                            else if (strline.Contains(".sql"))
                            {
                                imgindex = 10;
                            }
                            else if (strline.Contains(".xml"))
                            {
                                imgindex = 12;
                            }
                            else if (strline.Contains(".class"))
                            {
                                imgindex = 0;
                            }
                            else if (strline.Contains(".jar"))
                            {
                                imgindex = 4;
                            }
                            else if (strline.Contains(".c") || strline.Contains(".cpp")||strline.Contains(".c++"))
                            {
                                imgindex = 13;
                            }
                            else if (strline.Contains(".h"))
                            {
                                imgindex = 14;
                            }
                            else if (strline.Contains(".bmp") || strline.Contains(".dib") || strline.Contains(".jpg") || strline.Contains(".jpeg") || strline.Contains(".gif") || strline.Contains(".tif") || strline.Contains(".tiff") || strline.Contains(".png"))
                            {
                                imgindex = 3;
                            }
                            else
                            {
                                imgindex = 7;
                            }

                            //creating tree node and adding it to projectExpTree view under project
                            //name tree node
                            TreeNode prjnode2 = ProjectExplorerTreeView.Nodes[0];
                            TreeNode trnode = new TreeNode();
                            trnode.Text = strline;
                            trnode.ImageIndex = imgindex;
                            trnode.SelectedImageIndex = imgindex;
                            prjnode2.Nodes.Add(trnode);
                            ProjectExplorerTreeView.ExpandAll();
                        }
                    }

                    //add all package folder files to ProjectExplorerTreeView
                    foreach (String packagefolderpath in packagelist)
                    {
                        foreach (String packfile in packagejavafilelist)
                        {
                            String strline = packfile.Substring(packfile.LastIndexOf("\\") + 1);

                            if (File.Exists(packagefolderpath + "\\" + strline))
                            {
                                if (packfile != "")
                                {
                                    int imgindex = 0;
                                    //String packfolder = packagefolderpath.Substring(packagefolderpath.LastIndexOf("\\") + 1);
                                    String packfolder = packagename.Replace("\\", ".");
                                    strline = "[Package File-(" + packfolder + ")] - " + strline;
                                    if (strline.Contains(".java"))
                                    {
                                        imgindex = 8;
                                    }
                                    TreeNode prjnode2 = ProjectExplorerTreeView.Nodes[0];
                                    TreeNode trnode = new TreeNode();
                                    trnode.Text = strline;
                                    trnode.ImageIndex = imgindex;
                                    trnode.SelectedImageIndex = imgindex;
                                    prjnode2.Nodes.Add(trnode);
                                    ProjectExplorerTreeView.ExpandAll();
                                }
                            }
                        }
                    }

                    //add other selected files to ProjectExplorerTreeView
                    foreach (String file in otherfileslist)
                    {
                        if (file != "")
                        {
                            String filename = file.Substring(file.LastIndexOf("\\") + 1);
                            TreeNode trnode = new TreeNode();
                            trnode.Text = filename;
                            int imgindex2 = 0;
                            if (filename.Contains(".java"))
                            {
                                imgindex2 = 5;
                            }
                            else if (filename.Contains(".html"))
                            {
                                imgindex2 = 2;
                            }
                            else if (filename.Contains(".css"))
                            {
                                imgindex2 = 1;
                            }
                            else if (filename.Contains(".js"))
                            {
                                imgindex2 = 6;
                            }
                            else if (filename.Contains(".txt"))
                            {
                                imgindex2 = 11;
                            }
                            else if (filename.Contains(".sql"))
                            {
                                imgindex2 = 10;
                            }
                            else if (filename.Contains(".xml"))
                            {
                                imgindex2 = 12;
                            }
                            else if (filename.Contains(".class"))
                            {
                                imgindex2 = 0;
                            }
                            else if (filename.Contains(".jar"))
                            {
                                imgindex2 = 4;
                            }
                            else if (filename.Contains(".c") || filename.Contains(".cpp") || filename.Contains(".c++"))
                            {
                                imgindex2 = 13;
                            }
                            else if (filename.Contains(".h"))
                            {
                                imgindex2 = 14;
                            }
                            else if (filename.Contains(".bmp") || filename.Contains(".dib") || filename.Contains(".jpg") || filename.Contains(".jpeg") || filename.Contains(".gif") || filename.Contains(".tif") || filename.Contains(".tiff") || filename.Contains(".png"))
                            {
                                imgindex2 = 3;
                            }
                            else
                            {
                                imgindex2 = 7;
                            }
                            trnode.ImageIndex = imgindex2;
                            trnode.SelectedImageIndex = imgindex2;
                            ProjectExplorerTreeView.Nodes.Add(trnode);
                        }
                    }
                }
            }
        }


        //**************************************************************************************************************
        //      Copy All Source Files To SRC Folder
        //**************************************************************************************************************
        /// <summary>
        /// Copying all Java(.java) source files to src folder
        /// from srcclasses folder
        /// </summary>
        public void CopyAllSourceFilesToSRCFolder()
        {
            String projectfile = ReadCurrentProjectFileName();
            List<String> mylist = new List<String> { };

            //first getting all file names from folder by reading current project file name
            //and adding that file name to list
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
                                case "JavaClassFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "HTMLFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "CSSFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "TextFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "JavaScriptFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "SQLFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "XMLFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "NewFile":
                                    mylist.Add(reader.ReadString());
                                    break;
                            }
                        }
                    }
                }
            }

            //copying all files to src folder
            //if src directory exists then remove all files from src folder and copy files from srcclasses folder
            //otherwise create src folder and copy all files
            String projectlocationfolder = getCurrentProjectLocationFolder();
            if (projectlocationfolder != "")
            {
                if (Directory.Exists(projectlocationfolder + "\\src"))
                {
                    //delete all files from src folder
                    String[] files = Directory.GetFiles(projectlocationfolder + "\\src");
                    foreach (String file in files)
                    {
                        File.Delete(file);
                    }
                    //copy all files in mylist from srcclasses folder to src
                    foreach (String cpyfile in mylist)
                    {
                        String file2 = cpyfile.Replace("srcclasses", "src");
                        File.Copy(cpyfile, file2);
                    }
                }
                else
                {
                    Directory.CreateDirectory(projectlocationfolder + "\\src");
                    //delete all files from src folder
                    String[] files = Directory.GetFiles(projectlocationfolder + "\\src");
                    foreach (String file in files)
                    {
                        File.Delete(file);
                    }
                    //copy all files in mylist from srcclasses folder to src
                    foreach (String cpyfile in mylist)
                    {
                        String file2 = cpyfile.Replace("srcclasses", "src");
                        File.Copy(cpyfile, file2);
                    }
                }
            }
        }



        //**************************************************************************************************************
        //      Write Current File Names
        //**************************************************************************************************************
        /// <summary>
        /// Getting all file names from current opened project file name and adding those to list
        /// Writing these file names to '\files\files.slvjfile' file
        /// For keeping track of that file operations like Save,also change status filename label
        /// </summary>
        void WriteCurrentFileNames()
        {
            List<String> mylist = new List<String> { };
            String projectfile = ReadCurrentProjectFileName();
            mylist.Clear();

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
                                case "JavaClassFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "HTMLFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "CSSFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "TextFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "JavaScriptFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "SQLFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "XMLFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "NewFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "PackageJavaClassFile":
                                    mylist.Add(reader.ReadString());
                                    break;
                            }
                        }
                    }
                }

                //i created a RichTextBox object and adding those files by new line
                //and writing contents of that richTextBox to '\files\files.slvjfile'
                RichTextBox rtb = new RichTextBox();
                foreach (String file in mylist)
                {
                    rtb.Text = rtb.Text.Insert(rtb.SelectionStart, file + "\n");
                }

                String defaultprojfilepath = Application.StartupPath + "\\files\\files.slvjfile";
                File.WriteAllText(defaultprojfilepath, "");
                File.WriteAllText(defaultprojfilepath, rtb.Text);
            }
        }


        //*************************************************************************************
        // Update windows list to Window menu
        //*************************************************************************************
        /// <summary>
        /// Reads all the tabs from myTabControl
        /// Create ToolStripMenuItem and adding names same as tab name
        /// Adding menuitem to 'Window' menu of 'MyMenuStripZ'
        /// </summary>
        public void UpdateWindowsList_WindowMenu()
        {
            //getting all tabs from tabcontrol
            TabControl.TabPageCollection tabcoll = myTabControl.TabPages;

            int n = WindowMenuItem.DropDownItems.Count;
            for (int i = n - 1; i >= 4; i--)
            {
                WindowMenuItem.DropDownItems.RemoveAt(i);
            }

            //getting each tab,create menu item and adding it to 'Window' menu
            foreach (TabPage tabpage in tabcoll)
            {
                ToolStripMenuItem menuitem = new ToolStripMenuItem();
                String s = tabpage.Text;
                menuitem.Text = s;
                if (myTabControl.SelectedTab == tabpage)
                {
                    menuitem.Checked = true;
                }
                else
                {
                    menuitem.Checked = false;
                }
                WindowMenuItem.DropDownItems.Add(menuitem);
                //add click event to created menu item
                menuitem.Click += new System.EventHandler(WindowListEvent_Click);
            }
        }

        private void WindowListEvent_Click(object sender, EventArgs e)
        {
            //select tab when clicked on added menu item of Window menu
            ToolStripItem toolstripitem = (ToolStripItem)sender;
            TabControl.TabPageCollection tabcoll = myTabControl.TabPages;
            foreach (TabPage tb in tabcoll)
            {
                if (toolstripitem.Text == tb.Text)
                {
                    myTabControl.SelectedTab = tb;
                    UpdateWindowsList_WindowMenu();
                }
            }
        }



        //*************************************************************************************
        //         RemoveFileNamesByRemovingTabs()
        //*************************************************************************************
        /// <summary>
        /// Remove file names from \files\files.slvjfile when that file name tab is closed or removed
        /// </summary>
        public void RemoveFileNamesByRemovingTabs()
        {
            if (myTabControl.TabCount > 0)
            {
                System.Windows.Forms.TabControl.TabPageCollection tabcoll = myTabControl.TabPages;
                String file = Application.StartupPath + "\\files\\files.slvjfile";
                RichTextBox rtb = new RichTextBox();
                rtb.Text = File.ReadAllText(file);
                String[] lines = rtb.Lines;
                RichTextBox rtb2 = new RichTextBox();

                foreach (TabPage tabpage in tabcoll)
                {
                    foreach (String line in lines)
                    {
                        String fname = line.Substring(line.LastIndexOf("\\") + 1);

                        if (tabpage.Text == fname)
                        {
                            rtb2.Text = rtb2.Text.Insert(rtb2.SelectionStart, line + "\n");
                        }
                        else
                        {

                        }
                    }
                }

                File.WriteAllText(file, "");
                StreamWriter strw = new StreamWriter(file);
                strw.Write(rtb2.Text);
                strw.Close();
                strw.Dispose();
            }
        }


        //*************************************************************************************
        //  Remove VisualFile names from project file
        //*************************************************************************************
        /// <summary>
        /// Removes the node where that node text is equal to filename
        /// 'VisualFile' is the tag name in project file that can be read and opened when
        /// project is opened
        /// e.g:)"<VisualFile>C:\My Java Projects\BasicControlsDemo\srcclasses\ButtonsDemo.java</VisualFile>"
        /// Above file is read and opened when project is loaded/opened
        /// </summary>
        /// <param name="filename">Opened file name</param>
        public void RemoveVisualFilesTextFromProjectFile(String filename)
        {
            String projectfile= ReadCurrentProjectFileName();
            if(projectfile!="")
            {
                if(File.Exists(projectfile))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(projectfile);
                    XmlNodeList nodes = doc.GetElementsByTagName("VisualFile");
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        XmlNode node = nodes[i];
                        if (node.InnerText == filename)
                        {
                            node.ParentNode.RemoveChild(node);
                        }
                    }
                    doc.Save(projectfile);
                }
            }
        }


        //*************************************************************************************
        //     SelectJDKPath_OnStartup()
        //*************************************************************************************
        /// <summary>
        /// Getting the Java Development Kit(JDK) path when application loads(Form loads)
        /// Adding that JDK path to '\files\config.slvjfile' to <JDKPath></JDKPath> tag.
        /// </summary>
        public void SelectJDKPath_OnStartup()
        {
            String jdkpath = "";

            //First check that path is already selected or not by reading <JDKPath> tag from
            // file '\files\config.slvjfile'
            using (XmlReader reader = XmlReader.Create(Application.StartupPath + "\\files\\config.slvjfile"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "JDKPath":
                                jdkpath = reader.ReadString();
                                break;
                        }
                    }
                }
            }

            //if it is null or empty or '\n' then show dialog to select JDK path
            //Here i am checking 'bin' folder in that bin folder checking whether
            // javac.exe and java.exe file exists or not
            // you can check many more files for validation of correct JDK path
            //if it does not contain these files then show error dialog and exit
            if (jdkpath == "null" || jdkpath == ""||jdkpath=="\n  ")
            {
                DialogResult dg = MessageBox.Show("Java Development Kit (JDK) Path is not specified.\n Please select JDK Path", "Select JDK Path", MessageBoxButtons.OKCancel);
                if (dg == DialogResult.OK)
                {
                    FolderBrowserDialog folderbrowserdialog = new FolderBrowserDialog();
                    folderbrowserdialog.Description = "Select your Java Development Kit (JDK) Folder\ne.g. : C:\\Java\\jdk1.8.0_25";

                    if (folderbrowserdialog.ShowDialog() == DialogResult.OK)
                    {
                        String folderpath = folderbrowserdialog.SelectedPath;
                        String configfile = Application.StartupPath + "\\files\\config.slvjfile";

                        //check bin folder available in selected path or not
                        if (folderpath.Contains("bin"))
                        {
                            if (File.Exists(folderpath + "\\javac.exe") && File.Exists(folderpath + "\\java.exe"))
                            {
                                XmlDocument doc = new XmlDocument();
                                doc.Load(configfile);
                                doc.SelectSingleNode("SilverJConfiguration/JDKPath").InnerText = folderpath;
                                doc.Save(configfile);
                            }
                            else
                            {
                                MessageBox.Show("Invalid JDK Path\nApplication will exit......");
                                Application.Exit();
                            }
                        }

                        else
                        {
                            folderpath = folderpath + "\\bin";
                            if (File.Exists(folderpath + "\\javac.exe") && File.Exists(folderpath + "\\java.exe"))
                            {
                                XmlDocument doc = new XmlDocument();
                                doc.Load(configfile);
                                doc.SelectSingleNode("SilverJConfiguration/JDKPath").InnerText = folderpath;
                                doc.Save(configfile);
                            }
                            else
                            {
                                MessageBox.Show("Invalid JDK Path\nApplication will exit.......");
                                Application.Exit();
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
                            XmlDocument doc = new XmlDocument();
                            doc.Load(configfile);
                            doc.SelectSingleNode("SilverJConfiguration/JDKPath").InnerText = "null";
                            doc.Save(configfile);
                            Application.Exit();
                        }
                        catch
                        {
                            DialogResult dg2 = MessageBox.Show("Error Occured to access the file in current path.\nTry to run program as System Administrator", "Error......",MessageBoxButtons.OK);
                            if (dg == DialogResult.OK)
                            {
                                Application.Exit();
                            }
                        }
                    }
                }
                else
                {
                    try
                    {
                        String configfile = Application.StartupPath + "\\files\\config.slvjfile";
                        XmlDocument doc = new XmlDocument();
                        doc.Load(configfile);
                        doc.SelectSingleNode("SilverJConfiguration/JDKPath").InnerText = "null";
                        doc.Save(configfile);
                        Application.Exit();
                    }
                    catch
                    {
                        DialogResult dg2= MessageBox.Show("Error Occured to access the file in current path.\nTry to run program as System Administrator","Error......",MessageBoxButtons.OK);
                        if(dg==DialogResult.OK)
                        {
                            Application.Exit();
                        }
                    }
                }
            }
        }


        //*************************************************************************************
        //  getWebBrowser()
        //*************************************************************************************
        /// <summary>
        /// Reading web browser application path from '\files\config.slvjfile'
        /// by tag name '<WebBrowser></WebBrowser>'
        /// </summary>
        /// <returns>WebBrowser application path as string</returns>
        public String getWebBrowser()
        {
            String webbrowser = "";
            using (XmlReader reader = XmlReader.Create(Application.StartupPath + "\\files\\config.slvjfile"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "WebBrowser":
                                webbrowser = reader.ReadString();
                                break;
                        }
                    }
                }
            }
            return webbrowser;
        }


        //*************************************************************************************
        //  getMainClassFileName()
        //*************************************************************************************
        /// <summary>
        /// Reading Java source file name by tag name '<MainClassFile></MainClassFile>'
        /// This file is the main file that only be used to compile
        /// because we need only one java source file for compilation
        /// when you click on compile or run,this file gets compile only
        /// see (Run->Compile) event
        /// </summary>
        /// <returns>Java source file name(.java) as string</returns>
        public String getMainClassFileName()
        {
            String mainclass = "";

            using (XmlReader reader = XmlReader.Create(ReadCurrentProjectFileName()))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "MainClassFile":
                                mainclass = reader.ReadString();
                                break;
                        }
                    }
                }
            }
            return mainclass;
        }


        //*************************************************************************************
        //  getJDKPath()
        //*************************************************************************************
        /// <summary>
        /// Reads JDK path from file \files\config.slvjfile
        /// </summary>
        /// <returns>JDK path as string</returns>
        public String getJDKPath()
        {
            String jdkpath = "";

            using (XmlReader reader = XmlReader.Create(Application.StartupPath + "\\files\\config.slvjfile"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "JDKPath":
                                jdkpath = reader.ReadString();
                                break;
                        }
                    }
                }
            }
            return jdkpath;
        }


        //*************************************************************************************
        // getAppletViewerHTMLFileName()
        //*************************************************************************************
        /// <summary>
        /// Reads main HTML file for running applet code by using appletviewer.exe fro JDK path
        /// </summary>
        /// <returns>Applet HTML file name as string</returns>
        public String getAppletViewerHTMLFileName()
        {
            String appletfile = "";

            using (XmlReader reader = XmlReader.Create(ReadCurrentProjectFileName()))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "AppletViewerHTMLFile":
                                appletfile = reader.ReadString();
                                break;
                        }
                    }
                }
            }
            return appletfile;
        }

        //*************************************************************************************
        //  Remove VisualFile names from project file
        //*************************************************************************************
        /// <summary>
        /// Checks input type and set different colors and adding renderers to controls
        /// Set the appearance to controls
        /// </summary>
        /// <param name="type">Appearance type (Default,System,Light,Dark,Night)</param>
        public void SetAppearances(String type)
        {
            if (type == "Default")
            {
                MyMenuStripZ.Renderer = new DefaultMenuRenderer();
                textEditorContextMenuStrip.Renderer = new DefaultMenuRenderer();
                ProjectExplorerTreeViewContextMenuStrip.Renderer = new DefaultMenuRenderer();
                myTabControlContextMenuStrip.Renderer = new DefaultMenuRenderer();
                ErrorsListContextMenuStrip.Renderer = new DefaultMenuRenderer();
                MyToolStripZ.Renderer = new DefaultToolStripRenderer();
                myTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;

                myTabControl.Transparent1 = 170;
                myTabControl.Transparent2 = 160;
                toolstrippanel.Transparent1 = 255;
                toolstrippanel.Transparent2 = 255;
                projectexplorerpanel.Transparent1 = 255;
                projectexplorerpanel.Transparent2 = 255;
                classespanel.Transparent1 = 255;
                classespanel.Transparent2 = 255;
                methodspanel.Transparent1 = 255;
                methodspanel.Transparent2 = 255;
                errorslistpanel.Transparent1 = 255;
                errorslistpanel.Transparent2 = 255;

                MyMenuStripZ.BackColor = Color.FromArgb(235,235,235);
                toolstrippanel.StartColor = Color.FromArgb(220, 220, 255);
                toolstrippanel.EndColor = Color.FromArgb(230, 230, 230);
                MyToolStripZ.BackColor = Color.FromArgb(232, 232, 235);

                projectexplorerpanel.StartColor = Color.FromArgb(240, 240, 240);
                projectexplorerpanel.EndColor = Color.Gainsboro;
                classespanel.StartColor = Color.FromArgb(240, 240, 240);
                classespanel.EndColor = Color.Gainsboro;
                methodspanel.StartColor = Color.FromArgb(240, 240, 240);
                methodspanel.EndColor = Color.Gainsboro;
                errorslistpanel.StartColor = Color.FromArgb(240, 240, 240);
                errorslistpanel.EndColor = Color.Gainsboro;

                projectexplorerlabel.ForeColor = Color.Black;
                classeslabel.ForeColor = Color.Black;
                methodslabel.ForeColor = Color.Black;
                errorslabel.ForeColor = Color.Black;

                myTabControl.ActiveTabStartColor = Color.FromArgb(146, 202, 230);
                myTabControl.ActiveTabEndColor = Color.FromArgb(241, 248, 251);
                myTabControl.NonActiveTabStartColor = Color.Silver;
                myTabControl.NonActiveTabEndColor = Color.WhiteSmoke;

                splitContainer1.BackColor = SystemColors.InactiveCaption;

                ProjectExplorerTreeView.BackColor = SystemColors.Window;
                ClassesTreeView.BackColor = SystemColors.Window;
                MethodsTreeView.BackColor = SystemColors.Window;
                ErrorTextBox.BackColor = SystemColors.Window;

                ProjectExplorerTreeView.ForeColor = Color.Black;
                ClassesTreeView.ForeColor = Color.Black;
                MethodsTreeView.ForeColor = Color.Black;
                ErrorTextBox.ForeColor = Color.Red;
                myTabControl.TextColor = Color.Navy;

            }


            else if (type == "System")
            {
                MyMenuStripZ.RenderMode = ToolStripRenderMode.System;
                textEditorContextMenuStrip.RenderMode = ToolStripRenderMode.System;
                ProjectExplorerTreeViewContextMenuStrip.RenderMode = ToolStripRenderMode.System;
                myTabControlContextMenuStrip.RenderMode = ToolStripRenderMode.System;
                ErrorsListContextMenuStrip.RenderMode = ToolStripRenderMode.System;
                MyToolStripZ.RenderMode = ToolStripRenderMode.System;
                myTabControl.DrawMode = TabDrawMode.Normal;


                MyMenuStripZ.BackColor = SystemColors.Control;
                toolstrippanel.StartColor = SystemColors.Control;
                toolstrippanel.EndColor = SystemColors.Control;
                MyToolStripZ.BackColor = SystemColors.Control;

                projectexplorerpanel.StartColor = SystemColors.Control;
                projectexplorerpanel.EndColor = SystemColors.Control;
                classespanel.StartColor = SystemColors.Control;
                classespanel.EndColor = SystemColors.Control;
                methodspanel.StartColor = SystemColors.Control;
                methodspanel.EndColor = SystemColors.Control;
                errorslistpanel.StartColor = SystemColors.Control;
                errorslistpanel.EndColor = SystemColors.Control;

                projectexplorerlabel.ForeColor = Color.Black;
                classeslabel.ForeColor = Color.Black;
                methodslabel.ForeColor = Color.Black;
                errorslabel.ForeColor = Color.Black;

                splitContainer1.BackColor =SystemColors.ControlDark;

                ProjectExplorerTreeView.BackColor = SystemColors.Window;
                ClassesTreeView.BackColor = SystemColors.Window;
                MethodsTreeView.BackColor = SystemColors.Window;
                ErrorTextBox.BackColor = SystemColors.Window;

                ProjectExplorerTreeView.ForeColor = Color.Black;
                ClassesTreeView.ForeColor = Color.Black;
                MethodsTreeView.ForeColor = Color.Black;
                ErrorTextBox.ForeColor = Color.Red;
            }


            else if (type == "Light")
            {
                MyMenuStripZ.Renderer = new LightMenuRenderer();
                textEditorContextMenuStrip.Renderer = new LightMenuRenderer();
                ProjectExplorerTreeViewContextMenuStrip.Renderer = new LightMenuRenderer();
                myTabControlContextMenuStrip.Renderer = new LightMenuRenderer();
                ErrorsListContextMenuStrip.Renderer = new LightMenuRenderer();
                MyToolStripZ.Renderer = new LightToolStripRenderer();
                myTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;

                myTabControl.Transparent1 = 255;
                myTabControl.Transparent2 = 255;
                toolstrippanel.Transparent1 = 255;
                toolstrippanel.Transparent2 = 255;
                projectexplorerpanel.Transparent1 = 255;
                projectexplorerpanel.Transparent2 = 255;
                classespanel.Transparent1 = 255;
                classespanel.Transparent2 = 255;
                methodspanel.Transparent1 = 255;
                methodspanel.Transparent2 = 255;
                errorslistpanel.Transparent1 = 255;
                errorslistpanel.Transparent2 = 255;

                MyMenuStripZ.BackColor = Color.FromArgb(255, 255, 255);
                toolstrippanel.StartColor = Color.FromArgb(255, 255, 255);
                toolstrippanel.EndColor = Color.FromArgb(255, 255, 255);
                MyToolStripZ.BackColor = Color.FromArgb(255, 255, 255);

                projectexplorerpanel.StartColor = Color.FromArgb(255, 255, 255, 255);
                projectexplorerpanel.EndColor = Color.FromArgb(255, 172, 236, 255);
                classespanel.StartColor = Color.FromArgb(255, 255, 255, 255);
                classespanel.EndColor = Color.FromArgb(255, 172, 236, 255);
                methodspanel.StartColor = Color.FromArgb(255, 255, 255, 255);
                methodspanel.EndColor = Color.FromArgb(255, 172, 236, 255);
                errorslistpanel.StartColor = Color.FromArgb(255, 255, 255, 255);
                errorslistpanel.EndColor = Color.FromArgb(255, 172, 236, 255);

                projectexplorerlabel.ForeColor = Color.Black;
                classeslabel.ForeColor = Color.Black;
                methodslabel.ForeColor = Color.Black;
                errorslabel.ForeColor = Color.Black;

                myTabControl.ActiveTabStartColor = Color.FromArgb(255, 255, 255, 255);
                myTabControl.ActiveTabEndColor = Color.FromArgb(255, 172, 236, 255);
                myTabControl.NonActiveTabStartColor = Color.FromArgb(250, 200, 200, 200);
                myTabControl.NonActiveTabEndColor = Color.FromArgb(250, 130, 200, 210);

                splitContainer1.BackColor = Color.FromArgb(255, 180, 220, 250);

                ProjectExplorerTreeView.ForeColor = Color.Black;
                ClassesTreeView.ForeColor = Color.Black;
                MethodsTreeView.ForeColor = Color.Black;
                ErrorTextBox.ForeColor = Color.Red;
                myTabControl.TextColor = Color.Navy;

                ProjectExplorerTreeView.BackColor = Color.White;
                ClassesTreeView.BackColor = Color.White;
                MethodsTreeView.BackColor = Color.White;
                ErrorTextBox.BackColor = Color.White;
            }


            else if(type=="Dark")
            {
                MyMenuStripZ.Renderer = new DarkMenuRenderer();
                textEditorContextMenuStrip.Renderer = new DarkMenuRenderer();
                ProjectExplorerTreeViewContextMenuStrip.Renderer = new DarkMenuRenderer();
                myTabControlContextMenuStrip.Renderer = new DarkMenuRenderer();
                ErrorsListContextMenuStrip.Renderer = new DarkMenuRenderer();
                MyToolStripZ.Renderer = new DarkToolStripRenderer();
                myTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;

                myTabControl.Transparent1 = 255;
                myTabControl.Transparent2 = 255;
                toolstrippanel.Transparent1 = 255;
                toolstrippanel.Transparent2 = 255;
                projectexplorerpanel.Transparent1 = 255;
                projectexplorerpanel.Transparent2 = 255;
                classespanel.Transparent1 = 255;
                classespanel.Transparent2 = 255;
                methodspanel.Transparent1 = 255;
                methodspanel.Transparent2 = 255;
                errorslistpanel.Transparent1 = 255;
                errorslistpanel.Transparent2 = 255;

                MyMenuStripZ.BackColor = Color.FromArgb(250, 160, 180, 210);
                toolstrippanel.StartColor = Color.FromArgb(250, 160, 180, 210);
                toolstrippanel.EndColor = Color.FromArgb(250, 160, 180, 210);
                MyToolStripZ.BackColor = Color.FromArgb(250, 160, 180, 210);

                projectexplorerpanel.StartColor = Color.FromArgb(255, 20, 20, 120);
                projectexplorerpanel.EndColor = Color.FromArgb(255, 20, 20, 110);
                classespanel.StartColor = Color.FromArgb(255, 20, 20, 120);
                classespanel.EndColor = Color.FromArgb(255, 20, 20, 110);
                methodspanel.StartColor = Color.FromArgb(255, 20, 20, 120);
                methodspanel.EndColor = Color.FromArgb(255, 20, 20, 110);
                errorslistpanel.StartColor = Color.FromArgb(255, 20, 20, 100);
                errorslistpanel.EndColor = Color.FromArgb(255, 20, 20, 110);

                myTabControl.ActiveTabStartColor = Color.FromArgb(255, 255, 255, 180);
                myTabControl.ActiveTabEndColor = Color.FromArgb(255, 255, 234, 130);
                myTabControl.NonActiveTabStartColor = Color.FromArgb(250, 130, 150, 200);
                myTabControl.NonActiveTabEndColor = Color.FromArgb(250, 130, 150, 200);

                splitContainer1.BackColor = Color.FromArgb(255, 20, 20, 80);

                projectexplorerlabel.ForeColor = Color.White;
                classeslabel.ForeColor = Color.White;
                methodslabel.ForeColor = Color.White;
                errorslabel.ForeColor = Color.White;

                ProjectExplorerTreeView.ForeColor = Color.Black;
                ClassesTreeView.ForeColor = Color.Black;
                MethodsTreeView.ForeColor = Color.Black;
                ErrorTextBox.ForeColor = Color.Red;
                myTabControl.TextColor = Color.Black;

                ProjectExplorerTreeView.BackColor = Color.FromArgb(255, 240, 240, 255);
                ClassesTreeView.BackColor = Color.FromArgb(255, 240, 240, 255);
                MethodsTreeView.BackColor = Color.FromArgb(255, 240, 240, 255);
                ErrorTextBox.BackColor = Color.FromArgb(255, 240, 240, 255);
                ErrorTextBox.ForeColor = Color.FromArgb(255, 235, 80, 80);
            }


            else if(type=="Night")
            {
                MyMenuStripZ.Renderer = new NightMenuRenderer();
                textEditorContextMenuStrip.Renderer = new NightMenuRenderer();
                ProjectExplorerTreeViewContextMenuStrip.Renderer = new NightMenuRenderer();
                myTabControlContextMenuStrip.Renderer = new NightMenuRenderer();
                ErrorsListContextMenuStrip.Renderer = new NightMenuRenderer();
                MyToolStripZ.Renderer = new NightToolStripRenderer();
                myTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;

                myTabControl.Transparent1 = 255;
                myTabControl.Transparent2 = 255;
                toolstrippanel.Transparent1 = 255;
                toolstrippanel.Transparent2 = 255;
                projectexplorerpanel.Transparent1 = 255;
                projectexplorerpanel.Transparent2 = 255;
                classespanel.Transparent1 = 255;
                classespanel.Transparent2 = 255;
                methodspanel.Transparent1 = 255;
                methodspanel.Transparent2 = 255;
                errorslistpanel.Transparent1 = 255;
                errorslistpanel.Transparent2 = 255;

                MyMenuStripZ.BackColor = Color.FromArgb(255, 30, 30, 30);
                toolstrippanel.StartColor = Color.FromArgb(30, 30, 30);
                toolstrippanel.EndColor = Color.FromArgb(30, 30, 30);
                MyToolStripZ.BackColor = Color.FromArgb(250, 30, 30, 30);

                projectexplorerpanel.StartColor = Color.FromArgb(15, 15, 15);
                projectexplorerpanel.EndColor = Color.FromArgb(15, 15, 15);
                classespanel.StartColor = Color.FromArgb(15, 15, 15);
                classespanel.EndColor = Color.FromArgb(15, 15, 15);
                methodspanel.StartColor = Color.FromArgb(15, 15, 15);
                methodspanel.EndColor = Color.FromArgb(15, 15, 15);
                errorslistpanel.StartColor = Color.FromArgb(15, 15, 15);
                errorslistpanel.EndColor = Color.FromArgb(15, 15, 15);

                projectexplorerlabel.ForeColor = Color.White;
                classeslabel.ForeColor = Color.White;
                methodslabel.ForeColor = Color.White;
                errorslabel.ForeColor = Color.White;

                myTabControl.ActiveTabStartColor = Color.FromArgb(10, 10, 30);
                myTabControl.ActiveTabEndColor = Color.FromArgb(10, 10, 30);
                myTabControl.NonActiveTabStartColor = Color.FromArgb(60, 60, 60);
                myTabControl.NonActiveTabEndColor = Color.FromArgb(60, 60, 60);

                myTabControl.TextColor = Color.White;

                splitContainer1.BackColor = Color.FromArgb(255,10, 10, 10);

                ProjectExplorerTreeView.BackColor = Color.FromArgb(255, 25, 25, 25);
                ClassesTreeView.BackColor = Color.FromArgb(255, 25, 25, 25);
                MethodsTreeView.BackColor = Color.FromArgb(255, 25, 25, 25);
                ErrorTextBox.BackColor = Color.FromArgb(255, 25, 25, 25);

                ProjectExplorerTreeView.ForeColor = Color.White;
                ClassesTreeView.ForeColor = Color.White;
                MethodsTreeView.ForeColor = Color.White;
                ErrorTextBox.ForeColor = Color.FromArgb(255, 255, 220, 220);
            }
        }


        //**************************************************************************************************************
        //      SetVisibilityOfToolStripButtons()
        //**************************************************************************************************************
        /// <summary>
        /// Checks counts of tabs if, count > 0 then set visibility of toolstrip buttons to true
        /// also checks current opened project,if it is application type then set Run Applet tool
        /// strip button to false and same as when applet type
        /// also checks current tab is Start Page tab is or not
        /// </summary>
        public void SetVisibilityOfToolStripButtons()
        {
            if(this.Text!="Silver-J")
            {
                JavaApplicationProject_ToolStripButton.Enabled = true;
                JavaAppletProject_ToolStripButton.Enabled = true;
                Class_ToolStripButton.Enabled = true;
                if(myTabControl.TabCount>0)
                {
                    Save_ToolStripButton.Enabled = true;
                    SaveAs_ToolStripButton.Enabled = true;
                    SaveAll_ToolStripButton.Enabled = true;
                    Cut_ToolStripButton.Enabled = true;
                    Copy_ToolStripButton.Enabled = true;
                    Paste_ToolStripButton.Enabled = true;
                    Undo_ToolStripButton.Enabled = true;
                    Redo_ToolStripButton.Enabled = true;
                    ViewinWebBrowser_ToolStripButton.Enabled = true;
                }
                if(getCurrentProjectType()=="ApplicationType")
                {
                    Compile_ToolStripButton.Enabled = true;
                    Run_ToolStripButton.Enabled = true;
                    RunApplet_ToolStripButton.Enabled = false;
                    RunWithParameters_ToolStripButton.Enabled = true;
                    Build_ToolStripButton.Enabled = true;
                }
                else
                {
                    Compile_ToolStripButton.Enabled = true;
                    Run_ToolStripButton.Enabled = false;
                    RunApplet_ToolStripButton.Enabled = true;
                    RunWithParameters_ToolStripButton.Enabled = false;
                    Build_ToolStripButton.Enabled = true;
                }
            }
            else
            {
                if(myTabControl.TabCount>0)
                {
                    if(myTabControl.SelectedTab.Text!="Start Page")
                    {
                        JavaApplicationProject_ToolStripButton.Enabled = true;
                        JavaAppletProject_ToolStripButton.Enabled = true;
                        Class_ToolStripButton.Enabled = false;
                        Save_ToolStripButton.Enabled = true;
                        SaveAs_ToolStripButton.Enabled = true;
                        SaveAll_ToolStripButton.Enabled = true;
                        Cut_ToolStripButton.Enabled = true;
                        Copy_ToolStripButton.Enabled = true;
                        Paste_ToolStripButton.Enabled = true;
                        Undo_ToolStripButton.Enabled = true;
                        Redo_ToolStripButton.Enabled = true;
                        ViewinWebBrowser_ToolStripButton.Enabled = true;
                        Compile_ToolStripButton.Enabled = false;
                        Run_ToolStripButton.Enabled = false;
                        RunApplet_ToolStripButton.Enabled = false;
                        RunWithParameters_ToolStripButton.Enabled = false;
                        Build_ToolStripButton.Enabled = false;
                    }
                    else
                    {
                        JavaApplicationProject_ToolStripButton.Enabled = true;
                        JavaAppletProject_ToolStripButton.Enabled = true;
                        Class_ToolStripButton.Enabled = false;
                        Save_ToolStripButton.Enabled = false;
                        SaveAs_ToolStripButton.Enabled = false;
                        SaveAll_ToolStripButton.Enabled = false;
                        Cut_ToolStripButton.Enabled = false;
                        Copy_ToolStripButton.Enabled = false;
                        Paste_ToolStripButton.Enabled = false;
                        Undo_ToolStripButton.Enabled = false;
                        Redo_ToolStripButton.Enabled = false;
                        ViewinWebBrowser_ToolStripButton.Enabled = false;
                        Compile_ToolStripButton.Enabled = false;
                        Run_ToolStripButton.Enabled = false;
                        RunApplet_ToolStripButton.Enabled = false;
                        RunWithParameters_ToolStripButton.Enabled = false;
                        Build_ToolStripButton.Enabled = false;
                    }
                }
                else
                {
                    JavaApplicationProject_ToolStripButton.Enabled = true;
                    JavaAppletProject_ToolStripButton.Enabled = true;
                    Class_ToolStripButton.Enabled = false;
                    Save_ToolStripButton.Enabled = false;
                    SaveAs_ToolStripButton.Enabled = false;
                    SaveAll_ToolStripButton.Enabled = false;
                    Cut_ToolStripButton.Enabled = false;
                    Copy_ToolStripButton.Enabled = false;
                    Paste_ToolStripButton.Enabled = false;
                    Undo_ToolStripButton.Enabled = false;
                    Redo_ToolStripButton.Enabled = false;
                    ViewinWebBrowser_ToolStripButton.Enabled = false;
                    Compile_ToolStripButton.Enabled = false;
                    Run_ToolStripButton.Enabled = false;
                    RunApplet_ToolStripButton.Enabled = false;
                    RunWithParameters_ToolStripButton.Enabled = false;
                    Build_ToolStripButton.Enabled = false;
                }
            }
        }





        //***********************************************************************************
        //  ChangeTextOfReadyLabel() function to change text of ReadyLabel
        //***********************************************************************************
        /// <summary>
        /// Changing the text of Ready label when mouse enters on the menu items for show information
        /// First getting each menu item and adding MouseEnter & MouseLeave events to it.
        /// </summary>
        /// <param name="menuitem"></param>
        public void ChangeTextOfReadyLabel(ToolStripMenuItem menuitem)
        {
            menuitem.MouseEnter += new EventHandler(this.menuitem_MouseEnter);
            menuitem.MouseLeave += new EventHandler(this.menuitem_MouseLeave);
        }
        private void menuitem_MouseEnter(object sender, EventArgs e)
        {
            Object b = (ToolStripMenuItem)sender;
            String s = b.ToString().Trim();
            switch (s)
            {
                case "File":
                    ShowAboutToolStripLabel.Text = "Create New Java Application/Applet Project,Open Project/Files,Save or Close Files";
                    break;
                case "New":
                    ShowAboutToolStripLabel.Text = "Create New Java Application/Applet Project,Adding Classes or Other files to project";
                    break;
                case "Java Application Project":
                    ShowAboutToolStripLabel.Text = "Create New Java Application Project";
                    break;
                case "Java Applet Project":
                    ShowAboutToolStripLabel.Text = "Create New Java Applet Project";
                    break;
                case "Class":
                    ShowAboutToolStripLabel.Text = "Add New Java Class to current project";
                    break;
                case "Package":
                    ShowAboutToolStripLabel.Text = "Add/Create New Package to current project";
                    break;
                case "Interface":
                    ShowAboutToolStripLabel.Text = "Add New Interface to current project";
                    break;
                case "Enums":
                    ShowAboutToolStripLabel.Text = "Add New Enums to current project";
                    break;
                case "HTML File":
                    ShowAboutToolStripLabel.Text = "Add New HTML file to current project";
                    break;
                case "CSS File":
                    ShowAboutToolStripLabel.Text = "Add New CSS file to current project";
                    break;
                case "Text File":
                    ShowAboutToolStripLabel.Text = "Add New Text file to current project";
                    break;
                case "JavaScript File":
                    ShowAboutToolStripLabel.Text = "Add New JavaScript file to current project";
                    break;
                case "SQL File":
                    ShowAboutToolStripLabel.Text = "Add New SQL file to current project";
                    break;
                case "XML File":
                    ShowAboutToolStripLabel.Text = "Add New XML file to current project";
                    break;
                case "New File":
                    ShowAboutToolStripLabel.Text = "Add New file with your extension to current project";
                    break;
                case "Open Project":
                    ShowAboutToolStripLabel.Text = "Open another project in Silver-J";
                    break;
                case "Open Files":
                    ShowAboutToolStripLabel.Text = "Open multiple files in window";
                    break;
                case "Load Sample Project":
                    ShowAboutToolStripLabel.Text = "Load sample project in window";
                    break;
                case "Save":
                    ShowAboutToolStripLabel.Text = "Save current opened document";
                    break;
                case "Save As":
                    ShowAboutToolStripLabel.Text = "Save As current opened document";
                    break;
                case "Save All":
                    ShowAboutToolStripLabel.Text = "Save All opened documents one by one";
                    break;
                case "Close":
                    ShowAboutToolStripLabel.Text = "Close current opened document";
                    break;
                case "Close All":
                    ShowAboutToolStripLabel.Text = "Close All opened documents one by one";
                    break;
                case "Close Project":
                    ShowAboutToolStripLabel.Text = "Close current opened project";
                    break;
                case "Delete Project":
                    ShowAboutToolStripLabel.Text = "Close current opened project and delete it";
                    break;
                case "Print":
                    ShowAboutToolStripLabel.Text = "Print contents of current opened document";
                    break;
                case "Print Preview":
                    ShowAboutToolStripLabel.Text = "View the print preview of current opened document";
                    break;
                case "Exit":
                    ShowAboutToolStripLabel.Text = "Exit from Silver-J..............!";
                    break;

                case"Edit":
                    ShowAboutToolStripLabel.Text = "Cut,Copy,Paste,Undo,Redo,GoTo etc operations";
                    break;
                case "Change Case":
                    ShowAboutToolStripLabel.Text = "Change cases to Upper,Lower or Sentence";
                    break;
                case "Upper Case":
                    ShowAboutToolStripLabel.Text = "Convert selected to Upper Case";
                    break;
                case "Lower Case":
                    ShowAboutToolStripLabel.Text = "Convert selected case to Lower Case";
                    break;
                case "Sentence Case":
                    ShowAboutToolStripLabel.Text = "Conver selected case to Sentence Case";
                    break;
                case "Comment Line":
                    ShowAboutToolStripLabel.Text = "Insert/Add comments to current document";
                    break;
                case "Single Line Comment":
                    ShowAboutToolStripLabel.Text = "Insert Single Line Comment in current document";
                    break;
                case "Multi Line Comment":
                    ShowAboutToolStripLabel.Text = "Insert Multi Line Comment in current document";
                    break;
                case "Selection Comment":
                    ShowAboutToolStripLabel.Text = "Place selected contents in current document in comment";
                    break;
                case "Insert":
                    ShowAboutToolStripLabel.Text = "Insert main function,class,events to current document";
                    break;
                case "Next Document":
                    ShowAboutToolStripLabel.Text = "Go to the next document";
                    break;
                case "Previous Document":
                    ShowAboutToolStripLabel.Text = "Go to the previous document";
                    break;


                case"View":
                    ShowAboutToolStripLabel.Text = "Change the visibility & appearances of application";
                    break;
                case "Tabs Alignment":
                    ShowAboutToolStripLabel.Text = "Set Tabs alignment to Top or Bottom";
                    break;
                case "Status Strip":
                    ShowAboutToolStripLabel.Text = "Show or Hide Status Strip";
                    break;
                case "Tool Strip":
                    ShowAboutToolStripLabel.Text = "Show or Hide Tool Strip";
                    break;
                case "Full Screen":
                    ShowAboutToolStripLabel.Text = "Go to or Exit from Full Screen mode";
                    break;
                case "Appearance":
                    ShowAboutToolStripLabel.Text = "Set different appearances to window";
                    break;
                case "Project Explorer":
                    ShowAboutToolStripLabel.Text = "Show or Hide Project Explorer View";
                    break;
                case "Classes View":
                    ShowAboutToolStripLabel.Text = "Show or Hide Classes View";
                    break;
                case "Methods View":
                    ShowAboutToolStripLabel.Text = "Show or Hide Methods View View";
                    break;
                case "Errors List":
                    ShowAboutToolStripLabel.Text = "Show or Hide Errors List View";
                    break;
                case "Show Error Dialog":
                    ShowAboutToolStripLabel.Text = "Show or Hide Error Dialog";
                    break;
                case "View in Web Browser":
                    ShowAboutToolStripLabel.Text = "View current HTML document in web browser";
                    break;

                case "Run":
                    ShowAboutToolStripLabel.Text = "Compile,Run Applications or Applets";
                    break;
                case "Auto Compile Program":
                    ShowAboutToolStripLabel.Text = "Set Auto Compile Program function to true";
                    break;
                case "Main Class":
                    ShowAboutToolStripLabel.Text = "Change the main class of the project";
                    break;
                case "Add Files to Project":
                    ShowAboutToolStripLabel.Text = "Add external files to project";
                    break;
                case "Build":
                    ShowAboutToolStripLabel.Text = "Build Executable Jar Application or current project";
                    break;
                case "Preview HTML Page":
                    ShowAboutToolStripLabel.Text = "View current HTML document contents in internal browser of Silver-J";
                    break;
                case "Options":
                    ShowAboutToolStripLabel.Text = "Change the options of Silver-J editor or appearance";
                    break;

            }
        }
        private void menuitem_MouseLeave(object sender, EventArgs e)
        {
            ShowAboutToolStripLabel.Text = "Ready";
        }

        //Passing each menu items to ChangeTextOfReadyLabel
        public void UpdateReadyLabel()
        {
            ChangeTextOfReadyLabel(FileMenuItem);
            ChangeTextOfReadyLabel(File_NewMenuItem);
            ChangeTextOfReadyLabel(File_New_JavaApplicationProjectMenuItem);
            ChangeTextOfReadyLabel(File_New_JavaAppletMenuItem);
            ChangeTextOfReadyLabel(File_New_ClassMenuItem);
            ChangeTextOfReadyLabel(File_New_PackageMenuItem);
            ChangeTextOfReadyLabel(File_New_InterfaceMenuItem);
            ChangeTextOfReadyLabel(File_New_EnumsMenuItem);
            ChangeTextOfReadyLabel(File_New_HTMLFileMenuItem);
            ChangeTextOfReadyLabel(File_New_CSSFileMenuItem);
            ChangeTextOfReadyLabel(File_New_TextFileMenuItem);
            ChangeTextOfReadyLabel(File_New_JavaScriptFileMenuItem);
            ChangeTextOfReadyLabel(File_New_SQLFileMenuItem);
            ChangeTextOfReadyLabel(File_New_XMLFileMenuItem);
            ChangeTextOfReadyLabel(File_New_NewFileMenuItem);
            ChangeTextOfReadyLabel(File_OpenProjectMenuItem);
            ChangeTextOfReadyLabel(File_OpenFilesMenuItem);
            ChangeTextOfReadyLabel(File_LoadSampleProjectMenuItem);
            ChangeTextOfReadyLabel(File_SaveMenuItem);
            ChangeTextOfReadyLabel(File_SaveAsMenuItem);
            ChangeTextOfReadyLabel(File_SaveAllMenuItem);
            ChangeTextOfReadyLabel(File_CloseMenuItem);
            ChangeTextOfReadyLabel(File_CloseAllMenuItem);
            ChangeTextOfReadyLabel(File_CloseProjectMenuItem);
            ChangeTextOfReadyLabel(File_DeleteProjectMenuItem);
            ChangeTextOfReadyLabel(File_PrintMenuItem);
            ChangeTextOfReadyLabel(File_PrintPreviewMenuItem);
            ChangeTextOfReadyLabel(File_ExitMenuItem);

            ChangeTextOfReadyLabel(EditMenuItem);
            ChangeTextOfReadyLabel(Edit_ChangeCaseMenuItem);
            ChangeTextOfReadyLabel(Edit_ChangeCase_UpperCaseMenuItem);
            ChangeTextOfReadyLabel(Edit_ChangeCase_LowerCaseMenuItem);
            ChangeTextOfReadyLabel(Edit_ChangeCase_SentenceCaseMenuItem);
            ChangeTextOfReadyLabel(Edit_CommentLineMenuItem);
            ChangeTextOfReadyLabel(Edit_CommentLine_SingleLineMenuItem);
            ChangeTextOfReadyLabel(Edit_CommentLine_MultiLineMenuItem);
            ChangeTextOfReadyLabel(Edit_CommentLine_SelectionCommentMenuItem);
            ChangeTextOfReadyLabel(Edit_InsertMenuItem);
            ChangeTextOfReadyLabel(Edit_NextDocumentMenuItem);
            ChangeTextOfReadyLabel(Edit_PreviousDocumentMenuItem);

            ChangeTextOfReadyLabel(ViewMenuItem);
            ChangeTextOfReadyLabel(View_TabsAlignmentMenuItem);
            ChangeTextOfReadyLabel(View_StatusStripMenuItem);
            ChangeTextOfReadyLabel(View_ToolStripMenuItem);
            ChangeTextOfReadyLabel(View_FullScreenMenuItem);
            ChangeTextOfReadyLabel(View_AppearanceMenuItem);
            ChangeTextOfReadyLabel(View_ProjectExplorerMenuItem);
            ChangeTextOfReadyLabel(View_ClassesViewMenuItem);
            ChangeTextOfReadyLabel(View_MethodsViewMenuItem);
            ChangeTextOfReadyLabel(View_ErrorsListMenuItem);
            ChangeTextOfReadyLabel(View_ShowErrorDialogMenuItem);
            ChangeTextOfReadyLabel(View_ViewinWebBrowserMenuItem);

            ChangeTextOfReadyLabel(RunMenuItem);
            ChangeTextOfReadyLabel(Run_AutoCompileProgramMenuItem);
            ChangeTextOfReadyLabel(Run_MainClassMenuItem);
            ChangeTextOfReadyLabel(Run_AddFilesToProjectMenuItem);
            ChangeTextOfReadyLabel(Run_BuildMenuItem);
            ChangeTextOfReadyLabel(Run_PreviewHTMLPageMenuItem);
            ChangeTextOfReadyLabel(Run_OptionsMenuItem);
        }


        //**************************************************************************************************************
        //      MainForm Load
        //**************************************************************************************************************
        /// <summary>
        /// When form loads apply appearances,select JDK path etc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            SelectJDKPath_OnStartup();
            UpdateWindowsList_WindowMenu();
            AddRecentProjects();
            SetVisibilityOfToolStripButtons();
            UpdateReadyLabel();


            //reading file \files\config.slvjfile for setting appearance
            // split container sizes,show or not project explorer,classes/methods view
            //show or not status or toolstrip
            //checking views from Views menu
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            String appearance = "";
            String splitcontain_1 = "";
            String splitcontain_2 = "";
            String splitcontain_3 = "";
            String splitcontain_4 = "";
            String tabsalign = "";
            String showstatusstrip = "";
            String showtoolstrip = "";
            String showprojectexplorerview = "";
            String showclassesview = "";
            String showmethodsview = "";
            String showerrorlist = "";
            String showerrordialog = "";
            String showstartpageonstartup = "";
            String showlinenumbers = "";
            String showlinehighlighter = "";
            String showinvalidlines = "";
            String showendoflinemarkers = "";
            String showvisiblespaces = "";
            String autocompilejava = "";
            //reading each value from file
            using (XmlReader reader = XmlReader.Create(configfile))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "Appearance":
                                appearance = reader.ReadString();
                                break;

                            case "SplitContainer1":
                                splitcontain_1 = reader.ReadString();
                                break;

                            case "SplitContainer2":
                                splitcontain_2 = reader.ReadString();
                                break;

                            case "SplitContainer3":
                                splitcontain_3 = reader.ReadString();
                                break;

                            case "SplitContainer4":
                                splitcontain_4 = reader.ReadString();
                                break;

                            case "TabsAlignment":
                                tabsalign = reader.ReadString();
                                break;

                            case "ShowStatusStrip":
                                showstatusstrip = reader.ReadString();
                                break;

                            case "ShowToolStrip":
                                showtoolstrip = reader.ReadString();
                                break;

                            case "ShowProjectExplorer":
                                showprojectexplorerview = reader.ReadString();
                                break;

                            case "ShowClassesView":
                                showclassesview = reader.ReadString();
                                break;

                            case "ShowMethodsView":
                                showmethodsview = reader.ReadString();
                                break;

                            case "ShowErrorList":
                                showerrorlist = reader.ReadString();
                                break;

                            case "ShowErrorDialog":
                                showerrordialog = reader.ReadString();
                                break;

                            case "ShowStartPageOnStartUp":
                                showstartpageonstartup = reader.ReadString();
                                break;

                            case "ShowLineNumbers":
                                showlinenumbers = reader.ReadString();
                                break;

                            case "ShowLineHighlighter":
                                showlinehighlighter = reader.ReadString();
                                break;

                            case "ShowInvalidLines":
                                showinvalidlines = reader.ReadString();
                                break;

                            case "ShowEndOfLineMarker":
                                showendoflinemarkers = reader.ReadString();
                                break;

                            case "ShowVisibleSpaces":
                                showvisiblespaces = reader.ReadString();
                                break;

                            case "AutoCompileJava":
                                autocompilejava = reader.ReadString();
                                break;
                        }
                    }
                }
            }

            //set the appearance
            //**************************************************************
            //  Appearance
            if(appearance=="Default")
            {
                SetAppearances("Default");
                View_Appearance_DefaultMenuItem.Checked = true;
                View_Appearance_SystemMenuItem.Checked = false;
                View_Appearance_LightMenuItem.Checked = false;
                View_Appearance_DarkMenuItem.Checked = false;
                View_Appearance_NightMenuItem.Checked = false;

                startpagepanel.StartColor = Color.FromArgb(250, 250, 250);
                startpagepanel.EndColor = Color.Gainsboro;

                /*startpagepanel_silverjlabel.StartColor = Color.Silver;
                startpagepanel_silverjlabel.EndColor = Color.Black;*/

                startpagepanel_newjavaappproject.ForeColor = Color.Gray;
                startpagepanel_newjavaappproject.HoverColor = Color.Black;
                startpagepanel_newjavaappletproject.ForeColor = Color.Gray;
                startpagepanel_newjavaappletproject.HoverColor = Color.Black;
                startpagepanel_loadsampleproject.ForeColor = Color.Gray;
                startpagepanel_loadsampleproject.HoverColor = Color.Black;
                startpagepanel_openproject.ForeColor = Color.Gray;
                startpagepanel_openproject.HoverColor = Color.Black;
                startpagepanel_openfiles.ForeColor = Color.Gray;
                startpagepanel_openfiles.HoverColor = Color.Black;

            }

            if(appearance=="System")
            {
                SetAppearances("System");
                View_Appearance_SystemMenuItem.Checked = true;
                View_Appearance_DefaultMenuItem.Checked = false;
                View_Appearance_LightMenuItem.Checked = false;
                View_Appearance_DarkMenuItem.Checked = false;
                View_Appearance_NightMenuItem.Checked = false;

                startpagepanel.StartColor = Color.FromArgb(240, 240, 240);
                startpagepanel.EndColor = Color.FromArgb(240, 240, 240);


                /*startpagepanel_silverjlabel.StartColor = Color.Silver;
                startpagepanel_silverjlabel.EndColor = Color.Black;*/

                startpagepanel_newjavaappproject.ForeColor = Color.Gray;
                startpagepanel_newjavaappproject.HoverColor = Color.Black;
                startpagepanel_newjavaappletproject.ForeColor = Color.Gray;
                startpagepanel_newjavaappletproject.HoverColor = Color.Black;
                startpagepanel_loadsampleproject.ForeColor = Color.Gray;
                startpagepanel_loadsampleproject.HoverColor = Color.Black;
                startpagepanel_openproject.ForeColor = Color.Gray;
                startpagepanel_openproject.HoverColor = Color.Black;
                startpagepanel_openfiles.ForeColor = Color.Gray;
                startpagepanel_openfiles.HoverColor = Color.Black;
            }

           if(appearance=="Light")
            {
                SetAppearances("Light");
                View_Appearance_LightMenuItem.Checked = true;
                View_Appearance_DefaultMenuItem.Checked = false;
                View_Appearance_SystemMenuItem.Checked = false;
                View_Appearance_DarkMenuItem.Checked = false;
                View_Appearance_NightMenuItem.Checked = false;

                startpagepanel.StartColor = Color.FromArgb(250, 250, 250, 250);
                startpagepanel.EndColor = Color.FromArgb(255, 172, 236, 255);


                /*startpagepanel_silverjlabel.StartColor = Color.LightSeaGreen;
                startpagepanel_silverjlabel.EndColor = Color.MidnightBlue;*/

                startpagepanel_newjavaappproject.ForeColor = Color.MidnightBlue;
                startpagepanel_newjavaappproject.HoverColor = Color.Black;
                startpagepanel_newjavaappletproject.ForeColor = Color.MidnightBlue;
                startpagepanel_newjavaappletproject.HoverColor = Color.Black;
                startpagepanel_loadsampleproject.ForeColor = Color.MidnightBlue;
                startpagepanel_loadsampleproject.HoverColor = Color.Black;
                startpagepanel_openproject.ForeColor = Color.MidnightBlue;
                startpagepanel_openproject.HoverColor = Color.Black;
                startpagepanel_openfiles.ForeColor = Color.MidnightBlue;
                startpagepanel_openfiles.HoverColor = Color.Black;
            }

            if(appearance=="Dark")
            {
                SetAppearances("Dark");
                View_Appearance_DarkMenuItem.Checked = true;
                View_Appearance_DefaultMenuItem.Checked = false;
                View_Appearance_SystemMenuItem.Checked = false;
                View_Appearance_LightMenuItem.Checked = false;
                View_Appearance_NightMenuItem.Checked = false;

                startpagepanel.StartColor = Color.FromArgb(255, 20, 20, 80);
                startpagepanel.EndColor = Color.FromArgb(255, 20, 20, 70);

                /*startpagepanel_silverjlabel.StartColor = Color.Gold;
                startpagepanel_silverjlabel.EndColor = Color.OrangeRed;*/

                startpagepanel_newjavaappproject.ForeColor = Color.Orange;
                startpagepanel_newjavaappproject.HoverColor = Color.White;
                startpagepanel_newjavaappletproject.ForeColor = Color.Orange;
                startpagepanel_newjavaappletproject.HoverColor = Color.White;
                startpagepanel_loadsampleproject.ForeColor = Color.Orange;
                startpagepanel_loadsampleproject.HoverColor = Color.White;
                startpagepanel_openproject.ForeColor = Color.Orange;
                startpagepanel_openproject.HoverColor = Color.White;
                startpagepanel_openfiles.ForeColor = Color.Orange;
                startpagepanel_openfiles.HoverColor = Color.White;
            }

            if (appearance == "Night")
            {
                SetAppearances("Night");
                View_Appearance_NightMenuItem.Checked = true;
                View_Appearance_DefaultMenuItem.Checked = false;
                View_Appearance_SystemMenuItem.Checked = false;
                View_Appearance_LightMenuItem.Checked = false;
                View_Appearance_DarkMenuItem.Checked = false;

                startpagepanel.StartColor = Color.FromArgb(20, 20, 20);
                startpagepanel.EndColor = Color.FromArgb(20, 20, 20);

                /*startpagepanel_silverjlabel.StartColor = Color.White;
                startpagepanel_silverjlabel.EndColor = Color.Silver;*/

                startpagepanel_newjavaappproject.ForeColor = Color.Silver;
                startpagepanel_newjavaappproject.HoverColor = Color.White;
                startpagepanel_newjavaappletproject.ForeColor = Color.Silver;
                startpagepanel_newjavaappletproject.HoverColor = Color.White;
                startpagepanel_loadsampleproject.ForeColor = Color.Silver;
                startpagepanel_loadsampleproject.HoverColor = Color.White;
                startpagepanel_openproject.ForeColor = Color.Silver;
                startpagepanel_openproject.HoverColor = Color.White;
                startpagepanel_openfiles.ForeColor = Color.Silver;
                startpagepanel_openfiles.HoverColor = Color.White;
            }

            //set split container sizes
            //**************************************************************
            // Split Container 1
            if (splitcontain_1 == "")
            {
                splitContainer1.SplitterDistance = 864;
            }
            else
            {
                int sd = Convert.ToInt32(splitcontain_1);
                if (sd > 5)
                {
                    splitContainer1.SplitterDistance = sd;
                }
            }

            //**************************************************************
            // Split Container 2
            if (splitcontain_2 == "")
            {
                splitContainer2.SplitterDistance = 381;
            }
            else
            {
                int sd = Convert.ToInt32(splitcontain_2);
                if (sd > 5)
                {
                    splitContainer2.SplitterDistance = sd;
                }
            }

            //**************************************************************
            // Split Container 3
            if (splitcontain_3 == "")
            {
                splitContainer3.SplitterDistance = 235;
            }
            else
            {
                int sd = Convert.ToInt32(splitcontain_3);
                if (sd > 5)
                {
                    splitContainer3.SplitterDistance = sd;
                }
            }

            //**************************************************************
            // Split Container 4
            if (splitcontain_4 == "")
            {
                splitContainer4.SplitterDistance = 250;
            }
            else
            {
                int sd = Convert.ToInt32(splitcontain_4);
                if (sd > 5)
                {
                    splitContainer4.SplitterDistance = sd;
                }
            }

            //**************************************************************
            //Tabs Alignment Top/Bottom
            if (tabsalign == "Top")
            {
                View_TabsAlign_TopMenuItem.Checked = true;
                myTabControl.Alignment = TabAlignment.Top;
            }
            else
            {
                View_TabsAlign_BottomMenuItem.Checked = true;
                myTabControl.Alignment = TabAlignment.Bottom;
            }

            //**************************************************************
            //Show Status Strip
            if (showstatusstrip == "true")
            {
                View_StatusStripMenuItem.Checked = true;
                MyStatusStrip.Visible = true;
            }
            else
            {
                View_StatusStripMenuItem.Checked = false;
                MyStatusStrip.Visible = false;
            }

            //**************************************************************
            //Show Tool Strip
            if(showtoolstrip=="true")
            {
                View_ToolStripMenuItem.Checked = true;
                toolstrippanel.Visible = true;
                MyToolStripZ.Visible = true;
            }
            else
            {
                View_ToolStripMenuItem.Checked = false;
                toolstrippanel.Visible = false;
                MyToolStripZ.Visible = false;
            }

            //**************************************************************
            //Show Project Explorer Tree View
            if (showprojectexplorerview == "true")
            {
                View_ProjectExplorerMenuItem.Checked = true;
                splitContainer3.Panel1Collapsed = false;
            }
            else
            {
                View_ProjectExplorerMenuItem.Checked = false;
                splitContainer3.Panel1Collapsed = true;
            }

            //**************************************************************
            //Show Classes View
            if (showclassesview == "true")
            {
                View_ClassesViewMenuItem.Checked = true;
                splitContainer4.Panel1Collapsed = false;
                splitContainer1.SplitterDistance = this.Width - 250;
                splitContainer1.IsSplitterFixed = false;
                splitContainer1.Panel2Collapsed = false;

                if (showmethodsview == "true")
                {
                    splitContainer1.SplitterDistance = this.Width - 250;
                    splitContainer1.IsSplitterFixed = false;
                }
                else if (showmethodsview == "false")
                {
                    splitContainer4.Panel2Collapsed = true;
                }
            }
            else
            {
                splitContainer4.Panel1Collapsed = true;
                View_ClassesViewMenuItem.Checked = false;

                if (showmethodsview == "false")
                {
                    splitContainer1.Panel2Collapsed = true;
                }
            }

            //**************************************************************
            //Show Methods View
            if (showmethodsview == "true")
            {
                View_MethodsViewMenuItem.Checked = true;
                splitContainer4.Panel2Collapsed = false;
                splitContainer1.SplitterDistance = this.Width - 250;
                splitContainer1.IsSplitterFixed = false;
                splitContainer1.Panel2Collapsed = false;

                if (showclassesview == "true")
                {
                    splitContainer1.SplitterDistance = this.Width - 250;
                    splitContainer1.IsSplitterFixed = false;
                }
            }
            else
            {
                splitContainer4.Panel2Collapsed = true;
                View_MethodsViewMenuItem.Checked = false;

                if (showclassesview == "false")
                {
                    splitContainer1.Panel2Collapsed = true;
                }
            }

            //**************************************************************
            //Show Error List
            if (showerrorlist == "true")
            {
                View_ErrorsListMenuItem.Checked = true;
                splitContainer2.Panel2Collapsed = false;
            }
            else
            {
                View_ErrorsListMenuItem.Checked = false;
                splitContainer2.Panel2Collapsed = true;
            }

            //**************************************************************
            //Show Error Dialog
            if (showerrordialog == "true")
            {
                View_ShowErrorDialogMenuItem.Checked = true;
                showErrorDialog = true;
            }
            else
            {
                View_ShowErrorDialogMenuItem.Checked = false;
                showErrorDialog = false;
            }

            //****************************************************************
            // Show Start Page on start up
            if(showstartpageonstartup=="true")
            {
                int x = startpagepanel.Size.Width;
          /*      startpagepanel_silverjlabel.Location = new Point(x / 3, 38);*/
            }
            else
            {
                this.myTabControl.TabPages.Clear();
            }

            //**************************************************************
            //Show Line Numbers
            if (showlinenumbers == "true")
            {
                View_LineNumbersMenuItem.Checked = true;
            }
            else
            {
                View_LineNumbersMenuItem.Checked = false;
            }

            //**************************************************************
            //Show Line Highlighter
            if (showlinehighlighter == "true")
            {
                View_LineHighlighterMenuItem.Checked = true;
            }
            else
            {
                View_LineHighlighterMenuItem.Checked = false;
            }

            //**************************************************************
            //Show Invalid Lines
            if (showinvalidlines == "true")
            {
                View_InvalidLinesMenuItem.Checked = true;
            }
            else
            {
                View_InvalidLinesMenuItem.Checked = false;
            }

            //**************************************************************
            //Show End of Line Marker
            if (showendoflinemarkers == "true")
            {
                View_EndLineMarkerMenuItem.Checked = true;
            }
            else
            {
                View_EndLineMarkerMenuItem.Checked = false;
            }

            //**************************************************************
            //Show Visible Spaces
            if (showvisiblespaces == "true")
            {
                View_VisibleSpacesMenuItem.Checked = true;
            }
            else
            {
                View_VisibleSpacesMenuItem.Checked = false;
            }

            //**************************************************************
            //Auto Compile Java/Program
            if (autocompilejava == "true")
            {
                Run_AutoCompileProgramMenuItem.Checked = true;
            }
            else
            {
                Run_AutoCompileProgramMenuItem.Checked = false;
            }
        }



        //**************************************************************************************************************
        //      MainForm Closing
        //**************************************************************************************************************
        /// <summary>
        /// When form is closing show dialog whether want to save files or not
        /// it first check * on tabs,if it contain then show save dialog box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            Boolean isfile = false;

            if (myTabControl.TabCount > 0)
            {
                TabControl.TabPageCollection tabcoll2 = myTabControl.TabPages;

                foreach (TabPage tabpage in tabcoll2)
                {
                    if (tabpage.Text.Contains("*"))
                    {
                        isfile = true;
                    }
                }

                if (this.Text != "Silver-J" && isfile == true)
                {
                    DialogResult dr = MessageBox.Show("Save modified files before exist ?\n\nClick Yes to Save all modified files one by one\nClick No to exit without saving", "Save ?", MessageBoxButtons.YesNoCancel);

                    if (dr == DialogResult.Yes)
                    {
                        TabControl.TabPageCollection tabcoll = myTabControl.TabPages;

                        foreach (TabPage tabpage in tabcoll)
                        {
                            if (tabpage.Text.Contains("*"))
                            {
                                File_CloseProjectMenuItem_Click(sender, e);
                            }
                        }
                    }
                    else if (dr == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }

                else if (this.Text == "Silver-J" && isfile == true)
                {
                    DialogResult dr = MessageBox.Show("Save modified files before exist ?\n\nClick Yes to Save all modified files one by one\nClick No to exit without saving", "Save ?", MessageBoxButtons.YesNoCancel);

                    if (dr == DialogResult.Yes)
                    {
                        TabControl.TabPageCollection tabcoll = myTabControl.TabPages;

                        foreach (TabPage tabpage in tabcoll)
                        {
                            if (tabpage.Text.Contains("*"))
                            {
                                myTabControl.SelectedTab = tabpage;

                                DialogResult dg = MessageBox.Show("Do you want to save modified file  " + tabpage.Text + "  before close ?", "Save or Not", MessageBoxButtons.YesNo);

                                if (dg == DialogResult.Yes)
                                {
                                    File_SaveMenuItem_Click(sender, e);
                                    myTabControl.TabPages.Remove(tabpage);
                                }
                                else
                                {
                                    myTabControl.TabPages.Remove(tabpage);
                                }
                            }
                        }
                    }
                    else if (dr == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }

            // change spliter distances in config.slvjfile
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            int s1 = splitContainer1.SplitterDistance;
            int s2 = splitContainer2.SplitterDistance;
            int s3 = splitContainer3.SplitterDistance;
            int s4 = splitContainer4.SplitterDistance;


            //writing split container splitter distance to config.slvjfile
            XmlDocument doc = new XmlDocument();
            doc.Load(configfile);
            doc.SelectSingleNode("SilverJConfiguration/SplitContainer1").InnerText = s1.ToString();
            doc.SelectSingleNode("SilverJConfiguration/SplitContainer2").InnerText = s2.ToString();
            doc.SelectSingleNode("SilverJConfiguration/SplitContainer3").InnerText = s3.ToString();
            doc.SelectSingleNode("SilverJConfiguration/SplitContainer4").InnerText = s4.ToString();
            doc.Save(configfile);

        }


//***********************************************************************************************************************************
//                                           File
//***********************************************************************************************************************************



        //**************************************************************************************************************
        //      FileMenuItem Drop Down Opened
        //**************************************************************************************************************
        /// <summary>
        /// set visibility to File menu items according to opened files & project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            if (this.Text == "Silver-J")
            {
                File_New_ClassMenuItem.Enabled = false;
                File_New_PackageMenuItem.Enabled = false;
                File_New_InterfaceMenuItem.Enabled = false;
                File_New_EnumsMenuItem.Enabled = false;
                File_New_HTMLFileMenuItem.Enabled = false;
                File_New_CSSFileMenuItem.Enabled = false;
                File_New_TextFileMenuItem.Enabled = false;
                File_New_JavaScriptFileMenuItem.Enabled = false;
                File_New_SQLFileMenuItem.Enabled = false;
                File_New_XMLFileMenuItem.Enabled = false;
                File_New_NewFileMenuItem.Enabled = false;
                if (FilenameToolStripLabel.Text.Contains("\\") && myTabControl.TabCount > 0)
                {
                    File_SaveMenuItem.Enabled = true;
                    File_SaveAsMenuItem.Enabled = true;
                    File_CloseMenuItem.Enabled = true;
                    File_CloseAllMenuItem.Enabled = true;
                    File_PrintPreviewMenuItem.Enabled = true;
                    File_PrintMenuItem.Enabled = true;
                }
                else
                {
                    File_SaveMenuItem.Enabled = false;
                    File_SaveAsMenuItem.Enabled = false;
                    File_CloseMenuItem.Enabled = false;
                    File_CloseAllMenuItem.Enabled = false;
                    File_PrintPreviewMenuItem.Enabled = false;
                    File_PrintMenuItem.Enabled = false;
                }
                File_SaveAllMenuItem.Enabled = false;
                File_CloseProjectMenuItem.Enabled = false;
                File_DeleteProjectMenuItem.Enabled = false;
            }
            else
            {
                File_New_ClassMenuItem.Enabled = true;
                File_New_PackageMenuItem.Enabled = true;
                File_New_InterfaceMenuItem.Enabled = true;
                File_New_EnumsMenuItem.Enabled = true;
                File_New_HTMLFileMenuItem.Enabled = true;
                File_New_CSSFileMenuItem.Enabled = true;
                File_New_TextFileMenuItem.Enabled = true;
                File_New_JavaScriptFileMenuItem.Enabled = true;
                File_New_SQLFileMenuItem.Enabled = true;
                File_New_XMLFileMenuItem.Enabled = true;
                File_New_NewFileMenuItem.Enabled = true;
                File_SaveMenuItem.Enabled = true;
                File_SaveAsMenuItem.Enabled = true;
                File_SaveAllMenuItem.Enabled = true;
                File_CloseMenuItem.Enabled = true;
                File_CloseAllMenuItem.Enabled = true;
                File_CloseProjectMenuItem.Enabled = true;
                File_DeleteProjectMenuItem.Enabled = true;
                File_PrintPreviewMenuItem.Enabled = true;
                File_PrintMenuItem.Enabled = true;
            }
        }


        //**************************************************************************************************************
        //      File -> New -> Java Application Project
        //**************************************************************************************************************
        /// <summary>
        /// Show the New_JavaApplicationProject_Form
        /// Once project name is entered,the New_JavaApplicationProject_Form.cs file
        /// function CheckProjectType return integer value as project type(Application type)
        /// the project file is already created in New_JavaApplicationProject_Form.cs file
        /// we just need to read that file,writing contents to that file
        /// adding project names & file names to project explorer tree view
        /// writing current project name with path to defaultprojloc.slvjfile which is defined in
        /// New_JavaApplicationProject_Form.cs file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_New_JavaApplicationProjectMenuItem_Click(object sender, EventArgs e)
        {
            New_JavaApplicationProject_Form njap = new New_JavaApplicationProject_Form(this, ProjectExplorerTreeView, myTabControl);
            njap.ShowDialog();

            String projectname = "";
            String javaclassfilename = "";

                //create project with class
                if (njap.CheckProjectType() == 2)
                {
                    if (ReadCurrentProjectFileName() != "" && File.Exists(ReadCurrentProjectFileName()))
                    {
                        //first reading project name & java class file when project type = 2
                        String projectfilename = ReadCurrentProjectFileName();
                        if (File.Exists(projectfilename))
                        {
                            using (XmlReader xmlreader = XmlReader.Create(projectfilename))
                            {
                                while (xmlreader.Read())
                                {
                                    if (xmlreader.IsStartElement())
                                    {
                                        switch (xmlreader.Name.ToString())
                                        {
                                            case "ProjectName": projectname = xmlreader.ReadString();
                                                break;

                                            case "JavaClassFile": javaclassfilename = xmlreader.ReadString();
                                                break;
                                        }
                                    }
                                }
                            }

                            if (projectfilename != "" && javaclassfilename != "" && njap.getCreatedFileName() != "")
                            {
                                //adding nodes to tree view
                                //changing main form name to project name
                                //adding file name to filenametoolstriplabel
                                ProjectExplorerTreeView.Nodes.Clear();
                                myTabControl.TabPages.Clear();

                                String prjname = projectfilename.Substring(projectfilename.LastIndexOf("\\") + 1);
                                prjname = prjname.Remove(prjname.Length - 9);
                                this.Text = "Silver-J - [ " + prjname + " ]";

                                String jclassfilename = javaclassfilename.Substring(javaclassfilename.LastIndexOf("\\") + 1);
                                String jclassnamewithoutjava = jclassfilename.Remove(jclassfilename.Length - 5);

                                MyTabPage mytabpage = new MyTabPage(this);
                                mytabpage.Text = jclassfilename;
                                mytabpage.textEditor.Text = "/*******************************\n                    "+prjname+"     \n*********************************/"
                                     + "\npublic class " + jclassnamewithoutjava + "  {" + "\n                                                                " + "\n}";
                                mytabpage.textEditor.ContextMenuStrip = textEditorContextMenuStrip;

                                myTabControl.TabPages.Add(mytabpage);
                                myTabControl.SelectedTab = mytabpage;

                                TreeNode projecttreenode = new TreeNode();
                                projecttreenode.Text = prjname;
                                projecttreenode.ImageIndex = 6;
                                projecttreenode.SelectedImageIndex = 6;
                                ProjectExplorerTreeView.Nodes.Add(projecttreenode);
                                ProjectExplorerTreeView.SelectedNode = projecttreenode;

                                TreeNode trnode = ProjectExplorerTreeView.Nodes[0];
                                TreeNode jclassnode = new TreeNode();
                                jclassnode.Text = jclassfilename;
                                jclassnode.ImageIndex = 2;
                                jclassnode.SelectedImageIndex = 2;
                                trnode.Nodes.Add(jclassnode);
                                ProjectExplorerTreeView.ExpandAll();

                                if (njap.getCreatedFileName() != "")
                                {
                                    try
                                    {
                                        StreamWriter strw = new StreamWriter(njap.getCreatedFileName());
                                        strw.Write(mytabpage.textEditor.Text);
                                        strw.Close();
                                        strw.Dispose();
                                    }
                                    catch
                                    { }
                                }
                            }
                        }
                    }
                }

                 //create project without class 
                else if (njap.CheckProjectType() == 1)
                {
                    if (ReadCurrentProjectFileName() != "" && File.Exists(ReadCurrentProjectFileName()))
                    {
                        String projectfilename2 = ReadCurrentProjectFileName();
                        if (File.Exists(projectfilename2))
                        {
                            using (XmlReader xmlreader = XmlReader.Create(projectfilename2))
                            {
                                while (xmlreader.Read())
                                {
                                    if (xmlreader.IsStartElement())
                                    {
                                        switch (xmlreader.Name.ToString())
                                        {
                                            case "ProjectName": projectname = xmlreader.ReadString();
                                                break;
                                        }
                                    }
                                }
                            }

                            if (projectfilename2 != "")
                            {
                                ProjectExplorerTreeView.Nodes.Clear();
                                myTabControl.TabPages.Clear();

                                String prjname = projectfilename2.Substring(projectfilename2.LastIndexOf("\\") + 1);
                                prjname = prjname.Remove(prjname.Length - 9);
                                this.Text = "Silver-J - [ " + prjname + " ]";


                                TreeNode projecttreenode = new TreeNode();
                                projecttreenode.Text = prjname;
                                projecttreenode.ImageIndex = 6;
                                projecttreenode.SelectedImageIndex = 6;
                                ProjectExplorerTreeView.Nodes.Add(projecttreenode);
                                ProjectExplorerTreeView.SelectedNode = projecttreenode;
                            }
                        }
                    }
                }
            

            if (njap.IsFinished() == true)
            {
                FilenameToolStripLabel.Text = "Silver-J";
                AddFilesToProjectExplorerTreeView();
                WriteCurrentFileNames();
                CopyAllSourceFilesToSRCFolder();
                UpdateWindowsList_WindowMenu();
                SetVisibilityOfToolStripButtons();
                myTabControl_SelectedIndexChanged(sender, e);
            }
        }




        //**************************************************************************************************************
        //      File -> New -> Java Applet Project
        //**************************************************************************************************************
        /// <summary>
        /// same as creating java application project only here we will create an html file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_New_JavaAppletMenuItem_Click(object sender, EventArgs e)
        {
            New_JavaAppletProject_Form njaf = new New_JavaAppletProject_Form(this, ProjectExplorerTreeView, myTabControl);
            njaf.ShowDialog();

            String projectname = "";
            String javaclassfilename = "";
            String htmlfilename = "";


            //create project with class
            if (njaf.CheckProjectType() == 2)
            {
                if (ReadCurrentProjectFileName() != "" && File.Exists(ReadCurrentProjectFileName()))
                {
                    String projectfilename = ReadCurrentProjectFileName();
                    if (File.Exists(projectfilename))
                    {
                        using (XmlReader xmlreader = XmlReader.Create(projectfilename))
                        {
                            while (xmlreader.Read())
                            {
                                if (xmlreader.IsStartElement())
                                {
                                    switch (xmlreader.Name.ToString())
                                    {
                                        case "ProjectName": projectname = xmlreader.ReadString();
                                            break;

                                        case "JavaClassFile": javaclassfilename = xmlreader.ReadString();
                                            break;

                                        case "HTMLFile": htmlfilename = xmlreader.ReadString();
                                            break;
                                    }
                                }
                            }
                        }

                        if (projectfilename != "" && javaclassfilename != "" && htmlfilename != "")
                        {
                            ProjectExplorerTreeView.Nodes.Clear();
                            myTabControl.TabPages.Clear();

                            String prjname = projectfilename.Substring(projectfilename.LastIndexOf("\\") + 1);
                            prjname = prjname.Remove(prjname.Length - 9);
                            this.Text = "Silver-J - [ " + prjname + " ]";

                            String jclassfilename = javaclassfilename.Substring(javaclassfilename.LastIndexOf("\\") + 1);
                            String jclassnamewithoutjava = jclassfilename.Remove(jclassfilename.Length - 5);

                            String apphtmlname = htmlfilename.Substring(htmlfilename.LastIndexOf("\\") + 1);
                            String apphtmlnamewithouthtml = apphtmlname.Remove(apphtmlname.Length - 5);


                            MyTabPage mytabpage = new MyTabPage(this);
                            mytabpage.Text = jclassfilename;
                            myTabControl.TabPages.Add(mytabpage);
                            myTabControl.SelectedTab = mytabpage;

                            if (njaf.getCreatedHTMLFileName() != "")
                            {
                                try
                                {
                                    String jfile = njaf.getCreatedFileName();
                                    jfile = jfile.Substring(jfile.LastIndexOf("\\") + 1);
                                    jfile = jfile.Remove(jfile.Length - 5);

                                    mytabpage.textEditor.Text =  "/*******************************\n                    "+prjname+"     \n*********************************/"
                                         +"import java.applet.*;\npublic class " + jfile + " extends Applet  {\n                                       \n} ";

                                    mytabpage.textEditor.ContextMenuStrip = textEditorContextMenuStrip;

                                    StreamWriter strw = new StreamWriter(File.Create(njaf.getCreatedFileName()));
                                    strw.Write(mytabpage.textEditor.Text);
                                    strw.Close();
                                    strw.Dispose();
                                }
                                catch
                                { }
                            }

                            RichTextBox rtb = new RichTextBox();
                            try
                            {
                                String jfile = njaf.getCreatedFileName();
                                jfile = jfile.Substring(jfile.LastIndexOf("\\") + 1);
                                jfile = jfile.Replace(".java", ".class");

                                StreamReader strreader = new StreamReader(Application.StartupPath + "\\files\\appletcode.slvjappletfile");
                                rtb.Text = strreader.ReadToEnd();
                                rtb.Text = rtb.Text.Replace("projectname", prjname);
                                rtb.Text = rtb.Text.Replace("mainclassname", jfile);
                                strreader.Close();
                                strreader.Dispose();
                            }
                            catch
                            { }

                            MyTabPage htmltabpage = new MyTabPage(this);
                            htmltabpage.AddLanguages("HTML");
                            htmltabpage.Text = apphtmlname;
                            myTabControl.TabPages.Add(htmltabpage);
                            myTabControl.SelectedTab = htmltabpage;
                            htmltabpage.textEditor.Text = rtb.Text;
                            htmltabpage.textEditor.ContextMenuStrip = textEditorContextMenuStrip;

                            if (htmltabpage.Text.Contains("*"))
                            {
                                htmltabpage.Text = htmltabpage.Text.Remove(htmltabpage.Text.LastIndexOf("*"));
                            }

                            if (njaf.getCreatedHTMLFileName() != "")
                            {
                                try
                                {
                                    StreamWriter strw = new StreamWriter(File.Create(njaf.getCreatedHTMLFileName()));
                                    strw.Write(rtb.Text);
                                    strw.Close();
                                    strw.Dispose();
                                }
                                catch
                                { }
                            }

                            TreeNode projecttreenode = new TreeNode();
                            projecttreenode.Text = prjname;
                            projecttreenode.ImageIndex = 6;
                            projecttreenode.SelectedImageIndex = 6;
                            ProjectExplorerTreeView.Nodes.Add(projecttreenode);
                            ProjectExplorerTreeView.SelectedNode = projecttreenode;

                            TreeNode trnode = ProjectExplorerTreeView.Nodes[0];

                            TreeNode jclassnode = new TreeNode();
                            jclassnode.Text = jclassfilename;
                            jclassnode.ImageIndex = 2;
                            jclassnode.SelectedImageIndex = 2;
                            trnode.Nodes.Add(jclassnode);

                            TreeNode htmlnode = new TreeNode();
                            htmlnode.Text = apphtmlname;
                            htmlnode.ImageIndex = 1;
                            htmlnode.SelectedImageIndex = 1;
                            trnode.Nodes.Add(htmlnode);

                            ProjectExplorerTreeView.ExpandAll();


                        }
                    }
                }
            }



             //create project without class 
            else if (njaf.CheckProjectType() == 1)
            {
                if (ReadCurrentProjectFileName() != "" && File.Exists(ReadCurrentProjectFileName()))
                {
                    String projectfilename2 = ReadCurrentProjectFileName();

                    using (XmlReader xmlreader = XmlReader.Create(projectfilename2))
                    {
                        while (xmlreader.Read())
                        {
                            if (xmlreader.IsStartElement())
                            {
                                switch (xmlreader.Name.ToString())
                                {
                                    case "ProjectName": projectname = xmlreader.ReadString();
                                        break;

                                    case "HTMLFile": htmlfilename = xmlreader.ReadString();
                                        break;
                                }
                            }
                        }
                    }

                    if (projectfilename2 != "" && htmlfilename != "")
                    {
                        ProjectExplorerTreeView.Nodes.Clear();
                        myTabControl.TabPages.Clear();

                        String prjname = projectfilename2.Substring(projectfilename2.LastIndexOf("\\") + 1);
                        prjname = prjname.Remove(prjname.Length - 9);
                        this.Text = "Silver-J - [ " + prjname + " ]";

                        String apphtmlname2 = htmlfilename.Substring(htmlfilename.LastIndexOf("\\") + 1);


                        RichTextBox rtb = new RichTextBox();
                        try
                        {
                            StreamReader strreader = new StreamReader("files\\appletcode.slvjappletfile");
                            rtb.Text = strreader.ReadToEnd();
                            rtb.Text = rtb.Text.Replace("projectname", prjname);
                            strreader.Close();
                            strreader.Dispose();
                        }
                        catch
                        { }

                        MyTabPage htmltabpage = new MyTabPage(this);
                        htmltabpage.AddLanguages("HTML");
                        htmltabpage.Text = apphtmlname2;
                        myTabControl.TabPages.Add(htmltabpage);
                        myTabControl.SelectedTab = htmltabpage;
                        htmltabpage.textEditor.Text = rtb.Text;
                        htmltabpage.textEditor.ContextMenuStrip = textEditorContextMenuStrip;

                        if (htmltabpage.Text.Contains("*"))
                        {
                            htmltabpage.Text = htmltabpage.Text.Remove(htmltabpage.Text.LastIndexOf("*"));
                        }

                        if (njaf.getCreatedHTMLFileName() != "")
                        {
                            try
                            {
                                StreamWriter strw = new StreamWriter(File.Create(njaf.getCreatedHTMLFileName()));
                                strw.Write(rtb.Text);
                                strw.Close();
                                strw.Dispose();
                            }
                            catch
                            { }
                        }

                        TreeNode projecttreenode = new TreeNode();
                        projecttreenode.Text = prjname;
                        projecttreenode.ImageIndex = 6;
                        projecttreenode.SelectedImageIndex = 6;
                        ProjectExplorerTreeView.Nodes.Add(projecttreenode);
                        ProjectExplorerTreeView.SelectedNode = projecttreenode;

                        TreeNode trnode = ProjectExplorerTreeView.Nodes[0];

                        TreeNode htmlnode = new TreeNode();
                        htmlnode.Text = apphtmlname2;
                        htmlnode.ImageIndex = 2;
                        htmlnode.SelectedImageIndex = 2;
                        trnode.Nodes.Add(htmlnode);

                        ProjectExplorerTreeView.ExpandAll();
                    }
                }
            }

            if (njaf.IsFinished() == true)
            {
                FilenameToolStripLabel.Text = "Silver-J";
                AddFilesToProjectExplorerTreeView();
                WriteCurrentFileNames();
                SetVisibilityOfToolStripButtons();
                CopyAllSourceFilesToSRCFolder();
                UpdateWindowsList_WindowMenu();
                myTabControl_SelectedIndexChanged(sender, e);
            }
        }


        //**************************************************************************************************************
        //      File -> New -> Class
        //**************************************************************************************************************
        /// <summary>
        /// creates a new .java file and adding entry to current project file
        /// file name is taken from New_Clas_Form.cs function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_New_ClassMenuItem_Click(object sender, EventArgs e)
        {
            MyTabPage tb = new MyTabPage(this);
            New_Class_Form ncf = new New_Class_Form(tb, myTabControl);
            ncf.ShowDialog();
            String filename = ncf.getCreatedFileName();
            int sel = myTabControl.SelectedIndex;

            if (sel != -1)
            {
                var mytexteditor = myTabControl.TabPages[sel].Controls[0];
                mytexteditor.ContextMenuStrip = textEditorContextMenuStrip;
                
                if (filename != "")
                {
                    //check file name is already exists
                    //if not then create that file 
                    if (File.Exists(filename))
                    {
                        MessageBox.Show("The file name you entered is already exists in the folder or already added to your project", "Error......");
                    }
                    else
                    {
                        try
                        {
                            StreamWriter strw = new StreamWriter(File.Create(filename));
                            strw.Write(mytexteditor.Text);
                            strw.Close();
                            strw.Dispose();
                        }
                        catch
                        { }

                        //adding entry to current opened project file
                        //JavaClassFile & VisualFile
                        if (ReadCurrentProjectFileName() != "")
                        {
                            String projectfilename = ReadCurrentProjectFileName();
                            XmlDocument xmldoc = new XmlDocument();
                            xmldoc.Load(projectfilename);
                            XmlNode node = xmldoc.CreateNode(XmlNodeType.Element, "JavaClassFile", null);
                            node.InnerText = filename;
                            xmldoc.DocumentElement.AppendChild(node);
                            xmldoc.Save(projectfilename);

                            XmlDocument xmldoc2 = new XmlDocument();
                            xmldoc2.Load(projectfilename);
                            XmlNode node2 = xmldoc2.CreateNode(XmlNodeType.Element, "VisualFile", null);
                            node2.InnerText = filename;
                            xmldoc2.DocumentElement.AppendChild(node2);
                            xmldoc2.Save(projectfilename);
                        }
                    }
                }
            }


            if (ncf.IsFinished() == true)
            {
                AddFilesToProjectExplorerTreeView();
                WriteCurrentFileNames();
                CopyAllSourceFilesToSRCFolder();
                UpdateWindowsList_WindowMenu();
                myTabControl_SelectedIndexChanged(sender, e);

                //check srcclasses directory exists or not
                if (Directory.Exists(getCurrentProjectLocationFolder() + "\\srcclasses"))
                {
                    String mainclassfile = "";
                    using (XmlReader reader = XmlReader.Create(ReadCurrentProjectFileName()))
                    {
                        while (reader.Read())
                        {
                            if (reader.IsStartElement())
                            {
                                switch (reader.Name.ToString())
                                {
                                    case "MainClassFile":
                                        mainclassfile = reader.ReadString();
                                        break;
                                }
                            }
                        }
                    }
                    if (mainclassfile == "")
                    {
                        if (ReadCurrentProjectFileName() != "")
                        {
                            String projectfilename = ReadCurrentProjectFileName();
                            XmlDocument xmldoc = new XmlDocument();
                            xmldoc.Load(projectfilename);
                            XmlNode node = xmldoc.CreateNode(XmlNodeType.Element, "MainClassFile", null);
                            node.InnerText = filename;
                            xmldoc.DocumentElement.AppendChild(node);
                            xmldoc.Save(projectfilename);
                        }
                    }
                }
            }
        }



        //**************************************************************************************************************
        //      File -> New -> Package
        //**************************************************************************************************************
        /// <summary>
        /// creates a folder in srcclasses directory
        /// if entered package name is AAA.BBB.CCC then AAA\BBB\CCC folder is created
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_New_PackageMenuItem_Click(object sender, EventArgs e)
        {
            MyTabPage mytabpage = new MyTabPage(this);
            New_Package_Form npf = new New_Package_Form(mytabpage, myTabControl);
            npf.ShowDialog();
            if (npf.TypeOfPackage() == 1)
            {
                String filename = npf.getCreatedClassFileName();
                String packagename = npf.getCreatedPackageName();
                String packagefolderpath = npf.getCreatedPackageFolderPath();
                int sel = myTabControl.SelectedIndex;

                packagelist.Add(packagefolderpath);

                if (sel != -1)
                {
                    var mytexteditor = myTabControl.TabPages[sel].Controls[0];
                    mytexteditor.ContextMenuStrip = textEditorContextMenuStrip;

                    if (npf.IsFormClosed() == true)
                    {
                        if (File.Exists(filename))
                        {
                            MessageBox.Show("The package file name you entered is already exists in the folder or already added to your project", "Error......");
                        }
                        else
                        {
                            try
                            {
                                StreamWriter strw = new StreamWriter(File.Create(filename));
                                strw.Write(mytexteditor.Text);
                                strw.Close();
                                strw.Dispose();
                            }
                            catch
                            { }

                            //adding java package file to current project file
                            if (ReadCurrentProjectFileName() != "")
                            {
                                String projectfilename = ReadCurrentProjectFileName();
                                XmlDocument xmldoc = new XmlDocument();
                                xmldoc.Load(projectfilename);
                                XmlNode node = xmldoc.CreateNode(XmlNodeType.Element, "PackageJavaClassFile", null);
                                node.InnerText = filename;
                                xmldoc.DocumentElement.AppendChild(node);
                                xmldoc.Save(projectfilename);

                                XmlDocument xmldoc2 = new XmlDocument();
                                xmldoc2.Load(projectfilename);
                                XmlNode node2 = xmldoc2.CreateNode(XmlNodeType.Element, "VisualFile", null);
                                node2.InnerText = filename;
                                xmldoc2.DocumentElement.AppendChild(node2);
                                xmldoc2.Save(projectfilename);
                            }

                            //adding package name to current project file
                            if (ReadCurrentProjectFileName() != "")
                            {
                                String projectfilename = ReadCurrentProjectFileName();
                                XmlDocument xmldoc = new XmlDocument();
                                xmldoc.Load(projectfilename);
                                XmlNode packnamenode = xmldoc.CreateNode(XmlNodeType.Element, "PackageName", null);
                                packnamenode.InnerText = packagename;
                                xmldoc.DocumentElement.AppendChild(packnamenode);
                                xmldoc.Save(projectfilename);
                            }

                            //adding package folder name to current project file
                            if (ReadCurrentProjectFileName() != "")
                            {
                                String projectfilename = ReadCurrentProjectFileName();
                                XmlDocument xmldoc = new XmlDocument();
                                xmldoc.Load(projectfilename);
                                XmlNode packfolderpathnode = xmldoc.CreateNode(XmlNodeType.Element, "PackageFolderName", null);
                                packfolderpathnode.InnerText = packagefolderpath;
                                xmldoc.DocumentElement.AppendChild(packfolderpathnode);
                                xmldoc.Save(projectfilename);
                            }
                        }
                    }
                }
            }

            else if (npf.TypeOfPackage() == 2)
            {
                String filename = npf.getCreatedClassFileName();
                int sel = myTabControl.SelectedIndex;
                if (sel != -1)
                {
                    var mytexteditor = myTabControl.TabPages[sel].Controls[0];
                    mytexteditor.ContextMenuStrip = textEditorContextMenuStrip;

                    if (npf.IsFormClosed() == true)
                    {
                        if (File.Exists(filename))
                        {
                            MessageBox.Show("The package file name you entered is already exists in the folder or already added to your project", "Error......");
                        }
                        else
                        {
                            try
                            {
                                StreamWriter strw = new StreamWriter(File.Create(filename));
                                strw.Write(mytexteditor.Text);
                                strw.Close();
                                strw.Dispose();
                            }
                            catch
                            { }

                            if (ReadCurrentProjectFileName() != "")
                            {
                                String projectfilename = ReadCurrentProjectFileName();
                                XmlDocument xmldoc = new XmlDocument();
                                xmldoc.Load(projectfilename);
                                XmlNode node = xmldoc.CreateNode(XmlNodeType.Element, "PackageJavaClassFile", null);
                                node.InnerText = filename;
                                xmldoc.DocumentElement.AppendChild(node);
                                xmldoc.Save(projectfilename);

                                XmlDocument xmldoc2 = new XmlDocument();
                                xmldoc2.Load(projectfilename);
                                XmlNode node2 = xmldoc2.CreateNode(XmlNodeType.Element, "VisualFile", null);
                                node2.InnerText = filename;
                                xmldoc2.DocumentElement.AppendChild(node2);
                                xmldoc2.Save(projectfilename);
                            }
                        }
                    }
                }
            }


            if (npf.IsFinished() == true)
            {
                AddFilesToProjectExplorerTreeView();
                WriteCurrentFileNames();
                CopyAllSourceFilesToSRCFolder();
                UpdateWindowsList_WindowMenu();
                myTabControl_SelectedIndexChanged(sender, e);
            }
        }


        //**************************************************************************************************************
        //      File -> New -> Interface
        //**************************************************************************************************************
        /// <summary>
        /// create java file and write interface code to it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_New_InterfaceMenuItem_Click(object sender, EventArgs e)
        {
            MyTabPage tb = new MyTabPage(this);
            New_Interface_Form nif = new New_Interface_Form(tb, myTabControl);
            nif.ShowDialog();
            String filename = nif.getCreatedFileName();
            int sel = myTabControl.SelectedIndex;
            if (sel != -1)
            {
                var mytexteditor = myTabControl.TabPages[sel].Controls[0];
                mytexteditor.ContextMenuStrip = textEditorContextMenuStrip;

                if (filename != "")
                {
                    if (File.Exists(filename))
                    {
                        MessageBox.Show("The file name you entered is already exists in the folder or already added to your project", "Error......");
                    }
                    else
                    {
                        try
                        {
                            StreamWriter strw = new StreamWriter(File.Create(filename));
                            strw.Write(mytexteditor.Text);
                            strw.Close();
                            strw.Dispose();
                        }
                        catch
                        { }

                        if (ReadCurrentProjectFileName() != "")
                        {
                            String projectfilename = ReadCurrentProjectFileName();
                            XmlDocument xmldoc = new XmlDocument();
                            xmldoc.Load(projectfilename);
                            XmlNode node = xmldoc.CreateNode(XmlNodeType.Element, "JavaClassFile", null);
                            node.InnerText = filename;
                            xmldoc.DocumentElement.AppendChild(node);
                            xmldoc.Save(projectfilename);

                            XmlDocument xmldoc2 = new XmlDocument();
                            xmldoc2.Load(projectfilename);
                            XmlNode node2 = xmldoc2.CreateNode(XmlNodeType.Element, "VisualFile", null);
                            node2.InnerText = filename;
                            xmldoc2.DocumentElement.AppendChild(node2);
                            xmldoc2.Save(projectfilename);
                        }
                    }
                }
            }


            if (nif.IsFinished() == true)
            {
                AddFilesToProjectExplorerTreeView();
                WriteCurrentFileNames();
                CopyAllSourceFilesToSRCFolder();
                UpdateWindowsList_WindowMenu();
                myTabControl_SelectedIndexChanged(sender, e);
            }
        }


        //**************************************************************************************************************
        //      File -> New -> Enums
        //**************************************************************************************************************
        private void File_New_EnumsMenuItem_Click(object sender, EventArgs e)
        {
            MyTabPage tb = new MyTabPage(this);
            New_Enums_Form nef = new New_Enums_Form(tb, myTabControl);
            nef.ShowDialog();
            String filename = nef.getCreatedFileName();
            int sel = myTabControl.SelectedIndex;
            if (sel != -1)
            {
                var mytexteditor = myTabControl.TabPages[sel].Controls[0];
                mytexteditor.ContextMenuStrip = textEditorContextMenuStrip;

                if (filename != "")
                {
                    if (File.Exists(filename))
                    {
                        MessageBox.Show("The file name you entered is already exists in the folder or already added to your project", "Error......");
                    }
                    else
                    {
                        try
                        {
                            StreamWriter strw = new StreamWriter(File.Create(filename));
                            strw.Write(mytexteditor.Text);
                            strw.Close();
                            strw.Dispose();
                        }
                        catch
                        { }

                        if (ReadCurrentProjectFileName() != "")
                        {
                            String projectfilename = ReadCurrentProjectFileName();
                            XmlDocument xmldoc = new XmlDocument();
                            xmldoc.Load(projectfilename);
                            XmlNode node = xmldoc.CreateNode(XmlNodeType.Element, "JavaClassFile", null);
                            node.InnerText = filename;
                            xmldoc.DocumentElement.AppendChild(node);
                            xmldoc.Save(projectfilename);

                            XmlDocument xmldoc2 = new XmlDocument();
                            xmldoc2.Load(projectfilename);
                            XmlNode node2 = xmldoc2.CreateNode(XmlNodeType.Element, "VisualFile", null);
                            node2.InnerText = filename;
                            xmldoc2.DocumentElement.AppendChild(node2);
                            xmldoc2.Save(projectfilename);
                        }
                    }
                }
            }


            if (nef.IsFinished() == true)
            {
                AddFilesToProjectExplorerTreeView();
                WriteCurrentFileNames();
                CopyAllSourceFilesToSRCFolder();
                UpdateWindowsList_WindowMenu();
                myTabControl_SelectedIndexChanged(sender, e);
            }
        }


        //**************************************************************************************************************
        //      File -> New -> HTML File
        //**************************************************************************************************************
        /// <summary>
        /// creates .html file and adds html source to it if option was selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_New_HTMLFileMenuItem_Click(object sender, EventArgs e)
        {
            MyTabPage tb = new MyTabPage(this);
            tb.AddLanguages("HTML");
            New_HTMLFile_Form nhtmlf = new New_HTMLFile_Form(tb, myTabControl);
            nhtmlf.ShowDialog();
            String filename = nhtmlf.getCreatedFileName();
            int sel = myTabControl.SelectedIndex;
            if (sel != -1)
            {
                var mytexteditor = myTabControl.TabPages[sel].Controls[0];
                mytexteditor.ContextMenuStrip = textEditorContextMenuStrip;

                if (filename != "")
                {
                    if (File.Exists(filename))
                    {
                        MessageBox.Show("The file name you entered is already exists in the folder or already added to your project", "Error......");
                    }
                    else
                    {
                        try
                        {
                            StreamWriter strw = new StreamWriter(File.Create(filename));
                            strw.Write(mytexteditor.Text);
                            strw.Close();
                            strw.Dispose();
                        }
                        catch
                        { }

                        if (ReadCurrentProjectFileName() != "")
                        {
                            String projectfilename = ReadCurrentProjectFileName();
                            XmlDocument xmldoc = new XmlDocument();
                            xmldoc.Load(projectfilename);
                            XmlNode node = xmldoc.CreateNode(XmlNodeType.Element, "HTMLFile", null);
                            node.InnerText = filename;
                            xmldoc.DocumentElement.AppendChild(node);
                            xmldoc.Save(projectfilename);

                            XmlDocument xmldoc2 = new XmlDocument();
                            xmldoc2.Load(projectfilename);
                            XmlNode node2 = xmldoc2.CreateNode(XmlNodeType.Element, "VisualFile", null);
                            node2.InnerText = filename;
                            xmldoc2.DocumentElement.AppendChild(node2);
                            xmldoc2.Save(projectfilename);
                        }
                    }
                }
            }


            if (nhtmlf.IsFinished() == true)
            {
                AddFilesToProjectExplorerTreeView();
                WriteCurrentFileNames();
                CopyAllSourceFilesToSRCFolder();
                UpdateWindowsList_WindowMenu();
                myTabControl_SelectedIndexChanged(sender, e);
            }
        }

        //**************************************************************************************************************
        //      File -> New -> CSS File
        //**************************************************************************************************************
        private void File_New_CSSFileMenuItem_Click(object sender, EventArgs e)
        {
            MyTabPage tb = new MyTabPage(this);
            New_CSSFile_Form ncssf = new New_CSSFile_Form(tb, myTabControl);
            ncssf.ShowDialog();
            String filename = ncssf.getCreatedFileName();
            int sel = myTabControl.SelectedIndex;
            if (sel != -1)
            {
                var mytexteditor = myTabControl.TabPages[sel].Controls[0];
                mytexteditor.ContextMenuStrip = textEditorContextMenuStrip;

                if (filename != "")
                {
                    if (File.Exists(filename))
                    {
                        MessageBox.Show("The file name you entered is already exists in the folder or already added to your project", "Error......");
                    }
                    else
                    {
                        try
                        {
                            StreamWriter strw = new StreamWriter(File.Create(filename));
                            strw.Write(mytexteditor.Text);
                            strw.Close();
                            strw.Dispose();
                        }
                        catch
                        { }

                        if (ReadCurrentProjectFileName() != "")
                        {
                            String projectfilename = ReadCurrentProjectFileName();
                            XmlDocument xmldoc = new XmlDocument();
                            xmldoc.Load(projectfilename);
                            XmlNode node = xmldoc.CreateNode(XmlNodeType.Element, "CSSFile", null);
                            node.InnerText = filename;
                            xmldoc.DocumentElement.AppendChild(node);
                            xmldoc.Save(projectfilename);

                            XmlDocument xmldoc2 = new XmlDocument();
                            xmldoc2.Load(projectfilename);
                            XmlNode node2 = xmldoc2.CreateNode(XmlNodeType.Element, "VisualFile", null);
                            node2.InnerText = filename;
                            xmldoc2.DocumentElement.AppendChild(node2);
                            xmldoc2.Save(projectfilename);
                        }
                    }
                }
            }


            if (ncssf.IsFinished() == true)
            {
                AddFilesToProjectExplorerTreeView();
                WriteCurrentFileNames();
                CopyAllSourceFilesToSRCFolder();
                UpdateWindowsList_WindowMenu();
                myTabControl_SelectedIndexChanged(sender, e);
            }
        }

        //**************************************************************************************************************
        //      File -> New -> Text File
        //**************************************************************************************************************
        private void File_New_TextFileMenuItem_Click(object sender, EventArgs e)
        {
            MyTabPage tb = new MyTabPage(this);
            tb.AddLanguages("Text");
            New_TextFile_Form ntxtf = new New_TextFile_Form(tb, myTabControl);
            ntxtf.ShowDialog();
            String filename = ntxtf.getCreatedFileName();
            int sel = myTabControl.SelectedIndex;
            if (sel != -1)
            {
                var mytexteditor = myTabControl.TabPages[sel].Controls[0];
                mytexteditor.ContextMenuStrip = textEditorContextMenuStrip;

                if (filename != "")
                {
                    if (File.Exists(filename))
                    {
                        MessageBox.Show("The file name you entered is already exists in the folder or already added to your project", "Error......");
                    }
                    else
                    {
                        try
                        {
                            StreamWriter strw = new StreamWriter(File.Create(filename));
                            strw.Write(mytexteditor.Text);
                            strw.Close();
                            strw.Dispose();
                        }
                        catch
                        { }

                        if (ReadCurrentProjectFileName() != "")
                        {
                            String projectfilename = ReadCurrentProjectFileName();
                            XmlDocument xmldoc = new XmlDocument();
                            xmldoc.Load(projectfilename);
                            XmlNode node = xmldoc.CreateNode(XmlNodeType.Element, "TextFile", null);
                            node.InnerText = filename;
                            xmldoc.DocumentElement.AppendChild(node);
                            xmldoc.Save(projectfilename);

                            XmlDocument xmldoc2 = new XmlDocument();
                            xmldoc2.Load(projectfilename);
                            XmlNode node2 = xmldoc2.CreateNode(XmlNodeType.Element, "VisualFile", null);
                            node2.InnerText = filename;
                            xmldoc2.DocumentElement.AppendChild(node2);
                            xmldoc2.Save(projectfilename);
                        }
                    }
                }
            }


            if (ntxtf.IsFinished() == true)
            {
                AddFilesToProjectExplorerTreeView();
                WriteCurrentFileNames();
                CopyAllSourceFilesToSRCFolder();
                UpdateWindowsList_WindowMenu();
                myTabControl_SelectedIndexChanged(sender, e);
            }
        }

        //**************************************************************************************************************
        //      File -> New -> JavaScript File
        //**************************************************************************************************************
        private void File_New_JavaScriptFileMenuItem_Click(object sender, EventArgs e)
        {
            MyTabPage tb = new MyTabPage(this);
            tb.AddLanguages("JavaScript");
            New_JavaScriptFile_Form njsf = new New_JavaScriptFile_Form(tb, myTabControl);
            njsf.ShowDialog();
            String filename = njsf.getCreatedFileName();
            int sel = myTabControl.SelectedIndex;
            if (sel != -1)
            {
                var mytexteditor = myTabControl.TabPages[sel].Controls[0];
                mytexteditor.ContextMenuStrip = textEditorContextMenuStrip;

                if (filename != "")
                {
                    if (File.Exists(filename))
                    {
                        MessageBox.Show("The file name you entered is already exists in the folder or already added to your project", "Error......");
                    }
                    else
                    {
                        try
                        {
                            StreamWriter strw = new StreamWriter(File.Create(filename));
                            strw.Write(mytexteditor.Text);
                            strw.Close();
                            strw.Dispose();
                        }
                        catch
                        { }

                        if (ReadCurrentProjectFileName() != "")
                        {
                            String projectfilename = ReadCurrentProjectFileName();
                            XmlDocument xmldoc = new XmlDocument();
                            xmldoc.Load(projectfilename);
                            XmlNode node = xmldoc.CreateNode(XmlNodeType.Element, "JavaScriptFile", null);
                            node.InnerText = filename;
                            xmldoc.DocumentElement.AppendChild(node);
                            xmldoc.Save(projectfilename);

                            XmlDocument xmldoc2 = new XmlDocument();
                            xmldoc2.Load(projectfilename);
                            XmlNode node2 = xmldoc2.CreateNode(XmlNodeType.Element, "VisualFile", null);
                            node2.InnerText = filename;
                            xmldoc2.DocumentElement.AppendChild(node2);
                            xmldoc2.Save(projectfilename);
                        }
                    }
                }
            }

            if (njsf.IsFinished() == true)
            {
                AddFilesToProjectExplorerTreeView();
                WriteCurrentFileNames();
                CopyAllSourceFilesToSRCFolder();
                UpdateWindowsList_WindowMenu();
                myTabControl_SelectedIndexChanged(sender, e);
            }
        }

        //**************************************************************************************************************
        //      File -> New -> SQL File
        //**************************************************************************************************************
        private void File_New_SQLFileMenuItem_Click(object sender, EventArgs e)
        {
            MyTabPage tb = new MyTabPage(this);
            tb.AddLanguages("SQL");
            New_SQLFile_Form nsqlf = new New_SQLFile_Form(tb, myTabControl);
            nsqlf.ShowDialog();
            String filename = nsqlf.getCreatedFileName();
            int sel = myTabControl.SelectedIndex;
            if (sel != -1)
            {
                var mytexteditor = myTabControl.TabPages[sel].Controls[0];
                mytexteditor.ContextMenuStrip = textEditorContextMenuStrip;

                if (filename != "")
                {
                    if (File.Exists(filename))
                    {
                        MessageBox.Show("The file name you entered is already exists in the folder or already added to your project", "Error......");
                    }
                    else
                    {
                        try
                        {
                            StreamWriter strw = new StreamWriter(File.Create(filename));
                            strw.Write(mytexteditor.Text);
                            strw.Close();
                            strw.Dispose();
                        }
                        catch
                        { }

                        if (ReadCurrentProjectFileName() != "")
                        {
                            String projectfilename = ReadCurrentProjectFileName();
                            XmlDocument xmldoc = new XmlDocument();
                            xmldoc.Load(projectfilename);
                            XmlNode node = xmldoc.CreateNode(XmlNodeType.Element, "SQLFile", null);
                            node.InnerText = filename;
                            xmldoc.DocumentElement.AppendChild(node);
                            xmldoc.Save(projectfilename);

                            XmlDocument xmldoc2 = new XmlDocument();
                            xmldoc2.Load(projectfilename);
                            XmlNode node2 = xmldoc2.CreateNode(XmlNodeType.Element, "VisualFile", null);
                            node2.InnerText = filename;
                            xmldoc2.DocumentElement.AppendChild(node2);
                            xmldoc2.Save(projectfilename);
                        }
                    }
                }
            }


            if (nsqlf.IsFinished() == true)
            {
                AddFilesToProjectExplorerTreeView();
                WriteCurrentFileNames();
                CopyAllSourceFilesToSRCFolder();
                UpdateWindowsList_WindowMenu();
                myTabControl_SelectedIndexChanged(sender, e);
            }
        }

        //**************************************************************************************************************
        //      File -> New -> XML File
        //**************************************************************************************************************
        private void File_New_XMLFileMenuItem_Click(object sender, EventArgs e)
        {
            MyTabPage tb = new MyTabPage(this);
            tb.AddLanguages("XML");
            New_XMLFile_Form nxmlf = new New_XMLFile_Form(tb, myTabControl);
            nxmlf.ShowDialog();
            String filename = nxmlf.getCreatedFileName();
            int sel = myTabControl.SelectedIndex;
            if (sel != -1)
            {
                var mytexteditor = myTabControl.TabPages[sel].Controls[0];
                mytexteditor.ContextMenuStrip = textEditorContextMenuStrip;

                if (filename != "")
                {
                    if (File.Exists(filename))
                    {
                        MessageBox.Show("The file name you entered is already exists in the folder or already added to your project", "Error......");
                    }
                    else
                    {
                        try
                        {
                            StreamWriter strw = new StreamWriter(File.Create(filename));
                            strw.Write(mytexteditor.Text);
                            strw.Close();
                            strw.Dispose();
                        }
                        catch
                        { }

                        if (ReadCurrentProjectFileName() != "")
                        {
                            String projectfilename = ReadCurrentProjectFileName();
                            XmlDocument xmldoc = new XmlDocument();
                            xmldoc.Load(projectfilename);
                            XmlNode node = xmldoc.CreateNode(XmlNodeType.Element, "XMLFile", null);
                            node.InnerText = filename;
                            xmldoc.DocumentElement.AppendChild(node);
                            xmldoc.Save(projectfilename);

                            XmlDocument xmldoc2 = new XmlDocument();
                            xmldoc2.Load(projectfilename);
                            XmlNode node2 = xmldoc2.CreateNode(XmlNodeType.Element, "VisualFile", null);
                            node2.InnerText = filename;
                            xmldoc2.DocumentElement.AppendChild(node2);
                            xmldoc2.Save(projectfilename);
                        }
                    }
                }
            }


            if (nxmlf.IsFinished() == true)
            {
                AddFilesToProjectExplorerTreeView();
                WriteCurrentFileNames();
                CopyAllSourceFilesToSRCFolder();
                UpdateWindowsList_WindowMenu();
                myTabControl_SelectedIndexChanged(sender, e);
            }
        }

        //**************************************************************************************************************
        //      File -> New -> New File
        //**************************************************************************************************************
        private void File_New_NewFileMenuItem_Click(object sender, EventArgs e)
        {
            MyTabPage tb = new MyTabPage(this);
            tb.AddLanguages("Text");
            New_NewFile_Form nnewf = new New_NewFile_Form(tb, myTabControl);
            nnewf.ShowDialog();
            String filename = nnewf.getCreatedFileName();
            int sel = myTabControl.SelectedIndex;
            if (sel != -1)
            {
                var mytexteditor = myTabControl.TabPages[sel].Controls[0];
                mytexteditor.ContextMenuStrip = textEditorContextMenuStrip;

                if (filename != "")
                {
                    if (File.Exists(filename))
                    {
                        MessageBox.Show("The file name you entered is already exists in the folder or already added to your project", "Error......");
                    }
                    else
                    {
                        try
                        {
                            StreamWriter strw = new StreamWriter(File.Create(filename));
                            strw.Write(mytexteditor.Text);
                            strw.Close();
                            strw.Dispose();
                        }
                        catch
                        { }

                        if (ReadCurrentProjectFileName() != "")
                        {
                            String projectfilename = ReadCurrentProjectFileName();
                            XmlDocument xmldoc = new XmlDocument();
                            xmldoc.Load(projectfilename);
                            XmlNode node = xmldoc.CreateNode(XmlNodeType.Element, "NewFile", null);
                            node.InnerText = filename;
                            xmldoc.DocumentElement.AppendChild(node);
                            xmldoc.Save(projectfilename);

                            XmlDocument xmldoc2 = new XmlDocument();
                            xmldoc2.Load(projectfilename);
                            XmlNode node2 = xmldoc2.CreateNode(XmlNodeType.Element, "VisualFile", null);
                            node2.InnerText = filename;
                            xmldoc2.DocumentElement.AppendChild(node2);
                            xmldoc2.Save(projectfilename);
                        }
                    }
                }
            }

            if (nnewf.IsFinished() == true)
            {
                AddFilesToProjectExplorerTreeView();
                WriteCurrentFileNames();
                CopyAllSourceFilesToSRCFolder();
                UpdateWindowsList_WindowMenu();
                myTabControl_SelectedIndexChanged(sender, e);
            }
        }




        //**************************************************************************************************************
        //      File -> Open Project
        //**************************************************************************************************************
        /// <summary>
        /// project file(.slvjproj) is selected from open file dialog
        /// each tag is read from file
        /// adding project names & files to tree view with image
        /// adding tabs to tabcontrol by reading files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_OpenProjectMenuItem_Click(object sender, EventArgs e)
        {
            //setting the value of IsClassCreated & isDataTypeDeclared property to false
            // in file MyTabPage.cs
            new MyTabPage(this).IsClassCreated = false;
            new MyTabPage(this).IsDataTypeDeclared = false;

            Boolean isfinished = false;
            if (this.Text != "Silver-J")
            {
                //check whether want to close current project or not
                //if yes then it is true otherwise false
                OpenProjectDialog opd = new OpenProjectDialog();
                opd.ShowDialog();
                isfinished = opd.IsFinished();

                if (isfinished==true)
                {
                    File_CloseProjectMenuItem_Click(sender, e);

                    if (OpenProjectFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Loading_Form frm = new Loading_Form();
                        frm.Show();


                        ProjectExplorerTreeView.Nodes.Clear();
                        myTabControl.TabPages.Clear();
                        this.Text = "Silver-J";

                        String projectfilename = OpenProjectFileDialog.FileName;

                        //read all files from selected project file .slvjproj
                        List<String> fileslist = new List<String> { };
                        List<String> otherfileslist = new List<String> { };
                        String projectname = "";
                        String projecttype = "";

                        if (File.Exists(projectfilename))
                        {
                            using (XmlReader reader = XmlReader.Create(projectfilename))
                            {
                                while (reader.Read())
                                {
                                    if (reader.IsStartElement())
                                    {
                                        switch (reader.Name.ToString())
                                        {
                                            case "ProjectName":
                                                projectname = reader.ReadString();
                                                break;

                                            case "ProjectType":
                                                projecttype = reader.ReadString();
                                                break;

                                            case "VisualFile":
                                                fileslist.Add(reader.ReadString());
                                                break;

                                            case "OtherFile":
                                                otherfileslist.Add(reader.ReadString());
                                                break;
                                        }
                                    }
                                }
                            }


                            //add filename to defaultprojloc.slvjfile
                            String defaultprojfilepath = Application.StartupPath + "\\files\\defaultprojloc.slvjfile";

                            XmlDocument doc = new XmlDocument();
                            doc.Load(defaultprojfilepath);
                            doc.SelectSingleNode("SilverJ/CurrentProjectName").InnerText = projectname;
                            doc.SelectSingleNode("SilverJ/CurrentProjectFileName").InnerText = projectfilename;
                            doc.SelectSingleNode("SilverJ/CurrentProjectType").InnerText = projecttype;
                            doc.Save(defaultprojfilepath);


                            //add project name to form
                            this.Text = "Silver-J - [ " + projectname + " ]";

                            //add all files to myTabControl
                            foreach (String file in fileslist)
                            {
                                if (File.Exists(file))
                                {
                                    String filename = file.Substring(file.LastIndexOf("\\") + 1);
                                    String langs = "Java";
                                    if (filename.Contains(".java"))
                                    {
                                        langs = "Java";
                                    }
                                    else if (filename.Contains(".html"))
                                    {
                                        langs = "HTML";
                                    }
                                    else if (filename.Contains(".css"))
                                    {
                                        langs = "Java";
                                    }
                                    else if (filename.Contains(".js"))
                                    {
                                        langs = "JavaScript";
                                    }
                                    else if (filename.Contains(".txt"))
                                    {
                                        langs = "Text";
                                    }
                                    else if (filename.Contains(".sql"))
                                    {
                                        langs = "SQL";
                                    }
                                    else if (filename.Contains(".xml"))
                                    {
                                        langs = "XML";
                                    }
                                    else
                                    {
                                        langs = "Text";
                                    }

                                    MyTabPage tabpage = new MyTabPage(this);
                                    tabpage.AddLanguages(langs);

                                    tabpage.textEditor.ContextMenuStrip = textEditorContextMenuStrip;

                                    //read each file text and add it to textEditor
                                    StreamReader filereader = new StreamReader(file);
                                    tabpage.textEditor.Text = filereader.ReadToEnd();
                                    filereader.Close();

                                    //add tab pages with filename
                                    tabpage.Text = filename;
                                    myTabControl.TabPages.Add(tabpage);
                                    myTabControl.SelectedTab = tabpage;

                                    //add other selected files to ProjectExplorerTreeView
                                    foreach (String file2 in otherfileslist)
                                    {
                                        if (file2 != "")
                                        {
                                            String filename2 = file2.Substring(file2.LastIndexOf("\\") + 1);
                                            TreeNode trnode = new TreeNode();
                                            trnode.Text = filename2;
                                            trnode.ImageIndex = 8;
                                            trnode.SelectedImageIndex = 8;
                                            trnode.BackColor = Color.FromArgb(255, 255, 210);
                                            ProjectExplorerTreeView.Nodes.Add(trnode);
                                        }
                                    }
                                }
                            }
                        }
                        AddFilesToProjectExplorerTreeViewFromOpenedProject(projectfilename);
                        WriteCurrentFileNames();
                        SetVisibilityOfToolStripButtons();
                        CopyAllSourceFilesToSRCFolder();
                        UpdateWindowsList_WindowMenu();
                        RemoveFileNamesByRemovingTabs();
                        myTabControl_SelectedIndexChanged(sender, e);

                        frm.Close();
                    }
                }
            }


          else
            {
                if (OpenProjectFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Loading_Form frm = new Loading_Form();
                    frm.Show();


                    ProjectExplorerTreeView.Nodes.Clear();
                    myTabControl.TabPages.Clear();
                    this.Text = "Silver-J";

                    String projectfilename = OpenProjectFileDialog.FileName;

                    //read all files from selected project file .slvjproj
                    List<String> fileslist = new List<String> { };
                    List<String> otherfileslist = new List<String> { };
                    String projectname = "";
                    String projecttype = "";

                    if (File.Exists(projectfilename))
                    {
                        using (XmlReader reader = XmlReader.Create(projectfilename))
                        {
                            while (reader.Read())
                            {
                                if (reader.IsStartElement())
                                {
                                    switch (reader.Name.ToString())
                                    {
                                        case "ProjectName":
                                            projectname = reader.ReadString();
                                            break;

                                        case "ProjectType":
                                            projecttype = reader.ReadString();
                                            break;

                                        case "VisualFile":
                                            fileslist.Add(reader.ReadString());
                                            break;

                                        case "OtherFile":
                                            otherfileslist.Add(reader.ReadString());
                                            break;
                                    }
                                }
                            }
                        }


                        //add filename to defaultprojloc.slvjfile
                        String defaultprojfilepath = Application.StartupPath + "\\files\\defaultprojloc.slvjfile";

                        XmlDocument doc = new XmlDocument();
                        doc.Load(defaultprojfilepath);
                        doc.SelectSingleNode("SilverJ/CurrentProjectName").InnerText = projectname;
                        doc.SelectSingleNode("SilverJ/CurrentProjectFileName").InnerText = projectfilename;
                        doc.SelectSingleNode("SilverJ/CurrentProjectType").InnerText = projecttype;
                        doc.Save(defaultprojfilepath);


                        //add project name to form
                        this.Text = "Silver-J - [ " + projectname + " ]";

                        //add all files to myTabControl
                        foreach (String file in fileslist)
                        {
                            if (File.Exists(file))
                            {
                                String filename = file.Substring(file.LastIndexOf("\\") + 1);
                                String langs = "Java";
                                if (filename.Contains(".java"))
                                {
                                    langs = "Java";
                                }
                                else if (filename.Contains(".html"))
                                {
                                    langs = "HTML";
                                }
                                else if (filename.Contains(".css"))
                                {
                                    langs = "Java";
                                }
                                else if (filename.Contains(".js"))
                                {
                                    langs = "JavaScript";
                                }
                                else if (filename.Contains(".txt"))
                                {
                                    langs = "Text";
                                }
                                else if (filename.Contains(".sql"))
                                {
                                    langs = "SQL";
                                }
                                else if (filename.Contains(".xml"))
                                {
                                    langs = "XML";
                                }
                                else
                                {
                                    langs = "Text";
                                }

                                MyTabPage tabpage = new MyTabPage(this);
                                tabpage.AddLanguages(langs);

                                tabpage.textEditor.ContextMenuStrip = textEditorContextMenuStrip;

                                //read each file text and add it to textEditor
                                StreamReader filereader = new StreamReader(file);
                                tabpage.textEditor.Text = filereader.ReadToEnd();
                                filereader.Close();

                                //add tab pages with filename
                                tabpage.Text = filename;
                                myTabControl.TabPages.Add(tabpage);
                                myTabControl.SelectedTab = tabpage;

                                //add other selected files to ProjectExplorerTreeView
                                foreach (String file2 in otherfileslist)
                                {
                                    if (file2 != "")
                                    {
                                        String filename2 = file2.Substring(file2.LastIndexOf("\\") + 1);
                                        TreeNode trnode = new TreeNode();
                                        trnode.Text = filename2;
                                        trnode.ImageIndex = 8;
                                        trnode.SelectedImageIndex = 8;
                                        trnode.BackColor = Color.FromArgb(255, 255, 210);
                                        ProjectExplorerTreeView.Nodes.Add(trnode);
                                    }
                                }
                            }
                        }
                    }
                    AddFilesToProjectExplorerTreeViewFromOpenedProject(projectfilename);
                    WriteCurrentFileNames();
                    SetVisibilityOfToolStripButtons();
                    CopyAllSourceFilesToSRCFolder();
                    UpdateWindowsList_WindowMenu();
                    RemoveFileNamesByRemovingTabs();
                    myTabControl_SelectedIndexChanged(sender, e);

                    frm.Close();
                }
            }
        }


        /// <summary>
        /// when project is opened then adding all files with images to project exp tree view
        /// </summary>
        /// <param name="projfilename">current opened project file</param>
        public void AddFilesToProjectExplorerTreeViewFromOpenedProject(String projfilename)
        {
            String projectfile = ReadCurrentProjectFileName();
            String prjname = "";
            List<String> mylist = new List<String> { };
            List<String> packagejavafilelist = new List<String> { };
            List<String> otherfileslist = new List<String> { };
            String packagename = "";
            packagelist.Clear();
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
                                case "ProjectName":
                                    prjname = reader.ReadString();
                                    break;

                                case "JavaClassFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "HTMLFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "CSSFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "TextFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "JavaScriptFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "SQLFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "XMLFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "NewFile":
                                    mylist.Add(reader.ReadString());
                                    break;

                                case "PackageName":
                                    packagename = reader.ReadString();
                                    break;

                                case "PackageFolderName":
                                    packagelist.Add(reader.ReadString());
                                    break;

                                case "PackageJavaClassFile":
                                    packagejavafilelist.Add(reader.ReadString());
                                    break;

                                case "OtherFile":
                                    otherfileslist.Add(reader.ReadString());
                                    break;
                            }
                        }
                    }
                }
                if (prjname != "")
                {
                    ProjectExplorerTreeView.Nodes.Clear();
                    TreeNode prjnode = new TreeNode();
                    prjnode.Text = prjname;
                    prjnode.ImageIndex = 9;
                    prjnode.SelectedImageIndex = 9;
                    ProjectExplorerTreeView.Nodes.Add(prjnode);

                    foreach (String line in mylist)
                    {
                        if (line != "")
                        {
                            String strline = line.Substring(line.LastIndexOf("\\") + 1);
                            int imgindex = 0;
                            if (strline.Contains(".java"))
                            {
                                imgindex = 5;
                            }
                            else if (strline.Contains(".html"))
                            {
                                imgindex = 2;
                            }
                            else if (strline.Contains(".css"))
                            {
                                imgindex = 1;
                            }
                            else if (strline.Contains(".js"))
                            {
                                imgindex = 6;
                            }
                            else if (strline.Contains(".txt"))
                            {
                                imgindex = 11;
                            }
                            else if (strline.Contains(".sql"))
                            {
                                imgindex = 10;
                            }
                            else if (strline.Contains(".xml"))
                            {
                                imgindex = 12;
                            }
                            else if (strline.Contains(".class"))
                            {
                                imgindex = 0;
                            }
                            else if (strline.Contains(".jar"))
                            {
                                imgindex = 4;
                            }
                            else if (strline.Contains(".c") || strline.Contains(".cpp") || strline.Contains(".c++"))
                            {
                                imgindex = 13;
                            }
                            else if (strline.Contains(".h"))
                            {
                                imgindex = 14;
                            }
                            else if (strline.Contains(".bmp") || strline.Contains(".dib") || strline.Contains(".jpg") || strline.Contains(".jpeg") || strline.Contains(".gif") || strline.Contains(".tif") || strline.Contains(".tiff") || strline.Contains(".png"))
                            {
                                imgindex = 3;
                            }
                            else
                            {
                                imgindex = 7;
                            }
                            TreeNode prjnode2 = ProjectExplorerTreeView.Nodes[0];
                            TreeNode trnode = new TreeNode();
                            trnode.Text = strline;
                            trnode.ImageIndex = imgindex;
                            trnode.SelectedImageIndex = imgindex;
                            prjnode2.Nodes.Add(trnode);
                            ProjectExplorerTreeView.ExpandAll();
                        }
                    }
                    //add all package folder files to ProjectExplorerTreeView
                    foreach (String packagefolderpath in packagelist)
                    {
                        foreach (String packfile in packagejavafilelist)
                        {
                            String strline = packfile.Substring(packfile.LastIndexOf("\\") + 1);

                            if (File.Exists(packagefolderpath + "\\" + strline))
                            {
                                if (packfile != "")
                                {
                                    int imgindex = 0;
                                    //String packfolder = packagefolderpath.Substring(packagefolderpath.LastIndexOf("\\") + 1);
                                    String packfolder = packagename.Replace("\\", ".");
                                    strline = "[Package File-(" + packfolder + ")] - " + strline;
                                    if (strline.Contains(".java"))
                                    {
                                        imgindex = 8;
                                    }
                                    TreeNode prjnode2 = ProjectExplorerTreeView.Nodes[0];
                                    TreeNode trnode = new TreeNode();
                                    trnode.Text = strline;
                                    trnode.ImageIndex = imgindex;
                                    trnode.SelectedImageIndex = imgindex;
                                    prjnode2.Nodes.Add(trnode);
                                    ProjectExplorerTreeView.ExpandAll();
                                }
                            }
                        }
                    }
                    //add other selected files to ProjectExplorerTreeView
                    foreach (String file in otherfileslist)
                    {
                        if (file != "")
                        {
                            String filename = file.Substring(file.LastIndexOf("\\") + 1);
                            TreeNode trnode = new TreeNode();
                            trnode.Text = filename;
                            int imgindex2 = 0;
                            if (filename.Contains(".java"))
                            {
                                imgindex2 = 5;
                            }
                            else if (filename.Contains(".html"))
                            {
                                imgindex2 = 2;
                            }
                            else if (filename.Contains(".css"))
                            {
                                imgindex2 = 1;
                            }
                            else if (filename.Contains(".js"))
                            {
                                imgindex2 = 6;
                            }
                            else if (filename.Contains(".txt"))
                            {
                                imgindex2 = 11;
                            }
                            else if (filename.Contains(".sql"))
                            {
                                imgindex2 = 10;
                            }
                            else if (filename.Contains(".xml"))
                            {
                                imgindex2 = 12;
                            }
                            else if (filename.Contains(".class"))
                            {
                                imgindex2 = 0;
                            }
                            else if (filename.Contains(".jar"))
                            {
                                imgindex2 = 4;
                            }
                            else if (filename.Contains(".bmp") || filename.Contains(".dib") || filename.Contains(".jpg") || filename.Contains(".jpeg") || filename.Contains(".gif") || filename.Contains(".tif") || filename.Contains(".tiff") || filename.Contains(".png"))
                            {
                                imgindex2 = 3;
                            }
                            else
                            {
                                imgindex2 = 7;
                            }
                            trnode.ImageIndex = imgindex2;
                            trnode.SelectedImageIndex = imgindex2;
                            ProjectExplorerTreeView.Nodes.Add(trnode);
                        }
                    }
                }
            }
        }



        //**************************************************************************************************************
        //      File -> Open Files
        //**************************************************************************************************************
        /// <summary>
        /// opens files which are not part of current project but want to open and edit
        /// like simple notepad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_OpenFilesMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenFilesFileDialog.ShowDialog() == DialogResult.OK)
            {
                // myTabControl.TabPages.Clear();
                String[] files = OpenFilesFileDialog.FileNames;
                foreach (String file in files)
                {
                    MyTabPage mytabpage = new MyTabPage(this);
                    mytabpage.textEditor.ContextMenuStrip = textEditorContextMenuStrip;

                    String filestr = file.Substring(file.LastIndexOf("\\") + 1);

                    if (Path.GetExtension(file) == ".java")
                    {
                        mytabpage.AddLanguages("Java");
                    }
                    if (Path.GetExtension(file) == ".html")
                    {
                        mytabpage.AddLanguages("HTML");
                    }
                    if (Path.GetExtension(file) == ".txt")
                    {
                        mytabpage.AddLanguages("Text");
                    }
                    if (Path.GetExtension(file) == ".C" || Path.GetExtension(file) == ".CPP" || Path.GetExtension(file) == ".c++")
                    {
                        mytabpage.AddLanguages("C/C++");
                    }
                    if (Path.GetExtension(file) == ".js")
                    {
                        mytabpage.AddLanguages("JavaScript");
                    }
                    if (Path.GetExtension(file) == ".xml")
                    {
                        mytabpage.AddLanguages("XML");
                    }
                    if (Path.GetExtension(file) == ".vb")
                    {
                        mytabpage.AddLanguages("VB");
                    }
                    if (Path.GetExtension(file) == ".cs")
                    {
                        mytabpage.AddLanguages("C#");
                    }

                    int imgindex2 = 0;
                    String filename = filestr;
                    if (filename.Contains(".java"))
                    {
                        imgindex2 = 5;
                    }
                    else if (filename.Contains(".html"))
                    {
                        imgindex2 = 2;
                    }
                    else if (filename.Contains(".css"))
                    {
                        imgindex2 = 1;
                    }
                    else if (filename.Contains(".js"))
                    {
                        imgindex2 = 6;
                    }
                    else if (filename.Contains(".txt"))
                    {
                        imgindex2 = 11;
                    }
                    else if (filename.Contains(".sql"))
                    {
                        imgindex2 = 10;
                    }
                    else if (filename.Contains(".xml"))
                    {
                        imgindex2 = 12;
                    }
                    else if (filename.Contains(".class"))
                    {
                        imgindex2 = 0;
                    }
                    else if (filename.Contains(".jar"))
                    {
                        imgindex2 = 4;
                    }
                    else if (filename.Contains(".bmp") || filename.Contains(".dib") || filename.Contains(".jpg") || filename.Contains(".jpeg") || filename.Contains(".gif") || filename.Contains(".tif") || filename.Contains(".tiff") || filename.Contains(".png"))
                    {
                        imgindex2 = 3;
                    }
                    else
                    {
                        imgindex2 = 7;
                    }


                    //read a file
                    try
                    {
                        StreamReader strreader = new StreamReader(file);
                        mytabpage.textEditor.Text = strreader.ReadToEnd();
                        strreader.Close();
                    }
                    catch
                    { }

                    //add tab page to mytabcontrol & ProjectExplorer tree view
                    mytabpage.Text = filestr;
                    myTabControl.TabPages.Add(mytabpage);
                    myTabControl.SelectedTab = mytabpage;

                    TreeNode trnode = new TreeNode();
                    trnode.Text = filestr;
                    trnode.ImageIndex = imgindex2;
                    trnode.SelectedImageIndex = imgindex2;
                    ProjectExplorerTreeView.Nodes.Add(trnode);
                    ProjectExplorerTreeView.SelectedNode = trnode;

                    FilenameToolStripLabel.Text = file;

                    openfileslist.Add(file);

                    SetVisibilityOfToolStripButtons();
                    myTabControl_SelectedIndexChanged(sender, e);

                    UpdateWindowsList_WindowMenu();

                    //add all file names to files.slvjfile
                    String filesexjfile = Application.StartupPath + "\\files\\files.slvjfile";
                    RichTextBox rtb = new RichTextBox();
                    rtb.Text = File.ReadAllText(filesexjfile);
                    rtb.Text = rtb.Text.Insert(rtb.SelectionStart, file + "\n");
                    StreamWriter strw = new StreamWriter(filesexjfile);
                    strw.Write(rtb.Text);
                    strw.Close();
                    strw.Dispose();
                }
            }
        }


        //**************************************************************************************************************
        //      File->Load Sample Project
        //**************************************************************************************************************
        /// <summary>
        /// show Load Sample Project dialog
        /// once project is selected,create a project file with folders and open that project
        /// by calling function LoadProject_byClickingOnRecentProject
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_LoadSampleProjectMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Text != "Silver-J")
            {
                File_CloseProjectMenuItem_Click(sender, e);
            }
            else
            {
                SampleProject_Form sampleproject = new SampleProject_Form();
                sampleproject.ShowDialog();

                if (sampleproject.isProjectCreated())
                {
                    if (sampleproject.getProjectFile() != "")
                    {
                        this.LoadProject_byClickingOnRecentProject(sampleproject.getProjectFile());
                    }
                }
            }


        }



        //****************************************************************
        //   OpenProject()
        //*****************************************************************
        /// <summary>
        /// function to open project in Silver-J when .slvjproj file is associated with silver-j
        /// means to open project by double clicking on project file
        /// see Program.cs file
        /// </summary>
        /// <param name="projectfilename"></param>
        public void OpenProject(String projectfilename)
        {
            Loading_Form frm = new Loading_Form();
            frm.Show();


            ProjectExplorerTreeView.Nodes.Clear();
            myTabControl.TabPages.Clear();
            this.Text = "Silver-J";

            //read all files from selected project file .slvjproj
            List<String> fileslist = new List<String> { };
            List<String> otherfileslist = new List<String> { };
            String projectname = "";
            String projecttype = "";

            if (File.Exists(projectfilename))
            {
                using (XmlReader reader = XmlReader.Create(projectfilename))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name.ToString())
                            {
                                case "ProjectName":
                                    projectname = reader.ReadString();
                                    break;

                                case "ProjectType":
                                    projecttype = reader.ReadString();
                                    break;

                                case "VisualFile":
                                    fileslist.Add(reader.ReadString());
                                    break;

                                case "OtherFile":
                                    otherfileslist.Add(reader.ReadString());
                                    break;
                            }
                        }
                    }
                }


                //add filename to defaultprojloc.slvjfile
                String defaultprojfilepath = Application.StartupPath + "\\files\\defaultprojloc.slvjfile";

                XmlDocument doc = new XmlDocument();
                doc.Load(defaultprojfilepath);
                doc.SelectSingleNode("SilverJ/CurrentProjectName").InnerText = projectname;
                doc.SelectSingleNode("SilverJ/CurrentProjectFileName").InnerText = projectfilename;
                doc.SelectSingleNode("SilverJ/CurrentProjectType").InnerText = projecttype;
                doc.Save(defaultprojfilepath);


                //add project name to form
                this.Text = "Silver-J - [ " + projectname + " ]";

                if (fileslist.Count == 1)
                {
                    String file = fileslist[0].ToString();
                    if (File.Exists(file))
                    {
                        String filename = file.Substring(file.LastIndexOf("\\") + 1);
                        String langs = "Java";
                        if (filename.Contains(".java"))
                        {
                            langs = "Java";
                        }
                        else if (filename.Contains(".html"))
                        {
                            langs = "HTML";
                        }
                        else if (filename.Contains(".css"))
                        {
                            langs = "Java";
                        }
                        else if (filename.Contains(".js"))
                        {
                            langs = "JavaScript";
                        }
                        else if (filename.Contains(".txt"))
                        {
                            langs = "Text";
                        }
                        else if (filename.Contains(".sql"))
                        {
                            langs = "SQL";
                        }
                        else if (filename.Contains(".xml"))
                        {
                            langs = "XML";
                        }
                        else
                        {
                            langs = "Text";
                        }

                        MyTabPage tabpage = new MyTabPage(this);
                        tabpage.AddLanguages(langs);

                        tabpage.textEditor.ContextMenuStrip = textEditorContextMenuStrip;

                        //read each file text and add it to textEditor
                        StreamReader filereader = new StreamReader(file);
                        tabpage.textEditor.Text = filereader.ReadToEnd();
                        filereader.Close();

                        //add tab pages with filename
                        tabpage.Text = filename;
                        myTabControl.TabPages.Add(tabpage);
                        myTabControl.SelectedTab = tabpage;

                        FilenameToolStripLabel.Text = file;

                        //add other selected files to ProjectExplorerTreeView
                        foreach (String file2 in otherfileslist)
                        {
                            if (file2 != "")
                            {
                                String filename2 = file2.Substring(file2.LastIndexOf("\\") + 1);
                                TreeNode trnode = new TreeNode();
                                trnode.Text = filename2;
                                trnode.ImageIndex = 8;
                                trnode.SelectedImageIndex = 8;
                                trnode.BackColor = Color.FromArgb(255, 255, 210);
                                ProjectExplorerTreeView.Nodes.Add(trnode);
                            }
                        }
                    }
                }
                else
                {
                    //add all files to myTabControl
                    foreach (String file in fileslist)
                    {
                        if (File.Exists(file))
                        {
                            String filename = file.Substring(file.LastIndexOf("\\") + 1);
                            String langs = "Java";
                            if (filename.Contains(".java"))
                            {
                                langs = "Java";
                            }
                            else if (filename.Contains(".html"))
                            {
                                langs = "HTML";
                            }
                            else if (filename.Contains(".css"))
                            {
                                langs = "Java";
                            }
                            else if (filename.Contains(".js"))
                            {
                                langs = "JavaScript";
                            }
                            else if (filename.Contains(".txt"))
                            {
                                langs = "Text";
                            }
                            else if (filename.Contains(".sql"))
                            {
                                langs = "SQL";
                            }
                            else if (filename.Contains(".xml"))
                            {
                                langs = "XML";
                            }
                            else
                            {
                                langs = "Text";
                            }

                            MyTabPage tabpage = new MyTabPage(this);
                            tabpage.AddLanguages(langs);

                            tabpage.textEditor.ContextMenuStrip = textEditorContextMenuStrip;

                            //read each file text and add it to textEditor
                            StreamReader filereader = new StreamReader(file);
                            tabpage.textEditor.Text = filereader.ReadToEnd();
                            filereader.Close();

                            //add tab pages with filename
                            tabpage.Text = filename;
                            myTabControl.TabPages.Add(tabpage);
                            myTabControl.SelectedTab = tabpage;

                            FilenameToolStripLabel.Text = file;

                            //add other selected files to ProjectExplorerTreeView
                            foreach (String file2 in otherfileslist)
                            {
                                if (file2 != "")
                                {
                                    String filename2 = file2.Substring(file2.LastIndexOf("\\") + 1);
                                    TreeNode trnode = new TreeNode();
                                    trnode.Text = filename2;
                                    trnode.ImageIndex = 8;
                                    trnode.SelectedImageIndex = 8;
                                    trnode.BackColor = Color.FromArgb(255, 255, 210);
                                    ProjectExplorerTreeView.Nodes.Add(trnode);
                                }
                            }
                        }
                    }
                }
            }
            AddFilesToProjectExplorerTreeViewFromOpenedProject(projectfilename);
            WriteCurrentFileNames();
            SetVisibilityOfToolStripButtons();
            CopyAllSourceFilesToSRCFolder();
            UpdateWindowsList_WindowMenu();
            RemoveFileNamesByRemovingTabs();

            frm.Close();
        }

        //*******************************************************************
        //  OpenFiles_FromCMD()
        //********************************************************************
        /// <summary>
        /// function to open files when those files are associated with silver j
        /// see Program.cs file
        /// </summary>
        /// <param name="files"></param>
        public void OpenFiles_FromCMD(String[] files)
        {
            foreach (String file in files)
            {
                MyTabPage mytabpage = new MyTabPage(this);
                mytabpage.textEditor.ContextMenuStrip = textEditorContextMenuStrip;

                String filestr = file.Substring(file.LastIndexOf("\\") + 1);

                if (Path.GetExtension(file) == ".java")
                {
                    mytabpage.AddLanguages("Java");
                }
                if (Path.GetExtension(file) == ".html")
                {
                    mytabpage.AddLanguages("HTML");
                }
                if (Path.GetExtension(file) == ".txt")
                {
                    mytabpage.AddLanguages("Text");
                }
                if (Path.GetExtension(file) == ".C" || Path.GetExtension(file) == ".CPP" || Path.GetExtension(file) == ".c++")
                {
                    mytabpage.AddLanguages("C/C++");
                }
                if (Path.GetExtension(file) == ".js")
                {
                    mytabpage.AddLanguages("JavaScript");
                }
                if (Path.GetExtension(file) == ".xml")
                {
                    mytabpage.AddLanguages("XML");
                }
                if (Path.GetExtension(file) == ".vb")
                {
                    mytabpage.AddLanguages("VB");
                }
                if (Path.GetExtension(file) == ".cs")
                {
                    mytabpage.AddLanguages("C#");
                }

                int imgindex2 = 0;
                String filename = filestr;
                if (filename.Contains(".java"))
                {
                    imgindex2 = 5;
                }
                else if (filename.Contains(".html"))
                {
                    imgindex2 = 2;
                }
                else if (filename.Contains(".css"))
                {
                    imgindex2 = 1;
                }
                else if (filename.Contains(".js"))
                {
                    imgindex2 = 6;
                }
                else if (filename.Contains(".txt"))
                {
                    imgindex2 = 11;
                }
                else if (filename.Contains(".sql"))
                {
                    imgindex2 = 10;
                }
                else if (filename.Contains(".xml"))
                {
                    imgindex2 = 12;
                }
                else if (filename.Contains(".class"))
                {
                    imgindex2 = 0;
                }
                else if (filename.Contains(".jar"))
                {
                    imgindex2 = 4;
                }
                else if (filename.Contains(".bmp") || filename.Contains(".dib") || filename.Contains(".jpg") || filename.Contains(".jpeg") || filename.Contains(".gif") || filename.Contains(".tif") || filename.Contains(".tiff") || filename.Contains(".png"))
                {
                    imgindex2 = 3;
                }
                else
                {
                    imgindex2 = 7;
                }


                //read a file
                try
                {
                    StreamReader strreader = new StreamReader(file);
                    mytabpage.textEditor.Text = strreader.ReadToEnd();
                    strreader.Close();
                }
                catch
                { }

                //add tab page to mytabcontrol & ProjectExplorer tree view
                mytabpage.Text = filestr;
                myTabControl.TabPages.Add(mytabpage);
                myTabControl.SelectedTab = mytabpage;

                TreeNode trnode = new TreeNode();
                trnode.Text = filestr;
                trnode.ImageIndex = imgindex2;
                trnode.SelectedImageIndex = imgindex2;
                ProjectExplorerTreeView.Nodes.Add(trnode);
                ProjectExplorerTreeView.SelectedNode = trnode;

                FilenameToolStripLabel.Text = file;
                

                openfileslist.Add(file);

                SetVisibilityOfToolStripButtons();

                UpdateWindowsList_WindowMenu();

                //add all file names to files.slvjfile
                String filesexjfile = Application.StartupPath + "\\files\\files.slvjfile";
                RichTextBox rtb = new RichTextBox();
                rtb.Text = File.ReadAllText(filesexjfile);
                rtb.Text = rtb.Text.Insert(rtb.SelectionStart, file + "\n");
                StreamWriter strw = new StreamWriter(filesexjfile);
                strw.Write(rtb.Text);
                strw.Close();
                strw.Dispose();
            }
        }


        //**************************************************************************************************************
        //      myTabControl Selected Index Changed
        //**************************************************************************************************************
        /// <summary>
        /// when tab is changed change the filenametoolstrip label with tab file name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                TabPage tabpage = myTabControl.SelectedTab;
                if (tabpage.Text != "Start Page")
                {
                    String defaultfiles = Application.StartupPath + "\\files\\files.slvjfile";
                    String[] files = File.ReadAllLines(defaultfiles);
                    MyTabPage mytabpage = new MyTabPage(this);
                    mytabpage.IsMouseClickOnTextEditor = true;

                    foreach (String prjfile in files)
                    {
                        openfileslist.Add(prjfile);
                    }

                    foreach (String filename in openfileslist)
                    {
                        if (tabpage != null)
                        {
                            String str = filename.Substring(filename.LastIndexOf("\\") + 1);
                            if (tabpage.Text.Contains("*"))
                            {
                                String str2 = tabpage.Text.Remove(tabpage.Text.Length - 1);
                                if (str == str2)
                                {
                                    FilenameToolStripLabel.Text = filename;
                                }
                            }

                            else
                            {
                                if (str == tabpage.Text)
                                {
                                    FilenameToolStripLabel.Text = filename;
                                }
                            }
                        }
                    }

                    if (tabpage.Text != "Start Page")
                    {
                        int select_index = myTabControl.SelectedIndex;
                        var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                        //texteditor.ContextMenuStrip = textEditorContextMenuStrip;
                    }
                }
                else
                {
                    FilenameToolStripLabel.Text = "Silver-J";
                }
                UpdateWindowsList_WindowMenu();
                SetVisibilityOfToolStripButtons();
            }
            else
            {
                FilenameToolStripLabel.Text = "Silver-J";
                UpdateWindowsList_WindowMenu();
                WriteCurrentFileNames();
                SetVisibilityOfToolStripButtons();
            }
        }


        //**************************************************************************************************************
        //      File->Save
        //**************************************************************************************************************
        /// <summary>
        /// save current opend file by tab and remove * from it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void File_SaveMenuItem_Click(object sender, EventArgs e)
        {
            if(myTabControl.TabCount>0 || myTabControl.SelectedTab.Text!="Start Page")
            {
                if (FilenameToolStripLabel.Text.Contains("\\"))
                {
                    String s = FilenameToolStripLabel.Text;
                    String s2 = s.Substring(s.LastIndexOf("\\") + 1);
                    String s3 = "";

                    TabPage tabpage = myTabControl.SelectedTab;

                     if(tabpage.Text.Contains("*"))
                     {
                         s3=tabpage.Text.Remove(tabpage.Text.Length - 1);
                     }

                     if (s2 == s3)
                     {
                         if (tabpage.Text.Contains("*"))
                         {
                             String filename = FilenameToolStripLabel.Text;
                             if (File.Exists(filename))
                             {
                                 var texteditor = (TextEditorControl)myTabControl.TabPages[myTabControl.SelectedIndex].Controls[0];
                                 RichTextBox rtb = new RichTextBox();
                                 rtb.Text = texteditor.Text;
                                 File.WriteAllText(filename, "");
                                 StreamWriter strwriter = System.IO.File.AppendText(filename);
                                 strwriter.Write(rtb.Text);
                                 strwriter.Close();
                                 strwriter.Dispose();
                                 tabpage.Text = tabpage.Text.Remove(tabpage.Text.Length - 1);
                             }
                         }
                     }
                }
                CopyAllSourceFilesToSRCFolder();
                UpdateWindowsList_WindowMenu();
            }
        }


        //**************************************************************************************************************
        //      File->Save As
        //**************************************************************************************************************
        private void File_SaveAsMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (myTabControl.TabCount > 0 || myTabControl.SelectedTab.Text != "Start Page")
                {
                    if (FilenameToolStripLabel.Text.Contains("\\"))
                    {
                        TabPage tabpage = myTabControl.SelectedTab;

                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            String filename = saveFileDialog1.FileName;
                            var texteditor = (TextEditorControl)myTabControl.TabPages[myTabControl.SelectedIndex].Controls[0];
                            RichTextBox rtb = new RichTextBox();
                            rtb.Text = texteditor.Text;
                            File.WriteAllText(filename, "");
                            StreamWriter strwriter = System.IO.File.AppendText(filename);
                            strwriter.Write(rtb.Text);
                            strwriter.Close();
                            strwriter.Dispose();
                            tabpage.Text = tabpage.Text.Remove(tabpage.Text.Length - 1);

                            String fname = filename.Substring(filename.LastIndexOf("\\") + 1);
                            MessageBox.Show("File " + fname + " Saved");
                        }
                    }
                    CopyAllSourceFilesToSRCFolder();
                    UpdateWindowsList_WindowMenu();
                }
            }
            catch { }
        }


        //**************************************************************************************************************
        //      File->Save All
        //**************************************************************************************************************
        /// <summary>
        /// reading tabs from tabcontrol as filenames
        /// reading full file path names from \files\files.slvjfile
        /// if tab text == filename(read from files.slvjfile) then save that file & remove * from it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_SaveAllMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                String defaultfiles = Application.StartupPath + "\\files\\files.slvjfile";
                String[] files = File.ReadAllLines(defaultfiles);
                int i = myTabControl.TabPages.Count - 1;
                do
                {
                    foreach (String file in files)
                    {
                        if (File.Exists(file))
                        {
                            try
                            {
                                if (myTabControl.TabPages[i].Text != "Start Page")
                                {

                                    var texteditor = (TextEditorControl)myTabControl.TabPages[i].Controls[0];
                                    File.WriteAllText(file, "");
                                    StreamWriter strwriter = System.IO.File.AppendText(file);
                                    strwriter.Write(texteditor.Text);
                                    strwriter.Close();
                                    strwriter.Dispose();
                                    i--;
                                }
                            }
                            catch { }
                        }
                    }
                }
                while (i > 0);

                System.Windows.Forms.TabControl.TabPageCollection tabcollection = myTabControl.TabPages;
                foreach (TabPage tabpage in tabcollection)
                {
                    String str = tabpage.Text;
                    if (str.Contains("*"))
                    {
                        str = str.Remove(str.Length - 1);
                    }
                    tabpage.Text = str;
                }
            }
            CopyAllSourceFilesToSRCFolder();
            UpdateWindowsList_WindowMenu();
        }


        //**************************************************************************************************************
        //      File->Close
        //**************************************************************************************************************
        /// <summary>
        /// removes a tab by showing save or not dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_CloseMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                TabPage tabpage = myTabControl.SelectedTab;
                if (tabpage.Text.Contains("*"))
                {
                    DialogResult dg = MessageBox.Show("Do you want to save " + tabpage.Text + " file before close ?", "Save before Close ?", MessageBoxButtons.YesNoCancel);
                    if (dg == DialogResult.Yes)
                    {
                        //save file before close
                        File_SaveMenuItem_Click(sender, e);
                        //remove file name from project file
                        RemoveVisualFilesTextFromProjectFile(FilenameToolStripLabel.Text);
                        //remove tab
                        myTabControl.TabPages.Remove(tabpage);

                        RemoveFileNamesByRemovingTabs();
                        UpdateWindowsList_WindowMenu();
                        SetVisibilityOfToolStripButtons();
                        myTabControl_SelectedIndexChanged(sender, e);

                        LineToolStripLabel.Text = "Line";
                        ColumnToolStripLabel.Text = "Col";
                        ClassesTreeView.Nodes.Clear();
                        MethodsTreeView.Nodes.Clear();

                        if (myTabControl.TabCount == 0)
                        {
                            File.WriteAllText(Application.StartupPath + "\\files\\files.slvjfile", "");
                            FilenameToolStripLabel.Text = "Silver-J";
                        }
                    }
                    else
                    {
                        //remove file name from project file
                        RemoveVisualFilesTextFromProjectFile(FilenameToolStripLabel.Text);
                        //remove tab
                        myTabControl.TabPages.Remove(tabpage);

                        RemoveFileNamesByRemovingTabs();
                        UpdateWindowsList_WindowMenu();
                        SetVisibilityOfToolStripButtons();
                        myTabControl_SelectedIndexChanged(sender, e);

                        LineToolStripLabel.Text = "Line";
                        ColumnToolStripLabel.Text = "Col";
                        ClassesTreeView.Nodes.Clear();
                        MethodsTreeView.Nodes.Clear();

                        if (myTabControl.TabCount == 0)
                        {
                            File.WriteAllText(Application.StartupPath + "\\files\\files.slvjfile", "");
                            FilenameToolStripLabel.Text = "Silver-J";
                        }
                    }
                }
                else
                {
                    //remove file name from project file
                    RemoveVisualFilesTextFromProjectFile(FilenameToolStripLabel.Text);
                    //remove tab
                    myTabControl.TabPages.Remove(tabpage);

                    RemoveFileNamesByRemovingTabs();
                    UpdateWindowsList_WindowMenu();
                    SetVisibilityOfToolStripButtons();
                    myTabControl_SelectedIndexChanged(sender, e);

                    LineToolStripLabel.Text = "Line";
                    ColumnToolStripLabel.Text = "Col";
                    ClassesTreeView.Nodes.Clear();
                    MethodsTreeView.Nodes.Clear();

                    if (myTabControl.TabCount == 0)
                    {
                        File.WriteAllText(Application.StartupPath + "\\files\\files.slvjfile", "");
                        FilenameToolStripLabel.Text = "Silver-J";
                    }
                }
            }
            else
            {
                File.WriteAllText(Application.StartupPath + "\\files\\files.slvjfile", "");
                FilenameToolStripLabel.Text = "Silver-J";

                LineToolStripLabel.Text = "Line";
                ColumnToolStripLabel.Text = "Col";
                ClassesTreeView.Nodes.Clear();
                MethodsTreeView.Nodes.Clear();
                SetVisibilityOfToolStripButtons();
            }
        }


        //**************************************************************************************************************
        //      File->Close All
        //**************************************************************************************************************
        private void File_CloseAllMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                System.Windows.Forms.TabControl.TabPageCollection tabcoll = myTabControl.TabPages;
                foreach (TabPage tabpage in tabcoll)
                {
                    myTabControl.SelectedTab = tabpage;

                    if (tabpage.Text.Contains("*"))
                    {
                        DialogResult dg = MessageBox.Show("Do you want to save file  "+tabpage.Text+"  before close ?", "Save before Close ?", MessageBoxButtons.YesNo);
                        if (dg == DialogResult.Yes)
                        {
                            //save file
                            File_SaveMenuItem_Click(sender, e);
                            //remove tab
                            myTabControl.TabPages.Remove(tabpage);
                            RemoveVisualFilesTextFromProjectFile(FilenameToolStripLabel.Text);
                            RemoveFileNamesByRemovingTabs();
                            UpdateWindowsList_WindowMenu();
                            SetVisibilityOfToolStripButtons();
                            myTabControl_SelectedIndexChanged(sender, e);
                            File.WriteAllText(Application.StartupPath + "\\files\\files.slvjfile", "");
                            myTabControl_SelectedIndexChanged(sender, e);
                            LineToolStripLabel.Text = "Line";
                            ColumnToolStripLabel.Text = "Col";
                            ClassesTreeView.Nodes.Clear();
                            MethodsTreeView.Nodes.Clear();

                            String projectfile = ReadCurrentProjectFileName();
                            if (File.Exists(projectfile))
                            {
                                XmlDocument doc = new XmlDocument();
                                doc.Load(projectfile);
                                XmlNodeList nodes = doc.GetElementsByTagName("VisualFile");
                                for (int i = 0; i < nodes.Count; i++)
                                {
                                    XmlNode node = nodes[i];
                                    node.ParentNode.RemoveChild(node);
                                }
                                doc.Save(projectfile);
                            }
                        }
                        else
                        {
                            //remove tab
                            myTabControl.TabPages.Remove(tabpage);
                            RemoveVisualFilesTextFromProjectFile(FilenameToolStripLabel.Text);
                            RemoveFileNamesByRemovingTabs();
                            SetVisibilityOfToolStripButtons();
                            UpdateWindowsList_WindowMenu();
                            myTabControl_SelectedIndexChanged(sender, e);
                            File.WriteAllText(Application.StartupPath + "\\files\\files.slvjfile", "");
                            myTabControl_SelectedIndexChanged(sender, e);
                            LineToolStripLabel.Text = "Line";
                            ColumnToolStripLabel.Text = "Col";
                            ClassesTreeView.Nodes.Clear();
                            MethodsTreeView.Nodes.Clear();

                            String projectfile = ReadCurrentProjectFileName();
                            if (File.Exists(projectfile))
                            {
                                XmlDocument doc = new XmlDocument();
                                doc.Load(projectfile);
                                XmlNodeList nodes = doc.GetElementsByTagName("VisualFile");
                                for (int i = 0; i < nodes.Count; i++)
                                {
                                    XmlNode node = nodes[i];
                                    node.ParentNode.RemoveChild(node);
                                }
                                doc.Save(projectfile);
                            }
                        }
                    }
                    else
                    {
                        //remove tab
                        myTabControl.TabPages.Remove(tabpage);
                        RemoveVisualFilesTextFromProjectFile(FilenameToolStripLabel.Text);
                        RemoveFileNamesByRemovingTabs();
                        SetVisibilityOfToolStripButtons();
                        UpdateWindowsList_WindowMenu();
                        myTabControl_SelectedIndexChanged(sender, e);
                        File.WriteAllText(Application.StartupPath + "\\files\\files.slvjfile", "");
                        myTabControl_SelectedIndexChanged(sender, e);
                        LineToolStripLabel.Text = "Line";
                        ColumnToolStripLabel.Text = "Col";
                        ClassesTreeView.Nodes.Clear();
                        MethodsTreeView.Nodes.Clear();

                        String projectfile = ReadCurrentProjectFileName();
                        if (File.Exists(projectfile))
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.Load(projectfile);
                            XmlNodeList nodes = doc.GetElementsByTagName("VisualFile");
                            for (int i = 0; i < nodes.Count; i++)
                            {
                                XmlNode node = nodes[i];
                                node.ParentNode.RemoveChild(node);
                            }
                            doc.Save(projectfile);
                        }
                    }
                }
            }
            else
            {
                File.WriteAllText(Application.StartupPath + "\\files\\files.slvjfile", "");
                FilenameToolStripLabel.Text = "Silver-J";
                LineToolStripLabel.Text = "Line";
                ColumnToolStripLabel.Text = "Col";
                ClassesTreeView.Nodes.Clear();
                MethodsTreeView.Nodes.Clear();
                SetVisibilityOfToolStripButtons();

                String projectfile = ReadCurrentProjectFileName();
                if (File.Exists(projectfile))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(projectfile);
                    XmlNodeList nodes = doc.GetElementsByTagName("VisualFile");
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        XmlNode node = nodes[i];
                         node.ParentNode.RemoveChild(node);
                    }
                    doc.Save(projectfile);
                }
            }
        }


        //**************************************************************************************************************
        //      File->Close Project
        //**************************************************************************************************************
        public static Boolean isFilechanged = false;
        private void File_CloseProjectMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Text != "Silver-J")
            {
                if (myTabControl.TabCount > 0)
                {
                    System.Windows.Forms.TabControl.TabPageCollection tabcoll = myTabControl.TabPages;

                    foreach (TabPage tabpage in tabcoll)
                    {
                        if (tabcoll.Count > 1)
                        {
                            myTabControl.SelectedTab = tabpage;
                        }

                        if (tabpage.Text.Contains("*"))
                        {

                            String tabtext = tabpage.Text;

                            DialogResult dg = MessageBox.Show("Do you want to save modified file  " + tabtext + "  before close ?", "Save or Not", MessageBoxButtons.YesNo);
                            if (dg == DialogResult.Yes)
                            {
                                File_SaveMenuItem_Click(sender, e);
                                this.Text = "Silver-J";
                                myTabControl.TabPages.Remove(myTabControl.SelectedTab);
                                ProjectExplorerTreeView.Nodes.Clear();
                                SetVisibilityOfToolStripButtons();
                                UpdateWindowsList_WindowMenu();
                                myTabControl_SelectedIndexChanged(sender, e);
                                File.WriteAllText(Application.StartupPath + "\\files\\files.slvjfile", "");
                                LineToolStripLabel.Text = "Line";
                                ColumnToolStripLabel.Text = "Col";
                                ClassesTreeView.Nodes.Clear();
                                MethodsTreeView.Nodes.Clear();
                            }
                            else
                            {
                                this.Text = "Silver-J";
                                myTabControl.TabPages.Remove(myTabControl.SelectedTab);
                                ProjectExplorerTreeView.Nodes.Clear();
                                SetVisibilityOfToolStripButtons();
                                UpdateWindowsList_WindowMenu();
                                myTabControl_SelectedIndexChanged(sender, e);
                                File.WriteAllText(Application.StartupPath + "\\files\\files.slvjfile", "");
                                LineToolStripLabel.Text = "Line";
                                ColumnToolStripLabel.Text = "Col";
                                ClassesTreeView.Nodes.Clear();
                                MethodsTreeView.Nodes.Clear();
                            }
                        }
                        else
                        {
                            isFilechanged = false;
                            this.Text = "Silver-J";
                            myTabControl.TabPages.Remove(myTabControl.SelectedTab);
                            ProjectExplorerTreeView.Nodes.Clear();
                            SetVisibilityOfToolStripButtons();
                            UpdateWindowsList_WindowMenu();
                            myTabControl_SelectedIndexChanged(sender, e);
                            File.WriteAllText(Application.StartupPath + "\\files\\files.slvjfile", "");
                            LineToolStripLabel.Text = "Line";
                            ColumnToolStripLabel.Text = "Col";
                            ClassesTreeView.Nodes.Clear();
                            MethodsTreeView.Nodes.Clear();
                        }
                    }
                }
                else
                {
                    this.Text = "Silver-J";
                    isFilechanged = false;
                    ProjectExplorerTreeView.Nodes.Clear();
                    FilenameToolStripLabel.Text = "Silver-J";
                    File.WriteAllText(Application.StartupPath + "\\files\\files.slvjfile", "");
                    LineToolStripLabel.Text = "Line";
                    ColumnToolStripLabel.Text = "Col";
                    ClassesTreeView.Nodes.Clear();
                    MethodsTreeView.Nodes.Clear();
                    SetVisibilityOfToolStripButtons();
                }
            }
        }


        //**************************************************************************************************************
        //      File->Delete Project
        //**************************************************************************************************************
        private void File_DeleteProjectMenuItem_Click(object sender, EventArgs e)
        {
            String projectfolder = getCurrentProjectLocationFolder();
            File_CloseProjectMenuItem_Click(sender, e);
            UpdateWindowsList_WindowMenu();
            SetVisibilityOfToolStripButtons();

            if (Directory.Exists(projectfolder))
            {
                //delete all files from srcclasses folder
                String[] files = Directory.GetFiles(projectfolder + "\\srcclasses");
                foreach (String file in files)
                {
                    File.Delete(file);
                }
                //delete all classes files from classes folder
                String[] classesfiles = Directory.GetFiles(projectfolder + "\\classes");
                foreach (String clafile in classesfiles)
                {
                    File.Delete(clafile);
                }
                //delete all files from src folder
                if(Directory.Exists(getCurrentProjectLocationFolder()+"\\src"))
                {
                    String[] srcfiles = Directory.GetFiles(projectfolder + "\\src");
                    foreach (String srcfile in srcfiles)
                    {
                        File.Delete(srcfile);
                    }
                    Directory.Delete(getCurrentProjectLocationFolder() + "\\src");
                }

                //delete project file
                File.Delete(ReadCurrentProjectFileName());

                //delete srcclasses,classes,srcclasses and classes & project folder
                Directory.Delete(projectfolder + "\\srcclasses");
                Directory.Delete(projectfolder + "\\classes");
                Directory.Delete(projectfolder);

                File.WriteAllText(Application.StartupPath + "\\files\\files.exjfile", "");
            }
        }


        //**************************************************************************************************************
        //      File->Print
        //**************************************************************************************************************
        private void File_PrintMenuItem_Click(object sender, EventArgs e)
        {
            if(myTabControl.TabCount>0)
            {
                if(myTabControl.SelectedTab.Text!="Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    RichTextBox rtb = new RichTextBox();
                    rtb.Text = texteditor.Text;
                    rtb.Print();
                }
            }
        }


        //**************************************************************************************************************
        //      File->Print Preview
        //**************************************************************************************************************
        private void File_PrintPreviewMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    RichTextBox rtb = new RichTextBox();
                    rtb.Text = texteditor.Text;
                    printPreviewDialog1.Document = printDocument1;
                    printPreviewDialog1.ShowDialog();
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    e.Graphics.DrawString(texteditor.Text, texteditor.Font, Brushes.Black, e.MarginBounds.Left, 0, new StringFormat());
                    e.Graphics.PageUnit = GraphicsUnit.Inch;
                }
            }
        }



        public void AddRecentProjects()
        {
            String prj = ReadCurrentProjectFileName();
            if (prj == "")
            {
                File_RecentProject_NoRecentProjectMenuItem.Text = "NoRecentProject";
            }
            else
            {
                File_RecentProject_NoRecentProjectMenuItem.Text = prj;
            }
        }



        //**************************************************************************************************************
        //      File->Recent Project
        //**************************************************************************************************************
        /// <summary>
        /// open project when clicking on Recent project file name
        /// </summary>
        /// <param name="projfilename"></param>
        public void LoadProject_byClickingOnRecentProject(String projfilename)
        {
            Loading_Form frm = new Loading_Form();
            frm.Show();

            new MyTabPage(this).IsClassCreated = false;
            new MyTabPage(this).IsDataTypeDeclared = false;


            ProjectExplorerTreeView.Nodes.Clear();
            myTabControl.TabPages.Clear();
            this.Text = "Silver-J";

            String projectfilename = projfilename;

            //read all files from selected project file .slvjproj
            List<String> fileslist = new List<String> { };
            List<String> otherfileslist = new List<String> { };
            String projectname = "";
            String projecttype = "";

            if (File.Exists(projectfilename))
            {
                using (XmlReader reader = XmlReader.Create(projectfilename))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name.ToString())
                            {
                                case "ProjectName":
                                    projectname = reader.ReadString();
                                    break;

                                case "ProjectType":
                                    projecttype = reader.ReadString();
                                    break;

                                case "VisualFile":
                                    fileslist.Add(reader.ReadString());
                                    break;

                                case "OtherFile":
                                    otherfileslist.Add(reader.ReadString());
                                    break;
                            }
                        }
                    }
                }


                //add filename to defaultprojloc.slvjfile
                String defaultprojfilepath = Application.StartupPath + "\\files\\defaultprojloc.slvjfile";

                XmlDocument doc = new XmlDocument();
                doc.Load(defaultprojfilepath);
                doc.SelectSingleNode("SilverJ/CurrentProjectName").InnerText = projectname;
                doc.SelectSingleNode("SilverJ/CurrentProjectFileName").InnerText = projectfilename;
                doc.SelectSingleNode("SilverJ/CurrentProjectType").InnerText = projecttype;
                doc.Save(defaultprojfilepath);


                //add project name to form
                this.Text = "Silver-J - [ " + projectname + " ]";

                //add all files to myTabControl
                foreach (String file in fileslist)
                {
                    if (File.Exists(file))
                    {
                        String filename = file.Substring(file.LastIndexOf("\\") + 1);
                        String langs = "Java";
                        if (filename.Contains(".java"))
                        {
                            langs = "Java";
                        }
                        else if (filename.Contains(".html"))
                        {
                            langs = "HTML";
                        }
                        else if (filename.Contains(".css"))
                        {
                            langs = "Java";
                        }
                        else if (filename.Contains(".js"))
                        {
                            langs = "JavaScript";
                        }
                        else if (filename.Contains(".txt"))
                        {
                            langs = "Text";
                        }
                        else if (filename.Contains(".sql"))
                        {
                            langs = "SQL";
                        }
                        else if (filename.Contains(".xml"))
                        {
                            langs = "XML";
                        }
                        else
                        {
                            langs = "Text";
                        }

                        MyTabPage tabpage = new MyTabPage(this);
                        tabpage.AddLanguages(langs);

                        tabpage.textEditor.ContextMenuStrip = textEditorContextMenuStrip;

                        //read each file text and add it to textEditor
                        StreamReader filereader = new StreamReader(file);
                        tabpage.textEditor.Text = filereader.ReadToEnd();
                        filereader.Close();

                        //add tab pages with filename
                        tabpage.Text = filename;
                        myTabControl.TabPages.Add(tabpage);
                        myTabControl.SelectedTab = tabpage;

                        //add other selected files to ProjectExplorerTreeView
                        foreach (String file2 in otherfileslist)
                        {
                            if (file2 != "")
                            {
                                String filename2 = file2.Substring(file2.LastIndexOf("\\") + 1);
                                TreeNode trnode = new TreeNode();
                                trnode.Text = filename2;
                                trnode.ImageIndex = 8;
                                trnode.SelectedImageIndex = 8;
                                trnode.BackColor = Color.FromArgb(255, 255, 210);
                                ProjectExplorerTreeView.Nodes.Add(trnode);
                            }
                        }
                    }
                }
            }
            AddFilesToProjectExplorerTreeViewFromOpenedProject(projectfilename);
            WriteCurrentFileNames();
            CopyAllSourceFilesToSRCFolder();
            UpdateWindowsList_WindowMenu();
            RemoveFileNamesByRemovingTabs();

            frm.Close();
        }

        private void File_RecentProject_NoRecentProjectMenuItem_Click(object sender, EventArgs e)
        {
            if(File_RecentProject_NoRecentProjectMenuItem.Text!="NoRecentProject")
            {
                String project = File_RecentProject_NoRecentProjectMenuItem.Text;
                if (File.Exists(project))
                {
                    LoadProject_byClickingOnRecentProject(project);
                    myTabControl_SelectedIndexChanged(sender, e);
                }
            }
        }


        //**************************************************************************************************************
        //      File->Recent Project->Clear Recent Project
        //**************************************************************************************************************
        private void File_RecentProject_ClearRecentProjectMenuItem_Click(object sender, EventArgs e)
        {
            File_RecentProject_NoRecentProjectMenuItem.Text = "NoRecentProject";
        }


        //**************************************************************************************************************
        //      File->Exit
        //**************************************************************************************************************
        private void File_ExitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }




//***********************************************************************************************************************************
//                                           Edit
//***********************************************************************************************************************************



        //**************************************************************************************************************
        //      EditMenuItem Drop Down Opened
        //**************************************************************************************************************
        private void EditMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            if (myTabControl.TabCount !=0)
            {
                if(myTabControl.SelectedTab.Text=="Start Page")
                {
                    Edit_CutMenuItem.Enabled = false;
                    Edit_CopyMenuItem.Enabled = false;
                    Edit_PasteMenuItem.Enabled = false;
                    Edit_UndoMenuItem.Enabled = false;
                    Edit_RedoMenuItem.Enabled = false;
                    Edit_FindMenuItem.Enabled = false;
                    Edit_ReplaceMenuItem.Enabled = false;
                    Edit_GoToMenuItem.Enabled = false;
                    Edit_DeleteMenuItem.Enabled = false;
                    Edit_SelectAllMenuItem.Enabled = false;
                    Edit_ChangeCaseMenuItem.Enabled = false;
                    Edit_CommentLineMenuItem.Enabled = false;
                    Edit_InsertMenuItem.Enabled = false;
                }
                else
                {
                    Edit_CutMenuItem.Enabled = true;
                    Edit_CopyMenuItem.Enabled = true;
                    Edit_PasteMenuItem.Enabled = true;
                    Edit_UndoMenuItem.Enabled = true;
                    Edit_RedoMenuItem.Enabled = true;
                    Edit_FindMenuItem.Enabled = true;
                    Edit_ReplaceMenuItem.Enabled = true;
                    Edit_GoToMenuItem.Enabled = true;
                    Edit_DeleteMenuItem.Enabled = true;
                    Edit_SelectAllMenuItem.Enabled = true;
                    Edit_ChangeCaseMenuItem.Enabled = true;
                    Edit_CommentLineMenuItem.Enabled = true;
                    Edit_InsertMenuItem.Enabled = true;
                }
            }
            else
            {
                Edit_CutMenuItem.Enabled = false;
                Edit_CopyMenuItem.Enabled = false;
                Edit_PasteMenuItem.Enabled = false;
                Edit_UndoMenuItem.Enabled = false;
                Edit_RedoMenuItem.Enabled = false;
                Edit_FindMenuItem.Enabled = false;
                Edit_ReplaceMenuItem.Enabled = false;
                Edit_GoToMenuItem.Enabled = false;
                Edit_SelectAllMenuItem.Enabled = false;
                Edit_ChangeCaseMenuItem.Enabled = false;
                Edit_CommentLineMenuItem.Enabled = false;
                Edit_InsertMenuItem.Enabled = false;
            }

            if(myTabControl.TabCount>=2)
            {
                Edit_NextDocumentMenuItem.Enabled = true;
                Edit_PreviousDocumentMenuItem.Enabled = true;
            }
            else
            {
                Edit_NextDocumentMenuItem.Enabled = false;
                Edit_PreviousDocumentMenuItem.Enabled = false;
            }
        }


        //**************************************************************************************************************
        //      Edit->Cut
        //**************************************************************************************************************
        private void Edit_CutMenuItem_Click(object sender, EventArgs e)
        {
            if(myTabControl.TabCount>0)
            {
                if(myTabControl.SelectedTab.Text!="Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    texteditor.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(sender, e);
                }
            }
        }


        //**************************************************************************************************************
        //      Edit->Copy
        //**************************************************************************************************************
        private void Edit_CopyMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    texteditor.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(sender, e);
                }
            }
        }


        //**************************************************************************************************************
        //      Edit->Paste
        //**************************************************************************************************************
        private void Edit_PasteMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    texteditor.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(sender, e);
                }
            }
        }


        //**************************************************************************************************************
        //      Edit->Undo
        //**************************************************************************************************************
        private void Edit_UndoMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    texteditor.Undo();
                }
            }
        }


        //**************************************************************************************************************
        //      Edit->Redo
        //**************************************************************************************************************
        private void Edit_RedoMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    texteditor.Redo();
                }
            }
        }


        //**************************************************************************************************************
        //      Edit->Find
        //**************************************************************************************************************
        private void Edit_FindMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    Find_Form ff = new Find_Form(texteditor);
                    ff.ShowDialog();
                }
            }
        }


        //**************************************************************************************************************
        //      Edit->Replace
        //**************************************************************************************************************
        private void Edit_ReplaceMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    Replace_Form rpf = new Replace_Form(texteditor);
                    rpf.ShowDialog();
                }
            }
        }


        //**************************************************************************************************************
        //      Edit->GoTo
        //**************************************************************************************************************
        private void Edit_GoToMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    GoTo_Form gtf = new GoTo_Form(texteditor);
                    gtf.ShowDialog();
                }
            }
        }


        //**************************************************************************************************************
        //      Edit->Delete
        //**************************************************************************************************************
        private void Edit_DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    texteditor.ActiveTextAreaControl.TextArea.ClipboardHandler.Delete(sender, e);
                }
            }
        }

        //**************************************************************************************************************
        //      Edit->Select All
        //**************************************************************************************************************
        private void Edit_SelectAllMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    texteditor.ActiveTextAreaControl.TextArea.ClipboardHandler.SelectAll(sender, e);
                }
            }
        }


        //**************************************************************************************************************
        //      Edit->Change Case->Upper Case
        //**************************************************************************************************************
        private void Edit_ChangeCase_UpperCaseMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    RichTextBox rtb = new RichTextBox();
                    String s = texteditor.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
                    rtb.Text = s;
                    rtb.SelectAll();
                    rtb.SelectedText = rtb.SelectedText.ToUpper();
                    texteditor.ActiveTextAreaControl.TextArea.InsertString("");
                    texteditor.Document.Insert(texteditor.ActiveTextAreaControl.TextArea.Caret.Offset, rtb.Text);
                }
            }
        }

        //**************************************************************************************************************
        //      Edit->Change Case->Lower Case
        //**************************************************************************************************************
        private void Edit_ChangeCase_LowerCaseMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    RichTextBox rtb = new RichTextBox();
                    String s = texteditor.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
                    rtb.Text = s;
                    rtb.SelectAll();
                    rtb.SelectedText = rtb.SelectedText.ToLower();
                    texteditor.ActiveTextAreaControl.TextArea.InsertString("");
                    texteditor.Document.Insert(texteditor.ActiveTextAreaControl.TextArea.Caret.Offset, rtb.Text);
                }
            }
        }

        //**************************************************************************************************************
        //      Edit->Change Case->Sentence Case
        //**************************************************************************************************************
        private void Edit_ChangeCase_SentenceCaseMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    String s = texteditor.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
                    if (s != "")
                    {
                        String firstchar = s[0].ToString();
                        firstchar = firstchar.ToUpper();
                        String str = firstchar + s.Remove(0, 1);
                        str = firstchar + str.Substring(1);
                        texteditor.ActiveTextAreaControl.TextArea.InsertString("");
                        texteditor.Document.Insert(texteditor.ActiveTextAreaControl.TextArea.Caret.Offset, str);
                    }
                }
            }
        }


        //**************************************************************************************************************
        //      Edit->Comment Line->Single Line Comment
        //**************************************************************************************************************
        private void Edit_CommentLine_SingleLineMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    TabPage tabpage = myTabControl.SelectedTab;
                    if (tabpage.Text.Contains(".java") || tabpage.Text.Contains(".c") || tabpage.Text.Contains(".cpp") || tabpage.Text.Contains("cs") || tabpage.Text.Contains(".js") || tabpage.Text.Contains(".css") || tabpage.Text.Contains(".sql") || tabpage.Text.Contains(".xml"))
                    {
                        int sel = texteditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                        texteditor.Document.Insert(sel, "//");
                        texteditor.ActiveTextAreaControl.TextArea.Caret.Column = sel;
                    }
                    else if (tabpage.Text.Contains(".html"))
                    {
                        int sel = texteditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                        texteditor.Document.Insert(sel, "<!--                                -->");
                        texteditor.ActiveTextAreaControl.TextArea.Caret.Column = sel;
                    }
                }
            }
        }

        //**************************************************************************************************************
        //      Edit->Comment Line->Multi Line Comment
        //**************************************************************************************************************
        private void Edit_CommentLine_MultiLineMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    TabPage tabpage = myTabControl.SelectedTab;
                    if (tabpage.Text.Contains(".java") || tabpage.Text.Contains(".c") || tabpage.Text.Contains(".cpp") || tabpage.Text.Contains("cs") || tabpage.Text.Contains(".js") || tabpage.Text.Contains(".css") || tabpage.Text.Contains(".sql"))
                    {
                        int sel = texteditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                        texteditor.Document.Insert(sel, "/*       */");
                        texteditor.ActiveTextAreaControl.TextArea.Caret.Column = sel;
                    }
                    else if (tabpage.Text.Contains(".html")||tabpage.Text.Contains(".xml"))
                    {
                        int sel = texteditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                        texteditor.Document.Insert(sel, "<!--                           -->");
                        texteditor.ActiveTextAreaControl.TextArea.Caret.Column = sel;
                    }
                }
            }
        }

        //**************************************************************************************************************
        //      Edit->Comment Line->Selection Comment
        //**************************************************************************************************************
        private void Edit_CommentLine_SelectionCommentMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    TabPage tabpage = myTabControl.SelectedTab;
                    if (tabpage.Text.Contains(".java") || tabpage.Text.Contains(".c") || tabpage.Text.Contains(".cpp") || tabpage.Text.Contains("cs") || tabpage.Text.Contains(".js") || tabpage.Text.Contains(".css") || tabpage.Text.Contains(".sql"))
                    {
                        RichTextBox rtb = new RichTextBox();
                        String s = texteditor.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
                        rtb.Text = s;
                        rtb.Text = rtb.Text.Insert(0, "/* ");
                        rtb.Text = rtb.Text.Insert(rtb.Text.Length, " */");
                        texteditor.ActiveTextAreaControl.TextArea.InsertString("");
                        texteditor.Document.Insert(texteditor.ActiveTextAreaControl.TextArea.Caret.Offset, rtb.Text);
                    }
                    else if (tabpage.Text.Contains(".html") || tabpage.Text.Contains(".xml"))
                    {
                        RichTextBox rtb = new RichTextBox();
                        String s = texteditor.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
                        rtb.Text = s;
                        rtb.Text = rtb.Text.Insert(0, "<!-- ");
                        rtb.Text = rtb.Text.Insert(rtb.Text.Length, " -->");
                        texteditor.ActiveTextAreaControl.TextArea.InsertString("");
                        texteditor.Document.Insert(texteditor.ActiveTextAreaControl.TextArea.Caret.Offset, rtb.Text);
                    }
                    else
                    {
                        RichTextBox rtb = new RichTextBox();
                        String s = texteditor.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
                        rtb.Text = s;
                        rtb.Text = rtb.Text.Insert(0, "/* ");
                        rtb.Text = rtb.Text.Insert(rtb.Text.Length, " */");
                        texteditor.ActiveTextAreaControl.TextArea.InsertString("");
                        texteditor.Document.Insert(texteditor.ActiveTextAreaControl.TextArea.Caret.Offset, rtb.Text);
                    }
                }
            }
        }

        //**************************************************************************************************************
        //      Edit->Insert->main function
        //**************************************************************************************************************
        private void Edit_Insert_mainFunctionMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    texteditor.ActiveTextAreaControl.TextArea.InsertString(" public static void main(String[] args)  {\n}");
                }
            }
        }

        //**************************************************************************************************************
        //      Edit->Insert->Class
        //**************************************************************************************************************
        private void Edit_Insert_ClassMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    InsertClass_Form ncf = new InsertClass_Form(texteditor);
                    ncf.ShowDialog();
                }
            }
        }


        //**************************************************************************************************************
        //      Edit->Insert->Packages
        //**************************************************************************************************************
        private void Edit_Insert_PackagesMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    InsertPackages_Form ipf = new InsertPackages_Form(texteditor);
                    ipf.ShowDialog();
                }
            }
        }

        //**************************************************************************************************************
        //      Edit->Insert->Events
        //**************************************************************************************************************
        private void Edit_Insert_EventsMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    InsertEvents_Form insertevf = new InsertEvents_Form(texteditor);
                    insertevf.ShowDialog();
                }
            }
        }

        //**************************************************************************************************************
        //      Edit->Next Document
        //**************************************************************************************************************
        private void Edit_NextDocumentMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                int count = myTabControl.TabCount;
                if (myTabControl.SelectedIndex <= count)
                {
                    myTabControl.SelectedIndex = myTabControl.SelectedIndex + 1;
                }
                UpdateWindowsList_WindowMenu();
            }
        }

        //**************************************************************************************************************
        //      Edit->Previous Document
        //**************************************************************************************************************
        private void Edit_PreviousDocumentMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedIndex == 0)
                {
                }
                else
                {
                    myTabControl.SelectedIndex = myTabControl.SelectedIndex - 1;
                }
                UpdateWindowsList_WindowMenu();
            }
        }



//***********************************************************************************************************************************
//                                           View
//***********************************************************************************************************************************

        //**************************************************************************************************************
        //     View->Tabs Alignment->Top
        //**************************************************************************************************************
        private void View_TabsAlign_TopMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_TabsAlign_TopMenuItem.Checked == false)
            {
                View_TabsAlign_TopMenuItem.Checked = true;
                View_TabsAlign_BottomMenuItem.Checked = false;
                myTabControl.Alignment = TabAlignment.Top;
                //add top text to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/TabsAlignment").InnerText = "Top";
                doc.Save(configfile);
            }
        }

        //**************************************************************************************************************
        //     View->Tabs Alignment->Bottom
        //**************************************************************************************************************
        private void View_TabsAlign_BottomMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_TabsAlign_BottomMenuItem.Checked == false)
            {
                View_TabsAlign_BottomMenuItem.Checked = true;
                View_TabsAlign_TopMenuItem.Checked = false;
                myTabControl.Alignment = TabAlignment.Bottom;
                //add Bottom text to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/TabsAlignment").InnerText = "Bottom";
                doc.Save(configfile);
            }
        }

        //**************************************************************************************************************
        //     View->Status Strip
        //**************************************************************************************************************
        private void View_StatusStripMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_StatusStripMenuItem.Checked == false)
            {
                View_StatusStripMenuItem.Checked = true;
                MyStatusStrip.Visible = true;
                //add true to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowStatusStrip").InnerText = "true";
                doc.Save(configfile);
            }
            else
            {
                View_StatusStripMenuItem.Checked = false;
                MyStatusStrip.Visible = false;
                //add false to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowStatusStrip").InnerText = "false";
                doc.Save(configfile);
            }
        }

        //**************************************************************************************************************
        //     View->Tool Strip
        //**************************************************************************************************************
        private void View_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_ToolStripMenuItem.Checked == false)
            {
                View_ToolStripMenuItem.Checked = true;
                toolstrippanel.Visible = true;
                MyToolStripZ.Visible = true;
                //add true to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowToolStrip").InnerText = "true";
                doc.Save(configfile);
            }
            else
            {
                View_ToolStripMenuItem.Checked = false;
                toolstrippanel.Visible = false;
                MyToolStripZ.Visible = false;
                //add true to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowToolStrip").InnerText = "false";
                doc.Save(configfile);
            }
        }

        //**************************************************************************************************************
        //     View->Full Screen
        //**************************************************************************************************************
        private void View_FullScreenMenuItem_Click(object sender, EventArgs e)
        {
            if(View_FullScreenMenuItem.Checked==false)
            {
                View_FullScreenMenuItem.Checked = true;
                this.Visible = false;
                this.FormBorderStyle = FormBorderStyle.None;
                this.Visible = true;
            }
            else
            {
                View_FullScreenMenuItem.Checked = false;
                this.Visible = false;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.Visible = true;
            }
        }

        //**************************************************************************************************************
        //     View->Appearance->Default
        //**************************************************************************************************************
        private void View_Appearance_DefaultMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_Appearance_DefaultMenuItem.Checked == false)
            {
                View_Appearance_DefaultMenuItem.Checked = true;
                SetAppearances("Default");
                View_Appearance_SystemMenuItem.Checked = false;
                View_Appearance_LightMenuItem.Checked = false;
                View_Appearance_DarkMenuItem.Checked = false;
                View_Appearance_NightMenuItem.Checked = false;
                //add true to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/Appearance").InnerText = "Default";
                doc.Save(configfile);
            }
        }

        //**************************************************************************************************************
        //     View->Appearance->System
        //**************************************************************************************************************
        private void View_Appearance_SystemMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_Appearance_SystemMenuItem.Checked == false)
            {
                View_Appearance_SystemMenuItem.Checked = true;
                SetAppearances("System");
                View_Appearance_DefaultMenuItem.Checked = false;
                View_Appearance_LightMenuItem.Checked = false;
                View_Appearance_DarkMenuItem.Checked = false;
                View_Appearance_NightMenuItem.Checked = false;
                //add true to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/Appearance").InnerText = "System";
                doc.Save(configfile);
            }
        }

        //**************************************************************************************************************
        //     View->Appearance->Light
        //**************************************************************************************************************
        private void View_Appearance_LightMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_Appearance_LightMenuItem.Checked == false)
            {
                View_Appearance_LightMenuItem.Checked = true;
                SetAppearances("Light");
                View_Appearance_DefaultMenuItem.Checked = false;
                View_Appearance_SystemMenuItem.Checked = false;
                View_Appearance_DarkMenuItem.Checked = false;
                View_Appearance_NightMenuItem.Checked = false;
                //add true to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/Appearance").InnerText = "Light";
                doc.Save(configfile);
            }
        }

        //**************************************************************************************************************
        //     View->Appearance->Dark
        //**************************************************************************************************************
        private void View_Appearance_DarkMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_Appearance_DarkMenuItem.Checked == false)
            {
                View_Appearance_DarkMenuItem.Checked = true;
                SetAppearances("Dark");
                View_Appearance_DefaultMenuItem.Checked = false;
                View_Appearance_SystemMenuItem.Checked = false;
                View_Appearance_LightMenuItem.Checked = false;
                View_Appearance_NightMenuItem.Checked = false;
                //add true to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/Appearance").InnerText = "Dark";
                doc.Save(configfile);
            }
        }

        //**************************************************************************************************************
        //     View->Appearance->Night
        //**************************************************************************************************************
        private void View_Appearance_NightMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_Appearance_NightMenuItem.Checked == false)
            {
                View_Appearance_NightMenuItem.Checked = true;
                SetAppearances("Night");
                View_Appearance_DefaultMenuItem.Checked = false;
                View_Appearance_SystemMenuItem.Checked = false;
                View_Appearance_LightMenuItem.Checked = false;
                View_Appearance_DarkMenuItem.Checked = false;
                //add true to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/Appearance").InnerText = "Night";
                doc.Save(configfile);
            }
        }

        //**************************************************************************************************************
        //     View->Line Numbers
        //**************************************************************************************************************
        private void View_LineNumbersMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_LineNumbersMenuItem.Checked == false)
            {
                View_LineNumbersMenuItem.Checked = true;
                //add false to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowLineNumbers").InnerText = "true";
                doc.Save(configfile);

                DialogResult dg = MessageBox.Show("Restart IDE to add Line Numbers to all documents\nYou can open your project by going to File->Recent Projects\nClick OK to Restart....", "Restart or Not ?", MessageBoxButtons.OKCancel);
                if (dg == DialogResult.OK)
                {
                    File_CloseProjectMenuItem_Click(sender, e);
                    if(this.Text=="Silver-J")
                    {
                        Application.Restart();
                    }
                }
            }
            else
            {
                View_LineNumbersMenuItem.Checked = false;
                //add false to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowLineNumbers").InnerText = "false";
                doc.Save(configfile);

                DialogResult dg = MessageBox.Show("Restart IDE to remove Line Numbers from all documents\nYou can open your project by going to File->Recent Projects\nClick OK to Restart....", "Restart or Not ?", MessageBoxButtons.OKCancel);
                if (dg == DialogResult.OK)
                {
                    File_CloseProjectMenuItem_Click(sender, e);
                    if (this.Text == "Silver-J")
                    {
                        Application.Restart();
                    }
                }
            }
        }

        //**************************************************************************************************************
        //     View->Line Highlighter
        //**************************************************************************************************************
        private void View_LineHighlighterMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_LineHighlighterMenuItem.Checked == false)
            {
                View_LineHighlighterMenuItem.Checked = true;
                //add false to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowLineHighlighter").InnerText = "true";
                doc.Save(configfile);

                DialogResult dg = MessageBox.Show("Restart IDE to show Line Highlighter to all documents\nYou can open your project by going to File->Recent Projects\nClick OK to Restart....", "Restart or Not ?", MessageBoxButtons.OKCancel);
                if (dg == DialogResult.OK)
                {
                    File_CloseProjectMenuItem_Click(sender, e);
                    if (this.Text == "Silver-J")
                    {
                        Application.Restart();
                    }
                }
            }
            else
            {
                View_LineHighlighterMenuItem.Checked = false;
                //add false to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowLineHighlighter").InnerText = "false";
                doc.Save(configfile);

                DialogResult dg = MessageBox.Show("Restart IDE to remove Line Highlighter from all documents\nYou can open your project by going to File->Recent Projects\nClick OK to Restart....", "Restart or Not ?", MessageBoxButtons.OKCancel);
                if (dg == DialogResult.OK)
                {
                    File_CloseProjectMenuItem_Click(sender, e);
                    if (this.Text == "Silver-J")
                    {
                        Application.Restart();
                    }
                }
            }
        }

        //**************************************************************************************************************
        //     View->Invalid Lines
        //**************************************************************************************************************
        private void View_InvalidLinesMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_InvalidLinesMenuItem.Checked == false)
            {
                View_InvalidLinesMenuItem.Checked = true;
                //add false to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowInvalidLines").InnerText = "true";
                doc.Save(configfile);

                DialogResult dg = MessageBox.Show("Restart IDE to show Invalid Lines to all documents\nYou can open your project by going to File->Recent Projects\nClick OK to Restart....", "Restart or Not ?", MessageBoxButtons.OKCancel);
                if (dg == DialogResult.OK)
                {
                    File_CloseProjectMenuItem_Click(sender, e);
                    if (this.Text == "Silver-J")
                    {
                        Application.Restart();
                    }
                }
            }
            else
            {
                View_InvalidLinesMenuItem.Checked = false;
                //add false to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowInvalidLines").InnerText = "false";
                doc.Save(configfile);

                DialogResult dg = MessageBox.Show("Restart IDE to remove Invalid Lines from all documents\nYou can open your project by going to File->Recent Projects\nClick OK to Restart....", "Restart or Not ?", MessageBoxButtons.OKCancel);
                if (dg == DialogResult.OK)
                {
                    File_CloseProjectMenuItem_Click(sender, e);
                    if (this.Text == "Silver-J")
                    {
                        Application.Restart();
                    }
                }
            }
        }

        //**************************************************************************************************************
        //     View->End of Line Marker
        //**************************************************************************************************************
        private void View_EndLineMarkerMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_EndLineMarkerMenuItem.Checked == false)
            {
                View_EndLineMarkerMenuItem.Checked = true;
                //add false to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowEndOfLineMarker").InnerText = "true";
                doc.Save(configfile);

                DialogResult dg = MessageBox.Show("Restart IDE to show End Of Line Marker to all documents\nYou can open your project by going to File->Recent Projects\nClick OK to Restart....", "Restart or Not ?", MessageBoxButtons.OKCancel);
                if (dg == DialogResult.OK)
                {
                    File_CloseProjectMenuItem_Click(sender, e);
                    if (this.Text == "Silver-J")
                    {
                        Application.Restart();
                    }
                }
            }
            else
            {
                View_EndLineMarkerMenuItem.Checked = false;
                //add false to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowEndOfLineMarker").InnerText = "false";
                doc.Save(configfile);

                DialogResult dg = MessageBox.Show("Restart IDE to remove End Of Line Marker from all documents\nYou can open your project by going to File->Recent Projects\nClick OK to Restart....", "Restart or Not ?", MessageBoxButtons.OKCancel);
                if (dg == DialogResult.OK)
                {
                    File_CloseProjectMenuItem_Click(sender, e);
                    if (this.Text == "Silver-J")
                    {
                        Application.Restart();
                    }
                }
            }
        }

        //**************************************************************************************************************
        //     View->Visible Spaces
        //**************************************************************************************************************
        private void View_VisibleSpacesMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_VisibleSpacesMenuItem.Checked == false)
            {
                View_VisibleSpacesMenuItem.Checked = true;
                //add false to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowVisibleSpaces").InnerText = "true";
                doc.Save(configfile);

                DialogResult dg = MessageBox.Show("Restart IDE to show Visible Spaces to all documents\nYou can open your project by going to File->Recent Projects\nClick OK to Restart....", "Restart or Not ?", MessageBoxButtons.OKCancel);
                if (dg == DialogResult.OK)
                {
                    File_CloseProjectMenuItem_Click(sender, e);
                    if (this.Text == "Silver-J")
                    {
                        Application.Restart();
                    }
                }
            }
            else
            {
                View_VisibleSpacesMenuItem.Checked = false;
                //add false to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowVisibleSpaces").InnerText = "false";
                doc.Save(configfile);

                DialogResult dg = MessageBox.Show("Restart IDE to remove Visible Spaces from all documents\nYou can open your project by going to File->Recent Projects\nClick OK to Restart....", "Restart or Not ?", MessageBoxButtons.OKCancel);
                if (dg == DialogResult.OK)
                {
                    File_CloseProjectMenuItem_Click(sender, e);
                    if (this.Text == "Silver-J")
                    {
                        Application.Restart();
                    }
                }
            }
        }

        //**************************************************************************************************************
        //     View->Project Explorer
        //**************************************************************************************************************
        private void View_ProjectExplorerMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_ProjectExplorerMenuItem.Checked == false)
            {
                splitContainer3.Panel1Collapsed = false;
                View_ProjectExplorerMenuItem.Checked = true;
                //add true to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowProjectExplorer").InnerText = "true";
                doc.Save(configfile);
            }
            else
            {
                splitContainer3.Panel1Collapsed = true;
                View_ProjectExplorerMenuItem.Checked = false;
                //add false to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowProjectExplorer").InnerText = "false";
                doc.Save(configfile);
            }
        }

        //**************************************************************************************************************
        //     View->Classes View
        //**************************************************************************************************************
        private void View_ClassesViewMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_ClassesViewMenuItem.Checked == false)
            {
                splitContainer4.Panel1Collapsed = false;
                View_ClassesViewMenuItem.Checked = true;
                splitContainer1.SplitterDistance = this.Width - 250;
                splitContainer1.IsSplitterFixed = false;
                splitContainer1.Panel2Collapsed = false;

                //add true to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowClassesView").InnerText = "true";
                doc.Save(configfile);

                if (View_MethodsViewMenuItem.Checked == true)
                {
                    splitContainer1.SplitterDistance = this.Width - 250;
                    splitContainer1.IsSplitterFixed = false;
                }
                else if (View_MethodsViewMenuItem.Checked == false)
                {
                    splitContainer4.Panel2Collapsed = true;
                }
            }

            else if (View_ClassesViewMenuItem.Checked == true)
            {
                splitContainer4.Panel1Collapsed = true;
                View_ClassesViewMenuItem.Checked = false;

                //add false to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowClassesView").InnerText = "false";
                doc.Save(configfile);

                if (View_MethodsViewMenuItem.Checked == false)
                {
                    splitContainer1.Panel2Collapsed = true;
                }
            }
        }

        //**************************************************************************************************************
        //     View->Methods View
        //**************************************************************************************************************
        private void View_MethodsViewMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_MethodsViewMenuItem.Checked == false)
            {
                splitContainer4.Panel2Collapsed = false;
                View_MethodsViewMenuItem.Checked = true;
                splitContainer1.SplitterDistance = this.Width - 250;
                splitContainer1.IsSplitterFixed = false;
                splitContainer1.Panel2Collapsed = false;

                //add true to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowMethodsView").InnerText = "true";
                doc.Save(configfile);

                if (View_ClassesViewMenuItem.Checked == true)
                {
                    splitContainer1.SplitterDistance = this.Width - 250;
                    splitContainer1.IsSplitterFixed = false;
                }
            }

            else if (View_MethodsViewMenuItem.Checked == true)
            {
                splitContainer4.Panel2Collapsed = true;
                View_MethodsViewMenuItem.Checked = false;

                //add false to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowMethodsView").InnerText = "false";
                doc.Save(configfile);

                if (View_ClassesViewMenuItem.Checked == false)
                {
                    splitContainer1.Panel2Collapsed = true;
                }
            }
        }

        //**************************************************************************************************************
        //     View->Errors List
        //**************************************************************************************************************
        private void View_ErrorsListMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_ErrorsListMenuItem.Checked == false)
            {
                splitContainer2.Panel2Collapsed = false;
                View_ErrorsListMenuItem.Checked = true;

                //add true to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowErrorList").InnerText = "true";
                doc.Save(configfile);
            }
            else if (View_ErrorsListMenuItem.Checked == true)
            {
                splitContainer2.Panel2Collapsed = true;
                View_ErrorsListMenuItem.Checked = false;

                //add false to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowErrorList").InnerText = "false";
                doc.Save(configfile);
            }
        }

        //**************************************************************************************************************
        //     View->Show Error Dialog
        //**************************************************************************************************************
        private void View_ShowErrorDialogMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            if (View_ShowErrorDialogMenuItem.Checked == false)
            {
                showErrorDialog = true;
                View_ShowErrorDialogMenuItem.Checked = true;

                //add true to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowErrorDialog").InnerText = "true";
                doc.Save(configfile);
            }
            else if (View_ShowErrorDialogMenuItem.Checked == true)
            {
                showErrorDialog = false;
                View_ShowErrorDialogMenuItem.Checked = false;

                //add false to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/ShowErrorDialog").InnerText = "false";
                doc.Save(configfile);
            }
        }



        //**************************************************************************************************************
        //     View->View in Web Browser
        //**************************************************************************************************************
        public static Boolean isTabpagetextequaltofile = false;
        private void View_ViewinWebBrowserMenuItem_Click(object sender, EventArgs e)
        {
            String webbrowser = getWebBrowser();
            if (webbrowser == "null" || webbrowser == "")
            {
                String configfile = Application.StartupPath + "\\files\\config.slvjfile";
                DialogResult dg = MessageBox.Show("Web Browser is not selected\nClick OK to select web browser...", "Select Web Browser", MessageBoxButtons.OKCancel);
               // Checks if the browser exists in the default program directory  
                if (dg == DialogResult.OK)
                {
                    OpenFileDialog openfd = new OpenFileDialog();
                    openfd.Filter = "EXE files|*.exe";
                    if (openfd.ShowDialog() == DialogResult.OK)
                    {
                        String filename = openfd.FileName;
                        XmlDocument doc = new XmlDocument();
                        doc.Load(configfile);
                        doc.SelectSingleNode("SilverJConfiguration/WebBrowser").InnerText = filename;
                        doc.Save(configfile);
                    }
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(configfile);
                    doc.SelectSingleNode("SilverJConfiguration/WebBrowser").InnerText = "null";
                    doc.Save(configfile);
                }
            }
            else
            {
                String filename = "";

                if (myTabControl.TabCount > 0)
                {
                    String s = File.ReadAllText(Application.StartupPath + "\\files\\files.slvjfile");
                    RichTextBox rtb = new RichTextBox();
                    rtb.Text = s;
                    String[] lines = rtb.Lines;

                    foreach (String line in lines)
                    {
                        if (line != "")
                        {
                            TabPage tabpage = myTabControl.SelectedTab;
                            if (tabpage.Text.Contains(".html"))
                            {
                                if (line.Contains(tabpage.Text))
                                {
                                    String line2 = "";
                                    if(line.Contains(" "))
                                    {
                                        line2 = line.Replace(" ", "%20");
                                    }
                                    else
                                    {
                                        line2 = line;
                                    }
                                    isTabpagetextequaltofile = true;
                                    filename = line2;
                                }
                            }
                        }
                    }

                    if(filename!=""&&isTabpagetextequaltofile==true)
                    {
                        Process.Start(webbrowser, filename);
                        isTabpagetextequaltofile = false;
                    }
                }
            }
        }




//***********************************************************************************************************************************
//                                          Run
//***********************************************************************************************************************************



        //**************************************************************************************************************
        //      RunMenuItem Drop Down Opened
        //**************************************************************************************************************
        private void Run_DropDownOpened(object sender, EventArgs e)
        {
            if (this.Text == "Silver-J")
            {
                Run_CompileMenuItem.Enabled = false;
                Run_RunApplicationMenuItem.Enabled = false;
                Run_RunAppletMenuItem.Enabled = false;
                Run_RunwithParametersMenuItem.Enabled = false;
                Run_BuildMenuItem.Enabled = false;
                Run_MainClassMenuItem.Enabled = false;
                Run_AddFilesToProjectMenuItem.Enabled = false;
                Run_BuildProjectMenuItem.Enabled = false;
            }
            else
            {
                String prjtype = getCurrentProjectType();

                if (prjtype == "ApplicationType" && getMainClassFileName() != "")
                {
                    Run_CompileMenuItem.Enabled = true;
                    Run_RunApplicationMenuItem.Enabled = true;
                    Run_RunwithParametersMenuItem.Enabled = true;
                    Run_RunAppletMenuItem.Enabled = false;
                    Run_BuildMenuItem.Enabled = true;
                    Run_MainClassMenuItem.Enabled = true;
                    Run_AddFilesToProjectMenuItem.Enabled = true;
                    //get current project name
                    String mainclassfilename = getMainClassFileName();
                    String mnclass = mainclassfilename.Substring(mainclassfilename.LastIndexOf("\\") + 1);
                    mnclass = mnclass.Remove(mnclass.Length - 5);
                    Run_BuildProjectMenuItem.Text = "Build " + mnclass;
                    Run_BuildProjectMenuItem.Enabled = true;
                }
                else if (prjtype == "AppletType" && getMainClassFileName() != "")
                {
                    Run_CompileMenuItem.Enabled = true;
                    Run_RunApplicationMenuItem.Enabled = false;
                    Run_RunwithParametersMenuItem.Enabled = false;
                    Run_RunAppletMenuItem.Enabled = true;
                    Run_BuildMenuItem.Enabled = true;
                    Run_MainClassMenuItem.Enabled = true;
                    Run_AddFilesToProjectMenuItem.Enabled = true;
                    //get current project name
                    String mainclassfilename = getMainClassFileName();
                    String mnclass = mainclassfilename.Substring(mainclassfilename.LastIndexOf("\\") + 1);
                    mnclass = mnclass.Remove(mnclass.Length - 5);
                    Run_BuildProjectMenuItem.Text = "Build " + mnclass;
                    Run_BuildProjectMenuItem.Enabled = true;
                }
            }
        }


        //**************************************************************************************************************
        //      Run->Compile
        //**************************************************************************************************************
        /// <summary>
        /// Returns boolean value if process is completed successfully
        /// </summary>
        /// <param name="EXE">program which you want to run (javac.exe)</param>
        /// <param name="WorkingDirectory">directory name where .java file is stored</param>
        /// <param name="FileName">.java file name</param>
        /// <returns></returns>
        public bool Compile(String EXE, String WorkingDirectory, String FileName)
        {
            bool processStarted = false;

            if (File.Exists(EXE))
            {
                process.StartInfo.FileName = EXE;
                process.StartInfo.Arguments = FileName;
                process.StartInfo.WorkingDirectory = WorkingDirectory;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.ErrorDialog = false;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                processStarted = process.Start();
            }
            else
            {
                MessageBox.Show("Unable to compile java file. Check your Java Path settings: Current Java Path : ");
            }
            return processStarted;
        }


        /// <summary>
        /// starts a process by calling Compile function
        /// means just compiling java file through command line by starting process of CMD
        /// </summary>
        /// <param name="file">.java file name(Main class file)</param>
        /// <param name="jdkpath">JDK path</param>
        public void CompileJava(String file, String jdkpath)
        {
            String mystr = file;
            if (mystr.Contains(".java"))
            {
                mystr = mystr.Remove(mystr.Length - 5);
            }
            if (this.Compile(jdkpath + "\\javac.exe", Path.GetDirectoryName(file), Path.GetFileName(file)))
            {
                ErrorReader = process.StandardError;
                string response = ErrorReader.ReadToEnd();

                if (response != "")
                {
                    ErrorTextBox.Text = response;
                    if (showErrorDialog == true)
                    {
                        MessageBox.Show("" + response, "Errors");
                    }
                }
                else if (response == "")
                {
                    ErrorTextBox.Text = "Program Compiled Successfully.................!";
                }
                else if (response.Contains("uses or overrides a deprecated API."))
                {
                    ErrorTextBox.Text = "Program Compiled Successfully.................!";
                }
                else
                {
                    ErrorTextBox.Text = "Program Compiled Successfully.................!";
                }
            }
            else if (File.Exists("" + mystr + ".class"))
            {
                ErrorTextBox.Text = "Program Compiled Successfully.................!";
            }

            //set caret position to error line number
            if(myTabControl.TabCount>0)
            {
                if(myTabControl.SelectedTab.Text.Contains(".java"))
                {
                    String compilejavafilename = myTabControl.SelectedTab.Text;
                    if (ErrorTextBox.Text.Contains(compilejavafilename))
                    {
                        int select_index = myTabControl.SelectedIndex;
                        var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                        RichTextBox rtb = new RichTextBox();
                        rtb.Text = texteditor.Text;

                        for(int i=0;i<rtb.Lines.Length;i++)
                        {
                            if(ErrorTextBox.Lines[0].Contains(i.ToString()))
                            {
                                texteditor.ActiveTextAreaControl.TextArea.Caret.Line = i-1;
                            }
                        }
                    }
                }
            }
        }

        private void Run_CompileMenuItem_Click(object sender, EventArgs e)
        {
            File_SaveAllMenuItem_Click(sender, e);
            ShowAboutToolStripLabel.Text = "Ready";

            String projectlocationfolder = getCurrentProjectLocationFolder();
            //compile program
            if (projectlocationfolder != "")
            {
                if (Directory.Exists(projectlocationfolder + "\\srcclasses"))
                {
                    String filename = getMainClassFileName();
                    String jdkpath = getJDKPath();
                    //process = new Process();
                    this.CompileJava(filename, jdkpath);
                }
            }
            //delete all files from classes directory
            if (Directory.Exists(projectlocationfolder + "\\classes"))
            {
                String[] files = Directory.GetFiles(projectlocationfolder + "\\classes");
                foreach (String file in files)
                {
                    if (file.Contains(".class"))
                    {
                        File.Delete(file);
                    }
                }
            }
            //copy all .class files from srcclasses folder to classes folder
            if (Directory.Exists(projectlocationfolder + "\\srcclasses"))
            {
                String[] files = Directory.GetFiles(projectlocationfolder + "\\srcclasses");
                foreach (String file in files)
                {
                    if (file.Contains(".class"))
                    {
                        String file2 = file.Replace("srcclasses", "classes");
                        File.Copy(file, file2);
                    }
                }
            }
        }



        //**************************************************************************************************************
        //      Run->Run Application
        //**************************************************************************************************************
        /// <summary>
        /// starts the process of CMD with java.exe file from jdk\bin path
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="workingDirectory"></param>
        /// <param name="jdkpath"></param>
        public void RunJava(String filename, String workingDirectory, String jdkpath)
        {
            ErrorTextBox.Text = "";

            if (ErrorTextBox.Text == "")
            {
                if (this.Compile(jdkpath + "\\javac.exe", Path.GetDirectoryName(filename), Path.GetFileName(filename)))
                {
                    ErrorReader = process.StandardError;
                    string response = ErrorReader.ReadToEnd();
                    if (response != "")
                    {
                        ErrorTextBox.Text = response;
                        if (showErrorDialog == true)
                        {
                            MessageBox.Show("" + Environment.NewLine + Environment.NewLine + response, "Errors");
                        }
                    }
                    else if (response == "")
                    {
                        ErrorTextBox.Text = "Program Compiled Successfully.................!";
                    }
                    else
                    {
                        ErrorTextBox.Text = "Program Compiled Successfully.................!";
                    }
                }


                //set caret position to error line number
                if (myTabControl.TabCount > 0)
                {
                    if (myTabControl.SelectedTab.Text.Contains(".java"))
                    {
                        String compilejavafilename = myTabControl.SelectedTab.Text;
                        if (ErrorTextBox.Text.Contains(compilejavafilename))
                        {
                            int select_index = myTabControl.SelectedIndex;
                            var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                            RichTextBox rtb = new RichTextBox();
                            rtb.Text = texteditor.Text;

                            for (int i = 0; i < rtb.Lines.Length; i++)
                            {
                                if (ErrorTextBox.Lines[0].Contains(i.ToString()))
                                {
                                    texteditor.ActiveTextAreaControl.TextArea.Caret.Line = i - 1;
                                }
                            }
                        }
                    }
                }

                String projectlocationfolder = getCurrentProjectLocationFolder();
                if (Directory.Exists(projectlocationfolder + "\\srcclasses"))
                {
                    if (filename.Contains(".java"))
                    {
                        String ffname = filename.Remove(filename.Length - 5);
                        ffname = ffname + ".class";

                        if (File.Exists(ffname))
                        {
                            ProcessStartInfo ProcessInfo;
                            //Process process;
                            String javapath = "\"" + jdkpath + "\\java.exe" + "\"";
                            String getfilename = filename.Substring(filename.LastIndexOf("\\") + 1);
                            String fname = "";
                            if (getfilename.Contains(".java"))
                            {
                                fname = getfilename.Remove(getfilename.Length - 5);
                            }
                            ProcessInfo = new ProcessStartInfo("cmd.exe", "/K" + "  cd/   &&  cd " + workingDirectory + "  &&  " + javapath + "  " + fname);
                            ProcessInfo.CreateNoWindow = true;
                            ProcessInfo.UseShellExecute = true;
                            Process.Start(ProcessInfo);
                        }
                    }
                }
            }

            else if (ErrorTextBox.Text.Contains("uses or overrides a deprecated API.") || ErrorTextBox.Text.Contains("Note: Recompile with -Xlint:deprecation for details."))
            {
                ProcessStartInfo ProcessInfo;
                String javapath = "\"" + jdkpath + "\\java.exe" + "\"";
                String getfilename = filename.Substring(filename.LastIndexOf("\\") + 1);
                String fname = "";
                if (getfilename.Contains(".java"))
                {
                    fname = getfilename.Remove(getfilename.Length - 5);
                }
                ProcessInfo = new ProcessStartInfo("cmd.exe", "/K" + "  cd/   &&  cd " + workingDirectory + "  &&  " + javapath + "  " + fname);
                ProcessInfo.CreateNoWindow = true;
                ProcessInfo.UseShellExecute = true;
                Process.Start(ProcessInfo);
            }

            else if (ErrorTextBox.Text.Contains("Program Compiled Successfully.................!"))
            {
                ProcessStartInfo ProcessInfo;
                String javapath = "\"" + jdkpath + "\\java.exe" + "\"";
                String getfilename = filename.Substring(filename.LastIndexOf("\\") + 1);
                String fname = "";
                if (getfilename.Contains(".java"))
                {
                    fname = getfilename.Remove(getfilename.Length - 5);
                }
                ProcessInfo = new ProcessStartInfo("cmd.exe", "/K" + "  cd/   &&  cd " + workingDirectory + "  &&  " + javapath + "  " + fname + "  && exit");
                ProcessInfo.CreateNoWindow = true;
                ProcessInfo.UseShellExecute = true;
                Process.Start(ProcessInfo);
            }

            else
            {
                ProcessStartInfo ProcessInfo;
                String javapath = "\"" + jdkpath + "\\java.exe" + "\"";
                String getfilename = filename.Substring(filename.LastIndexOf("\\") + 1);
                String fname = "";
                if (getfilename.Contains(".java"))
                {
                    fname = getfilename.Remove(getfilename.Length - 5);
                }
                ProcessInfo = new ProcessStartInfo("cmd.exe", "/K" + "  cd/   &&  cd " + workingDirectory + "  &&  " + javapath + "  " + fname);
                ProcessInfo.CreateNoWindow = true;
                ProcessInfo.UseShellExecute = true;
                Process.Start(ProcessInfo);
            }
        }

        private void Run_RunApplicationMenuItem_Click(object sender, EventArgs e)
        {
            File_SaveAllMenuItem_Click(sender, e);
            ShowAboutToolStripLabel.Text = "Ready";

            String projectlocationfolder = getCurrentProjectLocationFolder();

            if (projectlocationfolder != "")
            {
                if (Directory.Exists(projectlocationfolder + "\\srcclasses"))
                {
                    String[] files = Directory.GetFiles(projectlocationfolder + "\\srcclasses");
                    foreach (String file in files)
                    {
                        if (file.Contains(".class"))
                        {
                            File.Delete(file);
                        }
                    }
                }
            }
            //Run Program
            if (projectlocationfolder != "")
            {
                if (Directory.Exists(projectlocationfolder + "\\srcclasses"))
                {
                    String filename = getMainClassFileName();
                    String jdkpath = getJDKPath();
                    this.RunJava(filename, projectlocationfolder + "\\srcclasses", jdkpath);
                }
            }

            //delete all files from classes directory
            if (Directory.Exists(projectlocationfolder + "\\classes"))
            {
                String[] files = Directory.GetFiles(projectlocationfolder + "\\classes");
                foreach (String file in files)
                {
                    if (file.Contains(".class"))
                    {
                        File.Delete(file);
                    }
                }
            }
            //copy all .class files from srcclasses folder to classes folder
            if (Directory.Exists(projectlocationfolder + "\\srcclasses"))
            {
                String[] files = Directory.GetFiles(projectlocationfolder + "\\srcclasses");
                foreach (String file in files)
                {
                    if (file.Contains(".class"))
                    {
                        String file2 = file.Replace("srcclasses", "classes");
                        File.Copy(file, file2);
                    }
                }
            }
        }



        //**************************************************************************************************************
        //      Run->Applet
        //**************************************************************************************************************
        /// <summary>
        /// starts process of .html file by passing it to \jdk\bin\appletviewer.exe
        /// </summary>
        /// <param name="filename">.java file name</param>
        /// <param name="appletviewerhtmlfilename">.html file name</param>
        /// <param name="workingDirectory"></param>
        /// <param name="jdkpath"></param>
        public void RunApplet(String filename, String appletviewerhtmlfilename, String workingDirectory, String jdkpath)
        {
            ErrorTextBox.Text = "";

            if (ErrorTextBox.Text == "")
            {
                if (this.Compile(jdkpath + "\\javac.exe", Path.GetDirectoryName(filename), Path.GetFileName(filename)))
                {
                    ErrorReader = process.StandardError;
                    string response = ErrorReader.ReadToEnd();
                    if (response != "")
                    {
                        ErrorTextBox.Text = response;
                        if (showErrorDialog == true)
                        {
                            MessageBox.Show("" + Environment.NewLine + Environment.NewLine + response, "Errors");
                        }
                    }
                    else if (response == "")
                    {
                        ErrorTextBox.Text = "Program Compiled Successfully.................!";
                    }
                    else
                    {
                        ErrorTextBox.Text = "Program Compiled Successfully.................!";
                    }
                }


                //set caret position to error line number
                if (myTabControl.TabCount > 0)
                {
                    if (myTabControl.SelectedTab.Text.Contains(".java"))
                    {
                        String compilejavafilename = myTabControl.SelectedTab.Text;
                        if (ErrorTextBox.Text.Contains(compilejavafilename))
                        {
                            int select_index = myTabControl.SelectedIndex;
                            var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                            RichTextBox rtb = new RichTextBox();
                            rtb.Text = texteditor.Text;

                            for (int i = 0; i < rtb.Lines.Length; i++)
                            {
                                if (ErrorTextBox.Lines[0].Contains(i.ToString()))
                                {
                                    texteditor.ActiveTextAreaControl.TextArea.Caret.Line = i - 1;
                                }
                            }
                        }
                    }
                }

                String projectlocationfolder = getCurrentProjectLocationFolder();
                if (Directory.Exists(projectlocationfolder + "\\srcclasses"))
                {
                    String mainclassfile = filename.Replace(".java", ".class");

                    if (File.Exists(appletviewerhtmlfilename))
                    {
                        if (File.Exists(mainclassfile))
                        {
                            ProcessStartInfo ProcessInfo;
                            String javapath = "\"" + jdkpath + "\\appletviewer.exe" + "\"";
                            String gethtmlfilename = appletviewerhtmlfilename.Substring(filename.LastIndexOf("\\") + 1);

                            ProcessInfo = new ProcessStartInfo("cmd.exe", "/K" + "  cd/   &&  cd " + workingDirectory + "  &&  " + javapath + "  " + gethtmlfilename);
                            ProcessInfo.CreateNoWindow = true;
                            ProcessInfo.UseShellExecute = true;
                            Process.Start(ProcessInfo);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot found applet html file........", "Error  to run");
                    }
                }
            }
        }

        private void Run_RunAppletMenuItem_Click(object sender, EventArgs e)
        {
            File_SaveAllMenuItem_Click(sender, e);
            ShowAboutToolStripLabel.Text = "Ready";

            String projectlocationfolder = getCurrentProjectLocationFolder();

            if (projectlocationfolder != "")
            {
                if (Directory.Exists(projectlocationfolder + "\\srcclasses"))
                {
                    String[] files = Directory.GetFiles(projectlocationfolder + "\\srcclasses");
                    foreach (String file in files)
                    {
                        if (file.Contains(".class"))
                        {
                            File.Delete(file);
                        }
                    }
                }
            }
            //Run Applet
            if (projectlocationfolder != "")
            {
                if (Directory.Exists(projectlocationfolder + "\\srcclasses"))
                {
                    String filename = getMainClassFileName();
                    String jdkpath = getJDKPath();
                    String applethtmlfile = getAppletViewerHTMLFileName();
                    this.RunApplet(filename, applethtmlfile, projectlocationfolder + "\\srcclasses", jdkpath);
                }
            }

            //delete all files from classes directory
            if (Directory.Exists(projectlocationfolder + "\\classes"))
            {
                String[] files = Directory.GetFiles(projectlocationfolder + "\\classes");
                foreach (String file in files)
                {
                    if (file.Contains(".class"))
                    {
                        File.Delete(file);
                    }
                }
            }
            //copy all .class files from srcclasses folder to classes folder
            if (Directory.Exists(projectlocationfolder + "\\srcclasses"))
            {
                String[] files = Directory.GetFiles(projectlocationfolder + "\\srcclasses");
                foreach (String file in files)
                {
                    if (file.Contains(".class"))
                    {
                        String file2 = file.Replace("srcclasses", "classes");
                        File.Copy(file, file2);
                    }
                }
            }
        }

        //**************************************************************************************************************
        //      Run->run with Parameters
        //**************************************************************************************************************
        public void RunWithParameters(String filename, String parameters, String workingDirectory, String jdkpath)
        {
            ErrorTextBox.Text = "";

            if (ErrorTextBox.Text == "")
            {
                if (this.Compile(jdkpath + "\\javac.exe", Path.GetDirectoryName(filename), Path.GetFileName(filename)))
                {
                    ErrorReader = process.StandardError;
                    string response = ErrorReader.ReadToEnd();
                    if (response != "")
                    {
                        ErrorTextBox.Text = response;
                        if (showErrorDialog == true)
                        {
                            MessageBox.Show("" + Environment.NewLine + Environment.NewLine + response, "Errors");
                        }
                    }
                    else if (response == "")
                    {
                        ErrorTextBox.Text = "Program Compiled Successfully.................!";
                    }
                    else
                    {
                        ErrorTextBox.Text = "Program Compiled Successfully.................!";
                    }
                }

                String projectlocationfolder = getCurrentProjectLocationFolder();
                if (Directory.Exists(projectlocationfolder + "\\srcclasses"))
                {
                    if (filename.Contains(".java"))
                    {
                        String ffname = filename.Remove(filename.Length - 5);
                        ffname = ffname + ".class";

                        if (File.Exists(ffname))
                        {
                            ProcessStartInfo ProcessInfo;
                            String javapath = "\"" + jdkpath + "\\java.exe" + "\"";
                            String getfilename = filename.Substring(filename.LastIndexOf("\\") + 1);
                            String fname = "";
                            if (getfilename.Contains(".java"))
                            {
                                fname = getfilename.Remove(getfilename.Length - 5);
                            }
                            ProcessInfo = new ProcessStartInfo("cmd.exe", "/K" + "  cd/   &&  cd " + workingDirectory + "  &&  " + javapath + "  " + fname + "  " + parameters);
                            ProcessInfo.CreateNoWindow = true;
                            ProcessInfo.UseShellExecute = true;
                            Process.Start(ProcessInfo);
                        }
                    }
                }
            }

            else if (ErrorTextBox.Text.Contains("uses or overrides a deprecated API.") || ErrorTextBox.Text.Contains("Note: Recompile with -Xlint:deprecation for details."))
            {
                ProcessStartInfo ProcessInfo;
                String javapath = "\"" + jdkpath + "\\java.exe" + "\"";
                String getfilename = filename.Substring(filename.LastIndexOf("\\") + 1);
                String fname = "";
                if (getfilename.Contains(".java"))
                {
                    fname = getfilename.Remove(getfilename.Length - 5);
                }
                ProcessInfo = new ProcessStartInfo("cmd.exe", "/K" + "  cd/   &&  cd " + workingDirectory + "  &&  " + javapath + "  " + fname + "  " + parameters);
                ProcessInfo.CreateNoWindow = true;
                ProcessInfo.UseShellExecute = true;
                Process.Start(ProcessInfo);
            }

            else if (ErrorTextBox.Text.Contains("Program Compiled Successfully.................!"))
            {
                ProcessStartInfo ProcessInfo;
                String javapath = "\"" + jdkpath + "\\java.exe" + "\"";
                String getfilename = filename.Substring(filename.LastIndexOf("\\") + 1);
                String fname = "";
                if (getfilename.Contains(".java"))
                {
                    fname = getfilename.Remove(getfilename.Length - 5);
                }
                ProcessInfo = new ProcessStartInfo("cmd.exe", "/K" + "  cd/   &&  cd " + workingDirectory + "  &&  " + javapath + "  " + fname + "  " + parameters);
                ProcessInfo.CreateNoWindow = true;
                ProcessInfo.UseShellExecute = true;
                Process.Start(ProcessInfo);
            }

            else
            {
                ProcessStartInfo ProcessInfo;
                String javapath = "\"" + jdkpath + "\\java.exe" + "\"";
                String getfilename = filename.Substring(filename.LastIndexOf("\\") + 1);
                String fname = "";
                if (getfilename.Contains(".java"))
                {
                    fname = getfilename.Remove(getfilename.Length - 5);
                }
                ProcessInfo = new ProcessStartInfo("cmd.exe", "/K" + "  cd/   &&  cd " + workingDirectory + "  &&  " + javapath + "  " + fname + "  " + parameters);
                ProcessInfo.CreateNoWindow = true;
                ProcessInfo.UseShellExecute = true;
                Process.Start(ProcessInfo);
            }
        }

        private void Run_RunwithParametersMenuItem_Click(object sender, EventArgs e)
        {
            RunwithParameter_Form rwpf = new RunwithParameter_Form();
            rwpf.ShowDialog();

            if (rwpf.IsFinished() == true && rwpf.getParameters() != "")
            {
                File_SaveAllMenuItem_Click(sender, e);
                ShowAboutToolStripLabel.Text = "Ready";

                String projectlocationfolder = getCurrentProjectLocationFolder();

                if (projectlocationfolder != "")
                {
                    if (Directory.Exists(projectlocationfolder + "\\srcclasses"))
                    {
                        String[] files = Directory.GetFiles(projectlocationfolder + "\\srcclasses");
                        foreach (String file in files)
                        {
                            if (file.Contains(".class"))
                            {
                                File.Delete(file);
                            }
                        }
                    }
                }
                //Run Program
                if (projectlocationfolder != "")
                {
                    if (Directory.Exists(projectlocationfolder + "\\srcclasses"))
                    {
                        String filename = getMainClassFileName();
                        String jdkpath = getJDKPath();
                        this.RunWithParameters(filename, rwpf.getParameters(), projectlocationfolder + "\\srcclasses", jdkpath);
                    }
                }

                //delete all files from classes directory
                if (Directory.Exists(projectlocationfolder + "\\classes"))
                {
                    String[] files = Directory.GetFiles(projectlocationfolder + "\\classes");
                    foreach (String file in files)
                    {
                        if (file.Contains(".class"))
                        {
                            File.Delete(file);
                        }
                    }
                }
                //copy all .class files from srcclasses folder to classes folder
                if (Directory.Exists(projectlocationfolder + "\\srcclasses"))
                {
                    String[] files = Directory.GetFiles(projectlocationfolder + "\\srcclasses");
                    foreach (String file in files)
                    {
                        if (file.Contains(".class"))
                        {
                            String file2 = file.Replace("srcclasses", "classes");
                            File.Copy(file, file2);
                        }
                    }
                }
            }
        }



        //**************************************************************************************************************
        //      Run->Auto Compile Program
        //**************************************************************************************************************
        private void Run_AutoCompileProgramMenuItem_Click(object sender, EventArgs e)
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";

            if (Run_AutoCompileProgramMenuItem.Checked == false)
            {
                Run_AutoCompileProgramMenuItem.Checked = true;
                //add true to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/AutoCompileJava").InnerText = "true";
                doc.Save(configfile);
            }
            else
            {
                Run_AutoCompileProgramMenuItem.Checked = false;
                //add false to config.exjfile
                XmlDocument doc = new XmlDocument();
                doc.Load(configfile);
                doc.SelectSingleNode("SilverJConfiguration/AutoCompileJava").InnerText = "false";
                doc.Save(configfile);
            }
        }

        //**************************************************************************************************************
        //      Run->Main Class
        //**************************************************************************************************************
        private void Run_MainClassMenuItem_Click(object sender, EventArgs e)
        {
            Run_MainClass_Form runmainclassf = new Run_MainClass_Form();
            runmainclassf.ShowDialog();
        }

        //**************************************************************************************************************
        //      Run->Add Files To Project
        //**************************************************************************************************************
        /// <summary>
        /// selects files from open file dialog
        /// and adds them to project, also these files can be added to builded jar application
        /// but will not add .java file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Run_AddFilesToProjectMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "All files|*.*";
            openfiledialog.Multiselect = true;

            if (openfiledialog.ShowDialog() == DialogResult.OK)
            {
                String[] files = openfiledialog.FileNames;
                String projectfolder = getCurrentProjectLocationFolder();
                if (Directory.Exists(projectfolder))
                {
                    if (Directory.Exists(projectfolder + "\\srcclasses"))
                    {
                        String prjfolder = projectfolder + "\\srcclasses";
                        foreach (String file in files)
                        {
                            String fname = file.Substring(file.LastIndexOf("\\") + 1);
                            if (File.Exists(prjfolder + "\\" + fname))
                            {
                                MessageBox.Show("The file name " + fname + " is already exists in that folder or already added to your project", "Error....", MessageBoxButtons.OK);
                                //File.Delete(prjfolder + "\\" + fname);
                                //File.Copy(file, prjfolder + "\\" + fname);
                            }
                            else
                            {
                                File.Copy(file, prjfolder + "\\" + fname);

                                //add files to project file
                                if (ReadCurrentProjectFileName() != "")
                                {
                                    String projectfilename = ReadCurrentProjectFileName();
                                    XmlDocument xmldoc = new XmlDocument();
                                    xmldoc.Load(projectfilename);
                                    XmlNode node = xmldoc.CreateNode(XmlNodeType.Element, "OtherFile", null);
                                    node.InnerText = prjfolder + "\\" + fname;
                                    xmldoc.DocumentElement.AppendChild(node);
                                    xmldoc.Save(projectfilename);

                                    AddFilesToProjectExplorerTreeView();
                                }
                            }
                        }
                    }
                }
            }
        }



        //**************************************************************************************************************
        //      Run->Build
        //**************************************************************************************************************
        public List<String> getPackageNamesList()
        {
            List<String> packlist = new List<String> { };

            using (XmlReader reader = XmlReader.Create(ReadCurrentProjectFileName()))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "PackageName":
                                packlist.Add(reader.ReadString());
                                break;
                        }
                    }
                }
            }
            return packlist;
        }

        /// <summary>
        /// gets all files from srcclasses instead of .java source files and create .jar file
        /// by adding manifest file to it
        /// by creating process with jdk\bin\jar.exe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Run_BuildMenuItem_Click(object sender, EventArgs e)
        {
            String projectfolderpath = getCurrentProjectLocationFolder();
            if (projectfolderpath != "")
            {
                if (Directory.Exists(projectfolderpath + "\\srcclasses"))
                {
                    String mainclass = getMainClassFileName();
                    String mnclass = mainclass.Substring(mainclass.LastIndexOf("\\") + 1);
                    mnclass = mnclass.Remove(mnclass.Length - 5);
                    //create mainclass manifest file
                    String maintexttomanifest = "Manifest-Version: 1.0"
                        + "\nAnt-Version: Apache Ant 1.9.4"
                        + "\nCreated-By: 1.8.0_25-b18 (Oracle Corporation)"
                        + "\nClass-Path: "
                        + "\nX-COMMENT: Main-Class will be added automatically by build"
                        + "\nMain-Class: " + mnclass
                        + "\nMain-Class: " + mnclass;

                    String manifestfile = projectfolderpath + "\\srcclasses\\mainclass.mf";
                    StreamWriter strw = new StreamWriter(manifestfile);
                    strw.Write(maintexttomanifest);
                    strw.Close();
                    strw.Dispose();

                    if (File.Exists(manifestfile))
                    {
                        String srcclassesfolderpath = projectfolderpath + "\\srcclasses";
                        List<String> packagenameslist = getPackageNamesList();
                        TextBox textbox = new TextBox();
                        string[] files = Directory.GetFiles(srcclassesfolderpath);

                        foreach (string filePath in files)
                        {
                            if (Path.GetExtension(filePath) != ".java" && Path.GetExtension(filePath) != ".mf")
                            {
                                textbox.Paste(" " + Path.GetFileName(filePath));
                            }
                        }

                        //check package folder exists or not
                        RichTextBox rtb = new RichTextBox();
                        String[] dirs = Directory.GetDirectories(srcclassesfolderpath);
                        foreach (String packagefolder in packagenameslist)
                        {
                            rtb.Text = rtb.Text.Insert(rtb.SelectionStart, "" + packagefolder + "  ");
                        }

                        //get folder by leaving package folders
                        foreach (String dirsname in dirs)
                        {
                            String dirs2 = dirsname.Substring(dirsname.LastIndexOf("\\") + 1);
                            if (rtb.Text.Contains(dirs2)) { }

                            else
                            {
                                textbox.Paste(" " + dirs2 + "* ");
                            }
                        }


                        //create jar file
                        if (File.Exists(manifestfile))
                        {
                            String jarfilepath = "\"" + getJDKPath() + "\\jar.exe" + "\"";
                            String prjfolder = getCurrentProjectLocationFolder() + "\\srcclasses";
                            String manifestfilename = manifestfile.Substring(manifestfile.LastIndexOf("\\") + 1);
                            String classes = textbox.Text;
                            String mainclassfile = mainclass.Substring(mainclass.LastIndexOf("\\") + 1);
                            mainclassfile = mainclassfile.Remove(mainclassfile.Length - 5);
                            String jarfilename = mainclassfile + ".jar";

                            ProcessStartInfo ProcessInfo;
                            Process Process;
                            ProcessInfo = new ProcessStartInfo("cmd.exe", "/K " + "cd/  &&  cd  " + prjfolder + " &&  " + jarfilepath + "  cmf   " + manifestfilename + "  " + jarfilename + "   " + classes);
                            ProcessInfo.CreateNoWindow = true;
                            ProcessInfo.UseShellExecute = true;
                            ProcessInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            Process = Process.Start(ProcessInfo);
                        }

                        //create build folder and moved created jar file to build folder
                        String createdjarfile = mainclass.Remove(mainclass.Length - 5) + ".jar";
                        if (File.Exists(createdjarfile))
                        {
                            String buildfolder = getCurrentProjectLocationFolder() + "\\build";

                            if (Directory.Exists(buildfolder))
                            {
                                String createdjarfile2 = createdjarfile.Replace("srcclasses", "build");
                                File.Delete(createdjarfile2);
                                File.Copy(createdjarfile, createdjarfile2);

                                try
                                {
                                    File.Delete(createdjarfile);
                                }
                                catch { }

                                ShowAboutToolStripLabel.Text = "Build Completed";
                                Run_BuildMenuItem_Click(sender, e);
                            }
                            else
                            {
                                Directory.CreateDirectory(buildfolder);
                                String createdjarfile2 = createdjarfile.Replace("srcclasses", "build");
                                File.Copy(createdjarfile, createdjarfile2);

                                ShowAboutToolStripLabel.Text = "Build Completed";
                                Run_BuildMenuItem_Click(sender, e);
                            }
                        }
                    }
                }
            }
        }



        //**************************************************************************************************************
        //      Run->Build Project
        //**************************************************************************************************************
        private void Run_BuildProjectMenuItem_Click(object sender, EventArgs e)
        {
            Run_BuildMenuItem_Click(sender, e);
            Run_BuildMenuItem_Click(sender, e);
        }

        //**************************************************************************************************************
        //      Run-> Preview HTML Page
        //**************************************************************************************************************
        private void Run_PreviewHTMLPageMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                TabPage tabpage = myTabControl.SelectedTab;
                if (tabpage.Text.Contains(".html") && FilenameToolStripLabel.Text.Contains("\\"))
                {
                    Preview_HTML_Form prvhtmlf = new Preview_HTML_Form(tabpage, this, FilenameToolStripLabel.Text);
                    prvhtmlf.FileTextBox.Text = FilenameToolStripLabel.Text;
                    prvhtmlf.Show();
                }
            }
        }


        //**************************************************************************************************************
        //      Run->Options
        //**************************************************************************************************************
        private void Run_OptionsMenuItem_Click(object sender, EventArgs e)
        {
            Options_Form optionsform = new Options_Form(this, myTabControl);
            optionsform.ShowDialog();
            showErrorDialog = optionsform.ShowErrorDialog();
            String appearance = optionsform.getAppearance();
            if(appearance!="")
            {
                if(appearance=="Default")
                {
                    View_Appearance_DefaultMenuItem_Click(sender, e);
                }
                else if(appearance=="System")
                {
                    View_Appearance_SystemMenuItem_Click(sender, e);
                }
                else if(appearance=="Light")
                {
                    View_Appearance_LightMenuItem_Click(sender, e);
                }
                else if(appearance=="Dark")
                {
                    View_Appearance_DarkMenuItem_Click(sender, e);
                }
                else if(appearance=="Night")
                {
                    View_Appearance_NightMenuItem_Click(sender, e);
                }
            }
        }



//***********************************************************************************************************************************
//                                          Window
//***********************************************************************************************************************************



        //**************************************************************************************************************
        //      WindowMenuItem Drop Down Opened
        //**************************************************************************************************************
        private void WindowMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            if(myTabControl.TabCount>0)
            {
                Window_CloseAllWindowsMenuItem.Enabled = true;
            }
            else
            {
                Window_CloseAllWindowsMenuItem.Enabled = false;
            }
        }

        //**************************************************************************************************************
        //      Window->Restart
        //**************************************************************************************************************
        private void Window_RestartMenuItem_Click(object sender, EventArgs e)
        {
            File_CloseProjectMenuItem_Click(sender, e);
            
            if(this.Text=="Silver-J")
            {
                Application.Restart();
            }
        }

        //**************************************************************************************************************
        //      Window->Close All Windows
        //**************************************************************************************************************
        private void Window_CloseAllWindowsMenuItem_Click(object sender, EventArgs e)
        {
            File_CloseAllMenuItem_Click(sender, e);
        }



//***********************************************************************************************************************************
//                                         Help
//***********************************************************************************************************************************



        //**************************************************************************************************************
        //      Help->View Help Topics
        //**************************************************************************************************************
        private void Help_ViewHelpTopicsMenuItem_Click(object sender, EventArgs e)
        {
            String file = Application.StartupPath + "\\Help\\silverjhelp.chm";
            if(File.Exists(file))
            {
                Process.Start(file);
            }
        }


        //**************************************************************************************************************
        //      Help->About
        //**************************************************************************************************************
        private void Help_AboutMenuItem_Click(object sender, EventArgs e)
        {
            About_Form about = new About_Form();
            about.ShowDialog();
        }



//***********************************************************************************************************************************
//                                          Tool Strip Buttons
//***********************************************************************************************************************************

        private void JavaApplicationProject_ToolStripButton_Click(object sender, EventArgs e)
        {
            File_New_JavaApplicationProjectMenuItem_Click(sender, e);
        }

        private void JavaAppletProject_ToolStripButton_Click(object sender, EventArgs e)
        {
            File_New_JavaAppletMenuItem_Click(sender, e);
        }

        private void Class_ToolStripButton_Click(object sender, EventArgs e)
        {
            File_New_ClassMenuItem_Click(sender, e);
        }

        private void OpenProject_ToolStripButton_Click(object sender, EventArgs e)
        {
            File_OpenProjectMenuItem_Click(sender, e);
        }

        private void OpenFiles_ToolStripButton_Click(object sender, EventArgs e)
        {
            File_OpenFilesMenuItem_Click(sender, e);
        }

        private void Save_ToolStripButton_Click(object sender, EventArgs e)
        {
            File_SaveMenuItem_Click(sender, e);
        }

        private void SaveAs_ToolStripButton_Click(object sender, EventArgs e)
        {
            File_SaveAsMenuItem_Click(sender, e);
        }

        private void SaveAll_ToolStripButton_Click(object sender, EventArgs e)
        {
            File_SaveAllMenuItem_Click(sender, e);
        }

        private void Cut_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_CutMenuItem_Click(sender, e);
        }

        private void Copy_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_CopyMenuItem_Click(sender, e);
        }

        private void Paste_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_PasteMenuItem_Click(sender, e);
        }

        private void Undo_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_UndoMenuItem_Click(sender, e);
        }

        private void Redo_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_RedoMenuItem_Click(sender, e);
        }

        private void ViewinWebBrowser_ToolStripButton_Click(object sender, EventArgs e)
        {
            View_ViewinWebBrowserMenuItem_Click(sender, e);
        }

        private void Compile_ToolStripButton_Click(object sender, EventArgs e)
        {
            Run_CompileMenuItem_Click(sender, e);
        }

        private void Run_ToolStripButton_Click(object sender, EventArgs e)
        {
            Run_RunApplicationMenuItem_Click(sender, e);
        }

        private void RunApplet_ToolStripButton_Click(object sender, EventArgs e)
        {
            Run_RunAppletMenuItem_Click(sender, e);
        }

        private void RunWithParameters_ToolStripButton_Click(object sender, EventArgs e)
        {
            Run_RunwithParametersMenuItem_Click(sender, e);
        }

        private void Build_ToolStripButton_Click(object sender, EventArgs e)
        {
            Run_BuildMenuItem_Click(sender, e);
        }

        private void ViewHelp_ToolStripButton_Click(object sender, EventArgs e)
        {
            Help_ViewHelpTopicsMenuItem_Click(sender, e);
        }





        //**************************************************************************************************************
        //      ClassesTreeView_NodeMouseDoubleClick
        //**************************************************************************************************************
        private void ClassesTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            String nodetext = ClassesTreeView.SelectedNode.Text;
            if (nodetext != "")
            {
                if (myTabControl.TabCount > 0)
                {
                    if (myTabControl.SelectedTab.Text != "Start Page")
                    {
                        int select_index = myTabControl.SelectedIndex;
                        var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                        RichTextBox rtb = new RichTextBox();
                        rtb.Text = texteditor.Text;

                        String[] lines = rtb.Lines;

                        for (int i = 0; i < lines.Length; i++)
                        {
                            if (lines[i].Contains(nodetext))
                            {
                                texteditor.ActiveTextAreaControl.TextArea.Caret.Line = i;
                                texteditor.ActiveTextAreaControl.TextArea.ScrollToCaret();
                            }
                        }
                    }
                }
            }
        }



        //**************************************************************************************************************
        //      ClassesTreeView_NodeMouseDoubleClick
        //**************************************************************************************************************
        private void MethodsTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            String nodetext = MethodsTreeView.SelectedNode.Text;
            if (nodetext != "")
            {
                if (myTabControl.TabCount > 0)
                {
                    if (myTabControl.SelectedTab.Text != "Start Page")
                    {
                        int select_index = myTabControl.SelectedIndex;
                        var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                        RichTextBox rtb = new RichTextBox();
                        rtb.Text = texteditor.Text;

                        String[] lines = rtb.Lines;

                        for (int i = 0; i < lines.Length; i++)
                        {
                            if (lines[i].Contains(nodetext))
                            {
                                texteditor.ActiveTextAreaControl.TextArea.Caret.Line = i;
                                texteditor.ActiveTextAreaControl.TextArea.ScrollToCaret();
                            }
                        }
                    }
                }
            }
        }



        //**************************************************************************************************************
        //      ProjectExplorerTreeView_NodeMouseDoubleClick
        //**************************************************************************************************************
        public List<String> tabpagestextlist = new List<String> { };


        private void ProjectExplorerTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (ProjectExplorerTreeView.SelectedNode != null)
            {
                String selectednodetext = ProjectExplorerTreeView.SelectedNode.ToString();
                String nodetext = selectednodetext.Substring(selectednodetext.LastIndexOf(":") + 2);
                int a = 0;
                System.Windows.Forms.TabControl.TabPageCollection tabcoll = myTabControl.TabPages;
                tabpagestextlist.Clear();

                foreach (TabPage tabpage in tabcoll)
                {
                    String tabstr = tabpage.Text;
                    if (tabstr.Contains("*"))
                    {
                        tabstr = tabstr.Remove(tabstr.Length - 1);
                    }
                    //check nodetext contains PackageFile text or not
                    if (nodetext.Contains("[Package File"))
                    {
                        nodetext = nodetext.Substring(nodetext.LastIndexOf("-") + 1);
                        nodetext = nodetext.Trim();
                    }

                    //if tabpage text == tree node text
                    if (tabstr == nodetext)
                    {
                        tabpagestextlist.Add(tabstr);
                        myTabControl.SelectedTab = tabpage;
                        myTabControl_SelectedIndexChanged(sender, e);
                    }
                    else
                    {
                        a = a + 1;
                    }
                }

                int tc = myTabControl.TabCount;
                if (a == tc - 1)
                {

                }

                else
                {
                    String projectfile = ReadCurrentProjectFileName();
                    String prjname = "";
                    List<String> mylist = new List<String> { };
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
                                        case "ProjectName":
                                            prjname = reader.ReadString();
                                            break;

                                        case "JavaClassFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "HTMLFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "CSSFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "TextFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "JavaScriptFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "SQLFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "XMLFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "NewFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "PackageJavaClassFile":
                                            mylist.Add(reader.ReadString());
                                            break;
                                    }
                                }
                            }
                        }

                        //read one file name at a time
                        foreach (String file in mylist)
                        {
                            String filename = file.Substring(file.LastIndexOf("\\") + 1);
                            if (nodetext == filename)
                            {
                                //add file name to files.slvjfile
                                RichTextBox rtb = new RichTextBox();
                                String filesexjfile = Application.StartupPath + "\\files\\files.slvjfile";
                                rtb.Text = File.ReadAllText(filesexjfile);
                                rtb.Text = rtb.Text.Insert(rtb.SelectionStart, file + "\n");

                                File.WriteAllText(filesexjfile, "");
                                StreamWriter strw = new StreamWriter(filesexjfile);
                                strw.Write(rtb.Text);
                                strw.Close();
                                strw.Dispose();

                                String checkstr = File.ReadAllText(filesexjfile);
                                if (checkstr.Contains(file))
                                {
                                    //first read text of a file
                                    String filetext = File.ReadAllText(file);

                                    //add tabpage to myTabControl
                                    MyTabPage mytabpage = new MyTabPage(this);
                                    mytabpage.Text = filename;
                                    mytabpage.textEditor.Text = filetext;
                                    mytabpage.textEditor.ContextMenuStrip = textEditorContextMenuStrip;
                                    myTabControl.TabPages.Add(mytabpage);
                                    myTabControl.SelectedTab = mytabpage;

                                    String langs = "Java";
                                    if (filename.Contains(".java"))
                                    {
                                        langs = "Java";
                                    }
                                    else if (filename.Contains(".html"))
                                    {
                                        langs = "HTML";
                                    }
                                    else if (filename.Contains(".css"))
                                    {
                                        langs = "Java";
                                    }
                                    else if (filename.Contains(".js"))
                                    {
                                        langs = "JavaScript";
                                    }
                                    else if (filename.Contains(".txt"))
                                    {
                                        langs = "Text";
                                    }
                                    else if (filename.Contains(".sql"))
                                    {
                                        langs = "SQL";
                                    }
                                    else if (filename.Contains(".xml"))
                                    {
                                        langs = "XML";
                                    }
                                    else
                                    {
                                        langs = "Text";
                                    }

                                    mytabpage.AddLanguages(langs);

                                    TabPage seltab = myTabControl.SelectedTab;
                                    if (seltab.Text.Contains("*"))
                                    {
                                        String str = seltab.Text.Remove(seltab.Text.Length - 1);
                                        seltab.Text = str;
                                    }
                                    FilenameToolStripLabel.Text = file;

                                    XmlDocument xmldoc2 = new XmlDocument();
                                    xmldoc2.Load(projectfile);
                                    XmlNode node2 = xmldoc2.CreateNode(XmlNodeType.Element, "VisualFile", null);
                                    node2.InnerText = file;
                                    xmldoc2.DocumentElement.AppendChild(node2);
                                    xmldoc2.Save(projectfile);

                                }
                                myTabControl_SelectedIndexChanged(sender, e);
                            }
                        }
                    }
                }
            }
        }


//***********************************************************************************************************************************
//                                  textEditorContextMenuStrip        
//***********************************************************************************************************************************

        private void textEditorContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {

        }

        private void textEditorContextMenu_CutMenuItem_Click(object sender, EventArgs e)
        {
            Edit_CutMenuItem_Click(sender, e);
        }

        private void textEditorContextMenu_CopyMenuItem_Click(object sender, EventArgs e)
        {
            Edit_CopyMenuItem_Click(sender, e);
        }

        private void textEditorContextMenu_PasteMenuItem_Click(object sender, EventArgs e)
        {
            Edit_PasteMenuItem_Click(sender, e);
        }

        private void textEditorContextMenu_DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControl.SelectedIndex;
                    var texteditor = (TextEditorControl)myTabControl.TabPages[select_index].Controls[0];
                    RichTextBox rtb = new RichTextBox();
                    String s = texteditor.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
                    rtb.Text = s;
                    rtb.Text = rtb.Text.Replace(s, "");
                    texteditor.ActiveTextAreaControl.TextArea.InsertString("");
                    texteditor.Document.Insert(texteditor.ActiveTextAreaControl.TextArea.Caret.Offset, rtb.Text);
                }
            }
        }

        private void textEditorContextMenu_SelectAllMenuItem_Click(object sender, EventArgs e)
        {
            Edit_SelectAllMenuItem_Click(sender, e);
        }

        private void textEditorContextMenu_UpperCaseMenuItem_Click(object sender, EventArgs e)
        {
            Edit_ChangeCase_UpperCaseMenuItem_Click(sender, e);
        }

        private void textEditorContextMenu_LowerCaseMenuItem_Click(object sender, EventArgs e)
        {
            Edit_ChangeCase_LowerCaseMenuItem_Click(sender, e);
        }

        private void textEditorContextMenu_SentenceCaseMenuItem_Click(object sender, EventArgs e)
        {
            Edit_ChangeCase_SentenceCaseMenuItem_Click(sender, e);
        }

        private void textEditorContextMenu_SingleLineCommentMenuItem_Click(object sender, EventArgs e)
        {
            Edit_CommentLine_SingleLineMenuItem_Click(sender, e);
        }

        private void textEditorContextMenu_MultiLineCommentMenuItem_Click(object sender, EventArgs e)
        {
            Edit_CommentLine_MultiLineMenuItem_Click(sender, e);
        }

        private void textEditorContextMenu_SelectionCommentMenuItem_Click(object sender, EventArgs e)
        {
            Edit_CommentLine_SelectionCommentMenuItem_Click(sender, e);
        }

        private void textEditorContextMenu_mainfunctionMenuItem_Click(object sender, EventArgs e)
        {
            Edit_Insert_mainFunctionMenuItem_Click(sender, e);
        }

        private void textEditorContextMenu_ClassMenuItem_Click(object sender, EventArgs e)
        {
            Edit_Insert_ClassMenuItem_Click(sender, e);
        }

        private void textEditorContextMenu_PackagesMenuItem_Click(object sender, EventArgs e)
        {
            Edit_Insert_PackagesMenuItem_Click(sender, e);
        }

        private void textEditorContextMenu_EventsMenuItem_Click(object sender, EventArgs e)
        {
            Edit_Insert_EventsMenuItem_Click(sender, e);
        }

        private void textEditorContextMenu_ViewinWebBrowserMenuItem_Click(object sender, EventArgs e)
        {
            View_ViewinWebBrowserMenuItem_Click(sender, e);
        }





//***********************************************************************************************************************************
//                        ProjectExplorerTreeViewContextMenuStrip  
//***********************************************************************************************************************************

        private void ProjectExplorerTreeViewContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if(this.Text=="Silver-J")
            {
                New_ProjExplorerContextMenuItem.Enabled = false;
                Open_ProjExplorerContextMenuItem.Enabled = false;
                OpenwithSystemEditor_ProjExplorerContextMenuItem.Enabled = false;
                Delete_ProjExplorerContextMenuItem.Enabled = false;
                CloseProject_ProjExplorerContextMenuItem.Enabled = false;
                RemoveAddedFiles_ProjExplorerContextMenuItem.Enabled = false;
            }
            else
            {
                New_ProjExplorerContextMenuItem.Enabled = true;
                Open_ProjExplorerContextMenuItem.Enabled = true;
                OpenwithSystemEditor_ProjExplorerContextMenuItem.Enabled = true;
                Delete_ProjExplorerContextMenuItem.Enabled = true;
                CloseProject_ProjExplorerContextMenuItem.Enabled = true;
                RemoveAddedFiles_ProjExplorerContextMenuItem.Enabled =true;
            }
        }

        private void New_Class_ProjExplorerContextMenuItem_Click(object sender, EventArgs e)
        {
            File_New_ClassMenuItem_Click(sender, e);
        }

        private void New_Package_ProjExplorerContextMenuItem_Click(object sender, EventArgs e)
        {
            File_New_PackageMenuItem_Click(sender, e);
        }

        private void New_Interface_ProjExplorerContextMenuItem_Click(object sender, EventArgs e)
        {
            File_New_InterfaceMenuItem_Click(sender, e);
        }

        private void New_Enums_ProjExplorerContextMenuItem_Click(object sender, EventArgs e)
        {
            File_New_EnumsMenuItem_Click(sender, e);
        }

        private void New_HTMLFile_ProjExplorerContextMenuItem_Click(object sender, EventArgs e)
        {
            File_New_HTMLFileMenuItem_Click(sender, e);
        }

        private void New_CSSFile_ProjExplorerContextMenuItem_Click(object sender, EventArgs e)
        {
            File_New_CSSFileMenuItem_Click(sender, e);
        }

        private void New_TextFile_ProjExplorerContextMenuItem_Click(object sender, EventArgs e)
        {
            File_New_TextFileMenuItem_Click(sender, e);
        }

        private void New_JavaScriptFile_ProjExplorerContextMenuItem_Click(object sender, EventArgs e)
        {
            File_New_JavaScriptFileMenuItem_Click(sender, e);
        }

        private void New_SQLFile_ProjExplorerContextMenuItem_Click(object sender, EventArgs e)
        {
            File_New_SQLFileMenuItem_Click(sender, e);
        }

        private void New_XMLFile_ProjExplorerContextMenuItem_Click(object sender, EventArgs e)
        {
            File_New_XMLFileMenuItem_Click(sender, e);
        }

        private void New_NewFile_ProjExplorerContextMenuItem_Click(object sender, EventArgs e)
        {
            File_New_NewFileMenuItem_Click(sender, e);
        }



        private void Open_ProjExplorerContextMenuItem_Click(object sender, EventArgs e)
        {
            if (ProjectExplorerTreeView.SelectedNode != null)
            {
                String selectednodetext = ProjectExplorerTreeView.SelectedNode.ToString();
                String nodetext = selectednodetext.Substring(selectednodetext.LastIndexOf(":") + 2);
                int a = 0;
                System.Windows.Forms.TabControl.TabPageCollection tabcoll = myTabControl.TabPages;
                tabpagestextlist.Clear();

                foreach (TabPage tabpage in tabcoll)
                {
                    String tabstr = tabpage.Text;
                    if (tabstr.Contains("*"))
                    {
                        tabstr = tabstr.Remove(tabstr.Length - 1);
                    }
                    //if tabpage text == tree node text
                    if (tabstr == nodetext)
                    {
                        tabpagestextlist.Add(tabstr);
                        myTabControl.SelectedTab = tabpage;
                        myTabControl_SelectedIndexChanged(sender, e);
                    }
                    else
                    {
                        a = a + 1;
                    }
                }

                int tc = myTabControl.TabCount;
                if (a == tc - 1)
                {

                }
                else
                {
                    String projectfile = ReadCurrentProjectFileName();
                    String prjname = "";
                    List<String> mylist = new List<String> { };
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
                                        case "ProjectName":
                                            prjname = reader.ReadString();
                                            break;

                                        case "JavaClassFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "HTMLFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "CSSFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "TextFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "JavaScriptFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "SQLFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "XMLFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "NewFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "PackageJavaClassFile":
                                            mylist.Add(reader.ReadString());
                                            break;
                                    }
                                }
                            }
                        }

                        //read one file name at a time
                        foreach (String file in mylist)
                        {
                            String filename = file.Substring(file.LastIndexOf("\\") + 1);
                            if (nodetext == filename)
                            {
                                //add file name to files.slvjfile
                                RichTextBox rtb = new RichTextBox();
                                String filesexjfile = Application.StartupPath + "\\files\\files.slvjfile";
                                rtb.Text = File.ReadAllText(filesexjfile);
                                rtb.Text = rtb.Text.Insert(rtb.SelectionStart, file + "\n");

                                File.WriteAllText(filesexjfile, "");
                                StreamWriter strw = new StreamWriter(filesexjfile);
                                strw.Write(rtb.Text);
                                strw.Close();
                                strw.Dispose();

                                String checkstr = File.ReadAllText(filesexjfile);
                                if (checkstr.Contains(file))
                                {
                                    //first read text of a file
                                    String filetext = File.ReadAllText(file);

                                    //add tabpage to myTabControl
                                    MyTabPage mytabpage = new MyTabPage(this);
                                    mytabpage.Text = filename;
                                    mytabpage.textEditor.Text = filetext;
                                    myTabControl.TabPages.Add(mytabpage);
                                    myTabControl.SelectedTab = mytabpage;

                                    TabPage seltab = myTabControl.SelectedTab;
                                    if (seltab.Text.Contains("*"))
                                    {
                                        String str = seltab.Text.Remove(seltab.Text.Length - 1);
                                        seltab.Text = str;
                                    }
                                    FilenameToolStripLabel.Text = file;
                                }
                                myTabControl_SelectedIndexChanged(sender, e);
                            }
                        }
                    }
                }
            }
        }


        private void OpenwithSystemEditor_ProjExplorerContextMenuItem_Click(object sender, EventArgs e)
        {
            if (ProjectExplorerTreeView.SelectedNode != null)
            {
                String selectednodetext = ProjectExplorerTreeView.SelectedNode.ToString();
                String nodetext = selectednodetext.Substring(selectednodetext.LastIndexOf(":") + 2);
                String projectname = getCurrentProjectName();

                if (nodetext != projectname)
                {
                    //check selected node is file name or not
                    String projectfile = ReadCurrentProjectFileName();
                    List<String> mylist = new List<String> { };
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
                                        case "JavaClassFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "HTMLFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "CSSFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "TextFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "JavaScriptFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "SQLFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "XMLFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "NewFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "PackageJavaClassFile":
                                            mylist.Add(reader.ReadString());
                                            break;

                                        case "OtherFile":
                                            mylist.Add(reader.ReadString());
                                            break;
                                    }
                                }
                            }
                        }
                        //open file
                        foreach (String file in mylist)
                        {
                            String fname = file.Substring(file.LastIndexOf("\\") + 1);

                            if (fname == nodetext)
                            {
                                Process.Start(file);
                            }
                        }
                    }
                }
            }
        }

        private void Delete_ProjExplorerContextMenuItem_Click(object sender, EventArgs e)
        {
            if (ProjectExplorerTreeView.SelectedNode != null)
            {
                String selectednodetext = ProjectExplorerTreeView.SelectedNode.ToString();
                String nodetext = selectednodetext.Substring(selectednodetext.LastIndexOf(":") + 2);

                //check selected node is project name or not
                String projectname = getCurrentProjectName();
                if (nodetext == projectname)
                {
                    DialogResult dg = MessageBox.Show("Do you want to delete current opened project ?", "Delete Project ?", MessageBoxButtons.YesNo);
                    if (dg == DialogResult.Yes)
                    {
                        File_DeleteProjectMenuItem_Click(sender, e);
                    }
                }

                //check selected node is file name or not
                String projectfile = ReadCurrentProjectFileName();
                List<String> mylist = new List<String> { };
                Boolean ispackagefile = false;

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
                                    case "JavaClassFile":
                                        mylist.Add(reader.ReadString());
                                        break;

                                    case "HTMLFile":
                                        mylist.Add(reader.ReadString());
                                        break;

                                    case "CSSFile":
                                        mylist.Add(reader.ReadString());
                                        break;

                                    case "TextFile":
                                        mylist.Add(reader.ReadString());
                                        break;

                                    case "JavaScriptFile":
                                        mylist.Add(reader.ReadString());
                                        break;

                                    case "SQLFile":
                                        mylist.Add(reader.ReadString());
                                        break;

                                    case "XMLFile":
                                        mylist.Add(reader.ReadString());
                                        break;

                                    case "NewFile":
                                        mylist.Add(reader.ReadString());
                                        break;

                                    case "PackageJavaClassFile":
                                        mylist.Add(reader.ReadString());
                                        ispackagefile = true;
                                        break;

                                    case "OtherFile":
                                        mylist.Add(reader.ReadString());
                                        break;
                                }
                            }
                        }
                    }

                    //ger each file from mylist
                    foreach (String file in mylist)
                    {
                        String fname = file.Substring(file.LastIndexOf("\\") + 1);
                        String tagname = "";

                        if (fname.Contains(".java"))
                        {
                            tagname = "JavaClassFile";
                        }
                        else if (fname.Contains(".html"))
                        {
                            tagname = "HTMLFile";
                        }
                        else if (fname.Contains(".css"))
                        {
                            tagname = "CSSFile";
                        }
                        else if (fname.Contains(".txt"))
                        {
                            tagname = "TextFile";
                        }
                        else if (fname.Contains(".js"))
                        {
                            tagname = "JavaScriptFile";
                        }
                        else if (fname.Contains(".sql"))
                        {
                            tagname = "SQLFile";
                        }
                        else if (fname.Contains(".xml"))
                        {
                            tagname = "XMLFile";
                        }
                        else
                        {
                            tagname = "NewFile";
                        }

                        if (ispackagefile == true)
                        {
                            tagname = "PackageJavaClassFile";
                        }

                        //check nodetext contains PackageFile text or not
                        if (nodetext.Contains("[Package File"))
                        {
                            nodetext = nodetext.Substring(nodetext.LastIndexOf("-") + 1);
                            nodetext = nodetext.Trim();
                        }

                        if (fname == nodetext)
                        {
                            //remove tab from my tabControl
                            System.Windows.Forms.TabControl.TabPageCollection tabcoll = myTabControl.TabPages;
                            foreach (TabPage tabpage in tabcoll)
                            {
                                if (tabpage.Text == nodetext)
                                {
                                    myTabControl.TabPages.Remove(tabpage);
                                }
                            }

                            if (File.Exists(file))
                            {
                                //delete file
                                File.Delete(file);

                                if (tagname != "")
                                {
                                    //remove filename from project file
                                    XmlDocument doc = new XmlDocument();
                                    doc.Load(projectfile);
                                    XmlNodeList nodes = doc.GetElementsByTagName(tagname);
                                    for (int i = 0; i < nodes.Count; i++)
                                    {
                                        XmlNode node = nodes[i];
                                        if (node.InnerText == file)
                                        {
                                            node.ParentNode.RemoveChild(node);
                                        }
                                    }
                                    doc.Save(projectfile);
                                }
                                AddFilesToProjectExplorerTreeView();
                                RemoveFileNamesByRemovingTabs();
                                UpdateWindowsList_WindowMenu();
                                CopyAllSourceFilesToSRCFolder();
                                myTabControl_SelectedIndexChanged(sender, e);
                            }
                        }
                    }
                }
            }
        }

        private void CloseProject_ProjExplorerContextMenuItem_Click(object sender, EventArgs e)
        {
            File_CloseProjectMenuItem_Click(sender, e);
        }

        private void RemoveAddedFiles_ProjExplorerContextMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAddedFilesToProject_Form removeaddfile = new RemoveAddedFilesToProject_Form();
            removeaddfile.ShowDialog();

            AddFilesToProjectExplorerTreeView();
        }






//***********************************************************************************************************************************
//                       myTabControlContextMenuStrip
//***********************************************************************************************************************************
        private void myTabControlContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (myTabControl.TabCount > 0)
            {
                if (myTabControl.SelectedTab.Text == "Start Page")
                {
                    myTabControlContextMenu_SaveMenuItem.Enabled = false;
                    myTabControlContextMenu_CloseAllButThisMenuItem.Enabled = false;
                    myTabControlContextMenu_CopyFullPathMenuItem.Enabled = false;
                    myTabControlContextMenu_OpenFileFolderMenuItem.Enabled = false;

                }
                else
                {
                    myTabControlContextMenu_SaveMenuItem.Enabled = true;
                    myTabControlContextMenu_CloseAllButThisMenuItem.Enabled = true;
                    myTabControlContextMenu_CopyFullPathMenuItem.Enabled = true;
                    myTabControlContextMenu_OpenFileFolderMenuItem.Enabled = true;

                    TabPage tabpage = myTabControl.SelectedTab;
                    myTabControlContextMenu_SaveMenuItem.Text = "Save  " + tabpage.Text;
                }
            }
        }

        private void myTabControlContextMenu_SaveMenuItem_Click(object sender, EventArgs e)
        {
            File_SaveMenuItem_Click(sender, e);
        }

        private void myTabControlContextMenu_CloseMenuItem_Click(object sender, EventArgs e)
        {
            File_CloseMenuItem_Click(sender, e);
        }

        private void myTabControlContextMenu_CloseAllMenuItem_Click(object sender, EventArgs e)
        {
            File_CloseAllMenuItem_Click(sender, e);
        }

        private void myTabControlContextMenu_CloseAllButThisMenuItem_Click(object sender, EventArgs e)
        {
            String tabtext = myTabControl.SelectedTab.Text;
            if(myTabControl.TabCount>1)
            {
                TabControl.TabPageCollection tabcoll = myTabControl.TabPages;
                foreach(TabPage tabpage in tabcoll)
                {
                    myTabControl.SelectedTab = tabpage;
                    if(myTabControl.SelectedTab.Text!=tabtext)
                    {
                        File_CloseMenuItem_Click(sender, e);
                    }
                }
            }
            else if(myTabControl.TabCount==1)
            {
                File_CloseMenuItem_Click(sender, e);
            }
        }

        private void myTabControlContextMenu_CopyFullPathMenuItem_Click(object sender, EventArgs e)
        {
            String filesfile = Application.StartupPath + "\\files\\files.slvjfile";
            if (File.Exists(filesfile))
            {
                RichTextBox rtb = new RichTextBox();
                rtb.Text = File.ReadAllText(filesfile);

                String seltabtext = myTabControl.SelectedTab.Text;

                if (seltabtext.Contains("*"))
                {
                    seltabtext = seltabtext.Remove(seltabtext.Length - 1);
                }

                String[] lines = rtb.Lines;

                foreach (String file in lines)
                {
                    if (file != "")
                    {
                        String str = file.Substring(file.LastIndexOf("\\") + 1);

                        if (str == seltabtext)
                        {
                            if(Clipboard.ContainsText())
                            {
                                Clipboard.SetText(file);
                            }
                            else
                            {
                                Clipboard.SetText(file);
                            }
                        }
                    }
                }
            }
        }

        private void myTabControlContextMenu_OpenFileFolderMenuItem_Click(object sender, EventArgs e)
        {
            String filesfile = Application.StartupPath + "\\files\\files.slvjfile";
            if (File.Exists(filesfile))
            {
                RichTextBox rtb = new RichTextBox();
                rtb.Text = File.ReadAllText(filesfile);

                String seltabtext = myTabControl.SelectedTab.Text;

                if (seltabtext.Contains("*"))
                {
                    seltabtext = seltabtext.Remove(seltabtext.Length - 1);
                }

                String[] lines = rtb.Lines;

                foreach (String file in lines)
                {
                    if (file != "")
                    {
                        String str = file.Substring(file.LastIndexOf("\\") + 1);

                        if (str == seltabtext)
                        {
                            String folder = file.Substring(0, file.Length - (str.Length + 1));
                            Process.Start(folder);
                        }
                    }
                }
            }
        }




 //***********************************************************************************************************************************
 //                        ErrorsListContextMenuStrip
 //***********************************************************************************************************************************
        private void ErrorList_ClearMenuItem_Click(object sender, EventArgs e)
        {
            ErrorTextBox.Text = "";   
        }

        private void ErrorList_SelectAllMenuItem_Click(object sender, EventArgs e)
        {
            ErrorTextBox.SelectAll();
        }

        private void ErrorList_CopyToClipboardMenuItem_Click(object sender, EventArgs e)
        {
            if(Clipboard.ContainsText())
            {
                Clipboard.Clear();
                Clipboard.SetText(ErrorTextBox.Text);
            }
            else
            {
                Clipboard.SetText(ErrorTextBox.Text);
            }
        }




        private void startpagepanel_newjavaappproject_Click(object sender, EventArgs e)
        {
            File_New_JavaApplicationProjectMenuItem_Click(sender, e);
        }

        private void startpagepanel_newjavaappletproject_Click(object sender, EventArgs e)
        {
            File_New_JavaAppletMenuItem_Click(sender, e);
        }

        private void startpagepanel_openproject_Click(object sender, EventArgs e)
        {
            File_OpenProjectMenuItem_Click(sender, e);
        }

        private void startpagepanel_openfiles_Click(object sender, EventArgs e)
        {
            File_OpenFilesMenuItem_Click(sender, e);
        }

        private void startpagepanel_loadsampleproject_Click(object sender, EventArgs e)
        {
            File_LoadSampleProjectMenuItem_Click(sender, e);
        }







    }
}
