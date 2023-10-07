using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue;
    public Vector3 oldPos;
    public float speed;
    private int numPickup = 6;
    public GameObject[] pickups;
    private int count = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI dbPos;
    public TextMeshProUGUI dbVel;
    public TextMeshProUGUI dbDist;
    public enum Modes { Normal, Distance, Vision};
    private Modes mode = Modes.Normal;

    private Vector3 pickUpPos;

    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();

    }


    private void SetCountText()
    {
        scoreText.text = "Score : " + count.ToString();
        if (count >= numPickup)
        {
            winText.text = "You Win";
        }
    }

    private float ClosestPickup()
    {
        float d = 9999;
        GameObject closest = pickups[0];
        foreach (GameObject p in pickups)
        {
            if(p.activeSelf)
            {
                float dist = (p.transform.position - transform.position).magnitude;

                if (dist <= d)
                {
                    d = dist;
                    closest = p.GameObject();
                }
            }
        }
        closest.GetComponent<Renderer>().material.color= Color.blue;
        pickUpPos = closest.transform.position;
        return d;
    }

    public Vector3 getPPP()
    {
        return pickUpPos;
    }

    public Modes getMode()
    {
        return mode;
    }

    private void Start()
    {
        SetCountText();
        winText.text = "";
        dbDist.text = "";
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveValue.x,0.0f,moveValue.y);
        oldPos = transform.position;
        GetComponent<Rigidbody>().AddForce(movement*speed*Time.fixedDeltaTime);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (mode != Modes.Vision)
            {
                mode++;
            }
            else
            {
                mode = Modes.Normal;
            }
        }

    }

    private void LateUpdate()
    {
        if (mode != Modes.Vision)
        {
            foreach (GameObject p in pickups)
            {
                p.GetComponent<Renderer>().material.color = Color.white;
            }
        }

        switch (mode)
        {
            case Modes.Normal:
                dbPos.text = "";
                dbVel.text = "";
                dbDist.text = "";
                break;

            case Modes.Distance:
                dbPos.text = transform.position.ToString();
                dbVel.text = ((transform.position - oldPos) / Time.deltaTime).magnitude.ToString();
                dbDist.text = ClosestPickup().ToString();
                break;

            case Modes.Vision:
                dbPos.text = "";
                dbVel.text = "";
                dbDist.text = "";

                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }
}
