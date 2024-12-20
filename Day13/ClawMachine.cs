using System;

namespace Day13;

public class ClawMachine
{
    private readonly int buttonAToken = 3;
    private readonly int buttonBToken = 1;
    public Button ButtonA { get; set; }
    public Button ButtonB { get; set; }
    public Prize Prize { get; set; }

    public override string ToString()
    {
        return $"{ButtonA}\n{ButtonB}\n{Prize}\n";
    }

    public int? CalculateTokens()
    {
        // We need to solve the system of equations:
        // ButtonA.Movement.X * a + ButtonB.Movement.X * b = Prize.Position.X
        // ButtonA.Movement.Y * a + ButtonB.Movement.Y * b = Prize.Position.Y
        // where a and b are the number of times to press each button

        int a1 = ButtonA.Movement.X;
        int b1 = ButtonB.Movement.X;
        int c1 = Prize.Position.X;

        int a2 = ButtonA.Movement.Y;
        int b2 = ButtonB.Movement.Y;
        int c2 = Prize.Position.Y;

        // Using elimination method to solve for b:
        // Multiply first equation by a2, second by a1:
        // a1a2*a + b1a2*b = c1a2
        // a1a2*a + b2a1*b = c2a1

        // Subtract equations to eliminate a:
        // (b1a2 - b2a1)*b = c1a2 - c2a1

        // Check if solution exists
        if ((b1 * a2 - b2 * a1) == 0)
        {
            return null; // No solution exists
        }

        // Solve for b
        double b = (double)(c1 * a2 - c2 * a1) / (b1 * a2 - b2 * a1);

        // Solve for a using original equation
        double a = (c1 - b1 * b) / a1;

        // Check if solution is valid (positive integers)
        if (Math.Abs(a - Math.Round(a)) > 0.0001 ||
            Math.Abs(b - Math.Round(b)) > 0.0001 ||
            a < 0 || b < 0)
        {
            return null; // No valid solution exists
        }

        int pressesA = (int)Math.Round(a);
        int pressesB = (int)Math.Round(b);

        // Verify solution
        if (pressesA * ButtonA.Movement.X + pressesB * ButtonB.Movement.X != Prize.Position.X ||
            pressesA * ButtonA.Movement.Y + pressesB * ButtonB.Movement.Y != Prize.Position.Y)
        {
            return null; // Solution doesn't reach prize exactly
        }

        // Calculate total tokens needed
        return pressesA * buttonAToken + pressesB * buttonBToken;
    }
}
