using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ScoreBoard.Models;
using ScoreBoard.ViewModels;

namespace ScoreBoard.Controllers
{
    public class EnterScoreController : BaseController
    {
        // GET: EnterScore
        public async Task<ActionResult> Index()
        {
            SetLoggedOnFlag();

            var model = await GetModel();

            var team = GetTeamFromCookie();
            if (team != null)
            {
                model.SelectedTeamID = team.ID;
                model.Teams = new List<SelectListItem>() { new SelectListItem()
                {
                    Text = $"{team.Name} - {team.Members}",
                    Value = team.ID.ToString() }
                };
            }

            return View(model);
        }


        private async Task<RecordScoreViewModel> GetModel()
        {
            // Get a list of all teams
            var allTeams = await this.Context().Teams.OrderByDescending(t => t.Name).ToListAsync();

            var model = new RecordScoreViewModel()
            {
                Teams = allTeams.Select(t => new SelectListItem()
                {
                    Text = $"{t.Name} - {t.Members}",
                    Value = t.ID.ToString()
                }),
                Score = 10000
            };
            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(RecordScoreViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check the score
                    if (model.Score <= 0)
                    {
                        ModelState.AddModelError("Score", "The score must be greater than zero");
                        var bm = await GetModel();
                        return View(bm);
                    }

                    var team = GetTeamFromCookie();

                    // Find the team with this ID
                    if (team == null)
                    {
                        team = this.Context().Teams.FirstOrDefault(t => t.ID == model.SelectedTeamID);
                        if (team == null)
                        {
                            this.AddErrorMessage("The team does not exist");
                            return RedirectToAction("Index");
                        }
                    }

                    team.Score = model.Score;

                    await this.Context().SaveChangesAsync();
                    this.AddSuccessMessage("Your score has been recorded");
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (System.Exception e)
            {
                this.AddErrorMessage(e.Message);
            }

            var baseModel = await GetModel();
            return View(baseModel);
        }


    }
}