using OnlineMusic.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineMusic.DAO
{
    public class SLIDE_DAO
    {
        OnlineMusicDB db = null;
        public SLIDE_DAO()
        {
            db = new OnlineMusicDB();
        }
        public List<SLIDE> ListAll()
        {
            return db.SLIDEs.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
        public bool Insert(SLIDE entity)
        {
            db.SLIDEs.Add(entity);
            db.SaveChanges();
            return true;
        }
        public IEnumerable<SLIDE> ListAllPaging(int page, int pageSize)
        {
            return db.SLIDEs.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
    }
}