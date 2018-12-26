using System;
using System.void;
using System.Collections.Generic;
using System.Linq;

namespace KmaOoad18.Assignments.Week3
{
    public enum Color
    {Red = 1, White = 2, Blue = 3, Orange = 4, Yellow = 5, Green = 6}

    // To help, here is the "adjacency" lists for each Rubik's face
    // Color.Red = [ Color.White, Color.Blue, Color.Yellow, Color.Green ]
    // Color.Orange = [ Color.Green, Color.Yellow, Color.Blue, Color.White ]

    // Color.White = [ Color.Blue, Color.Red, Color.Orange, Color.Green ]
    // Color.Yellow = [ Color.Green, Color.Orange, Color.Red, Color.Blue ]

    // Color.Blue = [ Color.Red, Color.White, Color.Orange, Color.Yellow ]
    // Color.Green = [ Color.Yellow, Color.Orange, Color.White, Color.Red ]

    public delegate void SetColor();
    public class Square
    {
        private SetColor colorInConsole;
        private String colorLetter;
        private Color color;
        public String ColorLetter
        {
            get { return colorLetter; }
        }
        public SetColor ColorInConsole
        {
            get { return colorInConsole; }
        }
        public Color SquareColor
        {
            get { return color; }
        }
        private static void setWhiteColor()
        {
            Console.BackgroundColor = ConsoleColor.White;
        }
        private static void setBlueColor()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
        }private static void setRedColor()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
        }
        private static void setOrangeColor()
        {
            Console.BackgroundColor = ConsoleColor.Red;
        }
        private static void setYellowColor()
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
        }
        private static void setGreenColor()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }

        public Square(Color c)
        {
            color = c;
            if (c == Color.Red)
            {
                colorLetter = "R";
                colorInConsole = setRedColor;
            }
            else if (c == Color.White)
            {
                colorLetter = "W";
                colorInConsole = setWhiteColor;
            }
            else if (c == Color.Blue)
            {
                colorLetter = "B";
                colorInConsole = setBlueColor;
            }
            else if (c == Color.Orange)
            {
                colorLetter = "O";
                colorInConsole = setOrangeColor;
            }
            else if (c == Color.Green)
            {
                colorLetter = "G";
                colorInConsole = setGreenColor;
            }
            else if (c == Color.Yellow)
            {
                colorLetter = "Y";
                colorInConsole = setYellowColor;
            }
        }
    }

    public class Face
    {
        private int numberOfSquaresOnOneFace = 9;
        private int size = 3;
        private List<Square> squares;
        private Color faceColor;
        private List<Color> adjacency;
        private Color opposite;
        public Face(Color c, int size)
        {
            this.size = size;
            numberOfSquaresOnOneFace = size * size;
            faceColor = c;
            squares = new List<Square>();
            for (int i = 0; i < numberOfSquaresOnOneFace; ++i)
            {
                squares.Add(new Square(faceColor));
            }

            if (c == Color.Red)
            {
                adjacency = new List<Color>() { Color.White, Color.Blue, Color.Yellow, Color.Green };
                opposite = Color.Orange;
            }
            else if (c == Color.Yellow)
            {
                adjacency = new List<Color>() { Color.Green, Color.Orange, Color.Red, Color.Blue };
                opposite = Color.White;
            }
            else if (c == Color.Blue)
            {
                adjacency = new List<Color>() { Color.Red, Color.White, Color.Orange, Color.Yellow };
                opposite = Color.Green;
            }
            else if (c == Color.Orange)
            {
                adjacency = new List<Color>() { Color.Green, Color.Yellow, Color.Blue, Color.White };
                opposite = Color.Red;
            }
            else if (c == Color.White)
            {
                adjacency = new List<Color>() { Color.Blue, Color.Red, Color.Orange, Color.Green };
                opposite = Color.Yellow;
            }
            else if (c == Color.Green)
            {
                adjacency = new List<Color>() { Color.Yellow, Color.Orange, Color.White, Color.Red };
                opposite = Color.Blue;
            }
        }
        public string Display()
        {
            String divide = "";
            // Return string of colors for given face
            String result = "";
            for (int i = 0; i < numberOfSquaresOnOneFace; ++i)
            {
                squares[i].ColorInConsole();
                int squareNum = i + 1;
                String colorLetter = "[" + squares[i].ColorLetter + "]";
                Console.Write(colorLetter);
                result += colorLetter;
                Console.BackgroundColor = ConsoleColor.Black;
                if (squareNum % size == 0 && squareNum > size - 1)
                {
                    Console.WriteLine();
                    if (squareNum != numberOfSquaresOnOneFace)
                    {
                        result += "\n" + divide + "\n";
                        Console.WriteLine(divide);
                    }
                }
                else
                {
                    String dotLine = " ";
                    result += dotLine;
                    Console.Write(dotLine);
                }
            }

            Console.BackgroundColor = ConsoleColor.Black;

            return result;
        }

        public bool isSolved()
        {
            foreach (Square square in squares)
            {
                if (square.SquareColor != faceColor)
                {
                    return false;
                }
            }
            return true;
        }

        List<Square> getHorizontalRow(int num)
        {
            if (num >= size)
            {
                return null;
            }
            List<Square> result = new List<Square>();
            int firstSquare = 0;
            for (int i = 0; i < num; ++i)
            {
                firstSquare += size;
            }
            int tilSquare = firstSquare + size - 1;
            for (int i = firstSquare; i <= tilSquare; ++i)
            {
                result.Add(squares[i]);
            }
            return result;
        }
        void setHorizontalRow(int num, List<Square> setSquares)
        {
            if (num >= size)
            {
                return;
            }
            int firstSquare = 0;
            for (int i = 0; i < num; ++i)
            {
                firstSquare += size;
            }
            int tillSquare = firstSquare + size - 1;
            int k = 0;
            for (int i = firstSquare; i <= tillSquare; ++i)
            {
                squares[i] = setSquares[k++];
            }
        }
        List<Square> getVerticalRow(int num)
        {
            if (num >= size)
            {
                return null;
            }
            List<Square> result = new List<Square>();
            int firstSquare = num;
            int tillSquare = num + (size - 1) * size;
            for (int i = firstSquare; i <= tillSquare; i += size)
            {
                result.Add(squares[i]);
            }
            return result;
        }
        void setVerticalRow(int num, List<Square> setSquares)
        {
            if (num >= size)
            {
                return;
            }
            int firstSquare = num;
            int tillSquare = num + (size - 1) * size;
            int k = 0;
            for (int i = firstSquare; i <= tillSquare; i += size)
            {
                squares[i] = setSquares[k++];
            }
        }

        public List<List<Square>> getRightPart(int quantity)
        {
            List<List<Square>> result = new List<List<Square>>();
            int firstRow = size - 1;
            int tillRow = size - 1 - quantity;
            for (int i = firstRow; i > tillRow; --i)
            {
                List<Square> row = getVerticalRow(i);
                result.Add(row);
            }
            return result;
        }

        public void setRightPart(List<List<Square>> setRows)
        {
            int firstRow = size - 1;
            int tillRow = size - 1 - setRows.Count;
            int k = 0;
            for (int i = firstRow; i > tillRow; --i)
            {
                setVerticalRow(i, setRows[k++]);
            }
        }

        public List<List<Square>> getLeftPart(int quantity)
        {
            List<List<Square>> result = new List<List<Square>>();
            int firstRow = 0;
            int tillRow = quantity;
            for (int i = firstRow; i < tillRow; ++i)
            {
                List<Square> row = getVerticalRow(0);
                result.Add(row);
            }
            return result;
        }
        public void setLeftPart(List<List<Square>> setRows)
        {
            int firstRow = 0;
            int tillRow = setRows.Count;
            int k = 0;
            for (int i = firstRow; i < tillRow; ++i)
            {
                setVerticalRow(i, setRows[k++]);
            }
        }
        public List<List<Square>> getUpperPart(int quantity)
        {
            List<List<Square>> result = new List<List<Square>>();
            int firstRow = 0;
            int tillRow = quantity;
            for (int i = firstRow; i < tillRow; ++i)
            {
                List<Square> row = getHorizontalRow(i);
                result.Add(row);
            }
            return result;
        }
        public void setUpperPart(List<List<Square>> setRows)
        {
            int firstRow = 0;
            int tillRow = setRows.Count;
            int k = 0;
            for (int i = firstRow; i < tillRow; ++i)
            {
                setHorizontalRow(i, setRows[k++]);
            }
        }
        public List<List<Square>> getLowerPart(int quantity)
        {
            List<List<Square>> result = new List<List<Square>>();
            int firstRow = size - 1;
            int tillRow = size - 1 - quantity;
            for (int i = firstRow; i > tillRow; --i)
            {
                List<Square> row = getHorizontalRow(i);
                result.Add(row);
            }
            return result;
        }
        public void setLowerPart(List<List<Square>> setRows)
        {
            int firstRow = size - 1;
            int tillRow = size - 1 - setRows.Count;
            int k = 0;
            for (int i = firstRow; i > tillRow; --i)
            {
                setHorizontalRow(i, setRows[k++]);
            }
        }
        public List<Color> Adjacency
        {
            get { return adjacency; }
        }

        public Color Opposite
        {
            get { return opposite; }
        }
    }

    public class AdvancedRubik
    {
        private int size;
        private int center;
        private static readonly Dictionary<Color, Square> squares = new Dictionary<Color, Square>() {
            {Color.Red, new Square(Color.Red)},
            {Color.White, new Square(Color.White)},
            {Color.Blue, new Square(Color.Blue)},
            {Color.Orange, new Square(Color.Orange)},
            {Color.Yellow, new Square(Color.Yellow)},
            {Color.Green, new Square(Color.Green)}
        };
        private Dictionary<Color, Face> faces;
        private AdvancedRubik() { }

        public static AdvancedRubik Init(int size)
        {
            var r = new AdvancedRubik();

            // Init Rubik here
            r.size = size;
            r.center = size / 2;
            Console.WriteLine(r.center);
            r.faces = new Dictionary<Color, Face>();
            foreach (Color c in (Color[])Enum.GetValues(typeof(Color)))
            {
                Face face = new Face(c, r.size);
                r.faces.Add(c, face);
            }

            return r;
        }

        public AdvancedRubik Scramble()
        {
            // Randomize Rubik here
            Random rnd = new Random();
            for (int i = 0; i < 1000; ++i) {
                int color = rnd.Next(1, 7);
                int num = rnd.Next(1, 101);
                int slicesToRotate = rnd.Next(1, size);
                if (num > 50) {
                   this.RotateClockwise((Color) color, slicesToRotate);
                } else {
                    this.RotateCounterClockwise((Color) color, slicesToRotate);
                }
            }
            return this;
        }

        public bool Solved()
        {
            // Put your check for solution here
            foreach (Face f in faces.Values)
            {
                if (!f.isSolved())
                {
                    return false;
                }
            }
            return true;
        }

        public string Display(Color face)
        {
            // Return string of colors for given face
            String result = faces[face].Display();

            return result;
        }

        public string Display()
        {
            // Print string of colors of all faces
            String result = "";
            foreach (Color c in squares.Keys)
            {
                result += Display(c);
                Console.WriteLine();
                Console.WriteLine();
                result += "\n\n";
            }

            return result;
        }

        public AdvancedRubik RotateClockwise(Color face, int slicesToRotate)
        {
            // Implement clockwise rotation for given face and number of slices to rotate
            if (slicesToRotate < size)
            {
                Face curFace = faces[face];
                if (slicesToRotate <= center)
                {
                    Color upperColor = curFace.Adjacency[0];
                    Face upperFace = faces[upperColor];

                    Color leftColor = curFace.Adjacency[3];
                    Face leftFace = faces[leftColor];

                    Color rightColor = curFace.Adjacency[1];
                    Face rightFace = faces[rightColor];

                    Color lowerColor = curFace.Adjacency[2];
                    Face lowerFace = faces[lowerColor];

                
                    List<List<Square>> tempUpperFaceLowerPart =
                    new List<List<Square>>(upperFace.getLowerPart(slicesToRotate));
                    upperFace.setLowerPart(leftFace.getRightPart(slicesToRotate));
                    List<List<Square>> tempRightFaceLeftPart =
                    new List<List<Square>>(rightFace.getLeftPart(slicesToRotate));
                    rightFace.setLeftPart(tempUpperFaceLowerPart);
                    List<List<Square>> tempLowerFaceUpperPart =
                    new List<List<Square>>(lowerFace.getUpperPart(slicesToRotate));
                    lowerFace.setUpperPart(tempRightFaceLeftPart);
                    leftFace.setRightPart(tempLowerFaceUpperPart);
                }
                else
                {
                    Color oppositecColor = curFace.Opposite;
                    int leftSlicesToRotate = size - slicesToRotate;
                    RotateCounterClockwise(oppositecColor, leftSlicesToRotate);
                }
            }
            return this;
        }

        public AdvancedRubik RotateCounterClockwise(Color face, int slicesToRotate)
        {
            // Implement counter-clockwise rotation for given face and number of slices to rotate
            if (slicesToRotate < size)
            {
                Face curFace = faces[face];
                if (slicesToRotate <= center)
                {
                    Color upperColor = curFace.Adjacency[0];
                    Face upperFace = faces[upperColor];

                    Color leftColor = curFace.Adjacency[3];
                    Face leftFace = faces[leftColor];

                    Color rightColor = curFace.Adjacency[1];
                    Face rightFace = faces[rightColor];

                    Color lowerColor = curFace.Adjacency[2];
                    Face lowerFace = faces[lowerColor];

                    List<List<Square>> tempUpperFaceLowerPart =
                    new List<List<Square>>(upperFace.getLowerPart(slicesToRotate));
                    upperFace.setLowerPart(rightFace.getLeftPart(slicesToRotate));
                    List<List<Square>> tempLeftFaceRightPart =
                    new List<List<Square>>(leftFace.getRightPart(slicesToRotate));
                    leftFace.setRightPart(tempUpperFaceLowerPart);
                    List<List<Square>> tempLowerFaceUpperPart =
                    new List<List<Square>>(lowerFace.getUpperPart(slicesToRotate));
                    lowerFace.setUpperPart(tempLeftFaceRightPart);
                    rightFace.setLeftPart(tempLowerFaceUpperPart);
                }
                else
                {
                    Color oppositecColor = curFace.Opposite;
                    int leftSlicesToRotate = size - slicesToRotate;
                    RotateClockwise(oppositecColor, leftSlicesToRotate);
                }
            }
            return this;
        }
    }

}
