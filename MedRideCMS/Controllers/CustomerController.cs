using AutoMapper;
using MedRideCMS.DTOs;
using MedRideCMS.Models;
using MedRideCMS.ViewModels.CustomerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MedRideCMS.Controllers
{
	public class CustomerController : Controller
	{

		private static int _nextId = 1;
		private static List<Customer> _customers = LoadCustomers();
		private static List<State> _states = LoadStates();

		public ActionResult Index(int sortBy = 0, int page = 0, int pageSize = 10)
		{
			var viewModel = new IndexViewModel();

            var customers = SortCustomersBy(_customers, sortBy);

			var pagedResult = new CustomerPagedResultViewModel(page, pageSize, customers);

			pagedResult.SortByType = sortBy;
			pagedResult.States = _states.ToList();
			pagedResult.ReturnUrl = new UrlHelper(ControllerContext.RequestContext);

			pagedResult.ReturnRouteValues = new RouteValueDictionary();

			viewModel.PagedResult = pagedResult;

			return View(viewModel);
		}

		public ActionResult CustomerLookup()
		{
			var viewModel = new CustomerLookupViewModel
			{
				SearchParams = new CustomerSearchParamsDto(),
				States = _states
			};

			ModelState.Clear();

			return View(viewModel);
		}
		
		public ActionResult NewCustomer()
		{
			var viewModel = new NewCustomerViewModel
			{
				Customer = new Customer(),
				States = _states
			};

			return View(viewModel);
		}


		public ActionResult CustomerLookupResult(CustomerSearchParamsDto searchParams, int sortBy = 0, int page = 0, int pageSize = 10)
		{
			IEnumerable<Customer> results = _customers.Where((Customer c) => (EqualsIgnoreCase(c.FirstName, searchParams.FirstName) && EqualsIgnoreCase(c.LastName, searchParams.LastName)) 
				|| (EqualsIgnoreCase(c.Address, searchParams.Address)
				&& EqualsIgnoreCase(c.City, searchParams.City)
				&& c.StateId == searchParams.StateId
				&& EqualsIgnoreCase(c.Zip, searchParams.Zip))
			);

			results = SortCustomersBy(results, sortBy);


			var pagedResult = new CustomerPagedResultViewModel(page, pageSize, results);
			pagedResult.SortByType = sortBy;
			pagedResult.States = _states.ToList();
			pagedResult.ReturnUrl= new UrlHelper(ControllerContext.RequestContext);

			pagedResult.ReturnRouteValues = new RouteValueDictionary(new {
				FirstName = searchParams.FirstName,
				LastName = searchParams.LastName,
				Address = searchParams.Address,
				City = searchParams.City,
				StateId = searchParams.StateId,
				Zip = searchParams.Zip
			});

			var viewModel = new CustomerLookupResultViewModel();
			viewModel.PagedResult = pagedResult;
			searchParams.StateName = searchParams.StateId.HasValue ? _states.SingleOrDefault(s => s.Id == searchParams.StateId).Name : string.Empty;
			viewModel.SearchParams = searchParams;
				

			return View("CustomerLookupResult", viewModel);
		}

		public ActionResult Save(Customer customer, string returnUrl)
		{
			var message = string.Empty;

			if(customer.Id == 0)
			{
				customer.Id = _nextId;
				_nextId++;

				try
				{
					customer.State = _states.SingleOrDefault(s => s.Id == customer.StateId);
					_customers.Add(customer);
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
					var customerInDb = _customers.SingleOrDefault(c => c.Id == customer.Id);
					customerInDb.FirstName = customer.FirstName;
					customerInDb.LastName = customer.LastName;
					customerInDb.Address = customer.Address;
					customerInDb.City = customer.City;
					customerInDb.StateId = customer.StateId;
					customerInDb.State = _states.SingleOrDefault(s => s.Id == customer.StateId);
					customerInDb.Zip = customer.Zip;
					customerInDb.Phone = customer.Phone;
					customerInDb.SecondaryPhone = customer.SecondaryPhone;
					customerInDb.Email = customer.Email;

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
			

			return RedirectToAction("CustomerDetails", new { id = customer.Id, message = message, returnUrl });
		}

		public ActionResult SaveCustomerFailed(Customer customer, string message, string errorMessage)
		{
			var viewModel = new SaveCustomerFailedViewModel
			{
				Customer = customer,
				Message = message,
				ErrorMessage = errorMessage,
				State = _states.SingleOrDefault(s => s.Id == customer.StateId)
			};

			return View(viewModel);
		}

		public ActionResult EditCustomer(int id, string returnUrl)
		{
			var customer = _customers.SingleOrDefault(c => c.Id == id);

			var viewModel = new EditCustomerViewModel { Customer = customer, States = _states, ReturnUrl = returnUrl };

			return View(viewModel);
		}

		public ActionResult CustomerDetails(int id, string message, string returnUrl)
		{
			var customer = _customers.SingleOrDefault(c => c.Id == id);

			if (customer == null)
				return HttpNotFound();

			var state = _states.SingleOrDefault(s => s.Id == customer.StateId);

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
			var customer = _customers.SingleOrDefault(c => c.Id == id);
			_customers.Remove(customer);

			var viewModel = new CustomerDeletedViewModel
			{
				Customer = customer,
				State = _states.SingleOrDefault(s => s.Id == customer.StateId),
				ReturnUrl = returnUrl
			};

			return View("CustomerDeleted", viewModel);
		}

		
		private static List<Customer> LoadCustomers()
		{
			var customers = new List<Customer>();

			customers.Add(new Customer { Id = 1, FirstName = "Jim", LastName = "Halpert", Address = "111 Cedar Way", City = "Scranton", StateId = State.Pennsylvania, Zip = "20213", Email = "jhalpert@dm.com", Phone = "111-111-1111", SecondaryPhone = null });
			customers.Add(new Customer { Id = 2, FirstName = "Michael", LastName = "Scott", Address = "532 1st St", City = "Scranton", StateId = State.Pennsylvania, Zip = "20210", Email = "mscott@dm.com", Phone = "111-111-3232", SecondaryPhone = "111-321-5789" });
			customers.Add(new Customer { Id = 3, FirstName = "John", LastName = "Doe", Address = "698 Florida St", City = "San Francisco", StateId = State.California, Zip = "65123", Email = "jdoe@gmail.com", Phone = "308-987-4561", SecondaryPhone = null });
			customers.Add(new Customer { Id = 4, FirstName = "John", LastName = "Smith", Address = "21 Oregon Way", City = "Deland", StateId = State.Florida, Zip = "32724", Email = "jsmith@gmail.com", Phone = "386-847-1948", SecondaryPhone = null });
			customers.Add(new Customer { Id = 5, FirstName = "John", LastName = "Doe", Address = "654 Hemingway Way", City = "San Francisco", StateId = State.California, Zip = "65123", Email = "jdoe@yahoo.com", Phone = "308-987-2142", SecondaryPhone = null });
			customers.Add(new Customer { Id = 6, FirstName = "John", LastName = "Smith", Address = "23 Oregon Way", City = "Deland", StateId = State.Florida, Zip = "32724", Email = "jsmith@yahoo.com", Phone = "386-847-1945", SecondaryPhone = null });
			customers.Add(new Customer { Id = 7, FirstName = "Michael", LastName = "Scott", Address = "532 1st St", City = "New York", StateId = State.NewYork, Zip = "40210", Email = "mscott21@dm.com", Phone = "211-111-3232", SecondaryPhone = null });
			customers.Add(new Customer { Id = 8, FirstName = "Bob", LastName = "Roberts", Address = "532 Fairview Ct", City = "Austin", StateId = State.Texas, Zip = "57846", Email = null, Phone = "111-111-3232", SecondaryPhone = null });
			customers.Add(new Customer { Id = 9, FirstName = "Pam", LastName = "Halpert", Address = "111 Cedar Way", City = "Scranton", StateId = State.Pennsylvania, Zip = "20213", Email = "phalpert@dm.com", Phone = "111-111-1111", SecondaryPhone = null });
			customers.Add(new Customer { Id = 10, FirstName = "Ben", LastName = "Cowart", Address = "541 3rd St", City = "Stockbridge", StateId = State.Michigan, Zip = "73254", Email = "bcowart@gmail.com", Phone = "724-596-6668", SecondaryPhone = null });
			customers.Add(new Customer { Id = 11, FirstName = "Mark", LastName = "Smit", Address = "412 Saxon St", City = "Munith", StateId = State.Michigan, Zip = "22222", Email = "msmit@yahoo.com", Phone = "418-111-3232", SecondaryPhone = null });
			customers.Add(new Customer { Id = 12, FirstName = "James", LastName = "Kirk", Address = "532 West Monte St", City = "Denver", StateId = State.Colorado, Zip = "87210", Email = "jkirk@enterprize.com", Phone = "349-111-3232", SecondaryPhone = null });
			customers.Add(new Customer { Id = 13, FirstName = "John", LastName = "Doe", Address = "698 1st St", City = "Denver", StateId = State.Colorado, Zip = "11112", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 14, FirstName = "John", LastName = "Doe", Address = "698 2nd St", City = "San Francisco", StateId = State.California, Zip = "11113", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 15, FirstName = "John", LastName = "Doe", Address = "698 3rd St", City = "Orlando", StateId = State.Florida, Zip = "11114", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 16, FirstName = "John", LastName = "Doe", Address = "698 4th St", City = "Munith", StateId = State.Michigan, Zip = "11115", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 17, FirstName = "John", LastName = "Doe", Address = "698 5th St", City = "Albany", StateId = State.NewYork, Zip = "11116", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 18, FirstName = "John", LastName = "Doe", Address = "698 6th St", City = "Pittsburgh", StateId = State.Pennsylvania, Zip = "11117", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 19, FirstName = "John", LastName = "Doe", Address = "698 7th St", City = "Dallas", StateId = State.Texas, Zip = "11118", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 20, FirstName = "John", LastName = "Doe", Address = "698 8th St", City = "Tampa", StateId = State.Florida, Zip = "11119", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 21, FirstName = "John", LastName = "Doe", Address = "698 9th St", City = "Jackson", StateId = State.Michigan, Zip = "11110", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 22, FirstName = "John", LastName = "Doe", Address = "698 10th St", City = "Colorado Springs", StateId = State.Colorado, Zip = "11112", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 23, FirstName = "John", LastName = "Doe", Address = "698 11th St", City = "Philadelphia", StateId = State.Pennsylvania, Zip = "11120", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 24, FirstName = "John", LastName = "Doe", Address = "698 12 St", City = "Dallas", StateId = State.Texas, Zip = "11122", Email = null, Phone = null, SecondaryPhone = null });


			customers.Add(new Customer { Id = 25, FirstName = "John", LastName = "Doe", Address = "698 1st St", City = "Denver", StateId = State.Colorado, Zip = "11112", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 14, FirstName = "John", LastName = "Doe", Address = "698 2nd St", City = "San Francisco", StateId = State.California, Zip = "11113", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 15, FirstName = "John", LastName = "Doe", Address = "698 3rd St", City = "Orlando", StateId = State.Florida, Zip = "11114", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 16, FirstName = "John", LastName = "Doe", Address = "698 4th St", City = "Munith", StateId = State.Michigan, Zip = "11115", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 17, FirstName = "John", LastName = "Doe", Address = "698 5th St", City = "Albany", StateId = State.NewYork, Zip = "11116", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 18, FirstName = "John", LastName = "Doe", Address = "698 6th St", City = "Pittsburgh", StateId = State.Pennsylvania, Zip = "11117", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 19, FirstName = "John", LastName = "Doe", Address = "698 7th St", City = "Dallas", StateId = State.Texas, Zip = "11118", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 20, FirstName = "John", LastName = "Doe", Address = "698 8th St", City = "Tampa", StateId = State.Florida, Zip = "11119", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 21, FirstName = "John", LastName = "Doe", Address = "698 9th St", City = "Jackson", StateId = State.Michigan, Zip = "11110", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 22, FirstName = "John", LastName = "Doe", Address = "698 10th St", City = "Colorado Springs", StateId = State.Colorado, Zip = "11112", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 23, FirstName = "John", LastName = "Doe", Address = "698 11th St", City = "Philadelphia", StateId = State.Pennsylvania, Zip = "11120", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 24, FirstName = "John", LastName = "Doe", Address = "698 12 St", City = "Dallas", StateId = State.Texas, Zip = "11122", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 9, FirstName = "Pam", LastName = "Halpert", Address = "111 Cedar Way", City = "Scranton", StateId = State.Pennsylvania, Zip = "20213", Email = "phalpert@dm.com", Phone = "111-111-1111", SecondaryPhone = null });
			customers.Add(new Customer { Id = 1, FirstName = "Jim", LastName = "Halpert", Address = "111 Cedar Way", City = "Scranton", StateId = State.Pennsylvania, Zip = "20213", Email = "jhalpert@dm.com", Phone = "111-111-1111", SecondaryPhone = null });

			customers.Add(new Customer { Id = 25, FirstName = "John", LastName = "Doe", Address = "698 1st St", City = "Denver", StateId = State.Colorado, Zip = "11112", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 14, FirstName = "John", LastName = "Doe", Address = "698 2nd St", City = "San Francisco", StateId = State.California, Zip = "11113", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 15, FirstName = "John", LastName = "Doe", Address = "698 3rd St", City = "Orlando", StateId = State.Florida, Zip = "11114", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 16, FirstName = "John", LastName = "Doe", Address = "698 4th St", City = "Munith", StateId = State.Michigan, Zip = "11115", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 17, FirstName = "John", LastName = "Doe", Address = "698 5th St", City = "Albany", StateId = State.NewYork, Zip = "11116", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 18, FirstName = "John", LastName = "Doe", Address = "698 6th St", City = "Pittsburgh", StateId = State.Pennsylvania, Zip = "11117", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 19, FirstName = "John", LastName = "Doe", Address = "698 7th St", City = "Dallas", StateId = State.Texas, Zip = "11118", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 20, FirstName = "John", LastName = "Doe", Address = "698 8th St", City = "Tampa", StateId = State.Florida, Zip = "11119", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 21, FirstName = "John", LastName = "Doe", Address = "698 9th St", City = "Jackson", StateId = State.Michigan, Zip = "11110", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 22, FirstName = "John", LastName = "Doe", Address = "698 10th St", City = "Colorado Springs", StateId = State.Colorado, Zip = "11112", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 23, FirstName = "John", LastName = "Doe", Address = "698 11th St", City = "Philadelphia", StateId = State.Pennsylvania, Zip = "11120", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 24, FirstName = "John", LastName = "Doe", Address = "698 12 St", City = "Dallas", StateId = State.Texas, Zip = "11122", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 9, FirstName = "Pam", LastName = "Halpert", Address = "111 Cedar Way", City = "Scranton", StateId = State.Pennsylvania, Zip = "20213", Email = "phalpert@dm.com", Phone = "111-111-1111", SecondaryPhone = null });
			customers.Add(new Customer { Id = 1, FirstName = "Jim", LastName = "Halpert", Address = "111 Cedar Way", City = "Scranton", StateId = State.Pennsylvania, Zip = "20213", Email = "jhalpert@dm.com", Phone = "111-111-1111", SecondaryPhone = null });


			customers.Add(new Customer { Id = 25, FirstName = "John", LastName = "Doe", Address = "698 1st St", City = "Denver", StateId = State.Colorado, Zip = "11112", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 14, FirstName = "John", LastName = "Doe", Address = "698 2nd St", City = "San Francisco", StateId = State.California, Zip = "11113", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 15, FirstName = "John", LastName = "Doe", Address = "698 3rd St", City = "Orlando", StateId = State.Florida, Zip = "11114", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 16, FirstName = "John", LastName = "Doe", Address = "698 4th St", City = "Munith", StateId = State.Michigan, Zip = "11115", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 17, FirstName = "John", LastName = "Doe", Address = "698 5th St", City = "Albany", StateId = State.NewYork, Zip = "11116", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 18, FirstName = "John", LastName = "Doe", Address = "698 6th St", City = "Pittsburgh", StateId = State.Pennsylvania, Zip = "11117", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 19, FirstName = "John", LastName = "Doe", Address = "698 7th St", City = "Dallas", StateId = State.Texas, Zip = "11118", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 20, FirstName = "John", LastName = "Doe", Address = "698 8th St", City = "Tampa", StateId = State.Florida, Zip = "11119", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 21, FirstName = "John", LastName = "Doe", Address = "698 9th St", City = "Jackson", StateId = State.Michigan, Zip = "11110", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 22, FirstName = "John", LastName = "Doe", Address = "698 10th St", City = "Colorado Springs", StateId = State.Colorado, Zip = "11112", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 23, FirstName = "John", LastName = "Doe", Address = "698 11th St", City = "Philadelphia", StateId = State.Pennsylvania, Zip = "11120", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 24, FirstName = "John", LastName = "Doe", Address = "698 12 St", City = "Dallas", StateId = State.Texas, Zip = "11122", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 9, FirstName = "Pam", LastName = "Halpert", Address = "111 Cedar Way", City = "Scranton", StateId = State.Pennsylvania, Zip = "20213", Email = "phalpert@dm.com", Phone = "111-111-1111", SecondaryPhone = null });
			customers.Add(new Customer { Id = 1, FirstName = "Jim", LastName = "Halpert", Address = "111 Cedar Way", City = "Scranton", StateId = State.Pennsylvania, Zip = "20213", Email = "jhalpert@dm.com", Phone = "111-111-1111", SecondaryPhone = null });


			customers.Add(new Customer { Id = 25, FirstName = "John", LastName = "Doe", Address = "698 1st St", City = "Denver", StateId = State.Colorado, Zip = "11112", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 14, FirstName = "John", LastName = "Doe", Address = "698 2nd St", City = "San Francisco", StateId = State.California, Zip = "11113", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 15, FirstName = "John", LastName = "Doe", Address = "698 3rd St", City = "Orlando", StateId = State.Florida, Zip = "11114", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 16, FirstName = "John", LastName = "Doe", Address = "698 4th St", City = "Munith", StateId = State.Michigan, Zip = "11115", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 17, FirstName = "John", LastName = "Doe", Address = "698 5th St", City = "Albany", StateId = State.NewYork, Zip = "11116", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 18, FirstName = "John", LastName = "Doe", Address = "698 6th St", City = "Pittsburgh", StateId = State.Pennsylvania, Zip = "11117", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 19, FirstName = "John", LastName = "Doe", Address = "698 7th St", City = "Dallas", StateId = State.Texas, Zip = "11118", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 20, FirstName = "John", LastName = "Doe", Address = "698 8th St", City = "Tampa", StateId = State.Florida, Zip = "11119", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 21, FirstName = "John", LastName = "Doe", Address = "698 9th St", City = "Jackson", StateId = State.Michigan, Zip = "11110", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 22, FirstName = "John", LastName = "Doe", Address = "698 10th St", City = "Colorado Springs", StateId = State.Colorado, Zip = "11112", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 23, FirstName = "John", LastName = "Doe", Address = "698 11th St", City = "Philadelphia", StateId = State.Pennsylvania, Zip = "11120", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 24, FirstName = "John", LastName = "Doe", Address = "698 12 St", City = "Dallas", StateId = State.Texas, Zip = "11122", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 9, FirstName = "Pam", LastName = "Halpert", Address = "111 Cedar Way", City = "Scranton", StateId = State.Pennsylvania, Zip = "20213", Email = "phalpert@dm.com", Phone = "111-111-1111", SecondaryPhone = null });
			customers.Add(new Customer { Id = 1, FirstName = "Jim", LastName = "Halpert", Address = "111 Cedar Way", City = "Scranton", StateId = State.Pennsylvania, Zip = "20213", Email = "jhalpert@dm.com", Phone = "111-111-1111", SecondaryPhone = null });


			customers.Add(new Customer { Id = 25, FirstName = "John", LastName = "Doe", Address = "698 1st St", City = "Denver", StateId = State.Colorado, Zip = "11112", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 14, FirstName = "John", LastName = "Doe", Address = "698 2nd St", City = "San Francisco", StateId = State.California, Zip = "11113", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 15, FirstName = "John", LastName = "Doe", Address = "698 3rd St", City = "Orlando", StateId = State.Florida, Zip = "11114", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 16, FirstName = "John", LastName = "Doe", Address = "698 4th St", City = "Munith", StateId = State.Michigan, Zip = "11115", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 17, FirstName = "John", LastName = "Doe", Address = "698 5th St", City = "Albany", StateId = State.NewYork, Zip = "11116", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 18, FirstName = "John", LastName = "Doe", Address = "698 6th St", City = "Pittsburgh", StateId = State.Pennsylvania, Zip = "11117", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 19, FirstName = "John", LastName = "Doe", Address = "698 7th St", City = "Dallas", StateId = State.Texas, Zip = "11118", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 20, FirstName = "John", LastName = "Doe", Address = "698 8th St", City = "Tampa", StateId = State.Florida, Zip = "11119", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 21, FirstName = "John", LastName = "Doe", Address = "698 9th St", City = "Jackson", StateId = State.Michigan, Zip = "11110", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 22, FirstName = "John", LastName = "Doe", Address = "698 10th St", City = "Colorado Springs", StateId = State.Colorado, Zip = "11112", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 23, FirstName = "John", LastName = "Doe", Address = "698 11th St", City = "Philadelphia", StateId = State.Pennsylvania, Zip = "11120", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 24, FirstName = "John", LastName = "Doe", Address = "698 12 St", City = "Dallas", StateId = State.Texas, Zip = "11122", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 9, FirstName = "Pam", LastName = "Halpert", Address = "111 Cedar Way", City = "Scranton", StateId = State.Pennsylvania, Zip = "20213", Email = "phalpert@dm.com", Phone = "111-111-1111", SecondaryPhone = null });
			customers.Add(new Customer { Id = 1, FirstName = "Jim", LastName = "Halpert", Address = "111 Cedar Way", City = "Scranton", StateId = State.Pennsylvania, Zip = "20213", Email = "jhalpert@dm.com", Phone = "111-111-1111", SecondaryPhone = null });


			customers.Add(new Customer { Id = 25, FirstName = "John", LastName = "Doe", Address = "698 1st St", City = "Denver", StateId = State.Colorado, Zip = "11112", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 14, FirstName = "John", LastName = "Doe", Address = "698 2nd St", City = "San Francisco", StateId = State.California, Zip = "11113", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 15, FirstName = "John", LastName = "Doe", Address = "698 3rd St", City = "Orlando", StateId = State.Florida, Zip = "11114", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 16, FirstName = "John", LastName = "Doe", Address = "698 4th St", City = "Munith", StateId = State.Michigan, Zip = "11115", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 17, FirstName = "John", LastName = "Doe", Address = "698 5th St", City = "Albany", StateId = State.NewYork, Zip = "11116", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 18, FirstName = "John", LastName = "Doe", Address = "698 6th St", City = "Pittsburgh", StateId = State.Pennsylvania, Zip = "11117", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 19, FirstName = "John", LastName = "Doe", Address = "698 7th St", City = "Dallas", StateId = State.Texas, Zip = "11118", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 20, FirstName = "John", LastName = "Doe", Address = "698 8th St", City = "Tampa", StateId = State.Florida, Zip = "11119", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 21, FirstName = "John", LastName = "Doe", Address = "698 9th St", City = "Jackson", StateId = State.Michigan, Zip = "11110", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 22, FirstName = "John", LastName = "Doe", Address = "698 10th St", City = "Colorado Springs", StateId = State.Colorado, Zip = "11112", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 23, FirstName = "John", LastName = "Doe", Address = "698 11th St", City = "Philadelphia", StateId = State.Pennsylvania, Zip = "11120", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 24, FirstName = "John", LastName = "Doe", Address = "698 12 St", City = "Dallas", StateId = State.Texas, Zip = "11122", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 9, FirstName = "Pam", LastName = "Halpert", Address = "111 Cedar Way", City = "Scranton", StateId = State.Pennsylvania, Zip = "20213", Email = "phalpert@dm.com", Phone = "111-111-1111", SecondaryPhone = null });
			customers.Add(new Customer { Id = 1, FirstName = "Jim", LastName = "Halpert", Address = "111 Cedar Way", City = "Scranton", StateId = State.Pennsylvania, Zip = "20213", Email = "jhalpert@dm.com", Phone = "111-111-1111", SecondaryPhone = null });


			customers.Add(new Customer { Id = 25, FirstName = "John", LastName = "Doe", Address = "698 1st St", City = "Denver", StateId = State.Colorado, Zip = "11112", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 14, FirstName = "John", LastName = "Doe", Address = "698 2nd St", City = "San Francisco", StateId = State.California, Zip = "11113", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 15, FirstName = "John", LastName = "Doe", Address = "698 3rd St", City = "Orlando", StateId = State.Florida, Zip = "11114", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 16, FirstName = "John", LastName = "Doe", Address = "698 4th St", City = "Munith", StateId = State.Michigan, Zip = "11115", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 17, FirstName = "John", LastName = "Doe", Address = "698 5th St", City = "Albany", StateId = State.NewYork, Zip = "11116", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 18, FirstName = "John", LastName = "Doe", Address = "698 6th St", City = "Pittsburgh", StateId = State.Pennsylvania, Zip = "11117", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 19, FirstName = "John", LastName = "Doe", Address = "698 7th St", City = "Dallas", StateId = State.Texas, Zip = "11118", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 20, FirstName = "John", LastName = "Doe", Address = "698 8th St", City = "Tampa", StateId = State.Florida, Zip = "11119", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 21, FirstName = "John", LastName = "Doe", Address = "698 9th St", City = "Jackson", StateId = State.Michigan, Zip = "11110", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 22, FirstName = "John", LastName = "Doe", Address = "698 10th St", City = "Colorado Springs", StateId = State.Colorado, Zip = "11112", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 23, FirstName = "John", LastName = "Doe", Address = "698 11th St", City = "Philadelphia", StateId = State.Pennsylvania, Zip = "11120", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 24, FirstName = "John", LastName = "Doe", Address = "698 12 St", City = "Dallas", StateId = State.Texas, Zip = "11122", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 9, FirstName = "Pam", LastName = "Halpert", Address = "111 Cedar Way", City = "Scranton", StateId = State.Pennsylvania, Zip = "20213", Email = "phalpert@dm.com", Phone = "111-111-1111", SecondaryPhone = null });
			customers.Add(new Customer { Id = 1, FirstName = "Jim", LastName = "Halpert", Address = "111 Cedar Way", City = "Scranton", StateId = State.Pennsylvania, Zip = "20213", Email = "jhalpert@dm.com", Phone = "111-111-1111", SecondaryPhone = null });


			customers.Add(new Customer { Id = 25, FirstName = "John", LastName = "Doe", Address = "698 1st St", City = "Denver", StateId = State.Colorado, Zip = "11112", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 14, FirstName = "John", LastName = "Doe", Address = "698 2nd St", City = "San Francisco", StateId = State.California, Zip = "11113", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 15, FirstName = "John", LastName = "Doe", Address = "698 3rd St", City = "Orlando", StateId = State.Florida, Zip = "11114", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 16, FirstName = "John", LastName = "Doe", Address = "698 4th St", City = "Munith", StateId = State.Michigan, Zip = "11115", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 17, FirstName = "John", LastName = "Doe", Address = "698 5th St", City = "Albany", StateId = State.NewYork, Zip = "11116", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 18, FirstName = "John", LastName = "Doe", Address = "698 6th St", City = "Pittsburgh", StateId = State.Pennsylvania, Zip = "11117", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 19, FirstName = "John", LastName = "Doe", Address = "698 7th St", City = "Dallas", StateId = State.Texas, Zip = "11118", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 20, FirstName = "John", LastName = "Doe", Address = "698 8th St", City = "Tampa", StateId = State.Florida, Zip = "11119", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 21, FirstName = "John", LastName = "Doe", Address = "698 9th St", City = "Jackson", StateId = State.Michigan, Zip = "11110", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 22, FirstName = "John", LastName = "Doe", Address = "698 10th St", City = "Colorado Springs", StateId = State.Colorado, Zip = "11112", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 23, FirstName = "John", LastName = "Doe", Address = "698 11th St", City = "Philadelphia", StateId = State.Pennsylvania, Zip = "11120", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 24, FirstName = "John", LastName = "Doe", Address = "698 12 St", City = "Dallas", StateId = State.Texas, Zip = "11122", Email = null, Phone = null, SecondaryPhone = null });
			customers.Add(new Customer { Id = 9, FirstName = "Pam", LastName = "Halpert", Address = "111 Cedar Way", City = "Scranton", StateId = State.Pennsylvania, Zip = "20213", Email = "phalpert@dm.com", Phone = "111-111-1111", SecondaryPhone = null });
			customers.Add(new Customer { Id = 1, FirstName = "Jim", LastName = "Halpert", Address = "111 Cedar Way", City = "Scranton", StateId = State.Pennsylvania, Zip = "20213", Email = "jhalpert@dm.com", Phone = "111-111-1111", SecondaryPhone = null });


			foreach(Customer c in customers)
			{
				c.Id = _nextId;
				_nextId++;
			}

			return customers.ToList();
		}
		
		private static List<State> LoadStates()
		{
			var states = new List<State>();

			states.Add(new State { Id = 1, Name = "Florida", AbbreviatedName = "FL", HasCoverage = true });
			states.Add(new State { Id = 2, Name = "Texas", AbbreviatedName = "TX", HasCoverage = true });
			states.Add(new State { Id = 3, Name = "Michigan", AbbreviatedName = "MI", HasCoverage = false });
			states.Add(new State { Id = 4, Name = "California", AbbreviatedName = "CA", HasCoverage = true });
			states.Add(new State { Id = 5, Name = "New York", AbbreviatedName = "NY", HasCoverage = true });
			states.Add(new State { Id = 6, Name = "Colorado", AbbreviatedName = "CO", HasCoverage = false });
			states.Add(new State { Id = 7, Name = "Pennsylvania", AbbreviatedName = "PA", HasCoverage = true });


			return states;
		}



		/*
		 * 
		 *  Helper Methods
		 * 
		 */
		private bool EqualsIgnoreCase(string str1, string str2)
		{
			return string.Equals(str1, str2, StringComparison.InvariantCultureIgnoreCase);
		}

		private IEnumerable<Customer> SortCustomersBy(IEnumerable<Customer> customers, int sortBy)
		{
			switch (sortBy)
			{
				case CustomerSortType.FirstName_Ascending:
					customers = customers.OrderBy(c => c.FirstName);
					break;
				case CustomerSortType.FirstName_Descending:
					customers = customers.OrderByDescending(c => c.FirstName);
					break;
				case CustomerSortType.LastName_Ascending:
					customers = customers.OrderBy(c => c.LastName);
					break;
				case CustomerSortType.LastName_Descending:
					customers = customers.OrderByDescending(c => c.LastName);
					break;
				case CustomerSortType.State_Ascending:
					customers = customers.OrderBy(c => _states.SingleOrDefault(s => s.Id == c.StateId).Name);
					break;
				case CustomerSortType.State_Descending:
					customers = customers.OrderByDescending(c => _states.SingleOrDefault(s => s.Id == c.StateId).Name);
					break;
				default:
					break;
			}

			return customers;
		}
	}
}