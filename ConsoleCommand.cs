using System;
using System.Collections.Generic;
using System.Text;

class ConsoleCommand
{
    static void Main()
    {
        bool isValidChoice = false;
        String input = "";

        while (!isValidChoice)
        {
            Console.Clear();
            DisplayMainMenu();
            input = Console.ReadLine();
            isValidChoice = CheckInput(input, 2);
        }

        // Handle the valid choice
        switch (int.Parse(input))
        {
            case 0:
                System.Environment.Exit(0);
                break;
            case 1:
                ConsoleCommand.StaffLogin();
                break;
            case 2:
                ConsoleCommand.MemberLogin();
                break;
        }


    }



    private static void DisplayMainMenu()
    {
        Console.WriteLine("Welcome to the Community Library Movie DVD Management System");
        Console.WriteLine("======================== Main Menu =========================");
        Console.WriteLine("  1. Staff Login");
        Console.WriteLine("  2. Member Login");
        Console.WriteLine("  0. Exit");
        Console.WriteLine("============================================================");
        Console.Write("Make a selection (0 / 1 / 2): ");
    }



    private static void StaffLogin()
    {

    }

    private static void MemberLogin()
    {

    }



    private static bool CheckInput(String inputString, int maxValue)
    {
        int input;
        try {
            input = int.Parse(inputString);
        }
        catch (Exception e) { 
            Console.WriteLine($"Your choice is not a valid number, must be from 0 to {maxValue}\n" + e);
            return false;
        }

        bool isValidChoice = input <= maxValue  &&  input >= 0;
        if (!isValidChoice)
            Console.WriteLine($"Your choice must be from 0 to {maxValue}");

        return isValidChoice;
        }
}