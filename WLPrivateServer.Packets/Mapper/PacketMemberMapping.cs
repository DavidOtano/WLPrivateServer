using System;
using System.Linq.Expressions;

namespace WLPrivateServer.Packets
{
	public class PacketMemberMapping<T>
	{
		public string MemberName { get; private set; }
		public Delegate Mapping { get; private set; }

		public PacketMemberMapping(string memberName)
		{
			MemberName = memberName;
		}

		public PacketMemberMapping<T> MapFrom(Func<T, object> mapping)
		{
			Mapping = mapping;

			return this;
		}

		public PacketMemberMapping<T> Ignore()
		{
			Expression<Func<T, object>> expr = ctx => ctx.GetType().GetProperty(MemberName).GetValue(ctx);

			Mapping = expr.Compile();

			return this;
		}
	}
}