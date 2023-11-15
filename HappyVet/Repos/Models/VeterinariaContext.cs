using HappyVet.Models;
using Microsoft.EntityFrameworkCore;

namespace HappyVet.Repos.Models
{
    public partial class HappyVetContext : DbContext
    {
        public HappyVetContext() { }

        public HappyVetContext(DbContextOptions<HappyVetContext> options) 
            : base(options) 
        {
        }

        public virtual DbSet<TipoAnimal> TipoAnimales { get; set; }
        public virtual DbSet<Raza> Razas { get; set; }
        public virtual DbSet<Tamaño> Tamaños { get; set; }
        public virtual DbSet<Edad> Edades { get; set; }
        public virtual DbSet<RegistroMascota> RegistroMascotas { get; set; }
        public virtual DbSet<Vacuna> Vacunas { get; set; }
        public virtual DbSet<ListaPrecio> ListaPrecios { get; set; }
        public virtual DbSet<Detalle> Detalles { get; set; }

        public virtual DbSet<Consulta> Consultas { get; set; }
        public virtual DbSet<ConsultaVacuna> ConsultaVacunas { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);



            modelBuilder.Entity<Consulta>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ConsultaVacuna>().Property(t => t.Id).ValueGeneratedOnAdd();


        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }


}

