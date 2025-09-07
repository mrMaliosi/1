using Lab1.DiningPhilosophers;

namespace Lab1.DiningPhilosophers
{
    public sealed class SimpleCoordinator : ICoordinator
    {
        public event Action<Philosopher>? PhilosopherReady;

        public void RequestPermission(Philosopher philosopher)
        {
            // Логика: можно ли дать вилки?
            PhilosopherReady?.Invoke(philosopher);
        }
    }
}
