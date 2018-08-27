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
            List<string> nomeTimes = new List<string>();

            try
            {
                //-- Adicionando os times ao banco na tabela TimesDaCopa --//

                CopaDoMundoEntities ce = new CopaDoMundoEntities();

                List<Chaves> chaves = new List<Chaves>();

                var listaChaves = from c in ce.Chaves select c;

                foreach (var item in listaChaves)
                {
                    Chaves ch = new Chaves();
                    ch.Id_Chave = item.Id_Chave;
                    ch.Nome_Chave = item.Nome_Chave;

                    chaves.Add(ch);
                }

                int contaTimes = 1;

                foreach (var i in chaves)
                {
                    for (int x = 1; x <= 4; x++)
                    {
                        TimesDaCopa ti = new TimesDaCopa();
                        ti.Nome_Time = form["Time" + x + i.Nome_Chave];
                        ti.Id_Chave = i.Id_Chave;
                        ti.Id_Time = contaTimes++;

                        ce.TimesDaCopa.Add(ti);   
                    }
                }

                ce.SaveChanges();

                //-- Criando e adicionando os times de forma aleatória ao banco na tabela Jogos --//

                CopaDoMundoEntities ce2 = new CopaDoMundoEntities();

                List<TimesDaCopa> timesJogo = new List<TimesDaCopa>();

                foreach (var item in ce.TimesDaCopa)
                {
                    TimesDaCopa tc = new TimesDaCopa();
                    tc.Id_Chave = item.Id_Chave;
                    tc.Id_Time = item.Id_Time;
                    tc.Nome_Time = item.Nome_Time;

                    timesJogo.Add(tc);
                }

                int a = 0;
                int IJogo = 1;
                for(int x = 1; x <= 4; x++)
                {

                    var list = new List<int> {0, 1, 2, 3};
                    var rnd = new Random();

                    var query =
                        from i in list
                        let r = rnd.Next()
                        orderby r
                        select i;

                    var shuffled = query.ToList();

                    var listaJ = from c in timesJogo where c.Id_Chave == x select c;

                    List<TimesDaCopa> timesJ = new List<TimesDaCopa>();

                    foreach (var item in listaJ)
                    {
                        TimesDaCopa tc = new TimesDaCopa();
                        tc.Id_Time = item.Id_Time;
                        tc.Nome_Time = item.Nome_Time;
                        tc.Id_Chave = item.Id_Chave;

                        timesJ.Add(tc);
                    }

                    for (int y = 1; y <= 2; y++)
                    {

                        Jogos ti = new Jogos();

                        ti.Id_Time1 = timesJ[shuffled[a]].Id_Time;
                        ti.Id_Time2 = timesJ[shuffled[a + 1]].Id_Time;
                        ti.FaseJogo = 1;
                        ti.Id_Chave = timesJ[shuffled[a]].Id_Chave;
                        ti.Id_Jogo = IJogo++;

                        a = (a + 1) + y;

                        ce2.Jogos.Add(ti);
                    }
                    a = 0;
                }

                ce2.SaveChanges();

                //-- Apresentando os jogos registrados no banco na view --//

                CopaDoMundoEntities ce3 = new CopaDoMundoEntities();

                var listaJa = from c in ce3.Jogos
                              join d in ce3.TimesDaCopa on c.Id_Time1 equals d.Id_Time
                              join x in ce3.TimesDaCopa on c.Id_Time2 equals x.Id_Time
                              select new { time1 = d.Nome_Time, time2 = x.Nome_Time};

                foreach (var item in listaJa)
                {
                    nomeTimes.Add(item.time1 + " X " + item.time2);
                }

                ViewBag.ListaTimes = nomeTimes;

            }
            catch (Exception ex)
            {

            }
            return View();
        }

        [HttpPost]
        public ActionResult ResultadoOitavas(FormCollection form)
        {
            try
            {
                CopaDoMundoEntities ce = new CopaDoMundoEntities();

                var listaJa = from c in ce.Jogos select c;

                foreach(var x in listaJa)
                {
                    var vPontos = NumeroAleatorioPlacar();

                    Jogos listaJogo = ce.Jogos.Single(f => f.Id_Jogo.Equals(x.Id_Jogo));

                    listaJogo.SalGols1 = vPontos[0];
                    listaJogo.SalGols2 = vPontos[1];
                }
                ce.SaveChanges();

                CopaDoMundoEntities ce3 = new CopaDoMundoEntities();

                var listaJb = from c in ce3.Jogos
                              join d in ce3.TimesDaCopa on c.Id_Time1 equals d.Id_Time
                              join x in ce3.TimesDaCopa on c.Id_Time2 equals x.Id_Time
                              select new { time1 = d.Nome_Time, c.SalGols1, time2 = x.Nome_Time, c.SalGols2 };

                List<string> nomeTimes = new List<string>();

                foreach (var item in listaJb)
                {
                    nomeTimes.Add(item.time1 + " " + item.SalGols1 + " X " + item.SalGols2 + " " + item.time2);
                }

                ViewBag.ListaTimes = nomeTimes;


            }
            catch(Exception ex)
            {

            }

            return View();
        }
        
        public List<int> NumeroAleatorioPlacar()
        {
            var list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var rnd = new Random();

            var query =
                from i in list
                let r = rnd.Next()
                orderby r
                select i;

            var lista = query.ToList();

            lista.RemoveRange(1, 9);

            var shuffled = lista.ToList();

            return shuffled;
        }
    }
}