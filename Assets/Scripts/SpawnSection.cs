using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;

public class SpawnSection : MonoBehaviour
{
    public float waitTime; // how long should the corrutine wait before spawning another section
    public float sectionHeight; //how tall is the section in units from the bottom to the top
    public float sectionSpeed; // how fast does each section move downwards
    public int yOffset; //how high up do we want the sections to spawn  

    private Vector3 sectionSpawnPos;


    //Scaling difficulty
    public List<GameObject> easySections;
    public List<GameObject> mediumSections;
    public List<GameObject> hardSections;

    //private bool easyActive = true;
    //private bool mediumActive = false;
    //private bool hardActive = false;

    //When sections start;
    private int easyStart = 0;
    private int mediumStart = 15;
    private int hardStart = 45;

    //When sections end
    private int easyEnd = 30;
    private int mediumEnd = 60;


    public int sectionPassed = 0;

    public GameObject startSection;


    public bool isGameActive = true;
    
    void Start()
    {
        sectionHeight = (easySections[0].transform.localScale.z) * 10;
        sectionSpawnPos = new Vector3(0, sectionHeight + yOffset, 0);
        waitTime = sectionHeight / sectionSpeed; // the equation for time is (distance / speed) so to see how long we should wait before spawning another section we
                                                 // see how far the section goes and divide it by how fast its moving.
                                                 //int index = UnityEngine.Random.Range(0, sections.Count);
                                                 //Instantiate(sections[index], sectionSpawnPos, Quaternion.Euler(270f, 0, 0));

        for (int i = 0; i <= 3; i++) 
        {
            if (i == 0)
            {
                int index = UnityEngine.Random.Range(0, easySections.Count);
                Instantiate(easySections[index], sectionSpawnPos, Quaternion.Euler(270f, 0, 0));
            }
            else if (i == 1)
            {
                int index = UnityEngine.Random.Range(0, easySections.Count);
                Instantiate(easySections[index], new Vector3(sectionSpawnPos.x, sectionSpawnPos.y - sectionHeight, sectionSpawnPos.z), Quaternion.Euler(270f, 0, 0));
            }
            else if (i == 2)
            {
                int index = UnityEngine.Random.Range(0, easySections.Count);
                Instantiate(startSection, new Vector3(sectionSpawnPos.x, sectionSpawnPos.y - (2 * sectionHeight), sectionSpawnPos.z), Quaternion.Euler(270f, 0, 0));
            }
            else if (i == 3)
            {
                //nt index = UnityEngine.Random.Range(0, sections.Count);
                Instantiate(startSection, new Vector3(sectionSpawnPos.x, sectionSpawnPos.y - (3 * sectionHeight), sectionSpawnPos.z), Quaternion.Euler(270f, 0, 0));
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


            if(sectionPassed < mediumStart)
            {
                int index = UnityEngine.Random.Range(0, easySections.Count);
                Instantiate(easySections[index], sectionSpawnPos, Quaternion.Euler(270f, 0, 0));
            }
            else if (sectionPassed>mediumStart && sectionPassed<easyEnd)
            {
                int sectionToSpawn = UnityEngine.Random.Range(0, 2);
                if(sectionToSpawn == 0)
                {
                    int index = UnityEngine.Random.Range(0, easySections.Count);
                    Instantiate(easySections[index], sectionSpawnPos, Quaternion.Euler(270f, 0, 0));
                }
                else
                {
                    int index = UnityEngine.Random.Range(0, mediumSections.Count);
                    Instantiate(mediumSections[index], sectionSpawnPos, Quaternion.Euler(270f, 0, 0));
                }
            }
            else if (sectionPassed > easyEnd)
            {
                int index = UnityEngine.Random.Range(0, mediumSections.Count);
                Instantiate(mediumSections[index], sectionSpawnPos, Quaternion.Euler(270f, 0, 0));
            }
            else if (sectionPassed > hardStart && sectionPassed<mediumEnd)
            {
                int sectionToSpawn = UnityEngine.Random.Range(0, 2);
                if (sectionToSpawn == 0)
                {
                    int index = UnityEngine.Random.Range(0, mediumSections.Count);
                    Instantiate(mediumSections[index], sectionSpawnPos, Quaternion.Euler(270f, 0, 0));
                }
                else
                {
                    int index = UnityEngine.Random.Range(0, hardSections.Count);
                    Instantiate(hardSections[index], sectionSpawnPos, Quaternion.Euler(270f, 0, 0));
                }
            }
            else if (sectionPassed > mediumEnd)
            {
                int index = UnityEngine.Random.Range(0, hardSections.Count);
                Instantiate(hardSections[index], sectionSpawnPos, Quaternion.Euler(270f, 0, 0));
            }
            //int index = UnityEngine.Random.Range(0, easySections.Count);
            //Instantiate(easySections[index], sectionSpawnPos, Quaternion.Euler(270f, 0, 0));
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Main");
    }
}
