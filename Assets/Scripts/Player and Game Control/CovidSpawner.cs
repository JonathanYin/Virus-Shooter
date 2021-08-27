using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CovidSpawner : MonoBehaviour
{
   public float spawnDistance = 11.0f;
    public float trajectoryVariance = 15.0f;
    public GameObject covidPrefab;
    public float spawnRate = 2.0f;
    public int spawnAmount = 1;
    public void Start() {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    public void Spawn() {
        for (int i = 0; i < this.spawnAmount; i++) {
            Vector2 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnpoint = this.spawnDistance * spawnDirection;


            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            GameObject covidobj = PhotonNetwork.Instantiate(this.covidPrefab.name, spawnpoint, rotation);
            Covid covid = covidobj.GetComponent<Covid>();
            covid.size = Random.Range(covid.minSize, covid.maxSize);
            covid.setTrajectory(rotation * -spawnDirection);
        }
    }
    }
