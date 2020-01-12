using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewsDisplayPanelBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public static News currentItem;
    public GameObject ItemNameText;
    public GameObject itemPrice;
    public GameObject DescAndSpecsContent;
    public GameObject image;
    private NewsManager itemManager;

    void Start()
    {
        //If not a complete gameobject don't continue
        if (ItemNameText is null)
        {
            return;
        }

        itemManager = GameObject.Find("News Manager").GetComponent<NewsManager>();

        Text name = ItemNameText.GetComponent<Text>();
        name.text = currentItem.GetTitle();


        Sprite imageSprite = Resources.Load<Sprite>($"Thumbnails/{currentItem.GetTitle()}") as Sprite;
        image.GetComponent<Image>().sprite = imageSprite;

        Text descAndSpecs = DescAndSpecsContent.GetComponentInChildren<Text>();
        descAndSpecs.text = "Description: \n\n" + currentItem.GetBriefly();
    }

    public void SetCurrentItem(News item)
    {
        currentItem = item;
        SceneManager.LoadScene("itemInformationView");
    }

    public void NavigateARScene()
    {
        GameObject selectedObject = Resources.Load($"Models/{currentItem.GetTitle()}") as GameObject;

        itemManager.ObjectToPlace = selectedObject;
        SceneManager.LoadScene("ARManipulation");
    }
    // Update is called once per frame
    void Update()
    {

    }


}
