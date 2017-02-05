#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="RemoveAddedFilesToProject_Form.cs" company="">
  
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
namespace Silver_J
{
    public partial class RemoveAddedFilesToProject_Form : Form
    {
        public RemoveAddedFilesToProject_Form()
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


        private void RemoveAddedFilesToProject_Form_Load(object sender, EventArgs e)
        {
            String projectfile = ReadCurrentProjectFileName();

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
                                case "OtherFile":
                                    listBox1.Items.Add(reader.ReadString());
                                    break;
                            }
                        }
                    }
                }
            }
        }



        private void RemoveButton_Click(object sender, EventArgs e)
        {
            String projectfile = ReadCurrentProjectFileName();

            if (listBox1.SelectedIndex > 0)
            {
                ListBox.SelectedObjectCollection items = listBox1.SelectedItems;
                foreach(Object item in items)
                {
                    if(File.Exists(item.ToString()))
                    {
                        File.Delete(item.ToString());

                        XmlDocument doc = new XmlDocument();
                        doc.Load(projectfile);
                        XmlNodeList nodes = doc.GetElementsByTagName("OtherFile");
                        for (int i = 0; i < nodes.Count; i++)
                        {
                            XmlNode node = nodes[i];
                            if (node.InnerText == item.ToString())
                            {
                                node.ParentNode.RemoveChild(node);
                            }
                        }
                        doc.Save(projectfile);
                    }
                }
            }

            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
