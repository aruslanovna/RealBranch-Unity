using System;
using System.Collections.Generic;

[Serializable]
public class News 
{
    private int Id;
    private string Title;
    private int UserId;
    private string Briefly;
    private string Article;
    private List<string> Category = new List<string>();
    private List<string> Hashtag = new List<string>();


   


    public News(int Id, string Title, string Briefly, string Article)
    {
        this.Id = Id;
        this.Title = Title;
        this.Briefly = Briefly;
        this.Article =Article;
        
      
       
    }

    public int GetId()
    {
        return Id;
    }

  
    public string GetTitle()
    {
        return Title;
    }

   
    public string GetBriefly()
    {
        return Briefly;
    }

    public string GetArticle()
    {
        return Article;
    }

    public List<string> GetCategories()
    {
        return Category;
    }

    public void AddCategory(string category)
    {
        Category.Add(category);
    }

    public void AddCategory(string[] categoryArray)
    {
        foreach (string category in categoryArray)
        {
            Category.Add(category);
        }
    }
    public List<string> GetHashtag()
    {
        return Hashtag;
    }

   

  

    public void AddHashtag(string[] categoryArray)
    {
        foreach (string category in categoryArray)
        {
            Hashtag.Add(category);
        }
    }


    override
    public string ToString()
    {
        return Title;
    }

    override
    public Boolean Equals(Object obj)
    {
        News compareItem = (News) obj;
        return (Id == compareItem.Id);
    }

   
}
