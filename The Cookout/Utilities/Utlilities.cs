using System;

namespace The_Cookout
{
	public class Utlilities
	{
		public Utlilities()
		{
		}

        public static string ParseBeforeAtSymbol(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email can not be null or empty.");

            int index = email.IndexOf('@');

            if (index != -1)
                return email.Substring(0, index);
            else
                return email;
        }

    }
}

