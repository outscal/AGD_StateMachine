using System;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using StatePattern.Main;
using StatePattern.UI;

namespace Coins
{
    public class CoinView : MonoBehaviour
    {
        [SerializeField] private float minDistance = 1f;
        [SerializeField] private float moveDuration = 10f;
        [SerializeField] private int multiplier = 500;
        
        private Vector3 playerPosition;
        private bool canMove = false;
        private void Start()
        {
            playerPosition = GameService.Instance.PlayerService.GetPlayer().Position;
        }

        private void Update()
        {
            if (Vector3.Distance(playerPosition, transform.position) <= minDistance && !canMove)
            {
                canMove = true;
                MoveCoin();
                
            }
        }
        
        private async void MoveCoin()
        {
            transform.DOMove(playerPosition,moveDuration, false).SetEase(Ease.OutQuad);
            GameService.Instance.UIService.UpdateCointCount();
            await Task.Delay((int)moveDuration * multiplier);
            DOTween.PauseAll();
            Destroy(gameObject);
        }
    }
}