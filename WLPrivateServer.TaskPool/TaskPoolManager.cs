using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace WLPrivateServer.TaskPool
{
	public static class TaskPoolManager
	{
		private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
		public static ConcurrentQueue<Task> taskQueue = new ConcurrentQueue<Task>();
		public static List<Thread> threads = new List<Thread>();

		static TaskPoolManager()
		{
			for (var i = 0; i < Environment.ProcessorCount; ++i)
			{
				var thread = new Thread(ThreadProc, 0)
				{
					Name = $"TaskPool_Thread_{ i }"
				};

				threads.Add(thread);

				thread.Start();
			}
		}

		public static void ThreadProc()
		{
			while (!cancellationTokenSource.IsCancellationRequested)
			{
				Task task;

				if (taskQueue.Count > 0)
				{
					if (!taskQueue.TryDequeue(out task))
					{
						Thread.Yield();
						Thread.Sleep(1);

						continue;
					}

					if (task.ExecuteNow)
					{
						task.UnitOfWork();
						task.OnCompleted();
					}
					else
						Enqueue(task);
				}
				else
					Thread.Yield();

				Thread.Sleep(1);
			}
		}

		public static void Stop()
		{
			cancellationTokenSource.Cancel();

			foreach (var thread in threads.ToList())
			{
				thread.Join();
				threads.Remove(thread);
			}
		}

		public static void Enqueue(Task task)
		{
			taskQueue.Enqueue(task);
		}
	}
}