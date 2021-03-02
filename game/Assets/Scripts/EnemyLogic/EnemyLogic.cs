using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public Transform MainCharPos;
    public Transform GunObj;
    public Transform FirePoint;
    private float speed;
    private int DistanceToShoot;
    private int HP;
    private bool IsMainCharRight;
    private bool CanSeeMainChar;
    private Animator anim;
    private AudioSource ShootAudio;
    private Weapon TakedWeaponChar;
    private bool IsFiring;
    void Start()
    {
        IsFiring = false;
        anim = GetComponent<Animator>();
        SetDifficultSettings();
        UpdateMainCharPlace();
        CanSeeMainChar = false;
        ShootAudio = GunObj.GetChild(0).transform.GetChild(1).GetComponent<AudioSource>();
    }

    private void SetDifficultSettings()
    {
        switch(GameSettings.Difficulty)
        {
            case 0:
                {
                    TakedWeaponChar = new Weapon()
                    {
                        Damage = 20,
                        FullBullets = 100,
                        MagazineBullets = 20,
                        ShotsPerMinute = 150,
                        WeaponRecoil = WeaponRecoilLevel.SuperLow,
                    };
                    speed = 3;
                    DistanceToShoot = 7;
                    HP = 70;
                    break;
                }
            case 1:
                {
                    TakedWeaponChar = new Weapon()
                    {
                        Damage = 25,
                        FullBullets = 100,
                        MagazineBullets = 20,
                        ShotsPerMinute = 80,
                        WeaponRecoil = WeaponRecoilLevel.Low,
                    };
                    speed = 5;
                    DistanceToShoot = 9;
                    HP = 100;
                    break;
                }
            case 2:
                {
                    TakedWeaponChar = new Weapon()
                    {
                        Damage = 30,
                        FullBullets = 100,
                        MagazineBullets = 30,
                        ShotsPerMinute = 120,
                        WeaponRecoil = WeaponRecoilLevel.Medium,
                    };
                    speed = 6;
                    DistanceToShoot = 16;
                    HP = 150;
                    break;
                }
        }
    }

    private void UpdateMainCharPlace()
    {
        if (transform.position.x - MainCharPos.position.x > 0) IsMainCharRight = false;
        else
        {
            IsMainCharRight = true;
        }
    }

    private void Update()
    {
        UpdateMainCharPlace();
        CheckVision();
        if (!CanSeeMainChar)
        {
            anim.SetBool("IsRunning", true);
            if (IsMainCharRight)
            {
                if (transform.localEulerAngles.y != 180) transform.localEulerAngles = new Vector3(0, 180, 0); 
                transform.localPosition += new Vector3(speed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.identity;
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            if (!IsFiring) StartCoroutine(Shoot());
            if (IsMainCharRight)
            {
                if (transform.eulerAngles.y != 180) transform.Rotate(new Vector3(0, 180, 0));
            }
            else
            {
                if (transform.rotation != Quaternion.identity) transform.rotation = Quaternion.identity;
            }
        }
    }

    private void CheckVision()
    {
        if (IsMainCharRight)
        {
            if (MainCharPos.position.x - transform.position.x <= DistanceToShoot)
            {
                CanSeeMainChar = true;
            }
            else
            {
                CanSeeMainChar = false;
            }
        }
        else
        {
            if (transform.position.x - MainCharPos.position.x <= DistanceToShoot)
            {
                CanSeeMainChar = true;
            }
            else
            {
                CanSeeMainChar = false;
            }
        }
    }

    private IEnumerator Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(FirePoint.position, -FirePoint.right);
        if (hit)
        {
            if (hit.transform.tag == "Player")
            {
                MainCharController player = hit.transform.GetComponent<MainCharController>();
                player.TakeDamage(TakedWeaponChar.Damage);
            }
        }
        IsFiring = true;
        ShootAudio.PlayOneShot(ShootAudio.clip);
        anim.SetBool("IsRunning", false);
        anim.SetTrigger("Fire");
        yield return new WaitForSeconds(60f / TakedWeaponChar.ShotsPerMinute);
        IsFiring = false;
    }
    public void GetDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0) Death();
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
