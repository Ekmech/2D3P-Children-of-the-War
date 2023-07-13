using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Bu s�n�f, envanter sistemini temsil eder.
public class Inventory : MonoBehaviour
{

    // Bu de�i�ken, envanterdeki ��elerin say�s�n� tutar.
    public int ItemCount;

    // Bu de�i�ken, envanterdeki ��elerin bir listesini tutar.
    public List<Item> Items;

    // Bu fonksiyon, envanterdeki bir ��eyi ekler.
    public void AddItem(Item item)
    {
        Items.Add(item);
        ItemCount++;
    }

    // Bu fonksiyon, envanterdeki bir ��eyi ��kar�r.
    public void RemoveItem(Item item)
    {
        Items.Remove(item);
        ItemCount--;
    }

    // Bu fonksiyon, envanterdeki bir ��enin say�s�n� d�nd�r�r.
    public int GetItemCount()
    {
        return ItemCount;
    }

    // Bu fonksiyon, envanterdeki bir ��eyi d�nd�r�r.
    public Item GetItem(int index)
    {
        return Items[index];
    }
}
