using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace MarcoCreatorTool
{
    public static class InputSimulator
    {
        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const uint MOUSEEVENTF_RIGHTUP = 0x10;
        private const uint KEYEVENTF_KEYUP = 0x0002;

        [DllImport("user32.dll")]
        private static extern bool mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        [DllImport("user32.dll")]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);
        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            public uint type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion
        {
            [FieldOffset(0)]
            public MOUSEINPUT mi;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        public static void MoveMouse(int x, int y)
        {
            // Move mouse to specified coordinates
            int virtualWidth = GetSystemMetrics(78);
            int virtualHeight = GetSystemMetrics(79);

            int normalizedX = (int)(x * 65535.0 / virtualWidth);
            int normalizedY = (int)(y * 65535.0 / virtualHeight);

            INPUT[] inputs = new INPUT[1];
            inputs[0] = new INPUT
            {
                type = 0, // Input type: mouse
                u = new InputUnion
                {
                    mi = new MOUSEINPUT
                    {
                        dx = normalizedX,
                        dy = normalizedY,
                        dwFlags = 0x0001 | 0x8000, // MOUSEEVENTF_MOVE
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };

            SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void Click(int x, int y)
        {
            // Mouse click at specified coordinates
            Cursor.Position = new Point(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static void LMouseDown(int x, int y)
        {
            // Mouse down at specified coordinates
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        }

        public static void LMouseUp(int x, int y)
        {
            // Mouse up at specified coordinates
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static void RMouseDown(int x, int y)
        {
            // Mouse down at specified coordinates
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
        }

        public static void RMouseUp(int x, int y)
        {
            // Mouse up at specified coordinates
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        public static void KeyPress(Keys key)
        {
            // Simulate key press
            byte vk = (byte)key;
            keybd_event(vk, 0, 0, 0);
            keybd_event(vk, 0, KEYEVENTF_KEYUP, 0);
        }

        public static void KeyDown(Keys key)
        {
            // Simulate key down
            byte vk = (byte)key;
            keybd_event(vk, 0, 0, 0);
        }

        public static void KeyUp(Keys key)
        {
            // Simulate key up
            byte vk = (byte)key;
            keybd_event(vk, 0, KEYEVENTF_KEYUP, 0);
        }

        public static async Task Delay(Stopwatch clock, double targetMs)
        {
            double remainingTime = targetMs - clock.Elapsed.TotalMilliseconds;
            while (remainingTime > 2)
            {
                Thread.Sleep(1);
                remainingTime = targetMs - clock.Elapsed.TotalMilliseconds;
            }
            
            var spinner = new SpinWait();
            while (clock.Elapsed.TotalMilliseconds < targetMs)
            {
                spinner.SpinOnce();
            }
        }
    }
}
