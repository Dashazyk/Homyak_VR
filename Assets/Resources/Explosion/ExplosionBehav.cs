using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehav : MonoBehaviour
{
    // Start is called before the first frame update
    private int countdown   = 15;
    public GameObject owner = null;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        countdown -= 1;
        transform.localScale += new Vector3(1.7f, 1.7f, 1.7f);
        if (countdown <= 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("collision");
        Debug.Log(collision.gameObject.name);
    }

    // void OnCollisionEnter(Collision collision) {
    void OnTriggerEnter(Collider collision) {
        // Debug.Log("trigger");
        // Debug.Log(collision.gameObject.name);
        // if (collision.gameObject.name == "building") {
            // owner.GetComponent<playerScript>().killCount += 1;
            // Debug.Log(owner.GetComponent<playerScript>().killCount);
            // Destroy(collision.gameObject);

        var gds = collision.gameObject.GetComponent<GetDamagedBehaviour>();
        if (gds != null && owner != null) {
            // owner.GetComponent<playerScript>().killCount += 
            gds.GetDamaged(owner);
        }
        // }
    }
}
