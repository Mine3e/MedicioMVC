using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class FileNullException : Exception
    {
        public string Propertyname {  get; set; }
        public FileNullException(string propertyName,string? message) : base(message)
        {
             Propertyname = propertyName;
        }
    }
}
