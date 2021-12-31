using OnlineMusic.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineMusic.DAO
{
    public class FEEDBACK_DAO
    {
         OnlineMusicDB db = null;
        public FEEDBACK_DAO()
        {
            db = new OnlineMusicDB();
        }
        public long Insert(FEEDBACK fb)
        {
            db.FEEDBACKs.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }
        public IEnumerable<FEEDBACK> ListAllPaging( int page, int pageSize)
        {
            return db.FEEDBACKs.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }
    }
}