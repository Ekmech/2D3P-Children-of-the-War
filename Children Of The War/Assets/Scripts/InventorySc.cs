using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Inventory : MonoBehaviour
{
    public GameObject[] slots; // Slotlarý temsil eden oyun nesneleri

    private int currentIndex = 0; // Aktif slotun indeksi

    private void Start()
    {
        // Ýlk slotu etkinleþtir
        slots[currentIndex].SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Aktif slotun indeksini deðiþtir
            currentIndex = (currentIndex + 1) % slots.Length;

            // Slotlarý etkinleþtirme/durdurma
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].SetActive(i == currentIndex);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            // Eþya nesnesini envanterin aktif slotuna ekle
            other.gameObject.SetActive(false);
            other.transform.SetParent(slots[currentIndex].transform);
            other.transform.localPosition = Vector3.zero;
        }
    }
}