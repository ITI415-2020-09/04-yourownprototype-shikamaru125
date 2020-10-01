using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbWeaver : MonoBehaviour
{
    [Header("Set in Inspector")]
    //prefab for instantiating orbs
    public GameObject orbPrefab;
    //speed at which the orbweaver moves
    public float speed = 1f;
    //distance where orbweaver turns around
    public float leftAndRightEdge = 10f;
    //chance that the orbweaver will change directions
    public float chanceToChangeDirections = 0.1f;
    //rate at which orbs will be instantiated
    public float secondsBetweenOrbDrops = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //dropping orbs every second
        Invoke("DropOrb", 2f);
    }

    void DropOrb(){
        GameObject orb = Instantiate<GameObject>(orbPrefab);
        orb.transform.position = transform.position;
        Invoke("DropOrb", secondsBetweenOrbDrops);
    }

    // Update is called once per frame
    void Update()
    {
        //basic movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        //changing direction
        if (pos.x < -leftAndRightEdge) {
            speed = Mathf.Abs(speed); //move right
        } else if (pos.x > leftAndRightEdge) {
            speed = -Mathf.Abs(speed); //move left
        } 
    }

    void FixedUpdate() {
        //changing direction randomly is now
        if (Random.value < chanceToChangeDirections){
            speed *= -1; //change direction
        }
    }
}
