using System;
using UnityEngine;
using UnityEngine.UI;

public class Mask : MonoBehaviour
{
    private PlayerController _playerController;

    [SerializeField] internal ImageGroup leftGroup;
    [SerializeField] internal ImageGroup rightGroup;
    internal ImageGroup lastGroup;
    void Start()
    {
        PlayerController.PlayerMaskAction += UseMask;   
    }

    private void OnDisable()
    {
        PlayerController.PlayerMaskAction -= UseMask;   

    }

    [Serializable]
    internal class ImageGroup
    {
        public GameObject MaskImage;
        public GameObject MaskObj;
    }
   
    private void UseMask(float val)
    {
        Debug.Log(val);
        switch (val)
        {
           case -1:
               SetMaskActive(leftGroup, rightGroup);
               break;
           case 1:
               SetMaskActive(rightGroup, leftGroup);

               break;
           default:
               Debug.LogError($"{val} is not a valid value");
               break;
        }
    }

    private void SetMaskActive(ImageGroup activeGroup, ImageGroup inactiveGroup)
    {

        if (lastGroup == activeGroup)
        {
            activeGroup.MaskObj.SetActive(false);
            activeGroup.MaskImage.SetActive(false);
            lastGroup = null;
            return;
        }
        activeGroup.MaskObj.SetActive(true);
        activeGroup.MaskImage.SetActive(true);
        
        inactiveGroup.MaskImage.SetActive(false);
        inactiveGroup.MaskObj.SetActive(false);

        lastGroup = activeGroup;


    }

   
    
}
