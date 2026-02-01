using System;
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
          if (SceneManager.GetActiveScene().buildIndex == 3)
          {
              WinScreen.gameObject.SetActive(true);
              return;
          }
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      }
  }
}
