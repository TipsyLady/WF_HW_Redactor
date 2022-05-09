using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_CW_Redactor
{
    public partial class Form1 : Form
    {
        ToolTip t = new ToolTip();
        //Color c;
        InstalledFontCollection font ;
        public Form1()
        {
            InitializeComponent();
            ToolTip();
            font = new InstalledFontCollection();
            foreach (var item in font.Families)
            {
                FontBox.Items.Add(item.Name);
            }
            //c = this.BackColor;
            menuStrip_Eng.Visible = false;
            ContextMenuStrip = contextMenuStrip2;
            BoldToolStripMenuItemRUS.Checked = false;
            ItalicToolStripMenuItemRUS.Checked = false;
            UnderlineToolStripMenuItemRUS.Checked = false;
            boldToolStripMenuItem.Checked = false;
            italicToolStripMenuItem.Checked = false;
            underlineToolStripMenuItem.Checked = false;
            buttonLang.Text = "English";
            FontBox.SelectedItem = "Times New Roman";
            SizeBox.SelectedItem = "14"; 
            t.SetToolTip(this.buttonLang, "Change the language of menu");
            
        }

        //private void redToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    ToolStripMenuItem it = (ToolStripMenuItem)sender;
        //    if (it.Checked == true) BackColor = Color.Red;
        //    else
        //    {
        //        it.Checked = false;
        //        BackColor = c;
        //    }
        //}

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonLang_Click(object sender, EventArgs e)
        {
            if (buttonLang.Text.CompareTo("English") == 0)
            {
                buttonLang.Text = "Русский";
                ToolTip t = new ToolTip();
                t.SetToolTip(this.buttonLang, "Сменить язык меню");
                ToolTip();
                menuStrip_Eng.Visible = true;
                menuStrip_Rus.Visible = false;
                this.ContextMenuStrip = contextMenuStrip1;
                this.MainMenuStrip = menuStrip_Eng;
            }
            else
            {
                ToolTip t = new ToolTip();
                t.SetToolTip(this.buttonLang, "Change the language of menu");
                buttonLang.Text = "English";
                ToolTip();
                menuStrip_Eng.Visible = false;
                menuStrip_Rus.Visible = true;
                this.ContextMenuStrip = contextMenuStrip2;
                this.MainMenuStrip = menuStrip_Rus;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.Filter = "Text Files (*.txt)|*.txt| All Files(*.*)|*.*";
            if(openFileDialog1.ShowDialog () ==DialogResult.OK)
            {
                if (openFileDialog1.FileName == "") return;
                else
                {
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    { richTextBox1.Text = sr.ReadToEnd(); }
                
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 0;
            if (saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                if (saveFileDialog1.FileName == "") return;
                else
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                    {
                        //this.richTextBox1.SaveFile(this.saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                       sw.Write(richTextBox1.Text);
                    }
                }
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = false;
            fontDialog1.Font = richTextBox1.SelectionFont;
            if (fontDialog1.ShowDialog()==DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog1.Font;
            }
        }

        private void colourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = richTextBox1.SelectionColor;
            if(colorDialog1.ShowDialog()==DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorDialog1.Color;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FontBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FontBox.SelectedIndex != -1 && SizeBox.SelectedIndex != -1)
                richTextBox1.SelectionFont = new Font(font.Families[FontBox.SelectedIndex], Convert.ToInt32(SizeBox.SelectedItem.ToString()));
        }

        private void SizeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FontBox.SelectedIndex != -1 && SizeBox.SelectedIndex != -1)
                richTextBox1.SelectionFont = new Font(font.Families[FontBox.SelectedIndex], Convert.ToInt32(SizeBox.SelectedItem.ToString()));
        }


        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "" && buttonLang.Text == "English")
            {
                DialogResult result = MessageBox.Show("Сохранить изменения ?", "Текстовый редактор", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(this, new EventArgs());
                    richTextBox1.Clear();
                }
                if (result == DialogResult.No)
                {
                    richTextBox1.Clear();
                }
            }
                if(richTextBox1.Text != "" && buttonLang.Text == "Русский")
                {
                    DialogResult result = MessageBox.Show("Do you want to safe the changes?", "Text Redactor ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        saveToolStripMenuItem_Click(this, new EventArgs());
                        richTextBox1.Clear();
                    }
                    if (result == DialogResult.No)
                    {
                        richTextBox1.Clear();
                    }
                }
            
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
                richTextBox1.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != "")
                richTextBox1.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(richTextBox1.SelectedText != "")
            {
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                toolStripMenuItemCut_RUS.Enabled = true;
                toolStripMenuItemCopy_RUS.Enabled = true;
                copyToolStripMenuItem1.Enabled = true;
                cutToolStripMenuItem1.Enabled = true;
            }
            else
            {
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                toolStripMenuItemCut_RUS.Enabled = false;
                toolStripMenuItemCopy_RUS.Enabled = false;
                copyToolStripMenuItem1.Enabled = false;
                cutToolStripMenuItem1.Enabled = false;
            }
        }
        private void toolStripButtonBold_Click(object sender, EventArgs e)
        {

            if (richTextBox1.SelectionFont.Bold)
            {
                boldToolStripMenuItem.Checked = false;
                BoldToolStripMenuItemRUS.Checked = false;
                toolStripButtonBold.Checked = false;
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Regular);
            }

            else
            {
                boldToolStripMenuItem.Checked = true;
                BoldToolStripMenuItemRUS.Checked = true;
                toolStripButtonBold.Checked = true;
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Bold | richTextBox1.SelectionFont.Style);
            }
            richTextBox1.Focus();
        }

        private void toolStripButtonItalic_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont.Italic)
            {
                italicToolStripMenuItem.Checked = false;
                ItalicToolStripMenuItemRUS.Checked = false;
                toolStripButtonItalic.Checked = false;
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Regular);
            }

            else if (richTextBox1.SelectionFont.Italic==false)
            {
                italicToolStripMenuItem.Checked = true;
                ItalicToolStripMenuItemRUS.Checked = true;
                toolStripButtonItalic.Checked = true;
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Italic );
            }
            
            richTextBox1.Focus();

        }

        private void toolStripButtonUnderline_Click(object sender, EventArgs e)
        {

            if (richTextBox1.SelectionFont.Underline)
            {
                underlineToolStripMenuItem.Checked = false;
                UnderlineToolStripMenuItemRUS.Checked = false;
                toolStripButtonUnderline.Checked = false;
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Regular);
            }
            else
            {
                underlineToolStripMenuItem.Checked = true;
                UnderlineToolStripMenuItemRUS.Checked = true;
                toolStripButtonUnderline.Checked = true;
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Underline | richTextBox1.SelectionFont.Style);
            } 
                    
            richTextBox1.Focus();
        }

        private void toolStripButtonLeft_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void toolStripButtonCenter_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void toolStripButtonRight_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void toolStripButtonClear_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }


        private void ToolTip()
        {
            if (buttonLang.Text.CompareTo("English") == 0)
            {
                toolStripButtonNew.ToolTipText = "Создать новый файл";
                toolStripButtonOpen.ToolTipText = "Открыть файл";
                toolStripButtonSave.ToolTipText = "Сохранить файл";
                toolStripButtonClose.ToolTipText = "Закрыть программу";
                toolStripButtonBold.ToolTipText = "Полужирный";
                toolStripButtonItalic.ToolTipText = "Курсив";
                toolStripButtonUnderline.ToolTipText = "Подчеркнутый";
                toolStripButtonLeft.ToolTipText = "Выравнить текст слева";
                toolStripButtonRight.ToolTipText = "Выравнить текст справа";
                toolStripButtonCenter.ToolTipText = "Выравнить текст по-центру";
                toolStripButtonClear.ToolTipText = "Очистить весь текст";
                toolStripButtonColour.ToolTipText = "Изменить цвет текста";
                FontBox.ToolTipText = "Выбрать тип шрифта";
                SizeBox.ToolTipText = "Выбрать размер шрифта";
            }
            else
            {
                toolStripButtonNew.ToolTipText = "Create new file";
                toolStripButtonOpen.ToolTipText = "Open file";
                toolStripButtonSave.ToolTipText = "Save file";
                toolStripButtonClose.ToolTipText = "Close file redactor";
                toolStripButtonBold.ToolTipText = "Bold";
                toolStripButtonItalic.ToolTipText = "Italic";
                toolStripButtonUnderline.ToolTipText = "Underline";
                toolStripButtonLeft.ToolTipText = "Align text to left";
                toolStripButtonRight.ToolTipText = "Align text to right";
                toolStripButtonCenter.ToolTipText = "Align text to right";
                toolStripButtonClear.ToolTipText = "Delete all text";
                toolStripButtonColour.ToolTipText = "Change text colour";
                FontBox.ToolTipText = "Choose text font";
                SizeBox.ToolTipText = "Choose text size";
            }
        }

        //Sender - источник события (item -red), eventArgs - объект обрабатывается, аргументы объекта
    }
}
