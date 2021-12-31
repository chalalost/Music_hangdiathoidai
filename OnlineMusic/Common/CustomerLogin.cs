using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineMusic.Common
{
    [Serializable]
    public class CustomerLogin
    {
        public string UserName { set; get; }
        public string Name { set; get; }
    }
}