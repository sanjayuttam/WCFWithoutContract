using System;
using System.Globalization;

namespace TimeService
{
    public class Service1 : IService1
    {
        public string GetData()
        {
            return string.Format("You hit this method at {0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
        }

        public string GetMoreData(SomeData data)
        {
            return string.Format("You hit this method at {0}", data.Data);
        }
    }
}
