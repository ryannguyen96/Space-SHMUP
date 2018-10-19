using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    static public Hero S;

    [Header("Set in Inspector")]

    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;

    [Header("Set Dynamically")]

    [SerializeField]
    private float _shieldLevel = 1;

    // This variable holds a reference to the last triggering GameObject 
    private GameObject lastTriggerGo = null;

    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Hello.Awake() - Attempted to assign second Hero.S!");
        }
    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //print("Triggered: " + go.name);
        if(go == lastTriggerGo)
        {
            return;
        }
        lastTriggerGo = go;

        if(go.tag == "Enemy")
        {
            shieldLevel--; // Decrement shield level
            Destroy(go); // Destroy enemy
        }
        else
        {
            print("Triggered by non-Enemy: " + go.name);
        }
    }

    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
            // if shield < 0
            if(value < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
