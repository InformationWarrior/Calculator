using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
namespace Calculator
{
    public class NetworkManager : MonoBehaviour
    {
        private const string url = "http://localhost:8080/calculate";

        public void SendRequest(int num1, int num2, string operation)
        {
            StartCoroutine(SendRequestCoroutine(2, 3, "plus"));
        }

        private IEnumerator SendRequestCoroutine(int num1, int num2, string operation)
        {
            CalculationRequest request = new CalculationRequest { num1 = num1, num2 = num2, operation = operation };

            string jsonData = JsonUtility.ToJson(request);

            UnityWebRequest webRequest = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            print("Request sent to server...");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                print("Result = " + webRequest.downloadHandler.text);
                CalculationResponse response = JsonUtility.FromJson<CalculationResponse>(webRequest.downloadHandler.text);
                HandleServerResponse(response);
            }

            else
            {
                Debug.LogError("Error in result");
            }
        }

        private void HandleServerResponse(CalculationResponse response)
        {
            print("Response = " + response.result);
        }
    }
}