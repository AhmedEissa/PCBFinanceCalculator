using System;
using System.Linq;

namespace PCBFinanceCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== PCB Car Finance Calculator ===");

            // Input values using LINQ to gather user inputs
            var prompts = new[]
            {
                "Enter the car price: ",
                "Enter the deposit amount: ",
                "Enter the annual interest rate (in %): ",
                "Enter the term length (in months): ",
                "Enter the balloon payment (optional, 0 if none): "
            };

            var values = prompts.Select(prompt =>
            {
                Console.Write(prompt);
                return Convert.ToDouble(Console.ReadLine());
            }).ToArray();

            // Extract input values
            double carPrice = values[0];
            double deposit = values[1];
            double annualInterestRate = values[2] / 100;
            int termLength = (int)values[3];
            double balloonPayment = values[4];

            // Calculating the amount financed (principal)
            double amountFinanced = carPrice - deposit;

            // Monthly interest rate
            double monthlyInterestRate = annualInterestRate / 12;

            // Calculate the present value of the balloon payment
            double presentValueBalloon = balloonPayment / Math.Pow(1 + monthlyInterestRate, termLength);

            // Calculate constant monthly payment using the correct PCP formula
            double monthlyPayment = (amountFinanced - presentValueBalloon) * monthlyInterestRate /
                                    (1 - Math.Pow(1 + monthlyInterestRate, -termLength));

            // Total amount payable including the balloon payment
            double totalAmountPayable = (monthlyPayment * termLength) + balloonPayment;

            // Display results
            Console.WriteLine($"\nMonthly Payment: {monthlyPayment:C2}");
            Console.WriteLine($"Total Amount Payable: {totalAmountPayable:C2}");
        }
    }
}
