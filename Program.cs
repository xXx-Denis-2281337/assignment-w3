using System;

namespace KmaOoad18.Assignments.Week3
{
    class Program
    {
        static void Main(string[] args)
        {
            AdvancedRubik rubik = AdvancedRubik.Init(5);
            Console.WriteLine(rubik.Solved());
            rubik.RotateCounterClockwise(Color.Green,4);
            rubik.Display();
            rubik.RotateClockwise(Color.Orange,2);
            rubik.Display(Color.White);
            rubik.Scramble();
            Console.WriteLine(rubik.Solved());
            Console.WriteLine("Hello World!");
        }
    }

}
