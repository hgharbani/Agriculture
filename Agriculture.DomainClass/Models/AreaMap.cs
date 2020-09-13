using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Agriculture.DomainClass.Models
{
    public class AreaMap
    {
        [Key]
        public int AreaId { get; set; }
        [DisplayName("نام منطقه")]
         [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string AreaName { get; set; }
        [DisplayName("وضعیت")]
         [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int AreaTypeId { get; set; }
        public virtual MapAreaType MapAreaType { get; set; }
        public virtual ICollection<MapPoint> MapPoints { get; set; }
    }
}
