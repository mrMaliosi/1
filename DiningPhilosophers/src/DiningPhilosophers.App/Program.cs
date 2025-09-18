using System;
using Lab1.DiningPhilosophers;

namespace Lab1.DiningPhilosophers
{
    public static class Program
	{
        public static void Main()
        {
            var configPath = @"C:\Users\Владимир\Desktop\Универ\4 курс\CS\1\DiningPhilosophers\conf\config.json";
            SimulationContext simContext = SimulationContext.makeFromJson(configPath);

            IPhilosopherStrategy strategy = simContext.StrategyName switch
            {
                "coord" => new CoordinatedStrategy(new SimpleCoordinator()),
                _ => new NaiveStrategy()
            };

            simContext.runSimulation(strategy.PerformAction);
            Console.WriteLine("Симуляция завершена.");
        }

    }
}
