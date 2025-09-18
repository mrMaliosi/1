using System;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

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
		TakeFork,
		ReleaseFork
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
        private Fork[] _forks;
        private int _forks_remain;

        //поля состояния
        public ActionType LastAction { get; private set; } = ActionType.None;
        public int StepsRemainingInState { get; private set; } = 0;

        //статистика
        public int MealsEaten { get; private set; } = 0;

        public Philosopher(string name, Fork[] forks, PhilosopherContext context)
        {
            Name = name;
            _forks = forks;
            Context = context;
            StepsRemainingInState = Context.GetThinkingDuration();
            _forks_remain = forks.Length;
        }

        public bool PickFork(int forkNumber) 
        {
            if (_forks[forkNumber].TryAcquire(this))
            {
                --_forks_remain;
                StepsRemainingInState = Context.GetForkPickDuration();
                LastAction = ActionType.TakeFork;
                return true;
            }
            return false;
        }

        public int GetForksNum() 
        {
            return _forks.Length;
        }

        // Осознать тщетность бытия - отпустить вилки
        public void RealizeFutilityOfBeing() {
            foreach (var fork in _forks)
            {
                fork.Release();
            }
            _forks_remain = GetForksNum();
            LastAction = ActionType.ReleaseFork;
        }

        private void ChangeState() {
            switch (State)
            {
                case PhilosopherState.Thinking:
                    State = PhilosopherState.Hungry;
                    break;
                case PhilosopherState.Hungry:
                    if (_forks_remain == 0) 
                    {
                        State = PhilosopherState.Eating;
                        StepsRemainingInState = Context.GetEatingDuration();
                    }
                    break;
                case PhilosopherState.Eating:
                    State = PhilosopherState.Thinking;
                    RealizeFutilityOfBeing();
                    StepsRemainingInState = Context.GetThinkingDuration();
                    ++MealsEaten;
                    break;
            }
        }

        // Осознать течение времени
        public void RealiseThePassageOfTime() 
        {
            --StepsRemainingInState;
            if (StepsRemainingInState == 0) ChangeState();
        }

        public bool IsBusy() 
        {
            return StepsRemainingInState > 0;
        }
    }
}


