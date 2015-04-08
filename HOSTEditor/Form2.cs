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
    public partial class Form2 : Form
    {

        private string templatePath = Constants.SaveTemplatePath;
        public string fileContents = "";
        public Form2()
        {
            InitializeComponent();
            btnSave.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string fullPath = this.templatePath + "template_" + txtTemplateName.Text + ".txt";
            if (System.IO.File.Exists(fullPath))
            {
                label2.Text = "Please choose another name";
                label2.Show();
                btnSave.Enabled = false;
            }
            else {
                label2.Hide();
                btnSave.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string templateFullPath = templatePath + "template_" + txtTemplateName.Text + ".txt";
                (new System.IO.FileInfo(templateFullPath)).Directory.Create();
                System.IO.File.WriteAllText(templateFullPath, this.fileContents);

                DialogResult drConfirm = MessageBox.Show("Se creó el template " + templateFullPath, "Todo Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo pasó: " + ex.Message, "Se espicho", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
