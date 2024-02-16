using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombChildControler : MonoBehaviour
{
    public float speedHigh;
    public float speedLow;
    public float bombAngle;
    Rigidbody2D RBBombChild;
    private void Awake()
    {
        RBBombChild = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        RBBombChild.AddForce (new Vector2 (Random.Range(-bombAngle, bombAngle), Random.Range(speedLow, speedHigh)), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
