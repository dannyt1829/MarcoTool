using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MarcoCreatorTool
{
    public static class InputSimulator
    {
        [DllImport("user32.dll")]
        private static extern bool mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const uint MOUSEEVENTF_RIGHTUP = 0x10;
        private const uint KEYEVENTF_KEYUP = 0x0002;

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
            Cursor.Position = new Point(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        }

        public static void LMouseUp(int x, int y)
        {
            // Mouse up at specified coordinates
            Cursor.Position = new Point(x, y);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static void RMouseDown(int x, int y)
        {
            // Mouse down at specified coordinates
            Cursor.Position = new Point(x, y);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
        }

        public static void RMouseUp(int x, int y)
        {
            // Mouse up at specified coordinates
            Cursor.Position = new Point(x, y);
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

        public static async Task Delay(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }
    }
}
