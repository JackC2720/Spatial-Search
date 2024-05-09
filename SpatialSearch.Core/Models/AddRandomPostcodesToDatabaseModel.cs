using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SpatialSearch.Core.Models
{
    public class AddRandomPostcodesToDatabaseModel
    {
        public AddRandomPostcodesToDatabaseModel()
        {

        }
        [DisplayName("Number")]
        [Required]
        public int Number { get; set; }
    }
}
