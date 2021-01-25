using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool switchedOn;
    [SerializeField] private float weight = int.MaxValue;
    [SerializeField] private Transform parentNode = null;
    [SerializeField] private List<Transform> neighbourNode;

	void Start () {
        this.ResetNode();
    }

    public void ResetNode()
    {
        weight = int.MaxValue;
        parentNode = null;
    }

    public void SetParentNode(Transform node)
    {
        this.parentNode = node;
    }

    public void SetWeight(float value)
    {
        this.weight = value;
    }


    public void addNeighbourNode(Transform node)
    {
        this.neighbourNode.Add(node);
    }

    public List<Transform> GetNeighbourNode()
    {
        List<Transform> result = this.neighbourNode;
        return result;
    }

    public float GetWeight()
    {
        float result = this.weight;
        return result;

    }
    public Transform GetParentNode()
    {
        Transform result = this.parentNode;
        return result;
    }

}
