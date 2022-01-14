using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Howest.Prog.CoinChop.Core.Models
{
    /// <summary>
    /// Simplifies validation of a single model using the Validator type
    /// </summary>
    public class ModelValidator
    {
        public bool TryValidateModel<T>(T model, ICollection<ValidationResult> validationResults)
        {
            var validationContext = new ValidationContext(model);
            return Validator.TryValidateObject(model, validationContext, validationResults);
        }
    }
}
