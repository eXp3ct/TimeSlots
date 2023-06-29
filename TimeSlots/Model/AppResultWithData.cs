using MediatR;

namespace TimeSlots.Model
{
	public class AppResultWithData<TData> : AppResult where TData : IBaseRequest
	{
		public TData Data { get; set; }
		public AppResultWithData(string message, bool isError, TData data) : base(message, isError)
		{
			Data = data;
		}

	}
}
