using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveBullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletLifetime = 6f;

    private Bullet _bullet;

    private Coroutine _lifetimeBulletCoroutine;

    public void Initialize(Transform spawnPointTransform, Vector3 directionToMousePosition, Bullet bullet)
    {
        _bullet = bullet;

        BulletMove(spawnPointTransform, directionToMousePosition);

        _lifetimeBulletCoroutine = StartCoroutine(LifetimeBulletJob(_bulletLifetime));
    }

    private void BulletMove(Transform spawnPointTransform, Vector3 directionToMousePosition)
    {
        Vector3 direction = directionToMousePosition - spawnPointTransform.position;

        _bullet.Rigidbody.AddForce(direction.normalized * _bulletSpeed);
    }

    private IEnumerator LifetimeBulletJob(float time)
    {
        Debug.Log("Coroutine Timer Start");

        while (time > 0)
        {
            time -= Time.deltaTime;
            Debug.Log("Time in while");
            yield return null;
        }

        Debug.Log("Timer is Done");

        Destroy(this.gameObject);

        StopCoroutine(_lifetimeBulletCoroutine);
        _lifetimeBulletCoroutine = null;
    }

}
