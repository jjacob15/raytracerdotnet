using RayTracer;

namespace Projectile
{
    public class Environment
    {
        public Environment(Tuple gravity, Tuple wind)
        {
            Gravity = gravity;
            Wind = wind;
        }

        public Tuple Gravity { get; set; }
        public Tuple Wind { get; set; }

        public override string ToString()
        {
            return $"{Gravity} (w={Wind})";
        }
    }
}
