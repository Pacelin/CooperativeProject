using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    [SerializeField] private Elevator _elevator;

    private int _currentScene;
    private CooperativeInitialization _currentInitializer;

    private void Awake()
    {
        LoadScene(1);
    }

    public void GoToNextScene()
    {
        UnloadLastScene();
    }

    private void LoadScene(int buildIndex)
    {
        _currentScene = buildIndex;
        var op = SceneManager.LoadSceneAsync(_currentScene, LoadSceneMode.Additive);
        op.completed += OnLoadScene;
    }

    private void UnloadLastScene()
    {
        _currentInitializer.DeinitializeScene();
        var op = SceneManager.UnloadSceneAsync(_currentScene);
        op.completed += OnUnloadLastScene;
    }

    private void OnUnloadLastScene(AsyncOperation op)
    {
        LoadScene(_currentScene + 1);
    }

    private void OnLoadScene(AsyncOperation op)
    {
        _currentInitializer = FindObjectOfType<CooperativeInitialization>();
        _currentInitializer.InitializeScene();
        
        _elevator.SetStartPoint(_currentInitializer.StartLiftPosition);
        _elevator.SetFinishPoint(_currentInitializer.FinishLiftPosition);
        _elevator.SetAuthor(_currentInitializer.AuthorName);

        _elevator.MoveToStartPoint();
        Player.FPSController.transform.position = _elevator.PlayerPoint.position;
    }
}
