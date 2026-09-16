using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace MarcoCreatorTool
{
    public static class InputSimulator
    {
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const uint MOUSEEVENTF_RIGHTUP = 0x10;
        private const uint KEYEVENTF_KEYUP = 0x0002;
        private const uint KEYEVENTF_KEYDOWN = 0x0000;
        private const uint MOUSEEVENTF_MOVE = 0x0001;
        private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;

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
            [FieldOffset(0)]
            public KEYBDINPUT ki;
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

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        public static void MoveMouse(int x, int y)
        {
            // Move mouse to specified coordinates
            int virtualWidth = GetSystemMetrics(78);
            int virtualHeight = GetSystemMetrics(79);
            int virtualX = GetSystemMetrics(76);
            int virtualY = GetSystemMetrics(77);

            int normalizedX = (int)((x - virtualX) * 65535.0 / virtualWidth);
            int normalizedY = (int)((y - virtualY) * 65535.0 / virtualHeight);

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

        public static void LMouseDown(int x, int y)
        {
            // Mouse down at specified coordinates

            int virtualWidth = GetSystemMetrics(78);
            int virtualHeight = GetSystemMetrics(79);
            int virtualX = GetSystemMetrics(76);
            int virtualY = GetSystemMetrics(77);

            int normalizedX = (int)((x - virtualX) * 65535.0 / virtualWidth);
            int normalizedY = (int)((y - virtualY) * 65535.0 / virtualHeight);

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
                        dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };

            SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void LMouseUp(int x, int y)
        {
            // Mouse up at specified coordinates
            int virtualWidth = GetSystemMetrics(78);
            int virtualHeight = GetSystemMetrics(79);
            int virtualX = GetSystemMetrics(76);
            int virtualY = GetSystemMetrics(77);

            int normalizedX = (int)((x - virtualX) * 65535.0 / virtualWidth);
            int normalizedY = (int)((y - virtualY) * 65535.0 / virtualHeight);

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
                        dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTUP,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };

            SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void RMouseDown(int x, int y)
        {
            // Mouse down at specified coordinates
            int virtualWidth = GetSystemMetrics(78);
            int virtualHeight = GetSystemMetrics(79);
            int virtualX = GetSystemMetrics(76);
            int virtualY = GetSystemMetrics(77);

            int normalizedX = (int)((x - virtualX) * 65535.0 / virtualWidth);
            int normalizedY = (int)((y - virtualY) * 65535.0 / virtualHeight);

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
                        dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_RIGHTDOWN,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };

            SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void RMouseUp(int x, int y)
        {
            // Mouse up at specified coordinates
            int virtualWidth = GetSystemMetrics(78);
            int virtualHeight = GetSystemMetrics(79);
            int virtualX = GetSystemMetrics(76);
            int virtualY = GetSystemMetrics(77);

            int normalizedX = (int)((x - virtualX) * 65535.0 / virtualWidth);
            int normalizedY = (int)((y - virtualY) * 65535.0 / virtualHeight);

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
                        dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_RIGHTUP,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };

            SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void KeyDown(Keys key)
        {
            // Simulate key down
            byte vk = (byte)key;

            INPUT[] inputs = new INPUT[1];
            inputs[0] = new INPUT
            {
                type = 1, // Input type: keyboard
                u = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = vk,
                        wScan = 0,
                        dwFlags = KEYEVENTF_KEYDOWN,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };

            SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void KeyUp(Keys key)
        {
            // Simulate key up
            byte vk = (byte)key;

            INPUT[] inputs = new INPUT[1];
            inputs[0] = new INPUT
            {
                type = 1, // Input type: keyboard
                u = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = vk,
                        wScan = 0,
                        dwFlags = KEYEVENTF_KEYUP,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };

            SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void MouseWheel(int delta)
        {
            // Simulate mouse wheel scroll
            mouse_event(0x0800, 0, 0, (uint)delta, UIntPtr.Zero);
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
