using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float camRayLength = 100f;
    private Vector3 movement;
    private Animator anim;
    private Rigidbody rb;
    private int floorMask;

    void Awake() 
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    //Obtain whether a key was pressed prior to physics, and then move the character accordingly (movement, turning, and animating according to Animator Controller)
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    //Calculate the necessary movement changes according to keypress
    void Move(float hrznt, float vert)
    {
        movement.Set(hrznt, 0.0f, vert);
        movement = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
        
    }

    //Turn the character as necessary according to the mouse
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            //Create a vector3 based off where the raycast hit the floor relative to the player, negate y value
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0.0f;
            //Rotate the player accordingly
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
        }
    }

    //Determine whether or not the character is walking
    void Animating(float hrznt, float vert)
    {
        bool walking = hrznt != 0f || vert != 0f;
        anim.SetBool("IsWalking", walking);
    }
}
