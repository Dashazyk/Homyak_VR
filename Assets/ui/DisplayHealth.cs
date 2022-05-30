using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHealth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var health = GameObject.Find("PlayerModel").GetComponent<GetDamagedBehaviour>().GetHealthPercent();
        var sc = new Vector3(health * 5, 0.6f, 1);
        transform.localScale = sc;
    }
}
