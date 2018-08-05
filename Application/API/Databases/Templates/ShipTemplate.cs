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

        public class WeaponTemplate
        {
            public enum WeaponTypes
            {
                Unknown,
                Beam,
                Missile,
            }
            public virtual WeaponTypes WeaponType { get; } = WeaponTypes.Unknown;
        }

        public class TubeTemplate : WeaponTemplate
        {
            public float LoadTIme = 0;
            public EMissileWeapons AllowedLoadings = EMissileWeapons.None;
            public float Direction = 0;
        }

        public class BeamWeaponBankTemplate : WeaponTemplate
        {
            public string BeamTexture = string.Empty;
            public string BeamType = string.Empty;
            public float BeamArc = 0;
            public float NominalCycleTime = 0;
            public float NominalRange = 0;
            public float NominalDamage = 0;
            public float NominalEnergyCost = 0;
            public float NominalHeatGeneration = 0;
        }

        public class WeaponMount
        {
            public Vector3 Postion = Vector3.Zero;
            public Quaternion BaseOrientation = Quaternion.Identity;

            public bool IsTurret = false;
            public float RotationArc = 0;
            public float RotationSpeed = 0;

            public WeaponTemplate MountedWeapon = null;
        }

        public List<WeaponMount> Weapons= new List<WeaponMount>();
       

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
