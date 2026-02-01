using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    PlayerController player;
    [SerializeField] private float FOV;
    [SerializeField] private float TurnSpeed;
    [SerializeField] private float SightRange = 10;
    [SerializeField] private LayerMask dectectionLayer;
    [SerializeField] private float chargeTime;
    private float curChargeTime;
    private float angle;
    private bool canSeePlayer;
    private Vector3 targetDir;
    
    LineRenderer lineRenderer;
    [SerializeField]Transform barrelTransform;
    private Vector3 hitPos;
    private bool shot;
    private bool OnCD;
    AudioSource audioSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        curChargeTime = chargeTime;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = target.GetComponent<PlayerController>();
        lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(FOVCheck());
    }

    private IEnumerator FOVCheck()
    {
        while (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(0.1f);
            FovMath();
        }
    }

    private void FovMath()
    {
        targetDir = target.position - transform.position;
        angle = Vector3.Angle(targetDir, transform.forward);
        if (!Physics.Raycast(transform.position, targetDir, out var hit, SightRange, dectectionLayer)) return;
        
        if (hit.collider.gameObject.CompareTag("Player") && angle < FOV / 2)
        {
            print("Player spotted ");
            hitPos = hit.point;
            canSeePlayer = true;
        }
        else
        {
//            print("Player not spotted ");
            canSeePlayer = false;
        }
    }

    
    // Credit goes to https://github.com/Comp3interactive/FieldOfView/blob/main/FieldOfViewEditor.cs
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 viewAngle01 = DirectionFromAngle(transform.eulerAngles.y, -FOV / 2);
        Vector3 viewAngle02 = DirectionFromAngle(transform.eulerAngles.y, FOV / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(transform.position, transform.position + viewAngle01 * SightRange);
        Handles.DrawLine(transform.position, transform.position + viewAngle02 * SightRange);

        if (canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(transform.position, target.transform.position);
        }
    }

#endif
   

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
    
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void Update()
    {
        if (canSeePlayer)
        {
            var targetDirection = Vector3.RotateTowards(transform.forward, targetDir, TurnSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(targetDirection);
            StartCoroutine( prepTurret());
            if(!audioSource.isPlaying)audioSource.PlayOneShot(audioSource.clip);

        }
        else
        {
            StopCoroutine(prepTurret());
        }
    }

    private IEnumerator prepTurret()
    {
        if(!canSeePlayer) yield break;
        yield return new WaitForSeconds(chargeTime);
        if (canSeePlayer && !shot && !OnCD) StartCoroutine(FireTurret());
         
    }

    private IEnumerator FireTurret()
    {
        shot = true;
        lineRenderer.enabled = true;
        if (shot)
        {
            lineRenderer.SetPosition(0, barrelTransform.position);
            lineRenderer.SetPosition(1, hitPos );
            if (Physics.Raycast(transform.position, targetDir, out var hit, SightRange, dectectionLayer))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                   PlayerController.PlayerDeathAction?.Invoke();
                }
            }

        }
        yield return new WaitForSeconds(chargeTime);
        if(!OnCD) StartCoroutine(EnforceCD());
        shot = false;
        lineRenderer.enabled = false;

    }

    private IEnumerator EnforceCD()
    {
        OnCD = true;
        yield return new WaitForSeconds(chargeTime);
        OnCD = false;
    }
}