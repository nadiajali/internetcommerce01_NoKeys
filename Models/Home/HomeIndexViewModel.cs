using internetcommerce01.DAL;
using internetcommerce01.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using internetcommerce01.Models;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Data.SqlClient;
using PagedList;
using PagedList.Mvc;

namespace internetcommerce01.Models.Home
{
    public class HomeIndexViewModel
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        db_internetcommerce01Entities context = new db_internetcommerce01Entities();

        public IPagedList<Tbl_Product> ListOfProducts { get; set; }

        public HomeIndexViewModel CreateModel(string search, int pageSize, int? page)
        {
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@search", search ?? (object)DBNull.Value) };
            IPagedList<Tbl_Product> data = context.Database.SqlQuery<Tbl_Product>("GetBySearch @search", parameter).ToList().ToPagedList(page ?? 1, pageSize);
            return new HomeIndexViewModel
            {
                ListOfProducts = data
            };
        }
    }
}
