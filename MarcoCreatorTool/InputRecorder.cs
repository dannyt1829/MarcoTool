using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace MarcoCreatorTool
{
    public class InputRecorder
    {
        private List<RecordedAction> _recordedActions = new List<RecordedAction>();
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private LowLevelMouseProc _mouseProc;
        private LowLevelKeyboardProc _keyboardProc;
        private Stopwatch _recordClock;
        private const int WH_KEYBOARD_LL = 13;
        private const int WH_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_LBUTTONUP = 0x0202;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_RBUTTONUP = 0x0205;
        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_MOUSEHWHEEL = 0x020E;

        private IntPtr _keyboardHookID = IntPtr.Zero;
        private IntPtr _mouseHookID = IntPtr.Zero;

        private uint _lastActionTime = 0;
        private ActionType _actionType;

        // Stores mouse hook data
        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        // Stores mouse hook data 
        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public void Start()
        {
            _recordedActions.Clear();
            _keyboardProc = KeyboardHookCallback;
            _mouseProc = MouseHookCallback;
            _recordClock = Stopwatch.StartNew();

            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                IntPtr moduleHandle = GetModuleHandle(curModule.ModuleName);
                _keyboardHookID = SetWindowsHookEx(WH_KEYBOARD_LL, _keyboardProc, moduleHandle, 0);
                _mouseHookID = SetWindowsHookEx(WH_MOUSE_LL, _mouseProc, moduleHandle, 0);
            }
        }

        public void Stop()
        {
            UnhookWindowsHookEx(_keyboardHookID);
            UnhookWindowsHookEx(_mouseHookID);
        }

        public List<RecordedAction> GetRecordedActions()
        {
            return _recordedActions;
        }

        private IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT hookInfo = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);
                Keys key = (Keys)hookInfo.vkCode;

                _lastActionTime = hookInfo.time;
                _actionType = (wParam == (IntPtr)WH_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN) ? ActionType.KeyDown : ActionType.KeyUp;

                _recordedActions.Add(new RecordedAction
                {
                    Type = _actionType,
                    Key = key,
                    Time = _recordClock.Elapsed.TotalMilliseconds,
                });
            }
            return CallNextHookEx(_keyboardHookID, nCode, wParam, lParam);
        }

        private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                //TODO: Add mouse move, mouse wheel, and extra mouse events
                MSLLHOOKSTRUCT hookInfo = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);

                _lastActionTime = hookInfo.time;
                _actionType = (wParam == (IntPtr)WM_LBUTTONDOWN) ? ActionType.LMouseDown
                            : (wParam == (IntPtr)WM_LBUTTONUP) ? ActionType.LMouseUp
                            : (wParam == (IntPtr)WM_RBUTTONDOWN) ? ActionType.RMouseDown
                            : (wParam == (IntPtr)WM_RBUTTONUP) ? ActionType.RMouseUp
                            : (wParam == (IntPtr)WM_MOUSEMOVE) ? ActionType.MouseMove
                            : (wParam == (IntPtr)WM_MOUSEWHEEL) ? ActionType.MouseWheel
                            : (wParam == (IntPtr)WM_MOUSEHWHEEL) ? ActionType.HMouseWheel
                            : ActionType.Placeholder;

                if (_actionType == ActionType.MouseWheel || _actionType == ActionType.HMouseWheel)
                {
                    System.Diagnostics.Debug.WriteLine($"Mouse wheel event: Delta={(short)((hookInfo.mouseData >> 16) & 0xffff)}");
                }

                _recordedActions.Add(new RecordedAction
                {
                    Type = _actionType,
                    X = hookInfo.pt.x,
                    Y = hookInfo.pt.y,
                    Time = _recordClock.Elapsed.TotalMilliseconds,
                    MouseWheelDelta = (wParam == (IntPtr)WM_MOUSEWHEEL || wParam == (IntPtr)WM_MOUSEHWHEEL) ? (short)((hookInfo.mouseData >> 16) & 0xffff) : 0
                });
            }

            return CallNextHookEx(_mouseHookID, nCode, wParam, lParam);
        }
    }
}
