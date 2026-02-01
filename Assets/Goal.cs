using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    [SerializeField] Image WinScreen;
  private void OnTriggerEnter(Collider other)
  {
      if (other.CompareTag("Player"))
      {
          PlayerPrefs.SetInt("SceneLoaded", 0);
          if (SceneManager.GetActiveScene().buildIndex == 3)
          {
              Cursor.lockState = CursorLockMode.None;
              SceneManager.LoadScene("Win");
              return;
          }

          StartCoroutine(LoadNext());
      }
  }

  private IEnumerator LoadNext()
  {
      PlayerPrefs.SetInt("SceneLoaded", 0);
      yield return new WaitForSeconds(0.1f);
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }
}
