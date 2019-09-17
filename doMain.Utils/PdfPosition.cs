using System;
using System.Collections.Generic;
using System.Linq;

namespace doMain.Utils
{
    public class PdfPosition
    {
        public float llx
        {
            get;
            set;
        }

        public float lly
        {
            get;
            set;
        }

        public float urx
        {
            get;
            set;
        }

        public float ury
        {
            get;
            set;
        }

        //public float Width
        //{
        //    get;
        //    set;
        //}

        //public float Height
        //{
        //    get;
        //    set;
        //}

        //public PdfPosition(float width, float height)
        //{
        //    this.Width = width; 
        //    this.Height = height;
            
        //}
        public PdfPosition()
        {
            this.llx = 0f;
            this.lly = 0f;
            this.urx = 1f;
            this.ury = 1f;
        }

        public PdfPosition(float pllx, float plly, float purx, float pury)
        {
            this.llx = pllx;
            this.lly = plly;
            this.urx = purx;
            this.ury = pury;
        }
    }
}
