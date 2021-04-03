using System;
using System.Threading;

namespace Bot_Framework.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(3500);
            //Mouse.SetPositionAsync(0, 0, delay:3, pixelsToMovePerDelay:10);

            Mouse.MouseDown();
            Thread.Sleep(1500);
            Mouse.MouseUp();

            /*Thread.Sleep(5000);
            Mouse.SetPosition(753, 258);
            Mouse.Click(MouseButton.Left);
            Thread.Sleep(500);
            Mouse.SetPosition(1897, 17);
            Thread.Sleep(500);
            Mouse.Click(MouseButton.Left);*/
            Console.ReadLine();
        }
    }
}