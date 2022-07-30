using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharma.Models
{
    public class Order
    {
        [Key]
        public int Order_Id { get; set; }
        public DateTime Order_Date { get; set; }
        [ForeignKey("Order_User_Id")]
        public string Order_User_Id { get; set; }
        public string Order_UserName { get; set; }

        
        public int Order_Medicine_Id { get; set; }
        [ForeignKey("Order_Medicine_Id")]
        public virtual Medicine Medicine { get; set; }


    }
}
