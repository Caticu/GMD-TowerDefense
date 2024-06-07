I will continue talking about my progress.

After today’s class about code structure, I realized that I have to restructure my code.

namespace Assets.Scripts.Interfaces
{
    /// <summary>
    /// Interface to define  a tower
    /// </summary>
    public interface ITower
    {
        #region Properties

        /// <summary>
        /// Range of the tower
        /// </summary>
        public float Range { get; set; }
        /// <summary>
        /// Damage the tower can deal
        /// </summary>
        public float Damage { get; set; }

        /// <summary>
        /// Fire rate of the tower
        /// </summary>
        public float FireRate { get; set; }

        public float AttackTimer { get; set; }
        /// <summary>
        /// Damage type 
        /// Physical or Magic
        /// </summary>
        public DamageType DamageType { get; set; }
        
        /// <summary>
        /// Armor penetration of the tower
        /// </summary>
        public float ArmorPenetration { get; set; }
        /// <summary>
        /// Magic penetration of the tower
        /// </summary>
        public float MagicPenetration { get; set; }
        /// <summary>
        /// A tower has 3 levels and a final upgrade
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Cost to level up a tower to level 1
        /// </summary>
        public int Level1Cost { get; set; }
        /// <summary>
        /// Cost to level up a tower to level 2
        /// </summary>
        public int Level2Cost { get; set; }
        /// <summary>
        /// Cost to level up a tower to level 3
        /// </summary>
        public int Level3Cost { get; set; }
        /// <summary>
        /// Cost to level up a tower to level 4
        /// </summary>
        public int Level4Cost { get; set; }


        #endregion Properties

        #region Methods

        /// <summary>
        /// Method to lvl up a tower
        /// </summary>
        /// <param name="gold"></param>
        public void LevelUpTower(int gold);
        /// <summary>
        /// On trigger stay for towers to detect monsters
        /// This method also deals with fire rate;
        /// </summary>
        /// <param name="collision"></param>
        void OnTriggerStay2D(Collider2D collision);
        /// <summary>
        /// Spawn bullet method
        /// </summary>
        /// <param name="target"></param>
        void SpawnBullet(Transform target);

        #endregion Methods
    }
}


namespace Assets.Scripts.Towers
{
    public class Archer : MonoBehaviour, ITower
    {
        #region Properties
        public float Range { get  ; set  ; }
        public float Damage { get; set; }
        public float FireRate { get  ; set  ; }
        public float AttackTimer { get  ; set  ; }
        public DamageType DamageType { get  ; set  ; }
        public float ArmorPenetration { get  ; set  ; }
        public float MagicPenetration { get  ; set  ; }
        public int Level { get  ; set  ; }
        public int Level1Cost { get  ; set  ; }
        public int Level2Cost { get  ; set  ; }
        public int Level3Cost { get  ; set  ; }
        public int Level4Cost { get  ; set  ; }



        public FireRates fireRates = new FireRates();
        
        private  CircleCollider2D TowerCollider;

        public GameObject Arrow;
        #endregion Properties

        #region Methods

        void Start()
        {
            // Set the stats for the tower
            Range = 5;
            Damage = 20;
            FireRate = fireRates.GetFireRateAndAttackTimer("2bullets/second").Item1;
            AttackTimer = fireRates.GetFireRateAndAttackTimer("2bullets/second").Item2;
            DamageType = DamageType.Physical;
            ArmorPenetration = 20 / 100;
            MagicPenetration = 0;
            Level = 1;
            Level1Cost = 100;
            Level2Cost = 100;
            Level3Cost = 100;
            Level4Cost = 100;
            // Get tower collider
            TowerCollider = GetComponent<CircleCollider2D>();
            TowerCollider.radius = Range;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LevelUpTower(int gold)
        {
            throw new NotImplementedException();
        }

        

        public void OnTriggerStay2D(Collider2D collision)
        {
            // Check if the collision is with a monster and enough time has passed since the last shot
            if (collision.gameObject.CompareTag("Monster") && Time.time >= AttackTimer)
            {
                // Spawn bullet
                Transform targetTransform = collision.transform;
                SpawnBullet(targetTransform);

                // Update next fire time based on fire rate
                AttackTimer = Time.time + 1f / FireRate;
            }
        }

        public void SpawnBullet(Transform target)
        {
            GameObject arrowGameObject = Instantiate(Arrow, transform.position, Quaternion.identity);

            // Get the Arrow component from the instantiated GameObject
            Arrow arrow = arrowGameObject.GetComponent<Arrow>();
            if (arrow != null)
            {
                // Construct BulletInfo object with desired parameters
                BulletInfo bulletInfo = new BulletInfo
                {
                    Damage = Damage,
                    DamageType = DamageType,
                    ArmorPenetration = ArmorPenetration,
                    MagicPenetration = MagicPenetration,
                    TowerName = this.name,
                    TargetTranform = target
                    
                };

                // Initialize the arrow with the BulletInfo object
                arrow.InitializeBullet(bulletInfo);
            }
            else
            {
                Debug.LogError("Arrow component not found on instantiated object.");
            }
        }

        #endregion
    }
}

1.	Single responsibility
My tower interface is doing too many things. I have to split it for different kinds of behaviour:
-	Lvl up the tower
-	Collision detection
-	Tower stats
2.	Open close
It is open for extension and closed for modification
3.	Liskov
Since it implements an interface this is okay
4.	Interface segregation
I will make multiple interfaces
5.	dependency inversion


I rewrote the interfaces and how my tower behaves. My structure looks like that now:
![image](https://github.com/Caticu/GMD-TowerDefense/assets/36474546/55292474-d6b6-45c3-95ee-e89deac17b6c)


And my archer tower looks like:
public class Archer : MonoBehaviour
{
    #region Properties
    public ITowerStats TowerStats { get; private set; }
    public ITowerLevel TowerLevel { get; private set; }
    public ITowerShoot TowerShoot { get; private set; }

    public GameObject Arrow;
    private CircleCollider2D TowerCollider;
    #endregion Properties


    #region Methods
    private void Start()
    {
        FireRates fireRates = new FireRates();
        TowerStats = new TowerStats
        {
            Range = 5,
            Damage = 20,
            FireRate = fireRates.GetFireRateAndAttackTimer("2bullets/second").Item1,
            AttackTimer = fireRates.GetFireRateAndAttackTimer("2bullets/second").Item2,
            DamageType = DamageType.Physical,
            ArmorPenetration = 20 / 100,
            MagicPenetration = 0,
        };
        TowerLevel = new TowerLevel();
        TowerShoot = new TowerShoot();
        // Get tower collider
        TowerCollider = GetComponent<CircleCollider2D>();
        TowerCollider.radius = TowerStats.Range;
    }

    private void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        // Check if the collision is with a monster and enough time has passed since the last shot
        if (collision.gameObject.CompareTag("Monster") && Time.time >= TowerStats.AttackTimer)
        {
            // Spawn bullet
            Transform targetTransform = collision.transform;
            TowerShoot.SpawnBullet(this.transform, targetTransform, Arrow, this.TowerStats);

            // Update next fire time based on fire rate
            TowerStats.AttackTimer = Time.time + 1f / TowerStats.FireRate;
        }
    }
    #endregion Methods



}

