using OnlineMusic.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineMusic.DAO
{
    public class PRODUCTCATEGORY_DAO
    {
        OnlineMusicDB db = null;
        public PRODUCTCATEGORY_DAO()
        {
            db = new OnlineMusicDB();
        }
        public List<DANHMUCSANPHAM> ListAll()
        {
            return db.DANHMUCSANPHAMs.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
        public bool Insert(DANHMUCSANPHAM entity)
        {
            db.DANHMUCSANPHAMs.Add(entity);
            db.SaveChanges();
            return true;
        }
        public IEnumerable<DANHMUCSANPHAM> ListAllPaging(int page, int pageSize)
        {
            return db.DANHMUCSANPHAMs.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public DANHMUCSANPHAM ViewDetail(long id)
        {
            return db.DANHMUCSANPHAMs.Find(id);
        }
    }
}