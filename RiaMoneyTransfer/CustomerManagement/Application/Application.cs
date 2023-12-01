// <copyright file="Application.cs" company="Felipe Pergher">
// Copyright (c) Felipe Pergher. All Rights Reserved.
// </copyright>

using CustomerManagement.Interfaces;
using CustomerManagement.Models.Data;
using CustomerManagement.Models.DTOs;

namespace CustomerManagement.Application
{
    public class Application : IApplication
    {
        private Customer[] _customers;
        private readonly ICustomerService _customerService;

        public Application(ICustomerService customerService)
        {
            _customerService = customerService;
            _customers = Array.Empty<Customer>();
        }

        public void ValidateCustomers(List<CustomerRequestInDto> customersRequest)
        {
            var customers = _customerService.GetCustomers();

            foreach (var customerRequestInDto in customersRequest)
            {
                // Check if the customer is already in the list received
                if (customersRequest.Count(x => x.Id == customerRequestInDto.Id) > 1)
                {
                    throw new Exception($"The id {customerRequestInDto.Id} is already in the list");
                }

                // Check if have already one customer with the same id saved
                if (customers.Any(x => x.Id == customerRequestInDto.Id))
                {
                    throw new Exception($"The id {customerRequestInDto.Id} is already being used");
                }
            }
        }

        public void SaveCustomers(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                AddItemToMemory(customer);
            }

            SaveCustomersInFile(_customers.ToList());
        }

        public List<Customer> GetCustomers()
        {
            if (!_customers.Any())
            {
                GetCustomersFromFileToMemory();
            }

            return _customers.ToList();
        }

        private void GetCustomersFromFileToMemory()
        {
            _customers = _customerService.GetCustomers().ToArray();
        }

        private void AddItemToMemory(Customer customer)
        {
            if (!_customers.Any())
            {
                GetCustomersFromFileToMemory();
            }

            // TODO add the item to the list and sort it by first name and last name
            _customers = _customers.Append(customer).ToArray();
        }

        private void SaveCustomersInFile(List<Customer> customers)
        {
            _customerService.SaveCustomers(customers);
        }
    }
}
