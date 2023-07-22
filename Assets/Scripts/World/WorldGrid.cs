using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    [SerializeField]
    GameObject sectionPrefab;

    [SerializeField]
    Sprite background;

    SectionGrid[,] worldGrid = new SectionGrid[3,3];
    int[,,] positionMultipliers = new int[3,3,2] { {{-1,1 },{0,1 },{1,1 } },
                                                   {{-1,0 },{0,0 },{1,0 } },
                                                   {{-1,-1 },{0,-1 },{1,-1 } } };
    Transform ship;
    float bg_size_x, bg_size_y;
    Vector2 currentCentre;

    Vector2 old_section = new Vector2(1,1);

    private void Start()
    {
        ship = GameObject.Find("Ship").transform;
        currentCentre = ship.transform.position;

        bg_size_x = background.bounds.max.x;
        bg_size_y = background.bounds.max.y;

        for (int i=0;i<3;i++)
        {
            for(int j=0;j<3;j++)
            {
                SpawnSection(i, j);
            }
        }

        
    }

    private void Update()
    {
        Vector2 current_section = CheckBounds();

        if(current_section!=old_section)
        {
            UpdateCentreSection(current_section);
        }
    }

    Vector2 CheckBounds()
    {
        Vector2 currentSection = new Vector2();

        for(int i=0;i<3;i++)
        {
            for(int j=0;j<3;j++)
            {
                if (worldGrid[i, j].gameObject.GetComponent<BoxCollider2D>().bounds.Contains(ship.position))
                {
                    currentSection.x = i;
                    currentSection.y = j;
                }
            }
        }

        return currentSection;
    }

    void UpdateCentreSection(Vector2 currentSection)
    {
        SectionGrid[,] new_world_grid = new SectionGrid[3, 3];

        currentCentre = worldGrid[(int)currentSection.x,(int)currentSection.y].gameObject.transform.position;

        for(int i=0;i<3;i++)
        {
            for(int j=0;j<3;j++)
            {
                new_world_grid[i, j] = null;
            }
        }

        for (int i=0;i<3;i++)
        {
            for(int j=0;j<3;j++)
            {
                Vector2 updatedSection = new Vector2(i + 1 - currentSection.x, j + 1 - currentSection.y);

                if (updatedSection.x < 3 && updatedSection.x >= 0 && updatedSection.y < 3 && updatedSection.y >= 0)
                {
                    new_world_grid[(int)updatedSection.x, (int)updatedSection.y] = worldGrid[i, j];
                }
                else
                {
                    worldGrid[i, j].DeleteSection();
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (new_world_grid[i, j] == null)
                {
                    Vector3 spawnPosition = new Vector3(currentCentre.x + bg_size_x * 2f * positionMultipliers[i, j, 0],
                                                        currentCentre.y + bg_size_y * 2f * positionMultipliers[i, j, 1], 0);
                    new_world_grid[i, j] = Instantiate(sectionPrefab, spawnPosition, Quaternion.Euler(0, 0, 0)).GetComponent<SectionGrid>();
                }
            }
        }

        worldGrid = new_world_grid;
    }

    void SpawnSection(int i,int j)
    {
        Vector3 spawnPosition = new Vector3(currentCentre.x + bg_size_x*2f * positionMultipliers[i, j, 0],
                                                    currentCentre.y + bg_size_y*2f * positionMultipliers[i, j, 1], 0);
        worldGrid[i, j] = Instantiate(sectionPrefab, spawnPosition, Quaternion.Euler(0, 0, 0)).GetComponent<SectionGrid>();
    }
}
