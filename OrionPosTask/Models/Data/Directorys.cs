using System.ComponentModel.DataAnnotations;

namespace OrionPosTask.Models.Data
{
    public class Directorys : IBaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(20)]
        public string Telephone { get; set; }
    }

}
