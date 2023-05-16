using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameremove : MonoBehaviour
{
    public GameObject target;
    
    // Start is called before the first frame update
    

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(target.transform.position.x, transform.position.y, -10);
    }
}
