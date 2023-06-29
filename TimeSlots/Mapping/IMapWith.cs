using AutoMapper;

namespace TimeSlots.Mapping
{
	public interface IMapWith<T> where T : class
	{
		public void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
	}
}
