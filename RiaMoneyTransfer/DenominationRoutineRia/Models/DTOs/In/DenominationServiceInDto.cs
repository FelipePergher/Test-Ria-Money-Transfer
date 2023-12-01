// <copyright file="DenominationServiceInDto.cs" company="Felipe Pergher">
// Copyright (c) Felipe Pergher. All Rights Reserved.
// </copyright>

namespace DenominationRoutineRia.Models.DTOs.In
{
    public record DenominationServiceInDto(double Amount, List<double> AvailableCash)
    {
        public double Amount { get; set; } = Amount;

        public List<double> AvailableCash { get; set; } = AvailableCash;
    }
}
