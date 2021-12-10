using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D m_Rigidbody2D;
    public float movementSpeed = 15.0f;
    public camerashake camerashakeOBJ;
    public int playerid = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Movement force
    void Movement(float horInput, float verInput)
    {
        Vector3 heading = new Vector3(horInput, verInput, 0);
        m_Rigidbody2D.AddForce((heading * Time.deltaTime) * movementSpeed, ForceMode2D.Impulse);
    }

    // Update
    void Update()
    {


        if (playerid == 1)
        {
            float horInput = Input.GetAxis("WASDHorizontal");
            float verInput = Input.GetAxis("WASDVertical");
            Movement(horInput, verInput);
        }
        if (playerid == 2)
        {
            float horInput = Input.GetAxis("ArrowHorizontal");
            float verInput = Input.GetAxis("ArrowVertical");
            Movement(horInput, verInput);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(camerashakeOBJ.CameraShake());
    }
}
