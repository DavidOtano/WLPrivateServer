using System;
using System.Runtime.Serialization;

namespace WLPrivateServer.Sockets
{
	[Serializable]
	public class SocketClosedException : Exception
	{
		public SocketClosedException()
		{
		}

		public SocketClosedException(string message) : base(message)
		{
		}

		public SocketClosedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected SocketClosedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}