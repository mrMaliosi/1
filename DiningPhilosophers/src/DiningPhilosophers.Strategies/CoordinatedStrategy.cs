using Lab1.DiningPhilosophers;

namespace Lab1.DiningPhilosophers
{
    public sealed class CoordinatedStrategy : IPhilosopherStrategy
    {
        private readonly ICoordinator _coordinator;

        public CoordinatedStrategy(ICoordinator coordinator)
        {
            _coordinator = coordinator;
        }

        public void PerformAction(Philosopher philosopher, SimulationContext context)
        {
            _coordinator.RequestPermission(philosopher);
        }
    }
}
