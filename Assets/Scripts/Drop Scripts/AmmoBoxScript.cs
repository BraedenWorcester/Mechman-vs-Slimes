using UnityEngine;
using System.Collections;

public class AmmoBoxScript : MonoBehaviour {

    public GameController gC;

    public float doomTimer;
    public float birthTime;

    public bool flashing;
    public bool flashed;
    public float lastFlash;
    public float flashCooldown;

    public Sprite defaultSprite;
    public Sprite invisibleSprite;

    public int bAmmoRestoreAmount;
    public int sAmmoRestoreAmount;
    public int lAmmoRestoreAmount;
    public int hAmmoRestoreAmount;
    public int pAmmoRestoreAmount;
    public int iAmmoRestoreAmount;

    // Use this for initialization
    void Start() {
        gC = GameObject.Find("GameController").GetComponent<GameController>();
        birthTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if (Time.time >= birthTime + (doomTimer * .3f)) {
            flashing = true;
        }

        if (Time.time >= birthTime + doomTimer) {
            Destroy(gameObject);
        }

        /*if (flashing) {
            if (!flashed && Time.time >= lastFlash + flashCooldown) {
                GetComponent<SpriteRenderer>().sprite = defaultSprite;
                lastFlash = Time.time;
            }
            else if (Time.time >= lastFlash + flashCooldown) {
                GetComponent<SpriteRenderer>().sprite = invisibleSprite;
                lastFlash = Time.time;
            }
        }*/
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            Destroy(gameObject);
            PlayerController p = other.GetComponent<PlayerController>();
            p.lAmmo += lAmmoRestoreAmount / gC.hardcore;
            p.bAmmo += bAmmoRestoreAmount / gC.hardcore;
            p.sAmmo += sAmmoRestoreAmount / gC.hardcore;
            p.hAmmo += hAmmoRestoreAmount / gC.hardcore;
            p.pAmmo += pAmmoRestoreAmount / gC.hardcore;
            p.iAmmo += iAmmoRestoreAmount / gC.hardcore;
            if (p.bAmmo > p.bCAmmo) {
                p.bAmmo = p.bCAmmo;
            }
            if (p.sAmmo > p.sCAmmo) {
                p.sAmmo = p.sCAmmo;
            }
            if (p.lAmmo > p.lCAmmo) {
                p.lAmmo = p.lCAmmo;
            }
            if (p.hAmmo > p.hCAmmo) {
                p.hAmmo = p.hCAmmo;
            }
            if (p.pAmmo > p.pCAmmo) {
                p.pAmmo = p.pCAmmo;
            }
            if (p.iAmmo > p.iCAmmo) {
                p.iAmmo = p.iCAmmo;
            }

        }
    }
}

