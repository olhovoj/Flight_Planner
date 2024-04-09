using System.Net;
using FlightPlanner.UseCases.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Extensions;

public static class ServiceResultExtensions
{
    public static IActionResult ToActionResult(this ServiceResult result)
    {
        return result.Status switch
        {
            HttpStatusCode.NotFound => new NotFoundResult(),
            HttpStatusCode.OK => new OkObjectResult(result.ResultObject),
            HttpStatusCode.BadRequest => new BadRequestObjectResult(result.ResultObject),
            HttpStatusCode.Conflict => new ConflictObjectResult(result.ResultObject),
            HttpStatusCode.Created => new CreatedResult(string.Empty, result.ResultObject),
            _ => new OkResult()
        };
    }
}