using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NomadDashboardsAPI.Models
{

    
    public class ClientQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Question_1_Answer { get; set; }

        [Required]
        public string Question_2_Answer { get; set; }

        [Required]
        public string Question_3_Answer { get; set; }

        [Required]
        public string Question_4_Answer { get; set; }

        [Required]
        public string Question_5_Answer { get; set; }

        [Required]
        public string Question_6_Answer { get; set; }

        [Required]
        public string Question_7_Answer { get; set; }

        [Required]
        public string Question_8_Answer { get; set; }

        [Required]
        public string Question_9_Answer { get; set; }


    }
}