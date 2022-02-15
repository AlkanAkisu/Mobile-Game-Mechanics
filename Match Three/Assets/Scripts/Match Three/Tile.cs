using DG.Tweening;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private float speed = 50;
    [SerializeField] private float blockSize = 0.5f;

    [SerializeField] private TileType tileType;
    public Transform spriteTransform;
    [SerializeField] private Collider2D TileColider, TileBlockerCollider;

    [Header("Raycast")] [SerializeField] private Vector3 raycastOffset;
    [SerializeField] private LayerMask tileLayerMask;
    [SerializeField] private float raycastDistance;

    private Camera _cam;
    private Tile _currentNeighbor;
    private Vector3 _currentNeighborInitialPos;
    private Vector3 _directionDrag;
    private Vector3 _dragDir;
    private Vector3 _dragStartPos;
    private Matrix _field;
    private Vector3 _initialPos;
    private float _nextPrint = 0;
    private Rigidbody2D _rb;
    private (int, int) _tileIndex;
    private bool firstInit;
    private bool reset;


    public TileTypeSO Type => tileType.Type;
    public float BlockSize => blockSize;

    public (int, int) TileIndex
    {
        get => _tileIndex;
        set
        {
            if (value == _tileIndex || firstInit)
            {
                // Nothing changed
                _tileIndex = value;
                return;
            }

            Debug.Log($"Index changed from {_tileIndex} to {value}");
            _tileIndex = value;
        }
    }

    private void Awake()
    {
        _cam = Camera.main;
        _field = transform.parent?.parent?.GetComponent<Matrix>();
        tileType = GetComponent<TileType>();
        _rb = GetComponent<Rigidbody2D>();
        reset = false;
    }

    private void Start()
    {
        firstInit = true;
        TileIndex = _field.GetTileIndex(this);
        firstInit = false;
        Physics2D.IgnoreCollision(TileColider, TileBlockerCollider, true);
    }

    private void FixedUpdate()
    {
        var hit = Physics2D.Raycast
        (
            transform.position + raycastOffset,
            Vector2.down,
            raycastDistance,
            tileLayerMask
        );
        if (hit.transform == null)
        {
            reset = false;
            return;
        }

        if (hit.rigidbody.velocity.y.Abs() < 0.1f && !reset)
        {
            _rb.velocity = Vector2.zero;
            var positionY = hit.rigidbody.position.y + blockSize;
            _rb.position = transform.position.ChangeVector(y: positionY);
            reset = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine
        (
            transform.position + raycastOffset,
            transform.position + raycastOffset + Vector3.down * raycastDistance
        );
    }


    private void OnMouseDown()
    {
        _initialPos = transform.position;
        _dragStartPos = _cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        var currentPos = _cam.ScreenToWorldPoint(Input.mousePosition);
        var drag = currentPos - _dragStartPos;
        if (_dragDir == Vector3.zero && drag.magnitude > 0.02f)
        {
            FindDragDir(drag);
            FindNeighbor(this);
        }

        if (_dragDir == Vector3.zero)
            return;
        if (_currentNeighbor == null)
            return;

        _directionDrag = Vector3.Dot(_dragDir, drag) * _dragDir;
        _directionDrag = Vector3.ClampMagnitude(_directionDrag, blockSize);
        _currentNeighbor.MoveAlongDir(-_directionDrag, _currentNeighborInitialPos);
        MoveAlongDir(_directionDrag, _initialPos);
    }

    private void OnMouseUp()
    {
        if (_currentNeighbor == null)
            return;
        if (CanBeSwitchable())
        {
            SwitchWithNeighbor();
            _currentNeighbor = null;
            _dragDir = Vector3.zero;
        }
        else
        {
            _dragDir = Vector3.zero;
            spriteTransform.DOLocalMove(Vector3.zero, 0.2f);
            _currentNeighbor.spriteTransform.DOLocalMove(Vector3.zero, 0.2f);
            _currentNeighbor = null;
        }
    }

    public void AssignField(Matrix field)
    {
        _field = field;
    }

    private void SwitchWithNeighbor()
    {
        var target = _currentNeighbor.transform.position;
        var neighborTarget = transform.position;

        MoveToNeighbor(target);
        _currentNeighbor.MoveToNeighbor(neighborTarget);


        _field.SwitchTiles(this, _currentNeighbor);

        // _field.CheckForMatch(Type, _field.GetTileIndex(this));
        // _field.CheckForMatch(_currentNeighbor.Type, _field.GetTileIndex(_currentNeighbor));
    }

    private void MoveToNeighbor(Vector3 pos)
    {
        spriteTransform.DOMove(pos, 0.2f)
            .OnComplete(() =>
            {
                spriteTransform.localPosition = Vector3.zero;
                transform.position = pos;
            });
    }

    private bool CanBeSwitchable()
    {
        return _directionDrag.magnitude > blockSize / 3f;
    }

    public void DestroyTile()
    {
        spriteTransform.DOScale(Vector3.one * 0.2f, 0.2f)
            .OnComplete(() => Destroy(gameObject));
    }

    public void CheckForMatch((int, int)? newIndex = null)
    {
        var val = newIndex ?? TileIndex;
        _field.CheckForMatch(Type, val);
    }


    // ------------------------------------      UTILS       ------------------------------------

    private void MoveAlongDir(Vector3 directionDrag, Vector3 initialPos)
    {
        var pos = spriteTransform.position;
        spriteTransform.position = Vector3.MoveTowards(pos, initialPos + directionDrag, speed * Time.deltaTime);

        spriteTransform.position = spriteTransform.position.ChangeVector(z: 0);
    }


    private void FindNeighbor(Tile tile)
    {
        _currentNeighbor = _field.GetNeighborTile(tile, _dragDir);
        if (_currentNeighbor == null)
            return;
        _currentNeighborInitialPos = _currentNeighbor.transform.position;
    }

    private void FindDragDir(Vector3 drag)
    {
        var maxDot = Mathf.NegativeInfinity;
        var vectors = new Vector3[4] {Vector3.down, Vector3.up, Vector3.right, Vector3.left};

        foreach (var vector in vectors)
        {
            var dot = Vector3.Dot(drag.normalized, vector);
            if (dot > maxDot)
            {
                _dragDir = vector;
                maxDot = dot;
            }
        }
    }
}