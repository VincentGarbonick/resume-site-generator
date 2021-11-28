using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime;
using System.Reflection;

namespace resume_site_generator
{
    class HElement
    {
        protected string tagName { get; set; } // protected so user cannot edit this later
        public string displayName { get; set; }
        // overides so our listbox displays the name of our element
        public override string ToString()
        {
            return displayName;
        }

        // inhereted method for generating a line of HTML to be put in our auto generated file 
        public virtual void generateLine()
        {
            Type tagType = this.GetType();
            var attributeList = tagType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            // <tagName attrs> innerText </tagnmae> 
            // this is not the most efficient method, but that's fine 
            for (int i = 0; i < attributeList.Length; i++)
            {
                System.Diagnostics.Debug.WriteLine(attributeList[i].Name);
                System.Diagnostics.Debug.WriteLine(attributeList[i].GetValue(this));
            }
        }
    }

    class Paragraph : HElement
    {
        public string innerText { get; set; }
        

        // default constructor 
        public Paragraph()
        {
            displayName = "paragraph";
            
            tagName = "p";
        }
    }

    class Header : HElement
    {
        public string innerText { get; set; }
        public int size;

        public Header()
        {
            tagName = "h";
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
        public string innerText { get; set; }
        public Button()
        {
            tagName = "buttonII";
            displayName = "buttonII";
            name = "button";
            href = "";
            action = "";
            value = "button";
            disabled = false;
            innerText = "";
        }
    }
}
