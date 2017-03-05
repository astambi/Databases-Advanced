namespace Bakery
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Distributor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Distributor()
        {
            Ingredients = new HashSet<Ingredient>();
        }

        public int Id { get; set; }

        [StringLength(25)]
        public string Name { get; set; }

        [StringLength(30)]
        public string AddressText { get; set; }

        [StringLength(200)]
        public string Summary { get; set; }

        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
