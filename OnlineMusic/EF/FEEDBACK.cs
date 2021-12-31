namespace OnlineMusic.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FEEDBACK")]
    public partial class FEEDBACK
    {
        public long ID { get; set; }

        [StringLength(100)]
        public string CreateDate { get; set; }

        [Column(TypeName = "ntext")]
        public string FeedBackContent { get; set; }

        [StringLength(100)]
        public string Email { get; set; }
    }
}
