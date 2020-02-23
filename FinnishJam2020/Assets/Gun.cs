using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    public Text KillCounter, AmmoCounter;
    public static Gun GunInstance; // Gun instance

    private float NextTimeToFire = 0f;
    public int Kills = 0;

    void Start()
    {
        if (GunInstance == null)
            GunInstance = this;

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
        AmmoCounter.text = "Ammo: " + CurrentAmmo.ToString();
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
            {
                StartCoroutine(target.TakeDamage(damage));
                //Kills += target.GetKills();
            }

            Debug.Log(Kills);

            GameObject obj = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(obj, 0.8f);
        }
    }

    public void UpdateCounter()
    {
        KillCounter.text = "Kills: " + Kills.ToString();
    }
}