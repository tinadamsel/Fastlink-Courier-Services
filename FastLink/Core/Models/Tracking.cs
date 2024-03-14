using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class Tracking : BaseModel
	{
		public string ClientName{ get; set; }
		public string Email { get; set; }
		public string? Phonenumber { get; set; }
		public string? Address { get; set; }
		public string? CurrentLocation { get; set; }
		public string? CurrentCity { get; set; }
		public string ItemName { get; set; }
		public string? ItemWeight { get; set; }
		public string? NewLocation { get; set; }
		public string TrackingID { get; set; }

	}
}
