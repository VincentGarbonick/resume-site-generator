﻿using System;
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
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // initialize our objects to put into list 1 

            /*
            Paragraph myParagraph = new Paragraph();
            Header myHeader = new Header();
            Button myButton = new Button();

            listBox1.Items.Add(myParagraph);
            listBox1.Items.Add(myHeader);
            listBox1.Items.Add(myButton);
            */

            // why are we doing this in listBox1? we can easily do this in listbox 2 and just use names for listbox 1...
            // we can ALSO auto name based on the position of the indexes :)
            // https://stackoverflow.com/questions/15322342/mousedown-and-click-conflict
            // left click -> for dragging and dropping
            // right click -> for changing aspects of an element (get right vs left mousebutton working for all forms)
            //MAKE RETURNER IN CLASS THAT RETURNS THE RENDERED HTML ELEMENT!!!!


            // delete the first listbox item in listbox2 
            listBox2.Items.RemoveAt(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          

        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button != MouseButtons.Left) { return; } // only work with leftclick
            
            //System.Diagnostics.Debug.WriteLine("mousing down");
            listBox1.DoDragDrop(listBox1.Text, DragDropEffects.Copy | DragDropEffects.Move);
        }
        private void listBox1_MouseEnter(object sender, EventArgs e)
        {
            listBox1.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private void listBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                // System.Diagnostics.Debug.WriteLine(e.Data.GetData(DataFormats.Text).ToString());
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void listBox2_DragDrop(object sender, DragEventArgs e)
        {
            // this is called reflection
            // https://stackoverflow.com/questions/223952/create-an-instance-of-a-class-from-a-string
            // https://stackoverflow.com/questions/43397926/could-not-load-type-xyz-from-assembly 

            // resume_site_generator.Button, resume-site-generator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null // format from printing out class 
            // TopNamespace.SubNameSpace.ContainingClass+NestedClass, MyAssembly, Version=1.3.0.0, Culture=neutral, PublicKeyToken=b17a5c561934e089  //MSDN format 
            var droppedObject = Activator.CreateInstance("resume-site-generator", "resume_site_generator." + e.Data.GetData(DataFormats.Text).ToString()).Unwrap();
            listBox2.Items.Add(droppedObject);
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

 

        private void listBox2_MouseEnter(object sender, EventArgs e)
        {
            listBox2.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        /*
         * CONTROLS THE LISTBOX2 REORDING FEATURE 
         * 
         * 
         */
        private void listBox2_MouseDown(object sender, MouseEventArgs e)
        {
            ListBox lst = sender as ListBox;

            int index = lst.IndexFromPoint(e.Location);

            // System.Diagnostics.Debug.WriteLine(index);

            lst.SelectedIndex = index;

            // Drag the item.
            DragItem drag_item = new DragItem(lst, index, lst.Items[index]);
            lst.DoDragDrop(drag_item, DragDropEffects.Move);

        }



        private void listBox2_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void listBox2_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}