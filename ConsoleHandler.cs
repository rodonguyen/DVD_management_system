using System;
using System.Collections.Generic;
using System.Text;

class ConsoleHandler
{
    public static void MainMenu()
    {
        Console.Clear();
        DisplayMainMenu();
        String choice = Console.ReadLine();
        bool isValidChoice = CheckChoice(choice, 0, 2);

        while (!isValidChoice)
        {
            Console.Clear();

            Console.WriteLine( "-----------------------------------------------------------------------");
            Console.WriteLine($"  Invalid choice ({choice}): Your choice must be an integer from 0 to 2!");
            Console.WriteLine( "-----------------------------------------------------------------------\n");

            DisplayMainMenu();
            //Console.CursorLeft -= 25;  // Move cursor back, near the arrow
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
        Console.WriteLine("Welcome to the Community Library Movie DVD Management System");
        Console.WriteLine("======================== MAIN MENU =========================");
        Console.WriteLine("  1. Staff Login");
        Console.WriteLine("  2. Member Login");
        Console.WriteLine("  0. Exit");
        Console.WriteLine("============================================================");
        Console.Write  ("\n  Enter your choice (1/2/0) => ");
    }

    private static void StaffLogin()
    {
        Console.WriteLine("============================ Staff Login =============================");
        Console.WriteLine("  Enter staff's username and password                               \n");
        Console.Write("    Username: ");      String username = Console.ReadLine();
        Console.Write("    Password: ");      String password = Console.ReadLine();

        bool isValidUsername = username == "staff";
        bool isValidPassword = password == "today123";
        bool isGoingBackToMainMenu = username == "0" || password == "0";


        if (isGoingBackToMainMenu) MainMenu();
        while ( !isValidUsername || !isValidPassword) {
            Console.Clear();
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("|       Incorrect login details! Please try again.                   |");
            //Console.WriteLine("|       Or enter 0 to either section to return to Main Menu.         |");
            Console.WriteLine("----------------------------------------------------------------------\n");
            Console.WriteLine("============================ Staff Login =============================");
            Console.WriteLine("  Enter staff's username and password");
            Console.Write("    Username: ");       username = Console.ReadLine();
            Console.Write("    Password: ");       password = Console.ReadLine();

            isValidUsername = username == "staff";
            isValidPassword = password == "today123";

            //isGoingBackToMainMenu = username == "0" || password == "0";
            //if (isGoingBackToMainMenu) MainMenu();
        }
        Console.Clear();
        DisplayStaffMenu();
        StaffMenu();
    }
    /*

    public static MovieGenre SelectMovieGenre()
    {
        DisplaySelectMovieGenre();
        Console.Write("\n\n  Enter your choice (1/2/3/4/5) => ");

        string genre = Console.ReadLine();
        bool isGenreValid = CheckChoice(genre, 1, 5);

        while (!isGenreValid)
        {
            
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine($"  Invalid choice ({genre}): Your choice must be an integer from 1 to 5!");
            Console.WriteLine("---------------------------------------------------------------------------\n");

            Console.Write("\n Enter your choice (1/2/3/4/5) => ");
            genre = Console.ReadLine();
            isGenreValid = CheckChoice(genre, 1, 5);
        }


        return (MovieGenre)Convert.ToInt32(genre);
    }

    public static MovieClassification SelectMovieClassification()
    {
        DisplaySelectMovieClassification();
        Console.Write("\n\n  Enter your choice (1/2/3/4) => ");

        string classification = Console.ReadLine();
        bool isGenreValid = CheckChoice(classification, 1, 4);

        while (!isGenreValid)
        {
            
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine($"  Invalid choice ({classification}): Your choice must be an integer from 1 to 4!");
            Console.WriteLine("---------------------------------------------------------------------------\n");

            Console.Write("\n Enter your choice (1/2/3/4) => ");
            classification = Console.ReadLine();
            isGenreValid = CheckChoice(classification, 1, 4);
        }

        return (MovieClassification)Convert.ToInt32(classification);
    }



    public static void DisplaySelectMovieGenre()
    {

        Console.WriteLine("\n Select the movie genre:  => ");
        Console.WriteLine("  1. Action");
        Console.WriteLine("  2. Comedy");
        Console.WriteLine("  3. History");
        Console.WriteLine("  4. Drama");
        Console.WriteLine("  5. Western");
        Console.WriteLine("=====================================================================");


    }
    public static void DisplaySelectMovieClassification()
    {
        Console.WriteLine("\n Select the movie classification:  => ");
        Console.WriteLine("  1. G");
        Console.WriteLine("  2. PG");
        Console.WriteLine("  3. M");
        Console.WriteLine("  4. M15Plus");
        Console.WriteLine("=====================================================================");

    }
    */


    private static void StaffMenu()
    {
        Console.Write("\n\n  Enter the staff menu action (1/2/3/4/5/6/0) => ");

        String choice = Console.ReadLine();
        bool isValidChoice = CheckChoice(choice, 0, 6);

        while (!isValidChoice) {

            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine($"  Invalid choice ({choice}): Your choice must be an integer from 0 to 6!");
            Console.WriteLine("---------------------------------------------------------------------------\n");

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
                staffFunctions.AddNewDvD(Program.movieCollection);
                StaffMenu();
                break;
            case 2:
                //Remove DVDs of a movie from the system.
                staffFunctions.DeleteDvD(Program.movieCollection);
                StaffMenu();
                break;
            case 3:
                //Register a new member with the system.
                staffFunctions.AddMember(Program.memberCollection);
                StaffMenu();
                break;
            case 4:
                //Remove a registered member from the system.
                staffFunctions.RemoveMember(Program.memberCollection);
                StaffMenu();
                break;
            case 5:
                //Display a member’s contact phone number, given the member’s full name.
                staffFunctions.DisplayMemberPhoneNumber(Program.memberCollection);
                StaffMenu();
                break;
            case 6:
                //Display a member’s contact phone number, given the member’s full name.
                Console.WriteLine("To be implemented...");
                break;
        }
    }


    private static void MemberLogin()
    {
        Console.WriteLine("============================ Member Login =============================");
        Console.WriteLine("  Enter your member's first name, last name and password               ");
        Console.Write("    First name: ");     String firstname = Console.ReadLine();
        Console.Write("    Last name: ");      String lastname  = Console.ReadLine();
        Console.Write("    Password: ");       String password  = Console.ReadLine(); 

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

        MemberMenu();
    }

    private static void MemberMenu()
    {
        Console.Clear();


        String choice = Console.ReadLine();
        bool isValidChoice = CheckChoice(choice, 0, 6);

        while (!isValidChoice) {
            
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine($"  Invalid choice ({choice}): Your choice must be an integer from 0 to 6!");
            Console.WriteLine("---------------------------------------------------------------------------\n");

            
            choice = Console.ReadLine();
            isValidChoice = CheckChoice(choice, 0, 6);
        }

        

        // Handle the valid choice
        switch (int.Parse(choice)) {
            case 0:
                MainMenu();
                break;
            case 1:
                Console.WriteLine("To be implemented...");
                break;
            case 2:
                Console.WriteLine("To be implemented...");
                break;
            case 3:
                Console.WriteLine("To be implemented...");
                break;
            case 4:
                Console.WriteLine("To be implemented...");
                break;
            case 5:
                Console.WriteLine("To be implemented...");
                break;
            case 6:
                Console.WriteLine("To be implemented...");
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
        Console.Write("\n\n  Enter your choice (1/2/3/4/5/6/0) => ");
    }


    private static bool CheckChoice(String inputString, int minValue, int maxValue)
    {
        try {
            int input = int.Parse(inputString);
            bool isValidChoice = input <= maxValue && input >= minValue;
            return isValidChoice;
        } catch {
            return false;
        }
    }

    //private static bool CheckInput(String inputString, int maxValue)
    //{
    //    int input;
    //    try {
    //        input = int.Parse(inputString);
    //    }
    //    catch (Exception e) { 
    //        Console.WriteLine($"\nInvalid choice ({inputString}): Your choice must be from 0 to {maxValue} !!!");
    //        return false;
    //    }

    //    bool isValidChoice = input <= maxValue  &&  input >= 0;
    //    if (!isValidChoice)
    //        Console.WriteLine($"\nInvalid choice ({inputString}): Your choice must be from 0 to {maxValue} !!!");

    //    return isValidChoice;
    //    }
}