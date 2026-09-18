using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphGenerator
{
    /// <summary>
    /// Contains a 2d graph stored as points with x and y co-ordinates.
    /// </summary>
    /// <remarks>
    /// Negative x values are not possible, and the points must have integer co-ordinates.
    /// </remarks>
    internal class Graph
    {
        public int xMax { get; set; } // xMin is not neccesary, because the graph always starts at zero
        public int yMin { get; set; }
        public int yMax { get; set; }
        public HashSet<(int, int)> pointSet { get; } = new HashSet<(int, int)>();

        /// <summary>
        /// Reads in a set of points to add to the graph from a text file at a path input from the user.
        /// </summary>
        public void ReadPoints()
        {
            while (true)
            {
                Console.Write("Please enter the file path of the graph you want to load in: ");
                string path = Console.ReadLine();
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string line;
                        int lineNumber = 0;
                        while ((line = sr.ReadLine()) != null)
                        {
                            lineNumber++;
                            string[] splat = line.Split(' ');
                            int x, y;
                            if (!int.TryParse(splat[1], out y))
                            {
                                throw new FormatException($"The y part of\"{line}\", \"{splat[1]}\" was in an invalid format (line {lineNumber})");
                            }
                            if (!int.TryParse(splat[0], out x))
                            {
                                throw new FormatException($"The x part of\"{line}\", \"{splat[0]}\" was in an invalid format (line {lineNumber})");
                            }
                            else if (x < 0)
                            {
                                throw new ArgumentOutOfRangeException($"The point ({x}, {y}) was out of range. The x co-ordinate must not be below zero (line {lineNumber})");
                            }
                            if (x > xMax)
                            {
                                xMax = x;
                            }
                            if (y < yMin)
                            {
                                yMin = y;
                            }
                            if (y > yMax)
                            {
                                yMax = y;
                            }
                            pointSet.Add((x, y));
                        }
                        return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Formats and writes the graph to a text file.
        /// </summary>
        /// <param name="path">The path in which to save the file.</param>
        public void WriteToFile(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                int indexWidth; // The amount of characters needed for the longest y index
                if (yMin > 0)
                {
                    indexWidth = yMax.ToString().Length;
                }
                else if (yMax > Math.Abs(yMin) * 10) // Checking if the maximum y value will end up having more characters than the minimum y value
                {
                    indexWidth = yMax.ToString().Length;
                }
                else
                {
                    indexWidth = yMin.ToString().Length;
                }

                for (int y = yMax; y >= yMin; y--)
                {
                    string index = y.ToString();
                    char empty; // The character to display if the cell is empty
                    sw.Write(new string(' ', indexWidth - index.Length) + index); // Writes the index of the current y value, aligning it to the right
                    if (y == 0)
                    {
                        sw.Write("+");
                        empty = '-';
                    }
                    else
                    {
                        sw.Write("|");
                        empty = ' ';
                    }
                    for (int x = 0; x < xMax; x++)
                    {
                        var searchTuple = (x, y);
                        if (pointSet.Contains(searchTuple))
                        {
                            sw.Write("X");
                        }
                        else
                        {
                            sw.Write(empty);
                        }
                    }
                    sw.WriteLine(); // Go down to a new line for the next y value of the graph
                }
            }
        }

        /// <summary>
        /// Formats and prints the graph to the console.
        /// </summary>
        public void DrawGraph()
        {
            int indexWidth; // The amount of characters needed for the longest y index
            if (yMin > 0)
            {
                indexWidth = yMax.ToString().Length;
            }
            else if (yMax > Math.Abs(yMin) * 10) // Checking if the maximum y value will end up having more characters than the minimum y value
            {
                indexWidth = yMax.ToString().Length;
            }
            else
            {
                indexWidth = yMin.ToString().Length;
            }

            for (int y = yMax; y >= yMin; y--)
            {
                string index = y.ToString();
                char empty; // The character to display if the cell is empty
                Console.Write(new string(' ', indexWidth - index.Length) + index); // Writes the index of the current y value, aligning it to the right
                if (y == 0)
                {
                    Console.Write("+");
                    empty = '-';
                }
                else
                {
                    Console.Write("|");
                    empty = ' ';
                }
                for (int x = 0; x < xMax; x++)
                {
                    var searchTuple = (x, y);
                    if (pointSet.Contains(searchTuple))
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write(empty);
                    }
                }
                Console.WriteLine(); // Go down to a new line for the next y value of the graph
            }
        }
    }
}
