using System;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;

public class spawnPoint : MonoBehaviour
{
    [SerializeField] Transform SpawnPointLocation;
    private PlayerController player;

    
    private void Start()
    {
       player = FindFirstObjectByType<PlayerController>(); 
       player.transform.position = SpawnPointLocation.position;
       FindFirstObjectByType<CinemachinePanTilt>().ForceCameraPosition(player.cameraTransform.position, SpawnPointLocation.rotation);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        Handles.DrawLine(transform.position, transform.position + transform.forward * 3);
    }
#endif
   
}
