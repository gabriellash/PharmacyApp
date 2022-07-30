using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharma.Models
{
    public class Medicine
    {
        [Key]
        public int Medicine_Id { get; set; }
        public string Medicine_Name { get; set; }
        public string Medicine_Type     { get; set; }
        
        public string Medicine_Reg_Number { get; set; }

        public double Medicine_Size { get; set; }
        public string Medicine_Unit     { get; set; }
        public string Medicine_Form { get; set; }
        public string Medicine_Qty { get; set; }
        public double Medicine_Price { get; set; }
        public DateTime Medicine_Expiry_Date { get; set; }


    }
}
