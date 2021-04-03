using System;
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



        public static Point GetCursorPos()
        {
            Win32.GetCursorPos(out Point point);
            return point;
        }

        public static void SetPosition(Point point)
        {
            SetPosition(point.X, point.Y);
        }

        public static void SetPosition(int x, int y)
        {
            Win32.SetCursorPos(x, y);
        }

        public static async Task SetPositionAsync(Point point, int delay, int pixelsToMovePerDelay = 1)
        {
            SetPositionAsync(point.X, point.Y, delay, pixelsToMovePerDelay);
        }

        public static async Task SetPositionAsync(int x, int y, int delay, int pixelsToMovePerDelay = 1)
        {
            await Task.Factory.StartNew(() => { SetPosition(x, y, delay, pixelsToMovePerDelay); });
        }

        public static void SetPosition(Point point, int delay, int pixelsToMovePerDelay = 1)
        {
            SetPosition(point.X, point.Y, delay, pixelsToMovePerDelay);
        }

        public static void SetPosition(int x, int y, int delay, int pixelsToMovePerDelay = 1)
        {
            var currentPos = GetCursorPos();
            while (currentPos.X > x || currentPos.Y > y)
            {
                int newXPos = currentPos.X;
                int newYPos = currentPos.Y;

                if (newXPos != x)
                {
                    if (currentPos.X > x)
                    {
                        newXPos = currentPos.X - pixelsToMovePerDelay;
                        if (newXPos < x)
                            newXPos = x;
                    }
                    else
                    {
                        newXPos = currentPos.X + pixelsToMovePerDelay;
                        if (newXPos > x)
                            newXPos = x;
                    }
                }

                if (newYPos != y)
                {
                    if (currentPos.Y > y)
                    {
                        newYPos = currentPos.Y - pixelsToMovePerDelay;
                        if (newYPos < y)
                            newYPos = y;
                    }
                    else
                    {
                        newYPos = currentPos.Y + pixelsToMovePerDelay;
                        if (newYPos > y)
                            newYPos = y;
                    }
                }

                SetPosition(newXPos, newYPos);

                Thread.Sleep(delay);
                currentPos = GetCursorPos();
            }
        }
    }
}