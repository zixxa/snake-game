using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UI;
using CustomEventBus;
public class LoadingDialog : Dialog{
    private EventBus _eventBus;
    [SerializeField] private List<Image> stages;
    private IEnumerator _wait;
    private PauseController _pauseController;
    private void Start()
    {
        _pauseController = ServiceLocator.Current.Get<PauseController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _wait = ProgressLoading();
        StartCoroutine(_wait);
    }
    private IEnumerator ProgressLoading()
    {
        foreach (Image stage in stages)
        {
            yield return new WaitForSeconds(0.2f);
            stage.gameObject.SetActive(true); 
        }
        Hide();
        _pauseController.SetPaused(false);
    }

    private void Update()
    {
        Debug.Log("${_pauseController.IsPaused}");
    }
}