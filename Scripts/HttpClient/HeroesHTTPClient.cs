using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;
using TMPro;

public class HeroesHTTPClient : MonoBehaviour
{
    [System.Serializable]
    private class AuthResponse
    {
        public string access_token;
        public string token_type;
        public string name;
    }

    [System.Serializable]
    private class RegisterPayload
    {
        public string name;
        public string email;
        public string password;
        public string password_confirmation;
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
    [Inject]
    private TimeManager _timeManager;

    [SerializeField]
    private TMP_InputField _emailInput;
    [SerializeField]
    private TMP_InputField _passwordInput;
    [SerializeField]
    private TMP_InputField _nameInput;

    [SerializeField]
    private TextMeshProUGUI _playerNameText;

    [SerializeField] private GameObject _nameInputGroup;
    [SerializeField] private GameObject _loginButton;
    [SerializeField] private GameObject _registerButton;
    [SerializeField] private GameObject _switchLoginButton;
    [SerializeField] private GameObject _switchRegisterButton;

    public void SwitchLogin()
    {
        _nameInputGroup.SetActive(false);
        _registerButton.SetActive(false);
        _loginButton.SetActive(true);

        _switchLoginButton.SetActive(false);
        _switchRegisterButton.SetActive(true);
    }

    public void SwitchRegister()
    {
        _nameInputGroup.SetActive(true);
        _registerButton.SetActive(true);
        _loginButton.SetActive(false);

        _switchLoginButton.SetActive(true);
        _switchRegisterButton.SetActive(false);
    }

    public void TrySave()
    {
        _gameSave = new GameSave(
            _mapObjectsCollector.GetGroups(), 
            _mapObjectsCollector.GetTowns(),
            _timeManager.TimeModel);

        StartCoroutine(SaveCoroutine());
    }

    public void TryLoad()
    {
        StartCoroutine(LoadLastCoroutine());
    }

    public void TryLogin()
    {
        //StartCoroutine(Login("che@gmail.com", "password123"));
        StartCoroutine(Login(_emailInput.text, _passwordInput.text));
    }

    public void TryRegister()
    {
        //StartCoroutine(Login("che@gmail.com", "password123"));
        StartCoroutine(
            Register(
                _nameInput.text, 
                _emailInput.text, 
                _passwordInput.text, 
                _passwordInput.text)
            );
    }

    private IEnumerator SaveCoroutine()
    {
        //string jsonData = JsonUtility.ToJson(new GameSavePayload(_playerName, _gameSave));

        string saveJson = JsonUtility.ToJson(_gameSave);
        string jsonData = JsonUtility.ToJson(new GameSavePayload(_playerNameText.text, saveJson));

        Debug.Log($"{URL}/game-save");

        // Create the request
        UnityWebRequest request = new UnityWebRequest($"{URL}/gamesave", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Content-Type", "application/json");

        string token = PlayerPrefs.GetString("AuthToken", "");
        if (!string.IsNullOrEmpty(token))
        {
            request.SetRequestHeader("Authorization", $"Bearer {token}");
        }
        else
        {
            Debug.LogWarning("Auth token not found. Save request may be rejected.");
        }

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

        string token = PlayerPrefs.GetString("AuthToken", "");
        if (!string.IsNullOrEmpty(token))
        {
            request.SetRequestHeader("Authorization", $"Bearer {token}");
        }
        else
        {
            Debug.LogWarning("Auth token not found. Save request may be rejected.");
        }

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            _gameSave = JsonUtility.FromJson<GameSavePayload>(jsonResponse).GetSaveData();

            _mapController.SpawnMapGroups(_gameSave);
            _mapController.RestoreTowns(_gameSave);
            _timeManager.TimeModel = _gameSave.Time;

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
            _playerNameText.text = authData.name;
        }
        else
        {
            Debug.LogError($"Login failed: {request.error}");
        }
    }

    public IEnumerator Register(string name, string email, string password, string passwordConfirmation)
    {
        var payload = new RegisterPayload
        {
            name = name,
            email = email,
            password = password,
            password_confirmation = passwordConfirmation
        };

        string jsonData = JsonUtility.ToJson(payload);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest($"{URL}/register", "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Registration successful!");

            string jsonResponse = request.downloadHandler.text;
            AuthResponse authData = JsonUtility.FromJson<AuthResponse>(jsonResponse);

            PlayerPrefs.SetString("AuthToken", authData.access_token);
            Debug.Log($"Registered and saved token: {authData.access_token}");

            _playerNameText.text = payload.name;
        }
        else
        {
            Debug.LogError("Registration failed: " + request.error);
            Debug.Log($"Response: {request.downloadHandler.text}");
        }
    }

}
