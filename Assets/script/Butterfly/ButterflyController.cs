using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0;
    [SerializeField] Vector3 moveDirection = new Vector3(1f,1f,1f);
    [SerializeField] float randomTime = 10;
    [SerializeField] bool isRandomDirection = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        CheckPosition();
        if (isRandomDirection)
        {
            StartCoroutine(RandomDirection());
        }
    }

    void move()
    {
        this.transform.position += moveDirection * moveSpeed;
    }

    void CheckPosition()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        if(Vector3.Distance(this.transform.position, cameraPos) < ButterflyBorn.bornDistance - 0.3f || Vector3.Distance(this.transform.position, cameraPos) > ButterflyBorn.bornDistance + 0.3f)
        {
            //Debug.Log(Vector3.Distance(this.transform.position, cameraPos));
            Destroy(this.gameObject);
            GameController.amount -= 1;
        }
    }

    IEnumerator RandomDirection()
    {
        isRandomDirection = false;
        yield return new WaitForSeconds(randomTime);
        moveDirection = new Vector3(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        isRandomDirection = true;
    }
}
