using Quizz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Quizz.Controllers
{
    public class HomeController : Controller
    {
        masterEntities1 db = new masterEntities1();

        [HttpGet]
        public ActionResult sregister()
        {

            return View();
        }
        [HttpPost]
        public ActionResult sregister(STUDENT rs,HttpPostedFileBase imgfile)
        {
            STUDENT s = new STUDENT();
            try
            {
                s.S_NAME = rs.S_NAME;
                s.S_PASSWORD = rs.S_PASSWORD;
                s.S_IMAGE = uploadimage(imgfile);
                db.STUDENTs.Add(s);
                db.SaveChanges();
                return RedirectToAction("slogin");
            }
            catch
            {
                ViewBag.msg = "Data could not be Insert....";
            }

            return View();
        }
        public string uploadimage(HttpPostedFileBase imgfile)
        {
            string path = "-1";

            try
            {
                if (imgfile != null && imgfile.ContentLength > 0)
                {
                    string extension = Path.GetExtension(imgfile.FileName);
                    if (extension.ToLower().Equals("jpg")||extension.ToLower().Equals("jpeg") || extension.ToLower().Equals("png"))
                     { 
                        Random r = new Random();    
                       path =  Path.Combine(Server.MapPath("~/Content/img"),r+Path.GetFileName(imgfile.FileName));
                        imgfile.SaveAs(path);
                        path = "~/Content/img"+r+Path.GetFileName(imgfile.FileName);
                     }
                    return path;
                }

            }
            catch (Exception)
            {

            }

            return path;

        }
        [HttpGet]
        public ActionResult logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index");

        
        }

        [HttpGet]
        public ActionResult tlogin()
        {

            return View();
        }
        [HttpPost]
        public ActionResult tlogin(ADMIN a)
        {
            ADMIN ad = db.ADMINS.Where(x => x.AD_NAME == a.AD_NAME && x.AD_PASSWORD == a.AD_PASSWORD).SingleOrDefault();
            if (ad != null)
            {
                Session["ad_id"] = ad.AD_ID;
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.msg = "Invalid username or password";
            }
            return View();
        }
        //[HttpGet]
        public ActionResult slogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult slogin(STUDENT s)
        {
            STUDENT std = db.STUDENTs.Where(x=>x.S_NAME == s.S_NAME && x.S_PASSWORD==s.S_PASSWORD).SingleOrDefault();
            if(std == null)
            {
                ViewBag.msg="Invalid username or password";
            }
            else
            {
                Session["std_id"] = std.S_ID;
                return RedirectToAction("StudentExam");
            }
            return View();
        }
        public ActionResult StudentExam()
        {
            if (Session["std_id"] == null)
            {
                return RedirectToAction("slogin");
            }

            return View();
        }
        [HttpPost]
        public ActionResult StudentExam(string room)
        {
            List<tbl_category> list = db.tbl_category.ToList();
            foreach (var item in list)
            {
                if (item.cat_encryptedstring == room)
                {
                    List < QUESTION > li = db.QUESTIONs.Where(x => x.q_fk_catid == item.cat_id).ToList();
                    Queue<QUESTION> queue = new Queue<QUESTION>(); //Queue
                    foreach (QUESTION a in li)
                    {
                        queue.Enqueue(a);

                    }
                    TempData["examid"]=item.cat_id;
                    TempData["questions"] = queue;
                    TempData["score"] = 0;
                    TempData.Keep();

                    return RedirectToAction("QuizStart");
                }
                else
                {
                    ViewBag.error = "No room found...";
                }

            }
            return View();
        }

        public ActionResult QuizStart()//applying stack and queue
        {
            if (Session["std_id"] == null)
            {
                return RedirectToAction("slogin");
            }
            QUESTION q = null;
            if (TempData["questions"] != null)
            {
                Queue<QUESTION> qlist =(Queue<QUESTION>) TempData["questions"];//type casting
                if (qlist.Count > 0)
                {
                    q = qlist.Peek();
                    qlist.Dequeue();
                    TempData["questions"]=qlist;
                    TempData .Keep();
                }
                else
                {
                    return RedirectToAction("EndExam");
                }

            }
            else
            {
                return RedirectToAction("StudentExam");
            }

            return View(q);

        }
        [HttpPost]
        public ActionResult QuizStart(QUESTION q)
        {
            string correctAns=null;

            if (q.OPA!=null)
            {
                correctAns = "A";
            }
            else if(q.OPB!=null)
            {
                correctAns = "B";
            }
            else if (q.OPC != null)
            {
                correctAns = "C";
            }
            else if (q.OPD != null)
            {
                correctAns = "D";
            }
            if (correctAns.Equals(q.COP))
            {
                TempData["score"] = Convert.ToInt32(TempData["score"])+1;
            }
            TempData.Keep();

            return RedirectToAction("QuizStart");
        }

        public ActionResult EndExam()
        {
            return View();

        }

        public ActionResult Dashboard()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Addcategory()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Index");
            }

            //Session["ad_id"]= 1;//---problem faced when it was removed
            int adid = Convert.ToInt32(Session["ad_id"].ToString());
            List<tbl_category> li = db.tbl_category.Where(x => x.cat_fk_adid == adid).OrderByDescending(x => x.cat_id).ToList();

            ViewData["list"] = li;

            return View();
        }



        [HttpPost]
        public ActionResult Addcategory(tbl_category cat)
        {
            List<tbl_category> li = db.tbl_category.OrderByDescending(x => x.cat_id).ToList();
            ViewData["list"] = li;

           Random r = new Random();
            tbl_category c = new tbl_category();
            c.cat_name = cat.cat_name;
            c.cat_encryptedstring = Crypto.Encrypt(cat.cat_name.Trim()+r.Next().ToString(),true);//Encryption
            c.cat_fk_adid = Convert.ToInt32(Session["ad_id"].ToString());
            db.tbl_category.Add(c);
            db.SaveChanges();


            return RedirectToAction("Addcategory");
        }


        [HttpGet]
        public ActionResult Addquestion()
        {
            int sid = Convert.ToInt32(Session["ad_id"]);
            List<tbl_category> li = db.tbl_category.Where(x => x.cat_fk_adid == sid).ToList();
            ViewBag.List = new SelectList(li, "cat_id", "cat_name");
            return View();
        }

        [HttpPost]
        public ActionResult Addquestion(QUESTION q)
        {
            int sid = Convert.ToInt32(Session["ad_id"]);
            List<tbl_category> li = db.tbl_category.Where(x => x.cat_fk_adid == sid).ToList();
            ViewBag.List = new SelectList(li, "cat_id", "cat_name");

            QUESTION QA = new QUESTION();
            QA.Q_TEXT = q.Q_TEXT;
            QA.OPA = q.OPA;
            QA.OPB = q.OPB;
            QA.OPC = q.OPC;
            QA.OPD = q.OPD;
            QA.COP = q.COP;
            QA.q_fk_catid = q.q_fk_catid;   

            db.QUESTIONs.Add(QA);
           db.SaveChanges();//if i enable it then i face error
            TempData["msg"] = "Question added successfully...";
            TempData.Keep();
           return RedirectToAction("Addquestion");
           //return View();
        }
        public ActionResult Index()
        {
            if (Session["ad_id"]!=null){
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}