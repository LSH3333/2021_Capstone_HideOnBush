using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_CharacterGrows : MonoBehaviour
{
    /*[SerializeField]
    private AudioSource CoinSound; // coin 획득 사운드 */
    [SerializeField]
    private AudioSource enemyDestroySound; // enemy 소멸 사운드 
    public GameObject DestroyEffect; // enemy,player 소멸 effect
    public float growAmount = 0.02f; // 캐릭터가 이 수치만큼 커짐 

    private void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {        
        // Enemy와 접촉 시
        if (collision.transform.tag == "Enemy")
        {
            // 적의 크기가 Player보다 작으면 적 소멸 
            if (collision.transform.localScale.x * 5 <= transform.localScale.x)
            {
                enemyDestroySound.Play(); // 적 소멸 사운드 플레이 
                Destroy(collision.gameObject);
                // 접촉한 enemy와 같은 사이즈의 effect 소환 
                DestroyEffect.transform.localScale = collision.transform.localScale;

                Vector3 spawnPos = new Vector3(collision.transform.position.x, 0.5f, collision.transform.position.z);
                Debug.Log(spawnPos);
                // Enemy 죽은자리에 Effect 소환 
                GameObject deadEffect = Instantiate(DestroyEffect, spawnPos, Quaternion.identity);
                //Debug.Log(collision.transform.position);
                
                //deadEffect.transform.localScale = collision.transform.localScale;

                // 나보다 작은 적을먹으면 나의 크기 커짐 
                GrowsWhenEat();
            }
            else // Player가 더 작으면 Player가 소멸 
            {
                Instantiate(DestroyEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        
    }

    private void GrowsWhenEat()
    {
        Vector3 GrowAmount = new Vector3(growAmount, growAmount, growAmount);
        gameObject.transform.localScale += GrowAmount;

    }


}
