using System;
using System.Collections.Generic;
using System.Text;

namespace Hyms.Api.Model.Common
{
    public class DataResult<T>
    {
        public T[] Data { get; set; }
        public int Total { get; set; }
    }

    public class DataResult
    {
        public object Data { get; set; }
        public int Total { get; set; }
    }
}
