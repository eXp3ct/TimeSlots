﻿using MediatR;
using TimeSlots.DataBase;
using TimeSlots.Model;
using TimeSlots.Model.Enums;

namespace TimeSlots.Queries
{
    public class GetTimeslotsQuery : IRequest<IEnumerable<TimeslotDto>>
    {
        public DateTime Date { get; set; }
		public int Pallets { get; set; }
        public TaskType TaskType { get; set; }

		public GetTimeslotsQuery(DateTime date, int pallets, TaskType taskType)
		{
			Date = date;
			Pallets = pallets;
			TaskType = taskType;
		}

		public GetTimeslotsQuery()
        {
            
        }
    }
}
