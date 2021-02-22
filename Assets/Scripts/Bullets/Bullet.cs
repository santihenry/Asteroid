using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ObjectPool<Bullet> pool;
    Vector3 _dir;
    Flyweight _flyweight;
    float _currentTime;
    public List<AudioClip> allSounds;
    private AudioSource source;
    public GameObject hitFx;
    public LayerMask layerMask;


    public Bullet(Vector3 dir,ObjectPool<Bullet> bulletPool)
    {
        _dir = dir;
        pool = bulletPool;
    }

    public Bullet SetPool(ObjectPool<Bullet> bulletPool)
    {
        pool = bulletPool;
        return this;
    }

    public Bullet SetInitPos(Vector3 pos)
    {
        transform.position = pos;      
        return this;

    }

    public Bullet SetDir(Vector3 dir)
    {
        _dir = dir;
        return this;
    }

    public Bullet SetFlyweight(Flyweight flyweight)
    {
        _flyweight = flyweight;
        return this;
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    private void Start()
    {
        if (!source.isPlaying || source.clip != allSounds[Sounds.shootLazer])
        {
            source.clip = allSounds[Sounds.shootLazer];
            source.Play();
        }
    }

    private void Update()
    {
        transform.position += _dir * _flyweight.speed * Time.deltaTime;
        Alive();
    }


    public void Alive()
    {
        _currentTime += Time.deltaTime;
        if (_flyweight.lifeTime < _currentTime)
        {
            Death();
        }
    }

    public void Explotion()
    {
        foreach (var item in Physics.OverlapSphere(transform.position,_flyweight.radius,layerMask))
        {
            item.GetComponent<Asteroids>().TakeDamage = _flyweight.damage;
        }
        Death();      
    }



    public void Death()
    {
        if ( _flyweight.explosiveBullet && (!source.isPlaying || source.clip != allSounds[Sounds.hit]))
        {
            GameObject f = new GameObject();
            f.name = "ExplotionSound";
            f.AddComponent<AudioSource>().clip = allSounds[Sounds.hit];
            f.GetComponent<AudioSource>().Play();
            Destroy(f, .3f);
        }
        var fx = Instantiate(hitFx, transform.position, transform.rotation);
        Destroy(fx, .5f);
        _currentTime = 0;
        TurnOff(this);
        pool.Recycle(this);
    }

    
    public static void TurnOn(Bullet bullet) 
    { 
        bullet.gameObject.SetActive(true);
    }

    public static void TurnOff(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    public float TakeDamage
    {
        get
        {
            return _flyweight.damage;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Asteroids>())
        {
            Death();
        }
    }

}
