// <copyright file="IDenominationService.cs" company="Felipe Pergher">
// Copyright (c) Felipe Pergher. All Rights Reserved.
// </copyright>

using DenominationRoutineRia.Models.DTOs.In;
using DenominationRoutineRia.Models.DTOs.Out.Services;

namespace DenominationRoutineRia.Interfaces
{
    public interface IDenominationService
    {
        public List<DenominationServiceOutDto> GetPossibilities(DenominationServiceInDto denominationServicedInDto);
    }
}
