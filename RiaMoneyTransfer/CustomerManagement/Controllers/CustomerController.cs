// <copyright file="CustomerController.cs" company="Felipe Pergher">
// Copyright (c) Felipe Pergher. All Rights Reserved.
// </copyright>

using CustomerManagement.Interfaces;
using CustomerManagement.Models.Data;
using CustomerManagement.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CustomerManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IApplication _application;

        public CustomerController(ILogger<CustomerController> logger, IApplication application)
        {
            _logger = logger;
            _application = application;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_application.GetCustomers());
        }

        [HttpPost("BatchAdd")]
        public IActionResult BatchAdd(List<CustomerRequestInDto> customers)
        {
            _logger.LogInformation(JsonSerializer.Serialize(customers));
            if (ModelState.IsValid == false)
            {
                _logger.LogWarning("Invalid Model:");
                _logger.LogWarning(JsonSerializer.Serialize(ModelState));

                return BadRequest("Invalid request");
            }

            try
            {
                _application.ValidateCustomers(customers);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Invalid request");
            }

            _application.SaveCustomers(customers.Select(x => new Customer(x.Id.Value, x.FirstName, x.LastName, x.Age)).ToList());

            return Ok(_application.GetCustomers());
        }
    }
}
