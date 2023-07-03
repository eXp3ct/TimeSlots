﻿using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TimeSlots.Model;
using TimeSlots.Queries;

namespace TimeSlots.Mapping
{
	public class TimeslotsMappingProfile : Profile
	{
        public TimeslotsMappingProfile()
        {
			CreateMap<SetTimeslotQuery, Timeslot>()
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