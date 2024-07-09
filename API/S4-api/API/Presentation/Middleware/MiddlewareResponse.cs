namespace s4.Presentation.Middleware
{
    public class MiddlewareResponse<T>
    {
        public MiddlewareResponse() { }

        public MiddlewareResponse(T data)
        {
            this.Status = 200;
            this.Data = data;
            this.error.Message = null;
        }

        public int Status { get; set; }

        public T Data { get; set; }

        public Error error = new();

        public class Error
        {
            public string Message { get; set; }
            public IDictionary<string, List<string>> Details { get; set; }
        }
    }
}
