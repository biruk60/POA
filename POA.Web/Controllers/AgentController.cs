using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POA.Domain;
using POA.Web.Filters;
using POA.Web.Models;
using POA.Web.Models.Title;
using Microsoft.Web.Mvc;
using POA.Web.Infrastructure.Alerts;
using AutoMapper.QueryableExtensions;
using POA.Web.Models.Agent;

namespace POA.Web.Controllers
{
	public class AgentController : BaseController
	{
		// GET: Agent
		public ActionResult Index()
		{
			return View();
		}

		private readonly  POADBEntities _context;
	   private Agent _agent { get; set; }

	   public AgentController(POADBEntities context, Agent agent)
	  {
		  _context = context;
		  _agent = agent;
	  }
			

		//
		// GET: /agent/
	  [ChildActionOnly]
	  public ActionResult All()
	  {

		  var models = _context.Agents
				.Project().To<AgentSummaryViewModel>();          

		  return PartialView(models.ToArray()); 
	  }

	 // [Log("Viewed agent {id}")]
	  public ActionResult View(int id)
	  {
		  var model = _context.Agents
			  .SingleOrDefault(i => i.Agent_ID == id);

		  if (model == null)
		  {
			  return RedirectToAction<AgentController>(c => c.Index())
					  .WithError("Unable to find the user.  Maybe it was deleted?");
			  
		  }

		  return View(new AgentDetailsViewModel
			  {
				  Agent_ID = model.Agent_ID,
				  First_Name = model.First_Name,
				  Last_Name = model.Last_Name
			  });
	  }

	  public ActionResult GetUserByID(int id)
	  {
		  //var model = _context.Agents              
		  //    .SingleOrDefault(i => i.Agent_ID == id);

		  AgentDetails model = (from a in _context.Agents
					  join cob in _context.Countries
					  on a.Country_Of_Birth_ID equals cob.Country_ID
					  join cor in _context.Countries
					  on a.Country_Of_Residence_ID equals cor.Country_ID
					  join coc in _context.Countries
					  on a.Country_Of_CitizenShip_ID equals coc.Country_ID
					  join t in _context.Titles
					  on a.Title_ID equals t.Title_ID
					  where a.Agent_ID == id
					  select new AgentDetails
					  {
						  Agent_ID = a.Agent_ID,
						  First_Name = a.First_Name,
						  Last_Name = a.Last_Name,
						  City = a.City,
						  Middle_Name = a.Middle_Name,
						  Phone_No = a.Phone_No,
						  KifleKetema = a.KifleKetema,
						  ID_No = a.ID_No,
						  House_No = a.House_No,
						  Email = a.Email,
						  BirthName = cob.Name,
						  CitizenName = coc.Name,
						  ResidenceName = cor.Name,
						  TitleName = t.Title_Name
					  }).SingleOrDefault();



		  if (model == null)
		  {
			  return RedirectToAction<AgentController>(c => c.Index())
					  .WithError("Unable to find the user.  Maybe it was deleted?");

		  }

		 // AgentDetails objAgentDetail = (AgentDetails)(model);
		  

		  return Json(model, JsonRequestBehavior.AllowGet);
	  }

		

	  public ActionResult New()
	  {
		  var form = new NewAgentForm
		  {
			  
		  };
		  return View(form);
	  }

	  [HttpPost, ValidateAntiForgeryToken, Log("Created agent")]
	  public ActionResult New(NewAgentForm form)
	  {
		  if (!ModelState.IsValid)
		  {             
			  return View(form);
		  }

		  //var cobID = _context.Countries.Single(u => u.Country_ID == form.Country_Of_Birth_ID.Country_ID);
		  int intCobID = Convert.ToInt32(form.Country_Of_Birth_ID);
		  int intCorID = Convert.ToInt32(form.Country_Of_Residence_ID);
		  int intCocID = Convert.ToInt32(form.Country_Of_CitizenShip_ID);
		  int inttitleID = Convert.ToInt32(form.Title_ID);
		  var cobID = _context.Countries.Single(u => u.Country_ID == intCobID);
		  var corID = _context.Countries.Single(u => u.Country_ID == intCorID);
		  var cocID = _context.Countries.Single(u => u.Country_ID == intCocID );
		  var titleID = _context.Titles.Single(u => u.Title_ID == inttitleID);
		  _agent.First_Name = form.First_Name;
		  _agent.Last_Name = form.Last_Name;
		  _agent.Middle_Name = form.Middle_Name;
		  _agent.City = form.City;
		  _agent.Email = form.Email;
		  _agent.House_No = form.House_No;
		  _agent.ID_No = form.ID_No;
		  _agent.KifleKetema = form.KifleKetema;
		  _agent.Wereda = form.Wereda;
		  _agent.Phone_No = form.Phone_No;
		  _agent.Title_ID = titleID.Title_ID;
		  _agent.Country_Of_Birth_ID = cobID.Country_ID;
		  _agent.Country_Of_CitizenShip_ID = cocID.Country_ID;
		  _agent.Country_Of_Residence_ID = corID.Country_ID;
		  _context.Agents.Add(_agent);
		  _context.SaveChanges();

		  //return RedirectToAction("Index", "Home");
		  return RedirectToAction<AgentController>(c => c.Index()).WithSuccess("user created!"); 
	  }

	  [Log("Started to edit user {id}")]
	  public ActionResult Edit(int id)
	  {
		  var agent = _context.Agents
			  .Project().To<EditAgentForm>()
			  .SingleOrDefault(i => i.Agent_ID == id);       

		  if (agent == null)
		  {
			  return RedirectToAction<AgentController>(c => c.Index())
					  .WithError("Unable to find the user.  Maybe it was deleted?");
		  }

		  return View(agent);
	  }

	  [HttpPost, Log("Saving changes")]
	  public ActionResult Edit(EditAgentForm form)
	  {
		  if (!ModelState.IsValid)
		  {
			return View(form);
		  }

		  var _agent = _context.Agents.SingleOrDefault(i => i.Agent_ID == form.Agent_ID);

		  if (_agent == null)
		  {
			  return RedirectToAction<AgentController>(c => c.Index())
					  .WithError("Unable to find the agent.  Maybe it was deleted?");
		  }


		  int intCobID = Convert.ToInt32(form.Country_Of_Birth_ID);
		  int intCorID = Convert.ToInt32(form.Country_Of_Residence_ID);
		  int intCocID = Convert.ToInt32(form.Country_Of_CitizenShip_ID);
		  int inttitleID = Convert.ToInt32(form.Title_ID);
		  var cobID = _context.Countries.Single(u => u.Country_ID == intCobID);
		  var corID = _context.Countries.Single(u => u.Country_ID == intCorID);
		  var cocID = _context.Countries.Single(u => u.Country_ID == intCocID);
		  var titleID = _context.Titles.Single(u => u.Title_ID == inttitleID);
		  _agent.First_Name = form.First_Name;
		  _agent.Last_Name = form.Last_Name;
		  _agent.Middle_Name = form.Middle_Name;
		  _agent.City = form.City;
		  _agent.Email = form.Email;
		  _agent.House_No = form.House_No;
		  _agent.ID_No = form.ID_No;
		  _agent.KifleKetema = form.KifleKetema;
		  _agent.Wereda = form.Wereda;
		  _agent.Phone_No = form.Phone_No;
		  _agent.Title_ID = titleID.Title_ID;
		  _agent.Country_Of_Birth_ID = cobID.Country_ID;
		  _agent.Country_Of_CitizenShip_ID = cocID.Country_ID;
		  _agent.Country_Of_Residence_ID = corID.Country_ID;         
		  _context.SaveChanges();

		  return this.RedirectToAction(c => c.View(form.Agent_ID))
				.WithSuccess("Changes saved!");
		  
	  }

	  [HttpPost, ValidateAntiForgeryToken, Log("Deleted user {id}")]
	  public ActionResult Delete(int id)
	  {
		  var agent = _context.Agents.Find(id);

		  if (agent == null)
		  {
			  return this.RedirectToAction<AgentController>(c => c.Index())
					 .WithError("Unable to find the user.  Maybe it was deleted?");
		  }

		  _context.Agents.Remove(agent);

		  _context.SaveChanges();

		  return RedirectToAction<AgentController>(c => c.Index()).WithSuccess("user deleted!"); 
	  }

		public ActionResult Cancel()
	  {
		  return RedirectToAction<AgentController>(c => c.Index());
	  }
	}
}