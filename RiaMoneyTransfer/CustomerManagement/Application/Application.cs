// <copyright file="Application.cs" company="Felipe Pergher">
// Copyright (c) Felipe Pergher. All Rights Reserved.
// </copyright>

using System;
using System.Text;
using System.Text.Json;
using CustomerManagement.Interfaces;
using CustomerManagement.Models.Data;
using CustomerManagement.Models.DTOs;

namespace CustomerManagement.Application
{
    public class Application : IApplication
    {
        private readonly ICustomerService _customerService;
        private readonly string[] firstNames = { "Leia", "Sadie", "Jose", "Sara", "Frank", "Dewey", "Tomas", "Joel", "Lukas", "Carlos" };
        private readonly string[] lastNames = { "Liberty", "Ray", "Harrison", "Ronan", "Drew", "Powell", "Larsen", "Chan", "Anderson", "Lane" };
        private List<int> _ids = new List<int>();
        private List<Customer> _customers;
        private readonly Random _random = new();
        private readonly HttpClient _httpClient;

        public Application(ICustomerService customerService)
        {
            _customerService = customerService;
            _httpClient = new HttpClient();
            _customers = new List<Customer>();
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

            return _customers;
        }

        private void GetCustomersFromFileToMemory()
        {
            _customers = _customerService.GetCustomers();
        }

        private void AddItemToMemory(Customer customer)
        {
            if (!_customers.Any())
            {
                GetCustomersFromFileToMemory();
            }

            customer.LastName = customer.LastName.Trim();
            customer.FirstName = customer.FirstName.Trim();

            InsertCustomerSortedNames(customer);
        }

        private void SaveCustomersInFile(List<Customer> customers)
        {
            _customerService.SaveCustomers(customers);
        }

        private void InsertCustomerSortedNames(Customer newCustomer)
        {
            int index = 0;

            // Get the position for the last name
            while (index < _customers.Count
                   && string.Compare(newCustomer.LastName, _customers[index].LastName, StringComparison.InvariantCultureIgnoreCase) > 0)
            {
                index++;
            }

            // Get the position for the first name after last name is sorted
            while (index < _customers.Count
                   && string.Compare(newCustomer.LastName, _customers[index].LastName, StringComparison.InvariantCultureIgnoreCase) > 0
                   && string.Compare(newCustomer.FirstName, _customers[index].FirstName, StringComparison.InvariantCultureIgnoreCase) > 0)
            {
                index++;
            }

            _customers.Insert(index, newCustomer);
        }

        public async Task<string> TestCustomerAddLogic()
        {
            var returnMessage = new StringBuilder();

            var saveRequests = _random.NextInt64(2, 10);
            var getRequests = _random.NextInt64(2, 10);

            for (int i = 0; i < saveRequests; i++)
            {
                var message = new StringBuilder();
                message.AppendLine("____________________________________________");
                message.AppendLine($"Request {i + 1} of saveRequests");

                var listCustomers = new List<CustomerRequestInDto>();

                var customersQuantity = _random.NextInt64(2, 10);
                for (int j = 0; j < customersQuantity; j++)
                {
                    listCustomers.Add(CreateNewRandomCustomer());
                }

                message.AppendLine($"Customers request => {JsonSerializer.Serialize(listCustomers)}");

                var data = new StringContent(JsonSerializer.Serialize(listCustomers), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:44362/Customer/BatchAdd", data);
                if (response.IsSuccessStatusCode)
                {
                    string responseBodyAsText = await response.Content.ReadAsStringAsync();
                    message.AppendLine($"Response: {responseBodyAsText}");
                }

                message.AppendLine($"Status code: {response.StatusCode}");
                message.AppendLine($"Reason phrase: {response.ReasonPhrase}");
                message.AppendLine("____________________________________________");

                returnMessage.AppendLine(message.ToString());
            }

            for (int i = 0; i < getRequests; i++)
            {
                var message = new StringBuilder();

                message.AppendLine("____________________________________________");
                message.AppendLine($"Request {i + 1} of getRequests");

                HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:44362/Customer/GetAll");
                if (response.IsSuccessStatusCode)
                {
                    string responseBodyAsText = await response.Content.ReadAsStringAsync();
                    message.AppendLine($"Response: {responseBodyAsText}");
                }

                message.AppendLine($"Status code: {response.StatusCode}");
                message.AppendLine($"Reason phrase: {response.ReasonPhrase}");
                message.AppendLine("____________________________________________");

                returnMessage.AppendLine(message.ToString());
            }

            return returnMessage.ToString();
        }

        private CustomerRequestInDto CreateNewRandomCustomer()
        {
            var firstName = firstNames[_random.Next(firstNames.Length)];
            var lastName = lastNames[_random.Next(lastNames.Length)];
            var age = _random.Next(10, 90);
            var id = GenerateId();
            _ids.Add(id);

            return new CustomerRequestInDto(firstName, lastName, age, id);
        }

        private int GenerateId()
        {
            var id = _customers.Count > 0 ? _customers.Max(x => x.Id) + 1 : 0;

            while (_ids.Contains(id))
            {
                id++;
            }

            return id;
        }
    }
}
