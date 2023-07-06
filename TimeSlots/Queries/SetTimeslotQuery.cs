using AutoMapper;
using MediatR;
using TimeSlots.Mapping;
using TimeSlots.Model;
using TimeSlots.Model.Enums;

namespace TimeSlots.Queries
{
	public class SetTimeslotQuery : IRequest
	{
		public DateTime Date { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public TaskType TaskType { get; set; }

		public SetTimeslotQuery(DateTime date, DateTime start, DateTime end, TaskType taskType)
		{
			Date = date;
			Start = start;
			End = end;
			TaskType = taskType;
		}
		public SetTimeslotQuery()
        {
        }
	}
}
