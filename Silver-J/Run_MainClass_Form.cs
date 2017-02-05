#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="Run_MainClass_Form.cs" company="">
  
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
using System.IO;
namespace Silver_J
{
    public partial class Run_MainClass_Form : Form
    {
        public Run_MainClass_Form()
        {
            InitializeComponent();
        }

        String defaultprojfilepath = Application.StartupPath + "\\files\\defaultprojloc.slvjfile";


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

        public List<String> getAllJavaFiles_AddToListBox()
        {
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
                            }
                        }
                    }
                }
            }
            return mylist;
        }



        private void Run_MainClass_Form_Load(object sender, EventArgs e)
        {
            List<String> javafiles = getAllJavaFiles_AddToListBox();
            String mainclass = getMainClassFileName();
            String mnclass = mainclass.Substring(mainclass.LastIndexOf("\\") + 1);
            foreach (String file in javafiles)
            {
                String str = file.Substring(file.LastIndexOf("\\") + 1);
                listBox1.Items.Add(str);
                if (str == mnclass)
                {
                    listBox1.SelectedItem = str;
                }
            }
        }



        private void OKButton_Click(object sender, EventArgs e)
        {
            String selecteditem = listBox1.SelectedItem.ToString();
            if (selecteditem != "")
            {
                List<String> javafiles = getAllJavaFiles_AddToListBox();
                foreach (String file in javafiles)
                {
                    if (file.Contains(selecteditem))
                    {
                        String projectfilename = ReadCurrentProjectFileName();
                        XmlDocument doc = new XmlDocument();
                        doc.Load(projectfilename);
                        doc.SelectSingleNode("SilverJProject/MainClassFile").InnerText = file;
                        doc.Save(projectfilename);

                        this.Close();
                    }
                }
            }
        }



        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
