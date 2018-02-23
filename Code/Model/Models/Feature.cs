using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Feature : BaseModel
    {
        public int FeatureID { get; set; }
        public string FeatureName { get; set; }
        public string FeatureNameAr { get; set; }
        public string MenuIcon { get; set; }
        public bool IsVisible { get; set; }
        public int FeatureOrder { get; set; }

        public List<Right> Rights { get; set; }


        public Feature()
        {
            Rights = new List<Right>();
        }
    }
}
