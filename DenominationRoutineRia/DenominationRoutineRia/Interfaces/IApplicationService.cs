// <copyright file="IApplicationService.cs" company="Felipe Pergher">
// Copyright (c) Felipe Pergher. All Rights Reserved.
// </copyright>

using DenominationRoutineRia.DTOs.Out;

namespace DenominationRoutineRia.Interfaces
{
    public interface IApplicationService
    {
        public DenominationsDto RunAllCases();
    }
}
