using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
handles finding a path to a target, does not find the target, 
just calculates a path
*/
public class PathManager : MonoBehaviour
{
	public float moveSpeed = 5.0f;

	private Stack<Vector3> currentPath;
	private Vector3 currentWaypointPosition;
	private float moveTimeTotal;
	private float moveTimeCurrent;

	//finds a path to the closest waypoint using A* algorithm (variant of dikjstra)
	public void NavigateTo(Vector3 destination)
	{
		currentPath = new Stack<Vector3> (); //use a stack to store the path
		Waypoint currentNode = FindClosestWaypoint (transform.position);
		Waypoint endNode = FindClosestWaypoint (destination);
		if (currentNode == null || endNode == null || currentNode == endNode)
			return;
		SortedList<float, Waypoint> openList = new SortedList<float, Waypoint>();
		List<Waypoint> closedList = new List<Waypoint>();
		openList.Add (0, currentNode);
		currentNode.previous = null;
		currentNode.distance = 0f;
		while (openList.Count > 0)
		{
			currentNode = openList.Values[0];
			openList.RemoveAt (0);
			float dist = currentNode.distance;
			closedList.Add (currentNode);
			if (currentNode == endNode) //if we are at the end already
				break;

			foreach (Waypoint neighbor in currentNode.neighbors)
			{
				if (closedList.Contains (neighbor) || openList.ContainsValue (neighbor))
					continue;
				neighbor.previous = currentNode;
				neighbor.distance = dist + (neighbor.transform.position - currentNode.transform.position).magnitude;
				float distanceToTarget = (neighbor.transform.position - endNode.transform.position).magnitude;
				openList.Add (neighbor.distance + distanceToTarget, neighbor);
			}
		}
		if (currentNode == endNode) //if we are at the end, go backwards
		{
			while (currentNode.previous != null)
			{
				currentPath.Push (currentNode.transform.position);
				currentNode = currentNode.previous;
			}
			currentPath.Push (transform.position);
		}
	}

	//if we reach the end of the path, stop
	public void Stop()
	{
		currentPath = null;
		moveTimeTotal = 0;
		moveTimeCurrent = 0;
	}

	void Update()
	{
		//if we have a path, move along it to the target
		if (currentPath != null && currentPath.Count > 0)
		{
			if (moveTimeCurrent < moveTimeTotal)
			{
				moveTimeCurrent += Time.deltaTime;
				if (moveTimeCurrent > moveTimeTotal)
					moveTimeCurrent = moveTimeTotal;
				transform.position = Vector3.Lerp (currentWaypointPosition, currentPath.Peek (), moveTimeCurrent / moveTimeTotal);
			} 
			else
			{
				currentWaypointPosition = currentPath.Pop ();
				if (currentPath.Count == 0) //if there are no waypoints in the path
					Stop ();
				else
				{
					moveTimeCurrent = 0;
					moveTimeTotal = (currentWaypointPosition - currentPath.Peek ()).magnitude / moveSpeed;
				}
			}
		}
	}

	//returns a waypoint object of the closest waypoint
	//similar logic to the robot class, but edited to fit this purpose
	private Waypoint FindClosestWaypoint(Vector3 target)
	{
		GameObject closest = null;
		float closestDist = Mathf.Infinity;
		foreach (GameObject waypoint in GameObject.FindGameObjectsWithTag("Waypoint"))
		{
			float dist = (waypoint.transform.position - target).magnitude;
			if (dist < closestDist)
			{
				closest = waypoint;
				closestDist = dist;
			}
		}
		if (closest != null)
			return closest.GetComponent<Waypoint>(); //return closest waypoint
		
		return null; //if no waypoint found for some reason, return nothing
	}

}
