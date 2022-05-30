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
    private bool   queued_shot = false;
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
        // var muzzleFlash = GameObject.Instantiate(muzzleFlashPrefab, transform.position, transform.rotation);
    }

    void Update() {
        // if (Input.GetKeyDown(KeyCode.LeftControl)) {
        //     queued_shot = true;
        // }
        // if (Input.GetKeyUp(KeyCode.LeftControl)) {
        //     queued_shot = false;
        // }

        // if (Input.GetKeyDown(KeyCode.W))
        // {
        //     do_move[0] = true;
        // }
        // if (Input.GetKeyUp(KeyCode.W))
        // {
        //     do_move[0] = false;
        // }

        // if (Input.GetKeyDown(KeyCode.S))
        // {
        //     do_move[3] = true;
        // }
        // if (Input.GetKeyUp(KeyCode.S))
        // {
        //     do_move[3] = false;
        // }

        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     transform.position += new Vector3(0, 5, 0);
        // }
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     transform.position -= new Vector3(0, 5, 0);
        // }

        // float mouse_x = Input.GetAxis("Mouse X");
        // float mouse_y = Input.GetAxis("Mouse Y");
        // transform.Rotate(-mouse_y * 2, mouse_x * 2, 0, Space.Self);

        // var GameObject.eulerAngles.x;
        // transform.Rotate( -transform.rotation.eulerAngles.x , mouse_x * 2, -transform.rotation.eulerAngles.z, Space.Self);
        // transform.
    }

    void FixedUpdate() {
        shot_cooldown -= 1;
        if (tushka.velocity.magnitude < speed_limit) {
            if (do_move[0] == true)
                tushka.AddForce(head.transform.forward *  thrust);
            if (do_move[3] == true)
                tushka.AddForce(head.transform.forward * -thrust);
            // if (do_move[0] == true)
            //     tushka.AddForce(transform.forward *  thrust);
            // if (do_move[3] == true)
            //     tushka.AddForce(transform.forward * -thrust);
        }

        float drag = 0.5f;
        tushka.AddForce(Vector3.Scale(new Vector3(drag, drag, drag), -tushka.velocity));

        tushka.angularVelocity = new Vector3(0, 0, 0);

        // GameObject.Find("kill_count_text").text = killCount.ToString();
        // var canvasGO = GameObject.Find("PlayerMode/Canvas");
        // var textGO = 
        // GameObject.Find("PlayerMode/Canvas/killCountText").GetComponent<Text>().text = killCount.ToString();
        // canvas.
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
        // var shot_direction = transform.forward.normalized;
        var shot_direction = head.transform.forward.normalized;
        if ( shot_cooldown <= 0) { 
            // queued_shot = false;
            // var bullet_instance = GameObject.Instantiate(
            //     bullet_prefab, 
            //     transform.position + shot_direction * 10, 
            //     transform.rotation
            // );
            var bullet_instance = GameObject.Instantiate(
                bullet_prefab, 
                head.transform.position + shot_direction * 10, 
                head.transform.rotation
            );
            // bullet_instance.GetComponent<Rigidbody>().velocity  = shot_direction * speed_limit * 3;
            // bullet_instance.GetComponent<bullet_script>().owner = gameObject;
            bullet_instance.GetComponent<bullet_script>().Setup(shot_direction * speed_limit * 3, gameObject);

            shot_cooldown = initial_shot_cooldown;
        }

        
    }
}
