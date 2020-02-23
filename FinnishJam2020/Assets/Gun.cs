using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage = 20f;
    public float range = 100f;
    public float fireRate = 15f;
    public int maxAmmo = 30;
    private int CurrentAmmo;
    private bool IsReload;
    public float reloadTime = 2f;
    public GameObject Camera;
    public GameObject impactEffect;
    public Animator Anim;
    public AudioClip Sound;
    public AudioClip Sound1;
    
    public ParticleSystem P;
    
    private float NextTimeToFire = 0f;

    void Start()
    {
        CurrentAmmo = maxAmmo;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonUp("Fire1") || IsReload)
            P.Stop();
        
        if (IsReload)
            return;
        
        if (CurrentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        
        if (Input.GetButton("Fire1") && Time.time >= NextTimeToFire)
        {
            NextTimeToFire = Time.time + 2f / fireRate;
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        IsReload = true;
        
        gameObject.GetComponent<AudioSource>().clip = Sound;
        gameObject.GetComponent<AudioSource>().Play();
        Anim.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - 0.5f);
        Anim.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.5f);
        CurrentAmmo = maxAmmo;
        
        IsReload = false;
    }
    void Shoot()
    {
        Debug.Log("Av3 lox");
        RaycastHit hit;
        gameObject.GetComponent<AudioSource>().clip = Sound1;
        gameObject.GetComponent<AudioSource>().Play();
        P.Play();
        CurrentAmmo--;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, range))
        {
            // Debug.Log(hit.transform.name);
            Mob target = hit.transform.GetComponent<Mob>();
            if (target != null)
                StartCoroutine(target.TakeDamage(damage));
            GameObject obj = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(obj, 0.8f);
        }
    }
}
