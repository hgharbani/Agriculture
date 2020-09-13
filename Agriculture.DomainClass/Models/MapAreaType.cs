using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agriculture.DomainClass.Models
{
    public class MapAreaType
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("وضعیت")]
        public string TypeName { get; set; }
        [DisplayName("رنگ وضعیت")]
        public string Color { get; set; }
        public virtual ICollection<AreaMap> AreaMaps { get; set; }
    }
}
