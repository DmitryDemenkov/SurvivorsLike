using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private Renderer _background;
    [SerializeField]
    private float _speed;

    [SerializeField]
    private Vector2 _targetRatio;
    [SerializeField]
    private Vector2 _targetTextureRation;

    private void Start()
    {
        float ratio = Screen.width / (float)Screen.height;

        float scaleY = 10;
        float scaleX = ratio * scaleY;

        _background.transform.localScale = new Vector3(scaleX, scaleY, 1f);

        float tillingY = _targetTextureRation.y;
        float tillingX = ratio * _targetRatio.y / _targetRatio.x * _targetTextureRation.x;

        _background.material.SetVector("_Tiling", new Vector2(tillingX, tillingY));
    }

    private void Update()
    {
        if (PlayerAnimationUnity.Singleton == null) return;

        Vector3 targetPosition = new Vector3(
            PlayerAnimationUnity.Singleton.transform.position.x, 
            PlayerAnimationUnity.Singleton.transform.position.y, 
            transform.position.z
        );
        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
        _background.material.SetVector("_Offset", new Vector2(transform.position.x / 2, transform.position.y / 2));
    }
}
