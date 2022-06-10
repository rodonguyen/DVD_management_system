using System;
using System.Collections.Generic;
using System.Text;

class MemberFunctions
{
    public static void ListOfMovies() {
        Console.Clear();
        Console.WriteLine("================================================");
        Console.WriteLine("  Movie Collection");
        Console.WriteLine("================================================");
        //What happens when the movie collection is empty when calling ToArray();
        IMovie[] movieList = Program.movieCollection.ToArray();
        //Is printing the array a better solution than printing the tree
        for (int i = 0; i < movieList.Length; i++)
        {
            if (movieList[0] == null) { 
                Console.WriteLine("  Movie list is currently empty, please check back later");
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
        Console.WriteLine("================================================");
        Console.Write("\n  Press enter to return to member menu...");
        Console.ReadLine();
    }

    public static void DisplayMovieInformation(IMovieCollection movieCollection)
    {
        Console.Clear();
        Console.WriteLine("================================================");
        Console.WriteLine("  Movie Information");
        Console.WriteLine("================================================");
        Console.Write("\n  Enter the movie name  =>  ");
        string movie = Console.ReadLine();
       
        IMovie searchedMovie = movieCollection.Search(movie);
        if (searchedMovie == null)
        {
            Console.WriteLine("  Movie does not exist in the system");
        }
        else {
            Console.WriteLine(searchedMovie.ToString());
        }
        Console.WriteLine("================================================");
        Console.Write("\n  Press enter to return to member menu...");
        Console.ReadLine();
    }

    public static void BorrowAMovie(IMember currentUser)
    {

        //How does it know which user to add??
        //AddBorrow method to add borrower
        Console.Clear();
        Console.WriteLine("================================================");
        Console.WriteLine("  Borrow Movie");
        Console.WriteLine("================================================");
        Console.Write("\n  Enter movie would you like to borrow  =>  ");

        //Looking for the movie
        string movie = Console.ReadLine();

        IMovie searchedMovie = Program.movieCollection.Search(movie);
        if (searchedMovie == null)
        {
            Console.WriteLine("  Movie does not exist in the system");
        }
        else
        {
            if (!searchedMovie.AddBorrower(currentUser))
            {
                Console.WriteLine("  Borrowing movie unsucessful");
            }
            else
            {
                Console.WriteLine("  The movie is successfully borrowed");
            }
        }

        //Current user borrows the movue
        Console.WriteLine("================================================");
        Console.Write("\n  Press enter to return to member menu...");
        Console.ReadLine();

    }

    public static void ReturnAMovie(IMember currentUser) {
        Console.Clear();
        Console.WriteLine("================================================");
        Console.WriteLine("          Return a Movie");
        Console.WriteLine("================================================");
        Console.Write("\n  Enter movie would you like to return  =>  ");

        //Looking for the movie
        string movie = Console.ReadLine();

        IMovie searchedMovie = Program.movieCollection.Search(movie);
        
        if (searchedMovie != null && searchedMovie.RemoveBorrower(currentUser))
        {
            Console.WriteLine("  The movie is successfully returned, have a nice day!");
        }
        else
        {
            Console.WriteLine("  You do not have a copy of that movie to return");
        }
        Console.WriteLine("================================================");
        Console.Write("\n  Press enter to return to member menu...");
        Console.ReadLine();

    }

    public static void DisplayBorrowingMovies(IMember member) {
        // Finding all borrowing movies
        MovieCollection borrowingMovies = new MovieCollection();
        foreach (Movie movie in Program.movieCollection.ToArray())
            if (movie.Borrowers.Search(member))
                borrowingMovies.Insert(movie);

        // Displaying the borrowing movies
        Console.Clear();
        Console.WriteLine("================================================");

        Console.WriteLine("  Your borrowing movies:");

        if (borrowingMovies.ToArray().Length == 0)
            Console.WriteLine("  < Empty >");
        else
            foreach (Movie movie in borrowingMovies.ToArray())
                Console.WriteLine("  "+movie.ToString());

        Console.WriteLine("================================================");
        Console.Write("\n  Press enter to return to member menu...");
        Console.ReadLine();
    }

    public static void DisplayTop3Movies() {
        Movie[] top3Movies = new Movie[] { null, null, null };
        int[] top3NoBorrowings = new int[]{-1,-1,-1};
        

        foreach (Movie movie in Program.movieCollection.ToArray()) {
            int objNoBorrowing = movie.NoBorrowings;
            
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
            else if (objNoBorrowing > top3NoBorrowings[1])
            {
                top3NoBorrowings[2] = top3NoBorrowings[1];
                top3NoBorrowings[1] = objNoBorrowing;
                top3Movies[2] = top3Movies[1];
                top3Movies[1] = movie;
            }
            else if (objNoBorrowing > top3NoBorrowings[2])
            {
                top3NoBorrowings[2] = objNoBorrowing;
                top3Movies[2] = movie;
            }
        }

        Console.Clear();
        Console.WriteLine("================================================");
        Console.WriteLine("  Top3 Most Borrowed Movies:");
        for (int i = 0; i < 3; i++)     {
            if (top3NoBorrowings[i] == -1)
                Console.WriteLine($"    Top {i + 1} - Not available");
            else
                Console.WriteLine($"    Top {i + 1} - {top3Movies[i].Title} (borrowed {top3NoBorrowings[i]} times)");
        }
        Console.WriteLine("================================================");
        Console.Write("\n  Press enter to return to member menu...");
        Console.ReadLine();
    }
}