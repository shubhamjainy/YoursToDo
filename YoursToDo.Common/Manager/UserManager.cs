namespace YoursToDo.Common.Manager
{
    public sealed class UserManager
    {
        private static UserManager? instance;
        private static readonly object lockObj = new();

        public string Name { get; private set; }
        public string Email { get; private set; }
        public int UserId { get; private set; }

        public static UserManager Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance is null)
                    {
                        instance = new UserManager();
                    }
                    return instance;
                }
            }
        }
        private UserManager()
        {

        }

        public void SetUserData(string userName, string email, int userId)
        {
            this.Name = userName;
            this.Email = email;
            this.UserId = userId;
        }
    }
}