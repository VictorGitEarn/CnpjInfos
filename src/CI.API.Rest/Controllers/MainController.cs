using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CI.API.Rest.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        protected ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidateOperation())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", Errors.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddRequestError(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool ValidateOperation()
        {
            return !Errors.Any();
        }

        protected void AddRequestError(string erro)
        {
            Errors.Add(erro);
        }

        protected void CleanErrors()
        {
            Errors.Clear();
        }
    }
}
