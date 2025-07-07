using Microsoft.CodeAnalysis;

namespace Api.Responses
{
    public class ApiResponse<T>
    {
		private object? entityResp;
		private int v;

		public int Status { get; set; }
        public T Data { get; set; }
        public Metadata Meta { get; set; }
        public int TotalRecords { get; set; }
        public ApiResponse(T data, int status)
        {
            Data = data;
            Status = status;

        }
        public ApiResponse(T data, int status, int totalRecords)
        {
            Data = data;
            Status = status;
            TotalRecords = totalRecords;
        }

		public ApiResponse(object? entityResp, int v)
		{
			this.entityResp = entityResp;
			this.v = v;
		}
	}
}
