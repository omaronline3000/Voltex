namespace Voltex.API.Helper
{
    public class ResponseApi<T>
    {
        public string? Message { get; set; }
        public T Data { get; set; }
        public ResponseApi(T data, string? message = null)
        {
            Message = message;
            Data = data;
        }
    }
}
