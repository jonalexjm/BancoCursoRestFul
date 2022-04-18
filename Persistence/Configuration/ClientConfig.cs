using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class ClientConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(t => t.Id);

            builder.Property(p => p.Nombre)
                   .HasMaxLength(80)
                   .IsRequired();

            builder.Property(p => p.Apellido)
                   .HasMaxLength(80)
                   .IsRequired();

            builder.Property(p => p.FechaNacimiento)                  
                   .IsRequired();

            builder.Property(p => p.Telefono)   
                    .HasMaxLength(9)
                   .IsRequired();

            builder.Property(p => p.Email)
                    .HasMaxLength(100);


            builder.Property(p => p.Direccion)
                  .HasMaxLength(100)
                  .IsRequired();

            builder.Property(p => p.Edad);


            builder.Property(p => p.CreateBy)
                   .HasMaxLength(30);

            builder.Property(p => p.LastModifiedBy)
                  .HasMaxLength(30);




        }
    }
}
