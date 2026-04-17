using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Button _scene1;
    [SerializeField] private Button _scene2;

    private void Start()
    {
        _scene1.onClick.AddListener(()=> CustomSceneManager.Instance.LoadSceneByIndex(1));
        _scene2.onClick.AddListener(()=> CustomSceneManager.Instance.LoadSceneByIndex(2));
    }
}
