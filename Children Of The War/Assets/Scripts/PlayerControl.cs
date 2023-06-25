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
    // Toplanacak ��eyle etkile�imde bulunan karakterin tag'i
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // Etkile�imde bulunan nesnenin tag'i "Player" m� diye kontrol et
        if (other.CompareTag(playerTag))
        {
            // ��eyi topla
            CollectItem();

            // Nesneyi yok et (iste�e ba�l�)
            Destroy(gameObject);
        }
    }

    private void CollectItem()
    {
        // ��enin toplanmas�na dair ger�ekle�tirilecek i�lemleri burada ger�ekle�tir
        // �rne�in, puan ekleme, envanter g�ncelleme, ses efekti oynatma vb.
    }
}