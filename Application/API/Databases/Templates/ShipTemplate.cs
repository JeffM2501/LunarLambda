using System;
using System.Collections.Generic;
using OpenTK;

using LudicrousElectron.Types;

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
    public enum MissileWeaponTypes
    {
        None = 0,
        Homing = 2,
        Nuke = 4,
        Mine = 8,
        EMP = 16,
        HVLI = 32,
        Probe = 64,
        All = Homing | Nuke | Mine | EMP | HVLI | Probe,
    }

    [Flags]
    public enum FTLDriveTypes
    {
        None,
        Warp,
        Jump,
    }

    public static class ShipSystemTypes
    {
        public static readonly string Reactor = "Reactor";
        public static readonly string Beams =   "Beams";
        public static readonly string Missiles = "Missiles";
        public static readonly string Maneuver = "Maneuver";
        public static readonly string Sublight = "Sublight";
        public static readonly string FTL = "FTL";
        public static readonly string Jump = "Jump";
        public static readonly string Shield = "Shield";
        public static string Shields(int n) { return Shield + n.ToString(); }
    };

    public class ShipTemplate : BaseTemplate
    {
        public float ImpulseSpeed = 0;
        public float ImpulseAcceleration = 0;
        public float TurnSpeed = 0;

        public float CombatManuverBoostSpeed = 0;
        public float CombatManuverStrafeSpeed = 0;

        public float WarpSpeed = 0;
        public double[] JumpRanges = new double[] { 0, 0 };
        public FTLDriveTypes DriveType = FTLDriveTypes.None;

        public bool IsPlayable = false;

        public double FuelCapacity = 0;
        public int MaxRepairCrew = 0;

        public class DockingPortInfo
        {
            public DockingClasses MaxSize = DockingClasses.None;
            public Vector3 Postion = Vector3.Zero;
            public Vector3 FacingVector = Vector3.UnitX;
            public bool Legacy = false;

            public DockingPortInfo Clone()
            {
                DockingPortInfo info = new DockingPortInfo();
                info.MaxSize = MaxSize;
                info.Postion = Postion;
                info.FacingVector = FacingVector;
                info.Legacy = Legacy;
                return info;
            }
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

            public virtual WeaponTemplate Clone()
            {
                WeaponTemplate t = new WeaponTemplate();
                return t;
            }
        }

        public class TubeTemplate : WeaponTemplate
        {
            public float LoadTime = 0;
            public MissileWeaponTypes AllowedLoadings = MissileWeaponTypes.None;

            public override WeaponTemplate Clone ()
            {
                TubeTemplate t = new TubeTemplate();
                t.AllowedLoadings = AllowedLoadings;
                t.LoadTime = LoadTime;
                return t;
            }
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

            public override WeaponTemplate Clone()
            {
                BeamWeaponBankTemplate b = new BeamWeaponBankTemplate();
                b.BeamTexture = BeamTexture;
                b.BeamType = BeamType;
                b.BeamArc = BeamArc;
                b.NominalCycleTime = NominalCycleTime;
                b.NominalRange = NominalRange;
                b.NominalDamage = NominalDamage;
                b.NominalEnergyCost = NominalEnergyCost;
                b.NominalHeatGeneration = NominalHeatGeneration;

                return b;
            }
        }

        public enum HardpointTypes
        {
            Beam,
            Tube,
            Other,
        }

        public class HardpointID : IEquatable<HardpointID>
        {
            public static readonly HardpointID Empty = new HardpointID();

            protected Tuple<HardpointTypes, int> Data = new Tuple<HardpointTypes, int>(HardpointTypes.Other, -1);

            public HardpointTypes Type { get { return Data.Item1; } set { Data = new Tuple<HardpointTypes, int>(value,Data.Item2); } }
            public int Index { get { return Data.Item2; } set { Data = new Tuple<HardpointTypes, int>(Data.Item1, value); } }

            public HardpointID() { }
            public HardpointID (HardpointTypes type, int index)
            {
                Data = new Tuple<HardpointTypes, int>(type, index);
            }

            public HardpointID(HardpointID id)
            {
                Data = new Tuple<HardpointTypes, int>(id.Type, id.Index);
            }

            public override int GetHashCode()
            {
                return Data.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                return obj as HardpointID != null && (obj as HardpointID).Data == Data;
            }

            public override string ToString()
            {
                return Data.ToString();
            }

            public bool Equals(HardpointID other)
            {
                return other.Data.Equals(Data);
            }
        }


        public class WeaponMount
        {
            public float DefaultFacing = 0;

            public bool IsTurret = false;
            public float RotationArc = 0;
            public float RotationSpeed = 0;

            public WeaponTemplate MountedWeapon = null;

            public HardpointID Hardpoint = HardpointID.Empty;

            public WeaponMount Clone()
            {
                WeaponMount m = new WeaponMount();
                m.DefaultFacing = DefaultFacing;
                m.IsTurret = IsTurret;
                m.RotationArc = RotationArc;
                m.RotationSpeed = RotationSpeed;
                m.MountedWeapon = MountedWeapon?.Clone();
                m.Hardpoint = new HardpointID(Hardpoint);
                return m;
            }
        }

        public Dictionary<HardpointID, WeaponMount> Weapons= new Dictionary<HardpointID, WeaponMount>();


        public class MissileMagazine
        {
            public MissileWeaponTypes AllowedTypes = new MissileWeaponTypes();
            public int Capacity = 0;
            public List<MissileWeaponTypes> Loadout = new List<MissileWeaponTypes>();
            public float LoadModificationFactor = 1.0f;
            public bool Indexable = false;
        }

        public List<MissileMagazine> Magazines = new List<MissileMagazine>();

        public class Room
        {
            public Rect2Di Rectangle = new Rect2Di();
            public List<string> Systems = new List<string>();


            public void AddSystem(string system)
            {
                if (Systems.Contains(system))
                    return;

                Systems.Add(system);
            }

            public Room Clone()
            {
                Room r = new Room();
                r.Rectangle = new Rect2Di(r.Rectangle);
                r.Systems.AddRange(Systems.ToArray());

                return r;
            }
        }

        public class Door
        {
            public Vector2i Postion = Vector2i.Zero;
            public bool Horizontal = false;
        }

        public List<Room> Rooms = new List<Room>();
        public List<Door> Doors = new List<Door>();


        public ShipTemplate()
        {
            Type = TemplateTypes.Ship;
        }

        public ShipTemplate CloneShip(string name)
        {
            return base.Clone(name) as ShipTemplate;
        }

        protected override void CopyTo(BaseTemplate newTemplate)
        {
            base.CopyTo(newTemplate);
            ShipTemplate newShip = newTemplate as ShipTemplate;
            if (newShip == null)
                return;

            newShip.ImpulseSpeed = ImpulseSpeed;
            newShip.ImpulseAcceleration = ImpulseAcceleration;
            newShip.TurnSpeed = TurnSpeed;
            newShip.WarpSpeed = WarpSpeed;
            newShip.IsPlayable = IsPlayable;
            newShip.FuelCapacity = FuelCapacity;
            newShip.MaxRepairCrew = MaxRepairCrew;

            foreach (var i in DockingPorts)
                newShip.DockingPorts.Add(i.Clone());

            foreach (var w in Weapons)
                newShip.Weapons.Add(new HardpointID(w.Key), w.Value.Clone());
        }

        protected override BaseTemplate Create()
        {
            return new ShipTemplate();
        }

        public void SetReairCrew(int count)
        {
            MaxRepairCrew = count;
        }

        public void SetFuelCapcity(float cap)
        {
            FuelCapacity = cap;
        }

        public void SetPlayable()
        {
            IsPlayable = true;
        }

        public void SetSpeed(float impulse, float turn, float acceleration)
        {
            ImpulseSpeed = impulse;
            TurnSpeed = turn;
            ImpulseAcceleration = acceleration;
        }

        public void SetWarpSpeed(float warp)
        {
            WarpSpeed = warp;
        }

        protected WeaponTemplate.WeaponTypes GetStandardWeaponTypeForMount(HardpointTypes type)
        {
            if (type == HardpointTypes.Beam)
                return WeaponTemplate.WeaponTypes.Beam;
            else if (type == HardpointTypes.Tube)
                return WeaponTemplate.WeaponTypes.Missile;

            return WeaponTemplate.WeaponTypes.Unknown;
        }

        protected T GetWeaponMount<T>(int index, HardpointTypes type) where T : WeaponTemplate, new()
        {
            WeaponMount mount = FindWeaponMount(type, index);
            if (mount.MountedWeapon == null || mount.MountedWeapon.WeaponType != GetStandardWeaponTypeForMount(type))
                mount.MountedWeapon = new T();

            return mount.MountedWeapon as T;
        }
        public WeaponMount FindWeaponMount(HardpointTypes type, int index)
        {
            HardpointID id = new HardpointID(type, index);
            if (Weapons.ContainsKey(id))
                return Weapons[id];

            WeaponMount mount = new WeaponMount();
            mount.Hardpoint = id;
            Weapons.Add(id,mount);
            return mount;
        }

        public void SetupBeamWeapon( int hardpointIndex, float arc, float nomialDir, float range, float cycleTime, float damage)
        {
            WeaponMount mount = FindWeaponMount(HardpointTypes.Beam, hardpointIndex);
            var beam = GetWeaponMount<BeamWeaponBankTemplate>(hardpointIndex, HardpointTypes.Beam);

            beam.BeamArc = arc;
            mount.DefaultFacing = nomialDir;
            beam.NominalRange = range;
            beam.NominalCycleTime = cycleTime;
            beam.NominalDamage = damage;
        }

        public void RemoveBeamWeapon(int hardpointIndex)
        {
            if (Weapons.ContainsKey(new HardpointID(HardpointTypes.Beam, hardpointIndex)))
                Weapons.Remove(new HardpointID(HardpointTypes.Beam, hardpointIndex));
        }

        public void SetupMissileWeapon(int hardpointIndex, float loadTime, MissileWeaponTypes loadTypes)
        {
            var tube = GetWeaponMount<TubeTemplate>(hardpointIndex, HardpointTypes.Tube);

            tube.AllowedLoadings = loadTypes;
            tube.LoadTime = loadTime;
        }

        public void RemoveMissleWeapon(int hardpointIndex)
        {
            if (Weapons.ContainsKey(new HardpointID(HardpointTypes.Tube, hardpointIndex)))
                Weapons.Remove(new HardpointID(HardpointTypes.Tube, hardpointIndex));
        }

        public void SetMissleWeaponDirection(int hardpointIndex, float direction)
        {
            WeaponMount mount = FindWeaponMount(HardpointTypes.Tube, hardpointIndex);
            mount.DefaultFacing = direction;
        }

        public MissileWeaponTypes GetTubeLoadTypes(int hardpointIndex)
        {
            HardpointID id = new HardpointID(HardpointTypes.Tube, hardpointIndex);
            if (!Weapons.ContainsKey(id) || Weapons[id].MountedWeapon as TubeTemplate == null)
                return MissileWeaponTypes.None;

            return (Weapons[id].MountedWeapon as TubeTemplate).AllowedLoadings;
        }

        public void AddTubeLoadTypes(int hardpointIndex, MissileWeaponTypes types)
        {
            HardpointID id = new HardpointID(HardpointTypes.Tube, hardpointIndex);
            if (!Weapons.ContainsKey(id) || Weapons[id].MountedWeapon as TubeTemplate == null)
                return;

            (Weapons[id].MountedWeapon as TubeTemplate).AllowedLoadings |= types;
        }

        public void RemoveTubeLoadTypes(int hardpointIndex, MissileWeaponTypes types)
        {
            HardpointID id = new HardpointID(HardpointTypes.Tube, hardpointIndex);
            if (!Weapons.ContainsKey(id) || Weapons[id].MountedWeapon as TubeTemplate == null)
                return;

            (Weapons[id].MountedWeapon as TubeTemplate).AllowedLoadings &= ~types;
        }

        public void SetMissleWeapoLoadTime(int hardpointIndex, float time)
        {
            WeaponMount mount = FindWeaponMount(HardpointTypes.Tube, hardpointIndex);
            if (mount.MountedWeapon == null || mount.MountedWeapon.WeaponType != WeaponTemplate.WeaponTypes.Missile)
                mount.MountedWeapon = new TubeTemplate();

            TubeTemplate tube = mount.MountedWeapon as TubeTemplate;

            tube.LoadTime = time;

        }

        public void SetMissileWeaponLoadingTypes(int hardpointIndex, MissileWeaponTypes loadTypes)
        {
            WeaponMount mount = FindWeaponMount(HardpointTypes.Tube, hardpointIndex);
            if (mount.MountedWeapon == null || mount.MountedWeapon.WeaponType != WeaponTemplate.WeaponTypes.Missile)
                mount.MountedWeapon = new TubeTemplate();

            TubeTemplate tube = mount.MountedWeapon as TubeTemplate;

            tube.AllowedLoadings = loadTypes;
        }

        public void SetMissleTubeCount(int count, float nomialDir)
        {
			List<HardpointID> toKill = new List<HardpointID>();
			foreach (var item in Weapons.Keys)  // flush any existing tubes
			{
				if (item.Type != HardpointTypes.Tube)
					continue;
				toKill.Add(item);
			}

			foreach (var item in toKill)
				Weapons.Remove(item);
 
            for (int i = 0; i < count; i++)
                SetupMissileWeapon(i, nomialDir, MissileWeaponTypes.All);
        }

        public Room AddRoom(int posX, int posY, int width, int height, string initalSystem = null)
        {
            Room r = new Room();
            r.Rectangle = new Rect2Di(posX, posY, width, height);
            if (!string.IsNullOrEmpty(initalSystem))
                r.Systems.Add(initalSystem);

            Rooms.Add(r);
            return r;
        }

        public Door AddDoor(int posX, int posY, bool horizontal)
        {
            Door r = new Door();
            r.Postion = new Vector2i(posX, posY);
            r.Horizontal = horizontal;

            Doors.Add(r);
            return r;
        }

        public void ClearMagazines()
        {
            Magazines.Clear();
        }

        protected MissileWeaponTypes GetSingleMissileType(MissileWeaponTypes types)
        {
            if (types.HasFlag(MissileWeaponTypes.Homing))
                return MissileWeaponTypes.Homing;

            if (types.HasFlag(MissileWeaponTypes.Nuke))
                return MissileWeaponTypes.Nuke;
            if (types.HasFlag(MissileWeaponTypes.Mine))
                return MissileWeaponTypes.Mine;
            if (types.HasFlag(MissileWeaponTypes.EMP))
                return MissileWeaponTypes.EMP;
            if (types.HasFlag(MissileWeaponTypes.HVLI))
                return MissileWeaponTypes.HVLI;
            if (types.HasFlag(MissileWeaponTypes.Probe))
                return MissileWeaponTypes.Probe;

            return MissileWeaponTypes.None;
        }

        public void SetupMissileMagazine(MissileWeaponTypes type, int capacity, MissileWeaponTypes fillWith)
        {
            if (type == MissileWeaponTypes.None)
                return;

            MissileMagazine magazine = new MissileMagazine();
            magazine.Capacity = capacity;
            magazine.AllowedTypes = type;

            var fill = GetSingleMissileType(fillWith);

            if (fill != MissileWeaponTypes.None && type.HasFlag(fill))
            {
                for (int i = 0; i < capacity; i++)
                    magazine.Loadout.Add(fill);
            }

            Magazines.Add(magazine);
        }

        public void SetupMissileMagazine(MissileWeaponTypes type, int capacity)
        {
            SetupMissileMagazine(type, capacity, GetSingleMissileType(type));
        }

        public void SetJumpDrive()
        {
            DriveType = FTLDriveTypes.Jump;
        }

        public void SetWarpDrive()
        {
            DriveType = FTLDriveTypes.Warp;
        }

        public void SetJumpRanges(double min, double max)
        {
            JumpRanges[0] = min;
            JumpRanges[1] = max;
        }

        public void SetCombatManeuvers(float boost, float strafe )
        {
            CombatManuverBoostSpeed = boost;
            CombatManuverStrafeSpeed = strafe;
        }
    }
}
