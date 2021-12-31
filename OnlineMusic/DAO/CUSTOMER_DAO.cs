using OnlineMusic.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineMusic.DAO
{
    public class CUSTOMER_DAO
    {
        OnlineMusicDB db = null;
        public CUSTOMER_DAO()
        {
            db = new OnlineMusicDB();
        }
        public bool Insert(CUSTOMER entity)
        {
            db.CUSTOMERs.Add(entity);
            db.SaveChanges();
            return true;
        }
        public CUSTOMER GetByID(string userName)
        {
            return db.CUSTOMERs.SingleOrDefault(x => x.UserName == userName);
        }
        public int Login(string userName, string password)
        {
            var result = db.CUSTOMERs.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            var result2 = db.CUSTOMERs.Where(x => x.UserName == userName && x.Password == password).Count();
            if (result2 == 0)
            {
                return -2;
            }

            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == password)
                        return 1;
                    else
                        return -2;
                }
            }
        }
        public bool CheckUserName(string userName)
        {
            return db.CUSTOMERs.Count(x => x.UserName == userName) > 0;
        }
        public bool CheckEmail(string mail)
        {
            return db.CUSTOMERs.Count(x => x.Email == mail) > 0;
        }
    }
}