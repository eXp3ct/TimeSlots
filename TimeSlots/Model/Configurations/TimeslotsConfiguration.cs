using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeSlots.Model.Configurations
{
	public class TimeslotsConfiguration : IEntityTypeConfiguration<Timeslot>
	{
		public void Configure(EntityTypeBuilder<Timeslot> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.From).HasMaxLength(15);
			builder.Property(x => x.To).HasMaxLength(15);
			builder.HasOne<Gate>(x => x.Gate)
				.WithMany(g => g.Timeslots)
				.HasForeignKey(x => x.GateId);
		}
	}
}
