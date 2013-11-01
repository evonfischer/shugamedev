using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	Transform currentNode;
	Transform nextNode;
	Node testNode;
	bool hasDestination = false;
	bool dying = false;
	public Transform nodes;
	public float speed = 5;
	public Transform player;
	Vector3 target = new Vector3();
	// Use this for initialization
	void Start () {
		int node_i = -1;
		if (nodes!=null){
			currentNode = FindClosestNode();
			testNode = currentNode.GetComponent("Node") as Node;
			nextNode = testNode.getNextNode();
			if(nextNode!=null)
				target = nextNode.position;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Movement();
	}
	
	void Movement (){
		if(dying && Time.timeScale>0) {
			/*for(int i = 0; i < transform.childCount; i++) {
				transform.GetChild(i).transform.localScale.x-=0.001;
				transform.GetChild(i).transform.localScale.y-=0.001;
				transform.GetChild(i).transform.localScale.z-=0.001;
				if(transform.GetChild(i).localScale.x<=0 || transform.GetChild(i).localScale.y<=0 || transform.GetChild(i).localScale.z<=0) {
					Die();
				}
			}*/
		} else {
			// if the distance to the player is less that sqrt(200) and there isnt a wall in the way, move to the player
			RaycastHit hit;
			Vector3 heading = (new Vector3(player.position.x, transform.position.y, player.position.z)-transform.position);
			if(!Physics.Raycast (transform.position, (heading/heading.magnitude), out hit, (Vector3.Distance(player.position, transform.position)-1))) {
				// move towards the player
				Debug.Log("Move to player");
				nextNode=player;
				target = nextNode.position;
				heading = (target-transform.position);
				transform.Translate(heading/heading.magnitude*speed*Time.deltaTime);//rigidbody.AddForce((heading/heading.magnitude)*speed*100*Time.deltaTime);
			} else {
				// move towards next node
				Debug.Log("Move to next node");
				// if you are closer to next node than any other node, get the closest next node and go to that
				if(Vector2.Distance(target, transform.position) < 2) {//FindClosestNode() == nextNode) {
					Debug.Log ("Finding closest node");
					currentNode = FindClosestNode();
					Debug.Log (currentNode);
					testNode= currentNode.GetComponent("Node") as Node;
					nextNode = testNode.getNextNode();
					if (nextNode !=null){
						target = nextNode.position;
						//target.x+=Random.Range(-3.0f,3.0f);
						//target.z+=Random.Range(-3.0f,3.0f);
					}
				} else if(Vector2.Distance(player.position, transform.position) > Mathf.Sqrt(250) && nextNode==player) {
					currentNode = FindClosestNode();
					testNode= currentNode.GetComponent("Node") as Node;
					nextNode = testNode.getNextNode();
					if (nextNode !=null){
						target = nextNode.position;
						//target.x+=Random.Range(-3.0f,3.0f);
						//target.z+=Random.Range(-3.0f,3.0f);
					}
				}
				// go to next node
				if(nextNode != null) {
					heading = (target-transform.position);
					transform.Translate(heading/heading.magnitude*speed*Time.deltaTime);
				} else {
					testNode= currentNode.GetComponent("Node") as Node;
					nextNode = testNode.getNextNode();
					if(nextNode != null) {
						target = nextNode.position;
						//target.x+=Random.Range(-3.0f,3.0f);
						//target.z+=Random.Range(-3.0f,3.0f);
					}
				}
			}
		}
	}
	void FindNextNode() {
		float distance = 100000000; int node_i = -1;
		for(int i = 0; i < nodes.childCount; i++) {
			if(Vector2.Distance(new Vector2(nodes.GetChild(i).position.x, nodes.GetChild(i).position.z), new Vector2(transform.position.x, transform.position.z)) < distance && nodes.GetChild(i)!=currentNode) {
				node_i = i;
				distance = Vector2.Distance(new Vector2(nodes.GetChild(i).position.x, nodes.GetChild(i).position.z), new Vector2(transform.position.x, transform.position.z));
			}
		}
	}
	Transform FindClosestNode() {
		float distance = 100000000; int node_i = -1;
		for(int i = 0; i < nodes.childCount; i++) {
			if(Vector2.Distance(new Vector2(nodes.GetChild(i).position.x, nodes.GetChild(i).position.z), new Vector2(transform.position.x, transform.position.z)) < distance) {
				node_i = i;
				distance = Vector2.Distance(new Vector2(nodes.GetChild(i).position.x, nodes.GetChild(i).position.z), new Vector2(transform.position.x, transform.position.z));
			}
		}
		return nodes.GetChild(node_i);
	}
}
