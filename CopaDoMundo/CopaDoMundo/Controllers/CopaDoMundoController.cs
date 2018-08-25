using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CopaDoMundo.Controllers
{
    public class CopaDoMundoController : Controller
    {
        // GET: CopaDoMundo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Escalacao()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Partidas(FormCollection form)
        {
            try
            {
                //CopaDoMundoEntities ce = new CopaDoMundoEntities();

                //TimesDaCopa ti = new TimesDaCopa();

                //ti.Nome_Time = form["Time1A"];
                //ti.Id_Chave = 1;
                ////ti.Id_Time = 1;

                //ce.TimesDaCopa.Add(ti);
                //ce.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            return View();
        }

    }
}