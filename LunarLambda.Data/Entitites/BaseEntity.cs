using System.Collections.Generic;

using LudicrousElectron.Entities.Collisions;
using LudicrousElectron.Entities.Collisions.Volumes;
using LudicrousElectron.Types.Volumes;
using LunarLambda.Data.Databases;

using OpenTK;

namespace LunarLambda.Data.Entitites
{
    public abstract class BaseEntity : ICollisionable
    {
        public int GUID = -1;

        public BaseTemplate Template = null;

        public Vector3d Postion = Vector3d.Zero;
        public Quaternion Orientation = Quaternion.Identity;
        public Vector3d LinearVelocity = Vector3d.Zero;
        public Quaternion RotaryVelocity = Quaternion.Identity;

        public CollisionSphere BoundingSphere = new CollisionSphere();

        protected List<Volume> CollisionVolumes = new List<Volume>();

        protected BaseEntity()
        {
            BoundingSphere.GetCenter = GetBoundsCenter;
            BoundingSphere.Radius = 10;

            CollisionVolumes.Add(BoundingSphere);
        }

        protected Vector3d GetBoundsCenter()
        {
            return Postion;
        }

        public CollisionSphere GetBoundingSphere()
        {
            return BoundingSphere;
        }

        public List<Volume> GetCollisionVolumes()
        {
            return CollisionVolumes;
        }

        public abstract void OnCollide(ICollisionable other);
        public abstract void OnUpdate(double dt, double now);
    }
}
