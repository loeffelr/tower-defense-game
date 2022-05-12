using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameController gameController;
    public double damage;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        if (gameController.enemies.Count != 0)
        {
            transform.LookAt(gameController.enemies[0].transform.position, Vector3.up);
        }

        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, transform.forward);

        Debug.DrawRay(transform.GetChild(1).position, transform.forward * 50, Color.red);

        if (Physics.Raycast(landingRay, out hit))
        {
            if (hit.collider.gameObject.name == "Sphere")
            {
                Debug.Log("hit");
                gameController.enemies[0].health -= damage * 0.1;
            }
        }
    }
}