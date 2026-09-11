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
        private const int WH_KEYBOARD_LL = 13;
        private const int WH_KEYDOWN = 0x0100;
        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;

        private IntPtr _keyboardHookID = IntPtr.Zero;
        private IntPtr _mouseHookID = IntPtr.Zero;

        // Stores mouse hook data
        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
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
            public int mouseData;
            public int flags;
            public int time;
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
            if (nCode >= 0 && wParam == (IntPtr)WH_KEYDOWN)
            {
                KBDLLHOOKSTRUCT hookInfo = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);
                Keys key = (Keys)hookInfo.vkCode;
                
                _recordedActions.Add(new RecordedAction
                {
                    Type = ActionType.KeyPress,
                    Key = key,
                    Delay = 1000
                });

                MessageBox.Show($"Recorded: {key}");
            }
            return CallNextHookEx(_mouseHookID, nCode, wParam, lParam);
        }

        private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        { 
            if (nCode >= 0 && wParam == (IntPtr)WM_LBUTTONDOWN)
            {
                MSLLHOOKSTRUCT hookInfo = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);
                
                _recordedActions.Add(new RecordedAction
                {
                    Type = ActionType.MouseClick,
                    X = hookInfo.pt.x,
                    Y = hookInfo.pt.y,
                    Delay = 1000
                });
            }

            return CallNextHookEx(_mouseHookID, nCode, wParam, lParam);
        }
    }
}
