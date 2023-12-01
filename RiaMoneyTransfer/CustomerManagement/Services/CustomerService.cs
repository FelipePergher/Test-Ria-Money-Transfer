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
        private readonly string _customerJsonFile;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger, IConfiguration config)
        {
            _logger = logger;
            _customerJsonFile = config.GetValue<string>("customerFileName") ?? "customersFallback.json";
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
            File.WriteAllText(_customerJsonFile, jsonData);
        }

        private List<Customer> GetCustomersFromFile()
        {
            try
            {
                if (File.Exists(_customerJsonFile))
                {
                    var fileJson = File.ReadAllText(_customerJsonFile);
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
