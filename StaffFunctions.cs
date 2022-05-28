using System;
using System.Collections.Generic;
using System.Text;

public class StaffFunctions
{
    // ------------------------------------- Assistive Functions ------------------------------------------
    private static bool CheckInteger(String input)
    {
        bool isInt = int.TryParse(input, out _);
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
            Console.WriteLine($"  Invalid choice ({classification}): Your choice must be an integer from 1 to 4!");
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
        bool isValidDuration = CheckInteger(duration);

        while (!isValidDuration)
        {
            Console.WriteLine("\n  !!!!!");
            Console.WriteLine($"  Invalid duration ({duration}). Please enter a numeric value.");
            Console.Write("  Re-enter duration   =>  ");
            duration = Console.ReadLine();
            isValidDuration = CheckInteger(duration);
        }

        return int.Parse(duration);
    }

    private static int EnterMovieCopies(string prompt) {
        Console.WriteLine("\n------------------------------------------------");
        Console.Write($"  {prompt}  =>  ");
        string copiesNum = Console.ReadLine();
        bool isValidCopiesNum = CheckInteger(copiesNum);

        while (!isValidCopiesNum)
        {
            Console.WriteLine("\n  !!!!!");
            Console.WriteLine($"  Invalid number of copies ({copiesNum}). Please enter a numeric value.");

            Console.Write("  Re-enter the number of copies  =>  ");
            copiesNum = Console.ReadLine();
            isValidCopiesNum = CheckInteger(copiesNum);
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



    // -------------------------------- Main Console Staff Function ---------------------------------


    // Add new DvD movie to the system
    // Pre-condition: NIL
    // Post-condition: If the movie is new (the library currently does not any DVD of this movie), then all the information about
    // the movie and the number of the new movie DVDs should be entered into
    // the system;
    // If the movie is not new (the library has some DVDs of this
    // movie), then only the total quantity of the movie DVDs needs to be
    // updated, but the information about the movie needs not to be re-entered.
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

            /*
            Console.Write("\n Enter the movie genre:  => ");
            MovieGenre genre = (MovieGenre)Convert.ToInt32(Console.ReadLine());

            Console.Write("\n Enter the movie classification:  => ");
            MovieClassification classification = (MovieClassification)Convert.ToInt32(Console.ReadLine());
            */
            MovieGenre genre = SelectMovieGenre();
            MovieClassification classification = SelectMovieClassification();
            int duration = EnterMovieDuration();
            int numCopies = EnterMovieCopies("Enter the number of copies");

            Program.movieCollection.Insert(new Movie(movie, genre, classification, duration, numCopies));
            Console.WriteLine( $"\n  The movie ({ movie}) was added to the database!");
        }
        else //If the movie is not new
        {
            int numCopies = EnterMovieCopies("Enter the new total number of copies");
            iMovie.TotalCopies = numCopies;
            //do we need to update the available copies too?
            Console.WriteLine($"\n  The number of DVDS is updated ({numCopies}).");
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
        Console.Write("\n  Enter the movie title:  => ");
        string movie = Console.ReadLine();

        IMovie iMovie = Program.movieCollection.Search(movie);

        if (iMovie == null)
        {
            Console.Write("\n  DVD copies of {0} cannot be deleted because that movie is not in the database", movie);
        }
        else
        {
            int numCopies = EnterMovieCopies("  Enter the number of DVD copies to remove");
            iMovie.TotalCopies -= numCopies;

            if (iMovie.TotalCopies > 0)
            {
                Console.Write("\n  DVD copies of the movie were removed.");
            }
            if(iMovie.TotalCopies <= 0)
            {
                Program.movieCollection.Delete(iMovie);
                Console.Write("\n  All DVD copies of {0} were removed. The movie was deleted from the database", movie);
            }
        }

        Console.Write("\n  Press enter to return to staff menu...");
        Console.ReadLine();
    }

    public void AddMember()
    {
        Console.Clear();
        Console.WriteLine("================================================");
        Console.WriteLine("  Add Member");
        Console.WriteLine("================================================");
        Console.WriteLine("\n  You have selected to Register a new member.");
        Console.Write("\n  Enter the member’s first name:  => ");
        string firstName = Console.ReadLine();

        Console.Write("\n  Enter the member’s last name:  => ");
        string lastName = Console.ReadLine();

        Console.Write("\n  Enter the member’s contact phone number:  => ");

        string phone = Console.ReadLine();
        bool valid = IMember.IsValidContactNumber(phone);
        while (!valid)
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine($"  Invalid input ({phone}): Please enter a valid phone number.");
            Console.WriteLine("---------------------------------------------------------------------------\n");

            Console.Write("=> ");
            phone = Console.ReadLine();
            valid = IMember.IsValidContactNumber(phone);
        }
       

        Console.Write("\n Enter the member’s password:  => ");
        string pin = Console.ReadLine();
        bool validPin = IMember.IsValidPin(pin);
        while (!validPin)
        {

            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine($"  Invalid input ({pin}): Please enter a valid pin number.");
            Console.WriteLine("---------------------------------------------------------------------------\n");

            Console.Write("=> ");
            pin = Console.ReadLine();
            validPin = IMember.IsValidPin(pin);
        }

        IMember newMember = new Member(firstName, lastName, phone, pin);

        Program.memberCollection.Add(newMember);

        Console.Write("\n  Press enter to return to staff menu...");
        Console.ReadLine();

    }

    public void RemoveMember()
    {
        Console.Clear();
        Console.WriteLine("================================================");
        Console.WriteLine("  Remove Member");
        Console.WriteLine("================================================");
        Console.Write("\n Enter the member’s fist name:  => ");
        string firstName = Console.ReadLine();

        Console.Write("\n Enter the member’s last name:  => ");
        string lastName = Console.ReadLine();
        
        IMember newMember = new Member(firstName, lastName);


        Program.memberCollection.Delete(newMember);


        Console.Write("\n  Press enter to return to staff menu...");
        Console.ReadLine();

    }
    public void DisplayMemberPhoneNumber()
    {
        Console.Clear();
        Console.WriteLine("================================================");
        Console.WriteLine("  Display A Member Phone Number");
        Console.WriteLine("================================================");
        Console.Write("\n Enter the member’s fist name:  => ");
        string firstName = Console.ReadLine();

        Console.Write("\n Enter the member’s last name:  => ");
        string lastName = Console.ReadLine();

        IMember newMember = new Member(firstName, lastName);

        if (Program.memberCollection.Search(newMember))
        {
            IMember foundMember = Program.memberCollection.Find(newMember);
            Console.WriteLine($"\n The member contact Number is {0}", foundMember.ContactNumber);
        }
        else
        {
            Console.WriteLine($"Member {firstName}, {lastName} does not exist in the system");
        }


        Console.Write("\n  Press enter to return to staff menu...");
        Console.ReadLine();

    }

    //pre-condition: 
    public void PrintBorrowersofMovie() {
        //Checks if the movie (IMovie) exists in the list?
        //If number of total copies == number of available copies
        //Return "no one is currently borriwing this movie"
        //else
        //
        //print the list of borrowers of that movie
        //
        //return to the main menu
        Console.Clear();
        Console.WriteLine("================================================");
        Console.WriteLine("         Display Borrowers of a Movie");
        Console.WriteLine("================================================");
        Console.WriteLine("Please enter a movie to view members borrowing the movie");
        string movie = Console.ReadLine();

        IMovie searchResult = Program.movieCollection.Search(movie);
        if (searchResult == null)
        {
            Console.WriteLine("Movie does not exist");
        }
        else
        {
            if (searchResult.AvailableCopies == searchResult.TotalCopies)
            {
                Console.WriteLine("No one is borrowing this movie currently");
            }
            else
            {
                Console.WriteLine("List of people currently borrowing " + searchResult + ": \n");
                Console.WriteLine(searchResult.Borrowers.ToString());
            }
        }

        Console.Write("\n  Press enter to return to staff menu...");
        Console.ReadLine();
    }
}