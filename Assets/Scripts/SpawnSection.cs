using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpawnSection : MonoBehaviour
{
    public float waitTime; // how long should the corrutine wait before spawning another section
    public float sectionHeight; //how tall is the section in units from the bottom to the top
    public float sectionSpeed; // how fast does each section move downwards
    public int yOffset; //how high up do we want the sections to spawn  

    private Vector3 sectionSpawnPos;

    public List<GameObject> sections;


    public bool isGameActive = true;
    
    void Start()
    {
        sectionHeight = (sections[0].transform.localScale.z) * 10;
        sectionSpawnPos = new Vector3(0, sectionHeight + yOffset, 0);
        waitTime = sectionHeight / sectionSpeed; // the equation for time is (distance / speed) so to see how long we should wait before spawning another section we
                                                 // see how far the section goes and divide it by how fast its moving.
                                                 //int index = UnityEngine.Random.Range(0, sections.Count);
                                                 //Instantiate(sections[index], sectionSpawnPos, Quaternion.Euler(270f, 0, 0));

        for (int i = 0; i <= 3; i++) 
        {
            if (i == 0)
            {
                int index = UnityEngine.Random.Range(0, sections.Count);
                Instantiate(sections[index], sectionSpawnPos, Quaternion.Euler(270f, 0, 0));
            }
            else if (i == 1)
            {
                int index = UnityEngine.Random.Range(0, sections.Count);
                Instantiate(sections[index], new Vector3(sectionSpawnPos.x, sectionSpawnPos.y - sectionHeight, sectionSpawnPos.z), Quaternion.Euler(270f, 0, 0));
            }
            else if (i == 2)
            {
                int index = UnityEngine.Random.Range(0, sections.Count);
                Instantiate(sections[index], new Vector3(sectionSpawnPos.x, sectionSpawnPos.y - (2 * sectionHeight), sectionSpawnPos.z), Quaternion.Euler(270f, 0, 0));
            }
            else if (i == 3)
            {
                int index = UnityEngine.Random.Range(0, sections.Count);
                Instantiate(sections[index], new Vector3(sectionSpawnPos.x, sectionSpawnPos.y - (3 * sectionHeight), sectionSpawnPos.z), Quaternion.Euler(270f, 0, 0));
            }
        }
        StartCoroutine(SpawnSections());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnSections()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(waitTime);
            int index = UnityEngine.Random.Range(0, sections.Count);
            Instantiate(sections[index], sectionSpawnPos, Quaternion.Euler(270f, 0, 0));
        }
    }
}
