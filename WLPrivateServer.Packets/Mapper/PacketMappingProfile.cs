using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WLPrivateServer.Packets
{
	public class PacketMappingProfile<TSource, TDestination> : IPacketMappingProfile
	{
		private List<PacketMemberMapping<TSource>> mappings = new List<PacketMemberMapping<TSource>>();
		private Func<TSource, TDestination> constructWith = null;
		private Action<TSource, TDestination> withConstructed = null;

		public PacketMappingProfile<TSource, TDestination> ForMember(Expression<Func<TDestination, object>> member,
			Func<PacketMemberMapping<TSource>, PacketMemberMapping<TSource>> memberMapping)
		{
			var memberName = GetMemberName(member);

			mappings.Add(memberMapping(new PacketMemberMapping<TSource>(memberName)));

			return this;
		}

		private string GetMemberName(Expression member)
		{
			if (typeof(LambdaExpression).IsAssignableFrom(member.GetType()))
				return GetMemberName((member as LambdaExpression).Body);
			if (typeof(UnaryExpression).IsAssignableFrom(member.GetType()))
				return GetMemberName((member as UnaryExpression).Operand);
			if (typeof(MemberExpression).IsAssignableFrom(member.GetType()))
				return (member as MemberExpression).Member.Name;
			throw new ArgumentException("Invalid expression.");
		}

		public PacketMappingProfile<TSource, TDestination> ConstructWith(Func<TSource, TDestination> constructWith)
		{
			this.constructWith = constructWith;

			return this;
		}

		public PacketMappingProfile<TSource, TDestination> WithConstructed(Action<TSource, TDestination> withConstructed)
		{
			this.withConstructed = withConstructed;

			return this;
		}

		public object Map(object source, Func<object> destinationFactory)
		{
			TDestination destination;

			if (constructWith != null)
				destination = constructWith((TSource)source);
			else if (destinationFactory != null)
				destination = (TDestination)destinationFactory();
			else
				destination = Activator.CreateInstance<TDestination>();

			withConstructed?.Invoke((TSource)source, destination);

			foreach (var mapping in mappings)
			{
				var prop = typeof(TDestination).GetProperty(mapping.MemberName);

				if (prop != null)
					prop.SetValue(destination, mapping.Mapping.DynamicInvoke(new[] { source }));
				else
					typeof(TDestination).GetField(mapping.MemberName).SetValue(destination, mapping.Mapping.DynamicInvoke(new[] { source }));
			}

			return destination;
		}
	}
}