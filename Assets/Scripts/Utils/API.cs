using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Utils
{
    public static class API
    {
        private const string BaseUrl = "https://65e1fae2a8583365b317be12.mockapi.io/api/buttons";

        public static async UniTask<string> GetAsync(int id = -1)
        {
            string url = id == -1 ? BaseUrl : $"{BaseUrl}/{id}";
            using UnityWebRequest webRequest = UnityWebRequest.Get(url);
            await webRequest.SendWebRequest();

            if (webRequest.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {webRequest.error}");
                return null;
            }
            else
            {
                return webRequest.downloadHandler.text;
            }
        }

        public static async UniTask<string> PostAsync(string jsonPayload)
        {
            using UnityWebRequest webRequest = new UnityWebRequest(BaseUrl, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            await webRequest.SendWebRequest();

            if (webRequest.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {webRequest.error}");
                return null;
            }
            else
            {
                return webRequest.downloadHandler.text;
            }
        }

        public static async UniTask<string> PutAsync(int id, string jsonPayload)
        {
            using UnityWebRequest webRequest = new UnityWebRequest($"{BaseUrl}/{id}", "PUT");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            await webRequest.SendWebRequest();

            if (webRequest.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {webRequest.error}");
                return null;
            }
            else
            {
                return webRequest.downloadHandler.text;
            }
        }

        public static async UniTask DeleteAsync(int id)
        {
            using UnityWebRequest webRequest = UnityWebRequest.Delete($"{BaseUrl}/{id}");
            await webRequest.SendWebRequest();

            if (webRequest.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {webRequest.error}");
            }
        }
    }
}
