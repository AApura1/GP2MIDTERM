using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFire : MonoBehaviour
{
    public Transform spawnbullet;
    public Transform target;
    public List<Transform> Enemies;
    public GameObject bulletPrefab;
    public float bulletspeed;
    public float fireRate;
    private float basefirerate;
    private Color[] colors = new Color[3]; 
    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        basefirerate = fireRate;
        colors[0] = Color.red;
        colors[1] = Color.green;
        colors[2] = Color.blue;

        ChangeColorRecursively(colors[currentIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
        fireRate -= Time.deltaTime;
        if (fireRate <= 0)
        {
            shoot();
        }
    }

    void shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnbullet.position, spawnbullet.rotation);
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.AddForce(spawnbullet.forward * bulletspeed);
        fireRate = basefirerate;
    }

    void OnMouseDown()
    {
        // Change the color of the cube and its children to the next one in the array
        currentIndex = (currentIndex + 1) % colors.Length;
        ChangeColorRecursively(colors[currentIndex]);
    }

    void ChangeColorRecursively(Color newColor)
    {
        // Change the color of the current object
        GetComponent<Renderer>().material.color = newColor;

        // Change the color of all children
        foreach (Transform child in transform)
        {
            child.GetComponent<Renderer>().material.color = newColor;
            // Recursively change color for each child's children
            ChangeColorRecursively(newColor);
        }
    }
}
