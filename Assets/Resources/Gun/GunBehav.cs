using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehav : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player = null;
    Vector3 random_miss = new Vector3();
    GameObject bullet_prefab;
    private const int initial_shot_cooldown = 80;
    private int shot_cooldown = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        bullet_prefab = Resources.Load("Bullet/Bullet") as GameObject;
        shot_cooldown = initial_shot_cooldown;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null) {
            if (Random.Range(1, 50) == 1)
                random_miss = new Vector3(
                    Random.Range(-1, 1),
                    Random.Range(-1, 1),
                    Random.Range(-1, 1)
                );
            // var trans = player.transform;
            // trans.position += random_miss;

            // transform.LookAt(trans/*, Vector3.left*/);
            

            Vector3 relativePos   = player.transform.position - transform.position + random_miss;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation    = Quaternion.Lerp( transform.rotation, toRotation, 2 * Time.deltaTime );

            shot_cooldown -= 1;

            if (shot_cooldown <= 0) {
                var distance = player.transform.position - transform.position;
                if (distance.magnitude <= 64) {
                    var shot_direction = transform.forward.normalized;

                    shot_cooldown = initial_shot_cooldown;

                    var bullet_instance = GameObject.Instantiate(
                        bullet_prefab, 
                        transform.position + shot_direction * 10, 
                        transform.rotation
                    );
                    // bullet_instance.GetComponent<Rigidbody>().velocity  = shot_direction * speed_limit * 3;
                    // bullet_instance.GetComponent<bullet_script>().owner = gameObject;

                    bullet_instance.GetComponent<bullet_script>().Setup(shot_direction * 25 * 3, gameObject);
                }
            }
        }
    }
}
