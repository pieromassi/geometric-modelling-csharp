namespace Geometry.Core;
using System;

public class BernsteinFunctions
{
    /* Return all Bernstein polynomials of degree n
       evaluated at u */
    public static float[] BernsteinRow(int n, float u)
    {
        if (n < 0)
        {
            throw new ArgumentException
            (
                "Degree must be positive"
            );
        }

        if (u < 0 || u > 1) 
        {
            throw new ArgumentException
            (
                "Value must be in range (0,1)"
            );
        }

        var row = new float[n+1]; // array of zeros
        row[0] = 1;

        for (int i = 0; i < n; i++)
        {    
            for (int j = n; j > 0; j--)
            {
                row[j] = (1-u) * row[j] + u * row[j-1];
            }
            
            row[0] = (1-u) * row[0];
        }
        return row;
    }
}

class TestBernstein 
{
    static void Main()
    {
        Console.WriteLine("Enter a degree: ");
        int n = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter a value: ");
        float u = float.Parse(Console.ReadLine());

        float[] row = BernsteinFunctions.BernsteinRow(n, u);
        
        Array.ForEach(row, Console.WriteLine);
    }
}