using UnityEngine;
using System.Collections;


public class Node : MonoBehaviour {
	Transform nextNode;
	public Transform player;
	private ArrayList adjacent_nodes = new ArrayList();
	Transform adjNode;
	Transform pointer;
	int id = 0;
	int nodeCount =0;
	float node_length = 0;
	bool traces_to_player = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		FindNextNode();
	}

	void FindNextNode() {
		double distance = 100000000.0; int node_i = -1;
		double smallest_node_length = 10000;
		RaycastHit hit;
		Ray ray;
		Node node;
		Node testNode;
		for(int i = 0; i < adjacent_nodes.Count; i++) {
			adjNode = adjacent_nodes[i] as Transform;
			if (adjNode!=null){
				testNode=adjNode.GetComponent("Node") as Node;
				if(testNode.GetPathLength() <= smallest_node_length && testNode.nextNode != transform && testNode.TracesToPlayer()) {
						node = testNode;
						node_i=i;
						smallest_node_length = node.GetPathLength();
				}
			}
		}
		if(node_i < adjacent_nodes.Count && node_i >= 0) {
			nextNode = adjacent_nodes[node_i] as Transform;
			traces_to_player=true;
			//Debug.Log(adjacent_nodes[node_i].position);
			//pointer.position.x=(nextNode.position.x+transform.position.x)/2;
			//pointer.position.z=(nextNode.position.z+transform.position.z)/2;
			//pointer.LookAt(nextNode);
			//pointer.Rotate(0,90,0);
			//pointer.rotation.eulerAngles.y=Vector2.Angle(Vector2(nextNode.position.x, nextNode.position.z), Vector2(transform.position.x, transform.position.z));
		} else {
			traces_to_player = false;
		}
		if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z),  new Vector2(player.position.x, player.position.z)) < Mathf.Sqrt(200)) {
	    	Vector3 heading = (new Vector3(player.position.x, player.position.y, player.position.z)-transform.position);
	    	RaycastHit hit2;
			if(!Physics.Raycast (new Vector3(transform.position.x, transform.position.y, transform.position.z), (heading/heading.magnitude), out hit2, Vector3.Distance(player.position, transform.position))) { //Physics.Raycast (transform.position, (heading/heading.magnitude), hit2, 15)) {
				nextNode = player;
			} else {
			}
		}
		if(nextNode !=null) {
			testNode=nextNode.GetComponent ("Node") as Node;
			if(testNode != null) {
				node_length = testNode.GetPathLength()+1;
			} else {
				traces_to_player = true;
				node_length = 1;
			}
		}
		if(nextNode==player) {
			RaycastHit hit2;
			Vector3 heading = (new Vector3(player.position.x, player.position.y, player.position.z)-transform.position);
			heading = (new Vector3(player.position.x, player.position.y, player.position.z)-transform.position);
			if(Physics.Raycast (new Vector3(transform.position.x, transform.position.y, transform.position.z), (heading/heading.magnitude), out hit2, Vector3.Distance(player.position, transform.position)) && Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(player.position.x, player.position.z)) > Mathf.Sqrt(200)) {
				//traces_to_player = false;
				node_length = 0;
			} else {
				//traces_to_player = true;
			}
		}
	}
	
	public float GetPathLength() {
		return node_length;
	}
	public bool TracesToPlayer() {
		return traces_to_player;
	}
	public void addNode (Transform N){
		adjacent_nodes.Add(N);
		nodeCount++;
	}
	public Transform getNextNode (){
		return nextNode;	
	}
}
