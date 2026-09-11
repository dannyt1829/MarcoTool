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
        private LowLevelMouseProc _proc;
        private const int WH_KEYBOARD_LL = 13;
        private const int WH_KEYDOWN = 0x0100;
        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;

        private IntPtr _hookID = IntPtr.Zero;
        

        [StructLayout(LayoutKind.Sequential)]
        private struct HookInfo
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public void Start()
        {
            _recordedActions.Clear();
            _proc = HookCallback;
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                _hookID = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public void Stop()
        {
            UnhookWindowsHookEx(_hookID);
        }

        public List<RecordedAction> GetRecordedActions()
        {
            return _recordedActions;
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WH_KEYDOWN)
            {
                HookInfo hookInfo = Marshal.PtrToStructure<HookInfo>(lParam);
                Keys key = (Keys)hookInfo.vkCode;
                
                _recordedActions.Add(new RecordedAction
                {
                    Type = ActionType.KeyPress,
                    Key = key,
                    Delay = 1000
                });

                MessageBox.Show($"Recorded: {key}");
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
    }
}
