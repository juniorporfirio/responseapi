namespace JuniorPorfirio.ResponseApi
{
    /// <summary>
    /// StatusCode avaliable to ResponseApi 
    /// </summary>
    public enum ResponseApiStatusCode
    {
        Success = 200,
        Created = 201,
        Invalid = 400,
        Unauthorized = 401,
        Error = 500,
        NotFound=404
    }
}
