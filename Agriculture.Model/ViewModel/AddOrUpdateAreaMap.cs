using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agriculture.DomainClass.Models;

namespace Agriculture.Model.ViewModel
{
    public class AddOrUpdateAreaMap
    {
        public AddOrUpdateAreaMap()
        {
            AvilableAreaTypes=new List<MapAreaType>();
            MapPoints=new List<MapPoint>();
        }
        public int AreaId { get; set; }
        [DisplayName("نام ناحیه")]
        public string AreaName { get; set; }
        public IList<MapPoint> MapPoints { get; set; }
        [DisplayName("وضعیت محدوده")]
        public int AreaTypeId { get; set; }
        public virtual ICollection<MapAreaType> AvilableAreaTypes { get; set; }
    }
}
