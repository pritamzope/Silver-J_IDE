#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="PanelWithBorder.cs" company="">
  
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
using System.Text;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
namespace Silver_J
{
    public class PanelWithBorder : System.Windows.Forms.Panel
    {
        private Color color1 = Color.SteelBlue;
        private Color color2 = Color.DarkBlue;
        private Color bordercolor = Color.Navy;
        private int color1Transparent = 150;
        private int color2Transparent = 150;
        private int angle = 90;
        int bdrwidth = 1;

        public Color StartColor
        {
            get { return color1; }
            set { color1 = value; Invalidate(); }
        }
        public Color EndColor
        {
            get { return color2; }
            set { color2 = value; Invalidate(); }
        }
        public int Transparent1
        {
            get { return color1Transparent; }
            set
            {
                color1Transparent = value;
                if (color1Transparent > 255)
                {
                    color1Transparent = 255;
                    Invalidate();
                }
                else
                    Invalidate();
            }
        }

        public int Transparent2
        {
            get { return color2Transparent; }
            set
            {
                color2Transparent = value;
                if (color2Transparent > 255)
                {
                    color2Transparent = 255;
                    Invalidate();
                }
                else
                    Invalidate();
            }
        }

        public int GradientAngle
        {
            get { return angle; }
            set { angle = value; Invalidate(); }
        }
        public Color BorderColor
        {
            get { return bordercolor; }
            set { bordercolor = value; Invalidate(); }
        }
        public int BorderWidth
        {
            get { return bdrwidth; }
            set { bdrwidth = value; Invalidate(); }
        }

        public PanelWithBorder()
        {
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Color c1 = Color.FromArgb(color1Transparent, color1);
            Color c2 = Color.FromArgb(color2Transparent, color2);
            Brush b = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, c1, c2, angle);
            e.Graphics.FillRectangle(b, ClientRectangle);
            Brush bdrbrush = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, bordercolor, bordercolor, angle);
            if (bdrwidth > 0)
            {
                if (bdrwidth == 1)
                {
                    e.Graphics.DrawLine(new Pen(bordercolor), 0, 0, this.Width, 0);
                    e.Graphics.DrawLine(new Pen(bordercolor), 0, this.Height - 1, this.Width, this.Height - 1);
                }
                else
                {
                    for (int i = 0; i < bdrwidth; i++)
                    {
                        e.Graphics.DrawLine(new Pen(bordercolor), 0, 0 + i, this.Width, 0 + i);
                        e.Graphics.DrawLine(new Pen(bordercolor), 0, this.Height - 1 - i, this.Width, this.Height - 1 - i);
                    }
                }
            }
            b.Dispose();
        }
    }
}
