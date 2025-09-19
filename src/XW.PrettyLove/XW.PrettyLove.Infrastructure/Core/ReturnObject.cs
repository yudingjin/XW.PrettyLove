namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// 统一的返回泛型对象
    /// </summary>
    public class ReturnObject<T>
    {
        public bool Success { get; set; } = true;

        public T Data { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }
    }
}
