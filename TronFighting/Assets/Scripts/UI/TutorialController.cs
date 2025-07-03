using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private TutorialView _view;
    public event Action OnClosed;

    public void Close()
    {
        gameObject.SetActive(false);
        OnClosed?.Invoke();
    }


    private void OnEnable()
    {
        _view.closeButton.onClick.AddListener(HandleCloseTutorial);
    }

    private void OnDisable()
    {
        _view.closeButton.onClick.RemoveListener(HandleCloseTutorial);
    }

    private void HandleCloseTutorial()
    {
        Close();
    }
}