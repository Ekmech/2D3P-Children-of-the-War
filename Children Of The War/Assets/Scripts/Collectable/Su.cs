using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Su : MonoBehaviour
{
    public int susuzlukAzaltmaMiktari = 45;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SusuzlukAzalt(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void SusuzlukAzalt(GameObject player)
    {
        SusuzlukKontrol susuzlukKontrol = player.GetComponent<SusuzlukKontrol>();
        if (susuzlukKontrol != null)
        {
            susuzlukKontrol.GereksinimAzalt(susuzlukAzaltmaMiktari);
        }
    }
}
public class Player : MonoBehaviour
{
    public Su su;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            IctiSu();
        }
    }

    private void IctiSu()
    {
        if (su!= null)
        {
            su.SusuzlukAzalt(gameObject);
        }
    }
}