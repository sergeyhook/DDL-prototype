using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    //Stats
    public float HP;
    public float MaxHP;
    public float moveSpeed;
    public float jumpSpeed;
    public float Xp;
    public float Mana;
    public float ManaRegen;
    public float MaxMana;
    public bool UnderRadiation;
    public float RadiationAmount;
    public float MaxRadiation;
    public float RadProtection;
    //Other
    public HealthBar RadBar;
    public TMP_Text CoinText;
    public int MaxWeapons;
    public int selectedWeapon = 0;
    public GameObject WeaponHolder;
    public HealthBar healthBar;
    public Camera playerCamera;
    public float rotateSpeed;
    CharacterController characterController;
    public bool OnGround;
    private Vector2 m_Rotation;
    private Vector2 m_Look;
    private Vector2 m_Move;
    private LayerMask ground;
    public Rigidbody rb;
    public static bool GameIsPaused;
    public GameObject pauseMenuUI;
    public float CoinAmount;
    public GameObject Shop;
    public bool ShopInRange;
    public bool DoorInRange;
    public GameObject Door;
    public TMP_Text XpText;
    public bool Shooting;
    public float BPM;
    public float ShootTime;
    public float ShootingTime;
    public HealthBar ManaBar;
    public GameObject spawner;
    private AsyncOperation sceneAsync;
    public GameObject TransferGameObject;
    public int Levels;
    public int Enemies;
    public GameObject BackgroundVar;
    public GameObject Portal;


    // Skill Panel
    public TMP_Text Description;
    public TMP_Text Stats;
    public GameObject SkillPanel;
    public bool SkillPanelOpenned;
    public float RequiredHealthXP;
    public float ExpansionHealth;
    public int SelectedToBuy;
    public float Skill1Cooldown;
    public float Skill2Cooldown;
    public float Skill1CooldownTimer;
    public float Skill2CooldownTimer;
    public bool Skill1Ready;
    public bool Skill2Ready;
    public bool Skill1Empty=true;
    public bool Skill2Empty=true;
    public int Skill1ID;
    public int Skill2ID;
    public Image Skill1Image;
    public Image Skill2Image;


    //PickUpWeapons
    RaycastHit hit;
    public float distance;
    public bool canGrab;
    GameObject wp;
    

    //Skills

    //FireBall
    public float FireballCooldown;
    public GameObject Fireball;
    public float FireballCost;
    public float FireballDamage;
    public float ExpansionDamageFireball;
    public float RequiredFireballXP;
    public bool FireballUnlocked;
    public Sprite FireballSprite;
    //Mana
    public float ManaExpansion;
    public float RequiredManaXP;

    //ManaRegen
    public float ManaRegenExpansion;
    public float RequiredManaRegenXp;

    //HPBuff
    public float HPBuff;
    public float HPBuffExpansion;
    public float RequiredHPBuffXp;
    public float HPBuffCooldown;
    public float HPBuffCost;
    public Sprite HPBuffSprite;
    //ManaBuff
    public float ManaBuff;
    public float ManaBuffExpansion;
    public float RequiredManaBuffXp;
    public float ManaBuffCooldown;
    public float ManaBuffCost;
    public Sprite ManaBuffSprite;
    //RadProt
    public float RadDecrease;
    public float RadDecreaseXPCost;

    void Awake()
    {
        MaxWeapons -= 1;
        HP = MaxHP;
        Mana = MaxMana;
        healthBar.SetMaxHealth(MaxHP);
        ManaBar.SetMaxHealth(MaxMana);
        RadBar.SetMaxHealth(MaxRadiation);
        characterController = GetComponent<CharacterController>();
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        DontDestroyOnLoad(this.gameObject);
        RadBar.SetHealth(RadiationAmount);
        healthBar.SetHealth(HP);
        ManaBar.SetHealth(Mana);
    }
    void Start()
    {
        BackgroundVar = GameObject.Find("BackgroundScripts");
        Portal = GameObject.Find("Portal");
        BackgroundVar.GetComponent<globals>().PlayerAlive = true;
        CoinAmount = BackgroundVar.GetComponent<globals>().PlayerMoney;
    }
    IEnumerator LoadAsyncScene(int index)
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();
        Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(index);
        if (index == 1)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Hub", LoadSceneMode.Single);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            transform.position = new Vector3(0, 2, 0);
        }
        if (index == 2)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            transform.position = new Vector3(0, 2, 0);
        }
        if (index == 3)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Boss", LoadSceneMode.Single);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            transform.position = new Vector3(0, 2, 0);
        }
        if (index == 4)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Tutorial", LoadSceneMode.Single);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            transform.position = new Vector3(0, 2, 0);
        }

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        m_Move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        m_Look = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (OnGround == true)
        {
            rb.velocity = new Vector3(rb.velocity.y, jumpSpeed);
            OnGround = false;
        }
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        float z = context.ReadValue<float>();
        if (z > 0)
            selectedWeapon += 1;
        else if (z < 0)
            selectedWeapon -= 1;
    }
    public void OnDrop(InputAction.CallbackContext context)
    {
        if (MaxWeapons >= 0)
        {
            wp = null;
            wp = WeaponHolder.transform.GetChild(selectedWeapon).gameObject;
            wp.transform.parent = null;
            wp.GetComponent<Rigidbody>().isKinematic = false;
            wp.SetActive(true);
            wp = null;
            MaxWeapons -= 1;
        }
        
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        ShootingTime = 0f;
        Shooting = true;
        if (context.canceled)
        {
            Shooting = false;
        }
        
    }
    public void PauseMenu(InputAction.CallbackContext context)
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
        if (SkillPanelOpenned)
        {
            SkillPanel.SetActive(false);
            SkillPanelOpenned = false;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SelectedToBuy = 0;
            Description.text = null;
            Stats.text = null;

        }
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (DoorInRange&& CoinAmount>=Door.GetComponent<Door>().CostToOpen)
        {
            CoinAmount -= Door.GetComponent<Door>().CostToOpen;
            Destroy(Door);
            DoorInRange = false;
            Door = null;
        }
        if(Physics.Raycast(playerCamera.transform.position,playerCamera.transform.forward,out hit, distance))
        {
            if(hit.transform.tag == "Grab")
            {
                wp = hit.transform.gameObject;
                wp.transform.position = WeaponHolder.transform.position;
                wp.transform.parent = WeaponHolder.transform;
                wp.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                wp.GetComponent<Rigidbody>().isKinematic = true;
                wp = null;
                MaxWeapons += 1;
            }
            if(hit.transform.tag == "Shop" && CoinAmount >= Shop.GetComponent<Shop>().Cost&& context.canceled)
            {
                Shop.GetComponent<Shop>().AvailableMoney = CoinAmount;
                Shop.GetComponent<Shop>().SellItem();
                CoinAmount = Shop.GetComponent<Shop>().AvailableMoney;
            }
        }
    }
    public void SkillWindow(InputAction.CallbackContext context)
    {
        if(SkillPanelOpenned == true)
        {
            SkillPanel.SetActive(false);
            SkillPanelOpenned = false;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SelectedToBuy = 0;
            Description.text = null;
            Stats.text = null;

        }
        else
        {
            
            SkillPanel.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SkillPanelOpenned = true;
            SelectedToBuy = 0;
        }
        
    }
    public void Skill1(InputAction.CallbackContext context)
    {
        if (Skill1Ready)
        {
            if(Skill1ID == 1)
            {
                Mana -= FireballCost;
                Instantiate(Fireball, spawner.transform.position, spawner.transform.rotation);
                Skill1Cooldown = 0;
                Skill1Ready = false;
            }
            if (Skill1ID == 2)
            {
                Mana -= HPBuffCost;
                HP += HPBuff;
                Skill1Cooldown = 0;
                Skill1Ready = false;
            }
            if (Skill1ID == 3)
            {
                HP -= ManaBuffCost;
                Mana += ManaBuff;
                Skill1Cooldown = 0;
                Skill1Ready = false;
            }
            healthBar.SetHealth(HP);
            ManaBar.SetHealth(Mana);
        }
}
    public void Skill2(InputAction.CallbackContext context)
    {
        if (Skill2Ready)
        {
            if (Skill2ID == 1)
            {
                Mana -= FireballCost;
                Instantiate(Fireball, playerCamera.transform.position + new Vector3(1f, 0.4f, 1f), playerCamera.transform.rotation);
                Skill2Cooldown = 0;
                Skill2Ready = false;
            }
            if (Skill2ID == 2)
            {
                Mana -= HPBuffCost;
                HP += HPBuff;
                Skill2Cooldown = 0;
                Skill2Ready = false;
            }
            if (Skill2ID == 3)
            {
                HP -= ManaBuffCost;
                Mana += ManaBuff;
                Skill2Cooldown = 0;
                Skill2Ready = false;
            }
            healthBar.SetHealth(HP);
            ManaBar.SetHealth(Mana);
        }
    }
        public void Update()
    {
        BackGround();
        Look(m_Look);
        Move(m_Move);
        WeaponHolder.GetComponent<ChooseWeapon>().SelectWeapon(selectedWeapon);
        healthBar.SetHealth(HP);
        ManaBar.SetHealth(Mana);
        RadBar.SetHealth(RadiationAmount);
    }


    

    private void Move(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01)
            return;
        var scaledMoveSpeed = moveSpeed * Time.deltaTime;

        var move = Quaternion.Euler(0, playerCamera.transform.localEulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
        transform.position += move * scaledMoveSpeed;
    }

    private void Look(Vector2 rotate)
    {
        if (rotate.sqrMagnitude < 0.01)
            return;
        var scaledRotateSpeed = rotateSpeed * Time.deltaTime;
        m_Rotation.y += rotate.x * scaledRotateSpeed;
        m_Rotation.x = Mathf.Clamp(m_Rotation.x - rotate.y * scaledRotateSpeed, -89, 89);
        playerCamera.transform.localEulerAngles = m_Rotation;
    }
    
    private void BackGround()
    {
        RadBar.SetHealth(RadiationAmount);
        BackgroundVar.GetComponent<globals>().PlayerMoney = CoinAmount;
        Enemies = BackgroundVar.GetComponent<globals>().Enemies;
        if (!UnderRadiation)
        {
            Mana += ManaRegen * Time.deltaTime;
        }
        
        Skill1Cooldown += Time.deltaTime;
        if (Skill1Cooldown >= Skill1CooldownTimer)
        {
            Skill1Ready = true;
        }
        Skill2Cooldown += Time.deltaTime;
        if (Skill2Cooldown >= Skill2CooldownTimer)
        {
            Skill2Ready = true;
        }
        if (Shooting)
        {
            ShootingTime += Time.deltaTime;
        }
        BPM = WeaponHolder.GetComponent<ChooseWeapon>().BPM;
        ShootTime = WeaponHolder.GetComponent<ChooseWeapon>().ShootTime;
        if (Shooting==true && ShootingTime >= ShootTime&& Mana>= WeaponHolder.GetComponent<ChooseWeapon>().CostToShoot)
        {
            Mana -= WeaponHolder.GetComponent<ChooseWeapon>().CostToShoot;
            WeaponHolder.GetComponent<ChooseWeapon>().Shoot();
            ShootingTime = 0f;
        }
        if (HP < 0)
        {
            StartCoroutine(LoadAsyncScene(1));
            BackgroundVar.GetComponent<globals>().PlayerAlive = false;
            BackgroundVar.GetComponent<globals>().Enemies = 0;
            BackgroundVar.GetComponent<globals>().PlayerMoney = 5;
            Destroy(this.gameObject);
        }
        if (selectedWeapon > MaxWeapons)
        {
            selectedWeapon = 0;
        }
        if (selectedWeapon < 0)
        {
            selectedWeapon = MaxWeapons;
        }
        if (MaxWeapons < 0)
        {
            MaxWeapons = 0;
        }
        if (HP > MaxHP)
        {
            HP = MaxHP;
        }
        if (Mana > MaxMana)
        {
            Mana = MaxMana;
        }
        if (RadiationAmount > MaxRadiation)
        {
            RadiationAmount = MaxRadiation;
        }
        if (RadiationAmount < 0)
        {
            RadiationAmount = 0;
        }
        HP -= RadiationAmount * RadProtection*0.005f;
        Portal = GameObject.Find("Portal");
        if (Levels >= 5)
        {
            Portal.GetComponent<Portal>().Index = 3;
        }
        CoinText.text =  CoinAmount.ToString();
        XpText.text = Xp.ToString();

        

    }
        private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            OnGround = true;
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            if (collision.gameObject.GetComponent<Item>().Type == "HP"&&HP<MaxHP)
            {
                HP += collision.gameObject.GetComponent<Item>().Amount;
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.GetComponent<Item>().Type == "Coin")
            {
                CoinAmount += collision.gameObject.GetComponent<Item>().Amount;
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.CompareTag("Ground"))
            {
                OnGround = true;
            }
        }
        if (collision.gameObject.CompareTag("Door"))
        {
            
            DoorInRange = true;
            Door = collision.gameObject;
        }
        if (collision.gameObject.CompareTag("Missle"))
        {
            HP -= collision.GetComponent<Missle>().Damage;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Shop"))
        {
            ShopInRange = true;
            Shop = collision.gameObject;

        }
        if (collision.gameObject.CompareTag("Portal"))
        {
            if (Enemies<=1)
            {
                BackgroundVar.GetComponent<globals>().Enemies = 0;
                Levels += 1;
                StartCoroutine(LoadAsyncScene(collision.GetComponent<Portal>().Index));
                if(collision.GetComponent<Portal>().Index == 1)
                {
                    Destroy(this.gameObject);
                }
            }
            

        }

    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            OnGround = false;
        }
        if (collision.gameObject.CompareTag("Shop"))
        {
            ShopInRange = false;
            Shop = null;

        }
        if (collision.gameObject.CompareTag("Door"))
        {
            DoorInRange = false;
            Door = null;
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void LoadMenu()
    {
        
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(LoadAsyncScene(0));
        Destroy(this.gameObject);
        Destroy(BackgroundVar);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void UpgradeHealth()
    {
        SelectedToBuy = 1;
        Description.text = "Благодаря этому ты можешь повысить количество здоровья";
        Stats.text = "Максимальное здоровье сейчас = " + MaxHP.ToString() + "\n" + "Максимальное здоровье, если повысить уровень = " + (MaxHP + ExpansionHealth).ToString()+"\n"+"Стоимость повышения "+RequiredHealthXP.ToString();

    }
    public void UpgradeFireball()
    {
        SelectedToBuy = 2;
        Description.text = "Стреляй огнеными шарами";
        Stats.text = "Урон фаерболла = "+ FireballDamage.ToString()+"\n"+"Стоимость обновления " + RequiredFireballXP.ToString();

    }
    
    public void UpgradeMana()
    {
        SelectedToBuy = 3;
        Description.text = "Благодаря этому ты можешь повысить количество маны";
        Stats.text = "Максимальная мана сейчас = " + MaxMana.ToString() + "\n" + "Максимальная мана, если повысить уровень = " + (MaxMana + ManaExpansion).ToString() + "\n" + "Стоимость повышения " + RequiredManaXP.ToString();

    }
    public void UpgradeManaRegen()
    {
        SelectedToBuy = 4;
        Description.text = "Благодаря этому ты можешь повысить скорость регенерации маны";
        Stats.text = "Скорость регенерации маны сейчас = " + ManaRegen.ToString() + "\n" + "Скорость регенерации маны, если повысить уровень = " + (ManaRegen + ManaRegenExpansion).ToString() + "\n" + "Стоимость повышения " + RequiredManaRegenXp.ToString();

    }
    public void UpgradeHPBuff()
    {
        SelectedToBuy = 5;
        Description.text = "Восстанови здоровье моментально, потратив ману";
        Stats.text = "Восстановление здоровья = " + HPBuff.ToString() + "\n" + "Восстановление здоровья, если повысить уровень = " + (HPBuff + HPBuffExpansion).ToString()  + "\n" + "Стоимость обновления " + RequiredHPBuffXp.ToString();

    }
    public void UpgradeManaBuff()
    {
        SelectedToBuy = 6;
        Description.text = "Восстанови ману моментально,потратив здоровье";
        Stats.text = "Восстановление маны = " + ManaBuff.ToString() + "\n" + "Восстановление маны, если повысить уровень = " + (ManaBuff + ManaBuffExpansion).ToString() + "\n" + "Стоимость обновления " + RequiredManaBuffXp.ToString();

    }
    public void UpgradeRadProt()
    {
        SelectedToBuy = 7;
        Description.text = "Увеличь защиту от радиации";
        Stats.text = "Защита от радиации = " + (100 - RadProtection * 100).ToString() + "\n" + "Защита от радиации, если повысить уровень = " + (100-(RadProtection - RadDecrease)*100).ToString() + "\n" + "Стоимость обновления " + RadDecreaseXPCost.ToString();

    }
    public void SkillUpgrade()
    {
        if (SelectedToBuy == 1&&Xp >= RequiredHealthXP)
        {
            Xp -= RequiredHealthXP;
            MaxHP += ExpansionHealth;
            HP = MaxHP;
            RequiredHealthXP = RequiredHealthXP * 2+ Random.Range(0,10);
            Stats.text = "Максимальное здоровье сейчас = " + MaxHP.ToString() + "\n" + "Максимальное здоровье, если повысить уровень = " + (MaxHP + ExpansionHealth).ToString() + "\n" + "Стоимость повышения " + RequiredHealthXP.ToString();
        }
        if (SelectedToBuy == 2 && Xp >= RequiredFireballXP)
        {
            Xp -= RequiredFireballXP;
            FireballDamage += ExpansionDamageFireball;
            FireballUnlocked = true;
            if (Skill1Empty && Skill2ID != 1)
            {
                Skill1ID = 1;
                Skill1Empty = false;
                Skill1CooldownTimer = FireballCooldown;
                Skill1Image.sprite = FireballSprite;
            }
            if(Skill2Empty&& Skill1Empty == false && Skill1ID != 1)
            {
                Skill2ID = 1;
                Skill2Empty = false;
                Skill2CooldownTimer = FireballCooldown;
                Skill2Image.sprite = FireballSprite;
            }
            RequiredFireballXP = RequiredFireballXP + Random.Range(4,9);
            Stats.text = "Урон фаерболла = " + FireballDamage.ToString() + "\n" + "Стоимость обновления " + RequiredFireballXP.ToString();
        }
        if (SelectedToBuy == 3 && Xp >= RequiredManaXP)
        {
            Xp -= RequiredManaXP;
            MaxMana += ManaExpansion;
            Mana = MaxMana;
            RequiredManaXP = RequiredManaXP * 2+Random.Range(0,10);
            Stats.text = "Максимальная мана сейчас = " + MaxMana.ToString() + "\n" + "Максимальная мана, если повысить уровень = " + (MaxMana + ManaExpansion).ToString() + "\n" + "Стоимость повышения " + RequiredManaXP.ToString();
        }
        if (SelectedToBuy == 4 && Xp > RequiredManaRegenXp)
        {
            Xp -= RequiredManaRegenXp;
            ManaRegen += ManaRegenExpansion;
            RequiredManaRegenXp += RequiredManaRegenXp * 2;
            Stats.text = "Скорость регенерации маны сейчас = " + ManaRegen.ToString() + "\n" + "Скорость регенерации маны, если повысить уровень = " + (ManaRegen + ManaRegenExpansion).ToString() + "\n" + "Стоимость повышения " + RequiredManaRegenXp.ToString();
        }
        if (SelectedToBuy == 5 && Xp >= RequiredHPBuffXp)
        {
            Xp -= RequiredHPBuffXp;
            HPBuff += HPBuffExpansion;
            if (Skill1Empty && Skill2ID != 2)
            {
                Skill1ID = 2;
                Skill1Empty = false;
                Skill1CooldownTimer = HPBuffCooldown;
                Skill1Image.sprite = HPBuffSprite;
            }
            if (Skill2Empty && Skill1Empty == false && Skill1ID != 2)
            {
                Skill2ID = 2;
                Skill2Empty = false;
                Skill2CooldownTimer = HPBuffCooldown;
                Skill2Image.sprite = HPBuffSprite;
            }
            RequiredHPBuffXp = RequiredHPBuffXp * 2+Random.Range(1,5);
            Stats.text = "Восстановление здоровья = " + HPBuff.ToString() + "\n" + "Восстановление здоровья, если повысить уровень = " + (HPBuff + HPBuffExpansion).ToString() + "\n" + "Стоимость обновления " + RequiredHPBuffXp.ToString();
        }
        if (SelectedToBuy == 6 && Xp >= RequiredManaBuffXp)
        {
            Xp -= RequiredManaBuffXp;
            ManaBuff += ManaBuffExpansion;
            if (Skill1Empty && Skill2ID != 3)
            {
                Skill1ID = 3;
                Skill1Empty = false;
                Skill1CooldownTimer = ManaBuffCooldown;
                Skill1Image.sprite = ManaBuffSprite;
            }
            if (Skill2Empty && Skill1Empty == false && Skill1ID != 3)
            {
                Skill2ID = 3;
                Skill2Empty = false;
                Skill2CooldownTimer = ManaBuffCooldown;
                Skill2Image.sprite = ManaBuffSprite;
            }
            RequiredManaBuffXp = RequiredManaBuffXp * 2 + Random.Range(1, 5);
            Stats.text = "Восстановление маны = " + ManaBuff.ToString() + "\n" + "Восстановление маны, если повысить уровень = " + (ManaBuff + ManaBuffExpansion).ToString() + "\n" + "Стоимость обновления " + RequiredManaBuffXp.ToString();
        }
        if (SelectedToBuy == 7 && Xp >= RadDecreaseXPCost&& RadProtection>=0.2f)
        {
            Xp -= RadDecreaseXPCost;
            RadProtection -= RadDecrease;

            RadDecreaseXPCost = RadDecreaseXPCost + Random.Range(3, 7);
            Stats.text = "Защита от радиации = " + (100 - RadProtection * 100).ToString() + "\n" + "Защита от радиации, если повысить уровень = " + (100 - (RadProtection - RadDecrease) * 100).ToString() + "\n" + "Стоимость обновления " + RadDecreaseXPCost.ToString();
        }

    }
}