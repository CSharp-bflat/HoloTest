using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeComponentManager : MonoBehaviour {

    private Dictionary<GazeComponent,float> components;
    private Dictionary<GazeComponent, Coroutine> coroutines;

    private float totalTime = 0;

    [SerializeField]
    private GameObject cubePrefab;

    [SerializeField]
    private Transform ui;
    // Use this for initialization
    void Awake () {
        components = new Dictionary<GazeComponent, float>();
        coroutines = new Dictionary<GazeComponent, Coroutine>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(cubePrefab, new Vector3(Random.Range(-3, 3), 0, 10),Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(PoolTest());
        }
	}
    public void EnableGaze(GazeComponent target)
    {
        coroutines.Add(target, StartCoroutine(Gazing(target)));
    }
    private IEnumerator Gazing(GazeComponent target)
    {
        while (true)
        {
            totalTime += Time.deltaTime;
            components[target] += Time.deltaTime;
            ui.position = HighestGaze().transform.position + new Vector3(1, 0, 0);
            PercentGazeAll();
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator PoolTest()
    {
        GameObject cube1 = transform.GetChild(0).gameObject;
        cube1.SetActive(false);
        yield return new WaitForSeconds(1);
        cube1.SetActive(true);
    }
    public void DisableGaze(GazeComponent target)
    {
        StopCoroutine(coroutines[target]);
        coroutines.Remove(target);
    }
    public float GetPercentGaze(GazeComponent target)
    {
        return (components[target]/totalTime);
    }
    public void PercentGazeAll()
    {
        string toprint = "";
        foreach (var item in components)
        {
            toprint+= item.Key.gameObject.name+":"+GetPercentGaze(item.Key)+" ";
        }
        print(toprint);
    }
    public GazeComponent HighestGaze()
    {
        float highest = -1;
        GazeComponent toreturn = null;
        foreach (var item in components)
        {
            if (item.Value>highest)
            {
                highest = item.Value;
                toreturn = item.Key;
            }
        }
        return toreturn;
    }

    public void SetComponentActive(GazeComponent component,bool setactive=true)
    {
        if (setactive)
        {
            components.Add(component,0);
        }
        else if (components.ContainsKey(component))
        {
            totalTime -= components[component];
            components.Remove(component);
        }
    }
}
