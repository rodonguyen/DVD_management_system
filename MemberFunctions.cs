using System;
using System.Collections.Generic;
using System.Text;

class MemberFunctions
{
    public static void listOfMovies(IMovieCollection movieCollection) {
        //What happens when the movie collection is empty when calling ToArray();
        IMovie[] movieList = movieCollection.ToArray();
        //Is printing the array a better solution than printing the tree
        for (int i = 0; i < movieList.Length; i++)
        {
            if (movieList[0] == null) { 
                Console.WriteLine("Movie list is currently empty, please check back later");
                return;
            }
            else
            {
                Console.WriteLine(movieList[i].ToString());
            }
        }
        //if movieCollection is empty
        //print The list of movies is empty please try again later
        //else
        //print the list of movies in alphabetical order

        Console.Write("\n  Press enter to return to member menu...");
        Console.ReadLine();
    }

    public static void displayMovieInformation(IMovieCollection movieCollection)
    {
        //Display information about a movie
        Console.WriteLine("Please enter a movie name:");

        //Read the string input of a movie 
        string movie = ConsoleHandler.CheckString(Console.ReadLine());
       

        IMovie searchedMovie = movieCollection.Search(movie);

        if (searchedMovie == null)
        {
            Console.WriteLine("Movie does not exist in the system");
        }
        else {
            Console.WriteLine(searchedMovie.ToString());
        }
        
        //print the movie's information 
        
        Console.Write("\n  Press enter to return to member menu...");
        Console.ReadLine();
    }

    public static void borrowAMovie(IMovieCollection movieCollection, IMember currentUser)
    {

        //How does it know which user to add??
        //AddBorrow method to add borrower

        Console.WriteLine("What movie would you like to borrow?");

        //Looking for the movie
        string movie = Console.ReadLine();

        IMovie searchedMovie = movieCollection.Search(movie);
        if (searchedMovie == null)
        {
            Console.WriteLine("Movie does not exist in the system");
        }
        else
        {
            searchedMovie.AddBorrower(currentUser);
            Console.WriteLine("The movie is successfully borrowed, enjoy the movie");
        }

        //Current user borrows the movue
        

        
        Console.Write("\n  Press enter to return to member menu...");
        Console.ReadLine();

    }

    public static void returnAMovie(IMovieCollection movieCollection, IMember currentUser) {
        Console.WriteLine("What movie would you like to return?:");

        //Looking for the movie
        string movie = Console.ReadLine();

        IMovie searchedMovie = movieCollection.Search(movie);
        if (searchedMovie == null)
        {
            
            Console.WriteLine("Movie does not exist in the system");
        }
        else
        {
            if (!searchedMovie.Borrowers.Search(currentUser))
            {
                Console.WriteLine("You currently do not have that movie");
            }
            else { 
            //Current user borrows the movue
                searchedMovie.RemoveBorrower(currentUser);
                Console.WriteLine("The movie is successfully returned, have a nice day");
            }
        }

        Console.Write("\n  Press enter to return to member menu...");
        Console.ReadLine();

    }

    public static void displayBorrowingMovies(IMember member) {
        // Finding all borrowing movies
        MovieCollection borrowingMovies = new MovieCollection();
        foreach (Movie movie in Program.movieCollection.ToArray())
            if (movie.Borrowers.Search(member))
                borrowingMovies.Insert(movie);

        // Displaying borrowing movies
        Console.Clear();
        Console.WriteLine("================================================");

        Console.WriteLine("  Your borrowing movies:");
        foreach (Movie movie in borrowingMovies.ToArray())
            Console.WriteLine("  "+movie.ToString());
        Console.WriteLine("================================================");
        Console.Write("\n  Press enter to return to member menu...");
        Console.ReadLine();
    }

    public static void displayTop3Movies() {
        Movie[] top3Movies = new Movie[] { null, null, null };
        int[] top3NoBorrowings = new int[]{0,0,0};

        foreach (Movie movie in Program.movieCollection.ToArray()) {
            int objNoBorrowing = movie.NoBorrowings;
            // Check if 'movie' is more popular than the third one in the current top3 list
            if (objNoBorrowing > top3NoBorrowings[2])
            {
                // Place the movie.Title and its NoBorrowings in the top3 list
                // but maintain the number of borrowings' descending order
                if (objNoBorrowing > top3NoBorrowings[0])
                {
                    top3NoBorrowings[2] = top3NoBorrowings[1];
                    top3NoBorrowings[1] = top3NoBorrowings[0];
                    top3NoBorrowings[0] = objNoBorrowing;
                    top3Movies[2] = top3Movies[1];
                    top3Movies[1] = top3Movies[0];
                    top3Movies[0] = movie;
                }
                else if (objNoBorrowing <= top3NoBorrowings[0] && objNoBorrowing > top3NoBorrowings[1])
                {
                    top3NoBorrowings[2] = top3NoBorrowings[1];
                    top3NoBorrowings[1] = objNoBorrowing;
                    top3Movies[2] = top3Movies[1];
                    top3Movies[1] = movie;
                }
                else if (objNoBorrowing <= top3NoBorrowings[1] && objNoBorrowing > top3NoBorrowings[2])
                {
                    top3NoBorrowings[2] = objNoBorrowing;
                    top3Movies[2] = movie;
                }
            }
        }

        Console.Clear();
        Console.WriteLine("================================================");
        Console.WriteLine("  Top3 Most Borrowed Movies:");
        for (int i = 0; i < 3; i++)     {
            if (top3NoBorrowings[i] == 0)
                Console.WriteLine($"    Top {i + 1} - Not available");
            else
                Console.WriteLine($"    Top {i + 1} - {top3Movies[i].Title} (borrowed {top3NoBorrowings[i]} times)");
        }
        Console.WriteLine("================================================");
        Console.Write("\n  Press enter to return to member menu...");
        Console.ReadLine();
    }
}