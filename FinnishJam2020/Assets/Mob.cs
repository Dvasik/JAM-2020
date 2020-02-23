using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking.Match;

public class Mob : MonoBehaviour
{
    public GameObject Player;
    public float health = 10f;
    public Animator Anim;
    public float Speed = 4f;

    private Vector3 Move;
    // Update is called once per frame

    private void Start()
    {
        Anim = GetComponent<Animator>();
        Anim.SetBool("IsDead", true);
    }

    public IEnumerator TakeDamage( float amount )
    {
        health -= amount;

        if (health <= 0f)
        {
            Anim.SetBool("IsDead", false);
            yield return new WaitForSeconds(5f); 
            Destroy(gameObject, 1f);
        }
    }
    void Update()
    {
        Move = new Vector3(Player.transform.position.x - gameObject.transform.position.x,
                           0, Player.transform.position.z - gameObject.transform.position.z);
        gameObject.transform.LookAt(Player.transform.position);
        if (health <= 0f)
            Speed = 0;  
        gameObject.transform.position += Move.normalized * Time.deltaTime * Speed;
    }

    void OnCollisionEnter( Collision InCollsion )
    {
        if (InCollsion.gameObject.name == "Capsule" && health > 0f)
            Destroy(Player);
    }
}
