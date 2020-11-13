using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using S4U.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Persistance.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasIndex(e => new { e.Id, e.Name });

            builder.HasData(
                new Role
                {
                    Id = new Guid("a8fe4874-562f-4f4a-9f83-6b47fce6792d"),
                    Name = "Administrator"
                },
                new Role
                {
                    Id = new Guid("d06ada51-cfd0-463b-a855-ce7c9dbe8d63"),
                    Name = "Client"
                }
            );
        }
    }
}
