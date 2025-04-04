using UnityEngine;
using System.Collections;
public class WanderingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    private bool _alive;
    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;


    void Start()
    {
        _alive = true; 
}
   


     void Update()
    {
        if (_alive) { 
            transform.Translate(0, 0, speed * Time.deltaTime);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (_fireball == null)
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;
                        _fireball.transform.position = transform.position + transform.forward * 3.5f + Vector3.up * 1.0f;

                        _fireball.transform.rotation = transform.rotation;
                        Rigidbody fireballRb = _fireball.GetComponent<Rigidbody>();
                        if (fireballRb != null)
                        {
                            fireballRb.useGravity = false;
                        }


                        Physics.IgnoreCollision(_fireball.GetComponent<Collider>(), GetComponent<Collider>());
                        float angle = Random.Range(-110, 110);
                        transform.Rotate(0, angle, 0);
                    } 
                }
                else if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }

        }
    }
    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}