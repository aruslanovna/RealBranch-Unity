﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Data;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    private static ItemManager _instance;
    private string dbURL, sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    private IDataReader reader;
    public GameObject ObjectToPlace;
    public InputField InputFieldName;
    public InputField InputFieldCategory;
    public InputField InputFieldDescr;
    public InputField InputFieldBrand;
    public InputField InputFieldPrice;
    public InputField InputFieldURL;
    public InputField InputFieldDesigner;
    public Image img;
    public List<GameObject> selectableModels;

    private List<Item> itemList = new List<Item>();
    private string[] dbReadOrder = { "Name", "url", "desc", "categories", "brands", "designer", "spec" };
    private static readonly string DatabaseName = "users.s3db"; // Do not change db or the consequences are bad

    public void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void Start()
    {
        //Application database Path android
        string filepath = Application.persistentDataPath + "/" + DatabaseName;

        CreateDBFileIfNotExist(filepath);
        EstablishDBConnection(filepath);
        ReadRecordFromDB();
    }

    private void CreateDBFileIfNotExist(string location)
    {
        if (!File.Exists(location))
        {
            // If not found on android will create Tables and database

            Debug.LogWarning("File \"" + location + "\" does not exist. Attempting to create from \"" +
                             Application.dataPath + "!/assets/users");

            // UNITY_ANDROID
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/users.s3db");
            while (!loadDB.isDone) { }
            // then save to Application.persistentDataPath
            File.WriteAllBytes(location, loadDB.bytes);
        }
    }

    private void EstablishDBConnection(string filePath)
    {
        dbURL = "URI=file:" + filePath;

        Debug.Log("Stablishing connection to: " + dbURL);
        dbconn = new SqliteConnection(dbURL);
        dbconn.Open();

        string query = "CREATE TABLE IF NOT EXISTS item (" +
                                   "ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                   "Name varchar(100), " +
                                   "price REAL," +
                                   "url varchar(30)," +
                                   "desc varchar(200)," +
                                   "categories varchar(200)," +
                                   "brands varchar(100)," +
                                   "designer varchar(50)," +
                                   "spec varchar(100)," +
                                   "noClick INT);";
        try
        {
            dbcmd = dbconn.CreateCommand(); // create empty command
            dbcmd.CommandText = query;
            dbcmd.ExecuteReader(); // execute command which returns a reader
            dbcmd.Dispose();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        dbconn.Close();
    }

    private void ReadRecordFromDB()
    {
        int totalRecordInDB = CountTotalRecordInDB();
        if (totalRecordInDB > 0)
        {
            LoadItemFromDBIntoList();
        }
        else
        {
            PopulateDemoData();
        }
    }

    private int CountTotalRecordInDB()
    {
        int totalRecordInDB = 0;
        using (dbconn = new SqliteConnection(dbURL))
        {
            dbconn.Open();
            IDbCommand dbCommand = dbconn.CreateCommand();
            dbCommand.CommandText = "Select COUNT(*) Total from item;";
            IDataReader dbRecord = dbCommand.ExecuteReader();
            if (dbRecord.Read()) totalRecordInDB = dbRecord.GetInt32(0);
        }

        return totalRecordInDB;
    }

    private void LoadItemFromDBIntoList()
    {
        using (dbconn = new SqliteConnection(dbURL))
        {
            dbconn.Open();
            IDbCommand dbQuery = dbconn.CreateCommand();
            dbQuery.CommandText = "Select * from item;";
            IDataReader dbRecord = dbQuery.ExecuteReader();
            LoadDBRecordFrom(dbRecord);
            dbQuery.Dispose();
        }
    }

    private void LoadDBRecordFrom(IDataReader dbRecord)
    {
        while (dbRecord.Read())
        {
            int itemId = dbRecord.GetInt32(0);
            string name = dbRecord.GetString(1);
            double price = dbRecord.GetFloat(2);
            string itemSiteURL = dbRecord.GetString(3);
            string itemDesc = dbRecord.GetString(4);
            string[] itemCategories = dbRecord.GetString(5).Split(',');
            string itemBrand = dbRecord.GetString(6);
            string itemDesigner = dbRecord.GetString(7);
            int numberClick = dbRecord.GetInt32(9);

            Item newItem = new Item(itemId, name, (float)price, itemSiteURL, itemDesc, numberClick);

            newItem.AddCategory(itemCategories);
            newItem.setNumberOfClick(numberClick);
            newItem.SetBrand(itemBrand);
            newItem.SetDesigner(itemDesigner);
            itemList.Add(newItem);
        }
        dbRecord.Close();
    }
    public void AddItem()
    {
        string filepath = Application.persistentDataPath + "/" + DatabaseName;
        dbURL = "URI=file:" + filepath;

        Debug.Log("Stablishing connection to: " + dbURL);


        using (dbconn = new SqliteConnection(dbURL))
        {
            if (InputFieldName.text != "" && InputFieldPrice.text!="" && InputFieldURL.text != "" && InputFieldDescr.text != "" && InputFieldCategory.text != "" && InputFieldBrand.text != "" && InputFieldDesigner.text != "")
            {
                try
                {
                    dbconn.Open();
                    IDbCommand dbCommand = dbconn.CreateCommand();
                    string insertRecord = string.Format("INSERT into item ( Name, price, url, desc, categories, brands, designer, spec,noClick )" +
                                                              "values (\"{0}\",\"{1}\",\"{2}\", \"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\")",
                                                              InputFieldName.text,
                                                              InputFieldPrice.text,
                                                              InputFieldURL.text,
                                                              InputFieldDescr.text,
                                                              InputFieldCategory.text,

                                                                InputFieldBrand.text,
                                                                 InputFieldDesigner.text,
                                                                InputFieldDesigner.text,0);
                    dbCommand.CommandText = insertRecord;
                    dbCommand.ExecuteScalar();
                    SceneManager.LoadScene("Catalog");
                }
                catch
                {
                    showWrongCredentialMessage();
                }
            }
            else
            {
                showWrongCredentialMessage();
            }
            
        }

       

       
    }
    private void showWrongCredentialMessage()
    {
        InputFieldName.placeholder.GetComponent<Text>().text = "Wrong username or password";
        InputFieldName.text = "";
        InputFieldPrice.placeholder.GetComponent<Text>().text = "Wrong username or password";
        InputFieldPrice.text = "";
        InputFieldURL.placeholder.GetComponent<Text>().text = "Wrong username or password";
        InputFieldURL.text = "";
        InputFieldDescr.placeholder.GetComponent<Text>().text = "Wrong username or password";
        InputFieldDescr.text = "";
        InputFieldCategory.placeholder.GetComponent<Text>().text = "Wrong username or password";
        InputFieldCategory.text = "";
        InputFieldBrand.placeholder.GetComponent<Text>().text = "Wrong username or password";
        InputFieldBrand.text = "";
        InputFieldDesigner.placeholder.GetComponent<Text>().text = "Wrong username or password";
        InputFieldDesigner.text = "";

    }
    private void PopulateDemoData()
    {
        Item chair = new Item(1, "Aloe", 30.00f, "https://realbranch20200107112240.azurewebsites.net", "Adjustable seat height Height adjustable back/lumbar\n Independently adjustable seat tilt - free floating or lockable");
        chair.AddCategory(new string[] { "Flower", "Green", "Growing" });
        chair.SetBrand("Ikea");
        chair.SetDesigner("Ikea");

        itemList.Add(chair);

        Item table = new Item(2, "Blue_fescue", 60.00f, "https://realbranch20200107112240.azurewebsites.net", "Good one");
        table.AddCategory(new string[] { "Flower", "Plant", "Garden" });
        table.SetBrand("Harvey Norman");
        table.SetDesigner("Parkland");
        itemList.Add(table);

        Item couch = new Item(3, "Blue_princess", 90.00f, "https://realbranch20200107112240.azurewebsites.net", "Easy growing");
        couch.AddCategory(new string[] { "Plant", "Water", "Garden" });
        couch.SetBrand("The Warehouse");
        couch.SetDesigner("Living & Co");

        itemList.Add(couch);

        Item chair2 = new Item(4, "Foliage", 90.00f, "https://realbranch20200107112240.azurewebsites.net", "A minimalist white plant");
        chair2.AddCategory(new string[] { "Tree", "Branch" });
        chair2.SetBrand("Harvey Norman");
        chair2.SetDesigner("Luxury Chairs");

        itemList.Add(chair2);

        Item sofa = new Item(5, "Japanese_barberry", 1000.00f, "https://realbranch20200107112240.azurewebsites.net", "Looks big and comfy");
        sofa.AddCategory(new string[] { "Green", "Growing" });
        sofa.SetBrand("Harvey Norman");
        sofa.SetDesigner("Luxury Chairs");

        itemList.Add(sofa);

        AddDemoDataToDB();
    }

    private void AddDemoDataToDB()
    {
        using (dbconn = new SqliteConnection(dbURL))
        {
            dbconn.Open();
            IDbCommand dbCommand = dbconn.CreateCommand();
            foreach (Item item in itemList)
            {
                string itemName = item.GetName();
                string itemDesc = item.GetDesc();
                string sellerURL = item.GetURL();
                float price = item.GetPrice();
                int itemId = item.GetItemID();
                string category = string.Join(",", item.GetCategories());
                string brand = item.GetBrand();
                string designer = item.GetDesigner();
                string specs = item.GetSpecs().ToString();
                int noClick = item.getNumberOfClick();

                string insertRecord = string.Format("INSERT into item (ID, Name, price, url, desc, categories, brands, designer, spec, noClick)" +
                                                        "values (\"{0}\",\"{1}\",\"{2}\", \"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\", \"{9}\")",

                                                        itemId,
                                                      itemName,
                                                        price,
                                                        sellerURL,
                                                        itemDesc,
                                                        category,
                                                        brand,
                                                        designer,
                                                        specs,
                                                        noClick);
                dbCommand.CommandText = insertRecord;
                dbCommand.ExecuteScalar();
            }
            dbCommand.Dispose();
        }
    }
    
    public void updateNumberOfClicks(Item item)
    {
        using (dbconn = new SqliteConnection(dbURL))
        {
            dbconn.Open();
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("UPDATE Item set noClick = @noClick where ID = @id ");

            SqliteParameter P_update_name = new SqliteParameter("@noClick", item.getNumberOfClick());
            SqliteParameter P_update_id = new SqliteParameter("@id", item.GetItemID());

            dbcmd.Parameters.Add(P_update_name);
            dbcmd.Parameters.Add(P_update_id);

            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbcmd.Dispose();
        }
    }
}
