using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI;
using CustomEventBus;
using CustomEventBus.Signals;
public class LoadingDialog : Dialog{
    private EventBus _eventBus;
    [SerializeField] private List<Image> stages;
    private IEnumerator _wait;
    private void Start()
    {
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
    }
}