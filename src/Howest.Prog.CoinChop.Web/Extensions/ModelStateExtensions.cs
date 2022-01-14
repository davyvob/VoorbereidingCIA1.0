using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Howest.Prog.CoinChop.Web.Extensions
{
    public static class ModelStateExtensions
    {
        /// <summary>
        /// Clears given ModelState dictionary and adds new errors from a given ValidationResult collection
        /// </summary>
        /// <param name="modelState">The ModelStateDictionary to add to</param>
        /// <param name="validationResults">A collection of ValidationResults to add to the ModelState </param>
        /// <param name="memberNamePrefix">Optional prefix for each membername</param>
        public static void MapValidationResults(this ModelStateDictionary modelState, IEnumerable<ValidationResult> validationResults, string memberNamePrefix = "")
        {
            modelState.Clear();
            foreach (var error in validationResults)
            {
                foreach (var memberName in error.MemberNames)
                {
                    modelState.AddModelError($"{memberNamePrefix}{memberName}", error.ErrorMessage);
                }
            }
        }

    }
}
