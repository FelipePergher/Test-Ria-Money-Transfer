// <copyright file="DenominationServiceInDto.cs" company="Felipe Pergher">
// Copyright (c) Felipe Pergher. All Rights Reserved.
// </copyright>

namespace DenominationRoutineRia.Models.DTOs.In
{
    public class DenominationServiceInDto
    {
        public DenominationServiceInDto(double amount, List<double> availableCash)
        {
            Amount = amount;
            AvailableCash = availableCash;
        }

        public double Amount { get; set; }

        public List<double> AvailableCash { get; set; }
    }
}
