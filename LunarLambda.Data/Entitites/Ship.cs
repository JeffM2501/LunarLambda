using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LunarLambda.Data.Databases;

namespace LunarLambda.Data.Entitites
{
    public class Ship : BaseEntity
    {
        public virtual ShipTemplate ShipInfo { get { return Template as ShipTemplate; } }

        public Ship(ShipTemplate template) : base()
        {
            Template = template;
        }
    }
}
