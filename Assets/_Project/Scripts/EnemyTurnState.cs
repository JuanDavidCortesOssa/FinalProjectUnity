using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ConquistaGO
{
    public class EnemyTurnState : State
    {
        WorldManager worldManager;

        public EnemyTurnState(LevelManager levelManager) : base(levelManager)
        {
        }
        /// <summary>
        /// Waits untill the player finish its turn and execute the enemy's move.
        /// </summary>
        /// <returns></returns>
        public override IEnumerator Start()
        {
            yield return new WaitForSeconds(GameSettings.movementAnimationDuration);
            worldManager = WorldManager.Instance;
            worldManager.SetActive(false);
            PlayerManager.Instance.SetActive(true);

            ExecuteEnemiesMovement();

            yield return new WaitForSeconds(GameSettings.movementAnimationDuration);
            levelManager.SetState(new PlayerTurnState(levelManager));
        }

        /// <summary>
        /// Execute the movement of all the enemies stored in the worldManager 
        /// </summary>
        public void ExecuteEnemiesMovement()
        {
            for (int i = 0; i < worldManager.worldData.enemies.Count; i++)
            {
                Enemy enemy = worldManager.worldData.enemies[i];
                enemy.OnTurn(levelManager);
            }
        }
    }
}