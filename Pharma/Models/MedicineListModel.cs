using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pharma.Models
{
    public class MedicineListModel
    {
        public List<Medicine> Medicines { get; set; }
        public SelectList MedicineList { get; set; }
        public string MedicineType { get; set; }
        public string searchString { get; set; }
    }
}
