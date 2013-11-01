using UnityEngine;
using System.Collections;

public class NodeInitializer : MonoBehaviour {
	public GameObject nodeObj;
	GameObject createdNode;
	public Transform player;
	public Transform enemy;
	EnemyAI enemyAI;
	// Use this for initialization
	void Start () {
		DrawNodes(76,76);
		InitilizeNodes();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void DrawNodes (int x, int z){
		for (int i = 0; i<x; i=i+2){
			for (int n =0; n<z; n=n+2){
					Vector3 pos = new Vector3(transform.position.x+i, 0, transform.position.z+n);	
					createdNode=Instantiate(nodeObj, pos, transform.rotation) as GameObject;
					createdNode.transform.parent=transform;
			}
		}
	}
	
	void InitilizeNodes() {
		int nodeCount = 0;
		Transform node;
		Node nodeScript;
		for(int i = 0; i < transform.childCount; i++) {
			node = transform.GetChild(i);
			nodeCount = 0;
			nodeScript = node.GetComponent("Node") as Node;
			nodeScript.player = player;
			for(int r = 0; r < transform.childCount; r++) {
				if(Vector2.Distance(new Vector2(node.position.x, node.position.z), new Vector2(transform.GetChild(r).position.x, transform.GetChild(r).position.z)) < 10) {
					// add this r node to the i node's adjacent node list, as long as a ray doent come into contact with a wall;
					RaycastHit hit;
	    			Vector3 heading = (node.position - transform.GetChild(r).position);
					if ((Physics.Raycast (node.position, -1*(heading/heading.magnitude), out hit, 5)) ){
					} else {
						nodeScript.addNode(transform.GetChild(r));
						nodeCount++;
					}
				}
			}
		}
		enemyAI = enemy.GetComponent("EnemyAI") as EnemyAI;;
		enemyAI.nodes=transform;
	}
}
