using System;
using System.Collections.Generic;
using System.Drawing;

using LudicrousElectron.Entities.Collisions;
using LunarLambda.Data.Databases;


namespace LunarLambda.Data.Entitites
{
    public class Planet : BaseEntity
    {
        public double Mass = 1; // in earth masses 5.9722 × 10^24 kg
        public double SurfaceRadius = 6300; // in km
        public double AtomsphereRadius = 6400; // in km

        public double MinOrbitRadius = 12000; // in km

        public Color CoreColor = Color.LightGray;
        public Color AtmosphereColor = Color.LightBlue;

        public bool HasRings = false;
        public double RingInnerRadius = double.MinValue;
        public double RingOutterRadius = double.MinValue;
        public float RingTilt = 15.0f;
        public Color RingColor = Color.LavenderBlush;

        public double SpinTilt = 0;
        public double SpinSpeed = 0.0416; // in degrees per second

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
