using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLookAtPlayer : MonoBehaviour
{
    GameObject player;

    private void OnEnable()
    {
        player = GameManager.instance.player;
    }

    private void Update()
    {
        Vector3 aimDirection = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
