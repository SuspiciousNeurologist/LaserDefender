using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    [Header("Enemy")]
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] int scoreValue = 150;
    
    [Header("Enemy Projectile")]
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    
    [Header("VFX")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion=1f;

    [Header("SFX")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0,1)]float deathSFXVolume = 0.7f;
    [SerializeField] AudioClip enemyShootSFX;
    [SerializeField] [Range(0,1)]float enemyShootSFXVolume = 0.7f;
    

    void Start(){
        shotCounter = Random.Range(minTimeBetweenShots,maxTimeBetweenShots);
    }

    void Update(){
        CountDownAndShoot();
    }

    private void CountDownAndShoot(){
        shotCounter -=Time.deltaTime;
        if(shotCounter<=0f){
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots,maxTimeBetweenShots);
        }
    }

    private void Fire(){
            AudioSource.PlayClipAtPoint(enemyShootSFX,Camera.main.transform.position,enemyShootSFXVolume);
            GameObject laser = Instantiate(
                enemyLaserPrefab,
                transform.position,
                Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-(-projectileSpeed)); 
    }
    private void ProcessHit(DamageDealer damageDealer){
        health -=damageDealer.GetDamage();
        damageDealer.Hit();
        if(health <=0){
            Die();
        }
    }

    private void Die(){
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        AudioSource.PlayClipAtPoint(deathSFX,Camera.main.transform.position,deathSFXVolume);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion,durationOfExplosion);
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        DamageDealer damageDealer =other.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }


}
