using Lab1.DiningPhilosophers;

namespace Lab1.DiningPhilosophers
{
    public sealed class NaiveStrategy : IPhilosopherStrategy
    {
        public void PerformAction(Philosopher philosopher, SimulationContext context)
        {
            // очень простая логика:
            // если философ думает — меняем на "голодный"
            // если голодный и есть вилки — ест
            // если ел — снова думает
        }
    }
}
