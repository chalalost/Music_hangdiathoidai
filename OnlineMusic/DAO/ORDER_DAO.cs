using OnlineMusic.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineMusic.DAO
{
    public class ORDER_DAO
    {
        OnlineMusicDB db = null;
        public ORDER_DAO()
        {
            db = new OnlineMusicDB();
        }
        public long Insert(ORDER order)
        {
            db.ORDERs.Add(order);
            db.SaveChanges();
            return order.ID;
        }
    }
}
