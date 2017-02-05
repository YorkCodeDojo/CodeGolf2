using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ScoreBoard.Controllers
{
    public class EditController : BaseController
    {

        // GET: Index
        public ActionResult Index()
        {
            SetLoggedOnFlag();

            var team = GetTeamFromCookie();
            if (team == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(team);
        }

        // POST: Edit/Edit/5
        [HttpPost]
        public async Task<ActionResult> Index(FormCollection collection)
        {
            var team = GetTeamFromCookie();
            if (team == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                team.Name = collection["Name"];
                team.Members = collection["Members"];
                team.Language = collection["Language"];

                await this.Context().SaveChangesAsync();
                this.AddSuccessMessage("Your details have been amended");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                this.AddErrorMessage(e.Message);
                return View();
            }
        }


    }
}
