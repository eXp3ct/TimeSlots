using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeSlots.Model.Enums;

namespace TimeSlots.Model.Configurations
{
	public class GateScheduleConfiguration : IEntityTypeConfiguration<GateSchedule>
	{
		public void Configure(EntityTypeBuilder<GateSchedule> builder)
		{
			builder.HasKey(x => x.Id);
			builder.HasOne<Gate>()
				.WithMany(g => g.GateSchedules)
				.HasForeignKey(x => x.GateId);
			builder.HasOne<Company>()
				.WithOne(c => c.GateSchedule)
				.HasForeignKey<GateSchedule>(x => x.CompanyId);
			//builder.HasOne(x => x.Company)
			//	.WithOne(c => c.GateSchedule)
			//	.HasForeignKey<GateSchedule>(x => x.CompanyId);
		}
	}
}
