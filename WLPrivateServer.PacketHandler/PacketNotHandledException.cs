using System;
using System.Runtime.Serialization;

namespace WLPrivateServer.PacketHandler
{
	[Serializable]
	internal class PacketNotHandledException : Exception
	{
		public PacketNotHandledException()
		{
		}

		public PacketNotHandledException(string message) : base(message)
		{
		}

		public PacketNotHandledException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected PacketNotHandledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}