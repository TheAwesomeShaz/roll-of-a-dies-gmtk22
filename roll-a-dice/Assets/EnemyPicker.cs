using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPicker : MonoBehaviour
{
    CameraFollow cam;
    public List<Enemy> enemies;
    public int currEnemyIndex;
    Enemy[] enemyArray;
    public float initialDelayBeforeCamSwitch = 1f;
    public float delayBeforeCamSwitch = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //enemies = FindObjectsOfType<EnemyShoot>();
        enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
        cam = GetComponent<CameraFollow>(); 
        delayBeforeCamSwitch = initialDelayBeforeCamSwitch;
        cam.ChangeTarget(enemies[currEnemyIndex].transform);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currEnemyIndex > 0)
            {
                currEnemyIndex--;
            }
            else
            {
                currEnemyIndex = enemies.Count - 1;
            }
            cam.ChangeTarget(enemies[currEnemyIndex].transform);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currEnemyIndex < enemies.Count- 1)
            {
                currEnemyIndex++;
            }
            else
            {
                currEnemyIndex = 0;
            }
            cam.ChangeTarget(enemies[currEnemyIndex].transform);
        }

        if(!enemies[currEnemyIndex].canShoot) // enemy can't shoot means it isn't alive
        {
            delayBeforeCamSwitch -= Time.deltaTime;

            if(delayBeforeCamSwitch <= 0f)
            {
                enemies.RemoveAt(currEnemyIndex);
                if (currEnemyIndex < enemies.Count - 1)
                {
                    currEnemyIndex++;
                }
                else
                {
                    currEnemyIndex = 0;
                }
                cam.ChangeTarget(enemies[currEnemyIndex].transform);
                delayBeforeCamSwitch = initialDelayBeforeCamSwitch;
            }
        }

    }

}
