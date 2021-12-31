using OnlineMusic.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineMusic.DAO
{
    public class ORDERDETAIL_DAO
    {
        OnlineMusicDB db = null;
        public ORDERDETAIL_DAO()
        {
            db = new OnlineMusicDB();
        }
        public bool Insert(ORDERDETAIL detail)
        {
            try
            {
                db.ORDERDETAILs.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
    }
}