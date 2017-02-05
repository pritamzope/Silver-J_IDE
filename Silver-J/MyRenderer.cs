#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="MyRenderer.cs" company="">
  
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
using System.Drawing.Drawing2D;
namespace Silver_J
{
    class MyRenderer
    {
    }

    //***************************************************************************************************************************
    //               DefaultMenuRenderer
    //***************************************************************************************************************************
    public class DefaultMenuRenderer : ToolStripRenderer
    {
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderMenuItemBackground(e);

            if (e.Item.Enabled)
            {
                if (e.Item.IsOnDropDown == false && e.Item.Selected)
                {
                    var rect = new Rectangle(2, 1, e.Item.Width - 5, e.Item.Height - 3);
                    var rect2 = new Rectangle(2, 1, e.Item.Width - 5, e.Item.Height - 3);
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 200, 220, 250)), rect);
                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(50, 140, 180))), rect2);
                    e.Item.ForeColor = Color.Black;
                }
                else if (e.Item.IsOnDropDown && e.Item.Selected)
                {
                    var rect = new Rectangle(2, 0, e.Item.Width - 5, e.Item.Height - 1);
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, 220, 250)), rect);
                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(50, 140, 180))), rect);
                    e.Item.ForeColor = Color.Black;
                }
                else
                {
                    e.Item.ForeColor = Color.Black;
                }
                if ((e.Item as ToolStripMenuItem).DropDown.Visible && e.Item.IsOnDropDown == false)
                {
                    var rect = new Rectangle(2, 1, e.Item.Width - 5, e.Item.Height - 3);
                    var rect2 = new Rectangle(2, 1, e.Item.Width - 5, e.Item.Height - 3);
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 250)), rect);
                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(100, 190, 230))), rect2);
                    e.Item.ForeColor = Color.Black;
                }
            }
        }
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            base.OnRenderSeparator(e);
            var DarkLine = new SolidBrush(Color.FromArgb(160, 160, 160));
            var rect = new Rectangle(30, 3, e.Item.Width - 32, 1);
            e.Graphics.FillRectangle(DarkLine, rect);
        }


        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemCheck(e);

            if (e.Item.Selected)
            {
                var rect = new Rectangle(4, 2, 18, 18);
                var rect2 = new Rectangle(5, 3, 16, 16);
                SolidBrush b = new SolidBrush(Color.Orange);
                SolidBrush b2 = new SolidBrush(Color.FromArgb(255, 230, 240, 240));

                e.Graphics.FillRectangle(b, rect);
                e.Graphics.FillRectangle(b2, rect2);
                e.Graphics.DrawImage(e.Image, new Point(5, 3));
            }
            else
            {
                var rect = new Rectangle(4, 2, 18, 18);
                var rect2 = new Rectangle(5, 3, 16, 16);
                SolidBrush b = new SolidBrush(Color.Blue);
                SolidBrush b2 = new SolidBrush(Color.FromArgb(255, 240, 250, 250));

                e.Graphics.FillRectangle(b, rect);
                e.Graphics.FillRectangle(b2, rect2);
                e.Graphics.DrawImage(e.Image, new Point(5, 3));
            }
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            base.OnRenderImageMargin(e);

            var rect = new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(252, 252, 255)), rect);

            var DarkLine = new SolidBrush(Color.FromArgb(250, 250, 250));
            var rect3 = new Rectangle(0, 0, 26, e.AffectedBounds.Height);
            e.Graphics.FillRectangle(DarkLine, rect3);

            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.FromArgb(200, 200, 200))), 28, 0, 28, e.AffectedBounds.Height);

            var rect2 = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(80, 170, 200))), rect2);
        }
    }


    //***************************************************************************************************************************
    //               DarkMenuRenderer
    //***************************************************************************************************************************
    public class DarkMenuRenderer : ToolStripRenderer
    {
        public static void DrawRoundedRectangle(Graphics g, int x, int y, int width, int height, int m_diameter, Color color)
        {
            using (Pen pen = new Pen(color))
            {
                //Dim g As Graphics
                var BaseRect = new RectangleF(x, y, width, height);
                var ArcRect = new RectangleF(BaseRect.Location, new SizeF(m_diameter, m_diameter));
                //top left Arc
                g.DrawArc(pen, ArcRect, 180, 90);
                g.DrawLine(pen, x + Convert.ToInt32(m_diameter / 2), y, x + width - Convert.ToInt32(m_diameter / 2), y);

                // top right arc
                ArcRect.X = BaseRect.Right - m_diameter;
                g.DrawArc(pen, ArcRect, 270, 90);
                g.DrawLine(pen, x + width, y + Convert.ToInt32(m_diameter / 2), x + width, y + height - Convert.ToInt32(m_diameter / 2));

                // bottom right arc
                ArcRect.Y = BaseRect.Bottom - m_diameter;
                g.DrawArc(pen, ArcRect, 0, 90);
                g.DrawLine(pen, x + Convert.ToInt32(m_diameter / 2), y + height, x + width - Convert.ToInt32(m_diameter / 2), y + height);

                // bottom left arc
                ArcRect.X = BaseRect.Left;
                g.DrawArc(pen, ArcRect, 90, 90);
                g.DrawLine(pen, x, y + Convert.ToInt32(m_diameter / 2), x, y + height - Convert.ToInt32(m_diameter / 2));
            }
        }
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {

            base.OnRenderMenuItemBackground(e);
            if (e.Item.Enabled)
            {
                if (e.Item.IsOnDropDown == false && e.Item.Selected)
                {
                    var rect = new Rectangle(2, 1, e.Item.Width - 5, e.Item.Height - 2);
                    Brush b2 = new System.Drawing.Drawing2D.LinearGradientBrush(e.Item.ContentRectangle, Color.FromArgb(255, 255, 255, 180), Color.FromArgb(255, 255, 234, 130), 90);
                    e.Graphics.FillRectangle(b2, rect);
                    DrawRoundedRectangle(e.Graphics, rect.Left - 1, rect.Top - 1, rect.Width, rect.Height + 1, 4, Color.Orange);
                    //DrawRoundedRectangle(e.Graphics, rect.Left - 2, rect.Top - 2, rect.Width, rect.Height+1, 4, Color.White);
                    e.Item.ForeColor = Color.Black;
                }
                else if (e.Item.IsOnDropDown && e.Item.Selected)
                {
                    var rect = new Rectangle(2, 1, e.Item.Width - 4, e.Item.Height - 2);
                    Brush b2 = new System.Drawing.Drawing2D.LinearGradientBrush(e.Item.ContentRectangle, Color.FromArgb(255, 255, 255, 180), Color.FromArgb(255, 255, 234, 130), 90);
                    e.Graphics.FillRectangle(b2, rect);
                    DrawRoundedRectangle(e.Graphics, rect.Left - 1, rect.Top - 1, rect.Width, rect.Height + 1, 4, Color.Orange);
                    e.Item.ForeColor = Color.Black;
                }
                else
                {
                    e.Item.ForeColor = Color.Black;
                }
                if ((e.Item as ToolStripMenuItem).DropDown.Visible && e.Item.IsOnDropDown == false)
                {
                    var rect = new Rectangle(2, 1, e.Item.Width - 4, e.Item.Height - 2);
                    var rect2 = new Rectangle(2, 1, e.Item.Width - 4, e.Item.Height - 2);
                    Brush b2 = new System.Drawing.Drawing2D.LinearGradientBrush(e.Item.ContentRectangle, Color.FromArgb(255, 255, 255, 180), Color.FromArgb(255, 255, 234, 130), 90);
                    e.Graphics.FillRectangle(b2, rect);
                    e.Graphics.DrawRectangle(new Pen(Brushes.Orange), rect2);
                    e.Item.ForeColor = Color.Black;
                }
            }
        }
        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemCheck(e);

            if (e.Item.Selected)
            {
                var rect = new Rectangle(3, 1, 20, 20);
                var rect2 = new Rectangle(4, 2, 18, 18);
                SolidBrush b = new SolidBrush(Color.Orange);
                SolidBrush b2 = new SolidBrush(Color.FromArgb(255, 220, 230, 230));

                e.Graphics.FillRectangle(b, rect);
                e.Graphics.FillRectangle(b2, rect2);
                e.Graphics.DrawImage(e.Image, new Point(5, 3));
            }
            else
            {
                var rect = new Rectangle(3, 1, 20, 20);
                var rect2 = new Rectangle(4, 2, 18, 18);
                SolidBrush b = new SolidBrush(Color.Blue);
                SolidBrush b2 = new SolidBrush(Color.FromArgb(255, 240, 250, 250));

                e.Graphics.FillRectangle(b, rect);
                e.Graphics.FillRectangle(b2, rect2);
                e.Graphics.DrawImage(e.Image, new Point(5, 3));
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            base.OnRenderSeparator(e);
            var DarkLine = new SolidBrush(Color.FromArgb(160, 160, 160));
            var WhiteLine = new SolidBrush(Color.FromArgb(160, 160, 160));
            var rect = new Rectangle(30, 3, e.Item.Width - 32, 1);
            e.Graphics.FillRectangle(DarkLine, rect);
            e.Graphics.FillRectangle(WhiteLine, rect);
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            base.OnRenderImageMargin(e);

            var rect = new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 190, 210, 250)), rect);

            var DarkLine = new SolidBrush(Color.FromArgb(255, 240, 240, 250));
            var rect2 = new Rectangle(1, 2, 24, e.AffectedBounds.Height);
            e.Graphics.FillRectangle(DarkLine, rect2);

            var rect3 = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
            e.Graphics.DrawRectangle(new Pen(Brushes.DarkGray), rect3);
        }
    }


    //***************************************************************************************************************************
    //                          LightMenuRenderer
    //***************************************************************************************************************************
    public class LightMenuRenderer : ToolStripRenderer
    {
        public static void DrawRoundedRectangle(Graphics g, int x, int y, int width, int height, int m_diameter, Color color)
        {
            using (Pen pen = new Pen(color))
            {
                //Dim g As Graphics
                var BaseRect = new RectangleF(x, y, width, height);
                var ArcRect = new RectangleF(BaseRect.Location, new SizeF(m_diameter, m_diameter));
                //top left Arc
                g.DrawArc(pen, ArcRect, 180, 90);
                g.DrawLine(pen, x + Convert.ToInt32(m_diameter / 2), y, x + width - Convert.ToInt32(m_diameter / 2), y);

                // top right arc
                ArcRect.X = BaseRect.Right - m_diameter;
                g.DrawArc(pen, ArcRect, 270, 90);
                g.DrawLine(pen, x + width, y + Convert.ToInt32(m_diameter / 2), x + width, y + height - Convert.ToInt32(m_diameter / 2));

                // bottom right arc
                ArcRect.Y = BaseRect.Bottom - m_diameter;
                g.DrawArc(pen, ArcRect, 0, 90);
                g.DrawLine(pen, x + Convert.ToInt32(m_diameter / 2), y + height, x + width - Convert.ToInt32(m_diameter / 2), y + height);

                // bottom left arc
                ArcRect.X = BaseRect.Left;
                g.DrawArc(pen, ArcRect, 90, 90);
                g.DrawLine(pen, x, y + Convert.ToInt32(m_diameter / 2), x, y + height - Convert.ToInt32(m_diameter / 2));
            }
        }
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {

            base.OnRenderMenuItemBackground(e);
            if (e.Item.Enabled)
            {
                if (e.Item.IsOnDropDown == false && e.Item.Selected)
                {
                    var rect = new Rectangle(2, 2, e.Item.Width - 5, e.Item.Height);
                    Brush b2 = new System.Drawing.Drawing2D.LinearGradientBrush(e.Item.ContentRectangle, Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 172, 236, 255), 90);
                    e.Graphics.FillRectangle(b2, rect);
                    DrawRoundedRectangle(e.Graphics, rect.Left - 1, rect.Top - 1, rect.Width + 1, rect.Height - 3, 2, Color.FromArgb(136, 190, 230));
                    e.Item.ForeColor = Color.Black;
                }
                else if (e.Item.IsOnDropDown && e.Item.Selected)
                {
                    var rect = new Rectangle(3, 1, e.Item.Width - 4, e.Item.Height - 2);
                    Brush b2 = new System.Drawing.Drawing2D.LinearGradientBrush(e.Item.ContentRectangle, Color.FromArgb(255,255,255,255), Color.FromArgb(255, 172, 236, 255), 90);
                    e.Graphics.FillRectangle(b2, rect);
                    DrawRoundedRectangle(e.Graphics, rect.Left - 1, rect.Top - 1, rect.Width, rect.Height + 1, 4, Color.FromArgb(136, 190, 230));
                    e.Item.ForeColor = Color.Black;
                }
                else
                {
                    e.Item.ForeColor = Color.Black;
                }
                if ((e.Item as ToolStripMenuItem).DropDown.Visible && e.Item.IsOnDropDown == false)
                {
                    var rect = new Rectangle(2, 1, e.Item.Width - 4, e.Item.Height - 2);
                    var rect2 = new Rectangle(2, 1, e.Item.Width - 4, e.Item.Height - 2);
                    Brush b2 = new System.Drawing.Drawing2D.LinearGradientBrush(e.Item.ContentRectangle, Color.FromArgb(252, 252, 252, 252), Color.FromArgb(255, 172, 236, 255), 90);
                    e.Graphics.FillRectangle(b2, rect);
                    DrawRoundedRectangle(e.Graphics, rect.Left - 1, rect.Top - 1, rect.Width, rect.Height + 1, 4, Color.FromArgb(136, 190, 230));
                    e.Item.ForeColor = Color.Black;
                }
            }
        }
        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemCheck(e);

            if (e.Item.Selected)
            {
                var rect = new Rectangle(3, 1, 20, 20);
                var rect2 = new Rectangle(4, 2, 18, 18);
                SolidBrush b = new SolidBrush(Color.Orange);
                SolidBrush b2 = new SolidBrush(Color.FromArgb(255, 220, 230, 230));

                e.Graphics.FillRectangle(b, rect);
                e.Graphics.FillRectangle(b2, rect2);
                e.Graphics.DrawImage(e.Image, new Point(5, 3));
            }
            else
            {
                var rect = new Rectangle(3, 1, 20, 20);
                var rect2 = new Rectangle(4, 2, 18, 18);
                SolidBrush b = new SolidBrush(Color.Blue);
                SolidBrush b2 = new SolidBrush(Color.FromArgb(255, 240, 250, 250));

                e.Graphics.FillRectangle(b, rect);
                e.Graphics.FillRectangle(b2, rect2);
                e.Graphics.DrawImage(e.Image, new Point(5, 3));
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            base.OnRenderSeparator(e);
            var DarkLine = new SolidBrush(Color.FromArgb(200, 200, 200));
            var WhiteLine = new SolidBrush(Color.FromArgb(200, 200, 200));
            var rect = new Rectangle(30, 3, e.Item.Width - 32, 1);
            e.Graphics.FillRectangle(DarkLine, rect);
            e.Graphics.FillRectangle(WhiteLine, rect);
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            base.OnRenderImageMargin(e);

            var rect = new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255, 255)), rect);

            var DarkLine = new SolidBrush(Color.FromArgb(255, 240, 250, 255));
            var rect2 = new Rectangle(1, 2, 24, e.AffectedBounds.Height);
            e.Graphics.FillRectangle(DarkLine, rect2);

            var rect3 = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
            e.Graphics.DrawRectangle(new Pen(Brushes.DarkGray), rect3);
        }
    }


    //***************************************************************************************************************************
    //                          NightMenuRenderer
    //***************************************************************************************************************************
    public class NightMenuRenderer : ToolStripRenderer
    {
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderMenuItemBackground(e);

            if (e.Item.Enabled)
            {
                if (e.Item.IsOnDropDown == false && e.Item.Selected)
                {
                    var rect = new Rectangle(2, 1, e.Item.Width - 5, e.Item.Height - 3);
                    var rect2 = new Rectangle(2, 1, e.Item.Width - 5, e.Item.Height - 3);
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 60, 60)), rect);
                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), rect2);
                    e.Item.ForeColor = Color.White;
                }
                else
                {
                    e.Item.ForeColor=Color.White;
                }

               if (e.Item.IsOnDropDown && e.Item.Selected)
                {
                    var rect = new Rectangle(2, 0, e.Item.Width - 5, e.Item.Height - 1);
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 60, 60)), rect);
                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), rect);
                    e.Item.ForeColor = Color.White;
                }
                if ((e.Item as ToolStripMenuItem).DropDown.Visible && e.Item.IsOnDropDown == false)
                {
                    var rect = new Rectangle(2, 1, e.Item.Width - 5, e.Item.Height - 3);
                    var rect2 = new Rectangle(2, 1, e.Item.Width - 5, e.Item.Height - 3);
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(20, 20, 20)), rect);
                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), rect2);
                    e.Item.ForeColor = Color.White;
                }
            }
        }
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            base.OnRenderSeparator(e);
            var DarkLine = new SolidBrush(Color.FromArgb(30, 30, 30));
            var rect = new Rectangle(30, 3, e.Item.Width - 32, 1);
            e.Graphics.FillRectangle(DarkLine, rect);
        }


        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemCheck(e);

            if (e.Item.Selected)
            {
                var rect = new Rectangle(4, 2, 18, 18);
                var rect2 = new Rectangle(5, 3, 16, 16);
                SolidBrush b = new SolidBrush(Color.Black);
                SolidBrush b2 = new SolidBrush(Color.FromArgb(220, 220, 220));

                e.Graphics.FillRectangle(b, rect);
                e.Graphics.FillRectangle(b2, rect2);
                e.Graphics.DrawImage(e.Image, new Point(5, 3));
            }
            else
            {
                var rect = new Rectangle(4, 2, 18, 18);
                var rect2 = new Rectangle(5, 3, 16, 16);
                SolidBrush b = new SolidBrush(Color.White);
                SolidBrush b2 = new SolidBrush(Color.FromArgb(255, 80, 90, 90));

                e.Graphics.FillRectangle(b, rect);
                e.Graphics.FillRectangle(b2, rect2);
                e.Graphics.DrawImage(e.Image, new Point(5, 3));
            }
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            base.OnRenderImageMargin(e);

            var rect = new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(20, 20, 20)), rect);

            var DarkLine = new SolidBrush(Color.FromArgb(20, 20, 20));
            var rect3 = new Rectangle(0, 0, 26, e.AffectedBounds.Height);
            e.Graphics.FillRectangle(DarkLine, rect3);

            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.FromArgb(20, 20, 20))), 28, 0, 28, e.AffectedBounds.Height);

            var rect2 = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), rect2);
        }
    }


    //***************************************************************************************************************************
    //    //Tool Strip Renderer Classes
    //***************************************************************************************************************************


    //***************************************************************************************************************************
    //                      DefaultToolStripRenderer
    //***************************************************************************************************************************
    public class DefaultToolStripRenderer : ToolStripProfessionalRenderer
    {

        // Render button selected and pressed state
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderButtonBackground(e);
            var rectBorder = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
            var rect = new Rectangle(1, 1, e.Item.Width - 2, e.Item.Height - 2);

            if (e.Item.Selected == true || (e.Item as ToolStripButton).Checked)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, 220, 250)), rect);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(50, 140, 180))), rectBorder);
                e.Item.ForeColor = Color.Black;
            }
            if (e.Item.Pressed)
            {
                using (var b = new LinearGradientBrush(rect, Color.FromArgb(245, 245, 234, 130), Color.FromArgb(235, 235, 234, 130), 90))
                {
                    using (var b2 = new SolidBrush(Color.OrangeRed))
                    {
                        e.Graphics.FillRectangle(b2, rectBorder);
                        e.Graphics.FillRectangle(b, rect);
                    }
                }
            }
        }
    }


    //***************************************************************************************************************************
    //                         DarkToolStripRenderer
    //***************************************************************************************************************************
    public class DarkToolStripRenderer : ToolStripProfessionalRenderer
    {

        // Render button selected and pressed state
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderButtonBackground(e);
            var rectBorder = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
            var rect = new Rectangle(1, 1, e.Item.Width - 2, e.Item.Height - 2);
            Brush b2 = new System.Drawing.Drawing2D.LinearGradientBrush(e.Item.ContentRectangle, Color.FromArgb(255, 255, 255, 180), Color.FromArgb(255, 255, 234, 130), 90);

            if (e.Item.Selected == true || (e.Item as ToolStripButton).Checked)
            {
                e.Graphics.FillRectangle(b2, rect);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Orange)), rectBorder);
                e.Item.ForeColor = Color.Black;
            }

            if (e.Item.Pressed)
            {
                using (var b = new LinearGradientBrush(rect, Color.FromArgb(245, 245, 234, 130), Color.FromArgb(235, 235, 234, 130), 90))
                {
                    using (var b3 = new SolidBrush(Color.OrangeRed))
                    {
                        e.Graphics.FillRectangle(b3, rectBorder);
                        e.Graphics.FillRectangle(b, rect);
                    }
                }
            }
        }
    }


    //***************************************************************************************************************************
    //                         LightToolStripRenderer
    //***************************************************************************************************************************
    public class LightToolStripRenderer : ToolStripProfessionalRenderer
    {
        // Render button selected and pressed state
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderButtonBackground(e);
            var rectBorder = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
            var rect = new Rectangle(1, 1, e.Item.Width - 2, e.Item.Height - 2);
            Brush b2 = new System.Drawing.Drawing2D.LinearGradientBrush(e.Item.ContentRectangle, Color.FromArgb(241, 248, 251), Color.FromArgb(146, 202, 230), 90);

            if (e.Item.Selected == true || (e.Item as ToolStripButton).Checked)
            {
                e.Graphics.FillRectangle(b2, rect);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(150, 150, 210))), rectBorder);
                e.Item.ForeColor = Color.Black;
            }

            if (e.Item.Pressed)
            {
                using (var b = new LinearGradientBrush(rect, Color.FromArgb(231, 238, 240), Color.FromArgb(126, 180, 210), 90))
                {
                    using (var b3 = new SolidBrush(Color.OrangeRed))
                    {
                        e.Graphics.FillRectangle(b3, rectBorder);
                        e.Graphics.FillRectangle(b, rect);
                    }
                }
            }
        }
    }


    //***************************************************************************************************************************
    //                         NightToolStripRenderer
    //***************************************************************************************************************************
    public class NightToolStripRenderer : ToolStripProfessionalRenderer
    {
        // Render button selected and pressed state
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderButtonBackground(e);
            var rectBorder = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
            var rect = new Rectangle(1, 1, e.Item.Width - 2, e.Item.Height - 2);
            Brush b2 = new System.Drawing.Drawing2D.LinearGradientBrush(e.Item.ContentRectangle, Color.FromArgb(30, 30, 30), Color.FromArgb(30, 30, 30), 90);
            Brush b4 = new System.Drawing.Drawing2D.LinearGradientBrush(e.Item.ContentRectangle, Color.FromArgb(30, 30, 30), Color.FromArgb(30, 30, 30), 90);

            if (e.Item.Selected == true || (e.Item as ToolStripButton).Checked)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(80, 80, 80)), rect);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(50, 50, 50))), rectBorder);
                e.Item.ForeColor = Color.Black;
            }
            else
            {
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(50, 50, 50))), rectBorder);
                e.Graphics.FillRectangle(b4, rect);
            }

            if (e.Item.Pressed)
            {
                using (var b = new LinearGradientBrush(rect, Color.FromArgb(150, 150, 150), Color.FromArgb(150, 150, 150), 90))
                {
                    using (var b3 = new SolidBrush(Color.Black))
                    {
                        e.Graphics.FillRectangle(b3, rectBorder);
                        e.Graphics.FillRectangle(b, rect);
                    }
                }
            }
        }
    }
}
