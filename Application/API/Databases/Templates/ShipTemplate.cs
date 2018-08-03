using System;
using System.Collections.Generic;
using OpenTK;

namespace LunarLambda.API.Databases
{
    public enum DockingClasses
    {
        None,
        Tiny,
        Small,
        Medium,
        Large,
        Huge,
        All,
    }

    [Flags]
    public enum EMissileWeapons
    {
        None,
        Homing,
        Nuke,
        Mine,
        EMP,
        HVLI,
        Count
    }

    public class ShipTemplate : BaseTemplate
    {
        public float ImpulseSpeed = 0;
        public float ImpulseAcceleration = 0;
        public float TurnSpeed = 0;
        public float WarpSpeed = 0;

        public class DockingPortInfo
        {
            public DockingClasses MaxSize = DockingClasses.None;
            public Vector3 Postion = Vector3.Zero;
            public Vector3 FacingVector = Vector3.UnitX;
            public bool Legacy = false;
        }

        public List<DockingPortInfo> DockingPorts = new List<DockingPortInfo>();

        public class TubeTemplate
        {
            public float LoadTIme = 0;
            public EMissileWeapons AllowedLoadings = EMissileWeapons.None;
            public float Direction = 0;
        }

        public List<TubeTemplate> WeaponTubes = new List<TubeTemplate>();

        public class BeamWeaponBankTemplate
        {
            public string BeamTexture = string.Empty;
            public string BeamType = string.Empty;

            public float 
            public float LoadTIme = 0;
            public EMissileWeapons AllowedLoadings = EMissileWeapons.None;
            public float Direction = 0;
        }

        public List<TubeTemplate> WeaponTubes = new List<TubeTemplate>();



        public ShipTemplate()
        {
            Type = TemplateTypes.Ship;
        }

        public ShipTemplate SetSpeed(float impulse, float turn, float acceleration)
        {
            ImpulseSpeed = impulse;
            TurnSpeed = turn;
            ImpulseAcceleration = acceleration;
            return this;
        }

        public ShipTemplate SetWarpSpeed(float warp)
        {
            WarpSpeed = warp;
            return this;
        }
    }
}
