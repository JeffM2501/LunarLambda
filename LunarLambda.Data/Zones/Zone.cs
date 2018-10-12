using System;
using System.Collections.Generic;

using LudicrousElectron.Entities.Collisions;
using LudicrousElectron.Entities.Collisions.Volumes;
using LunarLambda.Data.Entitites;
using OpenTK;

namespace LunarLambda.Data.Zones
{
    public class Zone : CollisionManager
    {
        protected SortedDictionary<int, BaseEntity> Entities = new SortedDictionary<int, BaseEntity>();

        protected int LastID = 0;

		public string Name = string.Empty;
		public Vector3 Center = Vector3.Zero;

        public int Add(BaseEntity entity)
        {
            LastID++;
            entity.GUID = LastID;
            Entities.Add(entity.GUID, entity);

            CollideableElements.Add(entity);

            return entity.GUID;
        }

		public Ship Add(Ship entity)
		{
			Add(entity);

			return entity;
		}

		public void Update(double dt, double time)
        {
            bool dtIsBad = false;
            double effectiveDT = dt;
            int dtCount = 0;

            // see if the dt doesn't move anything over it's full radius, if it does, subdivide the dt until everyone is small enough.
            while (!dtIsBad && dtCount < 10)
            {
                dtIsBad = false;
                foreach (var element in Entities.Values)
                {
                    double dist = Vector3d.DistanceSquared(element.Postion, ProjectEntityPostion(effectiveDT, element));
                    if (dist > Math.Pow(element.GetBoundingSphere().Radius, 2))
                    {
                        dtIsBad = true;
                        break;
                    }
                }

                if (dtIsBad)
                {
                    dtCount++;
                    effectiveDT = effectiveDT * 0.5;
                }
            }

            double thisTime = time;
            // run each segment and check collisions as we move
            for (double dtSegment = effectiveDT; dtSegment <= dt; dtSegment += effectiveDT)
            {
                thisTime += dtSegment;

                foreach (var element in Entities.Values)
                {
                    element.Postion = ProjectEntityPostion(dtSegment, element);
                    element.Orientation += Quaternion.Multiply(element.RotaryVelocity, (float)dtSegment);
                    element.OnUpdate(dtSegment, thisTime);
                }
                this.CheckCollisions();
            }
        }

        protected Vector3d ProjectEntityPostion (double dt, BaseEntity ent)
        {
            return ent.Postion + (ent.LinearVelocity * (float)dt);
        }
    }
}
