namespace WebApplication10.Exceptions
{
    public class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException(string email)
                        : base($"Пользователь с таким {email} уже существует")
        {
        }
    }
}
