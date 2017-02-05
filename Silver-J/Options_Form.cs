#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="Options_Form.cs" company="">
  
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
namespace Silver_J
{
    public partial class Options_Form : Form
    {
        public MainForm F;
        public MyTabControl myTabControl;
        String configfile = Application.StartupPath + "\\files\\config.slvjfile";
        public static Boolean showErrorDialog = false;
        public static String getappearance = "";

        public Options_Form(MainForm frm, MyTabControl tabctrl)
        {
            InitializeComponent();
            F = frm;
            myTabControl = tabctrl;
        }

        //**************************************************************************************************************
        //    getJDKPath()
        //**************************************************************************************************************
        public String getJDKPath()
        {
            String jdkpath = "";

            using (XmlReader reader = XmlReader.Create(configfile))
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

        //**************************************************************************************************************
        //      getWebBrowser()
        //**************************************************************************************************************
        public String getWebBrowser()
        {
            String webbrowser = "";
            using (XmlReader reader = XmlReader.Create(configfile))
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


        //**************************************************************************************************************
        //      ShowErrorDialog()
        //**************************************************************************************************************
        public Boolean ShowErrorDialog()
        {
            return showErrorDialog;
        }

            
        public String getAppearance()
        {
            return getappearance;
        }

        //**************************************************************************************************************
        //      Options Form Load
        //**************************************************************************************************************
        private void Options_Form_Load(object sender, EventArgs e)
        {
            String jdkpath = getJDKPath();
            String webbrowser = getWebBrowser();
            String appearance = "";
            String tabsalign = "";
            String showstatusstrip = "";
            String showtoolstrip = "";
            String showprojectexplorerview = "";
            String showclassesview = "";
            String showmethodsview = "";
            String showerrorlist = "";
            String showerrordialog = "";
            String showstartpageonstartup = "";
            String font = "";
            String fontsize = "";
            String showlinenumbers = "";
            String showlinehighlighter = "";
            String bracesmatching = "";
            String autocompletebraces = "";
            String showinvalidlines = "";
            String showendoflinemarkers = "";
            String showvisiblespaces = "";
            String autocompilejava = "";
            String autocompletion = "";
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

                            case "Font":
                                font = reader.ReadString();
                                break;

                            case "FontSize":
                                fontsize = reader.ReadString();
                                break;

                            case "ShowLineNumbers":
                                showlinenumbers = reader.ReadString();
                                break;

                            case "ShowLineHighlighter":
                                showlinehighlighter = reader.ReadString();
                                break;

                            case "BracesMatching":
                                bracesmatching = reader.ReadString();
                                break;

                            case "AutoCompleteBraces":
                                autocompletebraces = reader.ReadString();
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

                            case "AutoCompletion":
                                autocompletion = reader.ReadString();
                                break;
                        }
                    }
                }
            }


            //add jdk path to JDKPathTextBox
            if (jdkpath == "" || jdkpath == "null")
            {

            }
            else
            {
                JDKPathTextBox.Text = jdkpath;
            }

            //add web browser path to WebBrowserTextBox
            if (webbrowser == "" || webbrowser == "null")
            {

            }
            else
            {
                WebBrowserTextBox.Text = webbrowser;
            }

            //set appearances to Combo Box
            if(appearance!="")
            {
                comboBox1.SelectedText = appearance;
                getappearance = comboBox1.SelectedText;
            }

            //select Tabs Alignment Radio Buttons
            if (tabsalign == "") { }
            else if (tabsalign == "Top")
            {
                TopRadioButton.Checked = true;
            }
            else
            {
                BottomRadioButton.Checked = true;
            }

            //select or not Status Strip check box
            if (showstatusstrip == "true")
            {
                StatusStripCheckBox.Checked = true;
            }
            else
            {
                StatusStripCheckBox.Checked = false;
            }

            //select or not Tool Strip check box
            if (showtoolstrip == "true")
            {
                ToolStripCheckBox.Checked = true;
            }
            else
            {
                ToolStripCheckBox.Checked = false;
            }

            //select or not Project Explorer check box
            if (showprojectexplorerview == "true")
            {
                ProjectExplorerCheckBox.Checked = true;
            }
            else
            {
                ProjectExplorerCheckBox.Checked = false;
            }

            //select or not Classes View check box
            if (showclassesview == "true")
            {
                ClassesViewCheckBox.Checked = true;
            }
            else
            {
                ClassesViewCheckBox.Checked = false;
            }

            //select or not Methods View check box
            if (showmethodsview == "true")
            {
                MethodsViewCheckBox.Checked = true;
            }
            else
            {
                MethodsViewCheckBox.Checked = false;
            }

            //select or not Errors List check box
            if (showerrorlist == "true")
            {
                ErrorListCheckBox.Checked = true;
            }
            else
            {
                ErrorListCheckBox.Checked = false;
            }

            //select or not Shwo Error Dialog check box
            if (showerrordialog == "true")
            {
                ShowErrorDialogCheckBox.Checked = true;
                showErrorDialog = true;
            }
            else
            {
                ShowErrorDialogCheckBox.Checked = false;
                showErrorDialog = false;
            }

            //select or not Show StartPage check box
            if (showstartpageonstartup == "true")
            {
                ShowStartPageCheckBox.Checked = true;
            }
            else
            {
                ShowStartPageCheckBox.Checked = false;
            }

            //select font to FontListBox
            if (font != "")
            {
                FontListBox.SelectedItem = font;
            }

            //select font size to FontSizeListBox
            if (fontsize != "")
            {
                FontSizeListBox.SelectedItem = fontsize;
            }

            //select or not Line Numbers Check box
            if (showlinenumbers == "true")
            {
                LineNumbersCheckBox.Checked = true;
            }
            else
            {
                LineNumbersCheckBox.Checked = false;
            }

            //select or not Line Highlighter check box
            if (showlinehighlighter == "true")
            {
                LineHighlighterCheckBox.Checked = true;
            }
            else
            {
                LineHighlighterCheckBox.Checked = false;
            }

            //select or not Braces Matching check box
            if (bracesmatching == "true")
            {
                BracesMatchingCheckBox.Checked = true;
            }
            else
            {
                BracesMatchingCheckBox.Checked = false;
            }

            //select or not Auto Complete Braces check box
            if (autocompletebraces == "true")
            {
                AutoCompleteBracesCheckBox.Checked = true;
            }
            else
            {
                AutoCompleteBracesCheckBox.Checked = false;
            }

            //select or not Invalid Lines check box
            if (showinvalidlines == "true")
            {
                InvalidLinesCheckBox.Checked = true;
            }
            else
            {
                InvalidLinesCheckBox.Checked = false;
            }

            //select or not End Of Line Marker check box
            if (showendoflinemarkers == "true")
            {
                EndOfLineMarkerCheckBox.Checked = true;
            }
            else
            {
                EndOfLineMarkerCheckBox.Checked = false;
            }

            //select or not Visible Spaces check box
            if (showvisiblespaces == "true")
            {
                VisibleSpacesCheckBox.Checked = true;
            }
            else
            {
                VisibleSpacesCheckBox.Checked = false;
            }

            //select or not Auto Compile Program check box
            if (autocompilejava == "true")
            {
                AutoCompileJavaCheckBox.Checked = true;
            }
            else
            {
                AutoCompileJavaCheckBox.Checked = false;
            }


            //select or not Auto Completion check box
            if (autocompletion == "true")
            {
                AutoCompleteCheckBox.Checked = true;
            }
            else
            {
                AutoCompleteCheckBox.Checked = false;
            }
        }



        //**************************************************************************************************************
        //      Select JDK Button Click
        //**************************************************************************************************************
        private void SelectJDKButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog = new FolderBrowserDialog();
            if (folderbrowserdialog.ShowDialog() == DialogResult.OK)
            {
                String folderpath = folderbrowserdialog.SelectedPath;
                //check bin folder available in selected path or not
                if (folderpath.Contains("bin"))
                {
                    if (File.Exists(folderpath + "\\javac.exe") && File.Exists(folderpath + "\\java.exe"))
                    {
                        JDKPathTextBox.Text = folderpath;
                    }
                    else
                    {
                        MessageBox.Show("Invalid JDK Path............................");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid JDK Path............................");
                }
            }
        }



        //**************************************************************************************************************
        //      Select Web Browser Button Click
        //**************************************************************************************************************
        private void SelectWebBrowserButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfd = new OpenFileDialog();
            openfd.Filter = "EXE files|*.exe";
            if (openfd.ShowDialog() == DialogResult.OK)
            {
                WebBrowserTextBox.Text = openfd.FileName;
            }
        }





        //**************************************************************************************************************
        //      OK Button
        //**************************************************************************************************************
        private void OKButton_Click(object sender, EventArgs e)
        {
            //add jdk path to config file
            XmlDocument doc = new XmlDocument();
            doc.Load(configfile);
            doc.SelectSingleNode("SilverJConfiguration/JDKPath").InnerText = JDKPathTextBox.Text;
            doc.Save(configfile);


            //add web browser path to config file
            XmlDocument doc2 = new XmlDocument();
            doc2.Load(configfile);
            doc2.SelectSingleNode("SilverJConfiguration/WebBrowser").InnerText = WebBrowserTextBox.Text;
            doc2.Save(configfile);


                //add Appearance to config file
            if(comboBox1.SelectedItem!=null)
            {
                XmlDocument appdoc = new XmlDocument();
                appdoc.Load(configfile);
                appdoc.SelectSingleNode("SilverJConfiguration/Appearance").InnerText = comboBox1.SelectedItem.ToString();
                appdoc.Save(configfile);

                getappearance = comboBox1.SelectedItem.ToString();
            }

            //add tabs alignment to config file
            if (TopRadioButton.Checked == true)
            {
                F.View_TabsAlign_TopMenuItem.Checked = true;
                F.View_TabsAlign_BottomMenuItem.Checked = false;
                myTabControl.Alignment = TabAlignment.Top;

                XmlDocument doc3 = new XmlDocument();
                doc3.Load(configfile);
                doc3.SelectSingleNode("SilverJConfiguration/TabsAlignment").InnerText = "Top";
                doc3.Save(configfile);
            }
            else
            {
                F.View_TabsAlign_BottomMenuItem.Checked = true;
                F.View_TabsAlign_TopMenuItem.Checked = false;
                myTabControl.Alignment = TabAlignment.Bottom;

                XmlDocument doc3 = new XmlDocument();
                doc3.Load(configfile);
                doc3.SelectSingleNode("SilverJConfiguration/TabsAlignment").InnerText = "Bottom";
                doc3.Save(configfile);
            }


            //status strip operation
            if (StatusStripCheckBox.Checked == true)
            {
                F.View_StatusStripMenuItem.Checked = true;
                F.MyStatusStrip.Visible = true;
                //add true to config.exjfile
                XmlDocument doc4 = new XmlDocument();
                doc4.Load(configfile);
                doc4.SelectSingleNode("SilverJConfiguration/ShowStatusStrip").InnerText = "true";
                doc4.Save(configfile);
            }
            else
            {
                F.View_StatusStripMenuItem.Checked = false;
                F.MyStatusStrip.Visible = false;
                //add false to config.exjfile
                XmlDocument doc4 = new XmlDocument();
                doc4.Load(configfile);
                doc4.SelectSingleNode("SilverJConfiguration/ShowStatusStrip").InnerText = "false";
                doc4.Save(configfile);
            }


            //tool strip operation
            if (ToolStripCheckBox.Checked == true)
            {
                F.View_ToolStripMenuItem.Checked = true;
                F.toolstrippanel.Visible = true;
                F.MyToolStripZ.Visible = true;
                //add true to config.exjfile
                XmlDocument doc4 = new XmlDocument();
                doc4.Load(configfile);
                doc4.SelectSingleNode("SilverJConfiguration/ShowToolStrip").InnerText = "true";
                doc4.Save(configfile);
            }
            else
            {
                F.View_ToolStripMenuItem.Checked = false;
                F.toolstrippanel.Visible = false;
                F.MyToolStripZ.Visible = false;
                //add false to config.exjfile
                XmlDocument doc4 = new XmlDocument();
                doc4.Load(configfile);
                doc4.SelectSingleNode("SilverJConfiguration/ShowToolStrip").InnerText = "false";
                doc4.Save(configfile);
            }


            //project explorer operation
            if (ProjectExplorerCheckBox.Checked == true)
            {
                F.splitContainer3.Panel1Collapsed = false;
                F.View_ProjectExplorerMenuItem.Checked = true;
                //add true to config.exjfile
                XmlDocument doc5 = new XmlDocument();
                doc5.Load(configfile);
                doc5.SelectSingleNode("SilverJConfiguration/ShowProjectExplorer").InnerText = "true";
                doc5.Save(configfile);
            }
            else
            {
                F.splitContainer3.Panel1Collapsed = true;
                F.View_ProjectExplorerMenuItem.Checked = false;
                //add false to config.exjfile
                XmlDocument doc5 = new XmlDocument();
                doc5.Load(configfile);
                doc5.SelectSingleNode("SilverJConfiguration/ShowProjectExplorer").InnerText = "false";
                doc5.Save(configfile);
            }


            //classes view operation
            if (ClassesViewCheckBox.Checked == true)
            {
                F.splitContainer4.Panel1Collapsed = false;
                F.View_ClassesViewMenuItem.Checked = true;
                F.splitContainer1.SplitterDistance = 1100;
                F.splitContainer1.IsSplitterFixed = false;
                F.splitContainer1.Panel2Collapsed = false;

                //add true to config.exjfile
                XmlDocument doc6 = new XmlDocument();
                doc6.Load(configfile);
                doc6.SelectSingleNode("SilverJConfiguration/ShowClassesView").InnerText = "true";
                doc6.Save(configfile);

                if (MethodsViewCheckBox.Checked == true)
                {
                    F.splitContainer1.SplitterDistance = 1100;
                    F.splitContainer1.IsSplitterFixed = false;
                }
                else if (MethodsViewCheckBox.Checked == false)
                {
                    F.splitContainer4.Panel2Collapsed = true;
                }
            }

            else if (ClassesViewCheckBox.Checked == false)
            {
                F.splitContainer4.Panel1Collapsed = true;
                F.View_ClassesViewMenuItem.Checked = false;

                //add false to config.exjfile
                XmlDocument doc6 = new XmlDocument();
                doc6.Load(configfile);
                doc6.SelectSingleNode("SilverJConfiguration/ShowClassesView").InnerText = "false";
                doc6.Save(configfile);

                if (MethodsViewCheckBox.Checked == false)
                {
                    F.splitContainer1.Panel2Collapsed = true;
                }
            }


            //methods view operation
            if (MethodsViewCheckBox.Checked == true)
            {
                F.splitContainer4.Panel2Collapsed = false;
                F.View_MethodsViewMenuItem.Checked = true;
                F.splitContainer1.SplitterDistance = 1100;
                F.splitContainer1.IsSplitterFixed = false;
                F.splitContainer1.Panel2Collapsed = false;

                //add true to config.exjfile
                XmlDocument doc7 = new XmlDocument();
                doc7.Load(configfile);
                doc7.SelectSingleNode("SilverJConfiguration/ShowMethodsView").InnerText = "true";
                doc7.Save(configfile);

                if (ClassesViewCheckBox.Checked == true)
                {
                    F.splitContainer1.SplitterDistance = 1100;
                    F.splitContainer1.IsSplitterFixed = false;
                }
            }

            else if (MethodsViewCheckBox.Checked == false)
            {
                F.splitContainer4.Panel2Collapsed = true;
                F.View_MethodsViewMenuItem.Checked = false;

                //add false to config.exjfile
                XmlDocument doc7 = new XmlDocument();
                doc7.Load(configfile);
                doc7.SelectSingleNode("SilverJConfiguration/ShowMethodsView").InnerText = "false";
                doc7.Save(configfile);

                if (ClassesViewCheckBox.Checked == false)
                {
                    F.splitContainer1.Panel2Collapsed = true;
                }
            }


            //errors list operation
            if (ErrorListCheckBox.Checked == true)
            {
                F.splitContainer2.Panel2Collapsed = false;
                F.View_ErrorsListMenuItem.Checked = true;

                //add true to config.exjfile
                XmlDocument doc8 = new XmlDocument();
                doc8.Load(configfile);
                doc8.SelectSingleNode("SilverJConfiguration/ShowErrorList").InnerText = "true";
                doc8.Save(configfile);
            }
            else if (ErrorListCheckBox.Checked == false)
            {
                F.splitContainer2.Panel2Collapsed = true;
                F.View_ErrorsListMenuItem.Checked = false;

                //add false to config.exjfile
                XmlDocument doc8 = new XmlDocument();
                doc8.Load(configfile);
                doc8.SelectSingleNode("SilverJConfiguration/ShowErrorList").InnerText = "false";
                doc8.Save(configfile);
            }

            //show error dialog operation
            if (ShowErrorDialogCheckBox.Checked == true)
            {
                showErrorDialog = true;
                F.View_ShowErrorDialogMenuItem.Checked = true;

                //add true to config.exjfile
                XmlDocument doc9 = new XmlDocument();
                doc9.Load(configfile);
                doc9.SelectSingleNode("SilverJConfiguration/ShowErrorDialog").InnerText = "true";
                doc9.Save(configfile);
            }
            else if (ShowErrorDialogCheckBox.Checked == false)
            {
                showErrorDialog = true;
                F.View_ShowErrorDialogMenuItem.Checked = false;

                //add false to config.exjfile
                XmlDocument doc9 = new XmlDocument();
                doc9.Load(configfile);
                doc9.SelectSingleNode("SilverJConfiguration/ShowErrorDialog").InnerText = "false";
                doc9.Save(configfile);
            }


            //show error start page on startup operation
            if (ShowStartPageCheckBox.Checked == true)
            {
                //add true to config.exjfile
                XmlDocument doc876 = new XmlDocument();
                doc876.Load(configfile);
                doc876.SelectSingleNode("SilverJConfiguration/ShowStartPageOnStartUp").InnerText = "true";
                doc876.Save(configfile);
            }
            else if (ShowStartPageCheckBox.Checked == false)
            {
                //add false to config.exjfile
                XmlDocument doc876 = new XmlDocument();
                doc876.Load(configfile);
                doc876.SelectSingleNode("SilverJConfiguration/ShowStartPageOnStartUp").InnerText = "false";
                doc876.Save(configfile);
            }




            //set font operation
            String font = FontListBox.SelectedItem.ToString();
            String fontsize = FontSizeListBox.SelectedItem.ToString();
            XmlDocument docfnt10 = new XmlDocument();
            docfnt10.Load(configfile);
            docfnt10.SelectSingleNode("SilverJConfiguration/Font").InnerText = font;
            docfnt10.Save(configfile);

            XmlDocument docfntsize10 = new XmlDocument();
            docfntsize10.Load(configfile);
            docfntsize10.SelectSingleNode("SilverJConfiguration/FontSize").InnerText = fontsize;
            docfntsize10.Save(configfile);


            //line numbers operation
            if (LineNumbersCheckBox.Checked == true)
            {
                XmlDocument doc11 = new XmlDocument();
                doc11.Load(configfile);
                doc11.SelectSingleNode("SilverJConfiguration/ShowLineNumbers").InnerText = "true";
                doc11.Save(configfile);
            }
            else
            {
                XmlDocument doc11 = new XmlDocument();
                doc11.Load(configfile);
                doc11.SelectSingleNode("SilverJConfiguration/ShowLineNumbers").InnerText = "false";
                doc11.Save(configfile);
            }

            //line highlighter operation
            if (LineHighlighterCheckBox.Checked == true)
            {
                XmlDocument doc12 = new XmlDocument();
                doc12.Load(configfile);
                doc12.SelectSingleNode("SilverJConfiguration/ShowLineHighlighter").InnerText = "true";
                doc12.Save(configfile);
            }
            else
            {
                XmlDocument doc12 = new XmlDocument();
                doc12.Load(configfile);
                doc12.SelectSingleNode("SilverJConfiguration/ShowLineHighlighter").InnerText = "false";
                doc12.Save(configfile);
            }

            //braces matching operation
            if (BracesMatchingCheckBox.Checked == true)
            {
                XmlDocument doc13 = new XmlDocument();
                doc13.Load(configfile);
                doc13.SelectSingleNode("SilverJConfiguration/BracesMatching").InnerText = "true";
                doc13.Save(configfile);
            }
            else
            {
                XmlDocument doc13 = new XmlDocument();
                doc13.Load(configfile);
                doc13.SelectSingleNode("SilverJConfiguration/BracesMatching").InnerText = "false";
                doc13.Save(configfile);
            }

            //auto complete braces operation
            if (AutoCompleteBracesCheckBox.Checked == true)
            {
                XmlDocument doc14 = new XmlDocument();
                doc14.Load(configfile);
                doc14.SelectSingleNode("SilverJConfiguration/AutoCompleteBraces").InnerText = "true";
                doc14.Save(configfile);
            }
            else
            {
                XmlDocument doc14 = new XmlDocument();
                doc14.Load(configfile);
                doc14.SelectSingleNode("SilverJConfiguration/AutoCompleteBraces").InnerText = "false";
                doc14.Save(configfile);
            }

            //invalid lines operation
            if (InvalidLinesCheckBox.Checked == true)
            {
                XmlDocument doc15 = new XmlDocument();
                doc15.Load(configfile);
                doc15.SelectSingleNode("SilverJConfiguration/ShowInvalidLines").InnerText = "true";
                doc15.Save(configfile);
            }
            else
            {
                XmlDocument doc15 = new XmlDocument();
                doc15.Load(configfile);
                doc15.SelectSingleNode("SilverJConfiguration/ShowInvalidLines").InnerText = "false";
                doc15.Save(configfile);
            }

            //end of line marker operation
            if (EndOfLineMarkerCheckBox.Checked == true)
            {
                XmlDocument doc16 = new XmlDocument();
                doc16.Load(configfile);
                doc16.SelectSingleNode("SilverJConfiguration/ShowEndOfLineMarker").InnerText = "true";
                doc16.Save(configfile);
            }
            else
            {
                XmlDocument doc16 = new XmlDocument();
                doc16.Load(configfile);
                doc16.SelectSingleNode("SilverJConfiguration/ShowEndOfLineMarker").InnerText = "false";
                doc16.Save(configfile);
            }

            //visible spaces operation
            if (VisibleSpacesCheckBox.Checked == true)
            {
                XmlDocument doc17 = new XmlDocument();
                doc17.Load(configfile);
                doc17.SelectSingleNode("SilverJConfiguration/ShowVisibleSpaces").InnerText = "true";
                doc17.Save(configfile);
            }
            else
            {
                XmlDocument doc17 = new XmlDocument();
                doc17.Load(configfile);
                doc17.SelectSingleNode("SilverJConfiguration/ShowVisibleSpaces").InnerText = "false";
                doc17.Save(configfile);
            }

            //auto compile program operation
            if (AutoCompileJavaCheckBox.Checked == true)
            {
                XmlDocument doc18 = new XmlDocument();
                doc18.Load(configfile);
                doc18.SelectSingleNode("SilverJConfiguration/AutoCompileJava").InnerText = "true";
                doc18.Save(configfile);
            }
            else
            {
                XmlDocument doc18 = new XmlDocument();
                doc18.Load(configfile);
                doc18.SelectSingleNode("SilverJConfiguration/AutoCompileJava").InnerText = "false";
                doc18.Save(configfile);
            }

            
            //auto completion operation
            if (AutoCompleteCheckBox.Checked == true)
            {
                XmlDocument doc18 = new XmlDocument();
                doc18.Load(configfile);
                doc18.SelectSingleNode("SilverJConfiguration/AutoCompletion").InnerText = "true";
                doc18.Save(configfile);
            }
            else
            {
                XmlDocument doc18 = new XmlDocument();
                doc18.Load(configfile);
                doc18.SelectSingleNode("SilverJConfiguration/AutoCompletion").InnerText = "false";
                doc18.Save(configfile);
            }


            this.Close();
        }


        //**************************************************************************************************************
        //     Cancel
        //**************************************************************************************************************
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
