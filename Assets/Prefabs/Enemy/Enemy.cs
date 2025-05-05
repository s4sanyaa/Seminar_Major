using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IBehaviourTreeInterface, ITeamInterface, ISpawnInterface
{
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] Animator animator;
    [SerializeField] private PerceptionComponent _perceptionComponent;
  //  private GameObject target;
  [SerializeField] private BehaviourTree behaviourTree;
  [SerializeField] private MovementComponent MovementComponent;
  [SerializeField] private int TeamID = 2;
  private Vector3 prevPos;
  public Animator Animator
  {
      get { return animator;}
      private set { animator = value; }
  }

  public int GetTeamID()
  {
      return TeamID;
  }

  private void Awake()
  {
      _perceptionComponent.onPerceptionTargetChanged += TargetChanged;
  }

  protected virtual void Start()
    {
        if (healthComponent != null)
        {
            healthComponent.onHealthEmpty += StartDeath;
            healthComponent.onTakeDamage += TakenDamage;
        }

      
        prevPos = transform.position;
        
    }

    private void TargetChanged(GameObject target, bool sensed)
    {
        if (sensed)
        {
          //  this.target = target;
          behaviourTree.Blackboard.SetOrAddData("Target", target);
        }
        else
        {
          //  this.target = null;
          behaviourTree.Blackboard.SetOrAddData("LastSeenLoc",target.transform.position);
          behaviourTree.Blackboard.RemoveBlackboardData("Target");
        }
    }

    private void TakenDamage(float health, float delta, float maxHealth, GameObject Instigator)
    {
    }

    private void StartDeath()
    {
        TriggerDeathAnimation();
    }

    private void TriggerDeathAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Dead");
        }
    }

    public void OnDeathAnimationFinished()
    {
        Dead();
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (behaviourTree && behaviourTree.Blackboard.GetBlackboardData("Target",out GameObject target))
        {
            Vector3 drawTargetPos = target.transform.position + Vector3.up;
            Gizmos.DrawWireSphere(drawTargetPos,0.7f);
            Gizmos.DrawLine(transform.position + Vector3.up, drawTargetPos);
        }
    }

    public void RotateTowards(GameObject target, bool verticalAim = false)
    {
        Vector3 AimDir = target.transform.position - transform.position;
        AimDir.y = verticalAim ? AimDir.y : 0;
        AimDir = AimDir.normalized;

        MovementComponent.RotateTowards(AimDir);

    }

    void Update()
    {
        CalculateSpeed();
    }

    private void CalculateSpeed()
    {
        if (MovementComponent == null) return;
        Vector3 posDelta = transform.position - prevPos;
        float speed = posDelta.magnitude / Time.deltaTime;

        Animator.SetFloat("Speed", speed);
        prevPos = transform.position;
    }


    public virtual void AttackTarget(GameObject target)
    {
        
    }
    
    public void SpawnedBy(GameObject spawnerGameObject)
    {
        BehaviourTree spawnerBehaviorTree = spawnerGameObject.GetComponent<BehaviourTree>();
        if (spawnerBehaviorTree != null && spawnerBehaviorTree.Blackboard.GetBlackboardData<GameObject>("Target", out GameObject spawnerTarget))
        {
            PerceptionStimuli targetStimuli = spawnerTarget.GetComponent<PerceptionStimuli>();
            if (_perceptionComponent && targetStimuli)
            {
                _perceptionComponent.AssignPerceivedStimuli(targetStimuli);
            }
        }
    }

    protected virtual void Dead()
    {
        
    }
}
