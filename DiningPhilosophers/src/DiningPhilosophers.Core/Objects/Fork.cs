namespace Lab1.DiningPhilosophers
{
	public enum ForkState { Available, InUse }

	public sealed class Fork
	{
		public int Id { get; }
		public ForkState State { get; private set; } = ForkState.Available;
		public string? HeldBy { get; private set; }

		public Fork(int id) { Id = id; }

		public bool TryAcquire(string philosopherName)
		{
			if (State == ForkState.Available)
			{
				State = ForkState.InUse;
				HeldBy = philosopherName;
				return true;
			}
			return false;
		}

		public void Release()
		{
			State = ForkState.Available;
			HeldBy = null;
		}
	}
}


