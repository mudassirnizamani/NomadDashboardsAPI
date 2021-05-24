using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NomadDashboardsAPI.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }

        public DateTime When { get; set; }

        public string UserID { get; set; }

        public virtual User Sender { get; set; }

    }
}
