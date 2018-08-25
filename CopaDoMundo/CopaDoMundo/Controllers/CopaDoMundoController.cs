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
                CopaDoMundoEntities ce = new CopaDoMundoEntities();

                TimesDaCopa ti;

                List<Chaves> chaves = new List<Chaves>();

                Chaves ch;

                //var listaChaves = from c in ce.Chaves select c;

                var listaChaves = ce.Chaves.Select(x => new {x.Id_Chave, x.Nome_Chave });

                foreach (var item in listaChaves)
                {
                    ch = new Chaves();
                    ch.Id_Chave = item.Id_Chave;
                    ch.Nome_Chave = item.Nome_Chave;

                    chaves.Add(ch);
                }

                int contaTimes = 1;

                foreach (var i in chaves)
                {
                    for (int x = 1; x < 4; x++)
                    {
                        ti = new TimesDaCopa();
                        ti.Nome_Time = form["Time" + x + i.Nome_Chave];
                        ti.Id_Chave = i.Id_Chave;
                        ti.Id_Time = contaTimes++;

                        ce.TimesDaCopa.Add(ti);
                        ce.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return View();
        }

    }
}