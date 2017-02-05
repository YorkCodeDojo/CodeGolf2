using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScoreBoard.Models;
using ScoreBoard.ViewModels;

namespace ScoreBoard.Controllers
{
    public abstract class BaseController : Controller
    {
        public GlobalViewModel GlobalViewModel;

        private ScoreBoardContext db = new ScoreBoardContext();

        protected ScoreBoardContext Context() => this.db;

        public BaseController()
        {
            this.GlobalViewModel = new GlobalViewModel();
            this.ViewData["GlobalViewModel"] = this.GlobalViewModel;
            this.GlobalViewModel.IsLoggedIn = false;
        }

        protected void SetLoggedOnFlag()
        {
            this.GlobalViewModel.IsLoggedIn = !(string.IsNullOrWhiteSpace(GetTeamCookie()));
        }

        /// <summary>
        /// Feeds a positive message back to the user
        /// </summary>
        /// <param name="message"></param>
        internal void AddSuccessMessage(string message)
        {
            TempData["SuccessMessage"] = message;
        }

        internal void AddErrorMessage(string message)
        {
            TempData["ErrorMessage"] = message;
        }

        protected Team GetTeamFromCookie()
        {
            var teamGUID = GetTeamCookie();
            if (!string.IsNullOrEmpty(teamGUID))
            {
                return db.Teams.FirstOrDefault(t => t.CookieID == new Guid(teamGUID));
            }

            return null;
        }

        private string GetTeamCookie()
        {
            var myCookie = Request.Cookies["teamCookie"];
            if (myCookie != null)
            {
                return myCookie.Values["teamGUID"];
            }
            return string.Empty;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Context().Dispose();
            }
            base.Dispose(disposing);
        }
    }
}