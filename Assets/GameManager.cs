using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject goalCamera;
    [SerializeField] GameObject playerCamera;
    [SerializeField]private PlayerController player;
    private void Awake()
    {
    }

    private void Start()
    {
       if(PlayerPrefs.GetInt("SceneLoaded") == 0)  StartCoroutine(goalCameraActive());
       else playerCamera.SetActive(true);
    }

    private IEnumerator goalCameraActive()
    {
        PlayerPrefs.SetInt("SceneLoaded", 1);
       goalCamera.gameObject.SetActive(true);
       yield return new WaitForSeconds(2f);
       goalCamera.gameObject.SetActive(false);
       playerCamera.gameObject.SetActive(true);

    }
}
