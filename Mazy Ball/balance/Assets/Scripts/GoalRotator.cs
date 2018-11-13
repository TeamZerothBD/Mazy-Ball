using System.Collections;
using UnityEngine;
public class GoalRotator : MonoBehaviour {
    void OnCollisionEnter(Collision collision)
    {
        MeshCollider tem = gameObject.GetComponent<MeshCollider>();
        Destroy(tem);
        StartCoroutine(rotateX(gameObject));
    }
    IEnumerator rotateX(GameObject goal)
    {
        for(int i=0;i<10;i++)
        {
            goal.transform.localPosition += new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(0.05f);
        }
        while (true)
        {
            goal.transform.Rotate(new Vector3(1f, 0, 0));
            yield return new WaitForSeconds(0.01f);
        }
    }
}
