using AutoMapper;

namespace TimeSlots.Mapping
{
	public interface IMapWith<TDestination> where TDestination : class
	{
		public void Mapping(Profile profile) => profile.CreateMap(typeof(TDestination), GetType());
	}
}
