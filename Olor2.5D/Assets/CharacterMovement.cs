using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float movSpeed;
    public Rigidbody RB;
    Vector3 Transform;
    void Start()
    {
        RB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform.x= Input.GetAxisRaw("Horizontal");
        Transform.z = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        RB.MovePosition(RB.position + Transform * movSpeed * Time.fixedDeltaTime);
    }
}
