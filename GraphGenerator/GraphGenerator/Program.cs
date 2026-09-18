using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GraphGenerator
{
    internal class Program
    {
        /// <summary>
        /// Creates a new graph and inputs a set of points, writes it to the console and allows the user to save the graph to a text file.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            graph.ReadPoints();
            graph.DrawGraph();
            Console.Write("Would you like to save this graph to a text file [Y/N]: ");
            while (true)
            {
                string answer = Console.ReadLine().ToUpper();
                if (answer == "Y")
                {
                    string path = System.DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss") + ".txt";
                    Console.WriteLine($"Thank you, saving now to \"{path}\".");
                    graph.WriteToFile(path);
                    break;
                }
                else if (answer == "N")
                {
                    Console.WriteLine("Thank you, exiting the program now.");
                    break;
                }
                else
                {
                    Console.Write("Invalid input, please re-enter \"Y\" or \"N\": ");
                }
            }
        }
    }
}
