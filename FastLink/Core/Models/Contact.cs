using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class Contact : BaseModel
	{
		public string Subject { get; set; }
		public string ClientEmail { get; set; }
		public string Message { get; set; }
		public string? ResponseMessage { get; set; }
		public string? ResponseSubject { get; set; }
		public int? ClientId { get; set; }
		[ForeignKey("ClientId")]
		public virtual Client? Clients { get; set; }

	}
}
