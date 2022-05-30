using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDamagedBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private int ihealth = 100;
    private int health  = 100;
    private int reward  = 100;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStat(int h, int r) {
        ihealth = h;
        reward  = r;
        health  = ihealth;
    }

    public float GetHealthPercent() {
        return health / (float)ihealth;
    }

    public int GetDamaged(GameObject byWhom, int damage = 10) {
        int score = 0;

        Debug.Log(byWhom.name);
        Debug.Log(health);
        health -= damage;

        if (health <= 0) {
            score   = reward;
            var add = byWhom.GetComponent<playerScript>();
            if (add != null) 
                add.killCount += reward;
            
            
            var ya_player = GetComponent<playerScript>();
            if (ya_player)
                ya_player.DieInside();
            else
                Destroy(gameObject);
        }

        return score;
    }
}
