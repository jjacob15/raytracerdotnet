using System;
namespace Projectile
{
    using RayTracer;
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var p = new Projectile(Tuple.Point(0, 1, 0), Tuple.Vector(1, 1, 0).Normalize());
            var e = new Environment(Tuple.Vector(0, -0.05, 0), Tuple.Vector(-0.01, 0, 0));

            int i = 0;
            while (p.Position.Y > 0)
            {
                p = Tick(e, p);
                i++;
            }
            Console.WriteLine($"It took {i} iterations for Y to becomes zero");
            Console.Read();
        }

        static Projectile Tick(Environment env, Projectile projectile)
        {
            return new Projectile(
                projectile.Position + projectile.Velocity,
                projectile.Velocity + env.Gravity + env.Wind
                );
        }
    }
}
