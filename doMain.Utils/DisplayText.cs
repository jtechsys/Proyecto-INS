using System;
using System.Collections.Generic;
using System.Linq;

namespace doMain.Utils
{
    public class DisplayText : Attribute
    {

        public DisplayText(string Text)
        {
            this.text = Text;
        }


        private string text;


        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}
