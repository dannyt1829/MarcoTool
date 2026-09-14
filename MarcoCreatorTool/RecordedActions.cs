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
        public double Time { get; set; }
        public Keys Key { get; set; }
    }

    public enum ActionType
    {
        MouseClick,
        MouseMove,
        KeyPress,
        LMouseDown,
        LMouseUp,
        RMouseDown,
        RMouseUp,
        KeyDown,
        KeyUp,
        Placeholder // Placeholder for future action types
    }
}
