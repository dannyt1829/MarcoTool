using System;
using System.Collections.Generic;
using System.Text;

namespace MarcoCreatorTool
{
    public class RecordedAction
    {
        // Type of action (mouse click, mouse move, key press)
        public ActionType Type { get; set; }
        // Coordinates for mouse actions
        public int X { get; set; }
        public int Y { get; set; }
        // Delay in milliseconds
        public int Delay { get; set; }
        public Keys Key { get; set; }
    }

    public enum ActionType
    {
        MouseClick,
        MouseMove,
        KeyPress
    }
}
