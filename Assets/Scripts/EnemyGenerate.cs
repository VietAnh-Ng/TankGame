using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    [SerializeField]
    private GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Generate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool IsGenerate = true;
    IEnumerator Generate()
    {
        while(IsGenerate)
        {
            Instantiate(Enemy, transform);
            yield return new WaitForSeconds(5);
        }
    }

    private void OnDestroy()
    {
        IsGenerate = false;
        StopAllCoroutines();
    }
}
