using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ValidationFramework.Results;

namespace ValidationFramework
{
    class Customer
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    class Boss
    {
        public string name { get; set; }
    }

    class CustomerValidation : AbstractValidation<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(Customer => Customer.Address).NotNull().NotEmpty();
            RuleFor(Customer => Customer.Address).Must(Address => Address.Length > 10).WithMessage("Must >10 characters");
        }
    }

    class ProgramTesting
    {
        public static void Main(string[] args)
        {
            string str;
            ValidatorOptions.LanguageManager.Culture = new CultureInfo("vi");
            Console.Write("Nhap 1 chuoi bat ki: ");
            str = Console.ReadLine();
            Customer cus = new Customer();
            Boss boss = new Boss();
            cus.Address = str;
            CustomerValidation validate = new CustomerValidation();
            ValidationResult results = validate.Validate(cus);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors) 
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }
        }
    }
}
