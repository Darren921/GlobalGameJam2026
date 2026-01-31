using UnityEngine;

public class Mask : MonoBehaviour
{
    private PlayerController _playerController;
    
    [SerializeField] private GameObject leftProt;
    [SerializeField] private GameObject rightProt;

    private int LastVal;
    void Start()
    {
        PlayerController.PlayerMaskAction += UseMask;   
    }

    private void UseMask(float val)
    {
        Debug.Log(val);
        switch (val)
        {
           case -1:
               SetMaskActive(leftProt,rightProt);
               break;
           case 1:
               SetMaskActive(rightProt,leftProt);
               break;
           default:
               Debug.LogError($"{val} is not a valid value");
               break;
        }
    }

    private void SetMaskActive(GameObject maskActive, GameObject maskInactive )
    {
        maskActive.SetActive(!maskActive.activeSelf);
        if(maskInactive.activeSelf) maskInactive.SetActive(false);
    }
    
}
