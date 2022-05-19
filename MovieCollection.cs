// Phase 2
// An implementation of MovieCollection ADT
// 2022


using System;

//A class that models a node of a binary search tree
//An instance of this class is a node in a binary search tree 
public class BTreeNode
{
	private IMovie movie; // movie
	private BTreeNode lchild; // reference to its left child 
	private BTreeNode rchild; // reference to its right child

	public BTreeNode(IMovie movie)
	{
		this.movie = movie;
		lchild = null;
		rchild = null;
	}

	public IMovie Movie
	{
		get { return movie; }
		set { movie = value; }
	}

	public BTreeNode LChild
	{
		get { return lchild; }
		set { lchild = value; }
	}

	public BTreeNode RChild
	{
		get { return rchild; }
		set { rchild = value; }
	}
}

// invariant: no duplicates in this movie collection
// Some parts are adapted from Workshop 05 code
public class MovieCollection : IMovieCollection
{
	private BTreeNode root; // movies are stored in a binary search tree and the root of the binary search tree is 'root' 
	private int count; // the number of (different) movies currently stored in this movie collection 



	// get the number of movies in this movie colllection 
	// pre-condition: nil
	// post-condition: return the number of movies in this movie collection and this movie collection remains unchanged
	public int Number { get { return count; } }

	// constructor - create an object of MovieCollection object
	public MovieCollection()
	{
		root = null;
		count = 0;	
	}

	// Check if this movie collection is empty
	// Pre-condition: nil
	// Post-condition: return true if this movie collection is empty; otherwise, return false.
	public bool IsEmpty()
	{
		return count == 0;
	}


	// Insert a movie into this movie collection using Binary Insert Algorithm
	// Pre:
	//    recursively try to insert movie to root's Left Child if movie is front of root in dictionary order
	//	  recursively try to insert movie to root's Right Child if movie is behind root in dictionary order
	// Post: nil
	private void Insert(IMovie movie, BTreeNode root)
    {
		// Less than Root 
		if (root.Movie.CompareTo(movie) == 1) {
			if (root.LChild == null)  root.LChild = new BTreeNode(movie);
			else Insert(movie, root.LChild);
        }
		// More than Root
		else {
			if (root.RChild == null)  root.RChild = new BTreeNode(movie);
			else Insert(movie, root.RChild);
        }
    }

	// Insert a movie into this movie collection
	// Pre-condition: nil
	// Post-condition: the movie has been added into this movie collection and return true, if the movie is not in this movie collection; otherwise, the movie has not been added into this movie collection and return false.
	public bool Insert(IMovie movie)
	{
		if (!Search(movie))  {
			if (root == null) root = new BTreeNode(movie);
			else Insert(movie, root);
			count++;
			return true;
        }
		return false;
	}


	// Delete a movie from this movie collection
	// Pre-condition: nil
	// Post-condition: the movie is removed out of this movie collection and return true, if it is in this movie collection; return false, if it is not in this movie collection
	public bool Delete(IMovie movie)
	{
		// Search for movie and its parent
		BTreeNode pointer = root; // search reference
		BTreeNode parent = null; // parent of pointer

		while ((pointer != null) && (movie.CompareTo(pointer.Movie) != 0))
		{
			parent = pointer;
			if (movie.CompareTo(pointer.Movie) == -1)
				pointer = pointer.LChild;
			else
				pointer = pointer.RChild;
		}

		// Found the movie in movie collection
		if (pointer != null) { 
			// case 3: item has two children
			if ((pointer.LChild != null) && (pointer.RChild != null)) {
				// find the right-most node in left subtree of pointer
				if (pointer.LChild.RChild == null) {
					// a special case: the right subtree of pointer.LChild is empty
					pointer.Movie = pointer.LChild.Movie;
					pointer.LChild = pointer.LChild.LChild;
				}
				else {
					BTreeNode p = pointer.LChild;
					BTreeNode pp = pointer; // parent of p
					while (p.RChild != null) {
						pp = p;
						p = p.RChild;
					}
					// copy the item at p to pointer
					pointer.Movie = p.Movie;
					pp.RChild = p.LChild;
				}
			}
			else {
				// cases 1 & 2: item has no or only one child
				BTreeNode c;
				if (pointer.LChild != null)   c = pointer.LChild;
				else   c = pointer.RChild;

				// remove node pointer
				if (pointer == root)   root = c;
				else  {
					if (pointer == parent.LChild)
						parent.LChild = c;
					else
						parent.RChild = c;
				}
			}
			count--;
			return true;
		}
		else return false;
	}


    // Search a movie using Binary Search Algorithm
    // pre: nil
    // post: return an IMovie object if the movie exists in this MovieCollection, null otherwise.
    private IMovie Search(IMovie movie, BTreeNode root)
    {
        if (root != null)
        {
            // Equal Root
            if (root.Movie.CompareTo(movie) == 0) return root.Movie;
            // Less than Root
            else if (root.Movie.CompareTo(movie) == -1) return Search(movie, root.RChild);
            // More than Root
            else return Search(movie, root.LChild);
        }
        return null;
    }

    // Search for a movie in this movie collection
    // pre: nil
    // post: return true if the movie is in this movie collection;
    //	     otherwise, return false.
    public bool Search(IMovie movie)
	{
		bool result = Search(movie, root) == null? false : true;
		return result;
	}


    // Search for a movie by its title in this movie collection  
    // pre: nil
    // post: return the reference of the movie object if the movie is in this movie collection;
    //	     otherwise, return null.
    public IMovie Search(string movietitle)
    {
        IMovie movieToSearch = new Movie(movietitle);
        IMovie result = Search(movieToSearch, root);

        return result;
    }


    // InOrderTraverse to add all movies in dictionary order to 'movies'
    private void InOrderTraverse(BTreeNode root, ref IMovie[] movies, ref int counter)
	{
		if (root != null)
		{
			InOrderTraverse(root.LChild, ref movies, ref counter);
			movies[counter] = root.Movie;
			counter++;
			InOrderTraverse(root.RChild, ref movies, ref counter);
		}
	}


	// Store all the movies in this movie collection in an array in the dictionary order by their titles
	// Pre-condition: nil
	// Post-condition: return an array of movies that are stored in dictionary order by their titles
	public IMovie[] ToArray()
	{
        IMovie[] movies = new IMovie[count];
		int counter = 0;
		InOrderTraverse(root, ref movies, ref counter);
		return movies;
	}


	// Clear this movie collection
	// Pre-condotion: nil
	// Post-condition: all the movies have been removed from this movie collection
	public void Clear()
	{
		root = null;
		count = 0;
	}
}





