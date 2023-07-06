using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeSlots.Model.Configurations
{
	public class CompanyConfiguration : IEntityTypeConfiguration<Company>
	{
		public void Configure(EntityTypeBuilder<Company> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).HasMaxLength(256);
			builder.HasOne<Platform>().WithMany(p => p.Companies).HasForeignKey(x => x.PlatformId);
		}
	}
}
