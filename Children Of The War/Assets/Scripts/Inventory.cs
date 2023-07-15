using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Bu sýnýf, envanter sistemini temsil eder.
public class Inventory : ScriptableObject

{

    // Bu deðiþken, envanterdeki öðelerin sayýsýný tutar.
    public int ItemCount;

    // Bu deðiþken, envanterdeki öðelerin bir listesini tutar.
    public List<SCItem> Items;

    // Bu fonksiyon, envanterdeki bir öðeyi ekler.
    public void AddItem(SCItem item)
    {
        Items.Add(item);
        ItemCount++;
    }

    // Bu fonksiyon, envanterdeki bir öðeyi çýkarýr.
    public void RemoveItem(SCItem item)
    {
        Items.Remove(item);
        ItemCount--;
    }

    // Bu fonksiyon, envanterdeki bir öðenin sayýsýný döndürür.
    public int GetItemCount()
    {
        return ItemCount;
    }

    // Bu fonksiyon, envanterdeki bir öðeyi döndürür.
    public SCItem GetItem(int index)
    {
        return Items[index];
    }
}
