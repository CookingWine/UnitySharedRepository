using UnityEngine;

/// <summary>
/// 列表Itm
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class RecyclingListViewItem : MonoBehaviour
{

    private RecyclingListView parentList;

    /// <summary>
    /// 循环列表
    /// </summary>
    public RecyclingListView ParentList
    {
        get => parentList;
    }

    private int currentRow;
    /// <summary>
    /// 行号
    /// </summary>
    public int CurrentRow
    {
        get => currentRow;
    }

    private RectTransform rectTransform;
    public RectTransform RectTransform
    {
        get
        {
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
            return rectTransform;
        }
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        OnAwake( );
    }
    protected virtual void OnAwake( )
    {

    }

    /// <summary>
    /// item更新响应事件
    /// </summary>
    public virtual void NotifyCurrentAssignment(RecyclingListView v, int row)
    {
        parentList = v;
        currentRow = row;
    }


}
