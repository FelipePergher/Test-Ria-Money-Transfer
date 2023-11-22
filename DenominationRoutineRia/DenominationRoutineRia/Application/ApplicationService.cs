// <copyright file="ApplicationService.cs" company="Felipe Pergher">
// Copyright (c) Felipe Pergher. All Rights Reserved.
// </copyright>

using DenominationRoutineRia.DTOs.Out;
using DenominationRoutineRia.Interfaces;

namespace DenominationRoutineRia.Application
{
    public class ApplicationService : IApplicationService
    {
        private readonly IDenominationService _denominationService;

        public ApplicationService(IDenominationService denominationService)
        {
            _denominationService = denominationService;
        }

        public DenominationsDto RunAllCases()
        {
            // TODO go trough all cases and pass the cartridges types as parameter
            var denominations = new DenominationsDto();

            // TODO run trough all cases and pass the amount as parameter
            denominations.Denominations.Add(_denominationService.GetPossibilities(1.0));

            return denominations;
        }
    }
}
