using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public GameController gC;
    public GameObject treads;

    public float moveSpeed;
    public float constantSpeed;

    #region shooting
    public float gunFireDelay; //for determining fire rate
    private float lastFireTime; //last time a gun was fired, used for determining when the gun can fire again
    public GameObject shotPrefab;
    public GameObject superPrefab;
    public GameObject pulse;
    public int spread;
    private bool lasers;
    public bool aimAssist;
    public bool superCharged;
    public bool hasTurret;
    public bool pulsed;
    public float scB;
    public float scT;
    public float aaB;
    public float aaT;
    public int ammoPerShot;
    #region Ammo
    public int bAmmo;
    public int sAmmo;
    public int lAmmo;
    public int hAmmo;
    public int pAmmo;
    public int iAmmo;
    public int activeAmmo;
    public int activeAmmoMax;
    public int bCAmmo;
    public int sCAmmo;
    public int lCAmmo;
    public int hCAmmo;
    public int pCAmmo;
    public int iCAmmo;
    public int superAmmo;
    #endregion
    #endregion

    public int gunType;
    public string gunScreenName;

    private SpriteRenderer leftGun;
    private SpriteRenderer rightGun;
    private SpriteRenderer user;

    private Transform leftShotSpawn;
    private Transform rightShotSpawn;

    private bool lastShotWasLeft;
    private bool SCY;

    public GameObject lastLeftShot;
    public GameObject lastRightShot;

    public string[] gunProperties = new string[3];

    public GameObject turretPrefab;
    public GameObject blasterPrefab;
    public GameObject shotGunPrefab;
    public GameObject radarGunPrefab;
    public GameObject hypnoGunPrefab;
    public GameObject plasmathrowerPrefab;
    public GameObject pistolShotPrefab;
    public GameObject chargePrefab;
    public GameObject chargeHolder;

    public Sprite playerSprite;
    public Sprite playerTurretSprite;
    public Sprite gunInactiveSprite;
    public Sprite gunActiveSprite;
    public Sprite blasterGunSprite;
    public Sprite shotgunSprite;
    public Sprite radarGunSprite;
    public Sprite laserGunSprite;
    public Sprite hypnoGunSprite;
    public Sprite pistolSprite;
    public Sprite plasmaThrowerSprite;
    public Sprite blasterGunActiveSprite;
    public Sprite shotgunActiveSprite;
    public Sprite radarGunActiveSprite;
    public Sprite laserGunActiveSprite;
    public Sprite hypnoGunActiveSprite;
    public Sprite pistolActiveSprite;
    public Sprite plasmaThrowerActiveSprite;

    public Text gunText;
    public Text activeAmmoText;

    public float offset;
    public float maxCharge;
    public float chargeRateDelay;
    public float lastChargeIncrease;
    public float charge;
    public float lastRadarCharge;
    public float radarChargeDelay;

    public float lastLaserDrain;
    public float timeToLaserDrain;

    public float health;
    public float maxHealth;
    public float lastTimeHit;
    public float hitDisappearTime;

    public Slider healthBar;
    public GameObject f;

  
    // Use this for initialization
    void Start()
    {
        gC = GameObject.Find("GameController").GetComponent<GameController>();
        SCY = true;
        lastFireTime = -4000; //so the gun can fire at the start
        leftGun = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        rightGun = transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        user = GetComponent<SpriteRenderer>();
        leftShotSpawn = transform.GetChild(0).transform.GetChild(0).transform;
        rightShotSpawn = transform.GetChild(1).transform.GetChild(0).transform;
        gunType = 1;
        maxHealth = health;
        healthBar.maxValue = maxHealth;
        bCAmmo = bAmmo;
        sCAmmo = sAmmo;
        lCAmmo = lAmmo;
        hCAmmo = hAmmo;
        pCAmmo = pAmmo;
        iCAmmo = iAmmo;
}

    void FixedUpdate()
    {
        //GetComponent<Rigidbody2D>().velocity = Vector3.zero;
      //  GetComponent<Rigidbody2D>().angularVelocity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // ammunition();
        if (activeAmmo == 0 && gunType != 6)
        {
            leftGun.sprite = gunInactiveSprite;
            rightGun.sprite = gunInactiveSprite;
        }
        else if (gunType == 3)
        {
            leftGun.sprite = gunActiveSprite;
            rightGun.sprite = gunActiveSprite;
        }
        else if (lastShotWasLeft)
        {
            leftGun.sprite = gunInactiveSprite;
            rightGun.sprite = gunActiveSprite;
        }
        else
        {
            leftGun.sprite = gunActiveSprite;
            rightGun.sprite = gunInactiveSprite;
        }

        if (Time.time >= lastRadarCharge + radarChargeDelay && pAmmo != pCAmmo)
        {
            pAmmo++;
            lastRadarCharge = Time.time;
        }
        iAmmo = (int)charge;

        if (gC.debugMode)
        {
            Time.timeScale = 0f;
        }
        if (gunProperties[0] != "continuous")
        {
            Destroy(lastLeftShot);
            Destroy(lastRightShot);
            lastRightShot = null;
            lastLeftShot = null;
        }

        healthBar.value = health;
        if (gunType != 2)
        {
            activeAmmoText.text = "" + activeAmmo;
        }
        else
        {
            activeAmmoText.text = "" + activeAmmo / 3;
        }

        if (gunProperties[0] != "charge")
        {
            activeAmmoText.color = new Color(70 / ((float)activeAmmo / activeAmmoMax) / 255, 135 * ((float)activeAmmo / activeAmmoMax) / 255, 94 * ((float)activeAmmo / activeAmmoMax) / 255, 255 / 255);
        }
        else
        {
            activeAmmoText.color = new Color(1, 255 * ((float)activeAmmo / activeAmmoMax) / 255 + 135, 255 * ((float)activeAmmo / activeAmmoMax) / 255 + 94, 255 / 255);
        }
        gunText.text = "" + gunScreenName;

        if (aaT >= aaB)
        {
            aimAssist = false;
        }
        else
        {
            aaT = Time.time;
        }

        if (scT >= scB)
        {
            superCharged = false;
        }
        else
        {
            scT = Time.time;
        }

        CheckGun();
        #region moving
        if (Input.GetKey(KeyCode.D))
        {
            Vector2 newPosition = new Vector2(transform.position.x + (moveSpeed * Time.deltaTime), transform.position.y);
            if (gC.ValidateLocation(newPosition)){
                transform.position = newPosition;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Vector2 newPosition = new Vector2(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y);
            if (gC.ValidateLocation(newPosition)){
                transform.position = newPosition;
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            Vector2 newPosition = new Vector2(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime));
            if (gC.ValidateLocation(newPosition)){
                transform.position = newPosition;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Vector2 newPosition = new Vector2(transform.position.x, transform.position.y - (moveSpeed * Time.deltaTime));
            if (gC.ValidateLocation(newPosition)){
                transform.position = newPosition;
            }
        }
        #endregion
        #region face camera
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        #endregion
        #region shooting
        if (activeAmmo > 0 || gC.infiniteAmmo || gC.everything || gunProperties[0] == "charge" || gunProperties[0] == "continuous")
        {
            if (gunProperties[0] == "automatic")
            {
                if (Input.GetMouseButton(0) && Time.time > lastFireTime + gunFireDelay)
                {
                    if (!superCharged)
                    {
                        activeAmmo -= ammoPerShot;
                    }

                    //Debug.Log("pew");
                    if (gunProperties[1] == "shotgun")
                    {

                        if (lastShotWasLeft)
                        {
                            for (int i = 0; i < ammoPerShot; i++)
                            {
                                GameObject pellet = (GameObject)Instantiate(shotPrefab, rightShotSpawn.position, transform.rotation);
                                lastShotWasLeft = false;
                                pellet.transform.Rotate(new Vector3(0, 0, transform.rotation.z - spread + (i * spread) + offset));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < ammoPerShot; i++)
                            {
                                GameObject pellet = (GameObject)Instantiate(shotPrefab, leftShotSpawn.position, transform.rotation);
                                lastShotWasLeft = true;
                                pellet.transform.Rotate(new Vector3(0, 0, transform.rotation.z - spread + (i * spread) + offset));
                            }
                        }
                        lastFireTime = Time.time;
                    }
                    else
                    {
                        if (lastShotWasLeft)
                        {
                            GameObject bullet = (GameObject)Instantiate(shotPrefab, rightShotSpawn.position, transform.rotation);
                            lastShotWasLeft = false;
                            bullet.transform.Rotate(new Vector3(0, 0, transform.rotation.z + Random.Range(-spread, spread)));
                            if (gunProperties[2] == "adjustable")
                            {
                                Vector3 d = Input.mousePosition - Camera.main.WorldToScreenPoint(bullet.transform.position);
                                float a = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
                                bullet.transform.rotation = Quaternion.AngleAxis(a - 90, Vector3.forward);
                            }
                        }
                        else
                        {
                            GameObject bullet = (GameObject)Instantiate(shotPrefab, leftShotSpawn.position, transform.rotation);
                            lastShotWasLeft = true;
                            bullet.transform.Rotate(new Vector3(0, 0, transform.rotation.z + Random.Range(-spread, spread)));
                            if (gunProperties[2] == "adjustable")
                            {
                                Vector3 d = Input.mousePosition - Camera.main.WorldToScreenPoint(bullet.transform.position);
                                float a = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
                                bullet.transform.rotation = Quaternion.AngleAxis(a - 90, Vector3.forward);
                            }
                        }
                        lastFireTime = Time.time;
                    }
                    ammunition();
                }
            }
            else if (gunProperties[0] == "semi")
            {
                if (Input.GetMouseButtonDown(0) && Time.time > lastFireTime + gunFireDelay)
                {
                    if (!superCharged)
                    {
                        activeAmmo -= ammoPerShot;
                    }

                    //Debug.Log("pew");
                    if (lastShotWasLeft)
                    {
                        GameObject bullet = (GameObject)Instantiate(shotPrefab, rightShotSpawn.position, transform.rotation);
                        lastShotWasLeft = false;
                        bullet.transform.Rotate(new Vector3(0, 0, transform.rotation.z + Random.Range(-spread, spread)));
                        if (gunProperties[2] == "adjustable")
                        {
                            Vector3 d = Input.mousePosition - Camera.main.WorldToScreenPoint(bullet.transform.position);
                            float a = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
                            bullet.transform.rotation = Quaternion.AngleAxis(a - 90, Vector3.forward);
                        }
                    }
                    else
                    {
                        GameObject bullet = (GameObject)Instantiate(shotPrefab, leftShotSpawn.position, transform.rotation);
                        lastShotWasLeft = true;
                        bullet.transform.Rotate(new Vector3(0, 0, transform.rotation.z + Random.Range(-spread, spread)));
                        if (gunProperties[2] == "adjustable")
                        {
                            Vector3 d = Input.mousePosition - Camera.main.WorldToScreenPoint(bullet.transform.position);
                            float a = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
                            bullet.transform.rotation = Quaternion.AngleAxis(a - 90, Vector3.forward);
                        }
                    }
                    lastFireTime = Time.time;
                    ammunition();
                }
            }
            else if (gunProperties[0] == "continuous")
            {
                if (Input.GetMouseButtonDown(0) && (activeAmmo > 0 || gC.infiniteAmmo || gC.everything))
                {
                    lastLeftShot = (GameObject)Instantiate(shotPrefab, leftShotSpawn.position, transform.rotation);
                    lastRightShot = (GameObject)Instantiate(shotPrefab, rightShotSpawn.position, transform.rotation);
                    lastLeftShot.transform.parent = leftShotSpawn;
                    lastRightShot.transform.parent = rightShotSpawn;
                    lasers = true;
                }
                else if (Input.GetMouseButton(0))
                {
                    if (!lasers)
                    {
                        lastLeftShot = (GameObject)Instantiate(shotPrefab, leftShotSpawn.position, transform.rotation);
                        lastRightShot = (GameObject)Instantiate(shotPrefab, rightShotSpawn.position, transform.rotation);
                        lastLeftShot.transform.parent = leftShotSpawn;
                        lastRightShot.transform.parent = rightShotSpawn;
                        lasers = true;
                    }

                    if (!superCharged && Time.time >= lastLaserDrain + timeToLaserDrain)
                    {
                        lastLaserDrain = Time.time;
                        activeAmmo -= ammoPerShot;
                        ammunition();
                    }
                }
                if (activeAmmo <= 0 && !gC.infiniteAmmo)
                {
                    Destroy(lastLeftShot);
                    Destroy(lastRightShot);
                    lastRightShot = null;
                    lastLeftShot = null;
                    lasers = false;
                }

                else if (Input.GetMouseButtonUp(0))
                {
                    Destroy(lastLeftShot);
                    Destroy(lastRightShot);
                    lastRightShot = null;
                    lastLeftShot = null;
                    lasers = false;
                }
            }
            else if (gunProperties[0] == "charge")
            {
                if (Input.GetMouseButton(0))
                {
                    if (chargeHolder == null)
                    {
                        if (lastShotWasLeft)
                        {
                            chargeHolder = (GameObject)Instantiate(chargePrefab, new Vector2(rightShotSpawn.position.x, rightShotSpawn.position.y), transform.rotation);
                            // lastRightShot = (GameObject)Instantiate(chargePrefab, rightShotSpawn.position, transform.rotation);
                            chargeHolder.transform.parent = rightShotSpawn;
                            //lastRightShot.transform.parent = rightShotSpawn;
                        }
                        else
                        {
                            chargeHolder = (GameObject)Instantiate(chargePrefab, leftShotSpawn.position, transform.rotation);
                            // lastRightShot = (GameObject)Instantiate(chargePrefab, rightShotSpawn.position, transform.rotation);
                            chargeHolder.transform.parent = leftShotSpawn;
                            //lastRightShot.transform.parent = rightShotSpawn;
                        }
                    }
                    if (Time.time >= lastChargeIncrease + chargeRateDelay && charge < maxCharge)
                    {
                        //activeAmmo--;
                        //ammunition();
                        if (superCharged)
                        {
                            charge++;
                        }
                        charge += 1;
                        lastChargeIncrease = Time.time;
                        chargeHolder.transform.localScale = new Vector3(1 + (1.5f * (charge / maxCharge)), 1 + (1.5f * (charge / maxCharge)), 1);
                    }
                }
                if (Input.GetMouseButtonUp(0))
                {
                    Destroy(chargeHolder);
                    chargeHolder = null;
                    if (lastShotWasLeft)
                    {
                        GameObject rightShot = (GameObject)Instantiate(shotPrefab, rightShotSpawn.position, transform.rotation);
                        rightShot.transform.localScale = new Vector3(1 + (1.5f * (charge / maxCharge)), 1 + (1.5f * (charge / maxCharge)), 1);
                        rightShot.GetComponent<BulletScript>().damage = 1 + ((int)charge);
                        lastShotWasLeft = false;
                    }
                    else
                    {
                        GameObject leftShot = (GameObject)Instantiate(shotPrefab, leftShotSpawn.position, transform.rotation);
                        leftShot.transform.localScale = new Vector3(1 + (1.5f * (charge / maxCharge)), 1 + (1.5f * (charge / maxCharge)), 1);
                        leftShot.GetComponent<BulletScript>().damage = 1 + ((int)charge);
                        lastShotWasLeft = true;
                    }
                    charge = 0;
                }
            }

            // excluded until time is found to polish
            /*if (Input.GetMouseButton(1) && Time.time > lastFireTime + gunFireDelay)
            {
                if (!superCharged && activeAmmo >= superAmmo)
                {
                    Debug.Log(superAmmo);
                    activeAmmo -= superAmmo;
                }
                if (gunType == 2)
                {
                    if (lastShotWasLeft && activeAmmo >= superAmmo)
                    {
                        for (int i = 0; i < ammoPerShot; i++)
                        {
                            GameObject pellet = (GameObject)Instantiate(superPrefab, rightShotSpawn.position, transform.rotation);
                            lastShotWasLeft = false;
                            pellet.transform.Rotate(new Vector3(0, 0, transform.rotation.z - spread + (i * spread) + offset));

                        }
                    }
                    else if(activeAmmo >= superAmmo)
                    {
                        for (int i = 0; i < ammoPerShot; i++)
                        {
                            GameObject pellet = (GameObject)Instantiate(superPrefab, leftShotSpawn.position, transform.rotation);
                            lastShotWasLeft = true;
                            pellet.transform.Rotate(new Vector3(0, 0, transform.rotation.z - spread + (i * spread) + offset));
                        }
                    }
                    lastFireTime = Time.time;
                }
                else if (gunType == 4)
                {
                    if (lastShotWasLeft)
                    {
                        GameObject bullet = (GameObject)Instantiate(superPrefab, leftShotSpawn.position, transform.rotation);
                        lastShotWasLeft = false;
                        bullet.transform.Rotate(new Vector3(0, 0, transform.rotation.z + Random.Range(-spread, spread)));
                        if (gunProperties[2] == "adjustable")
                        {
                            Vector3 d = Input.mousePosition - Camera.main.WorldToScreenPoint(bullet.transform.position);
                            float a = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
                            bullet.transform.rotation = Quaternion.AngleAxis(a - 90, Vector3.forward);
                        }
                    }
                    else
                    {
                        GameObject bullet = (GameObject)Instantiate(superPrefab, leftShotSpawn.position, transform.rotation);
                        lastShotWasLeft = true;
                        bullet.transform.Rotate(new Vector3(0, 0, transform.rotation.z + Random.Range(-spread, spread)));
                        if (gunProperties[2] == "adjustable")
                        {
                            Vector3 d = Input.mousePosition - Camera.main.WorldToScreenPoint(bullet.transform.position);
                            float a = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
                            bullet.transform.rotation = Quaternion.AngleAxis(a - 90, Vector3.forward);
                        }
                    }
                    lastFireTime = Time.time;
                }
                else if(gunType == 5)
                {
                    if (!pulsed)
                    {
                        pulse = (GameObject)Instantiate(superPrefab, transform.position, transform.rotation);
                        pulsed = true;
                    }
                }
                ammunition();
               

            }*/

                if (Input.GetMouseButtonUp(0) && gunType == 1)
                {
                    spread = 0;
                }
            }
            
            #endregion
        #region changing gun
            if (Input.GetKey(KeyCode.Alpha1))
            {
                gunType = 1;
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                gunType = 2;
            }
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                gunType = 3;
            }
            else if (Input.GetKey(KeyCode.Alpha4))
            {
                gunType = 4;
            }
            else if (Input.GetKey(KeyCode.Alpha5))
            {
                gunType = 5;
            }
            else if (Input.GetKey(KeyCode.Alpha6))
            {
                gunType = 6;
            }
            else if (Input.GetKey(KeyCode.E) && gunType < 6)
            {
                gunType++;
            }
            else if (Input.GetKey(KeyCode.Q) && gunType > 1)
            {
                gunType--;
            }
            #endregion
            if (hasTurret)
            {
                user.sprite = playerTurretSprite;
            }
            else
            {
                user.sprite = playerSprite;
            }
            if (Input.GetMouseButtonDown(2) && hasTurret)
            {
                if (superCharged)
                {
                    Instantiate(turretPrefab, transform.position, transform.rotation);
                }
                else
                {
                   Instantiate(turretPrefab, transform.position, transform.rotation);
                }
                hasTurret = false;
            }

            if (Time.time <= lastTimeHit + hitDisappearTime)
            {
                treads.GetComponent<SpriteRenderer>().color = Color.red;
                GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                if (superCharged && !SCY)
                {
                    treads.GetComponent<SpriteRenderer>().color = Color.cyan;
                    GetComponent<SpriteRenderer>().color = Color.cyan;
                    SCY = true;
                }
                else if (superCharged && SCY)
                {
                    treads.GetComponent<SpriteRenderer>().color = Color.white;
                    GetComponent<SpriteRenderer>().color = Color.white;
                    SCY = false;
                }
                else
                {
                    treads.GetComponent<SpriteRenderer>().color = Color.white;
                    GetComponent<SpriteRenderer>().color = Color.white;
                    SCY = false;
                }
            }

            if (gunType != 6)
            {
                Destroy(chargeHolder);
                chargeHolder = null;
                charge = 0;
            }

            if (health <= 0)
            {
                Destroy(f);
                Destroy(gameObject);
            }
            else if (health > maxHealth)
                health = maxHealth;
        }

    private void CheckGun()
    {
        superAmmo = 0;
        if (gunType == 1) //rifle
        {
            activeAmmo = bAmmo;
            activeAmmoMax = bCAmmo;
            gunScreenName = "Plasma Blaster";
            ammoPerShot = 1;
            gunFireDelay = .1f;
            if (superCharged)
                gunFireDelay = .05f;
            spread = 11;
            gunProperties[0] = "automatic";
            gunProperties[1] = null;
            gunProperties[2] = null;
            shotPrefab = blasterPrefab;
            gunActiveSprite = blasterGunActiveSprite;
            gunInactiveSprite = blasterGunSprite;
            offset = 0;
        }
        else if(gunType == 2) //shotgun
        {
            activeAmmo = sAmmo;
            activeAmmoMax = sCAmmo;
            gunScreenName = "Plasma Shotgun";
            ammoPerShot = 3;
            superAmmo = 12;
            gunFireDelay = .5f;
            if (superCharged)
                gunFireDelay = .2f;
            spread = 10;
            gunProperties[0] = "automatic";
            gunProperties[1] = "shotgun";
            gunProperties[2] = null;
            shotPrefab = shotGunPrefab;
            gunActiveSprite = shotgunActiveSprite;
            gunInactiveSprite = shotgunSprite;
            offset = 0;
        }
        else if (gunType == 3) //laser
        {
            activeAmmo = lAmmo;
            activeAmmoMax = lCAmmo;
            gunScreenName = "Precision Laser";
            ammoPerShot = 1;
            gunFireDelay = 0;
            spread = 0;
            gunProperties[0] = "continuous";
            gunProperties[1] = null;
            gunProperties[2] = null;
            shotPrefab = radarGunPrefab;
            gunActiveSprite = laserGunActiveSprite;
            gunInactiveSprite = laserGunSprite;
            offset = 0;
        }
        else if (gunType == 4) //hypno gun
        {
            activeAmmo = hAmmo;
            activeAmmoMax = hCAmmo;
            gunScreenName = "Hypno Gun";
            ammoPerShot = 1;
            superAmmo = hCAmmo;
            gunFireDelay = 1;
            if (superCharged)
                gunFireDelay = .4f;
            spread = 0;
            gunProperties[0] = "automatic";
            gunProperties[1] = null;
            gunProperties[2] = "adjustable";
            shotPrefab = hypnoGunPrefab;
            gunActiveSprite = hypnoGunActiveSprite;
            gunInactiveSprite = hypnoGunSprite;
            offset = 0;
        }
        else if (gunType == 5) //radar gun
        {
            activeAmmo = pAmmo;
            activeAmmoMax = pCAmmo;
            gunScreenName = "Radar Gun";
            superAmmo = 1;
            ammoPerShot = 6;
            gunFireDelay = .2f;
            if (superCharged)
                gunFireDelay = .00001f;
            spread = 10;
            gunProperties[0] = "automatic";
            gunProperties[1] = "shotgun";
            gunProperties[2] = null;
            shotPrefab = plasmathrowerPrefab;
            gunActiveSprite = plasmaThrowerActiveSprite;
            gunInactiveSprite = plasmaThrowerSprite;
            offset = -10;
        }
        else if (gunType == 6) //pistol
        {
            activeAmmo = iAmmo;
            activeAmmoMax = iCAmmo;
            gunScreenName = "Charging Pistol";
            ammoPerShot = 1;
            gunFireDelay = 0;
            spread = 0;
            gunProperties[0] = "charge";
            gunProperties[1] = null;
            gunProperties[2] = null;
            shotPrefab = pistolShotPrefab;
            gunActiveSprite = pistolActiveSprite;
            gunInactiveSprite = pistolSprite;
            offset = -10;
        }

        if (aimAssist && gunType != 5)
        {
            spread = 0;
        }
        if (superCharged)
        {
            //gunFireDelay = 0.075f;
            moveSpeed = 4.5f;
        }
        else
        {
            moveSpeed = constantSpeed;
        }
        if(gunType != 3)
        {
            lasers = false;
        }
    }
    //written by Steven
    private void ammunition()
    {
        if (activeAmmo < 0)
        {
            activeAmmo = 0;
        }
        if (gunType == 1)
        {
            bAmmo = activeAmmo;
        }
        else if (gunType == 2)
        {
            sAmmo = activeAmmo;
        }
        else if (gunType == 3)
        {
            lAmmo = activeAmmo;
        }
        else if (gunType == 4)
        {
            hAmmo = activeAmmo;
        }
        else if (gunType == 5)
        {
            pAmmo = activeAmmo;
        }
        else if (gunType == 6) {
            iAmmo = activeAmmo;
        }
    }
    //written by Steven
    private void reload()
    {
        if (gunType == 1)
        {
            bAmmo = bCAmmo;
        }
        else if (gunType == 2)
        {
            sAmmo = sCAmmo;
        }
        else if (gunType == 3)
        {
            lAmmo = lCAmmo;
        }
        else if (gunType == 4)
        {
            hAmmo = hCAmmo;
        }
        else if (gunType == 5)
        {
            pAmmo = pCAmmo;
        }
    }
}
