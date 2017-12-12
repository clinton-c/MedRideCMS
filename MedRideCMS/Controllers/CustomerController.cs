using AutoMapper;
using MedRideCMS.DTOs;
using MedRideCMS.Models;
using MedRideCMS.ViewModels.CustomerViewModels;
using MedRideCMS.ViewModels.SharedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;

namespace MedRideCMS.Controllers
{
	public class CustomerController : Controller
	{

		private ApplicationDbContext _context;

		public CustomerController()
		{ 
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			_context.Dispose();
		}

		public ActionResult Index(int sortBy = CustomerSortType.DEFAULT, int page = 0, int pageSize = 10)
		{
			var customers = CustomerSortType.SortCustomersBy(_context.Customers.Include(c => c.State), sortBy);

			var pagedResult = new PagedResult<Customer>(page, pageSize, customers);

			var tableViewModel = new CustomerTableViewModel(pagedResult);
			tableViewModel.SortByType = sortBy;
			tableViewModel.States = _context.States.ToList();
			tableViewModel.ReturnUrl = new UrlHelper(ControllerContext.RequestContext);
			tableViewModel.ReturnRouteValues = new RouteValueDictionary();

			if(sortBy == CustomerSortType.CREATED_ASCENDING || sortBy == CustomerSortType.CREATED_DESCENDING)
			{
				tableViewModel.HidePrimaryPhoneCol = true;
				tableViewModel.HideSecondaryPhoneCol = true;
				tableViewModel.HideEmailCol = true;
				tableViewModel.HideUpdatedCol = true;
			}
			else if (sortBy == CustomerSortType.UPDATED_ASCENDING || sortBy == CustomerSortType.UPDATED_DESCENDING)
			{
				tableViewModel.HidePrimaryPhoneCol = true;
				tableViewModel.HideSecondaryPhoneCol = true;
				tableViewModel.HideEmailCol = true;
				tableViewModel.HideCreatedCol = true;
			}
			else
			{
				tableViewModel.HideCreatedCol = true;
				tableViewModel.HideUpdatedCol = true;
				tableViewModel.HideSecondaryPhoneCol = true;
			}

			var viewModel = new IndexViewModel();
			viewModel.TableViewModel = tableViewModel;

			return View(viewModel);
		}

		public ActionResult CustomerLookup()
		{
			var viewModel = new CustomerLookupViewModel
			{
				SearchParams = new CustomerSearchParamsDto(),
				States = _context.States.ToList()
			};

			ModelState.Clear();

			return View(viewModel);
		}
		
		public ActionResult NewCustomer()
		{
            var viewModel = new NewCustomerViewModel
            {
				Customer = new Customer(),
				States = _context.States.ToList()
			};

			return View(viewModel);
		}


		public ActionResult CustomerLookupResult(CustomerSearchParamsDto searchParams, int sortBy = 0, int page = 0, int pageSize = 10)
		{
			IEnumerable<Customer> results = _context.Customers.Include(c => c.State).Where((Customer c) => (c.FirstName.ToLower() == searchParams.FirstName.ToLower() && c.LastName.ToLower() == searchParams.LastName.ToLower()) 
				|| (c.Address.ToLower() == searchParams.Address.ToLower()
                && c.City.ToLower() == searchParams.City.ToLower()
                && c.StateId == searchParams.StateId
				&& c.Zip.ToLower() == searchParams.Zip.ToLower())
			).ToList();


			results = CustomerSortType.SortCustomersBy(results, sortBy);


			var pagedResult = new PagedResult<Customer>(page, pageSize, results);

			var tableViewModel = new CustomerTableViewModel(pagedResult);
			tableViewModel.SortByType = sortBy;
			tableViewModel.States = _context.States.ToList();
			tableViewModel.ReturnUrl= new UrlHelper(ControllerContext.RequestContext);
			tableViewModel.ReturnRouteValues = new RouteValueDictionary(new {
				FirstName = searchParams.FirstName,
				LastName = searchParams.LastName,
				Address = searchParams.Address,
				City = searchParams.City,
				StateId = searchParams.StateId,
				Zip = searchParams.Zip
			});

			tableViewModel.HideSecondaryPhoneCol = true;
			tableViewModel.HideUpdatedCol = true;
			tableViewModel.HideCreatedCol = true;

			var viewModel = new CustomerLookupResultViewModel();
			viewModel.TableViewModel = tableViewModel;
			searchParams.StateName = searchParams.StateId.HasValue ? _context.States.SingleOrDefault(s => s.Id == searchParams.StateId).Name : string.Empty;
			viewModel.SearchParams = searchParams;
				
			return View("CustomerLookupResult", viewModel);
		}

		public ActionResult Save(Customer customer, string returnUrl)
		{
			var message = string.Empty;

			if(customer.Id == 0)
			{
				try
				{
                    if (!ModelState.IsValid)
                    {
                        var viewModel = new NewCustomerViewModel { Customer = customer, States = _context.States.ToList() };
                        return View("NewCustomer", viewModel);
                    }

                    customer.State = _context.States.SingleOrDefault(s => s.Id == customer.StateId);
					customer = _context.Customers.Add(customer);
					message = "Customer has been added to the database!";
				}
				catch (Exception e)
				{
					return RedirectToAction("SaveCustomerFailed", new {
						customer = customer,
						message = "Failed to add new customer to database!",
						errorMessage = e.Message,
						returnUrl = returnUrl
					});
				}
				
			}
			else
			{
				try
				{
                    if(!ModelState.IsValid)
                    {
                        var viewModel = new EditCustomerViewModel { Customer = customer, States = _context.States.ToList(), ReturnUrl = returnUrl };
                        return View("EditCustomer", viewModel);
                    }

					var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == customer.Id);
					customerInDb.FirstName = customer.FirstName;
					customerInDb.LastName = customer.LastName;
					customerInDb.Address = customer.Address;
					customerInDb.City = customer.City;
					customerInDb.StateId = customer.StateId;
					customerInDb.State = _context.States.SingleOrDefault(s => s.Id == customer.StateId);
					customerInDb.Zip = customer.Zip;
					customerInDb.Phone = customer.Phone;
					customerInDb.SecondaryPhone = customer.SecondaryPhone;
					customerInDb.Email = customer.Email;
					customerInDb.Updated = DateTime.Now;

					message = "Customer has been updated in the database!";
				}
				catch (Exception e)
				{
					return RedirectToAction("SaveCustomerFailed", new
					{
						customer = customer,
						message = "Failed to update customer in the database!",
						errorMessage = e.Message,
						returnUrl = returnUrl
					});
				}
				
			}

			_context.SaveChanges();
			return RedirectToAction("CustomerDetails", new { id = customer.Id, message = message, returnUrl });
		}

		public ActionResult SaveCustomerFailed(Customer customer, string message, string errorMessage)
		{
			var viewModel = new SaveCustomerFailedViewModel
			{
				Customer = customer,
				Message = message,
				ErrorMessage = errorMessage,
				State = _context.States.SingleOrDefault(s => s.Id == customer.StateId)
			};

			return View(viewModel);
		}

		public ActionResult EditCustomer(int id, string returnUrl)
		{
			var customer = _context.Customers.Include(c => c.State).SingleOrDefault(c => c.Id == id);

			customer.Updated = DateTime.Now;

			var viewModel = new EditCustomerViewModel { Customer = customer, States = _context.States.ToList(), ReturnUrl = returnUrl };

			return View(viewModel);
		}

		public ActionResult CustomerDetails(int id, string message, string returnUrl)
		{
			var customer = _context.Customers.Include(c => c.State).SingleOrDefault(c => c.Id == id);

			if (customer == null)
				return HttpNotFound();

			var state = _context.States.SingleOrDefault(s => s.Id == customer.StateId);

			var viewModel = new CustomerDetailViewModel {
				Customer = customer,
				State = state,
				ReturnUrl = returnUrl,
				Message = message
			};

			return View(viewModel);
		}

		public ActionResult DeleteCustomer(int id, string returnUrl)
		{
			var customer = _context.Customers.Include(c => c.State).SingleOrDefault(c => c.Id == id);
			_context.Customers.Remove(customer);
            _context.SaveChanges();

			var viewModel = new CustomerDeletedViewModel
			{
				Customer = customer,
				State = _context.States.SingleOrDefault(s => s.Id == customer.StateId),
				ReturnUrl = returnUrl
			};

			return View("CustomerDeleted", viewModel);
		}
	}
}