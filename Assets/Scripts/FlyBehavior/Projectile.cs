using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float lerpDuration = 0.25f;
    private float lerpTimer = 0f;
    private Vector3 lerpStart;
    // Start is called before the first frame update
    void Start()
    {
        lerpStart = transform.position;
    }

    public HealthManager Target;

    // Update is called once per frame
    void FixedUpdate()
    {
        lerpTimer += Time.deltaTime;
        if (Target == null)
        {
            if (lerpTimer > lerpDuration) Destroy(gameObject);
            return;
        }
        // no target -> attack fails.
        if (lerpTimer >= lerpDuration)
        {
            Target.TakeDamage(damage);
            Destroy(gameObject);
        }
        Debug.Log(transform);
        transform.position = Vector3.Lerp(lerpStart, Target.transform.position, lerpTimer/lerpDuration) + Vector3.up * (float) Math.Sin(Math.PI * lerpTimer / lerpDuration);
    }
}
