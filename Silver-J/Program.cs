#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="Program.cs" company="">
  
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
using System.Windows.Forms;
using System.IO;
using System.Threading;
namespace Silver_J
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            if (args != null && args.Length > 0)
            {
                if (args.Length == 1)
                {
                    String file = args[0];
                    //when filename extension is .slvjproj then call OpenProject function
                    //if file is other type then call OpenFiles_FromCMD
                    if (Path.GetExtension(file) == ".slvjproj")
                    {
                        Application.Run(new Start_Form());
                        MainForm mf = new MainForm();
                        mf.OpenProject(file);
                        Application.EnableVisualStyles();
                        Application.Run(mf);
                    }
                    else
                    {
                        Application.Run(new Start_Form());
                        MainForm mf = new MainForm();
                        mf.OpenFiles_FromCMD(args);
                        Application.EnableVisualStyles();
                        Application.Run(mf);
                    }
                }
                else
                {
                    Application.Run(new Start_Form());
                    MainForm mf = new MainForm();
                    mf.OpenFiles_FromCMD(args);
                    Application.EnableVisualStyles();
                    Application.Run(mf);

                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Start_Form());
                Application.Run(new MainForm());
            }
        }
    }
}
