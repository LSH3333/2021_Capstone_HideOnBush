using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_ChickGrows : MonoBehaviour
{
    // mushroom 섭취에 따라 증가 
    // L_EatMushrooms.cs
    [HideInInspector]
    public int eatenMushrooms = 0;
    // 이 수치만큼 chick의 크기 커짐 
    public float growAmount = 0.1f;

    // mushroom eating sound
    public AudioSource mushEatSound;
    // 버섯을 먹으면 L_EatMushrooms.cs 에서 true로만듦
    [HideInInspector]
    public bool soundPlay;

    private void Update()
    {
        //Debug.Log("eatenMushrooms : " + eatenMushrooms);
        Grows();
        MushEatSound();
    }

    // chick 성장 
    void Grows()
    {
        // mushroom 2개 먹을때마다 성장 
        if(eatenMushrooms > 1)
        {
            //Debug.Log("GROW");
            eatenMushrooms = 0;
            // growAmount만큼 성장 
            transform.localScale += new Vector3(growAmount, growAmount, growAmount); 
        }
    }

    // 버섯먹을때 사운드 
    void MushEatSound()
    {
        // true면 사운드를 플레이 한후 
        if(soundPlay)
        {
            mushEatSound.Play();
            // bool을 다시 false로 만듦
            soundPlay = false;
        }
        
        
    }
}
