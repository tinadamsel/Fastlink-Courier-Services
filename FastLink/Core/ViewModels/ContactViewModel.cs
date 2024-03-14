using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class ContactViewModel
	{
		public int Id { get; set; }
		public string Subject { get; set; }
		public string ClientEmail { get; set; }
		public string Message { get; set; }
		public string? ResponseMessage { get; set; }
		public string? ResponseSubject { get; set; }
		public int? ClientId { get; set; }
		public virtual ClientViewModel? Clients { get; set; }
		public string? Name { get; set; }
		public DateTime DateCreated { get; set; }
		public bool Active { get; set; }
		public bool Deleted { get; set; }
	}
}
