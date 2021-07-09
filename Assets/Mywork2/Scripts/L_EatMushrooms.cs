using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_EatMushrooms : MonoBehaviour
{
    // 이 수치가 0이되면 버섯이 사라짐 (다먹어짐) 
    public int mushroomHP = 200;
    // bool chickEating 변수 참조 위해
    [HideInInspector]
    public L_ChickController chickController;
    // 
    [HideInInspector]
    public L_ChickGrows chickGrows;

    private void Start()
    {
        chickController = GameObject.FindWithTag("Player").GetComponent<L_ChickController>();
        chickGrows = GameObject.FindWithTag("Player").GetComponent<L_ChickGrows>();
    }

    // 버섯 범위 안으로 chick이 들어왔을때
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            // chick이 먹는중이라면
            if(chickController.chickEating)
            {
                //Debug.Log("Eating");
                mushroomHP -= 1;
                //Debug.Log(mushroomHP);
                if(mushroomHP <= 0)
                {
                    chickGrows.eatenMushrooms++;
                    chickGrows.soundPlay = true;
                    Destroy(gameObject);
                }
            }
        }
    }
}
