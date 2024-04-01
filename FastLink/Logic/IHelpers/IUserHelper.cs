using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelpers
{
    public interface IUserHelper
    {
        bool ChangeCurrentLocation(int id, string trackID, TrackingViewModel trackViewModel);
        bool CheckIfDelivered(int id);
        bool CheckIfPaused(int id);
        bool CheckItemName(string name);
        bool CreateContactMsg(ContactViewModel contactViewModel);
        bool DeleteMsg(int id);
        bool DeleteTrack(int id);
        bool DeliverTrack(int id);
        Task<ApplicationUser> FindByEmailAsync(string email);
        ApplicationUser FindById(string Id);
        ApplicationUser FindByUserName(string username);
        Tracking GenerateID(TrackingViewModel trackDetails);
        List<TrackingViewModel> GetAllGeneratedTrackDetails();
        List<ContactViewModel> GetAllMsgs();
        int GetAllUser();
        TrackingViewModel GetCurrentLocation(string trackingID);
		string GetCurrentUserId(string username);
        Tracking GetLocationToUpdate(int id, string trackingID);
        string GetMsg(int id);
        int GetTotalClients();
        int GetTotalMessages();
        int GetTotalTrackingId();
        string GetUserId(string username);
        bool PauseOrder(int id);
        bool RemovePauseOrder(int id);
    }
}
