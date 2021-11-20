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
    // parent class for html elements to generate 
    class HElement
    {
        public string tagName { get; set; }

        // overides so our listbox displays the name of our element
        public override string ToString()
        {
            return tagName;
        }
    }

    class Paragraph : HElement
    { 
        public string innerText { get; set; }

        // default constructor 
        public Paragraph()
        {
            tagName = "paragraph";
        }
    }

    class Header : HElement
    { 
        public string innerText { get; set; }
        public int size { get; set; }

        public Header()
        {
            tagName = "header";
            size = 1;
        }
    }


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // initialize our objects to put into list 1 
            Paragraph myParagraph = new Paragraph();
            Header myHeader = new Header();

            listBox1.Items.Add(myParagraph);
            listBox1.Items.Add(myHeader);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("mousing down");
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

        private void listBox2_DragDrop(object sender, DragEventArgs e)
        {
            listBox2.Items.Add(e.Data.GetData(DataFormats.Text).ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine(listBox2.Items[2].ToString());
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("not there yet");

            }
        }
    }
}
