using System;
using System.Collections.Generic;
using SimpleValidation.Model;
using SimpleValidation.Validation;
using SimpleValidation.Validation.Validator;

namespace SimpleValidation
{
    public class MyChangeManager : ChangeManager
    {

    }


    class Program
    {
        static void Main(string[] args)
        {
            MyChangeManager changeManager = new MyChangeManager();
            changeManager.AddCondition(new NotEmptyGuard(UserProxy.ATTRIBUTE_NAME));
            changeManager.AddCondition(new NumberGuard(UserProxy.ATTRIBUTE_ID, 1612300, NumberGuard.Comparator.EQUAL).SetWarningMessage("This ID is wrong!"));
            changeManager.AddCondition(new NumberGuard(UserProxy.ATTRIBUTE_ID, 1612315, NumberGuard.Comparator.SMALLER).SetWarningMessage("This ID is too big!"));

            UserProxy userProxy = new UserProxy(new User());
            userProxy.SetChangeManager(changeManager);
            
            Reporter r = userProxy.SetName("Thai Dang Khoa")
                .AddOnSuccessListener(new MyListener())
                .AddOnFailListener(new MyListener());
            r.Report();

            r = userProxy.SetID(1612315)
                .AddOnSuccessListener(new MyListener())
                .AddOnFailListener(new MyListener());
            r.Report();

            // if dont like those reports, simply use this:
            bool result = userProxy.SetID(1612300).GetResult();

            Console.WriteLine("Result: " + userProxy.GetID() + "   " + userProxy.GetName());
        }

    }

    class MyListener : OnSuccessListener, OnFailListener
    {
        public void Do()
        {
            Console.WriteLine("Nice!");
        }

        public void Do(List<string> messages)
        {
            Console.WriteLine("Warning:");
            messages.ForEach(message => Console.WriteLine(" " + message));
        }
    }

}
