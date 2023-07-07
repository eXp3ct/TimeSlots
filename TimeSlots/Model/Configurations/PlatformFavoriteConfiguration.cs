using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeSlots.Model.Configurations
{
	public class PlatformFavoriteConfiguration : IEntityTypeConfiguration<PlatformFavorite>
	{
		public void Configure(EntityTypeBuilder<PlatformFavorite> builder)
		{
			builder.HasKey(x => x.Id);
			builder.HasOne<Platform>()
				.WithMany(p => p.PlatformFavorites)
				.HasForeignKey(x => x.PlatformId);
			builder.HasOne<Company>()
				.WithMany(c => c.PlatformFavorites)
				.HasForeignKey(x => x.CompanyId);
		}
	}
}
