using System.Data.Entity;
using HomeManager.Domain.Entities.Media;
using HomeManager.Domain.Entities.Vehicle;

namespace HomeManager.Domain.Entities.Core {

    public class EntitiesContext : DbContext {

        public EntitiesContext() : base("HomeManager") { }
        
        // General Architectural Entities
        public IDbSet<User> Users { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<UserInRole> UserInRoles { get; set; }

        // Vehicle Entities
        public IDbSet<Car> Cars { get; set; }
        public IDbSet<FillUp> FillUps { get; set; }
        public IDbSet<Repair> Repairs { get; set; }
        public IDbSet<RepairShop> RepairShops { get; set; }

        //public IDbSet<MediaItem> MediaItems { get; set; }

        // Movie Entities
        public IDbSet<Movie> Movies { get; set; }
        public IDbSet<MovieGenre> MovieGenres { get; set; }
        public IDbSet<MovieInGenre> MovieInGenres { get; set; }
        public IDbSet<MovieFormat> MovieFormats { get; set; }

        // Image Entities
        public IDbSet<Image> Images { get; set; }
    }
}