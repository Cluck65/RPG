using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainerController : MonoBehaviour, Interactable, ISavable
{
    [SerializeField] string name;
    [SerializeField] Sprite sprite;
    [SerializeField] Dialog dialog;
    [SerializeField] Dialog dialogAfterBattle;

    [SerializeField] QuestBase questToStart;
    [SerializeField] QuestBase questToComplete;

    [SerializeField] GameObject exclamation;
    [SerializeField] GameObject fov;

    Quest activeQuest;

    //State
    bool battleLost = false;

    Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Start()
    {
        SetFovRotation(character.Animator.DefaultDirection);
    }

    private void Update()
    {
        character.HandleUpdate();
    }
    public IEnumerator Interact(Transform initiator)
    {
        character.LookTowards(initiator.position);
        
        if (!battleLost)
        {
            GameController.Instance.PlayTrainerPreMusic();
            yield return DialogManager.Instance.ShowDialog(dialog);
            GameController.Instance.PlayTrainerMusic();
            GameController.Instance.StartTrainerBattle(this);

        }
        else
        {
            if (questToComplete != null)
            {
                var quest = new Quest(questToComplete);
                if (quest.CanBeCompleted())
                {
                    Debug.Log($"{quest.Base.Name} completed");
                    yield return quest.CompleteQuest(initiator);
                    questToComplete = null;
                }
            }
            yield return DialogManager.Instance.ShowDialog(dialogAfterBattle);
        }

    }

    public void BattleLost()
    {
        battleLost = true;
        fov.gameObject.SetActive(false);
        
    }

    public IEnumerator TriggerTrainerBattle(PlayerController player)
    {
        //Show Exclamation
        exclamation.SetActive(true);
        GameController.Instance.PlayTrainerPreMusic();
        yield return new WaitForSeconds(0.5f);
        exclamation.SetActive(false);

        //Walk Towards Player
        var diff = player.transform.position - transform.position;
        var moveVec = diff - diff.normalized;
        moveVec = new Vector2(Mathf.Round(moveVec.x), Mathf.Round(moveVec.y));

        yield return character.Move(moveVec);

        //Show dialog
        yield return DialogManager.Instance.ShowDialog(dialog);
        GameController.Instance.PlayTrainerMusic();
        //yield return FaderAnimator.i.BattleTransition1();
        yield return Fader.i.BattleTransition();
        GameController.Instance.StartTrainerBattle(this);

    }

    public void SetFovRotation(FacingDirection dir)
    {
        float angle = 0f;
        if (dir == FacingDirection.Right)
            angle = 90f;
        else if (dir == FacingDirection.Up)
            angle = 180f;
        else if (dir == FacingDirection.Left)
            angle = 270f;

        fov.transform.eulerAngles = new Vector3(0f, 0f, angle);
    }

    public object CaptureState()
    {
        return battleLost;
    }

    public void RestoreState(object state)
    {
        battleLost = (bool)state;

        if (battleLost)
            fov.gameObject.SetActive(false);
    }

    public string Name {
        get => name;
    }

    public Sprite Sprite {
        get => sprite;
    }
    
}
