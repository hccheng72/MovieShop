using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Favorite")]
    public class Favorite
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int UserID { get; set; }

        // navigation properties
        public Movie Movie { get; set; }
        public User User { get; set; }  
    }
}
