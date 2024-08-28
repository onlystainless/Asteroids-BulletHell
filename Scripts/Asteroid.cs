using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float maxLifetime = 30.0f;
    public float speed = 100.0f;
    public Sprite[] sprites;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    public float size = 1.5f;
    public float minSize = 1f;
    public float maxSize = 2.0f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
   
   
    private void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;

        _rigidbody.mass = this.size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet");
        
        {
            if ((this.size * 1.0f) >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            Destroy(this.gameObject);
        }

        if ((this.size * 2) >= this.minSize)
        {
            CreateSplit();
            CreateSplit();
            CreateSplit();
            CreateSplit();
        }
    }

    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized);
    }

}
   
    




