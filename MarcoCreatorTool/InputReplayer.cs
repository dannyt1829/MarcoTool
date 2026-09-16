using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MarcoCreatorTool
{
    public class InputReplayer
    {
        public static async Task ReplayActions(List<RecordedAction> actions)
        {
            var actionsCopy = new List<RecordedAction>(actions);
            var playclock = Stopwatch.StartNew();

            foreach (var action in actionsCopy)
            {
                InputSimulator.Delay(playclock, action.Time).Wait();
                switch (action.Type)
                {
                    case ActionType.KeyDown:
                        InputSimulator.KeyDown(action.Key);
                        break;
                    case ActionType.KeyUp:
                        InputSimulator.KeyUp(action.Key);
                        break;
                    case ActionType.LMouseDown:
                        InputSimulator.LMouseDown(action.X, action.Y);
                        break;
                    case ActionType.LMouseUp:
                        InputSimulator.LMouseUp(action.X, action.Y);
                        break;
                    case ActionType.RMouseDown:
                        InputSimulator.RMouseDown(action.X, action.Y);
                        break;
                    case ActionType.RMouseUp:
                        InputSimulator.RMouseUp(action.X, action.Y);
                        break;
                    case ActionType.MouseMove:
                        InputSimulator.MoveMouse(action.X, action.Y);
                        break;
                    case ActionType.MouseWheel:
                        InputSimulator.MouseWheel(action.MouseWheelDelta);
                        break;
                    case ActionType.HMouseWheel:
                        InputSimulator.MouseHWheel(action.MouseWheelDelta); 
                        break;
                }
            }
        }

    }
}
