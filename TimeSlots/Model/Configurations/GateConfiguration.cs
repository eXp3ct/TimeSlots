using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeSlots.Model.Configurations
{
	public class GateConfiguration : IEntityTypeConfiguration<Gate>
	{
		public void Configure(EntityTypeBuilder<Gate> builder)
		{
			builder.HasKey(x => x.Id);
			builder.HasOne<Platform>().WithMany(x => x.Gates).HasForeignKey(x => x.PlatformId);
		}
	}
}
