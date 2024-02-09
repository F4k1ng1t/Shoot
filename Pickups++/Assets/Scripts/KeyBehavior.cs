using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.Build.Content;
using UnityEngine;

public class KeyBehavior : MonoBehaviour
{
    public GameObject Text;
    public Camera Camera;
    public GameBehavior gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Text.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Text.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                Destroy(this.transform.parent.gameObject);

                Debug.Log("Key acquired");

                AcquireKey();
                Text.SetActive(false);
                gameManager.Items += 1;
            }
        }
    }
    private void AcquireKey()
    {
        //Object.Instantiate(GameObject.Find("flashlight"), Camera.transform.position, new Quaternion(0, 0, 0, 0), Camera.transform);
        //No functionality yet

    }
    private void OnTriggerExit(Collider other)
    {
        Text.SetActive(false);
    }
}


