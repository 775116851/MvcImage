using MvcImage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcImage.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index(string imgUrl)
        {
            return View(imgUrl);
        }

        //http://www.poluoluo.com/jzxy/201305/208844.html
        //http://www.cnblogs.com/zhouhb/p/3906714.html
        public ActionResult Upload(HttpPostedFileBase imgUpLoad)//这里跟前台页面input输入框name保持一致
        {
            string imgUrl = string.Empty;
            if(imgUpLoad != null)
            {
                string fileName = imgUpLoad.FileName;
                //转换只取得文件名，去掉路径。 
                if (fileName.LastIndexOf("\\") > -1)
                {
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                }
                string pathForSaving = Server.MapPath("~/AjaxUpload/");
                //在根目录下创建目标文件夹AjaxUpload
                if (!Directory.Exists(pathForSaving))
                {
                    try
                    {
                        Directory.CreateDirectory(pathForSaving);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                imgUrl = pathForSaving + fileName;
                imgUpLoad.SaveAs(imgUrl); 
            }
            return RedirectToAction("Index", "Home", new { imgUrl = imgUrl });
        }


        public ActionResult IndexJS()
        {
            return View();
        }

        //http://www.cnblogs.com/artwl/archive/2012/03/31/2427019.html
        [HttpPost]
        public JsonResult JSUpload(HttpPostedFileBase imgUpLoad)
        {
            string fileName = System.IO.Path.GetFileName(imgUpLoad.FileName);
            string filePhysicalPath = Server.MapPath("~/AjaxUpload/" + fileName);
            string pic = "", error = "";
            try
            {
                imgUpLoad.SaveAs(filePhysicalPath);
                pic = "/AjaxUpload/" + fileName;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return Json(new
            {
                pic = pic,
                error = error
            });
        }

        //嵌套在Index中
        //[HttpGet]  ImagesModels imageModels
        public ActionResult IndexQT(string KJID, string KJIDM)
        {
            ViewData["KJID"] = KJID;
            ViewData["KJIDM"] = KJIDM;
            return PartialView();
        }

    }
}
