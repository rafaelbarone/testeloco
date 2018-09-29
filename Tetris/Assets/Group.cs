using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor.Build;
using UnityEngine;

public class Group : MonoBehaviour {

	// Variable that tells when was the last time gravity acted
	protected float lastFall = 0;
	
	// checks if group is inside the borders and not on top of other block
	bool isValidGridPos()
	{
		foreach (Transform child in transform)
		{
			Vector2 v = Grid.roundVec2(child.position);

			// check if grid position between borders
			if (!Grid.insideBorder(v))
			{
				return false;
			}
			
			// checks if there is a block on that position which is not part of the same group
			if (Grid.grid[(int) v.x, (int) v.y] != null && Grid.grid[(int) v.x, (int) v.y].parent != transform)
			{
				return false;
			}
		}
		return true;
	}
	
	void updateGrid()
	{
		// remove old children (old drawn blocks) from grid
		for (int y = 0; y < Grid.h; ++y)
		{
			for (int x = 0; x < Grid.w; ++x)
			{
				if (Grid.grid[x, y] != null)
				{
					if (Grid.grid[x, y].parent == transform)
					{
						Grid.grid[x, y] = null;
					}
				}
			}
		}
		
		// add new children to grid
		foreach (Transform child in transform)
		{
			Vector2 v = Grid.roundVec2(child.position);
			Grid.grid[(int) v.x, (int) v.y] = child;
		}
		{
			
		}
	}

	// Use this for initialization
	void Start () {
		if (!isValidGridPos())
		{
			Debug.Log("GAME OVER BRO");
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Move group left
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			//Modify position
			transform.position += new Vector3(-1, 0, 0);
			
			//Check validity
			if (isValidGridPos())
			{
				updateGrid();
			}
			else
			{
				transform.position += new Vector3(1, 0, 0);
			}
		}
		
		// Move group right
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			// Modify position
			transform.position += new Vector3(1, 0, 0);
			
			// Check validity
			if (isValidGridPos())
			{
				updateGrid();
			}
			else
			{
				transform.position += new Vector3(-1, 0, 0);
			}
		}
		
		// Rotate
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			transform.Rotate(0, 0, -90);
			
			// Check validity
			if (isValidGridPos())
			{
				updateGrid();
			}
			else
			{
				transform.Rotate(0, 0, 90);
			}
		}
		
		// Fall
		else if (Input.GetKeyDown(KeyCode.DownArrow) || (Time.time - lastFall >= 1))
		{
			// Modify position
			transform.position += new Vector3(0, -1, 0);

			// Check validity
			if (isValidGridPos())
			{
				updateGrid();
			}
			else
			{
				transform.position += new Vector3(0, 1, 0);
				Grid.deleteFullRows();
				FindObjectOfType<Spawner>().spawnNext();
				enabled = false;
			}

			lastFall = Time.time;
		}
		
		// Fall fast
		else if (Input.GetKeyDown((KeyCode.Space)))
		{
			while (isValidGridPos())
			{
				transform.position += new Vector3(0, -1, 0);
			}
			transform.position += new Vector3(0, 1, 0);
			updateGrid();
			Grid.deleteFullRows();
			FindObjectOfType<Spawner>().spawnNext();
			enabled = false;
			lastFall = Time.time; 
		}

	}

}