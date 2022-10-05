using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    int damage;
    [SerializeField]
    float range = 1f;
    [SerializeField]
    float initialRange = 1f;
    public bool isFriendly;

    private void OnEnable()
    {
        range = initialRange;// * GameManager.instance.rangeMultiplier;
    }
    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        range -= Time.deltaTime;
        if(range < 0f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isFriendly && collision.gameObject.tag=="Player")
        {
            return;
        }
        if(!isFriendly && collision.gameObject.tag=="Enemy")
        {
            return;
        }
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            collision.gameObject?.GetComponent<Health>().
                ChangeHP(
                    -damage //? -(int)(GameManager.instance.damageMultiplier * damage) : -damage
                );
        }
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        range = .5f;
    }
}
