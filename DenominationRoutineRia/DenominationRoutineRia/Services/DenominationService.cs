// <copyright file="DenominationService.cs" company="Felipe Pergher">
// Copyright (c) Felipe Pergher. All Rights Reserved.
// </copyright>

using DenominationRoutineRia.Interfaces;
using DenominationRoutineRia.Models.DTOs.In;
using DenominationRoutineRia.Models.DTOs.Out.Services;

namespace DenominationRoutineRia.Services
{
    public class DenominationService : IDenominationService
    {
        public List<DenominationServiceOutDto> GetPossibilities(DenominationServiceInDto denominationServicedInDto)
        {
            var denomination = new List<DenominationServiceOutDto>();

            GetPossibilitiesLogic(denominationServicedInDto.Amount, denominationServicedInDto.AvailableCash.OrderBy(x => x).ToList(), new(), denomination, 0);

            return denomination;
        }

        private void GetPossibilitiesLogic(double amount, List<double> availableCash, List<double> currentPossibilities, List<DenominationServiceOutDto> denomination, int index)
        {
            if (amount == 0)
            {
                denomination.Add(new DenominationServiceOutDto { CashValues = currentPossibilities.ToList() });
                return;
            }

            for (var i = index; i < availableCash.Count; i++)
            {
                var cashValue = availableCash[i];
                if (amount >= cashValue)
                {
                    currentPossibilities.Add(cashValue);

                    var currentCash = amount - cashValue;
                    GetPossibilitiesLogic(currentCash, availableCash, currentPossibilities, denomination, i);

                    // Remove the last item from the list
                    currentPossibilities.RemoveAt(currentPossibilities.Count - 1);
                }
            }
        }
    }
}
