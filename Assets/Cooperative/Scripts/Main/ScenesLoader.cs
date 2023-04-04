using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesLoader : MonoBehaviour
{
    [SerializeField] private Elevator _startElevator;
    [SerializeField] private Elevator _finishElevator;
    [Space]
    [SerializeField] private Image _fadeImage;
    [SerializeField] private Color _fadeColor;
    [SerializeField] private float _fadeInTime;
    [SerializeField] private float _fadeOutTime;

    private int _currentScene;
    private CooperativeInitialization _currentInitializer;

    private void Awake() 
    {
        _fadeImage.color = _fadeColor;
        StartCoroutine(SceneLoading(1));
    }
    public void GoToNextScene() => StartCoroutine(SceneSwitching(_currentScene + 1));

    private IEnumerator SceneLoading(int buildIndex)
    {
        _currentScene = buildIndex;
        yield return SceneManager.LoadSceneAsync(_currentScene, LoadSceneMode.Additive);

        _currentInitializer = FindObjectOfType<CooperativeInitialization>();
        _currentInitializer.InitializeScene();

        _startElevator.SetAuthor(_currentInitializer.AuthorName);
        _startElevator.transform.position = _currentInitializer.StartElevetorPoint.position;
        _startElevator.transform.rotation = _currentInitializer.StartElevetorPoint.rotation;

        _finishElevator.SetAuthor(_currentInitializer.AuthorName);
        _finishElevator.transform.position = _currentInitializer.FinishElevatorPoint.position;
        _finishElevator.transform.rotation = _currentInitializer.FinishElevatorPoint.rotation;

        Player.FPSController.transform.position = _startElevator.PlayerPoint.position;
        Player.FPSController.transform.rotation = _startElevator.PlayerPoint.rotation;

        yield return Fade(_fadeColor, Color.clear, _fadeOutTime);
        
        _startElevator.Open();
        _finishElevator.Open();
    }

    private IEnumerator SceneSwitching(int buildIndex)
    {
        _startElevator.Close();
        _finishElevator.Close();

        yield return Fade(Color.clear, _fadeColor, _fadeInTime);

        _currentInitializer.DeinitializeScene();
        yield return SceneManager.UnloadSceneAsync(_currentScene);
        yield return SceneLoading(buildIndex);
    }

    private IEnumerator Fade(Color from, Color to, float fadeTime)
    {
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            _fadeImage.color = Color.Lerp(from, to, t / fadeTime);
            yield return null;
        }
        _fadeImage.color = to;
    }
}
