using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] int damage = 40;
    [SerializeField] bool createAttackEvent = true;

    void Start()
    {
        // Za Deamon girl radimo na žgance, a za Zombija nam hit event na animaciji niti ne treba..on će nas jednostavno ubiti kada bude u radijusu..ili se predomislim?
        if (createAttackEvent)
            AddEvent(7, 0.04f, "AttackHitEvent", 0);

        // Ili ovako ili po tagu pretražiti...
        target = FindObjectOfType<PlayerHealth>();
    }

    // Metoda se poziva na event animacije "attack"
    // Event se aktivira u EnemyAI skripti u AttackTarget metodi
    public void AttackHitEvent()
    {
        if (target != null)
        {
            var deamonGirl = GetComponent<EnemyDemonAI>();
            target.TakeDamage(damage, deamonGirl);
        }
    }

    public void AttackHitEventZombie()
    {
        if (target != null)
        {
            var zombieObject = GetComponent<EnemyAI>();
            target.TakeDamageZombie(damage, zombieObject);
        }
    }

    // Zahvaljujući read-only animaciji, točka u kojoj neprijatelj napada moramo odraditi kroz kod na Deamon Girl
    void AddEvent(int Clip, float time, string functionName, float floatParameter)
    {
        var animator = GetComponent<Animator>();
        AnimationEvent animationEvent = new AnimationEvent();
        animationEvent.functionName = functionName;
        animationEvent.floatParameter = floatParameter;
        animationEvent.time = time;
        AnimationClip clip = animator.runtimeAnimatorController.animationClips[Clip];
        clip.AddEvent(animationEvent);
    }
}
