using System;
using System.ComponentModel.DataAnnotations;

namespace GringottsDatabase.Models
{
    public class WizardDeposits
    {
        private int id;
        private string firstName;
        private string lastName;
        private string notes;
        private int age;
        private string magicWandCreator;
        private int magicWandSize;
        private string depositGroup;
        private DateTime depositStartDate;
        private DateTime depositExpirationDate;
        private decimal depositAmount;
        private double depositCharge;
        private bool isDepositeExpired;

        public WizardDeposits()
        {
        }

        [Key]
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        [MaxLength(50)]
        public string FirstName
        {
            get { return this.firstName; }
            set { this.firstName = value; }
        }

        [MaxLength(60)]
        public string LastName
        {
            get { return this.lastName; }
            set { this.lastName = value; }
        }

        [MaxLength(1000)]
        public string Notes
        {
            get { return this.notes; }
            set { this.notes = value; }
        }

        [Required]
        public int Age
        {
            get { return this.age; }
            set
            {
                if (value<0)
                {
                    throw new ArgumentOutOfRangeException("The age cannot be negatve");
                }
                this.age = value;
            }
        }

        [MaxLength(100)]
        public string MagicWandCreator
        {
            get { return this.magicWandCreator; }
            set { this.magicWandCreator = value; }
        }

        [Range(1,32767)]
        public int MagicWandSize
        {
            get { return this.magicWandSize; }
            set { this.magicWandSize = value; }
        }

        [MaxLength(20)]
        public string DepositGroup
        {
            get { return this.depositGroup; }
            set { this.depositGroup = value; }
        }

        public DateTime DepositStartDate
        {
            get { return this.depositStartDate; }
            set { this.depositStartDate = value; }
        }

        public DateTime DepositExpirationDate
        {
            get { return this.depositExpirationDate; }
            set { this.depositExpirationDate = value; }
        }

        public decimal DepositAmount
        {
            get { return this.depositAmount; }
            set { this.depositAmount = value; }
        }

        public double DepositCharge
        {
            get { return this.depositCharge; }
            set { this.depositCharge = value; }
        }

        public bool IsDepositeExpired
        {
            get { return this.isDepositeExpired; }
            set { this.isDepositeExpired = value; }
        }
    }
}