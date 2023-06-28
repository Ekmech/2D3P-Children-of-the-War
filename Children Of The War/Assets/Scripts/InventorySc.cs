using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Inventory : MonoBehaviour
{
    public GameObject[] slots; // Slotlar� temsil eden oyun nesneleri

    private int currentIndex = 0; // Aktif slotun indeksi

    private void Start()
    {
        // �lk slotu etkinle�tir
        slots[currentIndex].SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Aktif slotun indeksini de�i�tir
            currentIndex = (currentIndex + 1) % slots.Length;

            // Slotlar� etkinle�tirme/durdurma
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
            // E�ya nesnesini envanterin aktif slotuna ekle
            other.gameObject.SetActive(false);
            other.transform.SetParent(slots[currentIndex].transform);
            other.transform.localPosition = Vector3.zero;
        }
    }
}