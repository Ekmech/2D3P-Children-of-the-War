using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraker : MonoBehaviour
{
    public int aclikGidermeMiktari = 30;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AclikGider(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void AclikGider(GameObject player)
    {
        AclikKontrol aclikKontrol = player.GetComponent<AclikKontrol>();
        if (aclikKontrol != null)
        {
            aclikKontrol.GereksinimEkle(aclikGidermeMiktari);
        }
    }
}

