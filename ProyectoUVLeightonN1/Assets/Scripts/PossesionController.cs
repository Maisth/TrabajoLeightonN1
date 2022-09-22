using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossesionController : MonoBehaviour
{

    [SerializeField]
    private GameObject playerModel;

    [SerializeField]
    private Transform enemiesInArea;
    [SerializeField]
    private Transform controlledEnemy;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ControlEnemy();

        if (controlledEnemy)
        {
            PutEnemyInPlayerPosition();
        }
    }

    void ControlEnemy()
    {
        if (Input.GetKeyDown(KeyCode.E) && enemiesInArea)
        {
            controlledEnemy = enemiesInArea;
            controlledEnemy.GetComponent<CapsuleCollider>().enabled = false;
            //Physics.IgnoreLayerCollision(3, 6, true);
        }

        else if(Input.GetKeyDown(KeyCode.E) && enemiesInArea)
        {
            controlledEnemy = null;
            playerModel.SetActive(true);
            controlledEnemy.GetComponent<CapsuleCollider>().enabled = true;
            //Physics.IgnoreLayerCollision(3, 6, false);
        }
    }

    void PutEnemyInPlayerPosition()
    {
        controlledEnemy.position = transform.position;
        playerModel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemiesInArea = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemiesInArea = null;
        }
    }
}
