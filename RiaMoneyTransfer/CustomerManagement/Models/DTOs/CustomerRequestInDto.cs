// <copyright file="CustomerRequestInDto.cs" company="Felipe Pergher">
// Copyright (c) Felipe Pergher. All Rights Reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models.DTOs
{
    public record CustomerRequestInDto
    {
        public CustomerRequestInDto(string firstName, string lastName, int age, int? id)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Id = id;
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [Range(minimum: 18, maximum: 150)]
        public int Age { get; set; }

        [Required]
        public int? Id { get; set; }
    }
}
