using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Movement : MonoBehaviour
{
    public float speed = 10f;
    private float Move;
    private Rigidbody2D rigidbody2D;

    public float GroundRadius;
    public float JumpForce;
    public Transform GrounCheck;

    private bool IsGround;
    public LayerMask Grounded;

    private bool CanDoubleJump;
    private bool StoppedJump;

    public float Jumptime;
    private float JumptimeCounter = 1f;

    public GameObject Bullet;
    public Transform ShootingPoint;


    public Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        IsGround = Physics2D.OverlapCircle(GrounCheck.transform.position, GroundRadius, Grounded);
        Shooting();
        Movement();
        Jump();
    }

    private void Movement()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Horizontal * speed * Time.deltaTime, 0f, 0f);
        Vector3 CharacterScale = transform.localScale;
        //For Move left and Right
        if (Horizontal < 0|| Horizontal > 0)
        {
            animator.SetBool("IsWalk", true);
            if (Horizontal < 0)
            {
                CharacterScale.x = -0.6221462f;
            }
            else
            {
                CharacterScale.x = 0.6221462f;
            }
            

        }
        else if(Horizontal==0)
        {
            animator.SetBool("IsWalk", false);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);
        }
        transform.localScale = CharacterScale;

    }

    void Jump()
    {
        if (IsGround)
        {
            CanDoubleJump = true;
            animator.SetBool("TouchGround", true);
        }
        else
        {
            animator.SetBool("TouchGround", false);
        }
        //for Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGround)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JumpForce);
            }

            else if (!IsGround && CanDoubleJump)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JumpForce);
                JumptimeCounter = Jumptime;
                CanDoubleJump = false;
            }
        }

    }

    void Shooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           GameObject BulletClone =(GameObject) Instantiate(Bullet, ShootingPoint.position, ShootingPoint.rotation);
            BulletClone.transform.localScale = transform.localScale;
            animator.SetTrigger("Shooting");
            animator.SetBool("IsWalk", false);
        }
        else
        {
            animator.SetBool("IsWalk", true);
        }
    }



}
