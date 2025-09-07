using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Lab1.DiningPhilosophers
{
	public sealed class SimulationContext
	{
        private int simulationSteps;
        private int totalPhilosophers;
		private int totalForks;
        private IPhilosopherStrategy strategy;

        private int step;

		public Philosopher[] Philosophers;
		public Fork[] Forks;
		public Metrics Metrics;

		public SimulationContext(int simSteps, int totalPhilosophers, int totalForks)
		{
            this.simulationSteps = simSteps;
            this.totalPhilosophers = totalPhilosophers;
            this.totalForks = totalForks;
            this.step = 0;
            this.Philosophers = new Philosopher[totalPhilosophers];
            this.Forks = new Fork[totalForks];
            this.Metrics = new Metrics();
            this.strategy = config.Strategy?.ToLowerInvariant() switch
            {
                "naive" => new NaiveStrategy(),
                "coordinated" => new CoordinatedStrategy(new SimpleCoordinator()),
                null or "" => new NaiveStrategy(),
                _ => new NaiveStrategy()
            };
		}

		public static SimulationContext makeFromJson(string filePath)
        {
            string json = File.ReadAllText(filePath);
            var config = JsonSerializer.Deserialize<SimulationConfig>(json) ?? throw new InvalidOperationException("Не удалось десериализовать конфигурацию");

            var context = new SimulationContext(config.SimulationSteps, config.TotalPhilosophers, config.TotalForks);

            // создаём вилки
            for (int i = 0; i < config.TotalForks; i++)
            {
                context.Forks[i] = new Fork(i);
            }

            PhilosopherContext ?defaultCtx = null;
            if (config.DefaultContext != null)
            {
                defaultCtx = new PhilosopherContext(config.DefaultContext);
            }

            for (int i = 0; i < config.TotalPhilosophers; i++)
            {
                string name = config.PhilosopherNames?[i] 
                            ?? config.Philosophers?[i].Name 
                            ?? $"Philosopher{i+1}";

                PhilosopherContext pCtx;
                if (defaultCtx != null) {
                    pCtx = defaultCtx;
                } else if (config.Philosophers != null && i < config.Philosophers.Length) {
                    pCtx = new PhilosopherContext(config.Philosophers[i].Context);
                } else {
                    throw new InvalidOperationException(
                        "Ни Philosopher[].Context, ни DefaultContext не заданы"
                    );
                }

                var leftFork = context.Forks[i];
                var rightFork = context.Forks[(i + 1) % config.TotalForks];

                context.Philosophers[i] = new Philosopher(name, leftFork, rightFork, pCtx);
            }

            return context;
        }

        public void runSimulation() {
            while (step <= simulationSteps) {
                foreach (var philosopher in simContext.Philosophers)
                {
                    strategy.PerformAction(philosopher, simContext);
                }
                simContext.Metrics.OnStep(simContext.Forks, simContext.Philosophers);
                ++step;
            }
        }
    }
}
