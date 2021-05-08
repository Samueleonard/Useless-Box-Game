/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UNUSED SCRIPT, JUST FOR INITIAL DIJKSTRA IMPLEMENTATION
public class ShortestPath : MonoBehaviour
{
    public GameObject[] switches;

    public List<Transform> FindShortestPath(Transform start, Transform end)
    {

        List<Transform> result = new List<Transform>();
        
        Transform @switch = DijkstrasAlgo(start, end);
        
        // While there's still previous node, we will continue.
        while (@switch != null)
        {
            result.Add(@switch);
            Switch currentSwitch = @switch.GetComponent<Switch>();
            @switch = currentSwitch.GetParentNode();
        }

        // Reverse the list so that it will be from start to end.
        result.Reverse();
        return result;
    }

    private Transform DijkstrasAlgo(Transform start, Transform end)
    {

        // Nodes that are unexplored
        List<Transform> unexplored = new List<Transform>();

        // We add all the nodes we found into unexplored.
        foreach (GameObject obj in switches)
        {
            Switch s = obj.GetComponent<Switch>();
            s.ResetNode();
            unexplored.Add(obj.transform);
        }

        // Set the starting node weight to 0;
        Switch startSwitch = start.GetComponent<Switch>();
        startSwitch.SetWeight(0);

        while (unexplored.Count > 0)
        {
            // Sort the explored by their weight in ascending order.
            unexplored.Sort((x, y) => x.GetComponent<Switch>().GetWeight().CompareTo(y.GetComponent<Switch>().GetWeight()));

            // Get the lowest weight in unexplored.
            Transform current = unexplored[0];

            // If we reach the end node, we will stop.
            if(current == end)
            {   
                return end;
            }

            //Remove the node, since we are exploring it now.
            unexplored.Remove(current);

            Switch currentSwitch = current.GetComponent<Switch>();
            List<Transform> neighbours = currentSwitch.GetNeighbourNode();
            foreach (Transform neighNode in neighbours)
            {
                Switch @switch = neighNode.GetComponent<Switch>();

                // We want to avoid those that had been explored and is not walkable.
                if (unexplored.Contains(neighNode))
                {
                    // Get the distance of the object.
                    float distance = Vector3.Distance(neighNode.position, current.position);
                    distance = currentSwitch.GetWeight() + distance;

                    // If the added distance is less than the current weight.
                    if (distance < @switch.GetWeight())
                    {
                        // We update the new distance as weight and update the new path now.
                        @switch.SetWeight(distance);
                        @switch.SetParentNode(current);
                    }
                }
            }

        }

        return end;
    }

}
*/