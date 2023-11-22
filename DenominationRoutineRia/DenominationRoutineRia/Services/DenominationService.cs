// <copyright file="DenominationService.cs" company="Felipe Pergher">
// Copyright (c) Felipe Pergher. All Rights Reserved.
// </copyright>

using DenominationRoutineRia.DTOs.Out;
using DenominationRoutineRia.Interfaces;

namespace DenominationRoutineRia.Services
{
    public class DenominationService : IDenominationService
    {
        public DenominationOutDto GetPossibilities(double amount)
        {
            var result = new DenominationOutDto
            {
                Amount = amount
            };

            // TODO calculate the possibilities
            result.Possibilities.Add(GetPossibilities1(amount));

            return result;
        }

        private DenominationOutItemDto GetPossibilities1(double amount)
        {
            var result = new DenominationOutItemDto
            {
                Quantity = 1
            };

            // TODO calculate the possibilities
            return result;
        }
    }
}
