using System.Collections.Generic;

namespace JuniorPorfirio.ResponseApi
{
    public interface IResponseApi
    {
        object GetValue();
        string Message { get; }
        IDictionary<string, string> Errors { get; }
        ResponseApiStatusCode Status { get; }

    }
}
