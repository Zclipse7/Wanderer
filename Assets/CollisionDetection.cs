using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.Progress;

public class CollisionDetection : MonoBehaviour
{
    public TextMeshProUGUI textM;    
  
    void Start()
    {
        innit();
        StartCoroutine(deleteCollectedItems("http://localhost/Module2/deleteData.php"));
    }

    void Update()
    {
        StartCoroutine(retrieveCollectedItems("http://localhost/Module2/FetchData.php"));
    }

    private void innit()
    {
        textM = GameObject.FindWithTag("text").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        switch (collision.gameObject.tag)
        {
            case "banana":
                Destroy(collision.gameObject);
                StartCoroutine(storeCollectedItem("http://localhost/Module2/collectData.php", "banana", "10"));
                break;

            case "cherry":
                Destroy(collision.gameObject);
                StartCoroutine(storeCollectedItem("http://localhost/Module2/collectData.php", "cherry", "20"));
                break;

            case "orange":
                Destroy(collision.gameObject);
                StartCoroutine(storeCollectedItem("http://localhost/Module2/collectData.php", "orange", "30"));
                break;
            case "diamond":
                Destroy(collision.gameObject);
                StartCoroutine(storeCollectedItem("http://localhost/Module2/collectData.php", "diamond", "30"));
                break;
            case " emerald":
                Destroy(collision.gameObject);
                StartCoroutine(storeCollectedItem("http://localhost/Module2/collectData.php", "emerald", "20"));
                break;
            case "topaz":
                Destroy(collision.gameObject);
                StartCoroutine(storeCollectedItem("http://localhost/Module2/collectData.php", "topaz", "10"));
                break;
            default:
                break;


        }

    }

    IEnumerator storeCollectedItem(string url, string item, string points)
    {
        WWWForm form = new WWWForm();
        form.AddField("item", item);
        form.AddField("points", points);
   

        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }

        

    }

    IEnumerator retrieveCollectedItems(string url)
    {
        WWWForm form = new WWWForm();
        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            textM.text = "Score: " + uwr.downloadHandler.text;
        }
    }

    /*
    IEnumerator retrieveSpecificItems(string url, string tag)
    {
        WWWForm form = new WWWForm();
        form.AddField("tag", tag);

        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            var text = uwr.downloadHandler.text;
            string[] power= text.Split(' ');
 
        }
    }*/

  

    IEnumerator deleteCollectedItems(string url)
    {
        WWWForm form = new WWWForm();
        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
    }
}





