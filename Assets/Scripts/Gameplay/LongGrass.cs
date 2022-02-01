using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongGrass : MonoBehaviour, IPlayerTriggerable
{
    public void OnPlayerTriggered(PlayerController player)
    {
        if (UnityEngine.Random.Range(1, 101) <= 10)
        {
            player.Character.Animator.IsMoving = false;
            StartCoroutine(TriggerWildBattle(player));
        }
    }

    public IEnumerator TriggerWildBattle(PlayerController player)
    {
        GameController.Instance.PauseGame(true);
        GameController.Instance.PlayWildMusic();
        yield return Fader.i.BattleTransition();
        GameController.Instance.PauseGame(false);
        GameController.Instance.StartBattle();
    }

    public bool TriggerRepeatedly => true;
}
