using System;
using System.Linq;
using System.Diagnostics;

class Program
{
    public static MemberCollection memberCollection = new MemberCollection(100000);
    public static MovieCollection movieCollection = new MovieCollection();

    static void Main()
    {
        /////// TEST DATA ///////
        //memberCollection.Add(new Member("Anthony", "Nguyen", "0450456788", "1234"));
        //memberCollection.Add(new Member("Dulce", "Acevedo", "0450456788", "1234"));
        //memberCollection.Add(new Member("Rodo", "N", "0123412341", "1111"));
        //memberCollection.Add(new Member("D", "D", "0444455555", "1111"));
        //memberCollection.Add(new Member("C", "C", "0444455555", "1111"));
        //memberCollection.Add(new Member("A", "A", "0444455555", "1111"));
        memberCollection.Add(new Member("a", "a", "0444455555", "1111"));
        //memberCollection.Add(new Member("E", "A", "0444455555", "1111"));
        //memberCollection.Add(new Member("E", "E", "0444455555", "1111"));
        //memberCollection.Add(new Member("F", "F", "0444455555", "2222"));

        //movieCollection.Insert(new Movie("Batman", MovieGenre.Action, MovieClassification.M15Plus, 180, 10));
        //movieCollection.Insert(new Movie("Barbie", MovieGenre.Western, MovieClassification.G, 90, 10));
        //movieCollection.Insert(new Movie("Everything Everywhere All at Once", MovieGenre.Western, MovieClassification.G, 240, 10));
        //movieCollection.Insert(new Movie("Movie 1", MovieGenre.Western, MovieClassification.G, 101, 1));
        //movieCollection.Insert(new Movie("Movie 2", MovieGenre.Action, MovieClassification.G, 102, 2));
        //movieCollection.Insert(new Movie("Movie 3", MovieGenre.History, MovieClassification.PG, 103, 5));
        //movieCollection.Insert(new Movie("Movie 4", MovieGenre.History, MovieClassification.PG, 104, 5));

        //movieCollection.Search("Movie 1").AddBorrower(new Member("a", "a"));
        //movieCollection.Search("Movie 3").AddBorrower(new Member("A", "A"));
        //movieCollection.Search("Movie 3").AddBorrower(new Member("C", "C"));
        //movieCollection.Search("Movie 3").AddBorrower(new Member("D", "D"));
        //movieCollection.Search("Movie 3").AddBorrower(new Member("Anthony", "Nguyen"));
        //movieCollection.Search("Movie 4").AddBorrower(new Member("E", "E"));
        //movieCollection.Search("Movie 4").AddBorrower(new Member("a", "a"));
        //movieCollection.Search("Batman").AddBorrower(new Member("a", "a"));

        ConsoleHandler.MainMenu();
    }
}
