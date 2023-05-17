namespace MoviesApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Newtonsoft.Json;

    [Table("Rental")]
    public partial class Rental
    {
        public int RentalId { get; set; }

        public DateTime RentalDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public int CustomerId { get; set; }

        public int MovieId { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }
        [JsonIgnore]
        public virtual Movie Movie { get; set; }
        public string MovieTitle => Movie == null ? null : Movie.Title;
        public string CustomerName => Customer == null ? null : Customer.FullName;
        //public string MovieTitle => Movie.Title;
        //public string CustomerName => Customer.FullName;
    }
}
