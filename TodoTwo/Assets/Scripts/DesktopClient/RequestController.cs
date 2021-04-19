using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RequestController : MonoBehaviour
{
    private static string responseData;

    public static string GetResponseData() => responseData;

    public static IEnumerator PostRequest(string relativePath, byte[] jsonData, string accessToken)
    {
        string data = System.Text.Encoding.UTF8.GetString(jsonData);
        //78.56.76.51:8080/api/v0.1/
        //http://ec2-52-59-205-209.eu-central-1.compute.amazonaws.com/api/v0.1/
        using (UnityWebRequest www = UnityWebRequest.Put("http://ec2-52-59-205-209.eu-central-1.compute.amazonaws.com/api/v0.1/" + relativePath, jsonData))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");
            if (accessToken != "")
            {
                www.SetRequestHeader("authorization", "Bearer " + accessToken);
                Debug.Log(accessToken);
            }
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                responseData = www.downloadHandler.text;
                Debug.Log(responseData);
            }

        }
    }

    public static IEnumerator GetRequest(string relativePath, string authorization)
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://ec2-52-59-205-209.eu-central-1.compute.amazonaws.com/api/v0.1/" + relativePath))
        {
            Debug.Log(authorization);

            //www.useHttpContinue = false;
            www.SetRequestHeader("authorization", "Bearer " + authorization);
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("GET complete!");
                responseData = www.downloadHandler.text;
                Debug.Log(responseData);
            }

        }
    }

    public static IEnumerator PutRequest(string relativePath, byte[] jsonData, string accessToken)
    {
        string data = System.Text.Encoding.UTF8.GetString(jsonData);
        using (UnityWebRequest www = UnityWebRequest.Put("http://ec2-52-59-205-209.eu-central-1.compute.amazonaws.com/api/v0.1/" + relativePath, jsonData))
        {
            www.method = "PUT";
            //www.useHttpContinue = false;
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("authorization", "Bearer " + accessToken);
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                responseData = www.downloadHandler.text;
                Debug.Log(responseData);
            }

        }
    }
    public static IEnumerator DeleteRequest(string relativePath, string accessToken)
    {
        using (UnityWebRequest www = UnityWebRequest.Delete("http://ec2-52-59-205-209.eu-central-1.compute.amazonaws.com/api/v0.1/" + relativePath))
        {
            //www.useHttpContinue = false;
            www.SetRequestHeader("authorization", "Bearer " + accessToken);
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("delete complete!");
                //responseData = www.downloadHandler.text;
                //Debug.Log(responseData);
            }

        }
    }
}
