using UnityEngine;

public class ElasticCameraFollow : MonoBehaviour
{
    // +++++++++++++++++++++++++ ATTRIBUTES +++++++++++++++++++++++++
    [SerializeField] private GameObject target; // The character to follow
    [SerializeField] private float speed = 2.0f;

    // +++++++++++++++++++++++++ METHODS +++++++++++++++++++++++++
    
    void Update () {
        float interpolation = speed * Time.deltaTime;
        
        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, target.transform.position.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, target.transform.position.x, interpolation);
        
        this.transform.position = position;
    }
}
