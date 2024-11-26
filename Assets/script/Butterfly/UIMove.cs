using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMove : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] float distance;
    [SerializeField] float speed;
    [SerializeField] float far;
    Vector3 newPos;
    Quaternion newRotate;
    float difTime;
    bool Changing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        check();
    }

    void SetNewPos()
    {
        newPos = Vector3.ClampMagnitude(camera.transform.forward*1000, distance) + camera.transform.position;
        newRotate = camera.transform.rotation;
        newRotate.z = 0f;
        StartCoroutine(ChangePos());
    }

    IEnumerator ChangePos()
    {
        yield return new WaitForSeconds(1f);
        this.transform.rotation = newRotate;
        while(this.transform.position != newPos)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, newPos, speed * Time.deltaTime);
            yield return 0;
        }
        Changing = false;
    }

    void check()
    {
        if(Vector3.Distance(Vector3.ClampMagnitude(camera.transform.forward * 1000, distance) + camera.transform.position, this.transform.position) > far && !Changing)
        {
            if(Time.time - difTime > 1.5f)
            {
                SetNewPos();
                Changing = true;
            }
        }
        else
        {
            difTime = Time.time;
        }
    }
}
