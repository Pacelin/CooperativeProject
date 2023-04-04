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

        _startElevator.button.CanInteract = false;
        _finishElevator.button.CanInteract = true;
        _startElevator.button.HideInteraction = true;
        _finishElevator.button.HideInteraction = false;

        yield return new WaitForSeconds(Random.Range(3, 10)); // a little bit if waiting time
        
        _startElevator.Open();
        _finishElevator.Open();
    }

    private IEnumerator SceneSwitching(int buildIndex)
    {
        _startElevator.Close();
        _finishElevator.Close();

        yield return new WaitForSeconds(Random.Range(3, 10)); // a little bit if waiting time

        _currentInitializer.DeinitializeScene();
        yield return SceneManager.UnloadSceneAsync(_currentScene);

        (_startElevator,_finishElevator) = (_finishElevator, _startElevator);

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
