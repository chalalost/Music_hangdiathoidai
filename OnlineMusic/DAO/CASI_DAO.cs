using OnlineMusic.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineMusic.DAO
{
    public class CASI_DAO
    {
        OnlineMusicDB db = null;
        public CASI_DAO()
        {
            db = new OnlineMusicDB();
        }
        public IEnumerable<CASI> ListAllPaging(int page, int pageSize)
        {

            return db.CASIs.OrderByDescending(x => x.SingerID).ToPagedList(page, pageSize);
        }
        public bool Insert(CASI entity)
        {
            db.CASIs.Add(entity);
            db.SaveChanges();
            return true;
        }
        public List<CASI> ListAll()
        {
            return db.CASIs.OrderBy(x => x.SingerID).ToList();
        }
    }
}