using System;

using DKL_Validation;
using ValidationResult = DKL_Validation.Results.ValidationResult;

namespace ConsoleAppClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer();
            CustomValidate validate = new CustomValidate();

            Console.Write("Nhap ten khach hang: ");
            customer.Name = Console.ReadLine();
            ValidationResult result = validate.Validate(customer);

            foreach (var results in result.Errors)
            {
                Console.WriteLine("Property " + results.PropertyName + " failed validation. Error was: " + results.ErrorMessage);
            }
        }
    }

    class Customer
    {

        public string Name { get; set; }
        public string Address { get; set; }
    }

    class CustomValidate : AbstractValidation<Customer>
    {
        public CustomValidate()
        {
            RuleFor(Customer => Customer.Name).Must(Name => Name.Length > 10).WithMessage("Chua du 10 ki tu").NotEmpty();
        }
    }
}
