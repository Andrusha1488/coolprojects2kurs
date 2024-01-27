using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Practic6;


public class Figure_changes
{

    public string Name { get; set; }
    public string weight { get; set; }
    public string length { get; set; }

    public void WriteFigure()
    {
        using (StreamWriter sw = File.AppendText(Program.FileName))
        {
            sw.WriteLine(Name);
            sw.WriteLine(weight);
            sw.WriteLine(length);
        }
    }
   

}
