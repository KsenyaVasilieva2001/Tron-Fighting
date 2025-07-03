using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private MainMenuView _view;
    [SerializeField] private GameObject _tutorialPanel;

    private void OnEnable()
    {
        _view.startButton.onClick.AddListener(HandleStartGame);
        _view.tutorialButton.onClick.AddListener(HandleOpenTutorial);
        _view.exitButton.onClick.AddListener(HandleExit);

        _view.startButton.gameObject.SetActive(true);
        _view.tutorialButton.gameObject.SetActive(true);
        _view.exitButton.gameObject.SetActive(true);
    }

    public void Show()
    {
        _view.gameObject.SetActive(true);
        _view.startButton.gameObject.SetActive(true);
        _view.tutorialButton.gameObject.SetActive(true);
        _view.exitButton.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _view.startButton.gameObject.SetActive(false);
        _view.tutorialButton.gameObject.SetActive(false);
        _view.exitButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _view.startButton.onClick.RemoveListener(HandleStartGame);
        _view.tutorialButton.onClick.RemoveListener(HandleOpenTutorial);
        _view.exitButton.onClick.RemoveListener(HandleExit);
    }

    private void HandleStartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void HandleOpenTutorial()
    {
        _tutorialPanel.SetActive(true);
        Hide();
    }
    private void HandleCloseTutorial()
    {
        _tutorialPanel.SetActive(false);
    }

    private void HandleExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
