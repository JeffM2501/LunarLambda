using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Input;

using LudicrousElectron.Engine.Input;

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
    }

    public static class HotKeys
    {
    }
}
