using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public interface IDropItem
{
    GameObject GetItemPrefab();
    float GetDropChance();
}

[System.Serializable]
public class DropItem : IDropItem
{
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField][Range(0f, 1f)] public float _dropChance;

    public GameObject GetItemPrefab()
    {
        return _itemPrefab;
    }

    public float GetDropChance()
    {
        return _dropChance;
    }
}

public class EnemyDropper : MonoBehaviour
{
    [SerializeField] private DropItem[] _dropItems;

    public void TryDropItems()
    {
        float totalDropChance = 0f;

        foreach (IDropItem dropItem in _dropItems)
        {
            totalDropChance += dropItem.GetDropChance();
        }

        float randomValue = Random.Range(0f, totalDropChance);
        float cumulativeChance = 0f;

        foreach (IDropItem dropItem in _dropItems)
        {
            cumulativeChance += dropItem.GetDropChance();

            if (randomValue <= cumulativeChance)
            {
                GameObject dropItemObject = Instantiate(dropItem.GetItemPrefab(), transform.position, Quaternion.identity);
                break;
            }
        }
    }
}
