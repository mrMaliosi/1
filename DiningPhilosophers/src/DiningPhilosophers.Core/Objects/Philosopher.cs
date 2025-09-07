using System;

namespace Lab1.DiningPhilosophers
{

	public enum PhilosopherState { 
		Thinking, 
		Hungry, 
		Eating 
	}

	public enum ActionType
	{
		None,
		TakeLeftFork,
		TakeRightFork,
		ReleaseLeftFork,
		ReleaseRightFork
	}

    /// <summary>
    /// Модель философа в задаче об обедающих философах.
    /// </summary>
    public sealed class Philosopher
    {
        //базовые поля
        public string Name { get; }
        public PhilosopherState State { get; private set; } = PhilosopherState.Thinking;
        public PhilosopherContext Context { get; }

        //вилки
        private readonly Fork _left;
        private readonly Fork _right;

        //поля состояния
        public ActionType LastAction { get; private set; } = ActionType.None;
        public int StepsRemainingInState { get; private set; } = 0;
        private bool _hasLeft;
        private bool _hasRight;

        //статистика
        public int MealsEaten { get; private set; } = 0;

        public Philosopher(string name, Fork left, Fork right, PhilosopherContext context)
        {
            Name = name;
            _left = left;
            _right = right;
            _hasLeft = false;
            _hasRight = false;
            Context = context;
        }
    }
}


