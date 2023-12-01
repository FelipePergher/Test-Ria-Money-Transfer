// <copyright file="CustomerService.cs" company="Felipe Pergher">
// Copyright (c) Felipe Pergher. All Rights Reserved.
// </copyright>

using CustomerManagement.Interfaces;
using CustomerManagement.Models.Data;
using System.Text.Json;

namespace CustomerManagement.Services
{
    public class CustomerService : ICustomerService
    {
        // Todo move to AppSettings
        private const string CustomerJsonFile = "data.json";
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public void SaveCustomers(List<Customer> customers)
        {
            SaveCustomersInFile(customers);
        }

        public List<Customer> GetCustomers()
        {
            var customers = GetCustomersFromFile();

            return customers;
        }

        private void SaveCustomersInFile(List<Customer> customers)
        {
            var jsonData = JsonSerializer.Serialize(customers);
            File.WriteAllText(CustomerJsonFile, jsonData);
        }

        private List<Customer> GetCustomersFromFile()
        {
            try
            {
                if (File.Exists(CustomerJsonFile))
                {
                    var fileJson = File.ReadAllText(CustomerJsonFile);
                    var customers = JsonSerializer.Deserialize<List<Customer>>(fileJson);
                    return customers ?? new List<Customer>();
                }

                return new List<Customer>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting file with customers data: {ex}");
                throw ex;
            }
        }
    }
}
