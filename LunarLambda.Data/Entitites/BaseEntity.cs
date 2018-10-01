using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LudicrousElectron.Entities.Collisions;
using LunarLambda.Data.Databases;

using OpenTK;

namespace LunarLambda.Data.Entitites
{
    public class BaseEntity : ICollisionable
    {
        public int GUID = -1;

        public BaseTemplate Template = null;

        public Vector3d Postion = Vector3d.Zero;
        public Quaternion Orientation = Quaternion.Identity;
        public Vector3 LinearVelocity = Vector3.Zero;
        public Quaternion RotaryVelocity = Quaternion.Identity;
    }
}
