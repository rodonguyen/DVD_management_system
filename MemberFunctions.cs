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
    }

    public static void displayMovieInformation(IMovieCollection movieCollection)
    {
        //Display information about a movie
        Console.WriteLine("Please enter a movie name:");

        //Read the string input of a movie 
        string movie = Console.ReadLine();
        

        IMovie searchedMovie = movieCollection.Search(movie);
        
        //print the movie's information 
        Console.WriteLine(searchedMovie.ToString());
    }

    public static void borrowAMovie(IMovieCollection movieCollection, IMember currentUser)
    {

        //How does it know which user to add??
        //AddBorrow method to add borrower

        Console.WriteLine("What movie would you like to borrow?");

        //Looking for the movie
        string movie = Console.ReadLine();

        IMovie searchedMovie = movieCollection.Search(movie);
        
        //Current user borrows the movue
        searchedMovie.AddBorrower(currentUser);

        Console.WriteLine("The movie is successfully borrowed, enjoy the movie");

    }

    public static void returnAMovie(IMovieCollection movieCollection, IMember currentUser) {
        Console.WriteLine("What movie would you like to return?:");

        //Looking for the movie
        string movie = Console.ReadLine();

        IMovie searchedMovie = movieCollection.Search(movie);

        //Current user borrows the movue
        searchedMovie.RemoveBorrower(currentUser);

        Console.WriteLine("The movie is successfully returned, have a nice day");
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
        Console.Write("\nPress enter to return to main menu...");
        Console.Read();
    }

    public void topThreeMovies() { 
        //Need to do 
    }
}