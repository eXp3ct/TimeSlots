using AutoMapper;
using System.Reflection;

namespace TimeSlots.Mapping
{
	//public class AssemblyMappingProfile : Profile
	//{
	//	public AssemblyMappingProfile(Assembly assembly)
	//	{
	//		// Apply mappings from the specified assembly
	//		ApplyMappingsFromAssembly(assembly);
	//	}
 //       public AssemblyMappingProfile()
 //       {
 //           ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
 //       }
 //       private void ApplyMappingsFromAssembly(Assembly assembly)
	//	{
	//		// Get a list of types in the assembly that implement the IMapWith<> interface
	//		var types = assembly.GetExportedTypes()
	//			.Where(type => type.GetInterfaces()
	//				.Any(i => i.IsGenericType &&
	//				i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
	//			.ToList();

	//		// For each type that was found, create an instance of the type and call its Mapping method
	//		foreach (var type in types)
	//		{
	//			var instance = Activator.CreateInstance(type);
	//			var methodInfo = type.GetMethod("Mapping");
	//			methodInfo?.Invoke(instance, new object[] { this });
	//		}
	//	}
	//}
}
