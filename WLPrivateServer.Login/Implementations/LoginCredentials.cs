namespace WLPrivateServer.Login.Implementations
{
	public class LoginCredentials
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public ushort ClientVersion { get; set; }
		public string ItemDataFileLength { get; set; }
	}
}