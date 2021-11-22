using System;
using System.Collections.Generic;
using System.Text;

namespace resume_site_generator
{
    class HElement
    {
        public string tagName { get; set; }
        public string displayName { get; set; }
        // overides so our listbox displays the name of our element
        public override string ToString()
        {
            return displayName;
        }
    }

    class Paragraph : HElement
    {
        public string innerText { get; set; }

        // default constructor 
        public Paragraph()
        {
            displayName = "paragraph";
            
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
            displayName = "header";
            size = 1;
        }
    }

    class Button : HElement
    {
        public string name { get; set; }
        public string href { get; set; }
        public string action { get; set; }
        public string value { get; set; }
        public bool disabled { get; set; }

        public Button()
        {
            tagName = "button";
            displayName = "button";
            name = "button";
            href = "";
            action = "";
            value = "button";
            disabled = false;
        }
    }

}
