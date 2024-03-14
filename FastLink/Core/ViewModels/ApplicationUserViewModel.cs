using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string UserName { get; set;}
        public string Password { get; set;}
        public string ConfirmPassword { get; set;}
        public string Email { get; set;}
        public string PhoneNumber { get; set;}
        public DateTime DateCreated { get; set;}
        public bool Deactivated { get; set;}
        public int TotalClients { get; set;}
        public int TotalMessages { get; set;}
        public int TotalTrackIds { get; set;}
        public List<TrackingViewModel> TrackIds { get; set;}
        public List<ContactViewModel> Messages { get; set;}

        
    }
}
