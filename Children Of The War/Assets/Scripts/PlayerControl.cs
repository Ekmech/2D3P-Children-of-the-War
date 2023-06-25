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

public class ItemPickup : MonoBehaviour
{
    // Toplanacak öðeyle etkileþimde bulunan karakterin tag'i
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // Etkileþimde bulunan nesnenin tag'i "Player" mý diye kontrol et
        if (other.CompareTag(playerTag))
        {
            // Öðeyi topla
            CollectItem();

            // Nesneyi yok et (isteðe baðlý)
            Destroy(gameObject);
        }
    }

    private void CollectItem()
    {
        // Öðenin toplanmasýna dair gerçekleþtirilecek iþlemleri burada gerçekleþtir
        // Örneðin, puan ekleme, envanter güncelleme, ses efekti oynatma vb.
    }
}