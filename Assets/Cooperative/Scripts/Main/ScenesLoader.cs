using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    [SerializeField] private Elevator _startElevator;
    [SerializeField] private Elevator _finishElevator;
    [MinMaxSlider(1, 15)] [SerializeField] private Vector2 _elevatorDelayRange = new Vector2(3, 6);

    private int _currentScene;
    private Initializer _currentInitializer;

    private void Start() 
    {
        StartCoroutine(SceneLoading(1, false));
    }

    public void GoToNextScene() => StartCoroutine(SceneSwitching(_currentScene + 1));

    private IEnumerator SceneLoading(int buildIndex, bool teleport = true)
    {
        _currentScene = buildIndex;
		
		var loadOperation = SceneManager.LoadSceneAsync(_currentScene, LoadSceneMode.Additive);
        yield return new WaitUntil(() => loadOperation.isDone);
        yield return InitializeScene(SceneManager.GetSceneByBuildIndex(buildIndex), teleport);

        _startElevator.Open();
        _finishElevator.Open();
    }

    private IEnumerator SceneSwitching(int buildIndex)
    {
        _startElevator.Close();
        _finishElevator.Close();
        yield return new WaitForSecondsRealtime(1.5f);
        
        Player.Inventory.Clear();
        _currentInitializer.DeinitializeScene();
	
		var unloadOperation = SceneManager.UnloadSceneAsync(_currentScene);
        yield return new WaitUntil(() => unloadOperation.isDone);
        yield return SceneLoading(buildIndex);
    }

    private IEnumerator InitializeScene(Scene scene, bool teleport)
    {
        var delayHalf = Random.Range(_elevatorDelayRange.x, _elevatorDelayRange.y) / 2;
        
		if (teleport) yield return new WaitForSecondsRealtime(delayHalf);

        SceneManager.SetActiveScene(scene);
        _currentInitializer = FindObjectOfType<Initializer>();

        _startElevator.SetAuthor(_currentInitializer.AuthorName);
        _startElevator.transform.position = _currentInitializer.StartElevatorPoint.position;
        _startElevator.transform.rotation = _currentInitializer.StartElevatorPoint.rotation;
        
        if (teleport)
        {
            Player.FPSController.transform.TeleportRelative(_finishElevator.PlayerPoint, _startElevator.PlayerPoint);
        }
        else
        {
            Player.FPSController.transform.position = _startElevator.PlayerPoint.position;
            Player.FPSController.transform.rotation = _startElevator.PlayerPoint.rotation;
        }

        _finishElevator.SetAuthor(_currentInitializer.AuthorName);
        _finishElevator.transform.position = _currentInitializer.FinishElevatorPoint.position;
        _finishElevator.transform.rotation = _currentInitializer.FinishElevatorPoint.rotation;

        _currentInitializer.InitializeScene();
        
        yield return new WaitForSecondsRealtime(delayHalf);
    }
}
