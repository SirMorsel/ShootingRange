using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private ControlPanelUI controlPanelUI;

    private float bulletSpeed;
    private float gravityOnBullet;
    private Vector2 windOnBullet;
    private Vector3 startPosition;
    private Vector3 startForward;

    private bool isInitialized = false;
    private float startTime = -1;

    private void Start()
    {
        controlPanelUI = ControlPanelUI.Instance;
    }

    private void FixedUpdate()
    {
        if (isInitialized)
        {
            if (startTime < 0)
            {
                startTime = Time.time;
            }

            RaycastHit hit;
            float currentTime = Time.time - startTime;
            float prevTime = currentTime - Time.time;
            float nextTime = currentTime + Time.fixedDeltaTime;

            Vector3 currentPoint = FindPointOnParable(currentTime);
            Vector3 nextPoint = FindPointOnParable(nextTime);

            if (prevTime > 0)
            {
                Vector3 prevPoint = FindPointOnParable(prevTime);
                if (HitSomethingBetweenPoint(prevPoint, currentPoint, out hit))
                {
                    OnHit(hit);
                }
            }
            if (HitSomethingBetweenPoint(currentPoint, nextPoint, out hit))
            {
                OnHit(hit);
            }
        }
    }

    private void Update()
    {
        if (!isInitialized || startTime < 0) return;
        float currentTime = Time.time - startTime;
        Vector3 currentPoint = FindPointOnParable(currentTime);
        transform.position = currentPoint;
    }

    public void Initialize(Transform startPoint, float speed, float gravity, Vector2 wind)
    {
        startPosition = startPoint.position;
        startForward = startPoint.forward.normalized;
        bulletSpeed = speed;
        gravityOnBullet = gravity;
        windOnBullet = wind;
        isInitialized = true;
        startTime = -1F;
    }

    private Vector3 FindPointOnParable(float time)
    {
        Vector3 movementVector = (startForward * bulletSpeed * time);
        Vector3 windVector = new Vector3(windOnBullet.x, 0, windOnBullet.y) * time * time * 0.5f;
        Vector3 gravityVector = Vector3.down * gravityOnBullet * time * time;
        return startPosition + movementVector + gravityVector + windVector;
    }

    private bool HitSomethingBetweenPoint(Vector3 startPoint, Vector3 endPoint, out RaycastHit hit)
    {
        return Physics.Raycast(startPoint, endPoint - startPoint, out hit, (endPoint - startPoint).sqrMagnitude);
    }

    private void OnHit(RaycastHit hit)
    {
        BulletImpact bulletImpact = hit.transform.GetComponent<BulletImpact>();
        if (bulletImpact)
        {
            bulletImpact.OnHit(hit);
            controlPanelUI.SetImpactPosition(transform.InverseTransformPoint(hit.transform.position));
        }
        Destroy(gameObject);
    }
}
