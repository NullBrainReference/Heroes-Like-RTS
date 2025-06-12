using System.Collections;
using System.Text;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;
using static System.Net.WebRequestMethods;

public class HeroesHTTPClient : MonoBehaviour
{
    [System.Serializable]
    private class AuthResponse
    {
        public string access_token;
        public string token_type;
    }

    private const string URL = @"http://localhost:8000/api";

    [SerializeField]
    private string _playerName;
    [SerializeField]
    private GameSave _gameSave;
    [Inject]
    private MapObjectsCollector _mapObjectsCollector;
    [Inject]
    private MapController _mapController;

    public void TrySave()
    {
        _gameSave = new GameSave(
            _mapObjectsCollector.GetGroups(), 
            _mapObjectsCollector.GetTowns());

        StartCoroutine(SaveCoroutine());
    }

    public void TryLoad()
    {
        StartCoroutine(LoadLastCoroutine());
    }

    public void TryLogin()
    {
        StartCoroutine(Login("che@gmail.com", "password123"));
    }

    private IEnumerator SaveCoroutine()
    {
        //string jsonData = JsonUtility.ToJson(new GameSavePayload(_playerName, _gameSave));

        string saveJson = JsonUtility.ToJson(_gameSave);
        string jsonData = JsonUtility.ToJson(new GameSavePayload(_playerName, saveJson));

        Debug.Log($"{URL}/game-save");

        // Create the request
        UnityWebRequest request = new UnityWebRequest($"{URL}/gamesave", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Game save successful!");
        }
        else
        {
            Debug.LogError("Error saving game: " + request.error); 
        }

        //Debug.Log(request.ToString());

        Debug.Log($"json is: {jsonData}");
    }

    public IEnumerator LoadLastCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get($"{URL}/gamesave/last");
        
        request.SetRequestHeader("Accept", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            _gameSave = JsonUtility.FromJson<GameSavePayload>(jsonResponse).GetSaveData();

            _mapController.SpawnMapGroups(_gameSave);

            Debug.Log("Game loaded successfully!");
            Debug.Log($"json response is: {jsonResponse}");
        }
        else
        {
            Debug.LogError("Error loading game: " + request.error);
        }
       
    }

    public IEnumerator Login(string email, string password)
    {
        // Create JSON request body
        string jsonData = $"{{\"email\":\"{email}\",\"password\":\"{password}\"}}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        // Create POST request
        UnityWebRequest request = new UnityWebRequest($"{URL}/login", "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Login successful!");

            // Parse token from response
            string jsonResponse = request.downloadHandler.text;
            AuthResponse authData = JsonUtility.FromJson<AuthResponse>(jsonResponse);

            Debug.Log($"Token: {authData.access_token}");

            // Store token for future API requests
            PlayerPrefs.SetString("AuthToken", authData.access_token);
        }
        else
        {
            Debug.LogError($"Login failed: {request.error}");
        }
    }
}
