using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;
using AutoMapper;
using WebApplication2.Dtos;

using System.Data.Entity; // uses .Include(c => c.MembershipType)

namespace WebApplication2.Controllers.Api
{
	public class CustomersController : ApiController
	{
		public ApplicationDbContext _context;

		public CustomersController()
		{
			_context = new ApplicationDbContext();
		}

		//GET /api/customers
		public IHttpActionResult GetCustmers(string query = null)
		{
			var customersQuery = _context.Customer.Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
            {
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));
            }

            var customerDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            

            return Ok(customerDtos);
		}

		//GET /api/customers/1
		public CustomerDto GetCustomer(int id)
		{
			var customer = _context.Customer.SingleOrDefault(c => c.Id == id);

			if (customer == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			return Mapper.Map<Customer, CustomerDto>(customer);
		}

		//POST /api/customers
		[HttpPost]
		public CustomerDto CreateCustomer(CustomerDto customerDto)
		{
			if (!ModelState.IsValid)
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);

				//returns the IHttpActionResult
				// return BadRequest();
			}

			var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
			_context.Customer.Add(customer);
			_context.SaveChanges();

			customerDto.Id = customer.Id;

			return customerDto;
		}

		//PUT /api/customers/1
		[HttpPut]
		public void UpdateCustomer(int id, CustomerDto customerDto)
		{
			if (!ModelState.IsValid)
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}

			var customerInDb = _context.Customer.SingleOrDefault(c => c.Id == id);

			if (customerInDb == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			Mapper.Map(customerDto, customerInDb);

			/*
			customerInDb.Name = customerDto.Name;
			customerInDb.Birthdate = customerDto.Birthdate;
			customerInDb.IsSubscribedToNewsLetter = customerDto.IsSubscribedToNewsLetter;
			customerInDb.MembershipTypeId = customerDto.MembershipTypeId;
			*/

			_context.SaveChanges();
		}

		//DELETE /api/customers/1
		[HttpDelete]
		public void DeleteCustomer(int id)
		{
			var customerInDb = _context.Customer.SingleOrDefault(c => c.Id == id);

			if (customerInDb == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			_context.Customer.Remove(customerInDb);
		}
	}
}
