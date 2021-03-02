using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainCharController : MonoBehaviour
{
    
    public float speed = 5f;
    public Slider HPSlider;
    public Slider ArmorSlider;
    public AudioSource WalkingSound;
    public GameObject CrossHair;
    public bool IsDropping;
    private int HP;
    private int Armor;
    private Animator anim;
    private bool isRight;
    private bool IsRunning;
    private bool moveToRight;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isRight = false;
        SetDifficultSettings();
        SetGameSettings();
    }

    
    void Update()
    {
        if (IsRunning)
        {
            if (!moveToRight)
            {
                if (isRight) Flip();
                anim.SetBool("IsRunning", IsRunning);
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            }
            else if (moveToRight)
            {
                if (!isRight) Flip();
                anim.SetBool("IsRunning", IsRunning);
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            anim.SetBool("IsRunning", IsRunning);
        }
    }

    public void TakeDamage(int damage)
    {
        if (Armor > 0)
        {
            Armor -= damage - 5;
        }
        else
        {
            HP -= damage;
        }
        if (HP <= 0) Death();
        HPSlider.value = HP; 
        ArmorSlider.value = Armor;
    }

    private void Death()
    {

    }

    public void Stay()
    {
        IsRunning = false;
    }
    public void Move(bool Right)
    {
        if (IsDropping) return;
        IsRunning = true;
        moveToRight = Right;
    }
    private void SetGameSettings()
    {
        if (GameSettings.CrossHair == "OFF") CrossHair.SetActive(false);
    }
    private void SetDifficultSettings()
    {
        if (GameSettings.Difficulty == 0)
        {
            HP = 150;
            Armor = 150;
            speed = 8;
        }
        else if (GameSettings.Difficulty == 1)
        {
            HP = 100;
            Armor = 100;
            speed = 6;
        }
        else if (GameSettings.Difficulty == 2)
        {
            HP = 70;
            Armor = 70;
            speed = 5;
        }
        HPSlider.maxValue = HP;
        HPSlider.value = HP;
        ArmorSlider.maxValue = Armor;
        ArmorSlider.value = Armor;
    }
    private void Flip()
    {
        transform.Rotate(new Vector3(0, 180, 0));
        isRight = !isRight;
    }
}
