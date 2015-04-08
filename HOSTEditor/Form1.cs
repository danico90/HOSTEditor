using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HOSTEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadTemplates();
        }

        private string path = Constants.Path;
        private string backupPath = Constants.BackupPath;
        private string templatePath = Constants.SaveTemplatePath;

        private void Form1_Load(object sender, EventArgs e)
        {
            string text = System.IO.File.ReadAllText(path);
            txtCurrent.Text = text;
            txtNew.Text = text;
            LoadTemplates();
        }

        private void Replace_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("Va a reemplazar la vara?", "Seguro?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    string backupFullPath = backupPath + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt";
                    (new System.IO.FileInfo(backupFullPath)).Directory.Create();
                    System.IO.File.WriteAllText(backupFullPath, txtCurrent.Text);
                    System.IO.File.WriteAllText(path, txtNew.Text);

                    DialogResult drConfirm = MessageBox.Show("Se hizo el reemplazo, si se la peló hay un respaldo en " + backupFullPath, "Todo Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    string text = System.IO.File.ReadAllText(path);
                    txtCurrent.Text = text;
                    txtNew.Text = text;
                }
                catch (Exception ex) {
                    MessageBox.Show("Algo pasó: " + ex.Message, "Se espicho", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }


        }

        private void saveCurrentFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form2 f2 = new Form2())
            {
                f2.fileContents = txtCurrent.Text;
                f2.ShowDialog();
            }
            LoadTemplates();
            
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No se necesita ayuda para usar esta vara.", "No sea caballo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadTemplates() {

            templatesToolStripMenuItem.DropDownItems.Clear();
            if (!System.IO.Directory.Exists(templatePath)) { return; }
            string[] filePaths = System.IO.Directory.GetFiles(templatePath);
            if (filePaths.Length == 0)
            {
                return;
            }
            else {
                ToolStripMenuItem[] items = new ToolStripMenuItem[filePaths.Length];
                for (int i = 0; i < items.Length; i++)
                {
                    string fileName = filePaths[i].Substring(filePaths[i].IndexOf("template_"));
                    items[i] = new ToolStripMenuItem();
                    items[i].Name = "template_" + i.ToString();
                    items[i].Tag = fileName.Replace("template_", "").Replace(".txt", "");
                    items[i].Text = fileName.Replace("template_", "").Replace(".txt", "");
                    items[i].Click += new EventHandler(MenuItemClickHandler);
                }
                templatesToolStripMenuItem.DropDownItems.AddRange(items);
            }

            
        }

        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
                txtNew.Text = System.IO.File.ReadAllText(this.templatePath + "template_" + clickedItem.Tag + ".txt");
            }
            catch (Exception ex){
                MessageBox.Show("Algo pasó: " + ex.Message, "Se espicho", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
