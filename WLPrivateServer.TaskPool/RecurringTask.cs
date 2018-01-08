using System;

namespace WLPrivateServer.TaskPool
{
	public class RecurringTask : Task
	{
		private DateTime? executionTime;
		private TimeSpan interval;

		public override bool ExecuteNow
		{
			get
			{
				return !executionTime.HasValue || (DateTime.Now - executionTime.Value) >= interval;
			}
			protected set { throw new NotImplementedException(); }
		}

		public RecurringTask(Action unitOfWork, TimeSpan interval)
			: base(() => { })
		{
			this.interval = interval;

			UnitOfWork = () =>
			{
				executionTime = DateTime.Now;

				unitOfWork();
			};

			OnCompleted = () => TaskPoolManager.Enqueue(this);
		}
	}
}