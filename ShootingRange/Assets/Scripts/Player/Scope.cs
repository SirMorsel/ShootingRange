using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scope : MonoBehaviour
{
    private Animator animator;
    private GameObject currentWeapon;

    private float startZoom = 60F;
    private float maxZoomIn = 0;

    [SerializeField] private Camera fpsCamera;
    [SerializeField] private GameObject scopeOverlay;
    [SerializeField] private TMP_Text zoomFactorText;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        currentWeapon = transform.GetChild(0).gameObject;
        maxZoomIn = CalcMaxZoomIn(startZoom, (int)(currentWeapon.GetComponent<WeaponManager>().GetWeaponMaxScopeZoom() * 0.5F));
    }

    void Update()
    {
        ScopeInOut();
        ZoomInOut();
    }

    private void ScopeInOut()
    {
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("IsScoped", !animator.GetBool("IsScoped"));

            if (animator.GetBool("IsScoped"))
            {
                StartCoroutine(OnScoped());
            }
            else
            {
                OnUnscoped();
                fpsCamera.fieldOfView = startZoom;
            }
        }
        zoomFactorText.SetText($"{startZoom / fpsCamera.fieldOfView}x");
    }

    private void ZoomInOut()
    {
        if (animator.GetBool("IsScoped"))
        {
            if (Input.mouseScrollDelta.y > 0f && fpsCamera.fieldOfView > maxZoomIn)
            {
                // Zoom In
                fpsCamera.fieldOfView *=  0.5F;
            }
            if (Input.mouseScrollDelta.y < 0f && fpsCamera.fieldOfView < startZoom)
            {
                // Zoom Out
                fpsCamera.fieldOfView *= 2;
            }
        }
    }

    private void OnUnscoped()
    {
        scopeOverlay.SetActive(false);
        currentWeapon.GetComponent<MeshRenderer>().enabled = true;
    }

    private IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(0.15F);
        currentWeapon.GetComponent<MeshRenderer>().enabled = false;
        scopeOverlay.SetActive(true);
    }

    private float CalcMaxZoomIn(float initalZoom, int maxZoomIn)
    {
        float calculatedMaxZoomIn = initalZoom;
        for (int i = 0; i < maxZoomIn - 1; i++)
        {
            calculatedMaxZoomIn *= 0.5F;
        }
        return calculatedMaxZoomIn;
    }
}
