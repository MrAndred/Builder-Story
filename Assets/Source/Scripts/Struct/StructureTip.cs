using DG.Tweening;
using UnityEngine;

namespace BuilderStory.Struct
{
    public class StructureTip : MonoBehaviour
    {
        private const float YOffsetPosition = 0.5f;
        private const float FloatingDuration = 1f;
        private const int Loops = -1;

        [SerializeField] private float _yOriginPosition = 3f;

        private Tweener _jumping;

        private void OnDisable()
        {
            _jumping?.Kill();
        }

        public void Init()
        {
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                _yOriginPosition,
                transform.localPosition.z);

            _jumping = transform
                .DOLocalMoveY(_yOriginPosition - YOffsetPosition, FloatingDuration)
                .SetLoops(Loops, LoopType.Yoyo)
                .SetEase(Ease.Linear);
        }
    }
}
