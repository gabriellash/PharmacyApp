using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharma.Models
{
    public class Request
        
    {
        [Key]
        public int Request_Id { get; set; }
        public DateTime Request_Date { get; set; }
        [ForeignKey("Request_User_Id")]
        public string Request_User_Id { get; set; }
     
        public int Medicine_Id { get; set; }
        [ForeignKey("Medicine_Id")]
        public virtual Medicine Medicine { get; set; }
    }
}
