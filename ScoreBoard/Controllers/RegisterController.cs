using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ScoreBoard.Models;

namespace ScoreBoard.Controllers
{
    public class RegisterController : BaseController
    {
        public ActionResult Index()
        {
            SetLoggedOnFlag();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "ID,Name,Members,Score,Language")] Team team)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    team.CookieID = Guid.NewGuid();
                    team.Score = 10000;
                    this.Context().Teams.Add(team);
                    await this.Context().SaveChangesAsync();
                    this.AddSuccessMessage("Your team has been created.");

                    var myCookie = new HttpCookie("teamCookie");
                    myCookie.Values.Add("teamGUID", team.CookieID.ToString());
                    myCookie.Expires = DateTime.Now.AddHours(12);
                    Response.Cookies.Add(myCookie);

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (System.Exception e)
            {
                this.AddErrorMessage(e.Message);
            }

            return View(team);
        }

    }
}
