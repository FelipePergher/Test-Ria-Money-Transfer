// <copyright file="Application.cs" company="Felipe Pergher">
// Copyright (c) Felipe Pergher. All Rights Reserved.
// </copyright>

using DenominationRoutineRia.Interfaces;
using DenominationRoutineRia.Models.DTOs.In;
using DenominationRoutineRia.Models.DTOs.Out.Application;
using DenominationRoutineRia.Models.DTOs.Out.Services;

namespace DenominationRoutineRia.Application
{
    public class Application : IApplication
    {
        private readonly IDenominationService _denominationService;

        public Application(IDenominationService denominationService)
        {
            _denominationService = denominationService;
        }

        public DenominationApplicationOutDto RunAllCases()
        {
            var availableCash = new List<double> { 10, 50, 100 };
            var amountsToValidate = new List<double> { 30, 50, 60, 80, 140, 230, 370, 610, 980 };

            var denominations = new DenominationApplicationOutDto();

            foreach (var amount in amountsToValidate)
            {
                List<DenominationServiceOutDto> possibilities = _denominationService.GetPossibilities(new DenominationServiceInDto(amount, availableCash));

                denominations.Denominations.Add(new DenominationApplicationItemOutDto
                {
                    Amount = amount,
                    Possibilities = possibilities.Select(x => x.CashValues.GroupBy(z => z)
                        .Select(y => new DenominationApplicationItemDetailsOutDto
                        {
                            Quantity = y.Count(),
                            Amount = y.Key
                        }).ToList()).ToList()
                });
            }

            return denominations;
        }
    }
}
