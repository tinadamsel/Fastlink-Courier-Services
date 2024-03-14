using Core.DbContext;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly AppDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        public UserHelper(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.Users.Where(s => s.Email == email)?.FirstOrDefaultAsync();
        }

        public ApplicationUser FindByUserName(string username)
        {
            return _userManager.Users.Where(s => s.UserName == username)?.FirstOrDefault();
        }
        public int GetAllUser()
        {
            return _userManager.Users.Where(x => !x.Deactivated).Count();
        }
        public string GetUserId(string username)
        {
            return _userManager.Users.Where(s => s.UserName == username)?.FirstOrDefaultAsync().Result.Id?.ToString();
        }
        public ApplicationUser FindById(string Id)
        {
            return _userManager.Users.Where(s => s.Id == Id)?.FirstOrDefault();
        }
        public string GetCurrentUserId(string username)
        {
            try
            {
                if (username != null)
                {
                    return _userManager.Users.Where(s => s.UserName == username)?.FirstOrDefault()?.Id?.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public int GetTotalClients()
        {
             return _context.Clients.Where(x => x.Id > 0 && x.Active).Count();
        }
        public int GetTotalMessages()
        {
            return _context.Contacts.Where(x => x.Id > 0 && x.Active).Count();
        }
        public int GetTotalTrackingId()
        {
            return _context.Trackings.Where(x => x.Id > 0 && x.Active).Count();
        }
        public List<TrackingViewModel> GetAllGeneratedTrackDetails()
        {
            var trackViewModel = new List<TrackingViewModel>();
            trackViewModel =  _context.Trackings.Where(a => a.Id > 0 && a.TrackingID != null && a.Active && !a.Deleted).
            Select( a => new TrackingViewModel
            {
                TrackingID = a.TrackingID,
                Address = a.Address,
                ClientName = a.ClientName,
                Phonenumber = a.Phonenumber,
                Active= a.Active,
                CurrentCity = a.CurrentCity, 
                CurrentLocation= a.CurrentLocation, 
                DateCreated = a.DateCreated,
                Email = a.Email,
                Id = a.Id,
                ItemName = a.ItemName,
                ItemWeight = a.ItemWeight,
                NewLocation = a.NewLocation,
            }).OrderByDescending(a => a.DateCreated).ToList();

            return trackViewModel;
        }
        public bool CheckItemName(string name)
        {
            if (name != null)
            {
                var checkName = _context.Trackings.Where(x => x.Name == name && x.Active && !x.Deleted).FirstOrDefault();
                if (checkName != null)
                {
                    return true;
                }
            }
            return false;
        }
        public Tracking GenerateID(TrackingViewModel trackDetails)
        {
            if (trackDetails != null)
            {
                var track = new Tracking()
                {
                    ClientName = trackDetails.ClientName,
                    CurrentCity = trackDetails.CurrentCity,
                    CurrentLocation = trackDetails.CurrentLocation,
                    Address = trackDetails.Address,
                    Email = trackDetails.Email,
                    Active = true,
                    DateCreated = DateTime.Now,
                    ItemName = trackDetails.ItemName,
                    ItemWeight = trackDetails.ItemWeight,
                    Phonenumber = trackDetails.Phonenumber,
                    Deleted = false,
                    TrackingID = GenerateTrackingID(),
                };
                _context.Trackings.Add(track);
                _context.SaveChanges();
                return track;
            }
            return null;
        }
        public string GenerateTrackingID()
        {
            var dateConvert = DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            int sequence = 10;
            var initial = "FLCSTID";
            var caseNumber = initial + dateConvert + sequence;
            return caseNumber;
        }

        public string GetCurrentLocation(string trackingID)
        {
            if (trackingID != null)
            {
                var getLocation = _context.Trackings.Where(a => a.TrackingID == trackingID && a.Active && !a.Deleted).FirstOrDefault();
                if (getLocation != null)
                {
                    return getLocation?.NewLocation;
                }
            }
            return null;
        }
        public Tracking GetLocationToUpdate(int id, string trackingID)
        {
            var getLocation = _context.Trackings.Where(a => a.Id == id && a.TrackingID == trackingID && a.Active && !a.Deleted).FirstOrDefault();
            if (getLocation != null)
            {
                return getLocation;
            }
            return null;
        }
        public bool ChangeCurrentLocation(int id, string trackID, string newLocation)
        {
            var updateLocation = _context.Trackings.Where(b => b.Id == id && b.TrackingID == trackID && b.Active && !b.Deleted).FirstOrDefault();
            if (updateLocation != null)
            {
                updateLocation.NewLocation = newLocation;
                _context.Trackings.Update(updateLocation);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DeleteTrack(int id)
        {
            var deleteTrack = _context.Trackings.Where(b => b.Id == id && b.Active && !b.Deleted).FirstOrDefault();
            if (deleteTrack != null)
            {
                deleteTrack.Active = false;
                deleteTrack.Deleted = true;
                _context.Trackings.Update(deleteTrack);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ContactViewModel> GetAllMsgs()
        {
            var contactViewModel = new List<ContactViewModel>();
            contactViewModel = _context.Contacts.Where(a => a.Id > 0 && a.Message != null && a.Active && !a.Deleted).Include(b => b.Clients).Select(a => new ContactViewModel
            {
                Active = a.Active,
                DateCreated = a.DateCreated,
                Subject = a.Subject,
                Name= a.Name,
                Message= a.Message,
                ClientEmail = a.Clients.Email,
                ClientId= a.ClientId,
                Id = a.Id,
            }).OrderByDescending(b => b.DateCreated).ToList();

            return contactViewModel;
        }

        public bool CreateContactMsg(ContactViewModel contactViewModel)
        {
            if (contactViewModel != null)
            {
                var getExistingClient = GetExistingClientDetails(contactViewModel.ClientEmail);
                if (getExistingClient == null )
                {
                    var createNewClient = CreateNewClient(contactViewModel?.Name, contactViewModel?.ClientEmail);
                    var model = new Contact()
                    {
                        Name = contactViewModel.Name != null ? contactViewModel.Name : "Client",
                        Subject = contactViewModel.Subject,
                        Message = contactViewModel.Message,
                        ClientEmail = contactViewModel.ClientEmail,
                        ClientId= createNewClient.Id,
                        DateCreated = DateTime.Now,
                        Active = true,
                        Deleted = false,
                    };
                    _context.Add(model);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    var model = new Contact()
                    {
                        Name = contactViewModel.Name != null ? contactViewModel.Name : "Client",
                        Subject = contactViewModel.Subject,
                        Message = contactViewModel.Message,
                        ClientEmail = contactViewModel.ClientEmail,
                        ClientId = getExistingClient.Id,
                        DateCreated = DateTime.Now,
                    };
                    _context.Add(model);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public Client GetExistingClientDetails(string email)
        {
            if (email != null)
            {
                var clientDetails = _context.Clients.Where(x => x.Email == email && x.Active && !x.Deleted).FirstOrDefault();
                if (clientDetails != null)
                {
                    return clientDetails;
                }
            }
            return null;
        }

        public Client CreateNewClient(string name, string clientEmail)
        {
            if (clientEmail != null )
            {
                var client = new Client()
                {
                    Name = name != null ? name : "Client",
                    Email = clientEmail,
                    DateCreated = DateTime.Now,
                    Active = true,
                    Deleted = false,
                };
                _context.Add(client);
                _context.SaveChanges();
                return client;
            }
            return null;
        }
        
        public string GetMsg(int id)
        {
            if (id > 0)
            {
                var getMsg = _context.Contacts.Where(a => a.Id == id && a.Active && !a.Deleted).FirstOrDefault();
                if (getMsg != null)
                {
                    return getMsg?.Message;
                }
            }
            return null;
        }

        public bool DeleteMsg(int id)
        {
            var deleteMsg = _context.Contacts.Where(b => b.Id == id && b.Active && !b.Deleted).FirstOrDefault();
            if (deleteMsg != null)
            {
                deleteMsg.Active = false;
                deleteMsg.Deleted = true;
                _context.Update(deleteMsg);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
