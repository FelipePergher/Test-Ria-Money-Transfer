// <copyright file="IApplication.cs" company="Felipe Pergher">
// Copyright (c) Felipe Pergher. All Rights Reserved.
// </copyright>

using DenominationRoutineRia.Models.DTOs.Out.Application;

namespace DenominationRoutineRia.Interfaces
{
    public interface IApplication
    {
        public DenominationApplicationOutDto RunAllCases();
    }
}
