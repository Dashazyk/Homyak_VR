using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_script : MonoBehaviour
{
    // Start is called before the first frame update
    private int lifetime  = 5000;
    public GameObject owner;
    private bool canBoom = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        lifetime--;

        if (lifetime <= 0)
            Boom();
    }

    void Boom() {
        var explosion = GameObject.Instantiate(
                Resources.Load("Explosion/ExplosionBall") as GameObject, 
                transform.position,
                transform.rotation
        );
        explosion.GetComponent<ExplosionBehav>().owner = owner;
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Explosion(Clone)") {
            if (canBoom)
                Boom();
        }
    }

    public void Setup(Vector3 velocity, GameObject _owner = null) {
        GetComponent<Rigidbody>().velocity = velocity;
        owner   = _owner;
        canBoom = true;
    }
}
