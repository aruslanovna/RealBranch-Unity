using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleARCore;


public class LoadModel : MonoBehaviour
{
    public GameObject popularItemPrefab;
    private List<Item> itemList = new List<Item>();
    public GameObject popularItemPanel;
    public List<GameObject> itemListings;
    public GameObject sceneController;
    public GameObject itemSceneController;
    private ItemManager itemManager;

    //Setting the number of popular items to display to 10
    private readonly int MAX_NUMBER_OF_POPULAR_ITEMS = 5;
    private readonly int FONT_SIZE = 30;
    // Start is called before the first frame update
    void Start()
    {
      
        itemListings = new List<GameObject>();

        sceneController = new GameObject();
        sceneController.AddComponent<ARSceneController>();

        itemSceneController = new GameObject();
        itemSceneController.AddComponent<ItemDisplayPanelBehaviour>();
        PopulateDemoData();
        updatePopularItems();
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void PopulateDemoData()
    {
        Item chair = new Item(1, "Chair222", 30.00f, "https://www.trademe.co.nz/business-farming-industry/office-furniture/desk-chairs/listing-2356237609.htm?rsqid=3512401f2c8a4cffad08f4acf7c7ab30-001", "Adjustable seat height Height adjustable back/lumbar\n Independently adjustable seat tilt - free floating or lockable");
        chair.AddCategory(new string[] { "Office", "Chairs", "Desks" });
        chair.SetBrand("Ikea");
        chair.SetDesigner("Ikea");

        itemList.Add(chair);

        Item table = new Item(2, "Table", 60.00f, "https://www.trademe.co.nz/business-farming-industry/office-furniture/desk-chairs/listing-2357653157.htm?rsqid=148a18ec29374beeafeeab8f14940dcc-001", "Good stuff");
        table.AddCategory(new string[] { "Living Room", "Couches", "Lounge" });
        table.SetBrand("Harvey Norman");
        table.SetDesigner("Parkland");
        itemList.Add(table);

        Item couch = new Item(3, "Couch", 90.00f, "https://google.com", "Cautions: heavy stuff");
        couch.AddCategory(new string[] { "Living Room", "Tables", "Dining Room", "Office" });
        couch.SetBrand("The Warehouse");
        couch.SetDesigner("Living & Co");

        itemList.Add(couch);

        Item chair2 = new Item(4, "Chair2", 90.00f, "https://www.HarveyNorman.com", "A minimalist white chair");
        chair2.AddCategory(new string[] { "Living Room", "Dining Room", "Chairs" });
        chair2.SetBrand("Harvey Norman");
        chair2.SetDesigner("Luxury Chairs");

        itemList.Add(chair2);

        Item sofa = new Item(5, "Sofa", 1000.00f, "https://www.HarveyNorman.com", "Looks big and comfy");
        sofa.AddCategory(new string[] { "Living Room", "Dining Room", "Couches" });
        sofa.SetBrand("Harvey Norman");
        sofa.SetDesigner("Luxury Chairs");

        itemList.Add(sofa);


    }


    public void updatePopularItems()
    {
        itemList.Sort();

        int numberOfPopularItems;


        // Check if the item list is less than the max number of popular items
        // If it is greater than the max number of items make it equal the max number
        if (itemList.Count < MAX_NUMBER_OF_POPULAR_ITEMS)
        {
            numberOfPopularItems = itemList.Count;
        }
        else
        {
            numberOfPopularItems = MAX_NUMBER_OF_POPULAR_ITEMS;
        }

        GameObject content = GameObject.Find("Content");

        for (int i = 0; i < numberOfPopularItems; ++i)
        {
            GameObject itemListing = Instantiate(popularItemPrefab);
            Item itemInList = itemList[i];

            itemListing.SetActive(false);

            itemListing.GetComponentInChildren<Text>().text = itemInList.GetName(); //Set Titletext
            itemListing.GetComponentInChildren<Text>().fontSize = FONT_SIZE;
            itemListing.name = $"Listing: {itemInList.GetItemID()} {itemInList.GetName()}"; //Set name of gameobject

            Sprite thumbnailSprite = Resources.Load<Sprite>($"Thumbnails/{ itemInList.GetName()}") as Sprite;
            itemListing.transform.Find("Thumbnail").GetComponent<Image>().sprite = thumbnailSprite; //Set thumbnail

            itemListing.transform.Find("Preview Button").GetComponent<Button>().onClick.AddListener(() => NavigateToARScene(itemInList.GetName())); //Make previewbutton go to ARScene
            itemListing.transform.Find("Info Button").GetComponent<Button>().onClick.AddListener(() => NavigateToInfoScene(itemInList)); //Make info button go to info scene

            itemListing.transform.SetParent(content.transform, false); //Set listing parent

            //set size of listing
            GameObject scrollView = GameObject.Find("Scroll View");
            RectTransform rt = itemListing.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(400, 390);

            itemListings.Add(itemListing);
            showListing(itemInList);
            itemListing.SetActive(true);

        }
    }

    private void showListing(Item listing)
    {
        GameObject content = GameObject.Find("Content");
        GameObject toShow = content.transform.Find($"Listing: { listing.GetItemID()} { listing.GetName()}").gameObject;

        toShow.SetActive(true);
    }

    private void NavigateToInfoScene(Item itemToShow)
    {
        Debug.Log("Navigate to infoscene");
        UpdateItemManagerModels();
        itemSceneController.GetComponent<ItemDisplayPanelBehaviour>().SetCurrentItem(itemToShow);
    }

    private void NavigateToARScene(string name)
    {
        GameObject selectedObject = Resources.Load($"Models/{name}") as GameObject;
        sceneController.GetComponent<ARSceneController>().ChangeObjectToPlace(selectedObject);

        itemManager.ObjectToPlace = selectedObject;
        UpdateItemManagerModels();
        SceneManager.LoadScene("ARManipulation");
    }

    private void UpdateItemManagerModels()
    {
        List<GameObject> newVisibles = new List<GameObject>();

        foreach (GameObject listing in itemListings)
        {
            string modelName;
            string[] GameObjectName = listing.name.Split(' ');

            if (GameObjectName[0] == "Listing:")
            {
                modelName = GameObjectName[2];
                GameObject itemModel = Resources.Load($"Models/{modelName}") as GameObject;

                newVisibles.Add(itemModel);

            }
            else
            {
                Debug.Log("Listing was not an itemListing");
            }
        }

        itemManager.selectableModels = newVisibles;
    }
}
