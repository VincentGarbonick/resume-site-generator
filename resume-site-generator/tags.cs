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
        public virtual string generateLine()
        {
            Type tagType = this.GetType();
            var attributeList = tagType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            // <tagName attrs> innerText </tagnmae> 
            // this is not the most efficient method, but that's fine 
            // we know the last elements appearing in attribute list are tagName and display name, and we want to get these out of the way first
            
            int i = attributeList.Length - 2; // we are setting our index to the second to last element because we don't care about displayName 
            string tag = "";
            string stringInnerText= "";
            string attributeString = "";
            string builtString = "";

            while(i != attributeList.Length - 3)
            {
                //System.Diagnostics.Debug.WriteLine(attributeList[i].Name);
                //System.Diagnostics.Debug.WriteLine(attributeList[i].GetValue(this));

                if(attributeList[i].Name == "tagName")
                {
                    tag = (string)attributeList[i].GetValue(this);
                }
                i--;
            }
            while(i != -1)
            {
                //System.Diagnostics.Debug.WriteLine(attributeList[i].Name);
                //System.Diagnostics.Debug.WriteLine(attributeList[i].GetValue(this));
                if (attributeList[i].Name != "innerText")
                {
                    
                    if(attributeList[i].PropertyType == typeof(System.String))
                    {
                        attributeString = String.Format("{0} {1}=\"{2}\"", attributeString, attributeList[i].Name, (string)attributeList[i].GetValue(this));
                    }
                    else if (attributeList[i].PropertyType == typeof(bool))
                    {
                        bool attrBoolValue = (bool)attributeList[i].GetValue(this);
                        string attrStringValue = "";
                        if(attrBoolValue)
                        {
                            attrStringValue = "true";
                        }
                        else
                        {
                            attrStringValue = "false";
                        }
                        attributeString = String.Format("{0} {1}=\"{2}\"", attributeString, attributeList[i].Name, attrStringValue);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("New type found");
                    }

                }
                else if (attributeList[i].Name == "innerText")
                {
                    stringInnerText = (string)attributeList[i].GetValue(this);
                }

                i--;
            }

            // if we have innertext 
            if(stringInnerText != "")
            {
                builtString = string.Format("<{0} {1}>{2}</{3}>", tag, attributeString, stringInnerText, tag);
            }
            else
            {
                builtString = string.Format("<{0} {1}></{2}>", tag, attributeString, tag);
            }

            System.Diagnostics.Debug.WriteLine(builtString);
            return builtString;
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

        public override string generateLine()
        {
            string builtString = base.generateLine();

            // get header size 
            Type tagType = this.GetType();
            var attributeList = tagType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            int size = 1; // our default size 
            for(int i = 0; i < attributeList.Length; i++)
            {
                if(attributeList[i].Name == "size")
                {
                    size = (int)attributeList[i].GetValue(this);
                    break;
                }
            }

            // get index of our first H so we can insert the size 
            int indexOfFirstH = builtString.IndexOf("h", 0);
            //                builtString = string.Format("<{0} {1}>{2}</{3}>", tag, attributeString, stringInnerText, tag);

            builtString = builtString.Insert(indexOfFirstH + 1, string.Format("{0} ", size.ToString()));
            int indexOfLastH = builtString.LastIndexOf("h");
            builtString = builtString.Insert(indexOfLastH + 1, size.ToString());

            System.Diagnostics.Debug.WriteLine(builtString);
            return builtString;
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
            tagName = "button";
            displayName = "button";
            name = "button";
            href = "";
            action = "";
            value = "button";
            disabled = false;
            innerText = "";
        }
    }
}
