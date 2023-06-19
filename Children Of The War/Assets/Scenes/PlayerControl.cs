using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float hareketHizi = 5f

        
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");
        Vector3 hareket = new Vector3(yatay, Of, dikey) * hareketHizi * Time.deltaTime;
        transform.Translate(hareket);

    }
}
