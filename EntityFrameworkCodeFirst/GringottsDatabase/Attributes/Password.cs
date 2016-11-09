using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GringottsDatabase.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Password : ValidationAttribute
    {
        private int minLenght;
        private int maxLenght;
        public Password(int minLenght, int maxLenght)
        {
            this.MinLenght = minLenght;
            this.MaxLenght = maxLenght;
        }

        private int MinLenght
        {
            get { return this.minLenght; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("the input value connot be negative");
                }

                this.minLenght = value;
            }
        }

        private int MaxLenght { get; set; }

        public virtual bool ContainsLowwerCase { get; set; }

        public virtual bool ContainsUpperCase { get; set; }
        public virtual bool ContainsDigit { get; set; }
        public virtual bool ContainsSpecialSymbol { get; set; }
        public virtual bool ErrorMessage { get; set; }

        private void ThrowError()
        {
            throw new ArgumentOutOfRangeException(this.ErrorMessage.ToString());
        }

        public override bool IsValid(object value)
        {
            string stringValue = (string)value;

            if (string.IsNullOrEmpty(stringValue))
            {
                if (ErrorMessage)
                {
                    ThrowError();
                }

                return false;
            }

            if (stringValue.Length<this.minLenght || stringValue.Length>this.maxLenght)
            {
                if (ErrorMessage)
                {
                    ThrowError();
                }

                return false;
            }
            
            if (ContainsDigit && !Regex.Match(stringValue, "[\\d+]", RegexOptions.ECMAScript).Success)
            {
                if (ErrorMessage)
                {
                    ThrowError();
                }

                return false;
            }
            if (ContainsLowwerCase && !Regex.Match(stringValue, "[a-z]", RegexOptions.ECMAScript).Success)
            {
                if (ErrorMessage)
                {
                    ThrowError();
                }

                return false;
            }
            if (ContainsUpperCase && !Regex.Match(stringValue, "[A-Z]", RegexOptions.ECMAScript).Success)
            {
                if (ErrorMessage)
                {
                    ThrowError();
                }

                return false;
            }
            if (ContainsSpecialSymbol && !Regex.Match(stringValue, "[!, @, #, $, %, ^, &, *, (, ), _, +, <, >, ?]", RegexOptions.ECMAScript).Success)
            {
                if (ErrorMessage)
                {
                    ThrowError();
                }

                return false;
            }

            return true;

        }
    }
}