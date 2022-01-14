using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Howest.Prog.CoinChop.Core.Models
{
    public abstract class ResultBase
    {
        protected IEnumerable<ValidationResult> _validationResults;

        public IEnumerable<ValidationResult> ValidationResults { get; set; } = new ValidationResult[0];
        public bool IsSuccess { get; set; }
    }
}
