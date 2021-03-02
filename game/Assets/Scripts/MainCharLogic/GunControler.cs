using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunControler : MonoBehaviour
{
    public LineRenderer LineRenderer;
    public Transform ShootPoint;
    public Animator anim;
    public GameObject[] BloodSprites;
    public GameObject[] ShootEffects;
    public static Weapon TakedGunChar;
    private bool isShooting;
    public bool CanShoot;
    private GameObject GunPrefab;
    private bool HasGun;
    private AudioSource GunShoot;
    private void Start()
    {
        GunPrefab = gameObject.transform.GetChild(0).gameObject;
        isShooting = false;
        TakedGunChar = SetAndGetWeapon—haracteristic(GunPrefab, ref HasGun, ShootPoint, ref GunShoot);
        anim.SetBool("HasGun", HasGun);
    }
    public Weapon SetAndGetWeapon—haracteristic(GameObject weapon, ref bool HasGun, Transform ShootPoint, 
                                                       ref AudioSource audioSource)
    {
        weapon.transform.localScale = new Vector3(1.66666675f, 1.66666675f, 1.66666675f);
        weapon.transform.localRotation = new Quaternion(0, 0, -0.716172636f, 0.697923183f);
        audioSource = weapon.transform.GetChild(1).GetComponent<AudioSource>();
        if (weapon.transform.name.Contains("AK47"))
        {
            weapon.transform.localPosition = Vector3.zero;
            if (ShootPoint != null)
                ShootPoint.localPosition = new Vector3(-3.78699994f, 5.16599989f, 0);
            HasGun = true;
            return new Weapon()
            {
                FullBullets = 200,
                MagazineBullets = 30,
                WeaponRecoil = WeaponRecoilLevel.Medium,
                ShotsPerMinute = 500,
                Damage = 30,
            };
        }
        else if (weapon.transform.name.Contains("Bennelli_M4"))
        {
            weapon.transform.localPosition = new Vector3(0.00200000009f, 0.338999987f, 0);
            if (ShootPoint != null)
                ShootPoint.localPosition = new Vector3(-3.77699995f, 5.14599991f, 0);
            HasGun = true;
            return new Weapon()
            {
                FullBullets = 50,
                MagazineBullets = 5,
                WeaponRecoil = WeaponRecoilLevel.SuperLow,
                ShotsPerMinute = 60,
                Damage = 100,
            };
        }
        else if (weapon.transform.name.Contains("M4_8"))
        {
            weapon.transform.localPosition = new Vector3(-0.330000013f, 0.177000001f, 0);
            if (ShootPoint != null)
                ShootPoint.localPosition = new Vector3(-3.63000011f, 5.17000008f, 0);
            HasGun = true;
            return new Weapon()
            {
                FullBullets = 420,
                MagazineBullets = 25,
                WeaponRecoil = 7,
                ShotsPerMinute = 150,
                Damage = 20,
            };
        }
        else if (weapon.transform.name.Contains("M107"))
        {
            weapon.transform.localPosition = new Vector3(0.00600000005f, 0.238000005f, 0);
            if (ShootPoint != null)
                ShootPoint.localPosition = new Vector3(-4.70900011f, 5.25299978f, 0);
            HasGun = true;
            return new Weapon()
            {
                FullBullets = 420,
                MagazineBullets = 5,
                WeaponRecoil = WeaponRecoilLevel.SuperLow,
                ShotsPerMinute = 60,
                Damage = 100,
            };
        }
        else if (weapon.transform.name.Contains("M249"))
        {
            weapon.transform.localPosition = new Vector3(-0.135000005f, 0.296000004f, 0);
            if (ShootPoint != null)
                ShootPoint.localPosition = new Vector3(-4.41699982f, 5.41800022f, 0);
            HasGun = true;
            return new Weapon()
            {
                FullBullets = 420,
                MagazineBullets = 30,
                WeaponRecoil = WeaponRecoilLevel.High,
                ShotsPerMinute = 1000,
                Damage = 30,
            };
        }
        else if (weapon.transform.name.Contains("Uzi"))
        {
            weapon.transform.localPosition = Vector3.zero;
            if (ShootPoint != null)
                ShootPoint.localPosition = new Vector3(-3.38599992f, 5.16200018f, 0);
            HasGun = false;
            return new Weapon()
            {
                FullBullets = 420,
                MagazineBullets = 25,
                WeaponRecoil = 7,
                ShotsPerMinute = 700,
                Damage = 20,
            };
        }
        else if(weapon.transform.name.Contains("M1911"))
        {
            weapon.transform.localPosition = Vector3.zero;
            if (ShootPoint != null)
                ShootPoint.localPosition = new Vector3(-2.9920001f, 5.09200001f, 0);
            HasGun = false;
            return new Weapon()
            {
                FullBullets = 420,
                MagazineBullets = 25,
                WeaponRecoil = 7,
                ShotsPerMinute = 40,
                Damage = 20,
            };
        }
        return new Weapon();
    }
    public void OnShootButton(bool IsShooting)
    {
        if (!IsShooting)
        {
            anim.SetBool("IsFiring", IsShooting);
        }
        CanShoot = IsShooting;
    }
    void Update()
    {
        if (CanShoot && !isShooting)
        {
            StartCoroutine(Shoot());
        }
    }
    private IEnumerator Shoot()
    {
        isShooting = true;
        anim.SetBool("IsFiring", true);
        anim.SetTrigger("Fire");
        GunShoot.volume = UnityEngine.Random.Range(0.5f, 0.7f);
        GunShoot.PlayOneShot(GunShoot.clip);
        RaycastHit2D hitInfo = Physics2D.Raycast(ShootPoint.position, -ShootPoint.right + new Vector3(0, UnityEngine.Random.Range(-TakedGunChar.WeaponRecoil, TakedGunChar.WeaponRecoil), 0));
        GameObject ShootEffect = Instantiate(ShootEffects[UnityEngine.Random.Range(0, ShootEffects.Length)], 
                                                                                   ShootPoint.parent.transform);
        ShootEffect.transform.localPosition = new Vector3(ShootPoint.transform.localPosition.x, 
                                                          ShootPoint.transform.localPosition.y,
                                                          ShootPoint.transform.localPosition.z + 0.1f);
        if (Mathf.Approximately(ShootPoint.parent.transform.eulerAngles.y, 180))
        {
            ShootEffect.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        GameObject Blood = new GameObject();
        if (hitInfo)
        {
            if (hitInfo.transform.tag == "Enemy")
            {
                EnemyLogic enemy = hitInfo.transform.GetComponent<EnemyLogic>();
                Blood = Instantiate(BloodSprites[UnityEngine.Random.Range(0, BloodSprites.Length)], hitInfo.point, Quaternion.identity);
                Blood.transform.localScale = new Vector3(UnityEngine.Random.Range(0f, 2f), 
                                                         UnityEngine.Random.Range(0f, 2f));
                enemy.GetDamage(TakedGunChar.Damage);
            }
            LineRenderer.SetPosition(0, ShootPoint.position);
            LineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            LineRenderer.SetPosition(0, ShootPoint.position);
            LineRenderer.SetPosition(1, -ShootPoint.right * 100);
        }
        LineRenderer.enabled = true;
        yield return new WaitForSeconds(0.02f);
        Destroy(ShootEffect);
        Destroy(Blood.gameObject);
        LineRenderer.enabled = false;
        yield return new WaitForSeconds(60f / TakedGunChar.ShotsPerMinute);
        isShooting = false;
    }
}
public class Weapon
{
    public int MagazineBullets;
    public int FullBullets;
    public int ShotsPerMinute;
    public float WeaponRecoil;
    public int Damage;
}
public static class WeaponRecoilLevel
{
    public const float SuperLow = 0.06f;
    public const float Low = 0.3f;
    public const float Medium = 1;
    public const float High = 3;
}
