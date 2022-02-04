using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_03_Polynomial_functions
{
    class Polynomial
    {
        List<Point2D> PointList = new List<Point2D>();
        List<double> Parameters = new List<double>();
        List<bool> ParameterSigns = new List<bool>();
        List<double> DerivativeParameters = new List<double>();
        List<bool> DerivativeSigns = new List<bool>();

        public Polynomial(List<Point2D> pointList)
        {
            PointList = pointList;
            Matrix matrix = new Matrix(pointList);
            matrix.Solve();
            Parameters = matrix.ReturnAnswers();
            foreach (double parameter in Parameters)
            {
                if (parameter >= 0)
                {
                    ParameterSigns.Add(true);
                }
                else
                {
                    ParameterSigns.Add(false);
                }
            }
        }

        public void ShowFunction()
        {
            string function = "f(x) = ";
            string sign = ParameterSigns.First() ? "" : "-";

            function += $"{sign}{Math.Round(Parameters.First(), 5).ToString().Replace("-", "")}x^{Parameters.Count()-1} ";

            for (int i = 1; i < Parameters.Count() - 1; i++)
            {
                sign = ParameterSigns[i] ? "+" : "-";
                function += $"{sign} {Math.Round(Parameters[i], 5).ToString().Replace("-", "")}x^{Parameters.Count()-1 - i} ";
            }

            sign = ParameterSigns.Last() ? "+" : "-";
            function += $"{sign} {Math.Round(Parameters.Last(), 5).ToString().Replace("-", "")}";

            Console.WriteLine(function);
        }

        public double Solve(double x)
        {
            double answer = Parameters.Last();

            for (int i = 0; i < Parameters.Count() - 1; i++)
            {
                answer += Parameters[i]*Math.Pow(x, Parameters.Count()-1 - i);
            }

            return answer;
        }

        public double SolveDerivative(double x)
        {
            double answer = DerivativeParameters.Last();

            for (int i = 0; i < DerivativeParameters.Count() - 1; i++)
            {
                answer += DerivativeParameters[i] * Math.Pow(x, DerivativeParameters.Count() - 1 - i);
            }

            return answer;
        }

        public void ShowSolution(double x)
        {
            Console.WriteLine($"f({x}) = {Solve(x)}");
        }

        public void FindDerivative()
        {
            for (int i = 0; i < Parameters.Count - 1; i++)
            {
                DerivativeParameters.Add((Parameters.Count-1-i) * Parameters[i]);
                DerivativeSigns.Add(DerivativeParameters[i] >= 0 ? true : false);
            }
        }

        public void ShowDerivative()
        {
            FindDerivative();
            Console.WriteLine("\nDerivative:");
            string derivative = "f'(x) = ";
            string sign = DerivativeSigns.First() ? "" : "-";
            if (DerivativeParameters.Count() > 1)
            {
                derivative += $"{sign}{Math.Round(DerivativeParameters.First(), 5).ToString().Replace("-", "")}x^{DerivativeParameters.Count() - 1} ";

                for (int i = 1; i < DerivativeParameters.Count() - 1; i++)
                {
                    sign = DerivativeSigns[i] ? "+" : "-";
                    derivative += $"{sign} {Math.Round(DerivativeParameters[i], 5).ToString().Replace("-", "")}x^{DerivativeParameters.Count() - 1 - i} ";
                }
            }

            sign = DerivativeSigns.Last() ? "" : "-";
            derivative += $"{sign} {Math.Round(DerivativeParameters.Last(), 5).ToString().Replace("-", "")}";

            Console.WriteLine(derivative);
        }

        public void FindRoots(double guess)
        {
            bool rootFound = false;
            double newRoot = guess;
            for (int i = 1; i <= 100; i++)
            {
                double root = newRoot;
                newRoot = root - (Solve(root) / SolveDerivative(root));
                double error = (newRoot - root) / newRoot;

                if(error < 0.0001 && Solve(newRoot) == 0)
                {
                    Console.WriteLine("Root found for x = " + newRoot);
                    rootFound = true;
                    break;
                }
            }
            if(!rootFound)
            {
                Console.WriteLine("Algorithm couldn't find the root");
            }
        }
    }
}
