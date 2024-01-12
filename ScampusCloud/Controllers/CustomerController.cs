using ScampusCloud.Models;
using ScampusCloud.Repository.Customer;
using ScampusCloud.Repository.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScampusCloud.Controllers
{
    public class CustomerController : Controller
    {

		#region Variable Declaration
		private readonly CustomerRepository _customerRepository;
		CustomerModel _CustomerModel = new CustomerModel();
		#endregion

		#region CTOR
		public CustomerController()
		{
			_customerRepository = new CustomerRepository();
		}
		#endregion

		#region Method
		// GET: Customer
		public ActionResult Customer()
        {
            return View();
        }
        public ActionResult AddEditCustomer(CustomerModel _CustomerModel)
        {
			if (_CustomerModel.CompanyName != null)
			{
				if (_CustomerModel.Id > 0)
				{
					_CustomerModel.ActionType = "Update";
				}
				else
				{
					_CustomerModel.ActionType = "Insert";
				}
				//_CustomerModel.CreatedBy=
				// _CustomerModel.ModifiedBy =
				var officemaster = _customerRepository.AddEdit_Customer(_CustomerModel);

				return RedirectToAction("Customer", "Customer");
			}
			else
			{
				return View();
			}
        }

		public JsonResult CustomerList()
		{
			var customer = _customerRepository.GetCustomerList();

			return Json(customer, JsonRequestBehavior.AllowGet);
		}
		#endregion
	}
}