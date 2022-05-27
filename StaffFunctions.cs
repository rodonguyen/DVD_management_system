using System;
using System.Collections.Generic;
using System.Text;

public class StaffFunctions
{


    // Add new DvD movie to the system
    // Pre-condition: NIL
    // Post-condition: If the movie is new (the library currently does not any DVD of this movie), then all the information about
    // the movie and the number of the new movie DVDs should be entered into
    // the system;
    // If the movie is not new (the library has some DVDs of this
    // movie), then only the total quantity of the movie DVDs needs to be
    // updated, but the information about the movie needs not to be re-entered.


    public void AddNewDvD(IMovieCollection movieCollection)
    {
        Console.WriteLine("\n You have selected to add DvD copies of a movie");
        Console.Write("\n Enter the movie title:  => ");
        string movie = Console.ReadLine();

        IMovie iMovie = movieCollection.Search(movie);

        //If the movie is new
        if (iMovie == null)
        {
            Console.WriteLine("\n Looks like this DvD is from a movie that has not been added to the database yet.");
            Console.WriteLine("Please add a few more details about the movie:");

            /*
            Console.Write("\n Enter the movie genre:  => ");
            MovieGenre genre = (MovieGenre)Convert.ToInt32(Console.ReadLine());

            Console.Write("\n Enter the movie classification:  => ");
            MovieClassification classification = (MovieClassification)Convert.ToInt32(Console.ReadLine());
            */
            MovieGenre genre = ConsoleHandler.SelectMovieGenre();
            MovieClassification classification = ConsoleHandler.SelectMovieClassification();


            Console.Write("\n Enter the movie duration:  => ");


            int duration = ConsoleHandler.CheckInteger(Console.ReadLine());



            Console.Write("\n Enter the movie number of available copies:  => ");
            int numCopies = ConsoleHandler.CheckInteger(Console.ReadLine());

            IMovie newMovie = new Movie(movie, genre, classification, duration, numCopies);
            movieCollection.Insert(newMovie);

            Console.Write( $"  \nThe movie({ movie}) was added to the database with new DvDs!");


        }
        else //If the movie is not new
        {
            Console.Write("\n Enter the number of new DvDs to add:  => ");
            int numCopies = Convert.ToInt32(Console.ReadLine());
            iMovie.TotalCopies += numCopies;
            //do we need to update the available copies too?
            Console.Write("\n New DvDs of the movie were added.");
        }

        Console.Write("\n  Press enter to return to staff menu...");
        Console.ReadLine();
    }

    public void DeleteDvD(IMovieCollection movieCollection)
    {
        Console.WriteLine ("\n You have selected to delete DvD copies of a movie");
        Console.Write("\n Enter the movie title:  => ");
        string movie = Console.ReadLine();

        IMovie iMovie = movieCollection.Search(movie);

        if (iMovie == null)
        {
            Console.Write("\n DVD copies of {0} cannot be deleted because that movie is not in the database", movie);
        }
        else
        {
            Console.Write("\n Enter the number of DVD copies to remove:  => ");
            int numCopies = Convert.ToInt32(Console.ReadLine());
            iMovie.TotalCopies -= numCopies;

            if (iMovie.TotalCopies > 0)
            {
                Console.Write("\n DVD copies of the movie were removed.");
            }
            if(iMovie.TotalCopies <= 0)
            {
                movieCollection.Delete(iMovie);
                Console.Write("\n All DVD copies of {0} were removed. The movie was deleted from the database", movie);
            }
        }

        Console.Write("\n  Press enter to return to staff menu...");
        Console.ReadLine();
    }

    public void AddMember(IMemberCollection memberCollection)
    {
        Console.WriteLine("\n You have selected to Register a new member.");
        Console.Write("\n Enter the member’s fist name:  => ");
        string firstName = Console.ReadLine();

        Console.Write("\n Enter the member’s last name:  => ");
        string lastName = Console.ReadLine();

        Console.Write("\n Enter the member’s contact phone number:  => ");
        string phone = Console.ReadLine();

        Console.Write("\n Enter the member’s password:  => ");
        string pin = Console.ReadLine();

        IMember newMember = new Member(firstName, lastName, phone, pin);

        memberCollection.Add(newMember);

        Console.Write("\n  Press enter to return to staff menu...");
        Console.ReadLine();

    }

    public void RemoveMember(IMemberCollection memberCollection)
    {
        Console.WriteLine("\n You have selected to Remove an existing member.");
        Console.Write("\n Enter the member’s fist name:  => ");
        string firstName = Console.ReadLine();

        Console.Write("\n Enter the member’s last name:  => ");
        string lastName = Console.ReadLine();
        
        IMember newMember = new Member(firstName, lastName);
        
        memberCollection.Delete(newMember);


        Console.Write("\n  Press enter to return to staff menu...");
        Console.ReadLine();

    }
    public void DisplayMemberPhoneNumber(IMemberCollection memberCollection)
    {
        Console.WriteLine("\n You have selected to display a member's contact number");
        Console.Write("\n Enter the member’s fist name:  => ");
        string firstName = Console.ReadLine();

        Console.Write("\n Enter the member’s last name:  => ");
        string lastName = Console.ReadLine();

        IMember newMember = new Member(firstName, lastName);

        if (memberCollection.Search(newMember))
        {
            IMember foundMember = memberCollection.Find(newMember);
            Console.WriteLine("\n The member contact Number is {0}", foundMember.ContactNumber);
        }
        else
        {
            Console.WriteLine($"Member {firstName} {lastName} does not exist in the system: ");
        }


        Console.Write("\n  Press enter to return to staff menu...");
        Console.ReadLine();

    }

    //pre-condition: 
    public void PrintBorrowersofMovie(IMovieCollection movieCollection) {
        //Checks if the movie (IMovie) exists in the list?
        //If number of total copies == number of available copies
        //Return "no one is currently borriwing this movie"
        //else
        //
        //print the list of borrowers of that movie
        //
        //return to the main menu
        Console.WriteLine("Please enter a movie to view members borrowing the movie");
        string movie = Console.ReadLine();
        IMovie result = movieCollection.Search(movie); //Searches for the movie
        if (result == null)
        {
            Console.WriteLine("Movie does not exist");
        }
        else
        {
            if (result.AvailableCopies == result.TotalCopies) //Available copies == total copies meaning no one has borrowed the movie
            {
                Console.WriteLine("No one is borrowing this movie currently");
            }
            else
            {
                Console.WriteLine("List of people currently borrowing " + result + ": \n");
                Console.WriteLine(result.Borrowers.ToString());
            }
        }

        Console.Write("\n  Press enter to return to staff menu...");
        Console.ReadLine();
    }
}