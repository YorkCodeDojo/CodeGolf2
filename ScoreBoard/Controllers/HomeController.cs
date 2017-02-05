using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ScoreBoard.Models;

namespace ScoreBoard.Controllers
{
    public class HomeController : BaseController
    {
        private ScoreBoardContext db = new ScoreBoardContext();

        /// <summary>
        /// Get the list of teams,  the one with lowest score first
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index(int refreshRate = 0)
        {
            SetLoggedOnFlag();

            if (refreshRate > 0)
                Response.AddHeader("Refresh", refreshRate.ToString());

            return View(await db.Teams.OrderBy(t => t.Score).ToListAsync());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}