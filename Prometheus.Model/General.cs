
using System.Collections.Generic;

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
