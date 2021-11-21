using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace resume_site_generator
{

        public class DragItem
        {
            public ListBox Client;
            public int Index;
            public object Item;

            public DragItem(ListBox client, int index, object item)
            {
                Client = client;
                Index = index;
                Item = item;
            }
     
        }
}
