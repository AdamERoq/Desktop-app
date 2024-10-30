using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop_app.Driver;
using Desktop_app.Pages;

namespace Desktop_app.Pages
{
    public  class PageManager : Base
    {
        public FilePage FilePage => new(driver);
        public PDFPage PDFPage => new(driver);
        public WordPage WordPage => new(driver);
        public HomePage Homepage => new(driver);
        public PageManager(Drivers driver) : base(driver)
        { 
        }
        

    }
}
