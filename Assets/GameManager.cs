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
       StartCoroutine(goalCameraActive());
    }

    private IEnumerator goalCameraActive()
    { 
       goalCamera.gameObject.SetActive(true);
       player.PanningDisabled();
       yield return new WaitForSeconds(2f);
       goalCamera.gameObject.SetActive(false);
       player.PanningEnabled();
    }
}
