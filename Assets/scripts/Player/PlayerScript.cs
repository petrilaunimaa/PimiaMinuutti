using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D m_Rigidbody2D;
    public float movementSpeed = 15.0f;
    public camerashake camerashakeOBJ;
    public int playerid = 1;
    public GameObject bullet;
    public AudioSource audiosource;
    public AudioClip[] shootingsounds;
    public GameObject gunpoint;

    public Color bulletColor;

    public GameStateManager gameState;
    public SpriteRenderer sr;

    private float bulletCooldown = 0f;
    [SerializeField] private float bulletMaxCooldown = 0.5f;

    [SerializeField] private ParticleSystem sparkParticlePrefab = null;

    public PostProcessVolume ppvolume;
    private ChromaticAberration chromaticAb;
    private LensDistortion lensdist;

    private void Start() {
        StartCoroutine(TeleportAnimationCoroutine());

        // initialize chromatic aberration
        ppvolume.profile.TryGetSettings(out chromaticAb);
        ppvolume.profile.TryGetSettings(out lensdist);
        chromaticAb.intensity.value = 0.13f;
        lensdist.intensity.value = 21.1f;
    }

    private IEnumerator TeleportAnimationCoroutine() {
        for (int i=0; i<10; ++i) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
            yield return new WaitForSeconds(.05f);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, .5f);
            yield return new WaitForSeconds(.05f);
        }
        for (int i=0; i<5; ++i) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
            yield return new WaitForSeconds(.1f);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            yield return new WaitForSeconds(.1f);
        }
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
    }

    // Movement force
    void Movement(float horInput, float verInput)
    {
        Vector3 heading = new Vector3(horInput, verInput, 0);
        heading.Normalize();
        m_Rigidbody2D.AddForce((heading * Time.deltaTime) * movementSpeed, ForceMode2D.Impulse);

        float rotationZ = Mathf.Atan2(heading.y, heading.x) * Mathf.Rad2Deg;
        Quaternion targetedRotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetedRotation, 20.0f * Time.deltaTime);
    }

    // Update
    void Update()
    {
        if (gameState.state != GameStateManager.GameState.ended) {
            if (playerid == 1) {
                float horInput = Input.GetAxis("WASDHorizontal");
                float verInput = Input.GetAxis("WASDVertical");
                if (horInput != 0 || verInput != 0) {
                    Movement(horInput, verInput);
                }
                if (Input.GetKeyDown("e")) {
                    shoot();
                }
            }
            if (playerid == 2) {
                float horInput = Input.GetAxis("ArrowHorizontal");
                float verInput = Input.GetAxis("ArrowVertical");
                if (horInput != 0 || verInput != 0) {
                    Movement(horInput, verInput);
                }
                if (Input.GetKeyDown("-")) {
                    shoot();
                }

            }

            bulletCooldown -= Time.deltaTime;
        }

        // Fade chromatic aberration to 0.13
        chromaticAb.intensity.value = Mathf.Lerp(chromaticAb.intensity.value, 0.13f, 2f * Time.deltaTime);

        // Fade lens distortion to 21.1
        lensdist.intensity.value = Mathf.Lerp(lensdist.intensity.value, 21.1f, 2f * Time.deltaTime);
    }

    private void shoot()
    {
        int activePlayer = gameState.ActivePlayerIndex();
        if (bulletCooldown <= float.Epsilon && (activePlayer == playerid || activePlayer == -1)) {
            bulletCooldown = bulletMaxCooldown;
            shake(0.05f);

            chromaticAb.intensity.value = 0.5f;
            lensdist.intensity.value = 19f;

            audiosource.Stop();
            audiosource.PlayOneShot(shootingsounds[Random.Range(0, shootingsounds.Length)], 0.2f);

            GameObject spawnedBullet = Instantiate(bullet);
            spawnedBullet.transform.position = gunpoint.transform.position;
            spawnedBullet.GetComponent<Rigidbody2D>().AddForce((transform.right) / 10, ForceMode2D.Force);
            spawnedBullet.GetComponent<SpriteRenderer>().color = new Color(bulletColor.r, bulletColor.g, bulletColor.b, 1);
        }
    }

    private void shake(float magnitude)
    {
        StartCoroutine(camerashakeOBJ.CameraShake(magnitude));
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "bullet") {
            shake(0.05f);
            gameState.swapTimerToPlayerIndex(playerid);
            ParticleSystem sparks = Instantiate<ParticleSystem>(sparkParticlePrefab);
            sparks.transform.position = transform.position;
        }
    }
}
