using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TEST.Domain.Entities;

namespace TEST.Infrastructure.Persistences.Contexts.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Domain.Entities.TaskEntity>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.TaskEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("TaskId");

            builder.Property(e => e.Name).HasMaxLength(100);
        }
    }
}