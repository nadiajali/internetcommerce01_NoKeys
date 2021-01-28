using internetcommerce01.DAL;
using internetcommerce01.Models;
using internetcommerce01.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace internetcommerce01.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var category = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords();
            foreach (var item in category)
            {
                list.Add(new SelectListItem { Value = item.CategoryID.ToString(), Text = item.CategoryName });
            }
            return list;
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Categories()
        {
            List<Tbl_Category> all_categories = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(all_categories);
        }

        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }

        public ActionResult UpdateCategory(int categoryID)
        {
            CategoryDetail cd;
            if (categoryID != null)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstOrDefault(categoryID)));
            }
            else
            {
                cd = new CategoryDetail();
            }
            return View("UpdateCategory", cd);
        }

        public ActionResult CategoryEdit(int categoryID)
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstOrDefault(categoryID));
        }

        [HttpPost]
        public ActionResult CategoryEdit(Tbl_Category table)
        {
            using (GenericUnitOfWork _unitOfWork = new GenericUnitOfWork())
            {
                _unitOfWork.GetRepositoryInstance<Tbl_Category>().Update(table);
                return RedirectToAction("Categories");
            }
        }

        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetProduct());
        }

        public ActionResult ProductEdit(int productID)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstOrDefault(productID));
        }

        //[HttpPost]
        //public ActionResult ProductEdit(Tbl_Product tbl)
        //{
        //    _unitOfWork.GetRepositoryInstance<Tbl_Product>().Update(tbl);
        //    return RedirectToAction("Product");
        //}

        [HttpPost]
        public ActionResult ProductEdit(Tbl_Product table)
        {
            table.ModifiedDate = DateTime.Now;

            using (GenericUnitOfWork _unitOfWork = new GenericUnitOfWork())
            {
                _unitOfWork.GetRepositoryInstance<Tbl_Product>().Update(table);
                return RedirectToAction("Product");
            }
        }

        public ActionResult ProductAdd()
        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }

        [HttpPost]
        public ActionResult ProductAdd(Tbl_Product table)
        {
            using (GenericUnitOfWork _unitOfWork = new GenericUnitOfWork())
            {
                table.CreatedDate = DateTime.Now;
                _unitOfWork.GetRepositoryInstance<Tbl_Product>().Add(table);
                return RedirectToAction("Product");
            }
        }
    }
}