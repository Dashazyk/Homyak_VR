using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class playerScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody tushka;
    private float  thrust      = 50f;
    private bool[] do_move     = new bool[4]{false, false, false, false};
    private int    speed_limit = 25;
    public GameObject head ;
    GameObject bullet_prefab;
    private const int initial_shot_cooldown = 20;
    private int shot_cooldown;

    public int killCount = 0;

    public void StartMovingForward(InputAction.CallbackContext context){
        Debug.Log(context.performed);
        if (context.performed)
            do_move[0] = true;
        else if (context.canceled)
            do_move[0] = false;
    }

    public void StartMovinBackward(InputAction.CallbackContext context){
        if (context.performed)
            do_move[3] = true;
        else if (context.canceled)
            do_move[3] = false;
    }

    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        tushka = GetComponent<Rigidbody>();
        bullet_prefab = Resources.Load("Bullet/Bullet") as GameObject;
        shot_cooldown = initial_shot_cooldown;
    }

    void FixedUpdate() {
        shot_cooldown -= 1;
        if (tushka.velocity.magnitude < speed_limit) {
            if (do_move[0] == true)
                tushka.AddForce(head.transform.forward *  thrust);
            if (do_move[3] == true)
                tushka.AddForce(head.transform.forward * -thrust);
           
        }

        float drag = 0.5f;
        tushka.AddForce(Vector3.Scale(new Vector3(drag, drag, drag), -tushka.velocity));

        tushka.angularVelocity = new Vector3(0, 0, 0);

    }

    public void DieInside() {
        Debug.Log("I'm dead inside");
        killCount = 0;
        transform.position = new Vector3(0, 0, 0);
        transform.localPosition = new Vector3(0, 0, 0);
        GetComponent<GetDamagedBehaviour>().SetStat(100, 100);
    }

    public void Shoot(){
        Debug.Log("Shooooooooooot");
        var shot_direction = head.transform.forward.normalized;
        if ( shot_cooldown <= 0) { 
            var bullet_instance = GameObject.Instantiate(
                bullet_prefab, 
                head.transform.position + shot_direction * 10, 
                head.transform.rotation
            );
            
            bullet_instance.GetComponent<bullet_script>().Setup(shot_direction * speed_limit * 3, gameObject);

            shot_cooldown = initial_shot_cooldown;
        }

        
    }
}
