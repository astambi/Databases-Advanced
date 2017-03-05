namespace Bakery
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ingredient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ingredient()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int? OriginCountryId { get; set; }

        public int? DistributorId { get; set; }

        public virtual Country Country { get; set; }

        public virtual Distributor Distributor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
