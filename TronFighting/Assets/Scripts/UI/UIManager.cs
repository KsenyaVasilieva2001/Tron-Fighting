using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TutorialController tutorialController;
    [SerializeField] private MainMenuController mainMenuController;

    private void Start()
    {
        tutorialController.OnClosed += HandleTutorialClosed;
    }

    private void HandleTutorialClosed() {
    
        mainMenuController.Show();
    }
}
