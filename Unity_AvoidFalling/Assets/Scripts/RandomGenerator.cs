using System.Collections;
using System.Collections.Generic;
using System;

public sealed class RandomGenerator 
{
    // Singleton Pattern
    private static readonly System.Random instance = new System.Random();
    static RandomGenerator() {}
    private RandomGenerator() {}

    // as long this is not called to often, it should not affect the performance
    public static System.Random get_instance()
    {
        return instance;
    }

    public static (float, float) generate_2D_point_rect(float[] x_bound, float[] z_bound)
    {
        // randomly draws a point from the rectangle with given x- and y-bounds
        // System.Random tmp = get_instance();
        float x = (float) instance.NextDouble();
        x = x_bound[0] + (x_bound[1] - x_bound[0]) * x;
        float z = (float) instance.NextDouble();
        z = z_bound[0] + (z_bound[1] - z_bound[0]) * z;
        return (x, z);
    }

    public static (float, float) generate_2D_point_circle(float spawn_radius)
    {
        System.Random rnd_gen = RandomGenerator.get_instance();
        // draw random angle and radius
        float rnd_angle = (float) rnd_gen.NextDouble() *360;
        float rnd_radius = (float) rnd_gen.NextDouble() * spawn_radius;
        // convert both appropriately to the correct random coordinates in a circle with radius "spawn_radius"
        float x = (float) (Math.Cos(rnd_angle) * Math.Sqrt(rnd_radius * spawn_radius));
        float z = (float) (Math.Sin(rnd_angle) * Math.Sqrt(rnd_radius * spawn_radius));
        return (x, z);
    }
}
