﻿// <copyright file="ICustomerService.cs" company="Felipe Pergher">
// Copyright (c) Felipe Pergher. All Rights Reserved.
// </copyright>

using CustomerManagement.Models.Data;

namespace CustomerManagement.Interfaces
{
    public interface ICustomerService
    {
        public void SaveCustomers(List<Customer> customers);

        public List<Customer> GetCustomers();
    }
}
