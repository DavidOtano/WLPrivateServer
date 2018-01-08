using System;

namespace WLPrivateServer.PacketHandler
{
	public class HandlesAttribute : Attribute
	{
		public int Code { get; private set; }

		public HandlesAttribute(int code)
		{
			Code = code;
		}
	}
}