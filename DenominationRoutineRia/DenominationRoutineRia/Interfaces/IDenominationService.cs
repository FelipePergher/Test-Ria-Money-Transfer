// <copyright file="IDenominationService.cs" company="Felipe Pergher">
// Copyright (c) Felipe Pergher. All Rights Reserved.
// </copyright>

using DenominationRoutineRia.DTOs.Out;

namespace DenominationRoutineRia.Interfaces
{
    public interface IDenominationService
    {
        public DenominationOutDto GetPossibilities(double amount);
    }
}
