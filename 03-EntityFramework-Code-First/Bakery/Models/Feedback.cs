namespace Bakery
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Feedback
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public decimal? Rate { get; set; }

        public int? ProductId { get; set; }

        public int? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}
