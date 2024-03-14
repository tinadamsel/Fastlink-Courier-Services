using Core.DbContext;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FastLink.Controllers
{
	public class AdminController : Controller
	{
		private readonly AppDbContext _context;
		private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private readonly IUserHelper _userHelper;
        public AdminController(AppDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IUserHelper userHelper)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _userHelper = userHelper;
        }
        [HttpGet]
        public IActionResult Index()
		{
            var trackings = _userHelper.GetTotalTrackingId();
            var clients = _userHelper.GetTotalClients();
            var messages = _userHelper.GetTotalMessages();
            var listOfMsgs = _userHelper.GetAllMsgs().Take(3).ToList();
            var listOftrackids = _userHelper.GetAllGeneratedTrackDetails().Take(3).ToList();

            var model = new ApplicationUserViewModel()
            {
                TotalTrackIds = trackings,
                TotalClients= clients,
                TotalMessages = messages,
                TrackIds = listOftrackids,
                Messages = listOfMsgs,
            };
			return View(model);
		}
        [HttpPost]
        public JsonResult GenerateId(string trackDetails)
        {
            if (trackDetails != null)
            {
                var trackViewModel = JsonConvert.DeserializeObject<TrackingViewModel>(trackDetails);
                if (trackViewModel != null)
                {
                    var checkItemName = _userHelper.CheckItemName(trackViewModel.ItemName);
                    if (checkItemName)
                    {
                        return Json(new { isError = true, msg = " Track ID has been generated for this Item/Good" });
                    }
                    var generateID = _userHelper.GenerateID(trackViewModel);
                    if (generateID != null)
                    {
                       var trackingID =  generateID.TrackingID;
                       return Json(trackingID);
                       //return Json(new { isError = false, msg = " Successful, Your Tracking ID is " + trackingID + ". Please copy out your Tracking ID." });
                    }
                }
               return Json(new { isError = true, msg = " Could not generate ID" });
            }
            return Json(new { isError = true, msg = " Network Failure" });
        }

        [HttpGet]
        public JsonResult GetCurrentLocation(string trackingID)
        {
            if (trackingID != null)
            {
                var location = _userHelper.GetCurrentLocation(trackingID);
                if (location != null)
                {
                    //return Json(location);
                    return Json(new { isError = false, msg = " Your Goods/Consignment is currently at " + location + ". Thank you for choosing us." });
                }
                return Json(new { isError = true, msg = " Your item is yet to be moved, Please exercise some patience. Thank you" });
            }
            return Json( new { isError = true, msg = "Tracking ID not found"});
        }

        [HttpGet]
        public JsonResult GetLocation(int id, string trackingID)
        {
            if (trackingID != null && id > 0)
            {
                var location = _userHelper.GetLocationToUpdate(id, trackingID);
                if (location != null)
                {
                    return Json(location);
                }
                return Json(new { isError = true, msg = " Your item is yet to be moved" });
            }
            return Json(new { isError = true, msg = "Tracking ID not found" });
        }

        [HttpGet]
        public IActionResult ChangeLocation()
        {
            var listOfTrackings = _userHelper.GetAllGeneratedTrackDetails();
            return View(listOfTrackings);
        }

        [HttpPost]
        public JsonResult ChangeCurrentLocation(int id, string trackID, string newLocation)
        {
            if (id > 0 && trackID != null && newLocation != null)
            {
                var updateLocation = _userHelper.ChangeCurrentLocation(id, trackID, newLocation);
                if (updateLocation)
                {
                    return Json(new { isError = false, msg = " Location Changed Successfully" });
                }
                return Json(new { isError = true, msg = " Could not update" });
            }
            return Json(new { isError = true, msg = " Network Failure" });

        }

        [HttpPost]
        public JsonResult DeleteTrack(int id)
        {
            if (id > 0)
            {
                var deleteTrack = _userHelper.DeleteTrack(id);
                if (deleteTrack)
                {
                    return Json(new { isError = false, msg = " Generated TrackID Deleted" });
                }
            }
            return Json(new { isError = true, msg = " Could not find track to delete" });
        }
        [HttpGet]
        public IActionResult ClientMessages()
        {
            var listOfMsgs = _userHelper.GetAllMsgs();
            return View(listOfMsgs);
        }

        [HttpPost]
        public JsonResult SaveContactMsg(string contactDetails)
        {
            if (contactDetails != null)
            {
                var contactViewModel = JsonConvert.DeserializeObject<ContactViewModel>(contactDetails);
                if (contactViewModel != null)
                {
                    var createContact = _userHelper.CreateContactMsg(contactViewModel);
                    if (createContact)
                    {
                        return Json(new { isError = false, msg = "Message sent successfully. Thank you for reaching out."});
                    }
                }
                return Json(new { isError = true, msg = " Could not send message" });
            }
            return Json(new { isError = true, msg = " Network Failure" });
        }
        [HttpGet]
        public JsonResult GetContactMsg(int id)
        {
            if (id > 0)
            {
                var msg = _userHelper.GetMsg(id);
                if (msg != null)
                {
                    return Json(msg);
                }
                //return Json(new { isError = true, msg = " Your item is yet to be moved" });
            }
            return Json(new { isError = true, msg = "Message not found" });
        }

        [HttpPost]
        public JsonResult DeleteMsg(int id)
        {
            if (id > 0)
            {
                var deleteMsg = _userHelper.DeleteMsg(id);
                if (deleteMsg)
                {
                    return Json(new { isError = false, msg = " Message Deleted" });
                }
            }
            return Json(new { isError = true, msg = " Could not find msg to delete" });
        }



    }
}
