using CourseWorkMT2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkMT2.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new NorthWindContext();
            var ll = db.Regions.ToList();
        }
    }
}
