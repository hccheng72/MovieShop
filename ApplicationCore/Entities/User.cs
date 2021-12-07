using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }

        [MaxLength(128)]
        public string? FirstName { get; set; }

        [MaxLength(128)]
        public string? LastName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(256)]
        public string? Email { get; set; }

        [MaxLength(1024)]
        public string? HashedPassword { get; set; }

        [MaxLength(1024)]
        public string? Salt { get; set; }

        [MaxLength(16)]
        public string? PhoneNumber { get; set; }

        public bool? TwoFactorEnabled { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LockOutEndDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastLoggingDateTime { get; set; }

        public bool? IsLocked { get; set; }
        public int? AccessFailedCount { get; set; }

        //navigation properties
        List<Favorite> Favorites { get; set; }
        List<Purchase> Purchases { get; set; }
    }
}
