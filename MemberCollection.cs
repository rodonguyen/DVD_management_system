//CAB301 assessment 1 - 2022
//The implementation of MemberCollection ADT
using System;
using System.Linq;


class MemberCollection : IMemberCollection
{
    // Fields
    private int capacity;
    private int count;
    private Member[] members; //make sure members are sorted in dictionary order

    // Properties

    // get the capacity of this member colllection 
    // pre-condition: nil
    // post-condition: return the capacity of this member collection and this member collection remains unchanged
    public int Capacity { get { return capacity; } }

    // get the number of members in this member colllection 
    // pre-condition: nil
    // post-condition: return the number of members in this member collection and this member collection remains unchanged
    public int Number { get { return count; } }

   


    // Constructor - to create an object of member collection 
    // Pre-condition: capacity > 0
    // Post-condition: an object of this member collection class is created

    public MemberCollection(int capacity)
    {
        if (capacity > 0)
        {
            this.capacity = capacity;
            members = new Member[capacity];
            count = 0;
        }
    }

    // check if this member collection is full
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is full; otherwise return false.
    public bool IsFull()
    {
        return count == capacity;
    }

    // check if this member collection is empty
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is empty; otherwise return false.
    public bool IsEmpty()
    {
        return count == 0;
    }

    // Add a new member to this member collection
    // Pre-condition: this member collection is not full
    // Post-condition: a new member is added to the member collection and the members are sorted in ascending order by their full names;
    // No duplicate will be added into this the member collection
    public void Add(IMember member)
    {
        if (!IsFull())     
        {
            if (!Search(member))
            {
                //System.Console.WriteLine($"Adding member {member.ToString()}...");
                for (int i = 0; i < count + 1; i++)
                {
                    // Reach the position after the final member OR no member exists so assign to the first element
                    if (count == i)
                        members[i] = (Member)member;
                    // If member should be front of members[i] (-1),
                    // move all elements to the back by 1, starting from the tail
                    else if (member.CompareTo(members[i]) == -1)
                    {
                        for (int j = count - 1; j > i - 1; j--)
                            members[j + 1] = members[j];
                        members[i] = (Member)member;
                        break;
                    }
                }
                count++;
                Console.WriteLine($"  Member {member.ToString()} is added to the system.");
            }
            else
                Console.WriteLine($"  Member {member.ToString()} already exists in the system.");
        }
        else
            Console.WriteLine("  Maximum number of member was achieved. No more members can be added.");
    }

    // Remove a given member out of this member collection
    // Pre-condition: nil
    // Post-condition: the given member has been removed from this member collection, if the given member was in the member collection
    public void Delete(IMember aMember)
    {
        if (Search(aMember))     {
            int position = count-1;
            for (int i = 0; i < count; i++)
                if (aMember.CompareTo(members[i]) == 0)
                {
                    position = i;
                    break;
                }
            
            for (int i = position; i < count-1; i++)
                members[i] = members[i+1];  
            
            members[count - 1] = null;
            count--;

            Console.WriteLine("  Member {0} is successfully removed.", aMember.ToString());
        } else
            Console.WriteLine("  Member {0} cannot be deleted as they do not exist in the system", aMember.ToString());

    }

    // Search a given member in this member collection 
    // Pre-condition: nil
    // Post-condition: return true if this memeber is in the member collection; return false otherwise; member collection remains unchanged
    public bool Search(IMember member)
    {
        if (IsEmpty()) return false;

        int min = 0;
        int max = count-1;
        while (min <= max)
        {
            int mid = (min + max) / 2;
            if (member.CompareTo(members[mid]) == 0)
                return true;
            else if (member.CompareTo(members[mid]) == 1)
                min = mid + 1;
            else
                max = mid - 1;
        }
        return false;
    }

    // Find a given member in this member collection 
    // Pre-condition: nil
    // Post-condition: return the reference of the member object in the member collection, if this member is in the member collection; return null otherwise; member collection remains unchanged
    public IMember Find(IMember member)
    {
        if (IsEmpty()) return null;

        int min = 0;
        int max = count - 1;
        while (min <= max)
        {
            int mid = (min + max) / 2;
            if (member.CompareTo(members[mid]) == 0)
                return members[mid];
            else if (member.CompareTo(members[mid]) == 1)
                min = mid + 1;
            else
                max = mid - 1;
        }
        return null;
    }


    // Remove all the members in this member collection
    // Pre-condition: nil
    // Post-condition: no member in this member collection 
    public void Clear()
    {
        for (int i = 0; i < count; i++)
        {
            this.members[i] = null;
        }
        count = 0;
    }

    // Return a string containing the information about all the members in this member collection.
    // The information includes last name, first name and contact number in this order
    // Pre-condition: nil
    // Post-condition: a string containing the information about all the members in this member collection is returned
    public string ToString()
    {
        string s = "    ";
        for (int i = 0; i < count; i++)
            s = s + members[i].ToString() + "\n    ";
        return s;
    }
}


