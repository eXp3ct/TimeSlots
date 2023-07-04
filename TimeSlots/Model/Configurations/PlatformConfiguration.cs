using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeSlots.Model.Configurations
{
	public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
	{
		public void Configure(EntityTypeBuilder<Platform> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).HasMaxLength(256);
			builder.HasMany(x => x.Gates)
				.WithOne()
				.HasForeignKey(x => x.PlatformId);
		}
	}
}
