﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
the class to store infomation about each waypoint node
*/
public class Waypoint : MonoBehaviour
{
	public List<Waypoint> neighbors;

	public Waypoint previous{get; set;}

	public float distance{get; set;}

	void OnDrawGizmos()
	{
		/*
		use unitys drawing system to draw a line between the waypoints - only visible in editor
		purely for debugging
		*/
		if (neighbors == null) 
			return;
		Gizmos.color = new Color (0f, 0f, 0f);
		foreach(Waypoint neighbor in neighbors)
		{
			if (neighbor != null)
				Gizmos.DrawLine (transform.position, neighbor.transform.position);
		}
	}
}
