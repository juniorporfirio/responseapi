using System;
using System.Collections.Generic;
using System.Linq;

namespace JuniorPorfirio.ResponseApi
{
    public static class CustomExtensions
    {
        /// <summary>
        /// Validate null value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">Notfound or Success</param>
        /// <returns></returns>
        public static IResponseApi IsNull<T>(this ResponseApi<T> response)
        {
            if (response.GetValue() is null)
                return ResponseApi<T>.NotFound();

            return ResponseApi<T>.Success(response.Value);
       
        }

        /// <summary>
        /// Validate collection exist anything
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">NotFound or Success</param>
        /// <returns></returns>
        public static ResponseApi<IEnumerable<T>> IsAny<T>(this ResponseApi<IEnumerable<T>> response)
        {
            if (response.Value.Any()) 
                return ResponseApi<IEnumerable<T>>.Success(response.Value);

            return ResponseApi<IEnumerable<T>>.NotFound();
        }

    }
}
