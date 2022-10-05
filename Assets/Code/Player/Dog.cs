using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    DogCircle dogCircle;
    [SerializeField]
    bool canBounce;
    private void Start()
    {
        animator = GetComponent<Animator>();
        canBounce = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if(!canBounce)
            {
                return;
            }
            canBounce = false;
            collision.GetComponent<Ennemy>().Bounce();
            animator.SetBool("Ghost", true);
            StartCoroutine(ReturnToNormal());
        }
    }

    IEnumerator ReturnToNormal()
    {
        dogCircle.ResetTimer();
        yield return new WaitForSeconds(8.0f);
        animator.SetBool("Ghost", false);
        canBounce = true;
    }
}
