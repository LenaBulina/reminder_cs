using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ReminderWF
{
    [Serializable]
    class Reminder: IComparable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
       

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", Date.ToShortDateString(), Date.ToShortTimeString(), Name);
           
        }

        public int CompareTo(object o)
        {
            Reminder r = o as Reminder;
            if(r != null)
            {
                return this.Date.CompareTo(r.Date);
            }
            else
            {
                throw new Exception("Невозможно сравнить");
            }
        }
       
    }

   
}
