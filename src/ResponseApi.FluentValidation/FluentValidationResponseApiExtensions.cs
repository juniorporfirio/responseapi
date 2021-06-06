using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace JuniorPorfirio.ResponseApi.FluentValidation
{
    public static class FluentValidationResponseApiExtensions
    {
        public static  Dictionary<string,string> AsMessages(this ValidationResult  validation)
        {

            return validation.Errors.ToDictionary(k => k.PropertyName, v => v.ErrorMessage);
        }

        public static ResponseApi<T> IsInvalid<T>(this ResponseApi<T> response, ValidationResult validation )
        {
            if (validation.IsValid)
                return ResponseApi<T>.Success(response.Value);
            else
                return ResponseApi<T>.Invalid(validation.AsMessages());
        }

       
    }
}
