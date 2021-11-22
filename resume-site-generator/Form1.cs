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
        // these are the variables for storing the index position of the list objects
        public int DOWN_INDEX= -1;
        public int NEW_INDEX = -1;
        

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
            //GENERATE NAME BASED ON ELEMENT OPTION FOR POPUP FORM 

            // TODO: get some kind of FX for draggin and dropping in list two
            // TODO: add helper text to all tag attributes for propertyviewer 
            
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
                System.Diagnostics.Debug.WriteLine(listBox2.Items[5].ToString());
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
            if (e.Button == MouseButtons.Right)
            {
                int index = listBox2.IndexFromPoint(e.Location);

                if (index == -1)
                {
                    return;
                }
                else
                {
                    DOWN_INDEX = index;
                }
                
            }
        }
        private void listBox2_MouseMove(object sender, MouseEventArgs e)
        {

        }
        private void listBox2_MouseUp(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Right)
            {
                int index = listBox2.IndexFromPoint(e.Location);
                if (index == -1)
                {
                    return;
                }
                else
                {
                    NEW_INDEX = index;

                    // For information on drag and drop implementation, check the dragAndDropExplain.txt in the project folder
                    if(NEW_INDEX != DOWN_INDEX)
                    {
                        if (NEW_INDEX < DOWN_INDEX)
                        {
                            var tempDownObject = listBox2.Items[DOWN_INDEX];
                            var tempCurrentObject = listBox2.Items[NEW_INDEX];

                            // remove and leave holes 
                            listBox2.Items.RemoveAt(DOWN_INDEX);
                            listBox2.Items.RemoveAt(NEW_INDEX);

                            // put the down object at the current index 
                            listBox2.Items.Insert(NEW_INDEX,tempCurrentObject);

                            // move all other objects down the list , to fill holes, starting at lowest number
                            for (int i = DOWN_INDEX; i == NEW_INDEX; i--)
                            {
                                var tempMoveDownObject = listBox2.Items[i + 1];
                                listBox2.Items.Insert(i, tempMoveDownObject);
                            }

                            // fill the second to last "hole" with the old data 
                            listBox2.Items.Insert(NEW_INDEX, tempDownObject);
                        }
                    
                    }
                }
            }     
        }

        private void listBox2_Click(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = listBox2.Items[listBox2.SelectedIndex];
        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // resets the display name of item you're currently working on 
            var tempObj = listBox2.Items[listBox2.SelectedIndex];
            listBox2.Items[listBox2.SelectedIndex] = tempObj;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < listBox2.Items.Count; i++)
            {
                var tempObj = listBox2.Items[i];
                listBox2.Items[i] = tempObj;
            }
        }
    }
}
