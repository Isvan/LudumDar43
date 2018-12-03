using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBase : MonoBehaviour {

	 Rigidbody rb;
	public float distanceNeeded;
	NavMeshAgent agent;
	public bool reachedTarget;

	public bool marked;
	Controller cont;
	public int treeSpawn;
	public bool marchToDeath;
	public int sacType;
	public float height;
	// Use this for initialization
	public GameObject[] trees;
	public GameObject fire;
	public GameObject blocker;
	void Start () {
		rb = GetComponent<Rigidbody>();
		reachedTarget = false;
		height = transform.localScale.y;
		agent = GetComponent<NavMeshAgent>();
        marchToDeath = false;
		marked = false;
		cont = GameObject.FindGameObjectWithTag("Controller").GetComponent<Controller>();
	}
	
	// Update is called once per frame
	void Update () {
		
		
			if(Vector3.Distance(agent.destination, rb.position) < distanceNeeded){

				if(marchToDeath){

					if(sacType == 0){
						int treeToPick = Mathf.RoundToInt(Random.Range(0,trees.Length));
						GameObject spawnedTower = (GameObject) Instantiate(trees[treeToPick],transform.position,new Quaternion(0,0,0,0));
						spawnedTower.transform.Rotate(-90f,0f,Random.Range(0,360));
						spawnedTower.transform.position = new Vector3(spawnedTower.transform.position.x,spawnedTower.transform.position.y - 3,spawnedTower.transform.position.z);
					
					}else if(sacType == 1){
						GameObject spawnedTower = (GameObject) Instantiate(blocker,transform.position,new Quaternion(0,0,0,0));
						spawnedTower.transform.Rotate(0f,0f,Random.Range(0,360));
					}else if(sacType == 2){
						GameObject spawnedTower = (GameObject) Instantiate(fire,transform.position,new Quaternion(0,0,0,0));
						spawnedTower.transform.position = new Vector3(spawnedTower.transform.position.x,spawnedTower.transform.position.y - 2,spawnedTower.transform.position.z);
						spawnedTower.transform.Rotate(0f,Random.Range(0,360),90f);
					}
					Destroy(gameObject);
				
					cont.vilsSacrifice();
				}

			}
		

	}
	 public void setTarget(Vector3 target){

		agent.destination = target;
		this.reachedTarget = true;

		if(agent.velocity.magnitude > 0){
		//	agent.velocity = new Vector3(0,0,0);
		}

	}

	public void setTargetTower(Vector3 target){

		agent.destination = target;
		this.reachedTarget = true;
		this.marchToDeath = true;

		if(agent.velocity.magnitude > 0){
		//	agent.velocity = new Vector3(0,0,0);
		}

	}
}
