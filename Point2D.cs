using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_03_Polynomial_functions
{
    class Point2D
    {
        private double x, y;

        public Point2D(double x, double y) { this.x = x; this.y = y; }
        public Point2D(string inputString)
        {
            if(CheckPoint(inputString))
            {
                x = double.Parse(inputString.Split(' ')[0]);
                y = double.Parse(inputString.Split(' ')[1]);
            }
            else 
            {
                Console.WriteLine("Input is incorrect. Only 2 dimensional points are allowed");
            }
        }
        public bool CheckPoint(string point)
        {
            bool isDigit = point.Replace("-", "").Split(' ').All(d => double.TryParse(d, out double num));
            bool isPoint = (point.Replace("-", "").Split(' ').Length == 2 && (point.Replace("-", "").Split(' ')[0] != ""));
            return (isDigit && isPoint);
        }
        public double ReturnX() { return x; }
        public double ReturnY() { return y; }
    }
}
