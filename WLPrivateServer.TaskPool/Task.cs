using System;

namespace WLPrivateServer.TaskPool
{
	public class Task
	{
		public virtual bool ExecuteNow { get; protected set; } = true;
		public Action UnitOfWork { get; protected set; }
		public Action OnCompleted { get; protected set; }

		public Task(Action unitOfWork, Action onCompleted = null)
		{
			if (onCompleted == null)
				onCompleted = () => { };

			UnitOfWork = unitOfWork ?? throw new ArgumentException("unitOfWork cannot be null.");
		}
	}
}