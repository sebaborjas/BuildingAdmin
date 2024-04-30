using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using Domain.DataTypes;
using Domain.Exceptions;

namespace Domain
{
    public class Invitation
    {
        private int _id;

        private string _email;

        private DateTime _expirationDate;

        public InvitationStatus Status { get; set; }

        public string Name { get { return "Test"; } set { 
            if (value == "")
                {
                throw new System.ArgumentNullException(); 
                }
            } }

        public int Id
        {
            get => _id;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException();

                _id = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException();

                string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";

                bool correctEmail = IsValidFormat(pattern, value);

                if (!correctEmail) throw new WrongEmailFormatException();

                _email = value;
            }
        }

        public DateTime ExpirationDate
        {
            get => _expirationDate;
            set
            {
                if (value < DateTime.Now.Date) throw new ArgumentOutOfRangeException();

                _expirationDate = value;
            }
        }

        private bool IsValidFormat(string pattern, string value)
        {
            Regex regex = new(pattern, RegexOptions.IgnoreCase);

            return regex.IsMatch(value);
        }
    }
}