﻿using System;
using System.Collections.Generic;
using System.Text;

public class StaffFunctions
{
    // ------------------------------------- Assistive Functions ------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    /// <param name="">  </param>
    /// <returns>  </returns>
    private static bool CheckInteger(String input, bool checkMinValue = false, int minValue = 1)
    {
        bool isInt = int.TryParse(input, out int ouput);
        if (checkMinValue) isInt = isInt && ouput >= minValue;
        return isInt;
    }

    //private static string CheckString(String input)
    //{
    //    int num;
    //    bool isInt = int.TryParse(input, out num);
    //    while (isInt)    {
    //        Console.WriteLine("---------------------------------------------------------------------------");
    //        Console.WriteLine($"  Invalid input ({input}): Please enter a word.");
    //        Console.WriteLine("---------------------------------------------------------------------------\n");
    //        Console.Write("=> ");
    //        input = Console.ReadLine();
    //        isInt = int.TryParse(input, out num);
    //    }
    //    return input;
    //}

    private static MovieGenre SelectMovieGenre()
    {
        DisplaySelectMovieGenre();

        string genre = Console.ReadLine();
        bool isGenreValid = ConsoleHandler.CheckChoice(genre, 1, 5);

        while (!isGenreValid) {
            Console.WriteLine("\n  !!!!!");
            Console.WriteLine($"  Invalid choice ({genre}): Your choice must be an integer from 1 to 5!");
            Console.Write("  Re-enter your choice (1/2/3/4/5) => ");
            genre = Console.ReadLine();
            isGenreValid = ConsoleHandler.CheckChoice(genre, 1, 5);
        }
        return (MovieGenre)Convert.ToInt32(genre);
    }

    private static MovieClassification SelectMovieClassification()
    {
        DisplaySelectMovieClassification();
        string classification = Console.ReadLine();
        bool isValidGenre = ConsoleHandler.CheckChoice(classification, 1, 4);

        while (!isValidGenre) {
            Console.WriteLine("\n  !!!!!");
            Console.WriteLine($"  Invalid choice ({classification}). Must enter an integer from 1 to 4!");
            Console.Write("  Re-enter your choice (1/2/3/4) => ");
            classification = Console.ReadLine();
            isValidGenre = ConsoleHandler.CheckChoice(classification, 1, 4);
        }
        return (MovieClassification)Convert.ToInt32(classification);
    }


    private static int EnterMovieDuration()
    {
        Console.WriteLine("\n------------------------------------------------");
        Console.Write("  Enter movie duration  =>  ");
        string duration = Console.ReadLine();
        bool isValidDuration = CheckInteger(duration, true);

        while (!isValidDuration)
        {
            Console.WriteLine("\n  !!!!!");
            Console.WriteLine($"  Invalid duration ({duration}). Please enter a positive integer.");
            Console.Write("  Re-enter duration   =>  ");
            duration = Console.ReadLine();
            isValidDuration = CheckInteger(duration, true);
        }
        return int.Parse(duration);
    }

    private static int EnterMovieCopies(string prompt) {
        Console.WriteLine("\n------------------------------------------------");
        Console.Write($"  {prompt}  =>  ");
        string copiesNum = Console.ReadLine();
        bool isValidCopiesNum = CheckInteger(copiesNum, true);

        while (!isValidCopiesNum)
        {
            Console.WriteLine("\n  !!!!!");
            Console.WriteLine($"  Invalid number of copies ({copiesNum}). Please enter a positive integer.");

            Console.Write("  Re-enter the number of copies  =>  ");
            copiesNum = Console.ReadLine();
            isValidCopiesNum = CheckInteger(copiesNum, true);
        }
        return int.Parse(copiesNum);
    }


    private static void DisplaySelectMovieGenre() {
        Console.WriteLine("\n------------------------------------------------");
        Console.WriteLine("  Select the movie genre:");
        Console.WriteLine("    1. Action");
        Console.WriteLine("    2. Comedy");
        Console.WriteLine("    3. History");
        Console.WriteLine("    4. Drama");
        Console.WriteLine("    5. Western");
        Console.Write("  Enter your choice (1/2/3/4/5)  =>  ");
    }
    private static void DisplaySelectMovieClassification() {
        Console.WriteLine("\n------------------------------------------------");
        Console.WriteLine("  Select the movie classification:");
        Console.WriteLine("    1. G");
        Console.WriteLine("    2. PG");
        Console.WriteLine("    3. M");
        Console.WriteLine("    4. M15Plus");
        Console.Write("  Enter your choice (1/2/3/4) => ");
    }



    // -------------------------- Main Console Staff Function ---------------------------

    public void AddNewDvD() {
        Console.Clear();
        Console.WriteLine("================================================");
        Console.WriteLine("             Add New Movie DVDs");
        Console.WriteLine("================================================");
        Console.Write("\n  Enter the movie title  => ");
        string movie = Console.ReadLine();

        IMovie iMovie = Program.movieCollection.Search(movie);

        //If the movie is new
        if (iMovie == null)
        {
            Console.WriteLine("\n  You are adding a new movie!");
            Console.WriteLine("  Please add a few more details about this movie");

            MovieGenre genre = SelectMovieGenre();
            MovieClassification classification = SelectMovieClassification();
            int duration = EnterMovieDuration();
            int numCopies = EnterMovieCopies("Enter the number of copies");

            Program.movieCollection.Insert(new Movie(movie, genre, classification, duration, numCopies));
            Console.WriteLine( $"\n  The movie ({movie}) was added to the database!");
        }
        else // If the movie is existed
        {
            int numCopies = EnterMovieCopies("Enter the number of new DvDs to add");
            
            iMovie.TotalCopies += numCopies;
            iMovie.AvailableCopies += numCopies;

            Console.WriteLine($"\n  The number of DVDS is updated (Total copies: {iMovie.TotalCopies}).");
        }

        Console.WriteLine("\n================================================");
        Console.Write("  Press enter to return to staff menu...");
        Console.ReadLine();
    }

    public void DeleteDvD()
    {
        Console.Clear();
        Console.WriteLine("================================================");
        Console.WriteLine("                Remove DVD");
        Console.WriteLine("================================================");
        Console.Write("\n  Enter the movie title  => ");
        string movie = Console.ReadLine();

        IMovie searchResult = Program.movieCollection.Search(movie);

        
        if (searchResult == null) {
            Console.WriteLine("\n  DVD copies of {0} cannot be deleted ", movie);
            Console.WriteLine("  because that movie is not in the database");
        }
            
        else {
            int numCopies = EnterMovieCopies("Enter the number of DVD copies to remove");

            
            if (searchResult.AvailableCopies < numCopies)
            {
                Console.WriteLine("\n  Cannot remove {0} DVD copy/ies of the movie.", numCopies);
                Console.WriteLine("  There is not enough copies available to remove.");
            }

            else
            {
                searchResult.TotalCopies -= numCopies;
                searchResult.AvailableCopies -= numCopies;

                if (searchResult.TotalCopies > 0)
                {
                    Console.Write("\n  {0} DVD copy/ies of the movie are removed.", numCopies);
                }
                if (searchResult.TotalCopies <= 0)
                {
                    Program.movieCollection.Delete(searchResult);
                    Console.Write("\n  All the DVD copies of {0} are removed. \n  The movie is deleted from the database", movie);
                }
            }           
        }
        
        Console.WriteLine("\n================================================");
        Console.Write("  Press Enter to return to staff menu...");
        Console.ReadLine();
    }

    public void AddMember()
    {
        Console.Clear();
        Console.WriteLine("================================================");
        Console.WriteLine("                  Add Member");
        Console.WriteLine("================================================");
        
        Console.Write("\n  Enter the member’s first name  =>  ");
        string firstName = Console.ReadLine();

        Console.WriteLine("\n------------------------------------------------");
        Console.Write("  Enter the member’s last name  => ");
        string lastName = Console.ReadLine();

        Console.WriteLine("\n------------------------------------------------");
        Console.Write("  Enter the member’s contact phone number  => ");
        string phone = Console.ReadLine();
        bool isValidContactNumber = IMember.IsValidContactNumber(phone);
        while (!isValidContactNumber)
        {
            Console.WriteLine("\n  !!!!!");
            Console.WriteLine($"  Invalid input ({phone}). Please enter a valid phone number.");
            Console.Write("  Phone number  =>  ");

            phone = Console.ReadLine();
            isValidContactNumber = IMember.IsValidContactNumber(phone);
        }

        Console.WriteLine("\n------------------------------------------------");
        Console.Write("  Enter the member’s PIN  => ");
        string pin = Console.ReadLine();
        bool validPin = IMember.IsValidPin(pin);
        while (!validPin)
        {
            Console.WriteLine("\n  !!!!!");
            Console.WriteLine($"  Invalid input ({pin}): Please enter a valid PIN number.");
            Console.Write("  PIN number  =>  ");

            pin = Console.ReadLine();
            validPin = IMember.IsValidPin(pin);
        }

        Console.WriteLine("\n------------------------------------------------");
        IMember newMember = new Member(firstName, lastName, phone, pin);
        Program.memberCollection.Add(newMember);
        Console.WriteLine("  Done!");

        Console.WriteLine("\n================================================");
        Console.Write("  Press Enter to return to staff menu...");
        Console.ReadLine();

    }

    public void RemoveMember()
    {
        Console.Clear();
        Console.WriteLine("================================================");
        Console.WriteLine("                  Remove Member");
        Console.WriteLine("================================================");

        Console.Write("\n  Enter the member’s fist name  =>  ");
        string firstName = Console.ReadLine();
        Console.Write("  Enter the member’s last name  =>  ");
        string lastName = Console.ReadLine();

        Member toDeleteMember = new Member(firstName, lastName);
        bool request = true;

        // Check if member is borrowing any DVD
        foreach (Movie movie in Program.movieCollection.ToArray())     {
            if (movie.Borrowers.Search(toDeleteMember))    {
                request = false;
                break;
            }                
        }
        Console.WriteLine();

        if (request)
        {
            Program.memberCollection.Delete(toDeleteMember);
            Console.WriteLine("  Done!");
        }
        else
            Console.WriteLine("  Member is not deleted because they are currently borrowing DVD(s).");

        Console.WriteLine("\n================================================");
        Console.Write("  Press enter to return to staff menu...");
        Console.ReadLine();
    }


    public void DisplayMemberPhoneNumber()
    {
        Console.Clear();
        Console.WriteLine("================================================");
        Console.WriteLine("         Display A Member Phone Number");
        Console.WriteLine("================================================");

        Console.Write("\n  Enter the member’s fist name:  =>  ");
        string firstName = Console.ReadLine();
        Console.Write("  Enter the member’s last name:  => ");
        string lastName = Console.ReadLine();


        IMember newMember = new Member(firstName, lastName);
        IMember findResult = Program.memberCollection.Find(newMember);

        Console.WriteLine("\n------------------------------------------------");
        if (findResult != null)
            Console.WriteLine("  The member contact number is: {0}", findResult.ContactNumber);
        else
            Console.WriteLine($"  Member {firstName} {lastName} does not exist.");

        Console.WriteLine("\n================================================");
        Console.Write("  Press enter to return to staff menu...");
        Console.ReadLine();
    }

    public void PrintBorrowersOfMovie() {
        Console.Clear();
        Console.WriteLine("================================================");
        Console.WriteLine("         Display Borrowers of a Movie");
        Console.WriteLine("================================================");

        Console.Write("\n  Please enter the movie title  =>  ");
        string movie = Console.ReadLine();
        Console.WriteLine();

        IMovie searchMovieResult = Program.movieCollection.Search(movie);
        if (searchMovieResult == null)
            Console.WriteLine("  Movie does not exist");
        else {
            if (searchMovieResult.AvailableCopies == searchMovieResult.TotalCopies)
                Console.WriteLine("  No one is borrowing this movie currently.");
            else {
                Console.WriteLine("  Borrowers of {0}:\n{1}", searchMovieResult.Title, 
                                                              searchMovieResult.Borrowers.ToString());
            }
        }

        Console.WriteLine("\n================================================");
        Console.Write("  Press enter to return to staff menu...");
        Console.ReadLine();
    }
}