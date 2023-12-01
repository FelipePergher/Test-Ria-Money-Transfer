// <copyright file="IApplication.cs" company="Felipe Pergher">
// Copyright (c) Felipe Pergher. All Rights Reserved.
// </copyright>

using CustomerManagement.Models.Data;
using CustomerManagement.Models.DTOs;

namespace CustomerManagement.Interfaces
{
    public interface IApplication
    {
        public void ValidateCustomers(List<CustomerRequestInDto> customers);

        public void SaveCustomers(List<Customer> customers);

        public List<Customer> GetCustomers();

        public Task<string> TestCustomerAddLogic();
    }
}
