using System;
using System.Runtime.Serialization;

namespace WLPrivateServer.Sockets
{
	[Serializable]
	internal class InvalidReceiveDataException : Exception
	{
		public InvalidReceiveDataException()
		{
		}

		public InvalidReceiveDataException(string message) : base(message)
		{
		}

		public InvalidReceiveDataException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected InvalidReceiveDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}