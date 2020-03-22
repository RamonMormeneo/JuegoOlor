using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float movementSpeed = 50;
    Animator anim;
    Rigidbody rb;

    public int boneCounter = 0;
    bool hasKey = false;

    float timer = 0.0f;
    float seconds;

    public GameObject finishCanv;
    public GameObject deathCanv;

    public GameObject[] thing;
    public GameObject[] save;
    private int a = 0;
    public GameObject FinalTarget;

    bool canMove = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        timer = 0.0f;
        boneCounter = 0;
    }

    void Update()
    {

        timer += Time.deltaTime;
        seconds = timer % 60;
        if(canMove == true)
        {
            ControllPlayer();
        }
        
    }

    void ControllPlayer()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            anim.SetInteger("Walk", 1);
        }
        else {
            anim.SetInteger("Walk", 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyPath a = FinalTarget.GetComponent<MyPath>();
            a.showPath();
            for (int i = 0; i < thing.Length; i++)
            {
                if (thing[i] != null)
                {
                    a = thing[i].GetComponent<MyPath>();
                    a.showPath();
                }
            }

        }
        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bone")
        {
            Destroy(collision.gameObject);
            boneCounter++;
            print("collision");
        }

        else if (collision.gameObject.tag == "Obstacle")
        {
            deathCanv.SetActive(true);
            canMove = false;

        }

        else if (collision.gameObject.tag == "Key")
        {
            hasKey = true;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Goal")
        {
            if(hasKey == true)
            {
                finishCanv.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MyPath>() != null)
        {

            thing[a] = other.gameObject;
            Debug.Log("Find");
            a++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<MyPath>() != null)
        {

            for (int i = 0; i < thing.Length; i++)
            {
                if (thing[i] != null && thing[i] == other.gameObject)
                {
                    thing[i] = null;
                    a--;
                }
            }
            ReARRay();
        }
    }
    private void ReARRay()
    {
        int j = 0;
        for (int i = 0; i < thing.Length; i++)
        {
            if (thing[i] != null)
            {
                save[j] = thing[i];
                j++;
            }
        }
        thing = save;
    }
    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void retry()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void returnMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}