using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.Model
{
    public class General<T>
    {
        public T Entity { get; set; }
        public List<T> List { get; set; }
        public bool IsSuccess { get; set; }
        public string SuccessfulMessage { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
