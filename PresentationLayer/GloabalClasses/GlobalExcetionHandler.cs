using BusinessLayer.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace App.API.GloabalClasses
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
           
            _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

            int statusCode;
            string title;
            string detail;

           
            switch (exception)
            {
              
                case BusinessException businessEx:
                    statusCode = StatusCodes.Status400BadRequest;
                    title = "Business Rule Violation";
                    detail = businessEx.Message;
                    break;

             
                case DbUpdateException dbEx when dbEx.InnerException is SqlException sqlEx && sqlEx.Number == 547:
                    statusCode = StatusCodes.Status409Conflict;
                    title = "Database Conflict";
                    detail = "Cannot complete this action because this record is referenced by other entities.";
                    break;

              
                case DbUpdateException dbEx when dbEx.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627):
                    statusCode = StatusCodes.Status409Conflict;
                    title = "Duplicate Entity";
                    detail = "A record with the same unique data already exists.";
                    break;

               
                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    title = "Internal Server Error";
                    detail = "An unexpected error occurred on the server. Please try again later.";
                    break;
            }

            
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail
            };

           
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true; 
        }
    }
}