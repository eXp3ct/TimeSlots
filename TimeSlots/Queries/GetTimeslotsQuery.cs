﻿using MediatR;
using TimeSlots.DataBase;
using TimeSlots.Model;

namespace TimeSlots.Queries
{
    public class GetTimeslotsQuery : IRequest<IList<TimeslotDto>>
    {
        public DateTime Date { get; set; }
		public int Pallets { get; set; }

		public GetTimeslotsQuery(DateTime date, int pallets)
		{
			Date = date;
			Pallets = pallets;
		}
	}
}
