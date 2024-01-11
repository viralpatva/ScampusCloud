using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScampusCloud.Models
{
	public class CustomerModel : ResponseMessage
	{
		public CustomerModel()
		{
		}

		public int? Id { get; set; }

		public Guid? CompanyId { get; set; }

		[Required(ErrorMessage = "Customer Name is required")]
		public string CustomerName { get; set; }

		[Required(ErrorMessage = "Last Name is required")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Company Name is required")]

		public string CompanyName { get; set; }

		[Required(ErrorMessage = "Contact Phone is required")]
		[RegularExpression(@"^[+]{1}[0-9]{2}[0-9]{10}$", ErrorMessage = "Invalid Contact Phone Format")]
		public string ContactPhone { get; set; }

		[Required(ErrorMessage = "Email Address is required")]
		[RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*(\+[a-z0-9-]+)?@[a-z0-9-]+(\.[a-z0-9-]+)*$", ErrorMessage = "Invalid Email Address Format")]
		public string EmailAddress { get; set; }

		[Required(ErrorMessage = "Company Site is required")]

		[RegularExpression(@"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})$", ErrorMessage = "Invalid Company Site Format")]
		public string CompanySite { get; set; }
		public Guid? CreatedBy { get; set; }
		public Guid? ModifiedBy { get; set; }
		public string ActionType { get; set; }
	}
}