using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Networking.Match;

public class Mob : MonoBehaviour
{
    public GameObject Player;
    public float health = 10f;
    public Animator Anim;
    public float Speed = 4f;
    public bool IsDeadInside = false;

    private Vector3 Move;
    private int kills = 0;
    
    // Update is called once per frame

    private void Start()
    {
        Anim = GetComponent<Animator>();
        Anim.SetBool("IsDead", true);
    }

    public IEnumerator TakeDamage( float amount )
    {
        health -= amount;

        if (health <= 0f && IsDeadInside == false)
        {
            IsDeadInside = true;
            Anim.SetBool("IsDead", false);
            Gun.GunInstance.Kills++;
            Gun.GunInstance.UpdateCounter();
            yield return new WaitForSeconds(5f); 
            Destroy(gameObject, 1f);
        }
    }

    public int GetKills()
    {
        return kills;
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
