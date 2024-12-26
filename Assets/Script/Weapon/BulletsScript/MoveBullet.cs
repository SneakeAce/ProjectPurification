using System.Collections;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletLifetime = 6f;

    private Bullet _bullet;

    private Coroutine _lifetimeBulletCoroutine;

}
