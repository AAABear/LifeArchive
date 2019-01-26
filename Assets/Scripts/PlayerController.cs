using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

    public float speed = 1;
    public GameObject catReadyJump;
    public GameObject catAtWindow;
    public GameObject ringAnimation;
    public GameObject babyDown;
    public GameObject babyCry;
    public GameObject babyMom;

    public Animator playerAnimator;

    private Rigidbody2D rBody;

    private bool collisionWithFootball = false;
    private bool collisionWithCat = false;
    private bool collisionWithBear = false;
    private bool collisionWithWall = false;

    private HashSet<string> hasVisited = new HashSet<string>();

    private float timer = 1.0f;

	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody2D>();
	}

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
             Jump();
        }

        if (Input.GetKey(KeyCode.F))
        {
            if (collisionWithFootball)
            {
                GameObject gObj = GameObject.Find("football");
                if (gObj != null)
                {
                    print("Move football");
                    Vector3 curPos = gObj.transform.localPosition;
                    Vector3 targetPos = curPos + new Vector3(0, 10, 0);

                    for (int i = 0; i < 100; ++i)
                    {
                        gObj.transform.position = Vector3.Lerp(curPos, targetPos, 5.0f * Time.deltaTime);
                    }

                }
            }
            else if (collisionWithCat)
            {
                GameObject gObj = GameObject.Find("Cat");
                if (gObj != null)
                {
                    gObj.SetActive(false);

                    // 
                    catReadyJump = Instantiate(catReadyJump);
                    catReadyJump.SetActive(true);

                    Invoke("DisableCatJump", 3.0f);

                }
            }
            else if (collisionWithWall && !hasVisited.Contains("Wall-Right"))
            {
                print("wall press f");
                //    transform.localPosition = new Vector2(5.0f, -8.0f);
                transform.gameObject.SetActive(false);
                babyDown = Instantiate(babyDown);
                babyDown.SetActive(true);

                hasVisited.Add("Wall-Right");

                Invoke("ShakeScreen", 2);
            }

        }

    }

    void ShakeScreen()
    {
        ShakeCamera.Shake();

        Invoke("HideBabyDown", 2);
    }

    void HideBabyDown()
    {
        babyDown.SetActive(false);

        Invoke("CallCryBaby", 1);
    }

    void CallCryBaby()
    {
        // start to cry
        babyCry = Instantiate(babyCry);
        babyCry.SetActive(true);

        Invoke("HideCryBaby", 2);
    }

    void HideCryBaby()
    {
        babyCry.SetActive(false);

        // call mom
        Invoke("CallMom", 1);
    }

    void CallMom()
    {
        babyMom = Instantiate(babyMom);
        babyMom.SetActive(true);

        Invoke("HideMom", 3);
    }

    void HideMom()
    {
        babyMom.SetActive(false);
        Invoke("GoBack", 1);
    }

    void GoBack()
    {
        transform.localPosition = new Vector2(5.0f, -8.0f);
        transform.gameObject.SetActive(true);
    }

    void DisableCatJump()
    {
        catReadyJump.SetActive(false);
        Invoke("CallJumpToWindow", 1.0f);

    }

    void CallJumpToWindow()
    {
        Instantiate(catAtWindow);

        GameObject.Find("Ring").SetActive(false);
        Instantiate(ringAnimation);
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");

        if (moveX > 0)
        {
            rBody.velocity = new Vector2(moveX, 0) * speed;
            playerAnimator.SetTrigger("MoveRight");
        }
        else if (moveX < 0)
        {
            rBody.velocity = new Vector2(moveX, 0) * speed;
            playerAnimator.SetTrigger("MoveLeft");
        }
    }

    public void Jump()
    {
        rBody.AddForce(Vector2.up * 600);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Football")
        {
            print("Collision with football.");
            collisionWithFootball = true;
        }
        else if (collision.collider.name == "Wall-Right")
        {
            print("collision With wall");
            collisionWithWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Football")
        {
            collisionWithFootball = false;
        }
        else if (collision.collider.name == "Wall-Right")
        {
            collisionWithWall = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Player trigger : " + collision.tag);
        if (collision.name == "Cat")
        {
            collisionWithCat = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Cat")
        {
            collisionWithCat = false;
        }

    }

}