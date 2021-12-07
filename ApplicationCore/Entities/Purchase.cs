using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Purchase")]
    public class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Column(TypeName= "uniqueidentifier")]
        public Guid PurchaseNumber { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PurchaseDateTime { get; set; }
        public int MovieId { get; set; }

        //navigation property
        public User User { get; set; }
        public Movie Movie { get; set; } 
    }
}
