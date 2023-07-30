using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UI;

public class WinDialog : Dialog{
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(ProgressLoading());
    }
    private IEnumerator ProgressLoading()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(ConstantValues.MENU_SCENE_NAME);
    }
}