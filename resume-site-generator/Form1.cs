using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace resume_site_generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("mousing down");
            listBox1.DoDragDrop(listBox1.Text, DragDropEffects.Copy | DragDropEffects.Move);
        }

        private void listBox2_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.Text))
            {
                System.Diagnostics.Debug.WriteLine(e.Data.GetData(DataFormats.Text).ToString());
                e.Effect = DragDropEffects.Copy;
            }
        }
        /*
private void pictureBox1_Click(object sender, EventArgs e)
{
System.Diagnostics.Debug.WriteLine("clicked");
}

private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
{
System.Diagnostics.Debug.WriteLine("mousing down");
pictureBox1.DoDragDrop(pictureBox1.Text, DragDropEffects.Copy | DragDropEffects.Move);
}

private void pictureBox1_MouseEnter(object sender, EventArgs e)
{
pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
}

private void textBox1_DragEnter(object sender, DragEventArgs e)
{
if (e.Data.GetDataPresent(DataFormats.Bitmap) != true)
{
System.Diagnostics.Debug.WriteLine("bbb");
e.Effect = DragDropEffects.Copy;
}
else
{
System.Diagnostics.Debug.WriteLine("aaaaa");
}
}
*/
    }
}
