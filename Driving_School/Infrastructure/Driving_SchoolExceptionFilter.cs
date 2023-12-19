using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Driving_School.Services.Contracts.Exceptions;
using Driving_School.Api.Models.Exceptions;

namespace Driving_School.Api.Infrastructure
{
    /// <summary>
    /// Фильтр для обработки ошибок раздела администрирования
    /// </summary>
    public class Driving_SchoolExceptionFilter : IExceptionFilter
    {
        /// <inheritdoc/>
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception as Driving_SchoolException;
            if (exception == null)
            {
                return;
            }

            switch (exception)
            {
                case Driving_SchoolValidationException ex:
                    SetDataToContext(
                        new ConflictObjectResult(new ApiValidationExceptionDetail
                        {
                            Errors = ex.Errors,
                        }),
                        context);
                    break;

                case Driving_SchoolInvalidOperationException ex:
                    SetDataToContext(
                        new BadRequestObjectResult(new ApiExceptionDetail { Message = ex.Message, })
                        {
                            StatusCode = StatusCodes.Status406NotAcceptable,
                        },
                        context);
                    break;

                case Driving_SchoolNotFoundException ex:
                    SetDataToContext(new NotFoundObjectResult(new ApiExceptionDetail
                    {
                        Message = ex.Message,
                    }), context);
                    break;

                default:
                    SetDataToContext(new BadRequestObjectResult(new ApiExceptionDetail
                    {
                        Message = exception.Message,
                    }), context);
                    break;
            }
        }

        /// <summary>
        /// Определяет контекст ответа
        /// </summary>
        static protected void SetDataToContext(ObjectResult data, ExceptionContext context)
        {
            context.ExceptionHandled = true;
            var response = context.HttpContext.Response;
            response.StatusCode = data.StatusCode ?? StatusCodes.Status400BadRequest;
            context.Result = data;
        }
    }
}

