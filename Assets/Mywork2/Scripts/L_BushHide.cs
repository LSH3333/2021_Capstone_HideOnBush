using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_BushHide : MonoBehaviour
{
    // Player가 Bush안에 숨으면 true 
    // 모든 Bush중 어느 Bush인지 상관없이 판단할것이므로 static 처리
    public static bool PlayerInBush = false;
    // 0 : 
    // 1 : Bush 들어갈때
    // 2: Bush 나갈때 
    public static int BushSound = 0;

    private void OnTriggerStay(Collider other)
    {
        // Bush 안에 Player가 숨으면 
        if(other.tag == "Player")
        {            
            PlayerInBush = true;
        }
    }

    // 덤불 숨을때
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            BushSound = 1;
        }
    }

    // 덤불 나갈때
    private void OnTriggerExit(Collider other)
    {
        // Bush에서 Player가 나가면 
        if(other.tag == "Player")
        {
            BushSound = 2;
            PlayerInBush = false;            
        }
    }
}
