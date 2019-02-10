using UnityEngine;

public class FollowPlayerButWait : MonoBehaviour
{
    public GameObject player;
    public float low = 1.4f;
    public float high = 2.4f;
    public float speed = 0.01f;

    private Vector3 start;
    private bool up = false;

    private void Update()
    {
        var distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > 5)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position - new Vector3(2, -3, 0), Time.deltaTime * 0.5f);

            start = transform.position;
        }
        else
        {
            var temp = transform.position;
            if (up)
            {
                temp.y += speed;
                transform.position = temp;
                if (transform.position.y >= high)
                {
                    up = false;
                }
            }
            else
            {
                temp.y -= speed;
                transform.position = temp;
                if (transform.position.y <= low)
                {
                    up = true;
                }
            }
        }
    }
}