using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20; //每次射擊給敵人的傷害
    public float timeBetweenBullets = 0.15f; //間隔時間
    public float range = 100f;


    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask; 
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime; 

        if (Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0) //因為不希望玩家能一直發射 所以要大於 timeBetweenBullets 可攻擊的時間  Time.timeScale != 0 時間不是暫停
        {
            Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position); //設位置在第0個 槍口的位置

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask)) //Physics.Raycast模擬一個射線往前面打
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            gunLine.SetPosition (1, shootHit.point);//雷射槍的終點變成打到的位置
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);//若沒有打到就把雷射槍的終點設在可打到的最大範圍
        }
    }
}
