using Microsoft.AspNetCore.Mvc;

namespace JuniorPorfirio.ResponseApi.AspNetCore
{
    public static class ResponseApiExtesions
    {
        public static ActionResult<T>
            ToActionResult<T>(this ControllerBase controller,
            ResponseApi<T> response)
        { 
            foreach (var error in response.Errors)
                controller.ModelState.AddModelError(error.Key, error.Value);
               
            var value = response.GetValue() ?? response.Message;

            return controller.StatusCode((int)response.Status, value);
        }

    }
}
