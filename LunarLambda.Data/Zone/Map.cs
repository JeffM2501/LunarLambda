using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LudicrousElectron.Entities.Collisions;
using LunarLambda.Data.Entitites;

namespace LunarLambda.Data.Zone
{
    public class Map : CollisionManager
    {
        public SortedDictionary<int, BaseEntity> Entities = new SortedDictionary<int, BaseEntity>();
    }
}
