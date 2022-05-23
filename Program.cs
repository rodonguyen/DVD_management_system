using System;
using System.Linq;
using System.Diagnostics;

class Program
{
    public static MemberCollection memberCollection = new MemberCollection(100000);
    public static MovieCollection movieCollection = new MovieCollection();

    static void Main()
    {
        memberCollection.Add(new Member("D", "D", "0444455555", "1111"));
        memberCollection.Add(new Member("C", "C", "0444455555", "1111"));
        memberCollection.Add(new Member("A", "A", "0444455555", "1111"));
        memberCollection.Add(new Member("E", "A", "0444455555", "1111"));
        memberCollection.Add(new Member("E", "E", "0444455555", "1111"));
        memberCollection.Add(new Member("F", "F", "0444455555", "2222"));
        memberCollection.Add(new Member("Anthony", "Nguyen", "0450456788", "1234"));



        movieCollection.Insert(new Movie("Batman", MovieGenre.Action, MovieClassification.M15Plus, 180, 10));
        movieCollection.Insert(new Movie("Barbie", MovieGenre.Western, MovieClassification.G, 90, 10));
        movieCollection.Insert(new Movie("Everything Everywhere All at Once", MovieGenre.Western, MovieClassification.G, 240, 10));


        ConsoleHandler.MainMenu();
    }








    // =======================================================================================
    // =======================================================================================
    // ||                                  TESTING CODE                                     ||
    // =======================================================================================
    // =======================================================================================
    static void TestPrompt(string testname, string description)
    {
        int length1 = testname.Length;
        int length2 = description.Length;

        int numDashAfter = 75 - length1;
        string dashAfter = string.Concat(Enumerable.Repeat("-", numDashAfter));
        string spaceAfter = string.Concat(Enumerable.Repeat(" ", (20+length1 + numDashAfter) - 4 - length2));
        System.Console.WriteLine("\n");
        System.Console.WriteLine($"----------  Test: {testname}  {dashAfter}");
        System.Console.WriteLine($"| {description} {spaceAfter}|");
        System.Console.WriteLine(string.Concat(Enumerable.Repeat("-", 20+ length1 + numDashAfter)));
    }

    static void MyTesting()
    {
        // ---------- Testing IsValidContactNumber ----------
        Console.WriteLine("\n--------- Testing IsValidContactNumber ----------");

        string[] validPhonenumbers = { "0444455555", "0123456789", "0123999111" };
        Console.WriteLine("Correct phone numbers");
        foreach (string phonenumber in validPhonenumbers)
        {
            Console.WriteLine(IMember.IsValidContactNumber(phonenumber));
        }

        string[] invalidPhonenumbers = { "088888888",      // 9 digits
                                         "011011011011",   // 12 digits
                                         "1231231230",     // 0 is not first 
                                         "0123x12399",     // contains non-digit 
                                         "08888888-",      // contains non-digit 
                                         "",               // empty
                                         "----------" };   // non-digit
        Console.WriteLine("\nInvalid phone numbers");
        foreach (string phonenumber in invalidPhonenumbers)
        {
            Console.WriteLine(IMember.IsValidContactNumber(phonenumber));

        }


        // ---------- Testing IsValidPin ----------
        Console.WriteLine("\n--------- Testing IsValidPin ----------");
        string[] validPins = { "0444", "1011", "12312", "012399" };
        Console.WriteLine("\nValid Pins");
        foreach (string pin in validPins)
        {
            Console.WriteLine(IMember.IsValidPin(pin));
        }
        string[] invalidPins = { ".012",        // contains non-digit
                                 "22(11",       // contains non-digit
                                 "888",         // 3 digits
                                 "0000001",     // 7 digits
                                 "",            // empty
                                 "aaaa" };      // all non-digits
        Console.WriteLine("\nInvalid Pins");
        foreach (string pin in invalidPins)
        {
            Console.WriteLine(IMember.IsValidPin(pin));
        }


        //  --------- Testing Add ----------
        MemberCollection memberCollection = new MemberCollection(5);
        Console.WriteLine("\n--------- Testing Add ----------");
        memberCollection.Add(new Member("D", "D"));
        memberCollection.Add(new Member("C", "C"));
        memberCollection.Add(new Member("A", "A"));
        memberCollection.Add(new Member("E", "A"));
        memberCollection.Add(new Member("E", "E"));
        memberCollection.Add(new Member("Z", "A"));     // memberCollection Full
        Console.WriteLine(memberCollection.ToString());


        // ---------- Testing Detele ----------
        Console.WriteLine("\n--------- Testing Delete ----------");
        memberCollection.Delete(new Member("N", "K"));   // Delete unexisted member
        memberCollection.Delete(new Member("E", "A"));
        memberCollection.Delete(new Member("E", "E"));
        Console.WriteLine(memberCollection.ToString());

        memberCollection.Clear();
        memberCollection.Delete(new Member("D", "D"));   // Deleting unexisted member

        memberCollection.Add(new Member("E", "E"));
        memberCollection.Delete(new Member("E", "E"));
        memberCollection.Delete(new Member("E", "E"));   // Deleting a member twice


        // ---------- Testing Search ----------
        Console.WriteLine("\n--------- Testing Search ----------");
        memberCollection.Add(new Member("A", "A"));
        Console.WriteLine(memberCollection.ToString());
        Console.WriteLine(memberCollection.Search(new Member("D", "D")));   // Searching unexisted member
        Console.WriteLine(memberCollection.Search(new Member("A", "A")));   // Searching valid member


        // ---------- Movie ----------

        //  TESTING IsEmpty
        TestPrompt("isEmpty", "Should print True");
        MovieCollection movieCollection = new MovieCollection();
        System.Console.WriteLine(movieCollection.IsEmpty());

        TestPrompt("isEmpty", "Should print False");
        IMovie movie1 = new Movie("KKK", MovieGenre.Action, MovieClassification.G, 150, 2);
        IMovie movie2 = new Movie("KKK", MovieGenre.Western, MovieClassification.G, 150, 3);
        IMovie movie3 = new Movie("AAA", MovieGenre.Drama, MovieClassification.G, 160, 3);
        IMovie movie4 = new Movie("OOO", MovieGenre.Western, MovieClassification.G, 170, 3);
        movieCollection.Insert(movie1);
        System.Console.WriteLine(movieCollection.IsEmpty());

        
        //  TESTING CompareTo
        TestPrompt("CompareTo", "Should print 0, 1, -1");
        System.Console.WriteLine(movie1.CompareTo(movie2));
        System.Console.WriteLine(movie1.CompareTo(movie3));
        System.Console.WriteLine(movie1.CompareTo(movie4));


        //  TESTING Insert
        TestPrompt("Insert", "Should not be able to insert 1st and 2nd movie");
        System.Console.WriteLine(movieCollection.Insert(movie1));     // Duplicate title
        System.Console.WriteLine(movieCollection.Insert(movie2));     // Duplicate title
        System.Console.WriteLine();
        System.Console.WriteLine(movieCollection.Insert(movie3));
        System.Console.WriteLine(movieCollection.Insert(movie4));


        TestPrompt("Insert", "Should print 3 - the number of movies in collection");
        System.Console.WriteLine(movieCollection.Number);


        //  TESTING ToArray()
        TestPrompt("ToArray, ToString", "Should print 3 movies' details");
        IMovie[] movies = movieCollection.ToArray();
        foreach (Movie movie in movies)   {
            System.Console.WriteLine(movie.ToString());
        }


        //  TESTING IsEmpty and other properties
        TestPrompt("Clear", "Should print True, 0");
        movieCollection.Clear();
        System.Console.WriteLine($"{movieCollection.IsEmpty()}, {movieCollection.Number}");


        TestPrompt("Movie check", "Should have the following numbers: 0, 2, 2");
        System.Console.WriteLine($"Number of borrowings: {movie1.NoBorrowings}");
        System.Console.WriteLine($"Total copies: {movie1.TotalCopies}");
        System.Console.WriteLine($"Available copiess: {movie1.AvailableCopies}");


        //  TESTING AddBorrower
        TestPrompt("AddBorrower", "Should have the followings: True, 1, 2, 1");
        System.Console.WriteLine(movie1.AddBorrower(new Member("A", "A")));
        System.Console.WriteLine($"Number of borrowings: {movie1.NoBorrowings}");
        System.Console.WriteLine($"Total copies: {movie1.TotalCopies}");
        System.Console.WriteLine($"Available copiess: {movie1.AvailableCopies}");

        TestPrompt("AddBorrower", "Should have the followings: True, 2, 2, 0");
        System.Console.WriteLine(movie1.AddBorrower(new Member("D", "D")));
        System.Console.WriteLine($"Number of borrowings: {movie1.NoBorrowings}");
        System.Console.WriteLine($"Total copies: {movie1.TotalCopies}");
        System.Console.WriteLine($"Available copiess: {movie1.AvailableCopies}");

        TestPrompt("AddBorrower - no more copies", "Should have the followings: False, 2, 2, 0");
        System.Console.WriteLine(movie1.AddBorrower(new Member("C", "C")));
        System.Console.WriteLine($"Number of borrowings: {movie1.NoBorrowings}");
        System.Console.WriteLine($"Total copies: {movie1.TotalCopies}");
        System.Console.WriteLine($"Available copiess: {movie1.AvailableCopies}");

        TestPrompt("AddBorrower - same member", "Should print the followings: True False False, 1, 3, 2");
        System.Console.WriteLine(movie3.AddBorrower(new Member("D", "D")));
        System.Console.WriteLine(movie3.AddBorrower(new Member("D", "D")));
        System.Console.WriteLine(movie3.AddBorrower(new Member("D", "D")));
        System.Console.WriteLine($"Number of borrowings: {movie3.NoBorrowings}");
        System.Console.WriteLine($"Total copies: {movie3.TotalCopies}");
        System.Console.WriteLine($"Available copiess: {movie3.AvailableCopies}");


        //  TESTING RemoveBorrower
        TestPrompt("RemoveBorrower - unexisted borrower", "Should print False x3");
        System.Console.WriteLine(movie2.RemoveBorrower(new Member("C", "C")));
        System.Console.WriteLine(movie1.RemoveBorrower(new Member("C", "C")));
        System.Console.WriteLine(movie1.RemoveBorrower(new Member("D", "A")));

        TestPrompt("RemoveBorrower", "Should print True");
        System.Console.WriteLine(movie1.RemoveBorrower(new Member("A", "A")));
        System.Console.WriteLine($"Number of borrowings: {movie1.NoBorrowings}");
        System.Console.WriteLine($"Total copies: {movie1.TotalCopies}");
        System.Console.WriteLine($"Available copiess: {movie1.AvailableCopies}");

        TestPrompt("RemoveBorrower", "Should print False");
        System.Console.WriteLine(movie1.RemoveBorrower(new Member("A", "A")));
        System.Console.WriteLine($"Number of borrowings: {movie1.NoBorrowings}");
        System.Console.WriteLine($"Total copies: {movie1.TotalCopies}");
        System.Console.WriteLine($"Available copiess: {movie1.AvailableCopies}");

        TestPrompt("RemoveBorrower", "Should print the followings: True x2, 3, 2, 1");
        System.Console.WriteLine(movie1.AddBorrower(new Member("E", "E")));
        System.Console.WriteLine(movie1.RemoveBorrower(new Member("E", "E")));
        System.Console.WriteLine($"Number of borrowings: {movie1.NoBorrowings}");
        System.Console.WriteLine($"Total copies: {movie1.TotalCopies}");
        System.Console.WriteLine($"Available copiess: {movie1.AvailableCopies}");

        //  TESTING ToArray
        TestPrompt("ToArray", "Should print 8 movies details: ");
        IMovie movie5 = new Movie("A10", MovieGenre.Action, MovieClassification.G, 150, 2);
        IMovie movie6 = new Movie("A11", MovieGenre.Western, MovieClassification.G, 150, 3);
        IMovie movie7 = new Movie("A20", MovieGenre.Drama, MovieClassification.G, 160, 3);
        IMovie movie8 = new Movie("A90", MovieGenre.Western, MovieClassification.G, 170, 3);
        IMovie movie9 = new Movie("A82", MovieGenre.Western, MovieClassification.G, 170, 3);
        movieCollection.Insert(movie1);
        movieCollection.Insert(movie2);
        movieCollection.Insert(movie3);
        movieCollection.Insert(movie4);
        movieCollection.Insert(movie5);
        movieCollection.Insert(movie6);
        movieCollection.Insert(movie7);
        movieCollection.Insert(movie8);
        movieCollection.Insert(movie9);
        foreach (Movie movie in movieCollection.ToArray())    {
            System.Console.WriteLine(movie.ToString());
        }


        //  TESTING Search (1)
        TestPrompt("Search", "Should print Foundx3, 3 other movies info.");

        System.Console.WriteLine(movieCollection.Search(movie1));
        System.Console.WriteLine(movieCollection.Search(movie2));
        System.Console.WriteLine(movieCollection.Search(movie3));

        System.Console.WriteLine();
        IMovie movieResult;
        movieResult = movieCollection.Search("AAA");
        System.Console.WriteLine(movieResult.Title);
        movieResult = movieCollection.Search("A82");
        System.Console.WriteLine(movieResult.ToString());
        movieResult = movieCollection.Search("OOO");
        System.Console.WriteLine(movieResult.ToString());

        movieResult = movieCollection.Search("XXXXX"); // Unexisted movie 
        System.Console.WriteLine(movieResult == null);


        TestPrompt("Delete", "Should print 5 movies details");
        Debug.Assert(movieCollection.Delete(movie9) == true);
        Debug.Assert(movieCollection.Delete(movie4) == true);
        Debug.Assert(movieCollection.Delete(movie5) == true);
        Debug.Assert(movieCollection.Number == 5);
        foreach (Movie movie in movieCollection.ToArray())    {
            System.Console.WriteLine(movie.ToString());
        }


        TestPrompt("Delete unexisted movies", "Should print 5 movies details");
        Debug.Assert(movieCollection.Delete(movie5) == false);
        Debug.Assert(movieCollection.Delete(new Movie("kkk")) == false);
        Debug.Assert(movieCollection.Number == 5);
        foreach (Movie movie in movieCollection.ToArray())
        {
            System.Console.WriteLine(movie.ToString());
        }

    }
}
