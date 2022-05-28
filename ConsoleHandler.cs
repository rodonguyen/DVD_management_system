using System;
using System.Collections.Generic;
using System.Text;

class ConsoleHandler
{
    public static void MainMenu()
    {
        Console.Clear();
        DisplayMainMenu();
        string choice = Console.ReadLine();
        bool isValidChoice = CheckChoice(choice, 0, 2);

        while (!isValidChoice)
        {
            Console.Clear();
            Console.WriteLine( "-----------------------------------------------------------------------");
            Console.WriteLine($"  Invalid choice ({choice}): Your choice must be an integer from 0 to 2!");
            Console.WriteLine( "-----------------------------------------------------------------------\n");

            DisplayMainMenu();
            choice = Console.ReadLine();
            isValidChoice = CheckChoice(choice, 0, 2);
        }

        // Handle the valid choice
        switch (int.Parse(choice))
        {
            case 0:
                Environment.Exit(0);
                break;
            case 1:
                Console.Clear();
                StaffLogin();
                break;
            case 2:
                Console.Clear();
                MemberLogin();
                break;
        }
    }


    private static void DisplayMainMenu()
    {
        Console.WriteLine("------------------------------------------------------------------");
        Console.WriteLine("|  Welcome to the Community Library Movie DVD Management System  |");
        Console.WriteLine("------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("=========================== MAIN MENU ============================");
        Console.WriteLine("    1. Staff Login");
        Console.WriteLine("    2. Member Login");
        Console.WriteLine("    0. Exit");
        Console.WriteLine("==================================================================");
        Console.Write  ("\n    Enter your choice (1/2/0) => ");
    }

    private static void StaffLogin()
    {
        Console.WriteLine("============================ Staff Login =============================");
        Console.WriteLine("  Enter staff's username and password                               \n");
        Console.Write("    Username: "); string username = Console.ReadLine();
        Console.Write("    Password: "); string password = Console.ReadLine();

        bool isValidUsername = username == "staff";
        bool isValidPassword = password == "today123";
        bool isGoingBackToMainMenu = username == "0" || password == "0";


        if (isGoingBackToMainMenu) MainMenu();
        while ( !isValidUsername || !isValidPassword) {
            Console.Clear();
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("|       Incorrect login details! Please try again.                   |");
            Console.WriteLine("----------------------------------------------------------------------\n");
            Console.WriteLine("============================ Staff Login =============================");
            Console.WriteLine("  Enter staff's username and password");
            Console.Write("    Username: ");   username = Console.ReadLine();
            Console.Write("    Password: ");   password = Console.ReadLine();

            isValidUsername = username == "staff";
            isValidPassword = password == "today123";
        }

        StaffMenu();
    }

   
    


    private static void StaffMenu()
    {
        Console.Clear();
        DisplayStaffMenu();

        string choice = Console.ReadLine();
        bool isValidChoice = CheckChoice(choice, 0, 6);

        while (!isValidChoice) {
            Console.Clear();
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine($"  Invalid choice ({choice}): Your choice must be an integer from 0 to 6!");
            Console.WriteLine("---------------------------------------------------------------------------\n");
            DisplayStaffMenu();
            Console.Write("\n\n  Enter the staff menu action (1/2/3/4/5/6/0) => ");
            choice = Console.ReadLine();
            isValidChoice = CheckChoice(choice, 0, 6);
        }

        StaffFunctions staffFunctions = new StaffFunctions();

        // Handle the valid choice
        switch (int.Parse(choice)) {
            case 0:
                MainMenu();
                break;
            case 1:
                // Add new DVDs of a movie to the system.
                staffFunctions.AddNewDvD();
                StaffMenu();
                break;
            case 2:
                //Remove DVDs of a movie from the system.
                staffFunctions.DeleteDvD();
                StaffMenu();
                break;
            case 3:
                //Register a new member with the system.
                staffFunctions.AddMember();
                StaffMenu();
                break;
            case 4:
                //Remove a registered member from the system.
                staffFunctions.RemoveMember();
                StaffMenu();
                break;
            case 5:
                //Display a member’s contact phone number, given the member’s full name.
                staffFunctions.DisplayMemberPhoneNumber();
                StaffMenu();
                break;
            case 6:
                //Display a member’s contact phone number, given the member’s full name.
                staffFunctions.PrintBorrowersofMovie();
                StaffMenu();
                break;
        }
    }


    private static void MemberLogin()
    {
        Console.WriteLine("============================ Member Login =============================");
        Console.WriteLine("  Enter your member's first name, last name and password               ");
        Console.Write("    First name: "); string firstname = Console.ReadLine();
        Console.Write("    Last name: ");  string lastname  = Console.ReadLine();
        Console.Write("    Password: ");   string password  = Console.ReadLine(); 

        IMember memberFound = Program.memberCollection.Find(new Member(firstname, lastname));
        bool isValidMember = false;
        if (memberFound != null) {
            isValidMember = memberFound.Pin == password;
        }

        while (!isValidMember)
        {
            Console.Clear();
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("|       Incorrect login details! Please try again.                   |");
            Console.WriteLine("----------------------------------------------------------------------\n");
            Console.WriteLine("============================ Member Login =============================");
            Console.WriteLine("  Enter your member's first name, last name and password               ");
            Console.Write("    First name: "); firstname = Console.ReadLine();
            Console.Write("    Last name: "); lastname = Console.ReadLine();
            Console.Write("    Password: "); password = Console.ReadLine();

            memberFound = Program.memberCollection.Find(new Member(firstname, lastname));
            if (memberFound != null)        {
                isValidMember = memberFound.Pin == password;
            }
        }
        MemberMenu(memberFound);
    }

    private static void MemberMenu(IMember member)
    {
        Console.Clear();
        DisplayMemberMenu();
        string choice = Console.ReadLine();
        bool isValidChoice = CheckChoice(choice, 0, 6);

        while (!isValidChoice) {
            Console.Clear();    
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine($"  Invalid choice ({choice}): Your choice must be an integer from 0 to 6!");
            Console.WriteLine("---------------------------------------------------------------------------\n");
            DisplayMemberMenu();

            choice = Console.ReadLine();
            isValidChoice = CheckChoice(choice, 0, 6);
        }

        // Handle the valid choice
        switch (int.Parse(choice))
        {
            case 0:
                MainMenu();
                break;
            case 1:
                MemberFunctions.ListOfMovies();
                MemberMenu(member);
                break;
            case 2:
                MemberFunctions.DisplayMovieInformation(Program.movieCollection);
                MemberMenu(member);
                break;
            case 3:
                MemberFunctions.BorrowAMovie( member);
                MemberMenu(member);
                break;
            case 4:
                MemberFunctions.ReturnAMovie(member);
                MemberMenu(member);
                break;
            case 5:
                MemberFunctions.DisplayBorrowingMovies(member);
                MemberMenu(member);
                break;
            case 6:
                MemberFunctions.DisplayTop3Movies();
                MemberMenu(member);
                break;
        }
    }
    
    private static void DisplayStaffMenu()
    {
        Console.WriteLine("============================ Staff Menu =============================");
        Console.WriteLine("  1. Add new DVDs of a new movie to the system");
        Console.WriteLine("  2. Remove DVDs of a movie from the system");
        Console.WriteLine("  3. Register a new member with the system");
        Console.WriteLine("  4. Remove a registered member from the system");
        Console.WriteLine("  5. Display a member's contact phone number, given the member's name");
        Console.WriteLine("  6. Display all members who are currently renting a particular movie");
        Console.WriteLine("  0. Return to the main menu");
        Console.WriteLine("=====================================================================");
        Console.Write("\n  Enter your choice (1/2/3/4/5/6/0) => ");
    }




    private static void DisplayMemberMenu()
    {
        Console.WriteLine("============================ Member Menu ==============================");
        Console.WriteLine("  1. Browse all the movies");
        Console.WriteLine("  2. Display all the information about a movie, given the movie's title");
        Console.WriteLine("  3. Borrow a movie DVD");
        Console.WriteLine("  4. Return a movie DVD");
        Console.WriteLine("  5. List current borrowing movies");
        Console.WriteLine("  6. Display the top 3 movies rented by the members");
        Console.WriteLine("  0. Return to the main menu");
        Console.WriteLine("=======================================================================");
        Console.Write("\n  Enter your choice (1/2/3/4/5/6/0) => ");
    }


    public static bool CheckChoice(String inputString, int minValue, int maxValue)
    {
        bool isInt = int.TryParse(inputString, out int num);
        if (isInt) {
            bool isValidChoice = num <= maxValue && num >= minValue;
            return isValidChoice;
        }
        else
            return false;
    }
}