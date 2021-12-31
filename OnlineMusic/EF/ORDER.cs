namespace OnlineMusic.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ORDER")]
    public partial class ORDER
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string CreateDate { get; set; }

        public long? CustomerID { get; set; }

        [StringLength(100)]
        public string ShipName { get; set; }

        [StringLength(50)]
        public string ShipPhone { get; set; }

        [StringLength(100)]
        public string ShipEmail { get; set; }

        [StringLength(200)]
        public string ShipAddress { get; set; }

        public bool? Status { get; set; }
    }
}
