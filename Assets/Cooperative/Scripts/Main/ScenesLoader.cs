using System.Data.SqlTypes;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesLoader : MonoBehaviour
{
    [SerializeField] private Elevator _startElevator;
    [SerializeField] private Elevator _finishElevator;
    [SerializeField] private float _elevatorDelay;

    private int _currentScene;
    private CooperativeInitialization _currentInitializer;

    private void Awake() 
    {
        StartCoroutine(SceneLoading(1, true));
    }
    public void GoToNextScene() => StartCoroutine(SceneSwitching(_currentScene + 1));

    private IEnumerator SceneLoading(int buildIndex, bool first = false)
    {
        _currentScene = buildIndex;
        yield return SceneManager.LoadSceneAsync(_currentScene, LoadSceneMode.Additive);

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(buildIndex));
        _currentInitializer = FindObjectOfType<CooperativeInitialization>();

        _startElevator.SetAuthor(_currentInitializer.AuthorName);
        _startElevator.transform.position = _currentInitializer.StartElevatorPoint.position;
        _startElevator.transform.rotation = _currentInitializer.StartElevatorPoint.rotation;

        _finishElevator.SetAuthor(_currentInitializer.AuthorName);
        _finishElevator.transform.position = _currentInitializer.FinishElevatorPoint.position;
        _finishElevator.transform.rotation = _currentInitializer.FinishElevatorPoint.rotation;
        
        if (first)
        {
            Player.FPSController.transform.position = _startElevator.PlayerPoint.position;
            Player.FPSController.transform.rotation = _startElevator.PlayerPoint.rotation;
        }
        else
        {
            Player.FPSController.transform.TeleportRelative(_finishElevator.PlayerPoint, _startElevator.PlayerPoint);
        }

        _currentInitializer.InitializeScene();
        
        yield return new WaitForSecondsRealtime(_elevatorDelay);

        _startElevator.Open();
        _finishElevator.Open();
    }

    private IEnumerator SceneSwitching(int buildIndex)
    {
        _startElevator.Close();
        _finishElevator.Close();
        _currentInitializer.DeinitializeScene();

        yield return new WaitForSecondsRealtime(_elevatorDelay);
        yield return SceneManager.UnloadSceneAsync(_currentScene);
        yield return SceneLoading(buildIndex);
    }
}
