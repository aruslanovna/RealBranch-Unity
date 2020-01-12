//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;
//using UnityEngine;

//public class UserDB : MonoBehaviour
//{
//    private string dbURL, sqlQuery;
//    IDbConnection dbconn;
//    IDbCommand dbcmd;
//    private IDataReader reader;
//    public GameObject ObjectToPlace;
//    public List<GameObject> selectableModels;

//    private List<Item> itemList = new List<Item>();
//    private string[] dbReadOrder = { "Name", "url", "desc", "categories", "brands", "designer", "spec" };
//    private static readonly string DatabaseName = "users.s3db"; // Do not change db or the consequences are bad

//    public void Start()
//    {
//        //Application database Path android
//        string filepath = Application.persistentDataPath + "/" + DatabaseName;

//        CreateDBFileIfNotExist(filepath);
//        EstablishDBConnection(filepath);
//        ReadRecordFromDB();
//    }

//    private void CreateDBFileIfNotExist(string location)
//    {
//        if (!File.Exists(location))
//        {
//            // If not found on android will create Tables and database

//            Debug.LogWarning("File \"" + location + "\" does not exist. Attempting to create from \"" +
//                             Application.dataPath + "!/assets/users");

//            // UNITY_ANDROID
//            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/users.s3db");
//            while (!loadDB.isDone) { }
//            // then save to Application.persistentDataPath
//            File.WriteAllBytes(location, loadDB.bytes);
//        }
//    }
//    private void EstablishDBConnection(string filePath)
//    {
//        dbURL = "URI=file:" + filePath;

//        Debug.Log("Stablishing connection to: " + dbURL);
//        dbconn = new SqliteConnection(dbURL);
//        dbconn.Open();

//        string query = "CREATE TABLE IF NOT EXISTS item (" +
//                                   "ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
//                                   "Name varchar(100), " +
//                                   "price REAL," +
//                                   "url varchar(30)," +
//                                   "desc varchar(200)," +
//                                   "categories varchar(200)," +
//                                   "brands varchar(100)," +
//                                   "designer varchar(50)," +
//                                   "spec varchar(100)," +
//                                   "noClick INT);";
//        try
//        {
//            dbcmd = dbconn.CreateCommand(); // create empty command
//            dbcmd.CommandText = query;
//            dbcmd.ExecuteReader(); // execute command which returns a reader
//            dbcmd.Dispose();
//        }
//        catch (Exception e)
//        {
//            Debug.Log(e);
//        }
//        dbconn.Close();
//    }

//    private void ReadRecordFromDB()
//    {
//        int totalRecordInDB = CountTotalRecordInDB();
//        if (totalRecordInDB > 0)
//        {
//            LoadItemFromDBIntoList();
//        }
//        else
//        {
//            PopulateDemoData();
//        }
//    }

//    private int CountTotalRecordInDB()
//    {
//        int totalRecordInDB = 0;
//        using (dbconn = new SqliteConnection(dbURL))
//        {
//            dbconn.Open();
//            IDbCommand dbCommand = dbconn.CreateCommand();
//            dbCommand.CommandText = "Select COUNT(*) Total from item;";
//            IDataReader dbRecord = dbCommand.ExecuteReader();
//            if (dbRecord.Read()) totalRecordInDB = dbRecord.GetInt32(0);
//        }

//        return totalRecordInDB;
//    }

//    private void LoadItemFromDBIntoList()
//    {
//        using (dbconn = new SqliteConnection(dbURL))
//        {
//            dbconn.Open();
//            IDbCommand dbQuery = dbconn.CreateCommand();
//            dbQuery.CommandText = "Select * from item;";
//            IDataReader dbRecord = dbQuery.ExecuteReader();
//            LoadDBRecordFrom(dbRecord);
//            dbQuery.Dispose();
//        }
//    }

//    private void LoadDBRecordFrom(IDataReader dbRecord)
//    {
//        while (dbRecord.Read())
//        {
//            int Id = dbRecord.GetInt32(0);
//            string Name = dbRecord.GetString(1);
//            string Email = dbRecord.GetString(2);
//            string Experience = dbRecord.GetString(3);
//            string Status = dbRecord.GetString(4);
//            string Info = dbRecord.GetString(5);
//            string Position = dbRecord.GetString(6);
//            string Number = dbRecord.GetString(7);


//            User newItem = new Item(Id, Name, Email, Experience, numberClick);

//            newItem.AddCategory(itemCategories);
//            newItem.setNumberOfClick(numberClick);
//            newItem.SetBrand(itemBrand);
//            newItem.SetDesigner(itemDesigner);
//            itemList.Add(newItem);
//        }
//        dbRecord.Close();
//    }
//}
