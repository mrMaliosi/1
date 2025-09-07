using System.Collections.Generic;
using System.Linq;

namespace Lab1.DiningPhilosophers
{
	public sealed class Metrics
	{
		public int TotalSteps { get; private set; }
		public readonly Dictionary<string, int> MealsByPhilosopher = new();
		public readonly Dictionary<string, int> HungryTimeByPhilosopher = new();
		public readonly Dictionary<int, int> ForkBusySteps = new();
		public readonly Dictionary<int, int> ForkFreeSteps = new();

		public void OnStep(IEnumerable<Fork> forks, IEnumerable<Philosopher> philosophers)
		{
			TotalSteps++;
			foreach (var fork in forks)
			{
				if (!ForkBusySteps.ContainsKey(fork.Id)) ForkBusySteps[fork.Id] = 0;
				if (!ForkFreeSteps.ContainsKey(fork.Id)) ForkFreeSteps[fork.Id] = 0;
				if (fork.State == ForkState.InUse) ForkBusySteps[fork.Id]++;
				else ForkFreeSteps[fork.Id]++;
			}
			foreach (var p in philosophers)
			{
				if (!MealsByPhilosopher.ContainsKey(p.Name)) MealsByPhilosopher[p.Name] = 0;
				if (!HungryTimeByPhilosopher.ContainsKey(p.Name)) HungryTimeByPhilosopher[p.Name] = 0;
				if (p.State == PhilosopherState.Hungry) HungryTimeByPhilosopher[p.Name]++;
			}
		}

		public void OnMeal(string name)
		{
			if (!MealsByPhilosopher.ContainsKey(name)) MealsByPhilosopher[name] = 0;
			MealsByPhilosopher[name]++;
		}
	}
}


