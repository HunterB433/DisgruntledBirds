using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotMed : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject projectilePrefab;
    public float velocityMult = 10f;

    public GameObject projLinePrefab;

    [Header("Dynamic")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile; // Used for SingleShot and BigShot only
    public bool aimingMode;

    public enum SlingshotMode
    {
        SingleShot,
        TripleShot,
        RapidFire,
        BigShot
    }

    public SlingshotMode currentMode;

    void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }

    void Update()
    {
        if (!aimingMode) return;

        // Handle aiming for SingleShot, TripleShot, BigShot, and RapidFire modes
        if (currentMode == SlingshotMode.SingleShot || currentMode == SlingshotMode.BigShot || currentMode == SlingshotMode.RapidFire)
        {
            Vector3 mousePos2D = Input.mousePosition;
            mousePos2D.z = -Camera.main.transform.position.z;
            Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
            Vector3 mouseDelta = mousePos3D - launchPos;
            float maxMagnitude = this.GetComponent<SphereCollider>().radius;
            if (mouseDelta.magnitude > maxMagnitude)
            {
                mouseDelta.Normalize();
                mouseDelta *= maxMagnitude;
            }

            Vector3 projPos = launchPos + mouseDelta;
            projectile.transform.position = projPos;

            if (Input.GetMouseButtonUp(0))
            {
                aimingMode = false;
                LaunchProjectile(-mouseDelta);

                // Start rapid fire for RapidFire mode
                if (currentMode == SlingshotMode.RapidFire)
                {
                    StartCoroutine(ContinuousRapidFire(-mouseDelta));
                }
            }
        }

        // Handle TripleShot mode
        if (currentMode == SlingshotMode.TripleShot && projectile != null)
        {
            Vector3 mousePos2D = Input.mousePosition;
            mousePos2D.z = -Camera.main.transform.position.z;
            Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
            Vector3 mouseDelta = mousePos3D - launchPos;
            float maxMagnitude = this.GetComponent<SphereCollider>().radius;
            if (mouseDelta.magnitude > maxMagnitude)
            {
                mouseDelta.Normalize();
                mouseDelta *= maxMagnitude;
            }

            Vector3 projPos = launchPos + mouseDelta;
            projectile.transform.position = projPos;

            if (Input.GetMouseButtonUp(0))
            {
                aimingMode = false;
                // Launch the triple shot
                LaunchTripleShot(-mouseDelta);
            }
        }
    }

    void OnMouseEnter()
    {
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        aimingMode = true;

        switch (currentMode)
        {
            case SlingshotMode.SingleShot:
                ShootSingleProjectile();
                break;

            case SlingshotMode.TripleShot:
                ShootSingleProjectile();  // Same as single, but will trigger extra projectiles on release
                break;

            case SlingshotMode.RapidFire:
                // Start aiming mode for rapid fire
                StartRapidFireAiming();
                break;

            case SlingshotMode.BigShot:
                ShootBigProjectile();
                break;
        }
    }

    void StartRapidFireAiming()
    {
        // Activate aiming mode for rapid fire, similar to other shots.
        projectile = Instantiate(projectilePrefab);
        projectile.transform.position = launchPos;
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }

    void ShootSingleProjectile()
    {
        projectile = Instantiate(projectilePrefab);
        projectile.transform.position = launchPos;
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Handle the launch of the triple shot (called when the mouse is released)
    void LaunchTripleShot(Vector3 direction)
    {
        // Launch the main projectile
        LaunchProjectile(direction);

        // Create and launch the two extra projectiles in the same direction
        for (int i = -1; i <= 1; i += 2) // -1 and 1 for the top and bottom projectiles
        {
            GameObject extraProj = Instantiate(projectilePrefab);
            extraProj.transform.position = launchPos + Vector3.up * i * 0.5f;

            Rigidbody extraRB = extraProj.GetComponent<Rigidbody>();
            extraRB.isKinematic = false;
            extraRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
            extraRB.linearVelocity = direction * velocityMult;
        }

        // Reset the main projectile
        projectile = null;

        MissionDemolition.SHOT_FIRED(); // Notify that a shot was fired
    }

    // Common function to launch any projectile
    void LaunchProjectile(Vector3 direction)
    {
        if (FollowCam.S == null)
        {
            Debug.LogError("FollowCam instance is not initialized!");
            return;
        }

        Rigidbody projRB = projectile.GetComponent<Rigidbody>();
        projRB.isKinematic = false;
        projRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
        projRB.linearVelocity = direction * velocityMult;

        FollowCam.SWITCH_VIEW(FollowCam.eView.slingshot);
        FollowCam.POI = projectile;
        Instantiate(projLinePrefab, projectile.transform);
        projectile = null;
        MissionDemolition.SHOT_FIRED();
    }

    // New continuous rapid fire
    IEnumerator ContinuousRapidFire(Vector3 direction)
    {
        float timeElapsed = 0f;
        float duration = 2f; // 5 seconds duration for rapid fire mode

        // Fire continuously in the same direction after mouse release
        while (timeElapsed < duration)
        {
            // Fire the projectile
            GameObject proj = Instantiate(projectilePrefab);
            proj.transform.position = launchPos;
            Rigidbody projRB = proj.GetComponent<Rigidbody>();
            projRB.isKinematic = false;
            projRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
            projRB.linearVelocity = direction * velocityMult;

            // Delay between shots
            yield return new WaitForSeconds(0.1f); // Adjust delay as needed

            // Increment the elapsed time
            timeElapsed += 0.1f; // Assuming 0.1f is the delay time, adjust if necessary
        }

        // Optionally, you can reset or change the mode after 5 seconds
        // For example, you could stop the rapid fire and switch modes here
    }

    void ShootBigProjectile()
    {
        projectile = Instantiate(projectilePrefab);
        projectile.transform.localScale *= 2; // Increase size
        projectile.transform.position = launchPos;
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void SetNormal()
    {
        currentMode = SlingshotMode.SingleShot;
    }

    public void SetTriple()
    {
        currentMode = SlingshotMode.TripleShot;
    }

    public void SetRapid()
    {
        currentMode = SlingshotMode.RapidFire;
    }

    public void SetBig()
    {
        currentMode = SlingshotMode.BigShot;
    }
}
