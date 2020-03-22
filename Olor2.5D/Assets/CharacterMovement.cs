using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float movSpeed;
    public Rigidbody RB;
    Vector3 Transform;
    public GameObject[] thing;
    public GameObject[] save;
    private int a = 0;
    public GameObject FinalTarget;
    void Start()
    {
        RB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform.x= Input.GetAxisRaw("Horizontal");
        Transform.z = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            MyPath a = FinalTarget.GetComponent<MyPath>();
            a.showPath();
            for(int i =0; i<thing.Length;i++)
            {
                if(thing[i]!=null)
                {
                    a=thing[i].GetComponent<MyPath>();
                    a.showPath();
                }
            }

        }
    }
    private void FixedUpdate()
    {
        RB.MovePosition(RB.position + Transform * movSpeed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<MyPath>()!=null)
        {

            thing[a] = other.gameObject;
            a++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<MyPath>() != null)
        {

            for (int i = 0; i < thing.Length; i++)
            {
                if (thing[i] != null && thing[i]==other.gameObject)
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
}

