using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;



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

            //MAKE RETURNER IN CLASS THAT RETURNS THE RENDERED HTML ELEMENT!!!!
            //GENERATE NAME BASED ON ELEMENT OPTION FOR POPUP FORM 

            // TODO: get some kind of FX for draggin and dropping in list two
            // TODO: add helper text to all tag attributes for propertyviewer 
            // TODO: dynamic object method calling 
            // TOOD: add resume button, add image button
            // TODO: change font button
            // TODO: get the window to size properly, probably attributes you can set in form
            // TODO: element to top, element to bottom buttons 
            
            // delete the first listbox item in listbox2 
            listBox2.Items.RemoveAt(0);

            textBox1.Text = Directory.GetCurrentDirectory();
            textBox2.PlaceholderText = "your_file_name";
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

                // this was a fucking nightmare NOTE TO FUTURE VINNIE, .CUR FILES HAVE TO BE 32X32 BIT TO WORK FFS REEEEE WOOOO
                listBox2.Cursor = new Cursor(new System.IO.MemoryStream(Properties.Resources.grabWhite32));
            }
        }
        private void listBox2_MouseUp(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Right)
            {
                listBox2.Cursor = System.Windows.Forms.Cursors.Hand;

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
                        var tempDownObject = listBox2.Items[DOWN_INDEX];
                        var tempCurrentObject = listBox2.Items[NEW_INDEX];

                        if (NEW_INDEX < DOWN_INDEX)
                        {
                            // remove and leave holes 
                            listBox2.Items.RemoveAt(DOWN_INDEX);
                            listBox2.Items.RemoveAt(NEW_INDEX);

                            listBox2.Items.Insert(NEW_INDEX, tempCurrentObject);

                            // move all other objects down the list , to fill holes, starting at lowest number
                            for (int i = DOWN_INDEX; i == NEW_INDEX; i--)
                            {
                                var tempMoveDownObject = listBox2.Items[i + 1];
                                listBox2.Items.Insert(i, tempMoveDownObject);
                            }

                            // fill the second to last "hole" with the old data 
                            listBox2.Items.Insert(NEW_INDEX, tempDownObject);
                        }
                        // we are moving an element "down"
                        else if(NEW_INDEX > DOWN_INDEX)
                        {
                            listBox2.Items.RemoveAt(NEW_INDEX);
                            listBox2.Items.Insert(NEW_INDEX, tempCurrentObject);
                            listBox2.Items.RemoveAt(DOWN_INDEX);

                            for (int i = DOWN_INDEX; i == NEW_INDEX; i++)
                            {
                                var tempMoveDownObject = listBox2.Items[i + 1];
                                listBox2.Items.Insert(i, tempMoveDownObject);
                            }

                            listBox2.Items.Insert(NEW_INDEX, tempDownObject);
                        }
                        else
                        {
                            // if there is some edge case I'm not thinking of, bail everything out
                            listBox2.Items.Insert(DOWN_INDEX, tempDownObject);
                            listBox2.Items.Insert(NEW_INDEX, tempCurrentObject);
                            return;
                        }
                    
                    }
                }
            }     
        }

        private void listBox2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                propertyGrid1.SelectedObject = listBox2.Items[listBox2.SelectedIndex];
            }
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

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            browser.Description = "The folder where the generated HTML file will be.";
            if(browser.ShowDialog () == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = browser.SelectedPath;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox2.Text))
            {
                string fileName = textBox1.Text + "\\" + textBox2.Text + ".html";
                System.Diagnostics.Debug.WriteLine(fileName);
            }
        }

        private void listBox2_MouseHover(object sender, EventArgs e)
        {
            Point point = listBox2.PointToClient(Cursor.Position);
            int index = listBox2.IndexFromPoint(point);
            if (index < 0) return;
            
            //System.Diagnostics.Write
        }
    }
}
