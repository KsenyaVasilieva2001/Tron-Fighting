using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    [SerializeField] private EndGameView _view;
    public void Show(string text)
    {
        _view.gameObject.SetActive(true);
        _view.text.text = text;
    }
}
