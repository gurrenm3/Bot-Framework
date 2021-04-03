﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Bot_Framework.Win32;

namespace Bot_Framework
{
    public enum MouseButton
    {
        Left,
        Right,
        Middle
    }

    enum MouseEventType
    {
        Down,
        Up
    }

    public class Mouse
    {
        public static Point GetCursorPos()
        {
            Win32.GetCursorPos(out Point point);
            return point;
        }

        public static void SetPosition(Point point)
        {
            SetPosition(point.x, point.y);
        }

        public static void SetPosition(int x, int y)
        {
            Win32.SetCursorPos(x, y);
        }

        public static async Task SetPositionAsync(Point point, int delay, int pixelsToMovePerDelay = 1)
        {
            SetPositionAsync(point.x, point.y, delay, pixelsToMovePerDelay);
        }

        public static async Task SetPositionAsync(int x, int y, int delay, int pixelsToMovePerDelay = 1)
        {
            await Task.Factory.StartNew(() => { SetPosition(x, y, delay, pixelsToMovePerDelay); });
        }

        public static void SetPosition(Point point, int delay, int pixelsToMovePerDelay = 1)
        {
            SetPosition(point.x, point.y, delay, pixelsToMovePerDelay);
        }

        public static void SetPosition(int x, int y, int delay, int pixelsToMovePerDelay = 1)
        {
            var currentPos = GetCursorPos();
            while (currentPos.x > x || currentPos.y > y)
            {
                int newXPos = currentPos.x;
                int newYPos = currentPos.y;

                if (newXPos != x)
                {
                    if (currentPos.x > x)
                    {
                        newXPos = currentPos.x - pixelsToMovePerDelay;
                        if (newXPos < x)
                            newXPos = x;
                    }
                    else
                    {
                        newXPos = currentPos.x + pixelsToMovePerDelay;
                        if (newXPos > x)
                            newXPos = x;
                    }
                }

                if (newYPos != y)
                {
                    if (currentPos.y > y)
                    {
                        newYPos = currentPos.y - pixelsToMovePerDelay;
                        if (newYPos < y)
                            newYPos = y;
                    }
                    else
                    {
                        newYPos = currentPos.y + pixelsToMovePerDelay;
                        if (newYPos > y)
                            newYPos = y;
                    }
                }

                SetPosition(newXPos, newYPos);

                Thread.Sleep(delay);
                currentPos = GetCursorPos();
            }
        }




        public static void MouseDown(MouseButton button = MouseButton.Left)
        {
            Win32.mouse_event((int)(MouseEventToMouseButton(button, MouseEventType.Down)), 0, 0, 0, 0);
        }

        public static void MouseUp(MouseButton button = MouseButton.Left)
        {
            Win32.mouse_event((int)(MouseEventToMouseButton(button, MouseEventType.Up)), 0, 0, 0, 0);
        }

        public static void Click(MouseButton button = MouseButton.Left)
        {
            Win32.mouse_event((int)(MouseEventToMouseButton(button, MouseEventType.Down)), 0, 0, 0, 0);
            Win32.mouse_event((int)(MouseEventToMouseButton(button, MouseEventType.Up)), 0, 0, 0, 0);
        }

        private static MouseEventFlags MouseEventToMouseButton(MouseButton mouseButton, MouseEventType pressType)
        {
            return (MouseEventFlags)Enum.Parse(typeof(MouseEventFlags), $"{mouseButton}{pressType}", true);
        }
    }
}