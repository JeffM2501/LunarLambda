using System;
using System.Collections.Generic;

using LudicrousElectron.Engine.Input;
using LunarLambda.Preferences;

namespace LunarLambda.GUI.Config
{
    public class HotkeyConfigItem
    {
        public string Key = string.Empty;
        public Tuple<string, string> Value = null;

        public KeyEvent Hotkey = new KeyEvent();

        public HotkeyConfigItem(string key, Tuple<string, string> value)
        {
            Key = key;
            Value = value;
        }

        public void Load(string key_config)
        {
            foreach (string config in key_config.Split(";".ToCharArray()))
            {
                if (config == "[alt]")
                    Hotkey.Alt = true;
                else if (config == "[control]")
                    Hotkey.Control = true;
                else if (config == "[shift]")
                    Hotkey.Shift = true;
                else if (config == "[system]")
                    Hotkey.Meta = true;
                else
                {
                    foreach (var key_name in KeyMap.Names)
                    {
                        if (key_name.Key == config)
                        {
                            Hotkey.Code = key_name.Value;
                            break;
                        }
                    }
                }
            }
        }
    }

    public class HotkeyConfigCategory
    {
        public string Key = string.Empty;
        public string Name = string.Empty;
        public List<HotkeyConfigItem> HotKeys = new List<HotkeyConfigItem>();

        public HotkeyConfigCategory() { }
        public HotkeyConfigCategory(string key, string name)
        {
            Key = key;
            Name = name;
        }
    }

    public class HotkeyResult
    {
        public HotkeyResult(string _category, string _hotkey)
        {
            Category = _category;
            Hotkey = _hotkey;

        }

        public string Category = string.Empty;
        public string Hotkey = string.Empty;
    }

    public class HotkeyConfig
    {
        protected internal HotkeyConfigCategory CurrentCategory = null;
        private readonly List<HotkeyConfigCategory> Categories = new List<HotkeyConfigCategory>();

        public HotkeyConfig()
        {
            LoadDefaults();
        }

        public void Load()
        {
            foreach (var cat in Categories)
            {
                foreach (var item in cat.HotKeys)
                {
                    string key_config = PreferencesManager.Get("HOTKEY." + cat.Key + "." + item.Key, item.Value.Item2);
                    item.Load(key_config);
                }
            }
        }

        public List<string> GetCategories()
        {
            List<string> cats = new List<string>();
            foreach (var cat in Categories)
                cats.Add(cat.Name);

            return cats;
        }

        public List<Tuple<string, string>> ListHotkeysByCategory(string hotkeyCategory)
        {
            List<Tuple<string, string>> ret = new List<Tuple<string, string>>();

            foreach (var cat in Categories)
            {
                if (cat.Name == hotkeyCategory)
                {
                    foreach (HotkeyConfigItem item in cat.HotKeys)
                    {
                        foreach (var key_name in KeyMap.Names)
                        {
                            if (key_name.Value == item.Hotkey.Code)
                                ret.Add(new Tuple<string, string>(item.Value.Item1, key_name.Key));
                        }
                    }
                }
            }

            return ret;
        }

        public List<HotkeyResult> GetHotkey(KeyEvent key)
        {
            List<HotkeyResult> results = new List<HotkeyResult>();
            foreach (var cat in Categories)
            {
                foreach (var item in cat.HotKeys)
                {
                    if (item.Hotkey == key)
                    {
                        results.Add(new HotkeyResult(cat.Key, item.Key));
                    }
                }
            }
            return results;
        }

        private void NewCategory(string key, string name)
        {
            CurrentCategory = new HotkeyConfigCategory(key, name);
            Categories.Add(CurrentCategory);
        }

        private void NewKey(string key, Tuple<string, string> value)
        {
            if (CurrentCategory == null)
                NewCategory("GENERAL", "GENERAL");

            CurrentCategory.HotKeys.Add(new HotkeyConfigItem(key, value));
        }

        private void LoadDefaults()
        {
            NewCategory("GENERAL", "General");
            NewKey("NEXT_STATION", new Tuple<string, string>("Switch to next crew station", "Tab"));
            NewKey("PREV_STATION", new Tuple<string, string>("Switch to previous crew station", ""));
            NewKey("STATION_HELMS", new Tuple<string, string>("Switch to helms station", "F2"));
            NewKey("STATION_WEAPONS", new Tuple<string, string>("Switch to weapons station", "F3"));
            NewKey("STATION_ENGINEERING", new Tuple<string, string>("Switch to engineering station", "F4"));
            NewKey("STATION_SCIENCE", new Tuple<string, string>("Switch to science station", "F5"));
            NewKey("STATION_RELAY", new Tuple<string, string>("Switch to relay station", "F6"));

            NewCategory("HELMS", "Helms");
            NewKey("INC_IMPULSE", new Tuple<string, string>("Increase impulse", "Up"));
            NewKey("DEC_IMPULSE", new Tuple<string, string>("Decrease impulse", "Down"));
            NewKey("ZERO_IMPULSE", new Tuple<string, string>("Zero impulse", "Space"));
            NewKey("MAX_IMPULSE", new Tuple<string, string>("Max impulse", ""));
            NewKey("MIN_IMPULSE", new Tuple<string, string>("Max reverse impulse", ""));
            NewKey("TURN_LEFT", new Tuple<string, string>("Turn left", "Left"));
            NewKey("TURN_RIGHT", new Tuple<string, string>("Turn right", "Right"));
            NewKey("WARP_0", new Tuple<string, string>("Warp off", ""));
            NewKey("WARP_1", new Tuple<string, string>("Warp 1", ""));
            NewKey("WARP_2", new Tuple<string, string>("Warp 2", ""));
            NewKey("WARP_3", new Tuple<string, string>("Warp 3", ""));
            NewKey("WARP_4", new Tuple<string, string>("Warp 4", ""));
            NewKey("DOCK_ACTION", new Tuple<string, string>("Dock request/abort/undock", "D"));
            NewKey("DOCK_REQUEST", new Tuple<string, string>("Initiate docking", ""));
            NewKey("DOCK_ABORT", new Tuple<string, string>("Abort docking", ""));
            NewKey("UNDOCK", new Tuple<string, string>("Undock", "D"));
            NewKey("INC_JUMP", new Tuple<string, string>("Increase jump distance", ""));
            NewKey("DEC_JUMP", new Tuple<string, string>("Decrease jump distance", ""));
            NewKey("JUMP", new Tuple<string, string>("Initiate jump", ""));
            NewKey("COMBAT_LEFT", new Tuple<string, string>("Combat maneuver left",string.Empty));
            NewKey("COMBAT_RIGHT", new Tuple<string, string>("Combat maneuver right", string.Empty));
            NewKey("COMBAT_BOOST", new Tuple<string, string>("Combat maneuver boost", string.Empty));

            NewCategory("WEAPONS", "Weapons");
            NewKey("SELECT_MISSILE_TYPE_HOMING", new Tuple<string, string>("Select homing", "Num1"));
            NewKey("SELECT_MISSILE_TYPE_NUKE", new Tuple<string, string>("Select nuke", "Num2"));
            NewKey("SELECT_MISSILE_TYPE_MINE", new Tuple<string, string>("Select mine", "Num3"));
            NewKey("SELECT_MISSILE_TYPE_EMP", new Tuple<string, string>("Select EMP", "Num4"));
            NewKey("SELECT_MISSILE_TYPE_HVLI", new Tuple<string, string>("Select HVLI", "Num5"));
            for (int n = 0; n < ShipConstants.MaxWeaponTubes; n++)
                NewKey("LOAD_TUBE_" + (n + 1).ToString(), new Tuple<string, string>("Load tube " + (n + 1).ToString(), ""));

            for (int n = 0; n < ShipConstants.MaxWeaponTubes; n++)
                NewKey("UNLOAD_TUBE_" + (n + 1).ToString(), new Tuple<string, string>("Unload tube " + (n + 1).ToString(), ""));

            for (int n = 0; n < ShipConstants.MaxWeaponTubes; n++)
                NewKey("FIRE_TUBE_" + (n + 1).ToString(), new Tuple<string, string>("Fire tube " + (n + 1).ToString(), ""));

            NewKey("NEXT_ENEMY_TARGET", new Tuple<string, string>("Select next target", ""));
            NewKey("NEXT_TARGET", new Tuple<string, string>("Select next target (any)", ""));
            NewKey("TOGGLE_SHIELDS", new Tuple<string, string>("Toggle shields", "S"));
            NewKey("ENABLE_SHIELDS", new Tuple<string, string>("Enable shields", ""));
            NewKey("DISABLE_SHIELDS", new Tuple<string, string>("Disable shields", ""));
            NewKey("BEAM_SUBSYSTEM_TARGET_NEXT", new Tuple<string, string>("Next beam subsystem target type", ""));
            NewKey("BEAM_SUBSYSTEM_TARGET_PREV", new Tuple<string, string>("Previous beam subsystem target type", ""));
            NewKey("BEAM_FREQUENCY_INCREASE", new Tuple<string, string>("Increase beam frequency", ""));
            NewKey("BEAM_FREQUENCY_DECREASE", new Tuple<string, string>("Decrease beam frequency", ""));
            NewKey("TOGGLE_AIM_LOCK", new Tuple<string, string>("Toggle missile aim lock", ""));
            NewKey("ENABLE_AIM_LOCK", new Tuple<string, string>("Enable missile aim lock", ""));
            NewKey("DISABLE_AIM_LOCK", new Tuple<string, string>("Disable missile aim lock", ""));
            NewKey("AIM_MISSILE_LEFT", new Tuple<string, string>("Turn missile aim to the left", ""));
            NewKey("AIM_MISSILE_RIGHT", new Tuple<string, string>("Turn missile aim to the right", ""));

            NewCategory("ENGINEERING", "Engineering");
            NewKey("SELECT_REACTOR", new Tuple<string, string>("Select reactor system", "Num1"));
            NewKey("SELECT_BEAM_WEAPONS", new Tuple<string, string>("Select beam weapon system", "Num2"));
            NewKey("SELECT_MISSILE_SYSTEM", new Tuple<string, string>("Select missile weapon system", "Num3"));
            NewKey("SELECT_MANEUVER", new Tuple<string, string>("Select maneuvering system", "Num4"));
            NewKey("SELECT_IMPULSE", new Tuple<string, string>("Select impulse system", "Num5"));
            NewKey("SELECT_WARP", new Tuple<string, string>("Select warp system", "Num6"));
            NewKey("SELECT_JUMP_DRIVE", new Tuple<string, string>("Select jump drive system", "Num7"));
            NewKey("SELECT_FRONT_SHIELDS", new Tuple<string, string>("Select front shields system", "Num8"));
            NewKey("SELECT_REAR_SHIELDS", new Tuple<string, string>("Select rear shields system", "Num9"));
            NewKey("INCREASE_POWER", new Tuple<string, string>("Increase system power", "Up"));
            NewKey("DECREASE_POWER", new Tuple<string, string>("Decrease system power", "Down"));
            NewKey("INCREASE_COOLANT", new Tuple<string, string>("Increase system coolant", "Right"));
            NewKey("DECREASE_COOLANT", new Tuple<string, string>("Decrease system coolant", "Left"));
            NewKey("NEXT_REPAIR_CREW", new Tuple<string, string>("Next repair crew", ""));
            NewKey("REPAIR_CREW_MOVE_UP", new Tuple<string, string>("Crew move up", ""));
            NewKey("REPAIR_CREW_MOVE_DOWN", new Tuple<string, string>("Crew move down", ""));
            NewKey("REPAIR_CREW_MOVE_LEFT", new Tuple<string, string>("Crew move left", ""));
            NewKey("REPAIR_CREW_MOVE_RIGHT", new Tuple<string, string>("Crew move right", ""));
            NewKey("SHIELD_CAL_INC", new Tuple<string, string>("Increase shield frequency target", ""));
            NewKey("SHIELD_CAL_DEC", new Tuple<string, string>("Decrease shield frequency target", ""));
            NewKey("SHIELD_CAL_START", new Tuple<string, string>("Start shield calibration", ""));
            NewKey("SELF_DESTRUCT_START", new Tuple<string, string>("Start self-destruct", ""));
            NewKey("SELF_DESTRUCT_CONFIRM", new Tuple<string, string>("Confirm self-destruct", ""));
            NewKey("SELF_DESTRUCT_CANCEL", new Tuple<string, string>("Cancel self-destruct", ""));
        }
    }


    public static class HotKeys
    {
        public static HotkeyConfig Config = new HotkeyConfig();
    }
}
