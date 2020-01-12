//using System;
//using System.Collections.Generic;

//[Serializable]
//public class User
//{
//    private int Id;
//    private string Name;
//    private string Experience;
//    private string Position;
//    private string Status;
//    private string Info;
//    private string Number;


//    public User(int userID, string username)
//    {
//        this.Id = Id;
//        this.Name = Name;
//        favourites = new List<Item>();
//    }

//    public int GetUserID()
//    {
//        return userID;
//    }

//    public string GetUsername()
//    {
//        return username;
//    }

//    public List<Item> GetFavourites()
//    {
//        return favourites;
//    }

//    //Add an Item to the user's favourites
//    public void addFavourite(Item favouritedItem)
//    {
//        favourites.Add(favouritedItem);
//    }

//    //Returns a formatted versions of the favourites
//    public string formatFavourites()
//    {
//        return string.Join(",", favourites);
//    }

//    public void removeItem(Item item)
//    {
//        favourites.Remove(item);
//    }

//    override
//    public string ToString()
//    {
//        return username;
//    }
//}