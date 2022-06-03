using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理任务列表
/// 挂在todolist
/// </summary>
public class TodoListManager : MonoBehaviour
{
    public List<TodoUnit> todos = new List<TodoUnit>();

    public TodoUnit todoPre;

    public float paddle;

    public float height;

    [SerializeField] private bool _isMoving = false;

    private TodoUnit _lastRemoved;

    // Start is called before the first frame update
    void Start()
    {
        height = todoPre.GetComponent<RectTransform>().rect.height;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.A))
    //     {
    //         CreatTodoUnit("啊对对对" + Time.time,"0");
    //     }
    //
    //     if (Input.GetKeyDown(KeyCode.B))
    //     {
    //         TodoIsDone("0");
    //     }
    // }


    public IEnumerator CreatTodoUnit(string text, string id)
    {
        foreach (var e in todos)
        {
            if (e.taskID == id) yield break;
        }

        TodoUnit instantiate = Instantiate(todoPre, transform);
        instantiate.transform.localPosition = new Vector3(0, -todos.Count * (height + paddle), 0);
        instantiate.SetText(text);
        instantiate.taskID = id;
        todos.Add(instantiate);
    }

    public bool TodoIsDone(string id)
    {
        if (string.IsNullOrEmpty(id)) goto DeleteOverFail;
        if (todos.Count <= 0) goto DeleteOverFail;
        if (_isMoving) goto DeleteOverFail;
        TodoUnit unit = null;
        foreach (var t in todos)
        {
            if (t.taskID == id)
            {
                unit = t;
                break;
            }
        }

        if (unit == null) goto DeleteOverFail;
        unit.IsDone();
        if (_lastRemoved) Destroy(_lastRemoved.gameObject);
        _lastRemoved = unit;
        for (int i = todos.IndexOf(unit) + 1; i < todos.Count; i++)
        {
            Vector3 lastPos = new Vector3(0, -(i - 1) * (height + paddle), 0);
            float time = 0.1f;
            if (i != todos.IndexOf(unit) + 1)
                time += (i - todos.IndexOf(unit)) * 0.01f;
            StartCoroutine(ReLocate(todos[i], lastPos, time));
        }

        todos.Remove(unit);
        return true;

        DeleteOverFail:
        {
            return false;
        }
    }

    IEnumerator ReLocate(TodoUnit unit, Vector3 tar, float time)
    {
        _isMoving = true;
        int last = todos.Count - 2;
        yield return new WaitUntil(() => _lastRemoved.isDone);
        Vector3 init = unit.transform.localPosition;
        yield return new WaitForSeconds(time);
        if (unit == null) yield break;
        float timer = 0;
        while (Vector3.Distance(unit.transform.localPosition, tar) >= 0.01f && timer < 2f)
        {
            init.y = Mathf.Lerp(init.y, tar.y, timer);
            unit.transform.localPosition = new Vector3(init.x, init.y, init.z);
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
            if (unit == null) yield break;
        }
        if (todos.IndexOf(unit) == last)
            _isMoving = false;
    }
}