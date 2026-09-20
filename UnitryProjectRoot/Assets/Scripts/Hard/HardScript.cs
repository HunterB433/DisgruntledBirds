using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardScript : MonoBehaviour

{
    private int lastLevel = -1;

    public MissionDemolition missionDemolitionScript;

    public HandSummoner handSummoner;
    // Start is called before the first frame update

    private void Update()
    {
        CheckAndTriggerLevelChange();
    }

    void LevelOne()
    {
        handSummoner.SummonHand(
           spawnPosition: new Vector3(4, 3, 0),
           speed: 2f,
           distance: 4f,
           angle: new Vector3(.75f, 1, 0),
           spinX: 0f,
           spinY: 0f,
           spinZ: 0f,
           resizeSpeed: 0f,
           resizeScale: 0f);

        handSummoner.SummonHand(
           spawnPosition: new Vector3(25, 5, 0),
           speed: 4f,
           distance: 8f,
           angle: new Vector3(0, 1, 0),
           spinX: 0f,
           spinY: 0f,
           spinZ: 0f,
           resizeSpeed: 0f,
           resizeScale: 0f);
    }

    void LevelTwo()
    {
        handSummoner.SummonHand(
           spawnPosition: new Vector3(0, 5, 0),
           speed: 1f,
           distance: 10f,
           angle: new Vector3(0, 1, 0),
           spinX: 0f,
           spinY: 0f,
           spinZ: 0f,
           resizeSpeed: 0f,
           resizeScale: 0f);

        handSummoner.SummonHand(
           spawnPosition: new Vector3(10, 5, 0),
           speed: 2f,
           distance: 10f,
           angle: new Vector3(0, 1, 0),
           spinX: 0f,
           spinY: 0f,
           spinZ: 0f,
           resizeSpeed: 0f,
           resizeScale: 0f);

        handSummoner.SummonHand(
           spawnPosition: new Vector3(20, 5, 0),
           speed: 3f,
           distance: 10f,
           angle: new Vector3(0, 1, 0),
           spinX: 0f,
           spinY: 0f,
           spinZ: 0f,
           resizeSpeed: 0f,
           resizeScale: 0f);

        handSummoner.SummonHand(
           spawnPosition: new Vector3(30, 5, 0),
           speed: 4f,
           distance: 10f,
           angle: new Vector3(0, 1, 0),
           spinX: 0f,
           spinY: 0f,
           spinZ: 0f,
           resizeSpeed: 0f,
           resizeScale: 0f);

        handSummoner.SummonHand(
           spawnPosition: new Vector3(40, 5, 0),
           speed: 5f,
           distance: 10f,
           angle: new Vector3(0, 1, 0),
           spinX: 0f,
           spinY: 0f,
           spinZ: 0f,
           resizeSpeed: 0f,
           resizeScale: 0f);
    }

    void LevelThree()
    {
        handSummoner.SummonHand(
           spawnPosition: new Vector3(10 , 5, 0),
           speed: 0f,
           distance: 0f,
           angle: new Vector3(0f, 0, 0),
           spinX: 0f,
           spinY: 0f,
           spinZ: 72.9123f,
           resizeSpeed: 1.1532f,
           resizeScale: 5.7123f);
    }

    void LevelFour()
    {
        for (int i = 0; i < 10; i++) // Loop 10 times
        {
            handSummoner.SummonHand(
                new Vector3(Random.Range(0f, 40f), Random.Range(0f, 20f), Random.Range(-5f, 5f)), // Spawn Position (X: 0-10, Y: 0-5, Z: -5 to 5)
                Random.Range(1f, 5f), // Speed (1 to 5)
                Random.Range(1f, 10f), // Distance (1 to 10)
                new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f)), // Angle (-30 to 30 for each axis)
                Random.Range(-10f, 10f), // SpinX (-10 to 10)
                Random.Range(-10f, 10f), // SpinY (-10 to 10)
                Random.Range(-10f, 10f), // SpinZ (-10 to 10)
                Random.Range(0.1f, 1f), // Resize Speed (0.1 to 1)
                Random.Range(1f, 3f) // Resize Scale (1 to 3)
            );
        }
    }

    public void CheckAndTriggerLevelChange()
    {
        if (missionDemolitionScript.level != lastLevel)  // Check if the level has changed
        {
            lastLevel = missionDemolitionScript.level;  // Update the last known level

            // DESTROY ALL HANDS
            handSummoner.DestroyAllHands();

            // Use a switch case to trigger functions based on the level
            switch (missionDemolitionScript.level)
            {
                case 0:
                    LevelOne();
                    break;
                case 1:
                    LevelTwo();
                    break;
                case 2:
                    LevelThree();
                    break;
                case 3:
                    LevelFour();
                    break;
                default:
                    Debug.LogWarning("Unknown level: " + missionDemolitionScript.level);
                    break;
            }
        }
    }
}
