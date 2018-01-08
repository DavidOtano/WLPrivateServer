using AutoMapper;
using System;
using WLPrivateServer.DataAccessLayer;
using WLPrivateServer.TaskPool;

namespace WLPrivateServer.Repository
{
	public static class DataRepository
	{
		private static readonly object syncRoot = new object();

		static DataRepository()
		{
			TaskPoolManager.Enqueue(new RecurringTask(() =>
			{
				lock (syncRoot)
				{
					DataContext.Save();
				}
			}, TimeSpan.FromMinutes(5)));
		}

		public static Users.User GetUser(string username, string password)
		{
			var user = DataContext.GetUser(username, password);

			if (user == null)
				throw new UserNotFoundException();

			return Mapper.Map<Users.User>(user);
		}

		public static void SubmitChanges(Users.User user)
		{
			var model = DataContext.GetUser(user.Id);

			if (model == null)
				throw new UserNotFoundException();

			Mapper.Map(user, model);
		}
	}
}