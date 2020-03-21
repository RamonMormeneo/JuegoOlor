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

        ControllPlayer();
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
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
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

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void returnMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}