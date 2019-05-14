using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestTask1
{
    public partial class Main_Form : Form
    {
        string connectionString = @"Data Source =DESKTOP-2N1E70M;Initial Catalog = DATABASE;Integrated Security = True";
        Form_rename form_Rename = new Form_rename();
        Form_dataBase form_Data = new Form_dataBase();
        public Main_Form()
        {
            InitializeComponent();
        }
        public void CheckMenuFontCharacterStyle()
        {
            if (richTextBox1.SelectionFont.Bold == true)
                boldToolStripMenuItem.Checked = true;
            else boldToolStripMenuItem.Checked = false;


            if (richTextBox1.SelectionFont.Italic == true)
                italicToolStripMenuItem.Checked = true;
            else italicToolStripMenuItem.Checked = false;

            if (richTextBox1.SelectionFont.Underline == true)
                underlineToolStripMenuItem.Checked = true;
            else underlineToolStripMenuItem.Checked = false;

            if (richTextBox1.SelectionFont.Strikeout == true)
                strikeoutToolStripMenuItem.Checked = true;
            else strikeoutToolStripMenuItem.Checked = false;
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (richTextBox1.Text != "")
            {
                DialogResult dialogResult = MessageBox.Show(
                 "Save current file?",
                 "Message",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);

                if (dialogResult == DialogResult.Yes && saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SaveFile(saveFileDialog.FileName);

                }
            }
            this.richTextBox1.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        { 
           if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.RichText);

                }
                catch (ArgumentException ex)
                {
                    richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);

                }
                this.Text = openFileDialog1.FileName;

            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                }
                catch (ArgumentException ex)
                {
                    richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);

                }
                this.Text = saveFileDialog1.FileName;

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }       

        private void getFileFromDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form_Data.ShowDialog();
            richTextBox1.Clear();
            richTextBox1.Text = form_Data.content;
            Text = form_Data.name;
        }

        private void dessFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form_Rename.ShowDialog();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"INSERT INTO DataBaseFiles (FileName,FileData)VALUES (@FileName, @FileData)";
                command.Parameters.AddWithValue("@FileName", form_Rename.fileName);              
                command.Parameters.AddWithValue("@FileData", richTextBox1.Text);
                
               

                command.ExecuteNonQuery();
            }
        }

        private void otherFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public static byte[] StringtToByteArray(string str)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, str);
                return ms.ToArray();
            }
        }
        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void CopeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog1.Font;
            }
        }

        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorDialog1.Color;
            }
        }

        private void LeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void RigthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void CenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void BoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newFontStyle;

                if (richTextBox1.SelectionFont.Bold == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                    newFontStyle = FontStyle.Bold;

                richTextBox1.SelectionFont = new Font(currentFont.FontFamily,
                    currentFont.Size, newFontStyle);

                CheckMenuFontCharacterStyle();
            }

        }
        private void ItalicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newFontStyle;

                CheckMenuFontCharacterStyle();
                if (richTextBox1.SelectionFont.Italic == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                    newFontStyle = FontStyle.Italic;
                richTextBox1.SelectionFont = new Font(currentFont.FontFamily,
                    currentFont.Size, newFontStyle);

                CheckMenuFontCharacterStyle();
            }
        }
        private void UnderLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newFontStyle;

                CheckMenuFontCharacterStyle();
                if (richTextBox1.SelectionFont.Underline == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                    newFontStyle = FontStyle.Underline;
                richTextBox1.SelectionFont = new Font(currentFont.FontFamily,
                    currentFont.Size, newFontStyle);

                CheckMenuFontCharacterStyle();
            }
        }

        private void StrikeoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newFontStyle;

                if (richTextBox1.SelectionFont.Strikeout == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                    newFontStyle = FontStyle.Strikeout;
                richTextBox1.SelectionFont = new Font(currentFont.FontFamily,
                    currentFont.Size, newFontStyle);

                CheckMenuFontCharacterStyle();
            }
        }

        private void EdiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void RedoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }
    }
}
