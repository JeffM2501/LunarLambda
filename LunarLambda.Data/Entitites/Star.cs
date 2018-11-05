using System;
using System.Collections.Generic;
using System.Drawing;

using LudicrousElectron.Entities.Collisions;
using LunarLambda.Data.Databases;

namespace LunarLambda.Data.Entitites
{
    public class Star : BaseEntity
    {
        public double Mass = 1; // in solar masses 2x10^30 kg
        public double CoreRadius = 700000.0; // in km
        public double CoronalRadius = 700000.0 * 3; // in km

        public Color CoreColor = Color.LightYellow;

        public override void OnCollide(ICollisionable other)
        {
            // do some damage and movement stuff now
        }

        public override void OnUpdate(double dt, double now)
        {
            // tell the AI to update or something
        }
    }
}
