using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);//InvokeRepeating  重複呼叫    每過幾秒 要呼叫的方法 第一次要delay多久時間(預設3秒)  每次間隔多久
    }


    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f) //玩家的生命值小於等於0
        {
            return;  //返回 跳出這個spawn{}不會再執行 
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);  //根據點隨機產生一個

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation); //如果玩家生命值不小於等於0  Instantiate複製產生一個敵人  位置和旋轉都放在槍口
    }
}
