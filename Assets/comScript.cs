using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class comScript : MonoBehaviour
{

    public string ip = "192.168.0.53";
    
    void Start()
    {

    }


    public void sendHardAttract(string strength, string direction, string time)
    {
        UnityWebRequest request = UnityWebRequest.Get("http://" + ip + "/" + "hard/" + strength + "/" + direction+ "/"+time);
        StartCoroutine(sendRequest(request));
    }

    public void sendSoftAttract(string strength, string waitTime, string raiseValue, string direction)
    {
        UnityWebRequest request = UnityWebRequest.Get("http://" + ip + "/" + "soft/" + strength + "/" + waitTime + "/" + raiseValue + "/" + direction);
        StartCoroutine(sendRequest(request));
    }
    
    public void sendVibrate(string strength, string waitTime, string noOfRounds, string direction)
    {
        UnityWebRequest request = UnityWebRequest.Get("http://" + ip + "/" + "vibrate/" + strength + "/" + waitTime + "/" + noOfRounds + "/" + direction);
        StartCoroutine(sendRequest(request));
    }

        public void sendSineVibrate(string strength, string waitTime, string noOfRounds, string direction)
    {
        UnityWebRequest request = UnityWebRequest.Get("http://" + ip + "/" + "sine/" + strength + "/" + waitTime + "/" + noOfRounds + "/" + direction);
        StartCoroutine(sendRequest(request));
    }


    IEnumerator sendRequest(UnityWebRequest request)
    {
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            // Show results as text
            Debug.Log("Commonication established");

            // Or retrieve results as binary data
            byte[] results = request.downloadHandler.data;
        }
    }
}