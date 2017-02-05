#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="OpenProjectDialog.cs" company="">
  
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

namespace Silver_J
{
    public partial class OpenProjectDialog : Form
    {
       public static Boolean isfinished = false;
        public OpenProjectDialog()
        {
            InitializeComponent();
        }

        public Boolean IsFinished()
        {
            return isfinished;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            isfinished = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isfinished = false;
            this.Close();
        }
    }
}
