using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock
{
    public class Alarm
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public Week Week { get; set; }
        public string Filename { get; set; }
        public string Message { get; set; }
        
        public Alarm() 
        {
            this.Week = new Week();
        }
        public Alarm(Alarm other)
        { 
            this.Date = other.Date;
            this.Time = other.Time;
            this.Week = new Week(other.Week);
            this.Filename = other.Filename;
            this.Message = other.Message;
        }

        public override string ToString()
        {
            string info = "";
            if (this.Date != DateTime.MinValue) info += this.Date.ToString("yyyy.MM.dd") + "    ";
            info += $"{(DateTime.Now.Date + Time).ToString("H:mm")}   {this.Week}   {this.Filename.Split('\\').Last()}";
            /*info += this.Time;
            info += this.Week;
            info += this.Filename;*/

            
            return info; 
        }
    }
}
