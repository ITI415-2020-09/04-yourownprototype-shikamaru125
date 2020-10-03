using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameMode{
    idle,
    playing,
    levelEnd
}

public class GameManager : MonoBehaviour
{
    static private OrbWeaver S; //a private Singleton

    [Header("Set in Inspector")]
    public Vector3 weaverPos; // The place to put weavers
    public GameObject[] weavers;   // An array of the weavers
    public GameObject playerPrefab;

    [Header("Set Dynamically")]
    public int level;  // The current level
    public int levelMax;  // The number of levels
    public GameObject weaver;  // The current weaver
    public GameObject player;
    public GameMode mode = GameMode.idle;

    // Start is called before the first frame update
    void Start()
    {
    

        level = 0;
        levelMax = weavers.Length;
        StartLevel();
    }

    void StartLevel(){
        //get rid of the old weaver if one exists
        if(weaver != null){
            Destroy(weaver);
        }
        if(player != null){
            Destroy(player);
        }

        //destroy old projectiles if they exist
      //  GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
      //  foreach (GameObject pTemp in gos) {
      //      Destroy( pTemp );
      //  }

        // Instantiate the new weaver
        weaver = Instantiate<GameObject>(weavers[level]);
        weaver.transform.position = weaverPos;

        //Instantiate the new player
        player = Instantiate<GameObject>(playerPrefab);
        player.GetComponent<Transform>().position = GameObject.Find("StartPoint").GetComponent<Transform>().position;

        Camera.main.GetComponent<CameraController>().player = player;
        // Reset the goal
        Goal.goalMet = false;
        mode = GameMode.playing;
    }


    // Update is called once per frame
    void Update()
    {
        //Check for level end
        if((mode == GameMode.playing)&& Goal.goalMet) {
            //change mode to stop checking for level end
            mode = GameMode.levelEnd;

            //start the next level in 2 seconds
            Invoke("NextLevel",2f);
        }
    }
    void NextLevel() {
        level++;
        if (level == levelMax) {
            level = 0;
        }
        StartLevel();
    }

}
