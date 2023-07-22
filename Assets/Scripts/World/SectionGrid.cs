using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionGrid : MonoBehaviour
{
    [SerializeField]
    int max_items;

    [SerializeField]
    List<GameObject> encounters;
    System.Random rnd = new System.Random();

    private void Start()
    {
        SpriteRenderer image = GetComponent<SpriteRenderer>();
        int nr_items = rnd.Next(1, max_items+1);

        for(int i=0;i<nr_items;i++)
        {
            int item_index = rnd.Next(0, encounters.Count);
            float pos_x = (float)rnd.NextDouble() * image.bounds.size.x - image.bounds.extents.x + image.bounds.center.x;
            float pos_y = (float)rnd.NextDouble() * image.bounds.size.y - image.bounds.extents.y + image.bounds.center.y;

            Instantiate(encounters[item_index], new Vector3(pos_x, pos_y, 0),Quaternion.identity,transform);
        }
    }

    public void DeleteSection()
    {
        Destroy(gameObject);
    }
}
