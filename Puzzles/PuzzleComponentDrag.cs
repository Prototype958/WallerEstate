
using UnityEngine;

public class PuzzleComponentDrag : MonoBehaviour
{
    private LayerMask _puzzleLayer;
    private Camera _cam;

    private Vector3 _screenPos;
    private Vector3 _offset;

    private void Awake()
    {
        _cam = Camera.main;

        _puzzleLayer = LayerMask.GetMask("PuzzleComp");
    }

    private void OnMouseDown()
    {
        _screenPos = _cam.WorldToScreenPoint(this.transform.position);

        _offset = this.transform.position - _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPos.z));
    }

    private void OnMouseDrag()
    {
        Vector3 mousePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPos.z);

        Vector3 curPosition = _cam.ScreenToWorldPoint(mousePoint) + _offset;
        transform.position = new Vector3(curPosition.x, curPosition.y, transform.position.z);
    }
}
