using System;
using Unity.VisualScripting;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private bool SameSide;

   
    

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            var playerMask = other.gameObject.GetComponentInChildren<Mask>();
            if (player.transform.localRotation.y <= 0.87 && playerMask.leftGroup.MaskObj.gameObject.activeInHierarchy)
            {
                SameSide = true;
                print("same side left");
                return;
            }
            if (player.transform.localRotation.y >= 0.87 && playerMask.rightGroup.MaskObj.gameObject.activeInHierarchy)
            {
                SameSide = true;
                print("same side right");
                return;
            }
          
            SameSide = false;
            if(!SameSide)   PlayerController.PlayerDeathAction?.Invoke();
            
            
        }
        
    }
}
