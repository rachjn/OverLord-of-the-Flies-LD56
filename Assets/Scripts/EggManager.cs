using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EggManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject flyToSpawn;
    [SerializeField]
    private string openClip = "eggOpen";
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        // Debug.LogWarning(anim.clip);
        OpenEgg("Player1");
    }
    public void OpenEgg(string flyTag)
    {
        StartCoroutine(openEgg(flyTag));
    }
    IEnumerator openEgg(string flyTag)
    {
        animator.Play(openClip);
        GameObject fly = Instantiate(flyToSpawn, transform.position + (Vector3.up * 0.25f), quaternion.EulerXYZ(-90, 0, 0));
        fly.tag = flyTag;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.2f);
        Destroy(gameObject);
    }
}
