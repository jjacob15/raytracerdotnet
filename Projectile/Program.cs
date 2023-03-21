using System;
namespace Projectile
{
    using RayTracer;
    class Program
    {
        static void Main(string[] args)
        {
            var a = Tuple.Vector(1, 1.8, 0).Normalize();
            var p = new Projectile(Tuple.Point(0, 1, 0), Tuple.Vector(1, 1.8, 0).Normalize() * 11.25);
            var e = new Environment(Tuple.Vector(0, -0.1, 0), Tuple.Vector(-0.01, 0, 0));

            int i = 0;
            var cx = 900;
            var cy = 500;
            Canvas c = new Canvas(cx, cy);
            while (p.Position.Y > 0)
            {
                p = Tick(e, p);
                var x = (int)Math.Round(p.Position.X);
                var y = cy - (int)Math.Round(p.Position.Y);
                if (x < 0 || x > cx || y < 0 || y > cy) continue;
                c.SetPixel(x, y,new Color(1, 0, 0));
                i++;
            }
            c.Save("projectile.ppm");
            Console.WriteLine($"It took {i} iterations for Y to becomes zero");
            Console.Read();
        }

        static Projectile Tick(Environment env, Projectile projectile)
        {
            return new Projectile(projectile.Position + projectile.Velocity,
                projectile.Velocity + env.Gravity + env.Wind
                );
        }
    }
}
