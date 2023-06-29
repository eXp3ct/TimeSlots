using AutoMapper;
using MediatR;
using TimeSlots.Mapping;
using TimeSlots.Model;

namespace TimeSlots.Queries
{
	public class SetTimeslotQuery : IRequest, IMapWith<Timeslot>
	{
		public DateTime Date { get; set; }
		public string Start { get; set; }
		public string End { get; set; }

		public SetTimeslotQuery(DateTime date, string start, string end)
		{
			Date = date;
			Start = start;
			End = end;
		}
        public SetTimeslotQuery()
        {
        }

        public void Mapping(Profile profile)
		{
			profile.CreateMap<SetTimeslotQuery, Timeslot>()
				.ForMember(timeslot => timeslot.Id,
					opt => opt.MapFrom(query => Guid.NewGuid()))
				.ForMember(timeslot => timeslot.Date,
					opt => opt.MapFrom(query => query.Date))
				.ForMember(timeslot => timeslot.From,
					opt => opt.MapFrom(query => query.Start))
				.ForMember(timeslot => timeslot.To,
					opt => opt.MapFrom(query => query.End))
				.ForMember(timeslot => timeslot.GateId,
					opt => opt.Ignore())
				.ForMember(timeslot => timeslot.UserId,
					opt => opt.MapFrom(query => Guid.NewGuid()));
		}
	}
}
