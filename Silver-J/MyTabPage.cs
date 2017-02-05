#region Copyright
/***************************************************************************************
 ******Copyright (C) 2016 Pritam Zope*****
 
  <copyright file="MyTabPage.cs" company="">
  
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
using System.IO;
using System.Windows.Forms;
using System.Xml;
using ICSharpCode.TextEditor;
using System.Diagnostics;
using ICSharpCode.TextEditor.Document;
namespace Silver_J
{
    public class MyTabPage : System.Windows.Forms.TabPage
    {
        private System.ComponentModel.IContainer components = null;
        public RichTextBox richTextBox1 = new RichTextBox();
        String class_str = "";
        String method_str = "";
        public static Boolean ischanged = false;
        public TextEditorControl textEditor = new TextEditorControl();
        public MainForm F;
        public Process process;
        public string javaPath;
        public StreamReader ErrorReader;
        public StreamReader OutputReader;


        /// <summary>
        /// textEditor text is changed or not
        /// </summary>
        public Boolean IsChanged
        {
            get { return ischanged; }
            set { ischanged = value; Invalidate(); }
        }



        public MyTabPage(MainForm frm)
        {
            F = frm;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            textEditor.Text = "";
            textEditor.Dock = DockStyle.Fill;
            AddLanguages("Java");
            
            textEditor.VRulerRow = 250;
            SetBackColorToTextEditor();

            //adding events  to text editor
            this.textEditor.TextChanged += new System.EventHandler(this.TextEditor_TextChanged);

            this.textEditor.ActiveTextAreaControl.TextArea.KeyPress += new
     System.Windows.Forms.KeyPressEventHandler(textEditor_KeyPress);

            this.textEditor.ActiveTextAreaControl.TextArea.KeyUp += new
            System.Windows.Forms.KeyEventHandler(textEditor_KeyUp);

            this.textEditor.ActiveTextAreaControl.TextArea.PreviewKeyDown += new
           System.Windows.Forms.PreviewKeyDownEventHandler(textEditor_PreviewKeyDown);

            this.textEditor.ActiveTextAreaControl.TextArea.MouseClick += new
            System.Windows.Forms.MouseEventHandler(textEditor_MouseClick);


            this.textEditor.ActiveTextAreaControl.TextArea.KeyDown += new
             System.Windows.Forms.KeyEventHandler(textEditor_KeyDown);

            //adding events to CodeCompleteBox
            CodeCompleteBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(CodeCompleteBox_KeyDown);
            CodeCompleteBox.KeyPress += new KeyPressEventHandler(CodeCompleteBox_KeyPress);
            CodeCompleteBox.MouseClick += new MouseEventHandler(CodeCompleteBox_MouseClick);
            CodeCompleteBox.KeyUp += new System.Windows.Forms.KeyEventHandler(CodeCompleteBox_KeyUp);

            //adding text editor to 
            this.Controls.Add(textEditor);
        }


        //*********************************************************************
        //   declare ToolTipControlPanel object ToolTipControl
        //*********************************************************************
        public ToolTipControlPanel ToolTipControl = new ToolTipControlPanel();


        public ListBox CodeCompleteBox = new ListBox();

        public static String EnteredKey = "";

        public static Boolean isClassCreated = false;

        public static Boolean isDataTypeDeclared = false;

        public static Boolean isToolTipControlAdded = false;


        public Boolean IsClassCreated
        {
            get { return isClassCreated; }
            set { isClassCreated = value; Invalidate(); }
        }


        public Boolean IsDataTypeDeclared
        {
            get { return isDataTypeDeclared; }
            set { isDataTypeDeclared = value; Invalidate(); }
        }

        //to check codecompletebox is added or not
        public static Boolean isCodeCompleteBoxAdded = false;


        public static Color backcolor = SystemColors.Window;
        public static Color forecolor = Color.Black;

        /// <summary>
        /// code complete box back & fore color
        /// </summary>
        public Color CodeCompleteBackColor
        {
            get { return backcolor; }
            set { backcolor = value; CodeCompleteBox.BackColor = value; Invalidate(); }
        }
        
        public Color CodeCompleteForeColor
        {
            get { return forecolor; }
            set { forecolor = value; CodeCompleteBox.ForeColor = value; Invalidate(); }
        }


        /// <summary>
        /// returns x,y points from texteditor
        /// </summary>
        /// <returns>points</returns>
        public Point getXYPoints()
        {
            //get current caret position point from texteditor
            Point pt = textEditor.ActiveTextAreaControl.TextArea.Caret.ScreenPosition;
            // increase the Y co-ordinate size by 10 & Font size of texteditor
            pt.Y = pt.Y + (int)textEditor.ActiveTextAreaControl.TextArea.Font.Size + 10;

            //  check Y co-ordinate value is greater than texteditor Height - CodeCompleteBox
            //   for add CodeCompleteBox at the Bottom of texteditor
            if (pt.Y > textEditor.Height - CodeCompleteBox.Height)
            {
                pt.Y = pt.Y - CodeCompleteBox.Height - (int)textEditor.ActiveTextAreaControl.TextArea.Font.Size - 10;
            }

            //  Point p = new Point(pt.X, pt.Y);

            return pt;
        }


        public List<String> keywordslist =new List<String> {
   "asm", 
"auto", 
"bool", 
"break", 
"case", 
"catch", 
"char", 
"class", 
"const", 
"const_cast", 
"continue", 
"default", 
"delete",
"do", 
"double", 
"dynamic_cast", 
"else", 
"enum", 
"explicit", 
"export", 
"extern", 
"false", 
"float", 
"for", 
"friend", 
"goto",
"include", 
"if", 
"inline", 
"int", 
"long", 
"mutable",
"namespace", 
"new",
"operator", 
"private", 
"protected", 
"public", 
"register",
"reinterpret_cast", 
"return", 
"short", 
"signed", 
"sizeof", 
"static", 
"static_cast", 
"struct", 
"switch", 
"template",
"this", 
"throw",
"true", 
"try", 
"typedef",
"typeid",
"typename",
"union",
"unsigned",
"using",
"virtual",
"void",
"volatile",
"wchar_t", 
"while",
"JFrame",
"JPanel",
"imports",
"Graphics",
"Graphics2D"
        };


        public List<String> classeslist =new List<String>
       {
           "JFrame",
           "JPanel",
           "Graphics",
           "Graphics2D"
       };

        public List<String> methodslist = new List<String>
       {
       };


        public List<String> datatypes =new List<String>
       {
          "byte",
          "short",
          "ushort",
          "int",
          "uint",
          "long",
          "ulong",
          "float",
          "double",
          "decimal",
          "char",
          "bool",
          "void"
       };




        public List<String> KeywordsList
        {
            get { return keywordslist; }
            set { keywordslist = value; Invalidate(); }
        }


        public List<String> ClassesList
        {
            get { return classeslist; }
            set { classeslist = value; Invalidate(); }
        }

        public List<String> MethodsList
        {
            get { return methodslist; }
            set { methodslist = value; Invalidate(); }
        }

        public List<String> DataTypesList
        {
            get { return datatypes; }
            set { datatypes = value; Invalidate(); }
        }


        public void ProcessDeclaredClasses(String input)
        {
            foreach (String item in classeslist)
            {
                if (item == input)
                {
                    isClassCreated = true;
                }
            }
        }



        public void ProcessDeclaredDataTypes(String input)
        {
            foreach (String item in datatypes)
            {
                if (item == input)
                {
                    isDataTypeDeclared = true;
                }
            }
        }



        //***************************************************************
        //    CreateClassObject_And_SetToolTips() 
        //***************************************************************
        public static int objectcount = 1;
        public void CreateClassObject_And_SetToolTips()
        {
            String configfile = Application.StartupPath + "\\files\\jkeywords.slvjfile";
            List<String> _classeslist = new List<String> { };
            using (XmlReader reader = XmlReader.Create(configfile))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "JClassKeyword":
                                _classeslist.Add(reader.ReadString());
                                break;
                        }
                    }
                }
            }

            if(isCodeCompleteBoxAdded)
            {
                foreach(String item in _classeslist)
                {
                    if(CodeCompleteBox.SelectedItem.ToString()==item)
                    {
                        if (EnteredKey != "")
                        {
                            if (EnteredKey.Length == 1)
                            {
                                int sel = textEditor.ActiveTextAreaControl.Caret.Offset;
                                String text = item.Remove(0, 1);
                                text = text + " obj" + objectcount + " = new " + item + "();";
                                textEditor.Document.Insert(sel, text);
                                textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                                textEditor.ActiveTextAreaControl.Caret.Column = sel + text.Length;
                                EnteredKey = "";

                                objectcount++;

                                if (isToolTipControlAdded)
                                {
                                    textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                                    isToolTipControlAdded = false;
                                }

                            }
                            else if (EnteredKey.Length == 2)
                            {
                                int sel = textEditor.ActiveTextAreaControl.Caret.Offset;
                                String text = item.Remove(0, 2);
                                text = text + " obj" + objectcount + " = new " + item + "();";
                                textEditor.Document.Insert(sel, text);
                                textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                                textEditor.ActiveTextAreaControl.Caret.Column = sel + text.Length;
                                EnteredKey = "";

                                objectcount++;

                                if (isToolTipControlAdded)
                                {
                                    textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                                    isToolTipControlAdded = false;
                                } 

                            }
                            else if (EnteredKey.Length == 3)
                            {
                                int sel = textEditor.ActiveTextAreaControl.Caret.Offset;
                                String text = item.Remove(0, 3);
                                text = text + " obj" + objectcount + " = new " + item + "();";
                                textEditor.Document.Insert(sel, text);
                                textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                                textEditor.ActiveTextAreaControl.Caret.Column = sel + text.Length;
                                EnteredKey = "";

                                if (isToolTipControlAdded)
                                {
                                    textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                                    isToolTipControlAdded = false;
                                }
                            }
                            else
                            {
                                int sel = textEditor.ActiveTextAreaControl.Caret.Offset;
                                String text = item;
                                if (text.Contains(EnteredKey))
                                {
                                    text = text.Replace(EnteredKey, "");
                                }
                                text = text + " obj" + objectcount + " = new " + item + "();";
                                textEditor.Document.Insert(sel, text);
                                textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                                textEditor.ActiveTextAreaControl.Caret.Column = sel + text.Length;
                                EnteredKey = "";

                                if (isToolTipControlAdded)
                                {
                                    textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                                    isToolTipControlAdded = false;
                                }

                            }
                        }
                    }
                }

            }
        }



        //*******************************************************************************************************************
        //  ProcessToolTips() function
        //  match selected item with keywords list item and set width & height & change text of label
        //*******************************************************************************************************************
        public void ProcessToolTips(String input)
        {
            switch (input)
            {
                case "class":
                    ToolTipControl.ToolTipText = "class\nCollections of datas and functions\n\nPress F2 to insert snippet";
                    ToolTipControl.Width = 220;
                    ToolTipControl.Height = 70;
                    break;

                case "else":
                    ToolTipControl.ToolTipText = "else\nCode snippet for else statement\n\nPress F2 to insert snippet";
                    ToolTipControl.Width = 220;
                    ToolTipControl.Height = 70;
                    break;

                case "enum":
                    ToolTipControl.ToolTipText = "enum\nCode snippet for enum statement\n\nPress F2 to insert snippet";
                    ToolTipControl.Width = 220;
                    ToolTipControl.Height = 70;
                    break;

                case "for":
                    ToolTipControl.ToolTipText = "for loop\nCode snippet for 'for' loop\n\nPress F2 to insert snippet";
                    ToolTipControl.Width = 220;
                    ToolTipControl.Height = 70;
                    break;

                case "if":
                    ToolTipControl.ToolTipText = "if\nCode snippet for if statement\n\nPress F2 to insert snippet";
                    ToolTipControl.Width = 220;
                    ToolTipControl.Height = 70;
                    break;

                case "switch":
                    ToolTipControl.ToolTipText = "switch\nCode snippet for switch statement\n\nPress F2 to insert snippet";
                    ToolTipControl.Width = 220;
                    ToolTipControl.Height = 70;
                    break;

                case "try":
                    ToolTipControl.ToolTipText = "try\nCode snippet for try catch\n\nPress F2 to insert snippet";
                    ToolTipControl.Width = 200;
                    ToolTipControl.Height = 70;
                    break;

                case "while":
                    ToolTipControl.ToolTipText = "while loop\nCode snippet for while loop\n\nPress F2 to insert snippet";
                    ToolTipControl.Width = 220;
                    ToolTipControl.Height = 70;
                    break;

                default:
                    if (isToolTipControlAdded)
                    {
                        ToolTipControl.Visible = false;
                    }
                    break;

            }

        }




        // insert code into CCRichTextBox at selection start position 
        // when Tab key is down
        // e.g if user pressed a key 'Tab' twice on 'for' selected item in
        // CodeCompleteBox then insert for loop
        public void InsertSyntax(String text)
        {
            if (isCodeCompleteBoxAdded)
            {
                if (EnteredKey != "")
                {
                    if (EnteredKey.Length == 1)
                    {
                        int sel = textEditor.ActiveTextAreaControl.Caret.Offset;
                        text = text.Remove(0, 1);
                        textEditor.Document.Insert(sel, text);
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }

                    }
                    else if (EnteredKey.Length == 2)
                    {
                        int sel = textEditor.ActiveTextAreaControl.Caret.Offset;
                        text = text.Remove(0, 2);
                        textEditor.Document.Insert(sel, text);
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        } 

                    }
                    else if (EnteredKey.Length == 3)
                    {
                        int sel = textEditor.ActiveTextAreaControl.Caret.Offset;
                        text = text.Remove(0, 3);
                        textEditor.Document.Insert(sel, text);
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }
                    }
                    else
                    {
                        int sel = textEditor.ActiveTextAreaControl.Caret.Offset;
                        if (text.Contains(EnteredKey))
                        {
                            text = text.Replace(EnteredKey, "");
                        }
                        textEditor.Document.Insert(sel, text);
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }

                    }
                }
            }
        }



        //**************************************************************
        // InsertingCodeSnippetCodes() function
        //**************************************************************
        public void InsertingCodeSnippetCodes()
        {
            if (isCodeCompleteBoxAdded)
            {
                RichTextBox rtb = new RichTextBox();

                switch (CodeCompleteBox.SelectedItem.ToString())
                {
                    case "class":
                        rtb.Text = "";
                        rtb.Text = "class MyClass\n{\n                   \n}";
                        this.InsertSyntax(rtb.Text);
                        break;

                    case "do":
                        rtb.Text = "";
                        rtb.Text = "do  {\n               \n}while(true);";
                        this.InsertSyntax(rtb.Text);
                        break;

                    case "else":
                        rtb.Text = "";
                        rtb.Text = "else  {\n                \n}";
                        this.InsertSyntax(rtb.Text);
                        break;

                    case "enum":
                        rtb.Text = "";
                        rtb.Text = "enum MyEnums  {\n             \n}";
                        this.InsertSyntax(rtb.Text);
                        break;

                    case "for":
                        rtb.Text = "";
                        rtb.Text = "for( )\n{\n             \n}";
                        this.InsertSyntax(rtb.Text);
                        break;

                    case "if":
                        rtb.Text = "";
                        rtb.Text = "if(true) \n{\n}";
                        this.InsertSyntax(rtb.Text);
                        break;

                    case "switch":
                        rtb.Text = "";
                        rtb.Text = "switch( ) \n{ \n           default : break;     \n}";
                        this.InsertSyntax(rtb.Text);
                        break;

                    case "try":
                        rtb.Text = "";
                        rtb.Text = "try\n{\n                   \n} \n catch(Exception e) \n{ \n              throw;          \n}";
                        this.InsertSyntax(rtb.Text);
                        break;

                    case "while":
                        rtb.Text = "";
                        rtb.Text = "while(true)\n{\n                   \n}";
                        this.InsertSyntax(rtb.Text);
                        break;
                }
            }
        }



        /// <summary>
        /// reading JKeyword,JClassKeyword,JMethodKeyword words from file jkeywords.slvjfile
        /// </summary>
        public void GetKeywordsListFromFile()
        {
            List<String> keylist = new List<String> { };
            List<String> classlist = new List<String> { };
            List<String> methlist = new List<String> { };

            using (XmlReader reader = XmlReader.Create(Application.StartupPath+"\\files\\jkeywords.slvjfile"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "JKeyword":
                                keylist.Add(reader.ReadString());
                                break;

                            case "JClassKeyword":
                                classlist.Add(reader.ReadString());
                                break;

                            case "JMethodKeyword":
                                keylist.Add(reader.ReadString());
                                break;
                        }
                    }
                }
            }

            this.KeywordsList = keylist;
            this.ClassesList = classlist;
            this.MethodsList = methlist;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns>int as code complete box width</returns>
        public int getWidth()
        {
            int width = 10;

            foreach (String item in CodeCompleteBox.Items)
            {
                if(item.Length<=5)
                {
                    width = 250;
                }
                else if (item.Length <= 10)
                {
                    width = 250;
                }
                else if (item.Length <= 20)
                {
                    width = width+item.Length*2;
                }
                else
                {
                    width = width+ item.Length*4;
                }
            }

            return width;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns>int code complete box height</returns>
        public int getHeight()
        {
            int height = 10;

            //  get Font size of richTextBox1
            int fontsize = (int)richTextBox1.Font.Size;

            //  get number of items added to CodeCompleteBox
            int count = CodeCompleteBox.Items.Count;


            //   increase the height of CodeCompleteBox if added items count is 0,1,2,3,4,5
            switch (count)
            {
                case 0: height = fontsize;
                    break;
                case 1: height += 10 + fontsize;
                    break;
                case 2: height += 20 + fontsize;
                    break;
                case 3: height += 30 + fontsize;
                    break;
                case 4: height += 40 + fontsize;
                    break;
                case 5: height += 50 + fontsize;
                    break;
                case 6: height += 60 + fontsize;
                    break;
                case 7: height += 70 + fontsize;
                    break;
                case 8: height += 80 + fontsize;
                    break;
                case 9: height += 90 + fontsize;
                    break;
                case 10: height += 100 + fontsize;
                    break;
                case 11: height += 110 + fontsize;
                    break;
                case 12: height += 120 + fontsize;
                    break;
                case 13: height += 130 + fontsize;
                    break;
                case 14: height += 140 + fontsize;
                    break;
                case 15: height += 150 + fontsize;
                    break;
                default: height += 200 + fontsize;
                    break;
                 
            }

            return height;
        }




        //*************************************************************************************
        // Add Languages
        //*************************************************************************************
        public void AddLanguages(String lang)
        {
            string dirc = Application.StartupPath;
            FileSyntaxModeProvider fsmp;
            if (Directory.Exists(dirc))
            {
                fsmp = new FileSyntaxModeProvider(dirc);
                HighlightingManager.Manager.AddSyntaxModeFileProvider(fsmp);
                textEditor.SetHighlighting(lang);
            }
            SetFont();
            SetViews();
            SetBackColorToTextEditor();
        }


        //*************************************************************************************
        // Set Font()
        //*************************************************************************************
        public void SetFont()
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            String font = "";
            int fontsize = 10;
            using (XmlReader reader = XmlReader.Create(configfile))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "Font":
                                font = reader.ReadString();
                                break;

                            case "FontSize":
                                fontsize = Convert.ToInt32(reader.ReadString());
                                break;
                        }
                    }
                }
            }

            textEditor.Font = new Font(font, fontsize);
        }



        //*************************************************************************************
        // Set Views()
        //*************************************************************************************
        public void SetViews()
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            String showlinenumbers = "";
            String showlinehighlighter = "";
            String showinvalidlines = "";
            String showendoflinemarker = "";
            String showvisiblespaces = "";
            String bracesmatching = "";
            using (XmlReader reader = XmlReader.Create(configfile))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
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
                                showendoflinemarker = reader.ReadString();
                                break;

                            case "ShowVisibleSpaces":
                                showvisiblespaces = reader.ReadString();
                                break;

                            case "BracesMatching":
                                bracesmatching = reader.ReadString();
                                break;
                        }
                    }
                }
            }
            if (showlinenumbers == "true")
            {
                textEditor.ShowLineNumbers = true;
            }
            else
            {
                textEditor.ShowLineNumbers = false;
            }

            if (showlinehighlighter == "true")
            {
                textEditor.LineViewerStyle = LineViewerStyle.FullRow;
            }
            else
            {
                textEditor.LineViewerStyle = LineViewerStyle.None;
            }

            if (showinvalidlines == "true")
            {
                textEditor.ShowInvalidLines = true;
            }
            else
            {
                textEditor.ShowInvalidLines = false;
            }

            if (showendoflinemarker == "true")
            {
                textEditor.ShowEOLMarkers = true;
            }
            else
            {
                textEditor.ShowEOLMarkers = false;
            }

            if (showvisiblespaces == "true")
            {
                textEditor.ShowSpaces = true;
            }
            else
            {
                textEditor.ShowSpaces = false;
            }

            if (bracesmatching == "true")
            {
                textEditor.ShowMatchingBracket = true;
            }
            else
            {
                textEditor.ShowMatchingBracket = false;
            }
        }


        //*************************************************************************************
        // IsAutoCompleteBracesSelected()
        //*************************************************************************************
        public Boolean IsAutoCompeleBracesSelected()
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            String autocompletebraces = "";
            Boolean ans = true;
            using (XmlReader reader = XmlReader.Create(configfile))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "AutoCompleteBraces":
                                autocompletebraces = reader.ReadString();
                                break;
                        }
                    }
                }
            }
            if (autocompletebraces == "true")
            {
                ans = true;
            }
            else if (autocompletebraces == "false")
            {
                ans = false;
            }
            return ans;
        }


        //*************************************************************************************
        // InitializeComponent()
        //*************************************************************************************
        public void InitializeComponent()
        {
            this.textEditor = new ICSharpCode.TextEditor.TextEditorControl();
            this.SuspendLayout();
            // 
            // textEditor
            // 
            this.textEditor.IsReadOnly = false;
            this.textEditor.Location = new System.Drawing.Point(0, 0);
            this.textEditor.Name = "textEditor";
            this.textEditor.Size = new System.Drawing.Size(100, 100);
            this.textEditor.TabIndex = 0;
            this.textEditor.Load += new System.EventHandler(this.textEditor_Load);
            this.ResumeLayout(false);

        }


        //*************************************************************************************
        // ReadCurrentProjectFileName()
        //*************************************************************************************

        public String ReadCurrentProjectFileName()
        {
            String s = "";
            using (XmlReader reader = XmlReader.Create(Application.StartupPath + "\\files\\defaultprojloc.slvjfile"))
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


        //*************************************************************************************
        // ReadCurrentProjectLocationFolder
        //*************************************************************************************
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


        //*************************************************************************************
        // getMainClassFileName
        //*************************************************************************************
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
        // getJDKPath
        //*************************************************************************************
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

        public Boolean IsAutoCompileJavaSelected()
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            Boolean isautocompile = false;
            String autocompile = "";
            using (XmlReader reader = XmlReader.Create(configfile))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "AutoCompileJava":
                                autocompile = reader.ReadString();
                                break;
                        }
                    }
                }
            }
            if (autocompile == "true")
            {
                isautocompile = true;
            }
            else
            {
                isautocompile = false;
            }
            return isautocompile;
        }



        //*************************************************************************************
        // Compile
        //*************************************************************************************
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


        //*************************************************************************************
        // Compile Java
        //*************************************************************************************
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
                    F.ErrorTextBox.Text = response;
                }
                else if (response == "")
                {
                    F.ErrorTextBox.Text = "Program Compiled Successfully.................!";
                }
                else
                {
                    F.ErrorTextBox.Text = "Program Compiled Successfully.................!";
                }
            }
            else if (File.Exists("" + mystr + ".class"))
            {
                F.ErrorTextBox.Text = "Program Compiled Successfully.................!";
            }
        }



        /// <summary>
        /// complete (,[,",',{ characters when pressed
        /// </summary>
        /// <param name="s"></param>
        public void ProcessAutoCompleteBrackets(String s)
        {
           int sel = textEditor.ActiveTextAreaControl.Caret.Offset;
            if (IsAutoCompeleBracesSelected() == true)
            {
                switch (s)
                {
                    case "(":
                        textEditor.Document.Insert(sel, ")");
                        ischanged = false;
                        ismouseclickontexteditor = true;
                        break;

                    case "[":
                        textEditor.Document.Insert(sel, "]");
                        ismouseclickontexteditor = true;
                        ischanged = false;
                        break;

                    case "{":
                        textEditor.Document.Insert(sel, "}");
                        ischanged = false;
                        ismouseclickontexteditor = true;
                        break;

                    case "'":
                        textEditor.Document.Insert(sel, "'");
                        ischanged = false;
                        ismouseclickontexteditor = true;
                        break;

                    case "\"":
                        textEditor.Document.Insert(sel, "\"");
                        ischanged = false;
                        ismouseclickontexteditor = true;
                        break;

                    case "<":
                        if (this.Text.Contains(".html") || this.Text.Contains(".xml"))
                        {
                            textEditor.Document.Insert(sel, ">");
                            ischanged = false;
                            ismouseclickontexteditor = true;
                        }
                        break;
                }
            }
        }



        //*************************************************************************************
        // IsAutoCompletionModeSelected()
        //*************************************************************************************
        public Boolean IsAutoCompeletionModeSelected()
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            String autocomplete = "";
            Boolean ans = true;
            using (XmlReader reader = XmlReader.Create(configfile))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "AutoCompletion":
                                autocomplete = reader.ReadString();
                                break;
                        }
                    }
                }
            }
            if (autocomplete == "true")
            {
                ans = true;
            }
            else if (autocomplete == "false")
            {
                ans = false;
            }
            return ans;
        }


        /// <summary>
        /// code completion action
        /// </summary>
        /// <param name="key">which character is pressed from keyboard</param>
        public void ProcessCodeCompletionAction(String key)
        {
            if (IsAutoCompeletionModeSelected())
            {
                EnteredKey = "";

                // concat the key & EnteredKey postfix
                EnteredKey = EnteredKey + key;

                this.GetKeywordsListFromFile();

                keywordslist.Sort();

                ToolTipControl.Visible = false;

                char ch;

                //check pressed key on texteditor is lower case alphabet or not
                for (ch = 'a'; ch <= 'z'; ch++)
                {
                    if (key == ch.ToString())
                    {
                        // Clear the CodeCompleteBox Items 
                        CodeCompleteBox.Items.Clear();
                        //add each item to CodeCompleteBox
                        foreach (String item in keywordslist)
                        {
                            //check item is starts with EnteredKey or not
                            if (item.StartsWith(EnteredKey))
                            {
                                CodeCompleteBox.Items.Add(item);
                            }
                        }

                        //  read each item from CodeCompleteBox to set SelectedItem
                        foreach (String item in keywordslist)
                        {
                            if (item.StartsWith(EnteredKey))
                            {
                                CodeCompleteBox.SelectedItem = item;

                                //  set Default cursor to CodeCompleteBox
                                CodeCompleteBox.Cursor = Cursors.Default;

                                //  set Size to CodeCompleteBox
                                // width=250 & height=this.getHeight()+(int)texteditor.Font.Size
                                CodeCompleteBox.Size = new System.Drawing.Size(this.getWidth(), this.getHeight() + (int)textEditor.ActiveTextAreaControl.TextArea.Font.Size);

                                //  set Location to CodeCompleteBox by calling getXYPoints() function
                                CodeCompleteBox.Location = this.getXYPoints();

                                //  adding controls of CodeCompleteBox to texteditor
                                textEditor.ActiveTextAreaControl.TextArea.Controls.Add(CodeCompleteBox);

                                //  set Focus to CodeCompleteBox
                                CodeCompleteBox.Focus();

                                //  set isCodeCompleteBoxAdded to true
                                isCodeCompleteBoxAdded = true;

                                // set location to ToolTipControl
                                ToolTipControl.Location = new Point(CodeCompleteBox.Location.X + CodeCompleteBox.Width, CodeCompleteBox.Location.Y);

                                // call ProcessToolTips() function
                                this.ProcessToolTips(CodeCompleteBox.SelectedItem.ToString());

                                // add ToolTipControl to CCRichTextBox
                                textEditor.ActiveTextAreaControl.TextArea.Controls.Add(ToolTipControl);

                                isToolTipControlAdded = true;

                                break;
                            }

                            else
                            {
                                isCodeCompleteBoxAdded = false;
                            }
                        }
                    }
                    else if (key == ch.ToString().ToUpper())
                    {
                        // Clear the CodeCompleteBox Items 
                        CodeCompleteBox.Items.Clear();
                        //add each item to CodeCompleteBox
                        foreach (String item in keywordslist)
                        {
                            //check item is starts with EnteredKey or not
                            if (item.StartsWith(EnteredKey))
                            {
                                CodeCompleteBox.Items.Add(item);
                            }
                        }

                        //  read each item from CodeCompleteBox to set SelectedItem
                        foreach (String item in keywordslist)
                        {
                            if (item.StartsWith(EnteredKey))
                            {
                                CodeCompleteBox.SelectedItem = item;

                                //  set Default cursor to CodeCompleteBox
                                CodeCompleteBox.Cursor = Cursors.Default;

                                //  set Size to CodeCompleteBox
                                // width=250 & height=this.getHeight()+(int)texteditor.Font.Size
                                CodeCompleteBox.Size = new System.Drawing.Size(this.getWidth(), this.getHeight() + (int)textEditor.ActiveTextAreaControl.TextArea.Font.Size);

                                //  set Location to CodeCompleteBox by calling getXYPoints() function
                                CodeCompleteBox.Location = this.getXYPoints();

                                //  adding controls of CodeCompleteBox to texteditor
                                textEditor.ActiveTextAreaControl.TextArea.Controls.Add(CodeCompleteBox);

                                //  set Focus to CodeCompleteBox
                                CodeCompleteBox.Focus();

                                //  set isCodeCompleteBoxAdded to true
                                isCodeCompleteBoxAdded = true;

                                // set location to ToolTipControl
                                ToolTipControl.Location = new Point(CodeCompleteBox.Location.X + CodeCompleteBox.Width, CodeCompleteBox.Location.Y);

                                // call ProcessToolTips() function
                                this.ProcessToolTips(CodeCompleteBox.SelectedItem.ToString());

                                // add ToolTipControl to CCRichTextBox
                                textEditor.ActiveTextAreaControl.TextArea.Controls.Add(ToolTipControl);

                                isToolTipControlAdded = true;

                                break;
                            }

                            else
                            {
                                isCodeCompleteBoxAdded = false;
                            }
                        }
                    }
                }
            }
        }





        //*************************************************************************************
        // TextEditor Text Changed
        //*************************************************************************************
        /// <summary>
        /// when text changed add * to tab page text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextEditor_TextChanged(object sender, EventArgs e)
        {
            String str = this.Text;
            if (str.Contains("*"))
            {

            }
            else
            {
                this.Text = str + "*";
            }
            richTextBox1.Text = textEditor.Text;

            String[] Lines = richTextBox1.Lines;
            int line = this.textEditor.ActiveTextAreaControl.TextArea.Caret.Line;
            if (this.textEditor.Text == "")
            {
                F.ClassesTreeView.Nodes.Clear();
                F.MethodsTreeView.Nodes.Clear();
            }
            else
            {
                try
                {
                    if (Lines[line].Contains("void "))
                    {
                        method_str = Lines[line];
                        method_str = method_str.Trim();
                    }
                    if (Lines[line].Contains("class "))
                    {
                        class_str = Lines[line];
                        class_str = class_str.Trim();
                    }
                }
                catch (IndexOutOfRangeException ie) { }
            }

            if (this.Text == "")
            {
                if (isCodeCompleteBoxAdded)
                {
                    textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                    EnteredKey = "";

                    if (isToolTipControlAdded)
                    {
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                        isToolTipControlAdded = false;
                    }
                }
            }

        }


        String[] symbols = { "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "-", "=", "+", "{", "}", "[", "]", ";", ":", "\"", "'", "\\", "|", "<", ">", ",", ".", "?", "/" };

        //*************************************************************************************
        // textEditor Key Press
        //*************************************************************************************
        private void textEditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            String s = e.KeyChar.ToString();
            Char ch = e.KeyChar;
            int numbers = Convert.ToInt32(e.KeyChar);

            for (char c = 'a'; c <= 'z'; c++)
            {
                if (ch == c)
                {
                    int col3 = textEditor.ActiveTextAreaControl.TextArea.Caret.Column + 1;
                    F.ColumnToolStripLabel.Text = "Col : " + col3.ToString();
                    ischanged = true;
                    ismouseclickontexteditor = true;

                    if (ischanged)
                    {
                        Process_CompileJava();
                    }
                }
            }

            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (ch == c)
                {
                    int col3 = textEditor.ActiveTextAreaControl.TextArea.Caret.Column + 1;
                    F.ColumnToolStripLabel.Text = "Col : " + col3.ToString();
                    ischanged = false;
                    ismouseclickontexteditor = true;

                    if (ischanged)
                    {
                        Process_CompileJava();
                    }
                }
            }

            for (int a = 0; a <= numbers; a++)
            {
                int col3 = textEditor.ActiveTextAreaControl.TextArea.Caret.Column + 1;
                F.ColumnToolStripLabel.Text = "Col : " + col3.ToString();
                ischanged = true;
                ismouseclickontexteditor = true;
            }


            this.ProcessAutoCompleteBrackets(s);


            if (this.Text.Contains(".java"))
            {
                String key = e.KeyChar.ToString();

                if (isClassCreated && (key == "=" || key == ";"))
                {

                    ProcessCodeCompletionAction(key);

                    isClassCreated = false;

                }
                else if (isClassCreated && key != "=")
                { }
                else if ((isDataTypeDeclared && key == ";") || (isDataTypeDeclared && key == "(") || (isDataTypeDeclared && key == ")"))
                {

                    ProcessCodeCompletionAction(key);

                    isDataTypeDeclared = false; 
                }
                else if (isDataTypeDeclared && key != ";")
                { }
                else
                {
                    ProcessCodeCompletionAction(key);
                }

            }


            for(int i=0;i<symbols.Length;i++)
            {
                if(s==symbols[i])
                {
                    isClassCreated = false;
                    isDataTypeDeclared = false;

                    if (ischanged)
                    {
                        Process_CompileJava();
                    }
                }
            }

        }


        //*************************************************************************************
        // textEditor Key Up
        //*************************************************************************************
        private void textEditor_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    int a = textEditor.ActiveTextAreaControl.TextArea.Caret.Line + 1;
                    F.LineToolStripLabel.Text = "Line : " + a.ToString();
                    int col = textEditor.ActiveTextAreaControl.TextArea.Caret.Column + 1;
                    F.ColumnToolStripLabel.Text = "Col : " + col.ToString();

                    if(ischanged)
                    {
                        Process_CompileJava();
                        ischanged = false;
                    }

                   isClassCreated = false;
                   isDataTypeDeclared = false;

                    if (ischanged == true)
                    {
                        Process_ClassesMethods_Actions();
                        ischanged = false;
                        F.ClassesTreeView.Scrollable = true;
                        F.MethodsTreeView.Scrollable = true;

                    }

                                    cnt = 0;
                                    charcount = 0;
                                    oldsel = 1;

                    break;

                case Keys.Down:
                    int a1 = textEditor.ActiveTextAreaControl.TextArea.Caret.Line + 1;
                    F.LineToolStripLabel.Text = "Line : " + a1.ToString();
                    int col1 = textEditor.ActiveTextAreaControl.TextArea.Caret.Column + 1;
                    F.ColumnToolStripLabel.Text = "Col : " + col1.ToString();

                    if (ischanged)
                    {
                        Process_CompileJava();
                        ischanged = false;
                    }

                   isClassCreated = false;
                   isDataTypeDeclared = false;

                    if (ischanged == true)
                    {
                        Process_ClassesMethods_Actions();
                        ischanged = false;
                        F.ClassesTreeView.Scrollable = true;
                        F.MethodsTreeView.Scrollable = true;
                    }

                    
                                    cnt = 0;
                                    charcount = 0;
                                    oldsel = 1;

                    break;

                case Keys.Enter:
                    int a2 = textEditor.ActiveTextAreaControl.TextArea.Caret.Line + 1;
                    F.LineToolStripLabel.Text = "Line : " + a2.ToString();
                    int col2 = textEditor.ActiveTextAreaControl.TextArea.Caret.Column + 1;
                    F.ColumnToolStripLabel.Text = "Col : " + col2.ToString();

                    if (ischanged)
                    {
                        Process_CompileJava();
                        ischanged = false;
                    }

                    if (isCodeCompleteBoxAdded)
                    {
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }
                    }

                    if (ischanged == true)
                    {
                        Process_ClassesMethods_Actions();
                        ischanged = false;
                        F.ClassesTreeView.Scrollable = true;
                        F.MethodsTreeView.Scrollable = true;
                    }

                    if (ischanged)
                    {
                        Process_CompileJava();
                        ischanged = false;
                    }

                    
                                    cnt = 0;
                                    charcount = 0;
                                    oldsel = 1;

                    break;

                case Keys.Left:
                    int col3 = textEditor.ActiveTextAreaControl.TextArea.Caret.Column + 1;
                    F.ColumnToolStripLabel.Text = "Col : " + col3.ToString();

                    if (ischanged)
                    {
                        Process_CompileJava();
                        ischanged = false;
                    }

                   isClassCreated = false;
                   isDataTypeDeclared = false;

                    
                                    cnt = 0;
                                    charcount = 0;
                                    oldsel = 1;

                    break;

                case Keys.Right:
                    int col4 = textEditor.ActiveTextAreaControl.TextArea.Caret.Column + 1;
                    F.ColumnToolStripLabel.Text = "Col : " + col4.ToString();

                    if (ischanged)
                    {
                        Process_CompileJava();
                        ischanged = false;
                    }

                   isClassCreated = false;
                   isDataTypeDeclared = false;

                    
                                    cnt = 0;
                                    charcount = 0;
                                    oldsel = 1;

                    break;

                case Keys.Space:
                    int col5 = textEditor.ActiveTextAreaControl.TextArea.Caret.Column + 1;
                    F.ColumnToolStripLabel.Text = "Col : " + col5.ToString();

                    if (ischanged)
                    {
                        Process_CompileJava();
                        ischanged = false;
                    }

                    if (isCodeCompleteBoxAdded)
                    {
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;
                        EnteredKey = "";
                        
                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }
                    }

                    
                                    cnt = 0;
                                    charcount = 0;
                                    oldsel = 1;

                    break;


                case Keys.Escape:
                    if (isCodeCompleteBoxAdded)
                    {
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }
                    }
                    
                   isClassCreated = false;
                   isDataTypeDeclared = false;

                    
                                    cnt = 0;
                                    charcount = 0;
                                    oldsel = 1;

                    break;


                case Keys.Back:
                    if (isCodeCompleteBoxAdded)
                    {
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }
                    }

                    if (ischanged)
                    {
                        Process_CompileJava();
                        ischanged = false;
                    }

                   isClassCreated = false;
                   isDataTypeDeclared = false;

                    
                                    cnt = 0;
                                    charcount = 0;
                                    oldsel = 1;

                    break;

                case Keys.F1:
                    if (isCodeCompleteBoxAdded)
                    {
                        this.InsertingCodeSnippetCodes();
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                             textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                             isToolTipControlAdded = false;
                        }

                    }
                    
                                    cnt = 0;
                                    charcount = 0;
                                    oldsel = 1;
                    break;
            }
        }



        //*************************************************************************************
        // Process Auto Compile Java
        //*************************************************************************************
        public void Process_CompileJava()
        {
            if (IsAutoCompileJavaSelected() == true)
            {
                String filename = getMainClassFileName();
                String jdkpath = getJDKPath();
                if (File.Exists(filename))
                {
                    try
                    {
                        StreamWriter str;
                        System.IO.File.WriteAllText(filename, "");
                        str = System.IO.File.AppendText(filename);
                        str.Write(textEditor.Text);
                        str.Close();
                    }
                    catch { }

                    process = new Process();
                    this.CompileJava(filename, jdkpath);
                }
            }
        }

        //*************************************************************************************
        // textEditor Key Down
        //*************************************************************************************
        private void textEditor_KeyDown(object sender, KeyEventArgs e)
        {
            
        }


        //*************************************************************************************
        // textEditor Preview Key Down
        //*************************************************************************************
        private void textEditor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    int a = textEditor.ActiveTextAreaControl.TextArea.Caret.Line + 1;
                    F.LineToolStripLabel.Text = "Line : " + a.ToString();
                    int col = textEditor.ActiveTextAreaControl.TextArea.Caret.Column + 1;
                    F.ColumnToolStripLabel.Text = "Col : " + col.ToString();

                    if (isCodeCompleteBoxAdded)
                    {
                        ToolTipControl.Visible = true;
                        this.ProcessToolTips(CodeCompleteBox.SelectedItem.ToString());
                    }
                    break;


                case Keys.Down:
                    int a1 = textEditor.ActiveTextAreaControl.TextArea.Caret.Line + 1;
                    F.LineToolStripLabel.Text = "Line : " + a1.ToString();
                    int col1 = textEditor.ActiveTextAreaControl.TextArea.Caret.Column + 1;
                    F.ColumnToolStripLabel.Text = "Col : " + col1.ToString();
                    if (isCodeCompleteBoxAdded)
                    {
                        ToolTipControl.Visible = true;
                        this.ProcessToolTips(CodeCompleteBox.SelectedItem.ToString());
                    }

                    break;


                case Keys.Enter:
                    int a2 = textEditor.ActiveTextAreaControl.TextArea.Caret.Line + 1;
                    F.LineToolStripLabel.Text = "Line : " + a2.ToString();
                    int col2 = textEditor.ActiveTextAreaControl.TextArea.Caret.Column + 1;
                    F.ColumnToolStripLabel.Text = "Col : " + col2.ToString();

                    break;


                case Keys.Left:
                    int col3 = textEditor.ActiveTextAreaControl.TextArea.Caret.Column + 1;
                    F.ColumnToolStripLabel.Text = "Col : " + col3.ToString();

                    break;


                case Keys.Right:
                    int col4 = textEditor.ActiveTextAreaControl.TextArea.Caret.Column + 1;
                    F.ColumnToolStripLabel.Text = "Col : " + col4.ToString();

                    break;

                case Keys.Space:
                    int col5 = textEditor.ActiveTextAreaControl.TextArea.Caret.Column + 1;
                    F.ColumnToolStripLabel.Text = "Col : " + col5.ToString();

                     if (isCodeCompleteBoxAdded)
                    {
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }
                    }
                    break;


                case Keys.Escape:
                    if (isCodeCompleteBoxAdded)
                    {
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }
                    }
                    break;


                case Keys.Back:
                    if (isCodeCompleteBoxAdded)
                    {
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }
                    }

                    break;

            }

            if(e.Modifiers==Keys.Control && e.KeyCode==Keys.S)
            {
                F.File_SaveMenuItem_Click(sender, e);
            }

        }



        static Boolean ismouseclickontexteditor = true;

        public Boolean IsMouseClickOnTextEditor
        {
            get { return ismouseclickontexteditor; }
            set
            {
                ismouseclickontexteditor = value; Invalidate();
            }
        }



        //*************************************************************************************
        // textEditor Mouse Click
        //*************************************************************************************
        private void textEditor_MouseClick(object sender, MouseEventArgs e)
        {
            int a = textEditor.ActiveTextAreaControl.TextArea.Caret.Line + 1;
            F.LineToolStripLabel.Text = "Line : " + a.ToString();

            int col = textEditor.ActiveTextAreaControl.TextArea.Caret.Column + 1;
            F.ColumnToolStripLabel.Text = "Col : " + col.ToString();

            if (ismouseclickontexteditor == true)
            {
                Process_ClassesMethods_Actions();
                ischanged = false;
                F.ClassesTreeView.Scrollable = true;
                F.MethodsTreeView.Scrollable = true;
                ismouseclickontexteditor = false;

                if (isCodeCompleteBoxAdded)
                {
                    textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                    isCodeCompleteBoxAdded = false;
                    EnteredKey = "";

                    if (isToolTipControlAdded)
                    {
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                        isToolTipControlAdded = false;
                    }
                }

                if (ischanged)
                {
                    Process_CompileJava();
                    IsChanged = false;
                }

            }

            isClassCreated = false;
            isDataTypeDeclared = false;

            cnt = 0;
            charcount = 0;
            oldsel = 1;

        }


        //*************************************************************************************
        // CheckSemicolon Exist or not
        //*************************************************************************************
        bool CheckSemicolonExistOrNot(String s)
        {
            if (s.Contains(";"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public RichTextBox fieldsrtb = new RichTextBox();


        //*************************************************************************************
        // Process_ClassesMethods Action
        //*************************************************************************************
        public void Process_ClassesMethods_Actions()
        {
            String[] Lines = richTextBox1.Lines;
            int line = textEditor.ActiveTextAreaControl.TextArea.Caret.Line;
            F.ClassesTreeView.Nodes.Clear();
            F.MethodsTreeView.Nodes.Clear();
            for (int i = 0; i < Lines.Length; i++)
            {
                if (richTextBox1.Text == "")
                {
                    F.ClassesTreeView.Nodes.Clear();
                    F.MethodsTreeView.Nodes.Clear();
                }
                else
                {
                    if (this.Text.Contains(".java"))
                    {
                        if (Lines[i].Contains("//"))
                        { }
                        else if (Lines[i].Contains("/*") && Lines[i].Contains("*/"))
                        { }
                        else if (Lines[i].Contains("\""))
                        { }
                        else if (Lines[i].Contains("'"))
                        { }
                        else
                        {
                            if (Lines[i].Contains("class "))
                            {
                                class_str = Lines[i];
                                TreeNode trnode = new TreeNode();
                                trnode.Text = class_str.Trim();
                                F.ClassesTreeView.Nodes.Add(trnode);
                                F.ClassesTreeView.SelectedNode = trnode;
                                F.ClassesTreeView.Scrollable = false;
                            }
                            if (Lines[i].Contains("void "))
                            {
                                method_str = Lines[i];
                                TreeNode trnode = new TreeNode();
                                trnode.Text = method_str.Trim();
                                F.MethodsTreeView.Nodes.Add(trnode);
                                F.MethodsTreeView.SelectedNode = trnode;
                                F.MethodsTreeView.Scrollable = false;
                            }
                        }
                        //check return functions exist or not
                        String[] returnfun = { "int ", "short ", "long ", "byte ", "float ", "double ", "char ", "boolean ", "String " };
                        for (int c = 0; c < returnfun.Length; c++)
                        {
                            if (Lines[i].Contains(returnfun[c]) && CheckSemicolonExistOrNot(Lines[i]) == true)
                            {
                                method_str = Lines[i];
                                TreeNode trnode = new TreeNode();
                                trnode.Text = method_str.Trim();
                                F.MethodsTreeView.Nodes.Add(trnode);
                                F.MethodsTreeView.SelectedNode = trnode;
                                F.MethodsTreeView.Scrollable = false;
                            }
                        }
                    }
                }
            }
        }


        //*************************************************************************************
        // ReadCurrentProjectLocationFolder
        //*************************************************************************************
        public String getAppearanceType()
        {
            String configfile = Application.StartupPath + "\\files\\config.slvjfile";
            String type = "";
            if (File.Exists(configfile))
            {
                using (XmlReader reader = XmlReader.Create(configfile))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name.ToString())
                            {
                                case "Appearance":
                                    type = reader.ReadString();
                                    break;
                            }
                        }
                    }
                }
            }
            return type;
        }


        /// <summary>
        /// set back color to text editor according to their appearance
        /// </summary>
        public void SetBackColorToTextEditor()
        {
            String type = getAppearanceType();
            if(type=="Default"||type=="System"||type=="Light")
            {
                Color color = Color.Black;
                Color backgroundColor = SystemColors.Window;
                HighlightColor highlightColor = new HighlightColor(color, backgroundColor, false, false);
                DefaultHighlightingStrategy highlightingStrategy = textEditor.Document.HighlightingStrategy as DefaultHighlightingStrategy;
                highlightingStrategy.SetColorFor("Default", highlightColor);
                highlightingStrategy.SetColorFor("LineNumbers", highlightColor);
                highlightingStrategy.SetColorFor("FoldLine", highlightColor);

            }
            else if(type=="Dark")
            {
                Color color = Color.Black;
                Color backgroundColor = Color.FromArgb(255,240,240,255);
                HighlightColor highlightColor = new HighlightColor(color, backgroundColor, false, false);
                DefaultHighlightingStrategy highlightingStrategy = textEditor.Document.HighlightingStrategy as DefaultHighlightingStrategy;
                highlightingStrategy.SetColorFor("Default", highlightColor);
                highlightingStrategy.SetColorFor("LineNumbers", highlightColor);
                highlightingStrategy.SetColorFor("FoldLine", highlightColor);
            }
            else if(type=="Night")
            {
                Color color = Color.White;
                Color backgroundColor = Color.FromArgb(0,0,0);
                HighlightColor highlightColor = new HighlightColor(color, backgroundColor, false, false);
                DefaultHighlightingStrategy highlightingStrategy = textEditor.Document.HighlightingStrategy as DefaultHighlightingStrategy;
                highlightingStrategy.SetColorFor("Default", highlightColor);
                highlightingStrategy.SetColorFor("LineNumbers", highlightColor);
                highlightingStrategy.SetColorFor("FoldLine", highlightColor);
            }


            if (type == "Default" || type == "System")
            {
                CodeCompleteBox.BackColor = SystemColors.Window;
                CodeCompleteBox.ForeColor = Color.Black;

                ToolTipControl.StartColor = Color.FromArgb(240, 240, 240);
                ToolTipControl.EndColor = Color.FromArgb(220, 220, 220);
                ToolTipControl.Transparent1 = 255;
                ToolTipControl.Transparent2 = 255;
                ToolTipControl.BorderColor = Color.FromArgb(160, 160, 160);
            }
            else if (type == "Light")
            {
                CodeCompleteBox.BackColor = Color.FromArgb(255, 222, 255, 255);
                CodeCompleteBox.ForeColor = Color.FromArgb(0, 0, 130);

                ToolTipControl.StartColor = Color.FromArgb(255,255,255);
                ToolTipControl.EndColor = Color.FromArgb(172, 236, 255);
                ToolTipControl.Transparent1 = 255;
                ToolTipControl.Transparent2 = 255;
                ToolTipControl.BorderColor = Color.FromArgb(160, 160, 160);
            }
            else if (type == "Dark")
            {
                CodeCompleteBox.BackColor = Color.FromArgb(230, 230, 240);
                CodeCompleteBox.ForeColor = Color.FromArgb(220, 20, 20);

                ToolTipControl.StartColor = Color.FromArgb(230,230,230);
                ToolTipControl.EndColor = Color.FromArgb(200,200,250);
                ToolTipControl.Transparent1 = 255;
                ToolTipControl.Transparent2 = 255;
                ToolTipControl.BorderColor = Color.FromArgb(200, 180, 100);
            }
            else if (type == "Night")
            {
                CodeCompleteBox.BackColor = Color.FromArgb(40, 40, 40);
                CodeCompleteBox.ForeColor = Color.FromArgb(250, 250, 250);

                ToolTipControl.StartColor = Color.FromArgb(50,50,50);
                ToolTipControl.EndColor = Color.FromArgb(80,80,80);
                ToolTipControl.Transparent1 = 255;
                ToolTipControl.Transparent2 = 255;
                ToolTipControl.ToolTipColor = Color.White;
                ToolTipControl.BorderColor = Color.FromArgb(200, 180, 100);
            }
        }

        private void textEditor_Load(object sender, EventArgs e)
        {
            SetBackColorToTextEditor();
        }



        //*********************************************************************
        //  CodeCompleteBox KeyDown events function
        //*********************************************************************
        private void CodeCompleteBox_KeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    if (isCodeCompleteBoxAdded)
                    {
                        if (CodeCompleteBox.SelectedItem.ToString().StartsWith(EnteredKey))
                        {
                            if (EnteredKey != "")
                            {
                                if (EnteredKey.Length == 1)
                                {
                                    int sel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                                    String text = CodeCompleteBox.SelectedItem.ToString();
                                    text = text.Remove(0, 1);
                                    textEditor.Document.Insert(sel, text + " ");
                                    textEditor.ActiveTextAreaControl.TextArea.Caret.Column = sel + (text + " ").Length;
                                    textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                                    isCodeCompleteBoxAdded = false;

                                    cnt = 0;
                                    charcount = 0;
                                    oldsel = 1;

                                    this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                                    this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                                    if (isToolTipControlAdded)
                                    {
                                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                                        isToolTipControlAdded = false;
                                    }

                                    ischanged = true;
                                }
                                else if (EnteredKey.Length == 2)
                                {
                                    int sel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                                    String text = CodeCompleteBox.SelectedItem.ToString();
                                    text = text.Remove(0, 2);
                                    textEditor.Document.Insert(sel, text + " ");
                                    textEditor.ActiveTextAreaControl.TextArea.Caret.Column = sel + (text + " ").Length;
                                    textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                                    isCodeCompleteBoxAdded = false;

                                    cnt = 0;
                                    charcount = 0;
                                    oldsel = 1;

                                    this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                                    this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                                    if (isToolTipControlAdded)
                                    {
                                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                                        isToolTipControlAdded = false;
                                    }

                                    ischanged = true;

                                }
                                else if (EnteredKey.Length == 3)
                                {
                                    int sel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                                    String text = CodeCompleteBox.SelectedItem.ToString();
                                    text = text.Remove(0, 3);
                                    textEditor.Document.Insert(sel, text + " ");
                                    textEditor.ActiveTextAreaControl.TextArea.Caret.Column = sel + (text + " ").Length;
                                    textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                                    isCodeCompleteBoxAdded = false;

                                    cnt = 0;
                                    charcount = 0;
                                    oldsel = 1;

                                    this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                                    this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                                    if (isToolTipControlAdded)
                                    {
                                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                                        isToolTipControlAdded = false;
                                    }

                                    ischanged = true;

                                }
                                else
                                {
                                    int sel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                                    String text = CodeCompleteBox.SelectedItem.ToString();
                                    if (text.Contains(EnteredKey))
                                    {
                                        text = text.Replace(EnteredKey, "");
                                    }
                                    textEditor.Document.Insert(sel, text + " ");
                                    textEditor.ActiveTextAreaControl.TextArea.Caret.Column = sel + (text + " ").Length;
                                    textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                                    isCodeCompleteBoxAdded = false;

                                    cnt = 0;
                                    charcount = 0;
                                    oldsel = 1;

                                    this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                                    this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                                    if (isToolTipControlAdded)
                                    {
                                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                                        isToolTipControlAdded = false;
                                    }

                                    ischanged = true;

                                }
                            }
                        }
                        else
                        {
                            int sel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                            textEditor.Document.Insert(sel," ");
                            textEditor.ActiveTextAreaControl.TextArea.Caret.Column = sel + (" ").Length;
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                            isCodeCompleteBoxAdded = false;

                            cnt = 0;
                            charcount = 0;
                            oldsel = 1;

                            if (isToolTipControlAdded)
                            {
                                textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                                isToolTipControlAdded = false;
                            }

                            ischanged = true;
                        }
                    }
                    break;


                case Keys.Enter:
                    if (isCodeCompleteBoxAdded)
                    {
                        if (CodeCompleteBox.SelectedItem.ToString().StartsWith(EnteredKey))
                        {
                            if (EnteredKey != "")
                            {
                                if (EnteredKey.Length == 1)
                                {
                                    int sel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                                    String text = CodeCompleteBox.SelectedItem.ToString();
                                    text = text.Remove(0, 1);
                                    textEditor.Document.Insert(sel, text);
                                    textEditor.ActiveTextAreaControl.TextArea.Caret.Column = sel + text.Length;
                                    textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                                    isCodeCompleteBoxAdded = false;
                                    
                                    cnt = 0;
                                    charcount = 0;
                                    oldsel = 1;

                                    this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                                    this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                                    if (isToolTipControlAdded)
                                    {
                                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                                        isToolTipControlAdded = false;
                                    }

                                    ischanged = true;

                                }
                                else if (EnteredKey.Length == 2)
                                {
                                    int sel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                                    String text = CodeCompleteBox.SelectedItem.ToString();
                                    text = text.Remove(0, 2);
                                    textEditor.Document.Insert(sel, text);
                                    textEditor.ActiveTextAreaControl.TextArea.Caret.Column = sel + text.Length;
                                    textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                                    isCodeCompleteBoxAdded = false;

                                    cnt = 0;
                                    charcount = 0;
                                    oldsel = 1;

                                    this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                                    this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                                    if (isToolTipControlAdded)
                                    {
                                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                                        isToolTipControlAdded = false;
                                    }

                                    ischanged = true;


                                }
                                else if (EnteredKey.Length == 3)
                                {
                                    int sel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                                    String text = CodeCompleteBox.SelectedItem.ToString();
                                    text = text.Remove(0, 3);
                                    textEditor.Document.Insert(sel, text);
                                    textEditor.ActiveTextAreaControl.TextArea.Caret.Column = sel + text.Length;
                                    textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                                    isCodeCompleteBoxAdded = false;

                                    cnt = 0;
                                    charcount = 0;
                                    oldsel = 1;

                                    this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                                    this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                                    if (isToolTipControlAdded)
                                    {
                                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                                        isToolTipControlAdded = false;
                                    }

                                    ischanged = true;

                                }
                                else
                                {
                                    int sel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                                    String text = CodeCompleteBox.SelectedItem.ToString();
                                    if (text.Contains(EnteredKey))
                                    {
                                        text = text.Replace(EnteredKey, "");
                                    }
                                    textEditor.Document.Insert(sel, text);
                                    textEditor.ActiveTextAreaControl.TextArea.Caret.Column = sel + text.Length;
                                    textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                                    isCodeCompleteBoxAdded = false;

                                    cnt = 0;
                                    charcount = 0;
                                    oldsel = 1;

                                    this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                                    this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                                    if (isToolTipControlAdded)
                                    {
                                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                                        isToolTipControlAdded = false;
                                    }

                                    ischanged = true;

                                }
                            }
                        }
                        else
                        {
                            int sel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                            textEditor.Document.Insert(sel, "");
                            textEditor.ActiveTextAreaControl.TextArea.Caret.Column = sel + ("").Length;
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                            isCodeCompleteBoxAdded = false;

                            cnt = 0;
                            charcount = 0;
                            oldsel = 1;

                            if (isToolTipControlAdded)
                            {
                                textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                                isToolTipControlAdded = false;
                            }

                            ischanged = true;

                        }
                    }
                    break;

                // if Left key is down then remove CodeCompleteBox from texteditor
                case Keys.Left:
                    if (isCodeCompleteBoxAdded)
                    {
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;

                        EnteredKey = "";
                        cnt = 0;
                        charcount = 0;
                        oldsel = 1;

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }

                        ischanged = true;

                    }
                    break;

                // if Right key is down then remove CodeCompleteBox from texteditor
                case Keys.Right:
                    textEditor.ActiveTextAreaControl.TextArea.Caret.Column = 4;
                    if (isCodeCompleteBoxAdded)
                    {
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;

                        EnteredKey = "";
                        cnt = 0;
                        charcount = 0;
                        oldsel = 1;

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }

                        ischanged = true;

                    }
                    break;

                case Keys.Escape:
                    if (isCodeCompleteBoxAdded)
                    {
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;

                        EnteredKey = "";
                        cnt = 0;
                        charcount = 0;
                        oldsel = 1;

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }

                        ischanged = true;

                    }
                    break;


                case Keys.Back:
                    if (isCodeCompleteBoxAdded)
                    {
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;

                        EnteredKey = "";
                        cnt = 0;
                        charcount = 0;
                        oldsel = 1;

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }

                        ischanged = true;

                    }
                    break;
            }
        }


        /// <summary>
        /// code complete box key up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CodeCompleteBox_KeyUp(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                    if (isCodeCompleteBoxAdded)
                    {
                        ToolTipControl.Visible = true;
                        this.ProcessToolTips(CodeCompleteBox.SelectedItem.ToString());
                    }
                    break;

                case Keys.F2:
                    if (isCodeCompleteBoxAdded)
                    {
                        this.InsertingCodeSnippetCodes();
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;
                        EnteredKey = "";

                        e.Handled = true;

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }
                    }
                    break;

                case Keys.F3:
                    this.CreateClassObject_And_SetToolTips();
                    break;
            }
        }


        //*********************************************************************
        //  CodeCompleteBox KeyPress events function
        //*********************************************************************
        static int cnt = 0;
        static int oldsel = 0;
        static int charcount = 0;
        private void CodeCompleteBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            String str = e.KeyChar.ToString();

            // in this event we must insert pressed key to texteditor because Focus is on CodeCompleteBox
            char ch;
            for (ch = 'a'; ch <= 'z'; ch++)
            {
                if (str == ch.ToString())
                {
                    charcount++;
                    // first check pressed key is not Space,Enter,Escape & Back
                    // Space=32, Enter=13, Escape=27, Back=8
                    if (Convert.ToInt32(e.KeyChar) != 13 && Convert.ToInt32(e.KeyChar) != 32 && Convert.ToInt32(e.KeyChar) != 27 && Convert.ToInt32(e.KeyChar) != 8)
                    {
                        if (isCodeCompleteBoxAdded)
                        {
                            cnt++;
                            int aa = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                            String s = textEditor.ActiveTextAreaControl.TextArea.Document.GetCharAt(aa).ToString();
                            
                            if(cnt==1)
                            {
                                oldsel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                            }

                            if (cnt < 2)
                            {
                                int sel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                                textEditor.Document.Insert(sel, ch.ToString());
                                textEditor.ActiveTextAreaControl.TextArea.Caret.Column = sel;
                            }
                            else
                            {
                                textEditor.Document.Insert(oldsel+1, ch.ToString());
                                textEditor.ActiveTextAreaControl.TextArea.Caret.Column = oldsel+charcount;
                                oldsel++;
                            }

                            textEditor.ActiveTextAreaControl.TextArea.Caret.Column = oldsel;
                            // concat the EnteredKey and pressed key on CodeCompleteBox
                            EnteredKey = EnteredKey + str;
                            
                            // search item in CodeCompleteBox which starts with EnteredKey and set it to selected
                            foreach (String item in CodeCompleteBox.Items)
                            {
                                if (item.StartsWith(EnteredKey))
                                {
                                    CodeCompleteBox.SelectedItem = item;
                                    this.ProcessToolTips(item);
                                    break;
                                }
                            }

                        }
                    }

                    // if pressed key is Back then set focus to texteditor 
                    else if (Convert.ToInt32(e.KeyChar) == 8)
                    {
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;

                        EnteredKey = "";
                        textEditor.Focus();
                        cnt = 0;
                        charcount = 0;
                        oldsel = 1;

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }

                    }

                      // if pressed key is not Back then remove CodeCompleteBox from texteditor
                    else if (Convert.ToInt32(e.KeyChar) != 8)
                    {
                        if (isCodeCompleteBoxAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                            isCodeCompleteBoxAdded = false;

                            EnteredKey = "";
                            cnt = 0;
                            charcount = 0;
                            oldsel = 1;

                            if (isToolTipControlAdded)
                            {
                                textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                                isToolTipControlAdded = false;
                            }

                        }
                    }
                }

                else if(str==ch.ToString().ToUpper())
                {
                    charcount++;
                    // first check pressed key is not Space,Enter,Escape & Back
                    // Space=32, Enter=13, Escape=27, Back=8
                    if (Convert.ToInt32(e.KeyChar) != 13 && Convert.ToInt32(e.KeyChar) != 32 && Convert.ToInt32(e.KeyChar) != 27 && Convert.ToInt32(e.KeyChar) != 8)
                    {
                        if (isCodeCompleteBoxAdded)
                        {
                            cnt++;
                            if (cnt == 1)
                            {
                                oldsel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset; ;
                            }

                            if (cnt < 2)
                            {
                                int sel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                                textEditor.Document.Insert(sel, ch.ToString().ToUpper());
                                textEditor.ActiveTextAreaControl.TextArea.Caret.Column = sel;
                            }
                            else
                            {
                                textEditor.Document.Insert(oldsel + 1, ch.ToString().ToUpper());
                                textEditor.ActiveTextAreaControl.TextArea.Caret.Column = oldsel + charcount;
                                oldsel++;
                            }

                            textEditor.ActiveTextAreaControl.TextArea.Caret.Column = oldsel;
                            // concat the EnteredKey and pressed key on CodeCompleteBox
                            EnteredKey = EnteredKey + str;

                            // search item in CodeCompleteBox which starts with EnteredKey and set it to selected
                            foreach (String item in keywordslist)
                            {
                                if (item.StartsWith(EnteredKey))
                                {
                                    CodeCompleteBox.SelectedItem = item;
                                    break;
                                }
                            }

                        }
                    }

                    // if pressed key is Back then set focus to texteditor
                    else if (Convert.ToInt32(e.KeyChar) == 8)
                    {
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;

                        EnteredKey = "";
                        textEditor.Focus();
                        cnt = 0;
                        charcount = 0;
                        oldsel = 1;

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }

                    }

                      // if pressed key is not Back then remove CodeCompleteBox from texteditor
                    else if (Convert.ToInt32(e.KeyChar) != 8)
                    {
                        if (isCodeCompleteBoxAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                            isCodeCompleteBoxAdded = false;

                            EnteredKey = "";
                            cnt = 0;
                            charcount = 0;
                            oldsel = 1;

                            if (isToolTipControlAdded)
                            {
                                textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                                isToolTipControlAdded = false;
                            }

                        }
                    }
                }
            }
            //  check pressed key on CodeCompleteBox is special character or not
            //   if it is a special character then remove CodeCompleteBox from texteditor
            switch (str)
            {
                case "~":
                case "`":
                case "!":
                case "@":
                case "#":
                case "$":
                case "%":
                case "^":
                case "&":
                case "*":
                case "-":
                case "_":
                case "+":
                case "=":
                case "(":
                case ")":
                case "[":
                case "]":
                case "{":
                case "}":
                case ":":
                case ";":
                case "\"":
                case "'":
                case "|":
                case "\\":
                case "<":
                case ">":
                case ",":
                case ".":
                case "/":
                case "?":
                    if (isCodeCompleteBoxAdded)
                    {
                        int sel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                        textEditor.Document.Insert(sel, str);
                        textEditor.ActiveTextAreaControl.TextArea.Caret.Column = sel + str.Length;

                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;

                        EnteredKey = "";
                        cnt = 0;
                        charcount = 0;
                        oldsel = 1;

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }

                        ischanged = true;

                    }
                    break;
            }

            if(isCodeCompleteBoxAdded)
            {
                for(int i=0;i<=9;i++)
                {
                    if(i.ToString()==str)
                    {
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;

                        EnteredKey = "";
                        cnt = 0;
                        charcount = 0;
                        oldsel = 1;

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }

                        ischanged = true;

                    }
                }
            }
        }




        //*********************************************************************
        //  CodeCompleteBox MouseClick events function
        //*********************************************************************
        private void CodeCompleteBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (isCodeCompleteBoxAdded)
            {
                if (EnteredKey != "")
                {
                    if (EnteredKey.Length == 1)
                    {
                        int sel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                        String text = CodeCompleteBox.SelectedItem.ToString();
                        text = text.Remove(0, 1);
                        textEditor.Document.Insert(sel, text);
                        textEditor.ActiveTextAreaControl.TextArea.Caret.Column = sel + text.Length;
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;

                        cnt = 0;
                        charcount = 0;
                        oldsel = 1;

                        this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                        this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }
                        ischanged = true;
                    }
                    else if (EnteredKey.Length == 2)
                    {
                        int sel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                        String text = CodeCompleteBox.SelectedItem.ToString();
                        text = text.Remove(0, 2);
                        textEditor.Document.Insert(sel, text);
                        textEditor.ActiveTextAreaControl.TextArea.Caret.Column = sel + text.Length;
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;

                        cnt = 0;
                        charcount = 0;
                        oldsel = 1;

                        this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                        this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }
                        ischanged = true;
                    }
                    else if (EnteredKey.Length == 3)
                    {
                        int sel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                        String text = CodeCompleteBox.SelectedItem.ToString();
                        text = text.Remove(0, 3);
                        textEditor.Document.Insert(sel, text);
                        textEditor.ActiveTextAreaControl.TextArea.Caret.Column = sel + text.Length;
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;

                        cnt = 0;
                        charcount = 0;
                        oldsel = 1;

                        this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                        this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }
                        ischanged = true;
                    }
                    else
                    {
                        int sel = textEditor.ActiveTextAreaControl.TextArea.Caret.Offset;
                        String text = CodeCompleteBox.SelectedItem.ToString();
                        if (text.Contains(EnteredKey))
                        {
                            text = text.Replace(EnteredKey, "");
                        }
                        textEditor.Document.Insert(sel, text);
                        textEditor.ActiveTextAreaControl.TextArea.Caret.Column = sel + text.Length;
                        textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(CodeCompleteBox);
                        isCodeCompleteBoxAdded = false;

                        cnt = 0;
                        charcount = 0;
                        oldsel = 1;

                        this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                        this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                        if (isToolTipControlAdded)
                        {
                            textEditor.ActiveTextAreaControl.TextArea.Controls.Remove(ToolTipControl);
                            isToolTipControlAdded = false;
                        }
                        ischanged = true;
                    }
                }
            }
        }



    }
}

