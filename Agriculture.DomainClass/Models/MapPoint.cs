using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agriculture.DomainClass.Models
{
  public  class MapPoint
    {
    
        [Key]
        public int MapPointId { get; set; }
        public int AreaMapId { get; set; }
        public string LatMap { get; set; }
        public string LngMap { get; set; }
        public virtual AreaMap AreaMap { get; set; }
    }
}
