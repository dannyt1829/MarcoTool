using System;
using System.Collections.Generic;
using System.Text;

namespace MarcoCreatorTool
{
    public class RecordedAction
    {
        public ActionType Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Delay { get; set; }
        public string Key { get; set; }
    }

    public enum ActionType
    {
        MouseClick,
        MouseMove,
        KeyPress
    }
}
