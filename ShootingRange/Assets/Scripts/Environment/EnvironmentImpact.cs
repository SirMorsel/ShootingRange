using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentImpact : BulletImpact
{
    [SerializeField] private GameObject particlesPrefab;
    [SerializeField] private bool isSpecialTarget = false;
    [SerializeField] private bool useObjectColorForImpact = false;
    [SerializeField] private Color defaultColor = Color.black;

    private PlayerController playerController;
    private ControlPanelUI controlPanelUI;

    private void Start()
    {
        playerController = PlayerController.Instance;
        controlPanelUI = ControlPanelUI.Instance;
    }

    public override void OnHit(RaycastHit hit)
    {
        GameObject particles = Instantiate(particlesPrefab, hit.point + (hit.normal * 0.05F), Quaternion.LookRotation(hit.normal), transform.root.parent);
        ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (particleSystem && renderer)
        {
            if (isSpecialTarget)
            {
                particleSystem.transform.localScale = new Vector3(2, 2, 2);
                particleSystem.startColor = Color.blue;
            }
            else
            {
                if (useObjectColorForImpact)
                {
                    particleSystem.startColor = renderer.material.color;
                }
                else
                {
                    particleSystem.startColor = defaultColor;
                }
            }
            controlPanelUI.ShowTargteHitRange(Vector3.Distance(transform.position, playerController.transform.position));
        }
        Destroy(particles, 2F);
    }
}
