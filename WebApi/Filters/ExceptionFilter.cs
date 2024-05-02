using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Domain.Exceptions;

namespace WebApi.Filters
{
    public class ExceptionFilter: IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            try
            {
                throw context.Exception;
            }
            catch (KeyNotFoundException exception)
            {
                context.Result = new JsonResult(exception.Message) { StatusCode = 404 };
            }
            catch (ArgumentNullException exception)
            {
                context.Result = new JsonResult(exception.Message) { StatusCode = 400 };
            }
            catch (ArgumentOutOfRangeException exception)
            {
                context.Result = new JsonResult(exception.Message) { StatusCode = 400 };
            }
            catch (InvalidDataException exception)
            {
                context.Result = new JsonResult(exception.Message) { StatusCode = 400 };
            }
            catch (ArgumentException exception)
            {
                context.Result = new JsonResult(exception.Message) { StatusCode = 400 };
            }
            catch (InvalidOperationException exception)
            {
                context.Result = new JsonResult(exception.Message) { StatusCode = 400 };
            }
            catch (WrongEmailFormatException exception)
            {
                context.Result = new JsonResult(exception.Message) { StatusCode = 400 };
            }
            catch (PasswordNotFollowPolicy exception)
            {
                context.Result = new JsonResult(exception.Message) { StatusCode = 400 };
            }
            catch (Exception)
            {
                context.Result = new JsonResult("Internal server error") { StatusCode = 500 };
            }
        }
    }
}
