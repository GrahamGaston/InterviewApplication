namespace InterviewProblem.Model
{
    public class User
    {
        public User(string userName, int userId)
        {
            UserName = userName;
            UserId = userId;
        }
        public string UserName { get; set; }
        public int UserId { get; private set; }

    }
}
