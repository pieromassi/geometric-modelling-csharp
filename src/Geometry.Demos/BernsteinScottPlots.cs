using System;
using System.Linq;
using Geometry.Core;
using ScottPlot;

public class BernsteinScottPlots
{
    public static void Run()
    {
        Console.WriteLine("Insert a degree: ");
        if (!int.TryParse(Console.ReadLine(), out int degree) || degree < 0)
        {
            throw new ArgumentException
            (
                "Degree must be a positive integer"
            );
        }

        /* Create samples */
        const int intervals = 1000;
        float[] values = Enumerable.Range(0, intervals+1)
                        .Select(i => i / (float)intervals)
                        .ToArray();
        
        /* Initialize matrix of Bernstein polynomials to evaluate */
        var evals = new float[degree+1][];
        for (int k = 0; k < evals.Length; k++) 
        {
            evals[k] = new float[values.Length];
        }
        
        /* Evaluate Bernstein polynomials efficiently
        * each Bernstein row is distributed to evals rows  */
        for (int j = 0; j < values.Length; j++)
        {
            float[] row = BernsteinFunctions.BernsteinRow(degree, values[j]);

            for (int k = 0; k < evals.Length; k++) 
            {
                evals[k][j] = row[k];
            }
        }
        
        Plot plot = new();

        for (int k = 0; k < evals.Length; k++) 
        {
            var curve = plot.Add.Scatter(values, evals[k]);
            curve.MarkerSize = 0;
            curve.LegendText = $"B({k},{degree})";
        }

        plot.ShowLegend();
        plot.SavePng("bernstein.png", 800, 500);
    }
}