using JuniorPorfirio.ResponseApi;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ResponseApi.AspNetCore
{
    public static class ResponseApiExtesions
    {
        public static ActionResult<T>
            ToActionResult<T>(this ControllerBase controller,
            ResponseApi<T> response)
        {

            if (response.Status == ResponseApiStatusCode.Invalid)
            {
                foreach (var error in response.Errors)
                    controller.ModelState.AddModelError(error.Key, error.Value);
                controller.BadRequest(controller.ModelState);

            }
            var value = response.GetValue() ?? response.Message;
            return controller.StatusCode((int)response.Status, value);
        }

    }
}
