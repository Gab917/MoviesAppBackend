namespace MoviesApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rental")]
    public partial class Rental
    {
        public int RentalId { get; set; }

        public DateTime RentalDate { get; set; }

        public int CustomerId { get; set; }

        public int MovieId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
