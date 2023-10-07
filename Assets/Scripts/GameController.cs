using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private LineRenderer lineRenderer;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = player.AddComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().getMode() == PlayerController.Modes.Distance)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, player.transform.position);
            lineRenderer.SetPosition(1, player.GetComponent<PlayerController>().getPPP());
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
        }
        else if (player.GetComponent<PlayerController>().getMode() == PlayerController.Modes.Vision)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, player.transform.position);
            lineRenderer.SetPosition(1, ((player.GetComponent<Rigidbody>().velocity)*3)+player.transform.position);
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;

            Vector3 v = (player.transform.position - player.GetComponent<PlayerController>().oldPos).normalized;
            GameObject closest = player.GetComponent<PlayerController>().pickups[0];
            float d=-1f;
            foreach(GameObject p in player.GetComponent<PlayerController>().pickups)
            {
                p.GetComponent<Renderer>().material.color = Color.white;
                Vector3 pp = p.transform.position.normalized;
                float pd = (pp - v).magnitude;
                if (d > 0)
                {
                    if (pd < d)
                    {
                        d = pd;
                        closest = p;
                    }
                }
                else
                {
                    d = pd;
                    closest = p;
                }
            }
            closest.GetComponent<Renderer>().material.color = Color.green;
            closest.transform.LookAt(player.transform.position);
        }
        else if(player.GetComponent<PlayerController>().getMode() == PlayerController.Modes.Normal)
        {
            lineRenderer.enabled = false;
        }
    }
}
