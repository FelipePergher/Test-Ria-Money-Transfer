// <copyright file="DenominationController.cs" company="Felipe Pergher">
// Copyright (c) Felipe Pergher. All Rights Reserved.
// </copyright>

using DenominationRoutineRia.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DenominationRoutineRia.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DenominationController : ControllerBase
    {
        private readonly ILogger<DenominationController> _logger;
        private readonly IApplicationService _applicationService;

        public DenominationController(ILogger<DenominationController> logger, IApplicationService applicationService)
        {
            _logger = logger;
            _applicationService = applicationService;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            try
            {
                var denominations = _applicationService.RunAllCases();
                return Ok(denominations);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while running all cases");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
