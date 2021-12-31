namespace OnlineMusic.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ORDERDETAIL")]
    public partial class ORDERDETAIL
    {
        public long ProductID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long OrderID { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }
    }
}
