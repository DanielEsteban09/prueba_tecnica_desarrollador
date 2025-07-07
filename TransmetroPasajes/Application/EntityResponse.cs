using Core.ModelResponse;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class EntityResponse<T>
    {
        public bool Estado { get; set; }

        public List<ResponseAction> ResponseAction { get; set; } = new List<ResponseAction>();

        public T? Entity { get; set; }

        public IList<ValidationFailure> ValidationErrors { get; set; }

        public string ErrorMensaje { get; set; }

        public EntityResponse()
        {
            ValidationErrors = new List<ValidationFailure>();
        }
    }
}
