#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="ToolTipControlPanel.cs" company="">
  
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
using System.Windows.Forms;
using System.Drawing;
namespace Silver_J
{
    public class ToolTipControlPanel : MyToolTipPanel
    {
        public Label tooltip = new Label();

        public String tooltiptext = "";

        public Color tooltipcolor = Color.Black;

        public ToolTipControlPanel()
        {
            this.StartColor = Color.FromArgb(250,250,250);
            this.EndColor = Color.FromArgb(230,230,230);
            this.Width = 2;
            this.Height = 2;

            this.tooltip.BackColor = Color.Transparent;
            this.tooltip.AutoSize = true;
            this.tooltip.Font = new System.Drawing.Font("Microsoft Ya Hei UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tooltip.Location = new System.Drawing.Point(5, 4);
            this.tooltip.Name = "tooltip";
            this.tooltip.Size = new System.Drawing.Size(100, 23);
            this.Controls.Add(tooltip);
        }

        public String ToolTipText
        {
            get { return tooltiptext; }
            set { tooltiptext = value; tooltip.Text = value; Invalidate(); }
        }

        public Color ToolTipColor
        {
            get { return tooltipcolor; }
            set { tooltipcolor = value; tooltip.ForeColor = value; Invalidate(); }
        }


    }
}
