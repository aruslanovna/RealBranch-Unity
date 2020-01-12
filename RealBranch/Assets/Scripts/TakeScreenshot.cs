using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour
{
    [SerializeField]
    GameObject blink;

    public void TakeAShot()
    {
        StartCoroutine("TakeScreenshotAndSave");
    }

    IEnumerator TakeScreenshotAndSave()
    {

        yield return null;
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
        yield return new WaitForEndOfFrame();
        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
        //Takes Image from Screen
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();
        //Convert to png
        byte[] imageBytes = screenImage.EncodeToJPG();

        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "Screenshot" + timeStamp + ".jpg";
        string pathToSave = Application.persistentDataPath + "/" + fileName;
        File.WriteAllBytes(pathToSave, imageBytes);
        //Save image to gallery
        // NativeGallery.SaveImageToGallery(imageBytes, fileName, fileName, null);
        yield return new WaitForEndOfFrame();
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
        ScreenCapture.CaptureScreenshot(pathToSave);
        yield return new WaitForEndOfFrame();
        Instantiate(blink, new Vector2(0f, 0f), Quaternion.identity);

        //yield return null;
        //GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
        //yield return new WaitForEndOfFrame();
        //Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
        ////Takes Image from Screen
        //screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        //screenImage.Apply();
        ////Convert to png
        //byte[] imageBytes = screenImage.EncodeToPNG();

        //string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        //string fileName = "Screenshot" + timeStamp + ".png";

        ////Save image to gallery
        //NativeGallery.SaveImageToGallery(imageBytes, "DCIM/Screenshots", fileName, null);
        //yield return new WaitForEndOfFrame();
        //GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
    }

    //IEnumerator TakeScreenshotAndSave()
    //{
    //    yield return null;
    //    GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
    //    yield return new WaitForEndOfFrame();
    //    Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
    //    //Takes Image from Screen
    //    screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
    //    screenImage.Apply();
    //    //Convert to png
    //    byte[] imageBytes = screenImage.EncodeToPNG();

    //    string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
    //    string fileName = "Screenshot" + timeStamp + ".png";
    //    string pathToSave = Application.persistentDataPath + "/" + fileName;
    //    //Save image to gallery
    //    NativeGallery.SaveImageToGallery(imageBytes, fileName, fileName, null);
    //    yield return new WaitForEndOfFrame();
    //    GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
    //    ScreenCapture.CaptureScreenshot(pathToSave);
    //    yield return new WaitForEndOfFrame();
    //    Instantiate(blink, new Vector2(0f, 0f), Quaternion.identity);

    //}
}
//    public void TakeAShot()
//    {
//        StartCoroutine("TakeScreenshotAndSave");
//    }
//    [SerializeField]
//    GameObject blink;
//    IEnumerator TakeScreenshotAndSave()
//    {
//        yield return null;
//        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
//        yield return new WaitForEndOfFrame();
//        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
//        //Takes Image from Screen
//        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
//        screenImage.Apply();
//        //Convert to png
//        byte[] imageBytes = screenImage.EncodeToPNG();

//        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
//        string fileName = "Screenshot" + timeStamp + ".png";
//        string pathToSave = "DCIM/Screenshots" + "/"+fileName; 
//        //Save image to gallery
//        NativeGallery.SaveImageToGallery(imageBytes, fileName, fileName, null);
//        yield return new WaitForEndOfFrame();
//        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
//        ScreenCapture.CaptureScreenshot(pathToSave);
//        yield return new WaitForEndOfFrame();
//        Instantiate(blink, new Vector2(0f, 0f), Quaternion.identity);
//    }
//}
