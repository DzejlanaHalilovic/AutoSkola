﻿namespace AutoSkola.Exceptions
{
    public class LoginCustomException : Exception
    {
        public LoginCustomException() : base() { }


        public LoginCustomException(string? message) : base(message)
        {
        }
    }
}
